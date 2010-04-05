<<<<<<< HEAD
var _cards = new Array();

var Card = function(card) {

    this.Id = card.Id;
    this.ColumnId = card.ColumnId;
    this.Title = card.Title;
    this.Color = card.Color;
    this.Deadline = card.Deadline;
    this.CardNumber = card.CardNumber;
    this.Size = card.Size;

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

    var element = document.createElement('li');
    $(element).addClass("card").addClass("grey");
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

    element.appendChild(head);
    element.appendChild(body);

    head.appendChild(number);
    head.appendChild(size);
    head.appendChild(worker);

    body.appendChild(document.createTextNode(scard.Title));

    return element;
};
=======
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

    element.appendChild(head);
    element.appendChild(body);
    element.appendChild(colors);
    element.appendChild(myTools);

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

    if (card.Status == "Ready") {
        $(blockedLink).addClass("hidden");
        $(readyLink).addClass("checked");
    }
    else if (card.Status == "Blocked") {
    $(readyLink).addClass("hidden");
    $(blockedLink).addClass("checked");
    }

    element.appendChild(up);
    element.appendChild(down);
    element.appendChild(color);
    element.appendChild(claim);
    element.appendChild(ready);
    element.appendChild(blocked);

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
>>>>>>> Lots of work on the JS for board
