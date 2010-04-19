function login(callback) {
    $.ajax({
        url: 'http://' + KokugenUrl + '/account/loginajax',
        dataType: 'json',
        type: 'POST',
        data: { Login: UserName, Password: Password },
        success: callback
    });
}

function loadProjects(callback) {
    $.ajax({
        url: 'http://' + KokugenUrl + '/project/menulist',
        dataType: 'json',
        success: callback
    });
}

function loadTaskList(callback) {
    $.ajax({
        url: 'http://' + KokugenUrl + '/taskcategory/menulist',
        dataType: 'json',
        success: callback
    });
}

function loadCardList(projectId, callback) {
    $.ajax({
        url: 'http://' + KokugenUrl + '/card/allactive',
        type: 'POST',
        data: { Id: projectId },
        dataType: 'json',
        success: callback
    });
}