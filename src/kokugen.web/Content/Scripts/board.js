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