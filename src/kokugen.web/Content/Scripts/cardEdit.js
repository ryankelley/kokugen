function notReady() {
    $("ul.btns #ready").removeClass("active");
    $(".indicator.ready").addClass("hidden");
}

function makeReady() {
    $("ul.btns #ready").addClass("active");
    $(".indicator.ready").removeClass("hidden");
}

function makeBlocked() {
    $("ul.btns #block").addClass("active");
    $(".indicator.block").removeClass("hidden");
    $('div.card-blocked').removeClass("hidden");

}

function notBlocked() {
    $("ul.btns #block").removeClass("active");
    $(".indicator.block").addClass("hidden");
    $('div.card-blocked').addClass("hidden");
}


function buildBlockageForm() {
    var reasonBlocked = document.createElement('span');
    $(reasonBlocked).addClass('reason-blocked');
    reasonBlocked.appendChild(document.createTextNode(reasonBlockedMessage));

    $('div.card-blocked').append(reasonBlocked);

    var reasonForm = document.createElement('div');
    $(reasonForm).addClass("reason-form hidden");
    var container = document.createElement('div');
    var input = document.createElement('textarea');
    $(input).attr("cols", "105").attr("rows", "3").attr("name", "value").addClass("required");

    input.appendChild(document.createTextNode(reasonBlockedMessage));

    $('div.card-blocked').append(reasonForm);

    container.appendChild(input);
    reasonForm.appendChild(container);

    var submitReason = document.createElement('button');
    submitReason.appendChild(document.createTextNode('OK'));
    var cancelReason = document.createElement('button');
    cancelReason.appendChild(document.createTextNode('Cancel'));

    reasonForm.appendChild(submitReason);
    reasonForm.appendChild(cancelReason);

    $(submitReason).click(function () {
        status = "Blocked";
        reasonBlockedMessage = $(input).val();
        $.ajax({
            url: "/card/blocked",
            data: { Id: cardId, Reason: reasonBlockedMessage, Status: status },
            dataType: "json",
            type: "POST"
        });

        //$('div.card-blocked').toggleClass("hidden");
        $('div.card-blocked span').html(reasonBlockedMessage);
        $('div.card-blocked span').removeClass("hidden");
        $('div.reason-form').addClass("hidden");
        makeBlocked();
        notReady();
    });

    $(cancelReason).click(function () {
        status = "New";
        notBlocked();
    });
}

function colorChange(color) {
    $.ajax({
        url: "/card/color",
        data: { Id: cardId, Color: color },
        dataType: "json",
        type: "POST"
    });

    $('.card-number').removeClass().addClass('card-number').addClass(color);
    $(".card-color-bar").slideToggle();
    return false;
};

function updateUserGravatar(response) {
    if (response.Item != null) {
        var newImg = document.createElement('img');
        newImg.setAttribute("src", "http://gravatar.com/avatar/" + response.Item.GravatarHash + "?s=56");
        $("li.owner img").replaceWith(newImg);

        $("li.owner .user-display").html(response.Item.UserDisplay);

    }
}

var Task = function (task) {
    this.Id = task.Id;
    this.Description = task.Description;
    this.TaskOrder = task.TaskOrder;

    this.CompletedDate = task.CompletedDate === null ? null : task.CompletedDate.toString();

    if (this.CompletedDate !== null) {
        this.CompletedDate = fixDate(this.CompletedDate);
    }
    this.UserName = task.UserName;
    this.IsComplete = task.IsComplete;
    this.CardId;
}

