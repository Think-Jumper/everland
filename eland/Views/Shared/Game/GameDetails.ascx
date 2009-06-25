<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GameIndexData>" %>
<%@ Import Namespace="eland.ViewData"%>
<%@ Import Namespace="eland.model"%>

<div style="border: 1px solid; padding: 5px">
    <div>Game Name : <%= ViewData.Model.GameSessionData.Game.Name %></div>
    <div>Game Created : <%= ViewData.Model.GameSessionData.Game.Started %></div>
    
    <div>World Name : <%= ViewData.Model.GameSessionData.Game.GameWorld.Name %></div>
    <div>Hexes : <%= ViewData.Model.GameSessionData.Game.GameWorld.Hexes.Count %></div>
    
    <div>
        <% foreach (var unit in ViewData.Model.GameSessionData.Nation.Units) %>
            <%=unit.GetType().ToString()%>
 
    </div>
</div>