<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ViewUserData>" %>
<%@ Import Namespace="eland.ViewData"%>
<%@ Import Namespace="eland.model"%>

<div>Username : <%= ViewData.Model.UserData.OpenId %></div>
<div>Name : <%= ViewData.Model.UserData.FirstName + " " + ViewData.Model.UserData.LastName %></div>


