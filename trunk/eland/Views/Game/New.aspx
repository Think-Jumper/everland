<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<Race>>" %>
<%@ Import Namespace="eland.model"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    $(document).ready(function() {

        $(".pane-list li").click(function() {
            $(this).children("input[type=radio]").click();
        });

        $(".pane-list li").hover(showDescription, hideDescription)


        function showDescription() {
            $('#raceDetails').html($(this).find('input:last').val());
        };

        function hideDescription() {
            $('#raceDetails').html('');
        };

    }); 
</script>


    <% using (Html.BeginForm("Create", "Game", FormMethod.Post)) { %>
        <h2>New Game</h2>
                
        <div id="raceDetails" class="divDetails">
            uiouio
        </div>
        
        <label for="race">Select Race: </label>
        
        <% foreach(var race in ViewData.Model) { %>
            <ul class="pane-list">
                <li><input type="radio" id="<%= race.Name %>" name="raceId" value="<%= race.Id %>" />
                <label for="<%= race.Name %>"><%= race.Name %></label><input type="hidden" value="<%= race.Description %>" />
            </li>
            </ul>
        <% } %>
        
        <input type="submit" value="Create Game" />
    
    <% } %>

</asp:Content>
