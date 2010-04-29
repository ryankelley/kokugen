jQuery.extend({

	Controller: function(model, view){
		/**
		 * listen to the view
		 */
		var vlist = $.ViewListener({

			// HOOK UP TO VIEW EVENTS
		
		});
		view.addListener(vlist);

		/**
		 * listen to the model
		 */
		var mlist = $.ModelListener({
			loadBegin : function() {
				view.message("Fetching Data...");
			},
			loadFail : function() {
				view.message("ajax error");
			},
			loadFinish : function() {
				view.message("Done.");
			},
			loadItem : function(item){
				view.message("from ajax: " + item.name);
			}
		});
		model.addListener(mlist);
	}
	
});
