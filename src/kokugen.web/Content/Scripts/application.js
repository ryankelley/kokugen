$(document).ready(function() {
    $(window).resize(setContentHeightToFull);
    $(".datepicker").datepicker({ buttonImage: '/content/images/datepicker.png' });
});

function setContentHeightToFull() {
    var cheight = $(window).height() - $("#head").height();
    $(".content").attr("style", "height: " + cheight + "px;");
}



function ValidateAndSave(successCallback, formObject) {
    var options = {
        success: successCallback,  // post-submit callback 
        type: 'post',        // 'get' or 'post', override for form's 'method' attribute 
        dataType: 'json',        // 'xml', 'script', or 'json' (expected server response type) 
        clearForm: true        // clear all form fields after successful submit 
    };
    var isValid = formObject.valid();

    if (isValid) {
        formObject.ajaxSubmit(options);
        return false;
    }

    return false;
}

