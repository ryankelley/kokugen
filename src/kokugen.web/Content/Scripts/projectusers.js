var _users = new Array();

var User = function (user) {

    this.Id = user.Id;
    this.DisplayName = user.DisplayName;
    this.IsOwner = user.IsOwner;
    this.GravatarHash = user.GravatarHash;
    this.DeleteUrl = user.DeleteUrl;
    this.ProjectId = user.ProjectId;

};


var buildUserWidget = function (user) {

    if (!(user instanceof User)) {
        throw ("user in not an instance of User");
    }

    var self = this;
    this.myUser = user;

    var element = document.createElement('li');
    var $element = $(element);

    $element
        .addClass("user ui-draggable")
        .addClass(user.Id)
        .data('User',user)
        .append("<span class='display'>" + user.DisplayName);

    if (user.IsOwner) {
        $element
            .find(".display")
            .append("<em class='isOwner'>(Owner)");
    } else {
        //append delete from project button 

        var deleteLink = document.createElement('a');
        deleteLink.setAttribute("href", user.DeleteUrl);
        $(deleteLink)
            .addClass('delete')
            .css('display',"inline")
            .html('<img style="float:right;" src="/content/images/btn-delete24.png" alt="delete"/>')
            .hide();

        $element.hover(function () {
            $(this).find('.delete').show();
        },
        function () {
            $(this).find('.delete').hide();
        });

        $element.hover(function () {
            $(this).find('.ui-icon').fadeIn();
        },
        function () {
            $(this).find('.ui-icon').fadeOut();
        });

        $(deleteLink).click(function (e) {
            e.preventDefault();
            $.ajax({
                url: this.href,
                data: { ProjectId: self.myUser.ProjectId, UserId: self.myUser.Id },
                success: removeUserFromDisplay,
                dataType: "json",
                type: "DELETE"
            });
        });

        element.appendChild(deleteLink);
    }

    function removeUserFromDisplay(response) {
        if (response.Success) {
            $('.'+self.myUser.Id).remove();
        }
        else {
            $.jGrowl('You cannot remove this user', { theme: 'jgrowl-error' });
        }
    };

    $element
        .append('<img class="gravatar" style="float:left; padding:0 5px;" src="' + 'http://gravatar.com/avatar/' + user.GravatarHash + '?s=27" alt="Gravatar Icon" />');



    return element;
};


var _roles = new Array();

var Role = function (role) {
    this.Id = role.Id;
    this.Name = role.Name;
};

var buildRoleWidget = function (role) {

    if (!(role instanceof Role)) {
        throw ("role in not an instance of Role");
    }

    var myUsers = new Array();

    this.myRole = role;

    var myTools = buildToolbar(role);

    var element = document.createElement('li');
    $(element).addClass('role-wrapper');

    var head = document.createElement('div');
    $(head).addClass('role-header').html(role.Name);

    var body = document.createElement('div');
    $(body).addClass('role-body');

    var foot = document.createElement('div');
    $(foot).addClass('role-footer');


    var userContainer = document.createElement('ul');
    $(userContainer).addClass('ui-sortable');

    $(userContainer).sortable({
        revert: true,
        placeholder: 'user-placeholder',
        forcePlaceholderSize: true,
        connectWith: '.ui-sortable'
    });

    body.appendChild(userContainer);

    element.appendChild(head);
    element.appendChild(body);
    element.appendChild(foot);

    return element;
};

function buildToolbar(role) {

}