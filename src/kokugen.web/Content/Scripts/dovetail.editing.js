var dovetailEditableValues = {};
var converter = new Showdown.converter();

(function($) {
    $.editable.addInputType('datepicker', {
        element: function(settings, origional) {
            var form = this;
            var input = $('<input type="button" >')
                            .datepicker({ 
                                onSelect: function() { 
                                    $(form).submit(); 
                                } 
                            });
            $(this).append(input);
            return (input);
        }
    });
    
    $.editable.addInputType('date-time-picker', {
        element: function(settings, origional) {
            var form = this;
            var input = $('<input type="button" >')
                            .datepicker({
                                showTime:true,
                                duration: '',
                                constrainInput: false,
                                onClose: function() { 
                                    $(form).submit(); 
                                }
                            });
            $(this).append(input);
            return (input);
        }
    });
   
    DovetailPropertyEditor = function() {
        var me = this;

        this.allowInPlaceEditing = function(entityId, propertySaveUrl) {
            $('.editable').makeEditable(entityId, propertySaveUrl);
            dovetail.windowManager.registerCallbackChildObjectSelected(function(eventName, selectionInfo) {
                var newId = selectionInfo.propertyValues['Id'];
                var postData = { Id: entityId, PropertyName: selectionInfo.propertyName, PropertyValue: newId };
                $.post(propertySaveUrl, postData, function() { me.onChildObjectSelected(selectionInfo); });
            });
        };

        this.onChildObjectSelected = function(callbackInfo) { };
    };

    //dovetail.editing = new DovetailPropertyEditor();

    propertyCount = function(target) {
        var count = 0;
        for (prop in target) {
            if (target.hasOwnProperty(prop)) {
                ++count;
            }
        }
        return count;
    };

    $.fn.makeEditable = function(entityId, propertySaveUrl) {
        $(this).each(function() {
            var div = $(this);
            div.html(div.html().trim());
            
            var itemMetadata = $(this).metadata({type:'attr',name:'data'});
            if (itemMetadata && itemMetadata.editoptions) {
                var editoptions = itemMetadata.editoptions;
                editoptions.EntityId = entityId;
                editoptions.SaveUrl = propertySaveUrl;
                $(this).configureEditing(editoptions);
            }
        });
    };


    $.fn.configureEditing = function(userOptions) {
        var empty = {};
        var options = $.extend(empty, $.fn.makeEditable.defaults, userOptions);
        
        var thisItem = $(this);        
        var itemData = dovetailEditableValues[thisItem[0].id];
        thisItem.data('editdata', itemData);

        var prepareInputSteps = [];

        if (userOptions.MaximumLength > 0) {
            prepareInputSteps.push(function(input) {
                input.attr("maxlength", userOptions.MaximumLength);
            });
        }
        if (userOptions.Required) {
            prepareInputSteps.push(function(input) {
                input.addClass("required");
            });
        }

        if (userOptions.MinimumValue != null) {
            prepareInputSteps.push(function(input) {
                input.attr("min", userOptions.MinimumValue);
            });
        }

        if (userOptions.MaximumValue != null) {
            prepareInputSteps.push(function(input) {
                input.attr("max", userOptions.MaximumValue);
            });
        }
        if (userOptions.IsNumber) {
            prepareInputSteps.push(function(input) {
                input.addClass("number");
            });
        }
        if (userOptions.IsNumber) {
            prepareInputSteps.push(function(input) {
                input.addClass("integer");
            });
        }

        $.editable.types["defaults"].value = function() {  
            var out = $(this).data('editdata');
            return out;          
            //return $(this).data("editdata").Value;
        };
        
        $.editable.types["defaults"].revertValue = function() {
        if(userOptions.Markdown) {
            var out = converter.makeHtml($(this).data('editdata'));
            return out;
        }
        else{
            var out = $(this).data('editdata');
            return out;
        }
        
            //return $(this).data('editdata');//.escapeHTML().convertCRLFToBreaks();
        };
        
        $.editable.types["defaults"].reset = function(settings, original) {
            original.reset(settings, original);
            $(original).removeClass("in-edit-mode");
        };

        $.editable.types["text"].plugin = function(settings, originalElement) {
            var editForm = this;
            $(this).validate();
            // need to pass in options to validate like on dovetail.crud.js  
            var inputElement = $("input:first", editForm);
            $.each(prepareInputSteps, function() {
                this.apply(editForm, [inputElement]);
            });
        };

        $.editable.types["textarea"].plugin = function(settings, originalElement) {
            $(originalElement).addClass("in-edit-mode");
        };

        $.editable.types["textarea"].content = function(input_content, settings, self) {
            var lineBreakFixedContent = input_content;
            $.editable.types["defaults"].content.apply(this, [lineBreakFixedContent, settings, self]);
        };

        var editableOptions = {
            onblur: 'submit',
            submit: '<a href="#" class="save-button" style="display:none">Save</a>',
            placeholder: userOptions.PlaceholderText || '&hellip;',
            id: "PropertyName",
            name: "PropertyValue",
            rows: 9,
            cssclass: "editing",
            tooltip: $(this).attr("id"),
            submitdata: { id: options.EntityId },
            callback: function(response, settings) {
                var element = $(this);
                element.removeClass("in-edit-mode");

                if (!response.success && response.errors) {
                    dovetail.windowManager.flashMessages(response.errors);
                }
                if(response.NewValueToDisplay || response.NewValueToDisplay === "") {
                if(userOptions.Markdown) {
                    var markdown = converter.makeHtml(response.NewValueToDisplay);
                    element.html(markdown);
                } else {
                    var escapedHTML = response.NewValueToDisplay.escapeHTML();//.convertCRLFToBreaks();
                    element.html(escapedHTML);
                    
                    }
                    element.data("editdata",response.NewValueToDisplay);
                }
                
                else {
                    element.html($("<div></div>").text(element.next().text()).html());
                }
                
                if (response.success) {
                    if(options.CallbackAfterSuccessfulEdit) {
                        window[options.CallbackAfterSuccessfulEdit]();
                    }
                    //dovetail.windowManager.refresh();
                } else {
                    this.reset();
                }
            }
        };

        if (options.MultiLine) {
            editableOptions.Markdown = options.Markdown;
            editableOptions.type = "textarea";
            editableOptions.onblur = "ignore";
            editableOptions.cols = "45";
            editableOptions.submit = '<a href="#" class="save-button button highlight"><span><span>' + options.SaveButtonText + '</span></span></a>';
            editableOptions.cancel = '<a href="#" class="cancel-button button cancel"><span><span>' + options.CancelButtonText + '</span></span></a>';
        } else if (options.IsDate && !(options.IsTime)) {
            editableOptions.type = "datepicker";
            editableOptions.onblur = "ignore";
        } else if (options.IsDate && options.IsTime) {
            editableOptions.type = "date-time-picker";
            editableOptions.onblur = "ignore";
        }
        else if (options.Choices && propertyCount(options.Choices) > 0) {
            editableOptions.type = "select";
            editableOptions.data = options.Choices;
            editableOptions.submitdata.Localize = true;
        } else {
            editableOptions.type = "text";
            if(options.RequiresExplicitUserActionForSave) {
                editableOptions.onblur = "ignore";
                editableOptions.submit = '<a href="#" class="save-button button highlight"><span><span>' + options.SaveButtonText + '</span></span></a>';
                editableOptions.cancel = '<a href="#" class="cancel-button button cancel"><span><span>' + options.CancelButtonText + '</span></span></a>';
            }
        }

        editableOptions.event = "dblclick";

        return this.editable(options.SaveUrl, editableOptions);
    };

    $.fn.makeEditable.defaults =
    {
        SaveUrl: function() { return "You did not supply a saveUrl!"; }
        , SaveButtonText: ''
        , CancelButtonText: ''
    };

})(jQuery);