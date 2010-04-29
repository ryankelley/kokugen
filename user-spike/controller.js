jQuery.extend({

	Controller: function(model, view){
		/**
		 * listen to the view
		 */
		var vlist = $.ViewListener({

			onReceived : function (data, origin) {
				log('I received it!  sender: ' + $(data.sender).attr('id') + ' me: ' + origin.id);
				
				
				
				var itemMetadata = data.item.metadata({ type : 'attr', name : 'data'  });
				if(itemMetadata && itemMetadata.Id){
					log("Received: " + itemMetadata.Id);
					
					if(model.canAdd(itemMetadata)){
						model.addUser(itemMetadata);
					}else{
						data.sender.sortable("cancel");
					}
				}else{
					log("error occured on receive - no metadata!");
				}
			},
			onStopped : function (data, origin) {
				log('I stopped helper: ' + $(data.helper).html() + ' me: ' + origin.id);
			},
			onOver : function (data, origin) {
				log('OH you hovered me! helper: ' + $(data.helper).html() + ' sender: ' + $(data.sender).attr('id') + ' me: ' + origin.id );
				var itemMetadata = data.item.metadata({ type : 'attr', name : 'data'  });
				if(!model.canAdd(itemMetadata)){
					view.indicateError();
				}
			},
			onRemoved : function (data, origin) {
				log('OH you removed me! item: ' + $(data.item).html() + ' me: ' + origin.id );
				var itemMetadata = data.item.metadata({ type : 'attr', name : 'data'  });
				if(itemMetadata && itemMetadata.Id){
					model.removeUser(itemMetadata);
				}else{
					log("error occured on remove - no metadata!");
				}
			}
		
		});
		view.addListener(vlist);

		/**
		 * listen to the model
		 */
		var mlist = $.ModelListener({
			loadBegin : function() {
				log("Fetching Data...");
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
			onAddSuccessful : function (data) {
				log("added it! id: " + $(data).html());
			}
			
		});
		model.addListener(mlist);
	}
	
	
});

function log(message) {
		if(console){
			console.log(message);
		}
	}

