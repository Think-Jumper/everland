<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <% if (ViewData["Message"] != null)
       { %><div style="border: solid 1px red">
           <%= Html.Encode(ViewData["Message"].ToString())%></div>
    <% } %>
    
    <fieldset>
        <legend>Log in below</legend>
        <div style="padding:10px;">
            <p>
                Please login using an <a href="http://openid.net/get/" target="_blank"><img alt="openId" src= "../../Content/Images/Logos/openid-16x16.gif" />OpenId Provider</a></p>
            <form action="Authenticate?ReturnUrl=<%=HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]) %>"
            method="post">
            <label for="openid_identifier">
                OpenID:</label>
            <input id="openid_identifier" name="openid_identifier" size="40" /><input type="submit"
                value="Login" />
            </form>
        </div>
    </fieldset>
    
</asp:Content>
