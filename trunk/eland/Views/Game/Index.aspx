<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GameIndexData>" %>
<%@ Import Namespace="eland.ViewData"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div>  
    
         <% if (ViewData.Model == null) { %>
               <%=Html.ActionLink("Create New", "Create") %>
         <% } else {
            Html.RenderPartial("~/Views/Shared/Game/GameDetails.ascx");
         } %>
        
    </div>

</asp:Content>

