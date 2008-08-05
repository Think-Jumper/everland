<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserDetails.ascx.cs" Inherits="eland.UserControls.User.UserDetails" %>

<div>Username : <%= ViewData.Model.OpenId %></div>
<div>Name : <%= ViewData.Model.FirstName + " " + ViewData.Model.LastName %></div>
