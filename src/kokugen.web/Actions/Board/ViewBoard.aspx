<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Board.ViewBoard" AutoEventWireup="true" MasterPageFile="~/Shared/Project.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.Card"%>
<%@ Import Namespace="Kokugen.Web"%>
<%@ Import Namespace="Kokugen.Web.Actions.BoardColumn"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="Kokugen.Web.Actions.Company"%>
<%@ Import Namespace="Kokugen.Web.Actions.Board"%>
<asp:Content ID="columnconfigHead" ContentPlaceHolderID="head" runat="server">
<%= this.CSS("board.css")%>
<%= this.Script("board.js") %>
<script type="text/javascript">
    var cards = <%= Model.AllCards.ToJson() %>
    $(document).ready(function() {

        // bind resizing
        $(window).resize(setCardColumnHeight);

        // Set the initial height of the sortables based on window size
        setCardColumnHeight();

        $("#add-story-button").click(function() {
            $("#compact-card-container").slideToggle('slow');
        });

        $(".ui-sortable").sortable({ connectWith: '.ui-sortable', placeholder: 'phase-placeholder', forcePlaceholderSize: true,
            receive: cardMoved, over: cardOverColumn, update: reOrderCards });
        $(".ui-sortable").disableSelection();
        for(var i = 0; i < cards.length; i++)
        {
            var newCard = new Card(cards[i]);
            _cards.push(newCard);
            
            var hcard = buildCardDisplay(newCard);
            
            $('#'+ newCard.ColumnId).append(hcard);
        }
        
//        $("#backlog-container .ui-sortable").bind("sortremove", backlogRemove);
//        
//        $("#backlog-container .ui-sortable").bind("sortreceive", backlogReceive);
//        
//        $("#archive-container .ui-sortable").bind("sortreceive", archiveReceive);
//        $("#archive-container .ui-sortable").bind("sortremove", archiveRemove);
        
        
       setWidths();
            
            $('.backlog-toggle').click(function() {
                $('#backlog-title').toggleClass('hidden');//.animate({width: 'toggle'});
                //$('#backlog-container').animate({width: 'toggle'});
                $('#backlog-container').toggleClass('hidden');
                setWidths();
                
            });
            
            $('.archive-toggle').click(function() {
                $('#archive-title').toggleClass('hidden');
                //$('#backlog-container').animate({width: 'toggle'});
                $('#archive-container').toggleClass('hidden');
                setWidths();
                
            });
            
    });
    
    function setWidths() {
     $("div.column").not(".hidden").each(function() {
        var width = 100 / $("div.column").not(".hidden").length;
            $(this).attr("style", "width: "+ width + "%");});
    }

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
    <div id="backlog-title" class="slider left-slider">
        <div id="backlog-toggle" class="slide-title left backlog-toggle">&nbsp;</div>
    </div>
    <div id="backlog-container"  class="column hidden">
        <div class="collapse-left backlog-toggle">&nbsp;</div><div class="board-phase-header"><%= Model.BackLog.Name %></div>
        <ul class="card-list ui-sortable" id="<%= Model.BackLog.Id %>" limit="<%= Model.BackLog.CardLimit %>"></ul>
    </div>

    <%= this.PartialForEach(m => m.Columns).WithoutItemWrapper().WithoutListWrapper().Using<BoardPhase_Control>() %>

    <div id="archive-container" class="column hidden">
        <div class="board-phase-header"><%= Model.Archive.Name %></div><div class="collapse-right archive-toggle">&nbsp;</div>
        <ul class="card-list ui-sortable" id="<%= Model.Archive.Id %>" limit="<%= Model.Archive.CardLimit %>"></ul>
    </div>
    <div id="archive-title" class="slider right-slider">
        <div id="archive-toggle" class="slide-title right archive-toggle">&nbsp;</div>
    </div>
</div>
</asp:Content>