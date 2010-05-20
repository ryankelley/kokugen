jQuery.extend({

	UserView : function ($list) {
		
		var me = this;
		
		var listeners = new Array();
			
		$('.ui-draggable', $list).draggable({
			//connectToSortable: '.ui-sortable',
			helper : 'clone',
			revert: 'invalid'
		});
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
		
		this.showActive = function () {
			$list.addClass('ui-sortable-active');
		}

		this.hideActive = function () {
			$list.removeClass('ui-sortable-hover');
			$list.removeClass('ui-sortable-error');
			$list.removeClass('ui-sortable-active');
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
				log('Receive ' + this.id);
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
				log('Remove ' + this.id);
				me.notifyRemoved(ui.item, this);
			},
			stop: function(event, ui){
				log('Stop ' + this.id);
				me.notifyStopped(ui.item, this);
			},
			beforeStop: function(event, ui){
				log('BeforeStop ' + this.id);
				me.notifyBeforeStopped(ui.item, this);
			},
			update: function(event, ui){
				log('Update ' + this.id);
			},
			activate: function(event, ui){
				log('Activate ' + this.id);
				me.notifyActivated(ui.item, this);
				
			},
			deactivate : function(event, ui){
				log('Deactivate ' + this.id);
				me.hideActive();
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
				log('Drop ' + this.id);
				$list.removeClass('ui-sortable-hover');
				$list.removeClass('ui-sortable-error');
				me.notifyDropped(ui.helper, this);
			},
			activeClass: 'ui-sortable-active',
			activate: function(event, ui){
				log('Activate ' + this.id);
				me.notifyActivated(ui.helper, this);
				
			},
			deactivate : function(event, ui){
				log('Deactivate ' + this.id);
				me.hideActive();
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
		
		this.notifyActivated = function (item, sender) {
			$.each(listeners, function (i) {
					listeners[i].onActivated(item, sender);
			});
		}
		
		function log(message) {
			if(console != undefined){
				console.log(message);
			}
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
			onActivated: function(){},
			onStopped: function () {},
			removeUserClicked : function (ui) { },
			addUserClicked : function () { }
		}, list);
	}
});