jQuery.extend({

	Controller: function(model, view, userView){
		
		function getMetadata(item) {
			return item.metadata({ type : 'attr', name : 'data'  });
		}
		
		/**
		 * listen to the view
		 */
		var vlist = $.ViewListener({

			onReceived : function (item, sender) {
			
				log('I received it!  item: ' + $(item).html() + ' sender: ' + $(sender).attr('id'));
				
				var itemmetadata = getMetadata(item);
				if(itemmetadata && itemmetadata.Id){
					log("Received: " + itemmetadata.Id);
					
					if(model.canAdd(itemmetadata)){
						model.addUser(itemmetadata);
					}else{
						sender.sortable("cancel");
						log("model can not be added");
					}
				}else{
					log("error occured on receive - no metadata!");
				}
			},
			onDropped : function (item, sender) {
			
				log('Hey! you dropped me... item: ' + $(item).html() + ' sender: ' + sender.id);
				
				var itemmetadata = getMetadata(item);
				if(itemmetadata && itemmetadata.Id){
					log("Dropped: " + itemmetadata.Id);
					
					if(model.canAdd(itemmetadata)){
						model.addUser(itemmetadata);
						view.addItem(itemmetadata);
					}else{
						item.remove();
					}
				}
			},
			onOver : function (item, sender) {
				
				log('OH you hovered me! item: ' + $(item).html() + ' sender: ' + sender.id );
				
				var itemmetadata = getMetadata(item);
				if(!model.canAdd(itemmetadata)){
					view.indicateError();
				}
			},
			onActivated : function (item, sender){
				var itemmetadata = getMetadata(item);
				
				if(!model.canAdd(itemmetadata)){
					view.indicateError();
				}else{
					view.showActive();
				}
			},
			onStopped : function (item, sender) {
				var itemmetadata = getMetadata(item);
				
				// get the child element count
				//if child element count is less that model.count
				//remove the user
				
				var elementCount = sender.childElementCount;
				
				if(model.getCount() > elementCount){
					model.removeUser(itemmetadata.Id);
				}
			},
			removeUserClicked : function (id){
				log('Dude you want me gone? Why? Id: ' + id);
				model.removeUser(id);
				view.removeUser(id);
			}
		});
		view.addListener(vlist);
		userView.addListener(vlist);

		/**
		 * listen to the model
		 */
		var mlist = $.ModelListener({
			loadBegin : function() {
				log("Fetching item...");
			},
			loadFail : function() {
				log("ajax error");
			},
			loadFinish : function() {
				log("Done.");
			},
			loadItem : function(item){
				log("from ajax: " + item.name);
			},
			onAddSuccessful : function (item) {
				log("added it! id: " + item.Id);
			}
			
		});
		model.addListener(mlist);
		
		function log(message) {
			if(console){
				console.log(message);
			}
		}
	}
	
	
	
	
});



