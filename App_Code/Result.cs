
using System;

[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="v3")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="v3", IsNullable=false)]
public partial class BulkSimulationResponse {
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("EnergySystemSiteSimulationResult", IsNullable=false)]
    public BulkSimulationResponseEnergySystemSiteSimulationResult[] EnergySystemSiteSimulationResults { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string RequestURL { get; set; }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="v3")]
public partial class BulkSimulationResponseEnergySystemSiteSimulationResult {
    /// <remarks/>
    public BulkSimulationResponseEnergySystemSiteSimulationResultErrors Errors { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("SimulationPeriod", IsNullable=false)]
    public BulkSimulationResponseEnergySystemSiteSimulationResultSimulationPeriod[] SimulationPeriods { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string EnergySiteId { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Status { get; set; }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="v3")]
public partial class BulkSimulationResponseEnergySystemSiteSimulationResultErrors {
    /// <remarks/>
    public BulkSimulationResponseEnergySystemSiteSimulationResultErrorsError Error { get; set; }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="v3")]
public partial class BulkSimulationResponseEnergySystemSiteSimulationResultErrorsError {
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string StatusCode { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Message { get; set; }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="v3")]
public partial class BulkSimulationResponseEnergySystemSiteSimulationResultSimulationPeriod {
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public DateTime StartTime { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public DateTime EndTime { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Energy_kWh { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string Power_kW { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string AmbientTemperature_DegreesC { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string WindSpeed_MetersPerSecond { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string ObservationTypes { get; set; }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string PlaneOfArrayIrradiance_WattsPerMeterSquared { get; set; }
}


