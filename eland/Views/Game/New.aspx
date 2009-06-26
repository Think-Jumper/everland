<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SelectList>" %>
<%@ Import Namespace="eland.model"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm("Create", "Game", FormMethod.Post)) { %>
        <h2>New Game</h2>
        
        <label for="race">Select Race: </label>
        <%= Html.DropDownList("raceId", ViewData.Model) %>
        
        <input type="submit" value="Create Game" />
    
    <% } %>

</asp:Content>
