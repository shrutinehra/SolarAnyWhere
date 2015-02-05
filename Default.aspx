<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
    </head>
    <body>
     
        <form id="form1" runat="server">
            
            <div>
                <div style="float: left; width: 45%;border:1px solid black">
    
                    Create Site<br />
                    <br />
                    Name&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <br />
                    Latitude :&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    <br />
                    Longitude:&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="Create" OnClick="Button1_Click" />
    
                </div>
                <div style="float: right; height: 100%; width: 50%;border:1px solid black">
            
                    Date :
                    <asp:TextBox ID="TextBox6" runat="server">2011-01-01</asp:TextBox>
               
                    &nbsp; Start Time
                    <asp:DropDownList ID="drStartTimeValue" runat="server" AutoPostBack="True">
                    </asp:DropDownList>:
                    <asp:DropDownList ID="drStartFormat" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp; End Time
                    <asp:DropDownList ID="drEndtimeValue" runat="server" AutoPostBack="True">
                    </asp:DropDownList>:
                    <asp:DropDownList ID="drEndFormat" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <br />
                    <br />
            
                    Get All Site Detail<br />
          
                    <br />
                    <br />
                    <asp:Button ID="Button2" runat="server" Text="Get All Sites Detail" OnClick="Button2_Click" />
            
                    <br />
                    <br />
                    <div style="float: right; height: 100%; width: 100%;border:1px solid black">
                    Get Particular Site Detail<br />
                    <br />
                    Name:
                    <asp:TextBox ID="TextBox5" Text="The Ozarks 2" runat="server"></asp:TextBox>
                    <br />
                    <br />
            
                    <asp:Button ID="Button3" runat="server" Text="Get Detail" OnClick="Button3_Click" />
                    <br />
            </div>
                </div>

            </div>
            <div id="resultSet" runat="Server" ></div>
        </form>
    </body>
</html>