function buildTaskLine(task) {

    var element = document.createElement('li');

    element.task = task;

    $(element).addClass('task-line');

    var move = document.createElement('span');
    $(move).addClass('task-move').html("&nbsp;");


    element.appendChild(move);

    var check = document.createElement('input');
    check.setAttribute('type', 'checkbox');
    $(check).addClass("check").attr('title', 'Click to toggle Task Completion');

    if (task.IsComplete) {
        $(check).attr('checked', 'checked');
    } else { $(check).attr('checked', ''); }

    $(check).click(function () {
        if (this.checked) {
            $.ajax({
                url: '/task/complete',
                type: 'POST',
                data: { Id: element.task.Id, IsComplete: true },
                success: element.taskComplete

            });


        } else {
            $.ajax({
                url: '/task/complete',
                type: 'POST',
                data: { Id: element.task.Id, IsComplete: false },
                success: element.taskNotComplete

            });

        }
    });

    element.appendChild(check);

    var desc = document.createElement('span');
    $(desc).addClass('task-description');

    desc.appendChild(document.createTextNode(task.Description));

    element.appendChild(desc);

    var completedInfo = document.createElement('span');
    completedInfo.appendChild(document.createTextNode(task.UserName + ' @ ' + formatDate(task.CompletedDate)));
    $(completedInfo).addClass("hidden").addClass("task-complete-info");

    if (task.IsComplete) {
        $(completedInfo).removeClass("hidden");
    }

    element.appendChild(completedInfo);

    var deleteLink = document.createElement('a');
    $(deleteLink).attr("href", "#").html("X").addClass("delete-link");
    $(deleteLink).click(function () {


        $.ajax({
            url: '/task',
            data: { Id: element.task.Id },
            type: 'DELETE',
            success: element.afterRemoved
        });

        return false;
    });

    element.afterRemoved = function () {
        $(element).remove();
    };

    element.appendChild(deleteLink);

    element.taskNotComplete = function () {
        $(completedInfo).hide();

    };

    element.taskComplete = function (response) {
        if (response && response.Success) {
            var date = fixDate(response.Item.CompletedDate);
            $(completedInfo).html(response.Item.UserName + ' @ ' + formatDate(date));
            $(completedInfo).show();


        }
    };

    $(desc).dblclick(function () {
        element.showInPlace();
    });

    var inPlace = buildInPlaceTaskEdit(element);
    element.appendChild(inPlace);


    element.showInPlace = function () {
        var width = $(element).parents("#tasks").width();
        $(inPlace).children("input").width(width - 65);
        $(inPlace).show();
        $(move).hide();
        $(check).hide();
        $(desc).hide();

        if (element.task.IsComplete) {
            $(completedInfo).hide();
        }

        $(deleteLink).hide();
    };

    element.hideInPlace = function () {
        $(inPlace).hide();
        $(move).show();
        $(check).show();
        $(desc).show();

        if (element.task.IsComplete) {
            $(completedInfo).show();
        }

        $(deleteLink).show();
    };

    element.afterSaveTask = function () {
        $(desc).html($(inPlace).children("input").val());
        element.hideInPlace();


    };

    element.UpdateOrder = function (order) {
        task.TaskOrder = order;
    }

    return element;
}

function buildInPlaceTaskEdit(element) {

    var inPlace = document.createElement('div');
    $(inPlace).addClass("task-inplace");

    var descEdit = document.createElement('input');
    descEdit.setAttribute('type', 'text');
    descEdit.setAttribute('class', 'task-description-edit');
    $(descEdit).val(element.task.Description);
    inPlace.appendChild(descEdit);

    var save = document.createElement('button');
    $(save).html('Save').addClass('task-save');

    $(save).click(function () {
        var text = $(descEdit).val();

        $.ajax({
            url: '/task',
            type: 'POST',
            data: { Id: this.parentNode.parentNode.task.Id, Description: text, CardId: this.parentNode.parentNode.task.CardId },
            success: element.afterSaveTask
        });
    });

    var cancel = document.createElement('button');
    $(cancel).html('Cancel').addClass('task-cancel');

    $(cancel).click(function () {
        element.hideInPlace();
    });

    inPlace.appendChild(save);
    inPlace.appendChild(cancel);

    $(inPlace).addClass("hidden");

    return inPlace;
}

var TaskOrderDTO = function (task) {
    if (!(task instanceof Task)) {
        throw ("task is not an instance of Task");
    }

    this.Id = task.Id;
    this.TaskOrder = task.TaskOrder;
}

function updateTaskOrder() {
    var count = 1;
    var order = $('#task-container li').each(function () {
        this.UpdateOrder(count++);
    });

    taskList.sort(function (a, b) {
        return a.Order - b.Order;
    });

    var cols = new Array();
    $(taskList).each(function () {
        cols.push(new TaskOrderDTO(this));
    });

    var data = new Object();
    data.CardId = cardId;
    data.Tasks = $.toJSON(cols);

    $.ajax({ type: 'POST', url: '/task/reorder', data: data, dataType: 'JSON' });
    //$("#info").load("process-sortable.php?" + order);
}