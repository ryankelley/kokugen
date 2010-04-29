jQuery.extend({
	Model: function(){
		/**
		 * our local cache of data
		 */
		var cache = new Array();
		/**
		 * a reference to ourselves
		 */
		var me = this;
		/**
		 * who is listening to us?
		 */
		var listeners = new Array();
		
		/**
		 * get contents of cache into an array
		 */
		function toArray(){
			var a = new Array();
			for (var i in cache){
				a.push(cache[i]);
			}
			return a;
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
			var outCache = toArray();
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
			loadFail : function() { }
		}, list);
	}
});