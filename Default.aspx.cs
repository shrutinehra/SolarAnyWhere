using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using WebGrease.Css.Extensions;

public partial class Default : Page
{
    private Dictionary<string, string> _energySiteList = new Dictionary<string, string>();
    private readonly List<string> _intermediateResult = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        _intermediateResult.Add("PowerAC");
        _intermediateResult.Add("EnergyAC");
        //_intermediateResult.Add("WindSpeed");
       // _intermediateResult.Add("AmbientTemperature");
        _intermediateResult.Add("PlaneOfArrayIrradiance");
        //   _intermediateResult.Add("ObservationType");
        if (!Page.IsPostBack)
        {
            var hours = Enumerable.Range(00, 24).Select(i => i.ToString("D2"));
            var minutes = Enumerable.Range(00, 60).Select(i => i.ToString("D2"));
            drStartTimeValue.DataSource = hours;

            drStartTimeValue.DataBind();
            drEndtimeValue.DataSource = hours;
            drEndtimeValue.DataBind();
            drStartFormat.DataSource = minutes;
            drStartFormat.DataBind();
            drStartTimeValue.Items.FindByValue("11").Selected = true;
           
            drEndFormat.DataSource = minutes;
            drEndFormat.DataBind();
            drEndtimeValue.Items.FindByValue("13").Selected = true;

        }
    }

    private async void HTTP_GET(bool particularSite = false)
    {
        const string uri = "https://service.solaranywhere.com/api/v1/EnergySites?key={enterKey}";

        using (var client = new HttpClient())
        {
            byte[] byteArray = Encoding.ASCII.GetBytes("UserName:Password");
            var header = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(byteArray));
            client.DefaultRequestHeaders.Authorization = header;

            HttpResponseMessage responseGet = await client.GetAsync(uri);
            //will throw an exception if not successful
            responseGet.EnsureSuccessStatusCode();

            string content = await responseGet.Content.ReadAsStringAsync();

            XDocument xdoc = XDocument.Parse(content);


            xdoc.Descendants("EnergySiteSummary").ForEach(x =>
            {
                XAttribute element = x.Attribute("EnergySiteId");
                XAttribute element1 = x.Attribute("Description");
                if (element == null) return;
                _energySiteList.Add(element.Value, element1.Value);
            });
        }

        string name = string.Empty;
        if (particularSite)
        {
            if (!string.IsNullOrEmpty(TextBox5.Text))
            {
                foreach (
                    var b in
                        _energySiteList.Where(
                            b =>
                                string.Equals(b.Value.Trim(), TextBox5.Text.Trim(),
                                    StringComparison.InvariantCultureIgnoreCase)))
                {
                    name = b.Key;
                }
            }
        }
        CreateRequestXML(name);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        const string uri = "https://service.solaranywhere.com/api/v1/EnergySites?key={Key}";
        string content;
        var uri1 = (new UriBuilder(Request.Url)).Scheme + "://" + HttpContext.Current.Request.Url.Authority + Request.ApplicationPath + "/Sample.xml";
        using (var client = new WebClient())
        {
            content = client.DownloadString(uri1);
        }
        content = content.Replace("{0}", TextBox1.Text).Replace("{1}",TextBox2.Text).Replace("{2}",TextBox3.Text);

        var httpContent = new StringContent(content, Encoding.UTF8, "application/xml");
        GetResponse(uri, httpContent);
        ScriptManager.RegisterClientScriptBlock(Button1, typeof(Button), "sas", "alert('Inserted successfully')", true);
    }

    protected async void GetResponse(string uri, StringContent httpStringContent, bool serialize = false)
    {
        try
        {


            using (var client = new HttpClient())
            {
                byte[] byteArray = Encoding.ASCII.GetBytes("Username:Password");
                var header = new AuthenticationHeaderValue(
                    "Basic", Convert.ToBase64String(byteArray));
                client.DefaultRequestHeaders.Authorization = header;
                var response = await client.PostAsync(uri, httpStringContent);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStreamAsync();
                if (serialize)
                {
                    var serializer = new XmlSerializer(typeof(BulkSimulationResponse));
                    var deserialized = (BulkSimulationResponse)serializer.Deserialize(content);
                    var strResult = new StringBuilder();
                    if (deserialized != null && deserialized.EnergySystemSiteSimulationResults != null)
                    {

                        deserialized.EnergySystemSiteSimulationResults.ForEach(x =>
                        {
                            string name = string.Empty;
                            if (x.EnergySiteId != null)
                            {
                                foreach (var b in _energySiteList.Where(b => b.Key.Trim() == x.EnergySiteId.Trim()))
                                {
                                    name = b.Value;
                                }
                            }
                            if (x.Status == "Failure")
                            {
                                strResult.Append("</br><b>Simulation Result for site " + name + "</b>");
                                strResult.Append("</br>Error Occured.Cause of Error: " + x.Errors.Error.Message);
                            }
                            else
                            {
                                strResult.Append("</br><b>Simulation Result for site " + name + "</b>");
                                x.SimulationPeriods.ForEach(y =>
                                {
                                    strResult.Append("</br>==============");
                                    strResult.Append("</br> Start Time " + y.StartTime + " End Time " + y.EndTime);
                                    strResult.Append("</br> Power " + y.Power_kW +" kW");
                                    strResult.Append("</br> Energy " + y.Energy_kWh+" kWh");
                                  //  strResult.Append("</br> Wind Speed " + y.WindSpeed_MetersPerSecond+" m/s");
                                  //  strResult.Append("</br> Ambient Temperature " + y.AmbientTemperature_DegreesC+"<sup>o</sup>C");
                                    strResult.Append("</br> PlaneOfArrayIrradiance " + y.PlaneOfArrayIrradiance_WattsPerMeterSquared+" w/m<sup>2</sup>") ;
                                });


                            }
                            strResult.Append("</br></br>=====================================================</br></br>");
                        });
                    }
                    resultSet.InnerHtml = strResult.ToString();
                }
            }
        }
        catch (Exception)
        {

            //request failed
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        _energySiteList = new Dictionary<string, string>();
        HTTP_GET();

    }


    private void CreateRequestXML(string id = "")
    {

        var doc = new XmlDocument();

        DateTime startDate = Convert.ToDateTime(TextBox6.Text + "T" + drStartTimeValue.SelectedItem.Value + ":" + drStartFormat.SelectedItem.Value);
        string startTime = String.Format("{0:s}", startDate);
        startTime += "-08:00";
        DateTime endDate = Convert.ToDateTime(TextBox6.Text + "T" + drEndtimeValue.SelectedItem.Value + ":" + drEndFormat.SelectedItem.Value);
        string endTime = String.Format("{0:s}", endDate);
        endTime += "-08:00";
        var strStartingElement = (XmlElement)doc.AppendChild(doc.CreateElement("BulkSimulationRequest"));
        strStartingElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
        strStartingElement.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
        strStartingElement.SetAttribute("xmlns", "http://service.solaranywhere.com/api/v1");
        var energySites = (XmlElement)strStartingElement.AppendChild(doc.CreateElement("EnergySiteIds"));
        if (string.IsNullOrEmpty(id))
        {
            _energySiteList.ForEach(x =>
            {
                var el1 = (XmlElement)energySites.AppendChild(doc.CreateElement("EnergySiteId"));
                el1.InnerText = x.Key;

            });
        }
        else
        {
            var el1 = (XmlElement)energySites.AppendChild(doc.CreateElement("EnergySiteId"));
            el1.InnerText = id;
        }
        var elSimulation = (XmlElement)strStartingElement.AppendChild(doc.CreateElement("SimulationOptions"));
        elSimulation.SetAttribute("PVSimulationModel", "PVFORM");
        elSimulation.SetAttribute("ShadingModel", "ShadeSimulator");

        var weatherDataOptions = (XmlElement)elSimulation.AppendChild(doc.CreateElement("WeatherDataOptions"));
        weatherDataOptions.SetAttribute("WeatherDataSource", "SolarAnywhere2_3");
        weatherDataOptions.SetAttribute("StartTime", startTime);
        weatherDataOptions.SetAttribute("EndTime", endTime);

        var intermediateResults = (XmlElement)elSimulation.AppendChild(doc.CreateElement("IntermediateResults"));
        _intermediateResult.ForEach(x =>
        {
            var el1 = (XmlElement)intermediateResults.AppendChild(doc.CreateElement("IntermediateResult"));
            el1.InnerText = x;
        });
        // string xmlString = System.IO.File.ReadAllText(@"../../Sample.xml");
        var httpContent = new StringContent(doc.InnerXml, Encoding.UTF8, "application/xml");

        const string uri = "https://service.solaranywhere.com/api/v1/BulkSimulate?key=SUSY8EE4";
        GetResponse(uri, httpContent, true);

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        _energySiteList = new Dictionary<string, string>();
        HTTP_GET(true);

    }
}
