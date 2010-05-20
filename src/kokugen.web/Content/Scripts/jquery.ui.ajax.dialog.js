/// <reference path="jquery/jquery-1.4.1-vsdoc.js" >

//depends on jquery and jquery-ui
(function ($) {
    $.ajaxDialog = {
        defaults: {
            onComplete: null,
            cache: false,
            dataType: 'html',
            data: null
        }
    }

    var launchDialog = function (id, title, config) {
        $form = $('form', id);

        //enable date time picker...
        $('.date', id).datepicker({ yearRange: 'c-100:c+0', changeMonth: true, changeYear: true });

        $form.validate({
            submitHandler: function () {
                $.ajax({
                    type: $form.attr("method"),
                    url: $form.attr("action"),
                    dataType: config.dataType,
                    data: $form.serialize(),
                    complete: function (req) {
                        config.onComplete(JSON.parse(req.responseText));
                    }
                });
            }
        });
        $(id).dialog({
            title: title ? title : '',
            autoOpen: true,
            width: config.width ? config.width : 'auto',
            modal: true,
            autoResize: true,
            position: ['center', 25],
            close: function () {
                $(id).dialog('destroy');
                if (!config.cache) {
                    $(id).remove();
                }
            },
            buttons: {

                Cancel: function () {
                    $(this).dialog('close');
                },

                'Ok': function () {
                    $form.submit();
                    if ($form.valid()) {
                        $(this).dialog('close');
                    }
                }
            }
        });
    };

    $.fn.extend({
        ajaxDialog: function (config) {
            var config = $.extend({}, $.ajaxDialog.defaults, config);
            return this.each(function () {
                $(this).click(function (e) {
                    $this = $(this);
                    e.preventDefault();
                    var tempId = $this.attr('id') + '_ajaxDialog' + new Date().getUTCMilliseconds().toString();
                    var href = $(this).attr('href');
                    $('body')
                    .append('<div id="' + tempId + '">')
                    .find('#' + tempId)
                    .load($(this).attr('href'), config.data, function () {
                        try {
                            launchDialog('#' + tempId, $this.attr('title'), config);
                        }
                        catch (err) {
                            launchDialog('#' + tempId, $this.attr('title'), config);
                        }
                    })
                });

            })
        }
    })
})(jQuery);
