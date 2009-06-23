<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ViewUserData>" %>
<%@ Import Namespace="eland.ViewData"%>
<%@ Import Namespace="eland.model"%>

<div style="border: 1px solid; padding: 5px">
    <div>Game Name : <%= ViewData.Model.GameSessionData.Game.Name %></div>
    <div>Game Created : <%= ViewData.Model.GameSessionData.Game.Started %></div>
</div>