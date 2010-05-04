jQuery.extend({
    
	UserWidget : function (user, canDelete) {
		var me = this;
		
		var listeners = new Array();
		
		/**
		 * add a listener to this view
		 */
		this.addListener = function(list){
			listeners.push(list);
		}
		
		this.myUser = user;

		var element = document.createElement('li');
		var $element = $(element);

		$element
			.addClass("user")
			.addClass(user.Id)
			.attr('data',JSON.stringify(user))
			.append("<span class='display'>" + user.DisplayName);

		if (user.IsOwner) {
			$element
				.find(".display")
				.append("<em class='isOwner'>(Owner)");
		} 
		
		if(canDelete) {
			//append delete from project button 

			var deleteLink = document.createElement('a');
			deleteLink.setAttribute("href", "#");
			$(deleteLink)
				.addClass('delete')
				.css('display',"inline")
				.html('<img style="float:right;" src="/content/images/btn-delete24.png" alt="delete"/>')
				.hide();

			$element.hover(function () {
				$(this).find('.delete').show();
			},
			function () {
				$(this).find('.delete').hide();
			});


			$(deleteLink).click(function () {
				me.notifyDeleteClicked(me.myUser.Id);
				return false;
			});

			element.appendChild(deleteLink);
		}
		
		this.notifyDeleteClicked = function (id) {
			$.each(listeners, function (i) {
					listeners[i].removeUserClicked(id);
			});
		}


		$element
			.append('<img class="gravatar" style="float:left; padding:0 5px;" src="' + 'http://gravatar.com/avatar/' + 					user.GravatarHash + '?s=27" alt="Gravatar Icon" />');

		me.dom = element;
		
		this.getDom = function () {
			return me.dom;
		}

    },


	UserView : function ($list) {
		
		var me = this;
		
		var listeners = new Array();
		
		/**
		 * add a listener to this view
		 */
		this.addListener = function(list){
			listeners.push(list);
		}
		
		this.removeUser = function (id) {
			$('.'+id,$list).remove();
		}
		
		this.showActive = function () {
			$list.addClass('ui-sortable-active');
		}

		this.hideActive = function () {
			$list.removeClass('ui-sortable-hover');
			$list.removeClass('ui-sortable-error');
			$list.removeClass('ui-sortable-active');
		}
		
		this.addUser = function (user) {
			var widget = new $.UserWidget(user, (!user.IsOwner));
			 
			 // lets listen to the user widgets from the left side
			// is this the right place for this???
			var widgetListener = $.ViewListener({
				removeUserClicked : function (id) {
					me.notifyDeleteClicked(id);
					me.removeUser(id);
				}
			});
			
			widget.addListener(widgetListener);
			 
			var $widget = widget.getDom();
			 
			$($widget).addClass('ui-draggable').draggable({
				//connectToSortable: '.ui-sortable',
				helper : 'clone',
				revert: 'invalid',
				start : function (event, ui) {
					ui.helper.find('.delete').hide();
					ui.helper.addClass('grip');
				}

			});
			
			$list.append($widget);
			
			$list.addClass('ui-sortable').sortable({
				receive : function (event, ui) {
					ui.item.remove();
				},
				over : function (event, ui) {
					$list.addClass('ui-sortable-hover');
				},
				out : function (event, ui) {
					$list.removeClass('ui-sortable-hover');
				},
				activate: function(event, ui){
					me.showActive();
					
				},
				deactivate : function(event, ui){
					me.hideActive();
				}
				
			});
			
		}
		
		// here we want to tell the controller that the user wants to completely remove the user 
		// from the project
		this.notifyDeleteClicked = function (id) {
			$.each(listeners, function (i) {
					listeners[i].removeUserClicked(id);
			});
		}
		
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
			var widget = new $.UserWidget(metadata, false);
			
			// lets listen to the widget added to my list
			// is this the right place for this???
			// var widgetListener = $.ViewListener({
				// removeUserClicked : function (id) {
					// me.notifyDeleteClicked(id);
				// }
			// });
			
			// widget.addListener(widgetListener);
			
			
			$list.append(widget.getDom());
		}
		
		this.removeUser = function (id) {
			$('.'+id,$list).remove();
		}

		
		/**
		*  set up listeners
		*/
		$list.sortable({
			revert: true,
			placeholder: 'user-placeholder',
			forcePlaceHolderSize: true,
			connectWith: '.ui-sortable',
			start : function (event, ui) {
				ui.item.addClass('grip').find('.delete').hide();
			},
			receive : function (event, ui) {
				Fb.log('Receive ' + this.id);
				me.notifyReceived(ui.item, ui.sender);
			},
			over : function (event, ui) {
				me.notifyOver(ui.item, this);
				$list.addClass('ui-sortable-hover');
			},
			out : function (event, ui) {
				$list.removeClass('ui-sortable-hover');
			},
			remove : function (event, ui) {
				Fb.log('Remove ' + this.id);
				me.notifyRemoved(ui.item, this);
			},
			stop: function(event, ui){
				Fb.log('Stop ' + this.id);
				me.notifyStopped(ui.item, this);
			},
			beforeStop: function(event, ui){
				Fb.log('BeforeStop ' + this.id);
				ui.item.removeClass('grip');
				me.notifyBeforeStopped(ui.item, this);
			},
			update: function(event, ui){
				Fb.log('Update ' + this.id);
			},
			activate: function(event, ui){
				Fb.log('Activate ' + this.id);
				me.notifyActivated(ui.item, this);
				
			},
			deactivate : function(event, ui){
				Fb.log('Deactivate ' + this.id);
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
			},
			drop: function (event, ui){
				Fb.log('Drop ' + this.id);
				$list.removeClass('ui-sortable-hover');
				$list.removeClass('ui-sortable-error');
				me.notifyDropped(ui.helper, this);
			},
			activeClass: 'ui-sortable-active',
			activate: function(event, ui){
				Fb.log('Activate ' + this.id);
				me.notifyActivated(ui.helper, this);
				
			},
			deactivate : function(event, ui){
				Fb.log('Deactivate ' + this.id);
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
		
		this.notifyDeleteClicked = function (id) {
			$.each(listeners, function (i) {
					listeners[i].removeUserClicked(id);
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
			onActivated: function(){},
			onStopped: function () {},
			removeUserClicked : function (id) { }
		}, list);
	}
});