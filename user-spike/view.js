jQuery.extend({

	UserView : function ($list) {
		
		var me = this;
		
		var listeners = new Array();
	

		
		$('.ui-draggable', $list).draggable({
			connectToSortable : '.ui-sortable',
			helper : 'clone'
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

		$list.sortable({
			revert: true,
			placeholder: 'user-placeholder',
			forcePlaceHolderSize: true,
			connectWith: '.ui-sortable',
			receive : function (event, ui) {
				me.notifyReceived(ui, this)
			},
			stop : function (event, ui) {
				me.notifyStopped(ui, this);
			},
			beforeStop: function (event, ui) {
				me.notifyBeforeStopped(ui, this);
			},
			over : function (event, ui) {
				me.notifyOver(ui, this);
				$list.addClass('ui-sortable-hover');
			},
			out : function (event, ui) {
				$list.removeClass('ui-sortable-hover');
				$list.removeClass('ui-sortable-error');
			},
			remove : function (event, ui) {
				me.notifyRemoved(ui, this);
			}
		});
		
		this.indicateError = function () {
			$list.addClass('ui-sortable-error');
		}
		
		this.notifyReceived = function (ui, origin){
			$.each(listeners, function (i) {
					listeners[i].onReceived(ui, origin);
			});
		}
		
		this.notifyStopped = function (ui, origin) {
			$.each(listeners, function (i) {
					listeners[i].onStopped(ui, origin);
			});
		}
		
		this.notifyBeforeStopped = function (ui, origin) {
			$.each(listeners, function (i) {
					listeners[i].onBeforeStopped(ui, origin);
			});
		}
		
		this.notifyOver = function (ui, origin) {
			$.each(listeners, function (i) {
					listeners[i].onOver(ui, origin);
			});
		}
		
		this.notifyRemoved = function (ui, origin) {
			$.each(listeners, function (i) {
					listeners[i].onRemoved(ui, origin);
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
			onStopped: function (ui, origin) {},
			onBeforeStopped: function(ui, origin){},
			onOver : function () {},
			onRemoved : function (){},
			removeUserClicked : function (ui) { },
			addUserClicked : function () { }
		}, list);
	}
});