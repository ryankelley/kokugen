<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Card.Get" AutoEventWireup="true" MasterPageFile="~/Shared/Project.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.Card" %>
<asp:Content ID="CardGetHead" ContentPlaceHolderID="head" runat="server">
<%= this.CSS("board.css") %>
<%= this.Script("board.js") %>
<%= this.Script("showdown.js")%>
<%= this.Script("jquery.metadata.js")%>
<%= this.Script("jquery.jeditable.js")%>
<%= this.Script("dovetail.editing.js")%>

<script type="text/javascript">
    var status = "<%= Model.Status %>";
    var cardId = "<%= Model.Id %>";
    var reasonBlockedMessage = "<%= Model.BlockReason %>";

    function notReady() {
        $("ul.btns #ready").removeClass("active");
        $(".indicator.ready").addClass("hidden");
    }

    function makeReady() {
        $("ul.btns #ready").addClass("active");
        $(".indicator.ready").removeClass("hidden");
    }

    function makeBlocked() {
        $("ul.btns #block").addClass("active");
        $(".indicator.block").removeClass("hidden"); 
        $('div.card-blocked').removeClass("hidden");

    }

    function notBlocked() {
        $("ul.btns #block").removeClass("active");
        $(".indicator.block").addClass("hidden"); 
        $('div.card-blocked').addClass("hidden");
    }


    function buildBlockageForm() {
        var reasonBlocked = document.createElement('span');
        $(reasonBlocked).addClass('reason-blocked');
        reasonBlocked.appendChild(document.createTextNode(reasonBlockedMessage));

        $('div.card-blocked').append(reasonBlocked);

        var reasonForm = document.createElement('div');
        $(reasonForm).addClass("reason-form hidden");
        var container = document.createElement('div');
        var input = document.createElement('textarea');
        $(input).attr("cols", "105").attr("rows", "3").attr("name", "value").addClass("required");

        input.appendChild(document.createTextNode(reasonBlockedMessage));

        $('div.card-blocked').append(reasonForm);

        container.appendChild(input);
        reasonForm.appendChild(container);

        var submitReason = document.createElement('button');
        submitReason.appendChild(document.createTextNode('OK'));
        var cancelReason = document.createElement('button');
        cancelReason.appendChild(document.createTextNode('Cancel'));

        reasonForm.appendChild(submitReason);
        reasonForm.appendChild(cancelReason);

            $(submitReason).click(function () {
             status = "Blocked";
             reasonBlockedMessage = $(input).val();
             $.ajax({
                            url: "/card/blocked",
                            data: { Id: cardId, Reason: reasonBlockedMessage, Status: status },
                            dataType: "json",
                            type: "POST"
                        });
        
                //$('div.card-blocked').toggleClass("hidden");
                        $('div.card-blocked span').html(reasonBlockedMessage);
                        $('div.card-blocked span').removeClass("hidden");
                        $('div.reason-form').addClass("hidden");
                        makeBlocked();
                        notReady();
            });

            $(cancelReason).click(function () {
                status = "New";
                notBlocked();
            });
    }

     function colorChange(color) {
        $.ajax({
            url: "/card/color",
            data: { Id: cardId, Color: color },
            dataType: "json",
            type: "POST"
        });

        $('.card-number').removeClass().addClass('card-number').addClass(color);
        $(".card-color-bar").slideToggle();
        return false;
    };

    function updateUserGravatar(response) {
        if(response.Item != null) {
        var newImg = document.createElement('img');
        newImg.setAttribute("src", "http://gravatar.com/avatar/" + response.Item.GravatarHash + "?s=56");
         $("li.owner img").replaceWith(newImg);

         $("li.owner .user-display").html(response.Item.UserDisplay);

        }
    }

    $(document).ready(function () {
        $('#tabs').tabs();
        dovetailEditableValues = <%= Model.ToJson() %>
        $('.editable').makeEditable('<%= Model.Id %>', "/Card/update");
        buildBlockageForm();


        if(status == "Ready") {
            makeReady();
        }
        else if(status == "Blocked") {
            makeBlocked();
        }

        $("#ready-btn").click(function(){ 
            if(status == "Ready") {
                status = "New";
                 $.ajax({
                    url: "/card/ready",
                    data: { Id: cardId, Status: false },
                    dataType: "json",
                    type: "POST"
                });
                notReady();
            }
            else {
                status = "Ready";
                 $.ajax({
                    url: "/card/ready",
                    data: { Id: cardId, Status: true },
                    dataType: "json",
                    type: "POST"
                });
                makeReady();
                notBlocked();
            }
            
        });

        $("#block-btn").click(function(){ 
            if(status == "Blocked")
            {
                status = "New";
                $.ajax({
                    url: "/card/blocked",
                    data: { Id: cardId, Status: status },
                    dataType: "json",
                    type: "POST"
                });
                notBlocked();
            }
            else {
                $('div.card-blocked').removeClass("hidden");
                $('div.card-blocked span').addClass("hidden");
                $('div.reason-form').removeClass("hidden");

            }
        });
        
        $("#color").click(function() {
            
            $(".card-color-bar").slideToggle();
        });

        $("#claim").click(function() { 
            $.ajax({
                    url: "/card/claim",
                    data: { CardId: cardId },
                    dataType: "json",
                    type: "POST",
                    success: updateUserGravatar
                });
        });
        
    });

    
