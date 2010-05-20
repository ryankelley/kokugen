$.ajaxSetup({
    beforeSend: function(x) {
        x.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
    }
});

function loadProjects(callback) {

    $.ajax({
        url: 'http://' + getPreferenceForKey('server', true) + '/project/menulist',
        dataType: 'json',
        success: callback
    });
}

function loadTaskList(callback) {
    $.ajax({
        url: 'http://' + getPreferenceForKey('server', true) + '/taskcategory/menulist',
        dataType: 'json',
        success: callback
    });
}

function loadCardList(projectId, callback) {
    $.ajax({
        url: 'http://' + getPreferenceForKey('server', true) + '/card/allactive',
        type: 'POST',
        data: { Id: projectId },
        dataType: 'json',
        success: callback
    });
}

var TimeRecord = function(record) {
	this.Description = record.Description;
	this.ProjectName = record.ProjectName;
	this.CardDesc = record.CardTitle;
	this.Duration = record.Duration;
	this.Billable = record.Billable;
};

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
        url: 'http://' + getPreferenceForKey('server', true) + '/timerecord',
        type: 'POST',
        data: TimeRecordData,
        dataType: 'json',
        success: callback
    });
}

function stopTimeRecord(TimeRecordData, callback) {
    $.ajax({
        url: 'http://' + getPreferenceForKey('server', true) + '/timerecord/stop',
        type: 'POST',
        data: TimeRecordData,
        dataType: 'json',
        success: callback
    });
}