jQuery.extend({

	Controller: function(model, view){
		
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
			onRemoved : function (item, sender) {
				
				log('OH you removed me! item: ' + $(item).html() + ' sender: ' + sender.id );
				
				var itemmetadata = getMetadata(item);
				if(itemmetadata && itemmetadata.Id){
					//model.removeUser(itemmetadata);
				}else{
					log("error occured on remove - no metadata!");
				}
			},
			onBeforeStopped : function (item, sender){
			
				log('Should I stop? item: ' + $(item).html() + ' sender: ' + sender.id );
				
				var itemmetadata = getMetadata(item);
				if(itemmetadata && itemmetadata.Id){
					log("Stopped: " + itemmetadata.Id);
					
					if(!model.canAdd(itemmetadata)){
						//$(sender).sortable("cancel");
					}
				}
			}
		});
		view.addListener(vlist);

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
	}
	
	
});

function log(message) {
		if(console){
			//console.log(message);
		}
	}

