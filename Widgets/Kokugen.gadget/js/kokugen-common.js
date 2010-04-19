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

var TimeRecordData = function () {
    this.Description;
    this.ProjectId;
    this.TaskId;
    this.UserId;
    this.CardId;
    this.Id;
    this.Duration;
    this.Billable;
};

function startTimeRecord(TimeRecordData, callback) {
    $.ajax({
        url: 'http://' + KokugenUrl + '/timerecord',
        type: 'POST',
        data: TimeRecordData,
        dataType: 'json',
        success: callback
    });
}

function stopTimeRecord(TimeRecordData, callback) {
    $.ajax({
        url: 'http://' + KokugenUrl + '/timerecord/stop',
        type: 'POST',
        data: TimeRecordData,
        dataType: 'json',
        success: callback
    });
}

