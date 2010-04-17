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
        .addClass("user")
        .append("<span class='display'>" + user.DisplayName);

    if (user.IsOwner) {
        $element
            .find(".display")
            .append("<em class='isOwner'>(Owner)");
    } else {
        //append delete from project button 
        var deleteLink = document.createElement('a');
        deleteLink.setAttribute("href", user.DeleteUrl);
        $(deleteLink).addClass('ui-icon-trash').html('delete');


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
            $(element).remove();
        }
        else {
            $.jGrowl('You cannot remove this user', { theme: 'jgrowl-error' });
        }
    };

    $element
        .append('<img class="gravatar" src="' + 'http://gravatar.com/avatar/' + user.GravatarHash + '?s=27" alt="Gravatar Icon" />');

    return element;
};


var _roles = new Array();

var Role = function (role) {
    this.Id = role.Id;
    this.Name = role.Name;
};

