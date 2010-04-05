var _cards = new Array();

var Card = function(card) {

    this.Id = card.Id;
    this.ColumnId = card.ColumnId;
    this.Title = card.Title;
    this.Color = card.Color;
    this.Deadline = card.Deadline;
    this.CardNumber = card.CardNumber;
    this.Size = card.Size;
    this.Priority = card.Priority;
    this.Status = card.Status;
    this.ReasonBlocked = card.ReasonBlocked;

    var self = this;

    this.Move = function() {
        alert("Implement the move function");
        return false;
    };

    this.Remove = function() {
        alert("Implement the remove function.");
        return false;
    };

}


var buildCardDisplay = function(scard) {
    if (!(scard instanceof Card)) {
        throw ("card is not an instance of Card");
    }

    var myTools = buildToolbar(scard);
    var colors = buildColorEditor();

    var element = document.createElement('li');
    $(element).addClass("card").addClass(scard.Color);
    var head = document.createElement('div');
    $(head).addClass("card-header");

    var body = document.createElement('div');
    $(body).addClass("card-body");
    // head items
    var number = document.createElement('div')
    $(number).addClass("card-number");

    var editLink = document.createElement('a');
    editLink.appendChild(document.createTextNode(scard.CardNumber));

    number.appendChild(editLink);

    var size = document.createElement('div');
    $(size).addClass("card-size");
    size.appendChild(document.createTextNode(scard.Size));
    var worker = document.createElement('div');

    // Blockage reason
    var blocked = document.createElement('div');
    $(blocked).addClass("card-blocked hidden").attr("title", "Double Click To Edit");

    var reasonBlocked = document.createElement('span');
    $(reasonBlocked).addClass('reason-blocked');
    reasonBlocked.appendChild(document.createTextNode(scard.ReasonBlocked));

    blocked.appendChild(reasonBlocked);

    var reasonForm = document.createElement('form');
    $(reasonForm).addClass("reason-form hidden");

    var input = document.createElement('textarea');
    $(input).attr("cols", "15").attr("rows", "5").attr("name", "value");

    input.appendChild(document.createTextNode(scard.ReasonBlocked));

    blocked.appendChild(reasonForm);
    reasonForm.appendChild(input);

    var submitReason = document.createElement('button');
    submitReason.appendChild(document.createTextNode('OK'));
    var cancelReason = document.createElement('button');
    cancelReason.appendChild(document.createTextNode('Cancel'));

    reasonForm.appendChild(submitReason);
    reasonForm.appendChild(cancelReason);



    // End blockage reason

    element.appendChild(head);
    element.appendChild(body);
    element.appendChild(colors);
    element.appendChild(myTools);
    element.appendChild(blocked);

    head.appendChild(number);
    head.appendChild(size);
    head.appendChild(worker);

    body.appendChild(document.createTextNode(scard.Title));

    element.receive = function(newColumnId) {
        $.ajax({
            url: "/card/move",
            data: { Id: scard.Id, ColumnId: newColumnId },
            dataType: "json",
            type: "POST"
        });
        //alert(newColumnId);
    };

    element.colorChange = function(color) {
        this.Color = color;

        originalColor = determineColor($(element));
        $(element).removeClass(originalColor);
        originalColor = color;
        $(element).addClass(color);

        $.ajax({
            url: "/card/color",
            data: { Id: scard.Id, Color: color },
            dataType: "json",
            type: "POST"
        });
    }

    element.isReady = function(status) {

        if (status) {
            this.Status = "Ready";
            $(element).addClass("ready");
        }
        else {
            this.Status = "New";
            $(element).removeClass("ready");
        }

        $.ajax({
            url: "/card/ready",
            data: { Id: scard.Id, Status: status },
            dataType: "json",
            type: "POST"
        });
    }

    element.isBlocked = function(value) {
        if (value) {
            $(element).addClass("blocked");
            $(blocked).removeClass("hidden");
            $(reasonBlocked).dblclick();
        }
        else {
            $(element).removeClass("blocked");
            $(blocked).addClass("hidden");
        }

        scard.Status = value ? "Blocked" : "New";

    }

    element.handleReasonBlocked = function(value) {
        element.updateBlocked(value);
    }

    element.updateBlocked = function(reason) {

        $.ajax({
            url: "/card/blocked",
            data: { Id: scard.Id, Reason: reason, Status: scard.Status },
            dataType: "json",
            type: "POST"
        });
    }

    $(reasonBlocked).dblclick(function() {
        $(reasonBlocked).addClass("hidden");
        $(reasonForm).removeClass("hidden");
    });

    $(submitReason).click(function() {
        element.handleReasonBlocked($(input).val());
    });
    $(cancelReason).click(function() {
        element.isBlocked(false);
    });

    element.reasonCallback = function(value, settings) {
        alert('the form was canceled');
    }

    //$(reasonBlocked).editable(element.handleReasonBlocked, { type: 'textarea', cancel: 'Cancel', submit: 'OK', onblur: 'ignore', cols: 5, rows: 10, event: 'dblclick', callback: element.reasonCallback });




    $(head).click(function() {
        $(this).siblings("#card-toolbar").slideToggle();
    });

    $(element).hover(function() { $(this).addClass("hover"); }, function() { $(this).removeClass("hover"); });
    return element;
};


