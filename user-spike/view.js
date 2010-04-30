jQuery.extend({

	UserView : function ($list) {
		
		var me = this;
		
		var listeners = new Array();
			
		$('.ui-draggable', $list).draggable({
			//connectToSortable: '.ui-sortable',
			helper : 'clone',
			revert: 'invalid'
		});

		
		
		// $list.sortable({
			// revert: true,
			// placeholder: 'user-placeholder',
			// forcePlaceHolderSize: true,
			// receive : function (event, ui) {
				// ui.item.remove();
			// },
		// });
	},

	View: function($list){

		/**
		 * keep a reference to ourselves
		 */
		var me = this;
		
		/**
		 * who is listening to us?
		 */
		var listeners = new Array();
		
		/**
		 * add a listener to this view
		 */
		this.addListener = function(list){
			listeners.push(list);
		}
		
		/**
		* public methods
		*/
		this.indicateError = function () {
			$list.addClass('ui-sortable-error');
		}
		
		this.addItem = function (metadata){
			var stringified = JSON.stringify(metadata);
			var element = $("<li><span>");
			element.find('span').text(metadata.Name);
			element.attr('data',stringified);
			$list.append(element);
		}

		
		/**
		*  set up listeners
		*/
		$list.sortable({
			revert: true,
			placeholder: 'user-placeholder',
			forcePlaceHolderSize: true,
			connectWith: '.ui-sortable',
			receive : function (event, ui) {
				console.log('Receive ' + this.id);
				me.notifyReceived(ui.item, ui.sender);
			},
			over : function (event, ui) {
				me.notifyOver(ui.item, this);
				$list.addClass('ui-sortable-hover');
			},
			out : function (event, ui) {
				$list.removeClass('ui-sortable-hover');
				$list.removeClass('ui-sortable-error');
			},
			remove : function (event, ui) {
				console.log('Remove ' + this.id);
				me.notifyRemoved(ui.item, this);
			},
			stop: function(event, ui){
				console.log('Stop ' + this.id);
			},
			beforeStop: function(event, ui){
				console.log('BeforeStop ' + this.id);
				me.notifyBeforeStopped(ui.item, this);
			},
			update: function(event, ui){
				console.log('Update ' + this.id);
			},
			activate: function(event, ui){
				console.log('Activate ' + this.id);
			},
			deactivate : function(event, ui){
				console.log('Deactivate ' + this.id);
			}
		});
		
		$list.droppable({
			accept: '.ui-draggable',
			over : function (event, ui) {
				me.notifyOver(ui.draggable, this);
				$list.addClass('ui-sortable-hover');
			},
			out : function (event, ui) {
				$list.removeClass('ui-sortable-hover');
				$list.removeClass('ui-sortable-error');
			},
			drop: function (event, ui){
				$list.removeClass('ui-sortable-hover');
				$list.removeClass('ui-sortable-error');
				me.notifyDropped(ui.helper, this);
			}
		});
		
		this.notifyReceived = function (item, sender){
			$.each(listeners, function (i) {
					listeners[i].onReceived(item, sender);
			});
		}
		
		this.notifyStopped = function (item, sender) {
			$.each(listeners, function (i) {
					listeners[i].onStopped(item, sender);
			});
		}
		
		this.notifyBeforeStopped = function (item, sender) {
			$.each(listeners, function (i) {
					listeners[i].onBeforeStopped(item, sender);
			});
		}
		
		this.notifyOver = function (item, sender) {
			$.each(listeners, function (i) {
					listeners[i].onOver(item, sender);
			});
		}
		
		this.notifyRemoved = function (item, sender) {
			$.each(listeners, function (i) {
					listeners[i].onRemoved(item, sender);
			});
		}
		
		this.notifyDropped = function (item, sender) {
			$.each(listeners, function (i) {
					listeners[i].onDropped(item, sender);
			});
		}
		
	},
	
	/**
	 * let people create listeners easily
	 */
	ViewListener: function(list) {
		if(!list) list = {};
		return $.extend({
			onReceived : function(ui, origin) { },
			onOver : function () {},
			onRemoved : function (){},
			onDropped : function () {},
			onBeforeStopped : function(){},
			removeUserClicked : function (ui) { },
			addUserClicked : function () { }
		}, list);
	}
});