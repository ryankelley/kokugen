<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Board.ViewBoard" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.Card"%>
<%@ Import Namespace="Kokugen.Web"%>
<%@ Import Namespace="Kokugen.Web.Actions.BoardColumn"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="Kokugen.Web.Actions.Company"%>
<%@ Import Namespace="Kokugen.Web.Actions.Board"%>
<asp:Content ID="columnconfigHead" ContentPlaceHolderID="head" runat="server">
<%= this.CSS("board.css")%>
<script type="text/javascript">

    $(document).ready(function() {
        $(".ui-sortable").sortable("option", "connectWith", '.ui-sortable');

        // bind resizing
        $(window).resize(setCardColumnHeight);

        // Set the initial height of the sortables based on window size
        setCardColumnHeight();

        $("#add-story-button").click(function() {
            $("#compact-card-container").slideToggle('slow');
        });

    });

    function setCardColumnHeight() {
        var newHeight = ($(window).height() - $(".card-list").position().top)-20;
        $(".card-list").attr("style", "min-height: " + newHeight + "px;");
    }

</script>
</asp:Content>
<asp:Content ID="BoardNav" ContentPlaceHolderID="extraNavigation" runat="server">
<li class="lbar"><a href="#" id="add-story-button">Add Story</a></li>
</asp:Content>
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">
<% this.Partial(new CompactCardFormInput{ Id = Model.Id}); %>
<div class="board">

    <div id="backlog-container"  class="column">
        <div class="board-phase-header"><%= Model.BackLog.Name %></div>
        <ul class="card-list ui-sortable" ></ul>
    </div>

    <%= this.PartialForEach(m => m.Columns).WithoutItemWrapper().WithoutListWrapper().Using<BoardPhase_Control>() %>

    <div id="archive-container" class="column">
        <div class="board-phase-header"><%= Model.Archive.Name %></div>
        <ul class="card-list ui-sortable" ></ul>
    </div>
</div>
</asp:Content>