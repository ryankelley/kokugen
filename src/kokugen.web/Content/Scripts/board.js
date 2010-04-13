var _cards = new Array();

var Card = function (card) {

    this.Id = card.Id;
    this.ColumnId = card.ColumnId;
    this.Title = card.Title;
    this.Color = card.Color;


    this.Deadline = card.Deadline === null ? null : card.Deadline.toString();
    if (this.Deadline !== null) {
        this.Deadline = new Date(parseInt(this.Deadline.replace("/Date(", "").replace(")/", ""), 10));
    }
    this.CardNumber = card.CardNumber;
    this.Size = card.Size;
    this.Priority = card.Priority;
    this.Status = card.Status;
    this.ReasonBlocked = card.BlockReason;
    this.CardOrder = card.CardOrder;
    this.ProjectId = card.ProjectId;

};


var buildCardDisplay = function (scard) {
    if (!(scard instanceof Card)) {
        throw ("card is not an instance of Card");
    }
    this.myCard = scard;

    var myTools = buildToolbar(scard);
    var colors = buildColorEditor();

    // makes outer container
    var element = document.createElement('li');
    $(element).addClass("card").addClass(scard.Color);

    var head = document.createElement('div');
    $(head).addClass("card-header");

    var body = document.createElement('div');
    $(body).addClass("card-body");
    // head items
    var number = document.createElement('div');
    $(number).addClass("card-number");

    var editLink = document.createElement('a');
    editLink.appendChild(document.createTextNode(scard.CardNumber));
    editLink.setAttribute("href", "/card/" + scard.Id);

    number.appendChild(editLink);

    var size = document.createElement('div');
    $(size).addClass("card-size").attr("title", "Estimated Size of this card");
    size.appendChild(document.createTextNode(scard.Size));
    var worker = document.createElement('div');

    // Blockage reason
    var blocked = document.createElement('div');
    $(blocked).addClass("card-blocked hidden").attr("title", "Double Click To Edit");

    var reasonBlocked = document.createElement('span');
    $(reasonBlocked).addClass('reason-blocked');
    reasonBlocked.appendChild(document.createTextNode(scard.ReasonBlocked));

    blocked.appendChild(reasonBlocked);

    var reasonForm = document.createElement('div');
    $(reasonForm).addClass("reason-form hidden");

    var input = document.createElement('textarea');
    $(input).attr("cols", "15").attr("rows", "5").attr("name", "value").addClass("required");

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

    if (scard.Status == "Ready") { $(element).addClass("ready"); }
    if (scard.Status == "Blocked") { $(element).addClass("blocked"); $(blocked).removeClass("hidden"); }

    element.appendChild(head);
    element.appendChild(body);
    element.appendChild(colors);
    element.appendChild(myTools);
    element.appendChild(blocked);

    head.appendChild(number);
    head.appendChild(size);
    head.appendChild(worker);

    body.appendChild(document.createTextNode(scard.Title));

    function unblock() {
        scard.Status = "New";
        scard.ReasonBlocked = "";
        $(element).removeClass("blocked");
        $(blocked).addClass("hidden");
        myTools.isBlocked(false);
        element.updateBlocked(false);
    }

    function notReady() {
        scard.Status = "New";
        element.isReady(false);
        myTools.isReady(false);
    }

    element.receive = function (newColumnId) {
        unblock();
        notReady();
        $.ajax({
            url: "/card/move",
            data: { Id: scard.Id, ColumnId: newColumnId },
            dataType: "json",
            type: "POST"
        });
    };

    element.colorChange = function (color) {
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
    };

    element.UpdateOrder = function (number) {
        scard.CardOrder = number;
        this.myCard = scard;
    };

    element.started = function () {
        $.ajax({
            url: "/card/dates",
            data: { Id: scard.Id, Status: "Started" },
            dataType: "json",
            type: "POST"
        });
    };

    element.notStarted = function () {
        $.ajax({
            url: "/card/dates",
            data: { Id: scard.Id, Status: "NotStarted" },
            dataType: "json",
            type: "POST"
        });
    };

    element.done = function () {
        $.ajax({
            url: "/card/dates",
            data: { Id: scard.Id, Status: "Done" },
            dataType: "json",
            type: "POST"
        });
    };

    element.notDone = function () {
        $.ajax({
            url: "/card/dates",
            data: { Id: scard.Id, Status: "NotDone" },
            dataType: "json",
            type: "POST"
        });
    };

    element.isReady = function (status) {
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
    };

    element.isBlocked = function (value) {
        scard.Status = value ? "Blocked" : "New";
        if (value) {
            $(element).addClass("blocked");
            $(blocked).removeClass("hidden");
            $(reasonBlocked).dblclick();
        }
        else {
            $(element).removeClass("blocked");
            $(blocked).addClass("hidden");
            element.updateBlocked(false);
        }

    };

    element.handleReasonBlocked = function (value) {
        element.updateBlocked(true, value);
    };

    element.updateBlocked = function (status, reason) {
        if (status) {
            var isValid = ($(input).val() != "" && $(input).val() !== undefined);
            if (isValid) {
                $.ajax({
                    url: "/card/blocked",
                    data: { Id: scard.Id, Reason: reason, Status: scard.Status },
                    dataType: "json",
                    type: "POST"
                });

                scard.ReasonBlocked = reason;
                return false;
            }
        }
        else {
            $.ajax({
                url: "/card/blocked",
                data: { Id: scard.Id, Reason: "", Status: scard.Status },
                dataType: "json",
                type: "POST"
            });
        }
    };

    $(reasonBlocked).dblclick(function () {
        $(reasonBlocked).addClass("hidden");
        $(reasonForm).removeClass("hidden");
    });

    $(submitReason).click(function () {
        element.updateBlocked(true, $(input).val());
        $(reasonForm).addClass("hidden");
        $(reasonBlocked).removeClass("hidden");
        $(reasonBlocked).html(scard.ReasonBlocked);
    });

    $(cancelReason).click(function () {
        if (scard.Status == "Blocked") {
            $(reasonForm).addClass("hidden");
            $(reasonBlocked).removeClass("hidden");
        }
        else {
            element.isBlocked(false);
        }
    });

    $(head).click(function () {
        $(this).siblings("#card-toolbar").slideToggle();
    });

    $(element).hover(function () { $(this).addClass("hover"); }, function () { $(this).removeClass("hover"); });
    return element;
};

