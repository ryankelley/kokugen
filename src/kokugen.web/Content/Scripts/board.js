var _cards = new Array();

var Card = function(card) {

    this.Id = card.Id;
    this.ColumnId = card.ColumnId;
    this.Title = card.Title;
    this.Color = card.Color;
    this.Deadline = card.Deadline;

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
 if(!(scard instanceof Card)) {
    throw("card is not an instance of Card");
 }
 
 var element = document.createElement('li');
 var head = document.createElement('div');
 var body = document.createElement('div');
 // head items
 var number = document.createElement('div')
 var size = document.createElement('div');
 var worker = document.createElement('div');
 
 element.appendChild(head);
 element.appendChild(body);
 
 head.appendChild(number);
 head.appendChild(size);
 head.appendChild(worker);
 
 body.appendChild(document.createTextNode(scard.Title));
 
 return element;
};