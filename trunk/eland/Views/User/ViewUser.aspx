<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ViewUser.aspx.cs" Inherits="eland.Views.User.ViewUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<%= Html.RenderUserControl("~/UserControls/User/UserDetails.ascx", ViewData.Model.UserData) %>


</asp:Content>
