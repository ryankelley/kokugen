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

var rand = Math.random();

var url = 'http://' + KokugenUrl + '/project/menulist/?trash='+rand;
if(debug) {
	debugWrite('Loading projects from: '+ url);
}
    $.ajax({
        url: url,
        dataType: 'json',
        success: callback
    });
}

function loadTaskList(callback) {
var url = 'http://' + KokugenUrl + '/taskcategory/menulist?temp=' + Math.random();
if(debug) {
	debugWrite('Loading tasks from: '+ url);
}
    $.ajax({
        url:url,
        dataType: 'json',
        success: callback,
        type: 'GET'
    });
}

function loadCardList(projectId, callback) {
var url = 'http://' + KokugenUrl + '/card/allactive?trash='+Math.random();
if(debug) {
	debugWrite('Loading tasks from: '+ url);
}
    $.ajax({
        url:url,
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

