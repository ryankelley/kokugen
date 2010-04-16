var _tasks = new Array();


var Task = function (task) {

    this.Id = task.Id;
    this.Name = task.Name
    

};

var buildtaskDisplay = function (t) {

    if (!(t instanceof Task)) { throw ("t is not an instance of task"); }

    var tr = document.createElement("tr");

    tr.setAttribute('id', t.Id);

    tr.mytask = t;

    var name = document.createElement("td");
    var buttons = document.createElement("td");

    tr.appendChild(name);
    tr.appendChild(buttons);

    name.appendChild(document.createTextNode(t.Name));

    var editLink = document.createElement('a');
    editLink.setAttribute("href", "#");
    var el = document.createElement('img');
    el.setAttribute('src', '/content/images/edit2.png');
    el.setAttribute('alt', 'edit');
    editLink.appendChild(el);
    $(editLink).addClass("edit-button");

    $(editLink).click(function () {
        var myParent = $(this).parent().parent()[0];
        showTaskForm(myParent.mytask);
    });

    var deleteLink = document.createElement('a');
    deleteLink.setAttribute("href", "#");
    var el = document.createElement('img');
    el.setAttribute('src', '/content/images/delete.png');
    el.setAttribute('alt', 'delete');
    deleteLink.appendChild(el);
    $(deleteLink).addClass("delete-button");


    $(deleteLink).click(function () {
        var myParent = $(this).parent().parent()[0];
        $.ajax({
            url: removetaskUrl,
            data: { Id: myParent.mytask.Id },
            success: removeFromDisplay,
            dataType: "json",
            type: "DELETE"
        });


    });

    buttons.appendChild(editLink);
    buttons.appendChild(deleteLink);

    function removeFromDisplay(response) {
        if (response.Success) {
            $(tr).remove();
        }
        else {
            $.jGrowl('You cannot remove this task', { theme: 'jgrowl-error' });
        }
    }

    return tr;
};