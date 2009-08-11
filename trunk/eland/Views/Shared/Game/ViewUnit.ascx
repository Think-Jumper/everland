<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<eland.model.Units.Unit>" %>

<div style="border: 1px solid; padding: 5px">
    Unit
    <%= ViewData.Model.GetType().ToString() %>
</div>