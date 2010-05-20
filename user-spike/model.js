jQuery.extend({
	Model: function(){
		/**
		 * our local cache of data
		 */
		var cache = new $.HashTable();
		/**
		 * a reference to ourselves
		 */
		var me = this;
		/**
		 * who is listening to us?
		 */
		var listeners = new Array();
		
		this.getCount = function () {
			return cache.toArray().length;
		}
		
		/**
		 * load a json response from an
		 * ajax call
		 */
		function loadResponse(data){
			$.each(data, function(item){
				cache[data[item].id] = data[item];
				me.notifyItemLoaded(data[item]);
			});
		}
		
		this.getAll = function () {
			var outCache = cache.toArray();
			if(outCache.length) return outCache;
			
			me.notifyLoadBegin();
			
			$.ajax({
				url: '/someurl',
				data : { load : true },
				type: 'GET',
				dataType: 'json',
				timeout: 1000,
				error: function () {
					me.notifyLoadFail();
				},
				success: function (data){
					loadResponse(data);
					me.notifyLoadFinish();
				}
			});
			
		}
		
		this.addUser = function (user) {
		    cache.put(user.Id, user);
			me.notifyUserAdded(user);
		}
		
		this.removeUser = function (user) {
			cache.clear(user.Id);
		}
		
		this.canAdd = function (data) {
			var user = cache.get(data.Id);
			return user === undefined;
		}
		
		/**
		 * add a listener to this model
		 */
		this.addListener = function(list){
			listeners.push(list);
		}
		
		/**
		 * notify everone that we're starting 
		 * to load some data
		 */
		this.notifyLoadBegin = function(){
			$.each(listeners, function(i){
				listeners[i].loadBegin();
			});
		}
		/**
		 * we're done loading, tell everyone
		 */
		this.notifyLoadFinish = function(){
			$.each(listeners, function(i){
				listeners[i].loadFinish();
			});
		}
		/**
		 * we're done loading, tell everyone
		 */
		this.notifyLoadFail = function(){
			$.each(listeners, function(i){
				listeners[i].loadFail();
			});
		}
		/**
		 * tell everyone the item we've loaded
		 */
		this.notifyItemLoaded = function(item){
			$.each(listeners, function(i){
				listeners[i].loadItem(item);
			});
		}
		
		this.notifyUserAdded = function (user){
			$.each(listeners, function(i){
				listeners[i].onAddSuccessful(user);
			});
		}
		
		
	},
	
	/**
	 * let people create listeners easily
	 */
	ModelListener: function(list) {
		if(!list) list = {};
		return $.extend({
			loadBegin : function() { },
			loadFinish : function() { },
			loadItem : function() { },
			loadFail : function() { },
			onAddSuccessful : function (user) {}
		}, list);
	}
});