jQuery.extend({

	View: function($console){
		/**
		 * keep a reference to ourselves
		 */
		var that = this;
		/**
		 * who is listening to us?
		 */
		var listeners = new Array();
	
		/**
		 * a box to put our incoming messages
		 */
		var $messages = $("<div style='height:130px; overflow: auto;'></div>");

		/**
		 * show a simple text-only message
		 * in the view
		 */
		this.message = function(str){
			$messages.append(str + "<br>");
		}
		
		/**
		 * set up the buttons to load data
		 */
		$console.append($("<input type='button' value='Load All'></input>").click(function(){
			that.notifyAllClicked();
		}));
		$console.append($("<input type='button' value='Load One'></input>").click(function(){
			that.notifyOneClicked();
		}));
		$console.append($("<input type='button' value='Clear Cache'></input>").click(function(){
			that.notifyClearAllClicked();
		}));
		$console.append($("<input type='button' value='Clear Console'></input><br><br>").click(function(){
			$messages.empty();
		}));
		$console.append($messages);
		
		
		/**
		 * add a listener to this view
		 */
		this.addListener = function(list){
			listeners.push(list);
		}
		
		/**
		 * notify everone that the user wants
		 * to load all the data
		 */
		this.notifyAllClicked = function(){
			$.each(listeners, function(i){
				listeners[i].loadAllClicked();
			});
		}
		
		/**
		 * notify everone that the user
		 * wants to load just 1 item of data
		 */
		this.notifyOneClicked = function(){
			$.each(listeners, function(i){
				listeners[i].loadOneClicked();
			});
		}
		
		/**
		 * notify everone that the user wants
		 * to clear the local cache
		 */
		this.notifyClearAllClicked = function(){
			$.each(listeners, function(i){
				listeners[i].clearAllClicked();
			});
		}
		
	},
	
	/**
	 * let people create listeners easily
	 */
	ViewListener: function(list) {
		if(!list) list = {};
		return $.extend({
			loadAllClicked : function() { },
			loadOneClicked : function() { },
			clearAllClicked : function() { }
		}, list);
	}
	
});
