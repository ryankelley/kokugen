jQuery.extend({
	
	HashTable: function(){
		/**
		 * our local cache of $.Note objects
		 */
		var cache = new Array();
		/**
		 * a reference to ourselves
		 */
		var that = this;

		/**
		 * get contents of cache into an array
		 */
		this.toArray = function(){
			var a = new Array();
			for (var i in cache){
				a.push(cache[i]);
			}
			return a;
		}
		
		/**
		 * put a new item into the HashTable
		 */
		this.put = function(key, obj){
			cache[key] = obj;
		}
		
		/**
		 * clear out the item with
		 * the input key
		 */
		this.clear = function(key){
			delete cache[key];
		}
		
		/**
		 * return the item associated
		 * with key
		 */
		this.get = function(key){
			return cache[key];
		}	
	}
});

var Fb = {}; //An empty object literal for holding the function
Fb.log = function(obj, consoleMethod) {
       if (window.console && window.console.firebug && window.console.firebug.replace(/^\s\s*/, '').replace(/\s\s*$/, '') !== '') {
               if (typeof consoleMethod === "string" && typeof console[consoleMethod] === "function") {
                       console[consoleMethod](obj);
               } else {
                       console.log(obj);
               }
       }
}