// Used by the sortable for handling card moves
function cardMoved(event, ui) {
    $(ui.item).each(function() {
        this.receive(this.parentNode.id);
    });

    checkLimit(this);
}
function checkLimit(list) {
    $(".ui-sortable").each(function() { 
    list = this;
    var limit = $(list).attr("limit");

    if (limit !== undefined && limit != "" && limit > 0) {
        var count = $(list).children().length;
        if (count > limit) {
            $(list).parent().addClass("over-limit");
        }
        else {
            $(list).parent().removeClass("over-limit");
        }
    }
});
}
function cardOverColumn(event, ui) {
    checkLimit(this);
}

function cardMovedOut(event, ui) {
    checkLimit(this);
}

function reOrderCards(event, ui) {
    var cardsInColumn = new Array();
    var column = this;
    var count = 1;
    $(column).children().each(function() {
        this.UpdateOrder(count++);
        cardsInColumn.push(this.myCard);
    });

    cardsInColumn.sort(function(a, b) {
        return a.Order - b.Order;
    });


    var data = new Object();
    data.Cards = $.toJSON(cardsInColumn);

    $.ajax({ type: 'POST', url: '/card/reorder', data: data, dataType: 'JSON' });
    //$("#info").load("process-sortable.php?" + order);
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
            element.isReady(false);
            readyLink.parentNode.parentNode.parentNode.isReady(false);

        }
        else {
            element.isReady(true);
            readyLink.parentNode.parentNode.parentNode.isReady(true);

        }

    });

    $(blockedLink).click(function() {
        if ($(blockedLink).hasClass("checked")) {
            element.isBlocked(false);
            blockedLink.parentNode.parentNode.parentNode.isBlocked(false);
        }
        else {
            element.isBlocked(true);
            blockedLink.parentNode.parentNode.parentNode.isBlocked(true);
        }
    });

    element.isBlocked = function(blocked) {
        if (blocked) {
            $(blockedLink).addClass("checked");
            $(readyLink).addClass("hidden");
        } else {
            $(blockedLink).removeClass("checked");
            $(readyLink).removeClass("hidden");
        }
    };

    element.isReady = function(ready) {
        if (ready) {
            $(readyLink).addClass("checked");
            $(blockedLink).addClass("hidden");

        } else {
            $(readyLink).removeClass("checked");
            $(blockedLink).removeClass("hidden");

        }
    };
    
    return element;
}

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

    var buttons = buildColorButtons(['grey', 'blue',  'yellow', 'orange', 'teal']);
    for (var i in buttons) {
        element.appendChild(buttons[i]);
    }
    
    
    var cancel = document.createElement('li');
    var cancelbutton = document.createElement('button');
    $(cancelbutton).addClass("cancel");
    cancelbutton.appendChild(document.createTextNode("Cancel"));
    $(cancelbutton).click(function() { $(element).slideToggle(); });
    cancel.appendChild(cancelbutton);

    element.appendChild(cancel);

    return element;
}

function buildColorButtons(listOfColors) {
    var _colorButtons = new Array();
    for (var i in listOfColors) {
        _colorButtons.push(buildColorButton(listOfColors[i]));
    }

    return _colorButtons;
}

function buildColorButton(color) {
    var container = document.createElement('li');

    var link = document.createElement('a');
    $(link).attr("href", "#").addClass("card-color").addClass(color);
    link.appendChild(document.createTextNode("&nbsp;"));
    setupColorHover(link, color);
    container.appendChild(link);

    // Setup click functions on color links
    $(link).click(function() {
        changeColor(link, color);
    });
    
    return container;
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

function backlogRemove(event, ui) {
    ui.item[0].started();
}

function backlogReceive(event, ui) {
    ui.item[0].notStarted();
}

function archiveRemove(event, ui) {
    ui.item[0].notDone();
}

function archiveReceive(event, ui) {
    ui.item[0].done();
}