</script>
</asp:Content>
<asp:Content ID="CardGetMain" ContentPlaceHolderID="mainContent" runat="server">
<% this.Partial(new CompactCardFormInput{ Id = Model.ProjectId}); %>
<div class="contentWrapper">
<div class="main-panel">
        <div class="action-bar">
        <ul class="btns">
		<li id="ready"><a id="ready-btn" href="#">Ready</a></li>
		<li id="block"><a id="block-btn" href="#">Block</a></li>
		<li id="color"><a href="#">Color</a></li>
		<li id="claim"><a href="#">Claim</a></li>
		<li id="delete"><a href="#">Delete</a></li>
	</ul></div>
    <div class="card-color-bar hidden">
        <ul class="card-section" id="card-color-editor">
        <li><a href="#" class="card-color grey" onclick="colorChange('grey');">&nbsp;</a></li>
        <li><a href="#" class="card-color blue" onclick="colorChange('blue');">&nbsp;</a></li>
        <li><a href="#" class="card-color yellow" onclick="colorChange('yellow');">&nbsp;</a></li>
        <li><a href="#" class="card-color orange" onclick="colorChange('orange');">&nbsp;</a></li>
        <li><a href="#" class="card-color teal" onclick="colorChange('teal');">&nbsp;</a></li>
        </ul>
    </div>
        <div class="card-blocked hidden"></div>
        <div class="card-title"><%= this.EditInPlace(m => m.Title) %></div>
        <div class="card-process"><% this.Partial(new CardProgressModel{ProjectId = Model.ProjectId, CurrentColumnId = Model.ColumnId}); %></div>
        <div id="tabs">
            <ul>
                <li><a href="#details"><span>Details</span></a></li>
            </ul>
            <div id="details" class="card-detail"><%= this.EditInPlace(m => m.Details)%></div>
        </div>
        
</div>
</div>
<div class="right-panel">
        <ul>
            <li class="card-number <%= Model.Color %>"><%= this.DisplayFor(m => m.CardNumber) %></li>
            <li class="indicator ready hidden">Ready to pull</li>
            <li class="indicator block hidden">Blocked</li>
            <li class=""><div class="sidebar-title">Size</div><%= this.EditInPlace(m => m.Size)%></li>
            <li class=""><div class="sidebar-title">Priority</div><%= this.EditInPlace(m => m.Priority)%></li>
            <li class=""><div class="sidebar-title">Deadline</div><%= this.EditInPlace(m => m.Deadline) %></li>
            <li class="owner"><div class="sidebar-title">Owner</div><img src="<%= "http://gravatar.com/avatar/" + Model.GravatarHash + "?s=56"  %>" alt="gravatar" /><div class="user-display"><%= Model.UserDisplay %></div></li>
            <li class=""><div class="sidebar-title">Created</div><%= this.DisplayFor(m => m.Created) %></li>
            <li class=""><div class="sidebar-title">Started</div><%= this.DisplayFor(m => m.Started) %></li>
            <li class=""><div class="sidebar-title">Finished</div><%= this.DisplayFor(m => m.DateCompleted) %></li>
            
            <li class=""></li>
        </ul>
</div>

</asp:Content>