function cardMoved(event, ui) {
    $(ui.item).each(function() {
        this.receive(this.parentNode.id);
    });
}

function buildToolbar(card) {

    var element = document.createElement('ul');
    
    element.setAttribute("class", "hidden card-section");
    element.setAttribute("id", "card-toolbar");
    
    //$(element).attr("id", "card-toolbar").addClass("hidden");

    var up = document.createElement('li');
    up.setAttribute("class", "icon");

    var upLink = document.createElement('a');
    upLink.setAttribute("class", "card-up");
    upLink.appendChild(document.createTextNode("Up"));

    up.appendChild(upLink);

    var down = document.createElement('li');
    var downLink = document.createElement('a');
    downLink.setAttribute("class", "card-down");
    downLink.appendChild(document.createTextNode("Down"));
    down.setAttribute("class", "icon");

    down.appendChild(downLink);
    
    var color = document.createElement('li');
    color.setAttribute("class", "icon");

    var colorLink = document.createElement('a');
    colorLink.setAttribute("class", "color-wheel");
    colorLink.appendChild(document.createTextNode("Color"));

    $(colorLink).click(function() {
        $(this).parent().parent().siblings("#card-color-editor").slideToggle();
    });
    
    color.appendChild(colorLink);
    
    var claim = document.createElement('li');
    claim.setAttribute("class", "icon");

    var claimLink = document.createElement('a');
    claimLink.appendChild(document.createTextNode("Claim"));
    claim.appendChild(claimLink);
    
    var ready = document.createElement('li');
    ready.setAttribute("class", "checkbox");

    var readyLink = buildAnchor("Ready", "#", "card-toolbar-ready");
    ready.appendChild(readyLink);

   

    var blocked = document.createElement('li');
    blocked.setAttribute("class", "checkbox");

    var blockedLink = buildAnchor("Blocked", "#", "card-toolbar-blocked");
    blocked.appendChild(blockedLink);

    

    element.appendChild(up);
    element.appendChild(down);
    element.appendChild(color);
    element.appendChild(claim);
    element.appendChild(ready);
    element.appendChild(blocked);

    if (card.Status == "Ready") {
        $(blockedLink).addClass("hidden");
        $(readyLink).addClass("checked");
    }
    else if (card.Status == "Blocked") {
        $(readyLink).addClass("hidden");
        $(blockedLink).addClass("checked");
    }

    $(readyLink).click(function() {
        if ($(readyLink).hasClass("checked")) {
            $(readyLink).removeClass("checked");
            readyLink.parentNode.parentNode.parentNode.isReady(false);
            $(blockedLink).removeClass("hidden");
        }
        else {
            $(readyLink).addClass("checked");
            readyLink.parentNode.parentNode.parentNode.isReady(true);
            $(blockedLink).addClass("hidden");
        }

    });

    $(blockedLink).click(function() {
    if ($(blockedLink).hasClass("checked")) {
        $(blockedLink).removeClass("checked");
        blockedLink.parentNode.parentNode.parentNode.isBlocked(false);
        $(readyLink).removeClass("hidden");
    }
    else {
        $(blockedLink).addClass("checked");
        blockedLink.parentNode.parentNode.parentNode.isBlocked(true);
        $(readyLink).addClass("hidden");
    }
    });
    
    return element;
};

function buildAnchor(text, href, cssClass) {
    var element = document.createElement('a');
    element.setAttribute("class", cssClass);
    element.setAttribute("href", href);
    element.appendChild(document.createTextNode(text));
    return element;
}

