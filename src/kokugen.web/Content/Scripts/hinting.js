/** field hinting **/
$(document).ready(function() {
    $("form").each(function() { $(this).hintify() });
});
$.extend(jQuery.expr[":"], {
    textarea: function(a) { return $.nodeName(a, 'textarea'); }
});
$.fn.extend({
    hintify: function() {
        this.submit(function() {
            $("[_hint]", this).each(function() { $(this).removeHint() });
        });

        $(window).unload(function() {
            $("form [_hint]").each(function() { $(this).removeHint() });
        });

        $(":text[title],:textarea[title]", this).filter(":enabled").each(function() { $(this).hint() });

        return this;
    },
    hint: function() {
        var hintText = this.attr("title");
        if (!!hintText && (this.is(":text") || this.is(":textarea"))) {
            this.attr("_hint", hintText);
            this.addHint()
            this.focus(function() { $(this).removeHint(); });
            this.blur(function() { $(this).addHint(); });
        }
        return this;
    },
    addHint: function() {
        if ($.trim(this.val()) === "") {
            this.addClass("hinted");
            this.removeClass("active");
            this.val(this.attr("_hint"));
        } else {
            this.addClass("active");
        }
    },
    removeHint: function() {
        if ($.trim(this.val()) === this.attr("_hint")) {
            this.val("");
            this.removeClass("hinted");
        }
        this.addClass("active");
    }
});