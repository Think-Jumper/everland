<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="New.aspx.cs" Inherits="eland.Views.User.New" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<% using(Html.BeginForm("Create", "User")) { %>


        <div>
            <table>
                <tr>
                    <td>First Name</td>
                    <td><%= Html.TextBox("FirstName", ViewData["FirstName"])%></td>
                </tr>
                <tr>
                    <td>Last Name</td>
                    <td><%= Html.TextBox("LastName")%></td>
                </tr>
                <tr>
                    <td>Email</td>
                    <td><%= Html.TextBox("Email", ViewData["Email"])%></td>
                </tr>
            </table>
        </div>
        
        <div><input type="submit" value="Submit" /></div>

<% } %>



</asp:Content>
