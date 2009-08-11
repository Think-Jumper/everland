<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<Race>>" %>
<%@ Import Namespace="eland.model"%>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    $(document).ready(function() {

        var clicked = false;
        var lastSelect = '';

        $(".pane-list li").click(function() {
            lastSelect = $(this).find('input:last').val();
            $('#raceDetails').html(lastSelect);
            $(this).find('input:first').attr("checked","checked");
            clicked = true;
        });

        $(".pane-list li").hover(showDescription, hideDescription)

        function showDescription() {
            $('#raceDetails').html($(this).find('input:last').val());
        };

        function hideDescription() {
            if (clicked == false) {
                $('#raceDetails').html('');
            } else {
                $('#raceDetails').html(lastSelect);
            }

        };

    }); 
</script>

    <div id="wrapper">
        <div id="leftcolumn">
            <% using (Html.BeginForm("Create", "Game", FormMethod.Post)) { %>
            
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
        </div>
        <div id="rightcolumn">
            <div id="raceDetails">
                uiouio
            </div>
        </div>
    </div>    

</asp:Content>
