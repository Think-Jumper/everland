<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<GameIndexData>" %>
<%@ Import Namespace="eland.ViewData"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div>  
           <% Html.RenderPartial("~/Views/Shared/Game/GameDetails.ascx"); %>
    </div>

</asp:Content>