function buildColorEditor() {
    var element = document.createElement('ul');
    $(element).addClass("card-section").attr("id", "card-color-editor").addClass("hidden");

    var grey = document.createElement('li');
    var blue = document.createElement('li');
    var red = document.createElement('li');
    var green = document.createElement('li');
    var yellow = document.createElement('li');
    var orange = document.createElement('li');
    var teal = document.createElement('li');
    var cancel = document.createElement('li');

    var greyLink = document.createElement('a');
    $(greyLink).attr("rel", "grey").attr("href", "#").addClass("card-color grey");
    greyLink.appendChild(document.createTextNode("&nbsp;"));
    setupColorHover(greyLink, "grey");
    grey.appendChild(greyLink);
    
    var blueLink = document.createElement('a');
    $(blueLink).attr("rel", "blue").attr("href", "#").addClass("card-color blue");
    blueLink.appendChild(document.createTextNode("&nbsp;"));

    setupColorHover(blueLink, "blue");
    blue.appendChild(blueLink);
    
    var redLink = document.createElement('a');
    $(redLink).attr("rel", "red").attr("href", "#").addClass("card-color red");
    redLink.appendChild(document.createTextNode("&nbsp;"));
    setupColorHover(redLink, "red");
    
    red.appendChild(redLink);
    
    var greenLink = document.createElement('a');
    $(greenLink).attr("rel", "green").attr("href", "#").addClass("card-color green");
    greenLink.appendChild(document.createTextNode("&nbsp;"));
    setupColorHover(greenLink, "green");
    green.appendChild(greenLink);
    
    var yellowLink = document.createElement('a');
    $(yellowLink).attr("rel", "yellow").attr("href", "#").addClass("card-color yellow");
    yellowLink.appendChild(document.createTextNode("&nbsp;"));
    setupColorHover(yellowLink, "yellow");
    yellow.appendChild(yellowLink);
    
    var orangeLink = document.createElement('a');
    $(orangeLink).attr("rel", "orange").attr("href", "#").addClass("card-color orange");
    orangeLink.appendChild(document.createTextNode("&nbsp;"));
    setupColorHover(orangeLink, "orange");
    orange.appendChild(orangeLink);
    
    var tealLink = document.createElement('a');
    $(tealLink).attr("rel", "teal").attr("href", "#").addClass("card-color teal");
    setupColorHover(tealLink, "teal");
    
    tealLink.appendChild(document.createTextNode("&nbsp;"));
    
    
    
    teal.appendChild(tealLink);

    var cancelbutton = document.createElement('button');
    $(cancelbutton).addClass("cancel");
    cancelbutton.appendChild(document.createTextNode("Cancel"));
    $(cancelbutton).click(function() { $(element).slideToggle(); });
    cancel.appendChild(cancelbutton);

    element.appendChild(grey);
    element.appendChild(blue);
    element.appendChild(red);
    element.appendChild(green);
    element.appendChild(yellow);
    element.appendChild(orange);
    element.appendChild(teal);
    element.appendChild(cancel);

    // Setup click functions on color links
    $(greyLink).click(function() {
        changeColor(greyLink, "grey");
    });
    
    $(blueLink).click(function() {
        changeColor(blueLink, "blue");
    });
    
    $(redLink).click(function() {
        changeColor(redLink, "red");
    });
    
    $(greenLink).click(function() {
        changeColor(greenLink, "green");
    });
    
    $(yellowLink).click(function() {
        changeColor(yellowLink, "yellow");
    });

    $(orangeLink).click(function() {
        changeColor(orangeLink, "orange");
    });
    
    $(tealLink).click(function() {
        changeColor(tealLink, "teal");
    });
    
    return element;
}
function changeColor(element, color) {
    //$(element).hover(function() { }, function() { });
    element.parentNode.parentNode.parentNode.colorChange(color);
    $(element.parentNode.parentNode).slideToggle();
    
}
var originalColor = "";

function setupColorHover(element, color) {
    $(element).hover(function() {
        originalColor = determineColor($(this).parent().parent().parent());
        $(this).parent().parent().parent().removeClass(originalColor);
        $(this).parent().parent().parent().addClass(color);
    },
    function() { $(this).parent().parent().parent().removeClass(color); 
    $(this).parent().parent().parent().addClass(originalColor);
    });
}

function determineColor(element) {

    if(element.hasClass("grey")) {
        return "grey"; }
    if(element.hasClass("blue")) {
        return "blue"; } 
    if(element.hasClass("red")) {
        return "red"; } 
    if(element.hasClass("green")) {
        return "green"; } 
    if(element.hasClass("yellow")) {
        return "yellow"; } 
    if(element.hasClass("orange")) {
        return "orange"; }
    if(element.hasClass("teal")) {
        return "teal"; }
  
}

