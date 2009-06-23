<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="ViewUser.aspx.cs" Inherits="eland.Views.User.ViewUser" %>
<%@ Import Namespace="eland.model.Units"%>
<%@ Import Namespace="eland.Controllers"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<% Html.RenderPartial("~/Views/Shared/User/UserDetails.ascx"); %>
<% Html.RenderPartial("~/Views/Shared/Game/GameDetails.ascx"); %>


<%= Html.ActionLink("Create Unit", "CreateUnit", "Game" ) %>

<% foreach(var unit in Model.GameSessionData.) %>

</asp:Content>
