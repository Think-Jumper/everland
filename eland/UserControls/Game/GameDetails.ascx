<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="eland.model"%>

<div style="border: 1px solid; padding: 3px">
    <div>Game Name : <%= ((GameSession)ViewData.Model).Game.Name %></div>
    <div>Game Created : <%= ((GameSession)ViewData.Model).Game.Started %></div>
    <div>Hex Count: <%= ((GameSession)ViewData.Model).Game.GameWorld.Hexes.Count %></div>
</div>