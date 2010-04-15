$(document).ready(function () {
    $(window).resize(setContentHeightToFull);
    setContentHeightToFull();
    $("input.datepicker").datepicker({ buttonImage: '/content/images/datepicker.png' });
});

function setContentHeightToFull() {
    var cheight = $(window).height() - $("#head").height();
    $(".content").attr("style", "height: " + cheight + "px;");
}



function ValidateAndSave(successCallback, formObject, clearForm) {
    var options = {
        success: successCallback,  // post-submit callback 
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute 
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type) 
        clearForm: clearForm == null ? true : clearForm        // clear all form fields after successful submit 
    };
    var isValid = formObject.valid();

    if (isValid) {
        formObject.ajaxSubmit(options);
        return false;
    }

    return false;
}

function HandleAjaxResponse(response) {
    if (typeof (response) === 'string')
        response = JSON.parse(response);

    if (response.Success) {
        $.jGrowl(response.Item, {theme:'jgrowl-dark'});

    } else {
    $.jGrowl(response.Item, {header: 'Error occurred!', sticky:true, theme:'jgrowl-error'});
    }
}

String.prototype.escapeHTML = function () {
    var val = this;
    // NOTE: the 'unescapeslashn' stuff is because in IE, innerHTML snarfs
    // \n's and destroys edit-in-place for multi-line textboxes
    val = val.replace(/\r?\n/ig, "---unescapeslashn");

    var div = document.createElement('div');
    var text = document.createTextNode(val);
    div.appendChild(text);
    var result = div.innerHTML;

    result = result.replace(/---unescapeslashn/ig, "\n");
    return result;
};

String.prototype.unescapeHTML = function () {
    var val = this;

    // NOTE: the 'unescapeslashn' stuff is because in IE, innerHTML snarfs
    // \n's and destroys edit-in-place for multi-line textboxes
    val = val.replace(/\r?\n/ig, "---unescapeslashn");
    var temp = document.createElement("pre");
    temp.innerHTML = val;
    var childNode = temp.childNodes[0];
    var result = '';
    if (childNode) {
        result = temp.childNodes[0].nodeValue;
        temp.removeChild(temp.firstChild)
    }
    result = result.replace(/---unescapeslashn/ig, "\n");
    return result;
};
