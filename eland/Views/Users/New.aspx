<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%
    using (Html.BeginForm("CreateUser", "Users"))
    {%>


        <div>
            <table>
                <tr>
                    <td>First Name</td>
                    <td><%=Html.TextBox("firstName")%></td>
                </tr>
                <tr>
                    <td>Last Name</td>
                    <td><%=Html.TextBox("lastName")%></td>
                </tr>
                <tr>
                    <td>Email</td>
                    <td><%=Html.TextBox("email")%></td>
                </tr>
            </table>
        </div>
        
        <div><input type="submit" value="Submit" /></div>

<%
    }%>
    






</asp:Content>
