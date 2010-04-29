jQuery.extend({

	View: function($console){

		/**
		 * keep a reference to ourselves
		 */
		var me = this;
		
		/**
		 * who is listening to us?
		 */
		var listeners = new Array();

		// BUILD MY VIEW HERE
		
		/**
		 * add a listener to this view
		 */
		this.addListener = function(list){
			listeners.push(list);
		}
	},
	
	/**
	 * let people create listeners easily
	 */
	ViewListener: function(list) {
		if(!list) list = {};
		return $.extend({
			/* the listener will attach its functions
			loadAllClicked : function() { },
			loadOneClicked : function() { },
			clearAllClicked : function() { }
			*/
		}, list);
	}
});