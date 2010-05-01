<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.Manage.Users.ProjectUsers"
    MasterPageFile="~/Shared/Project.Master" %>

<%@ Import Namespace="Kokugen.Web.Actions.Project.Manage.Users.Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<%=this.Script("project/manage/custom.jquery.metadata.js")%>
    <%=this.Script("util.js") %>
    <%=this.Script("project/manage/view.js") %>
    <%=this.Script("project/manage/model.js")%>
    <%=this.Script("project/manage/controller.js")%>
	
    <script type="text/javascript">
        var manageUsersUi = function (users, roles) {
            
            var userView = new $.UserView($('#users-list'));

            var callBackFunction = function (response) {
                userView.addUser(response.Item);
            };

            var setupEvents = function (){
                $('#add-user').ajaxDialog({
                    onComplete: callBackFunction,
                    dataType: 'json'
                });
            };

            var setupUserView = function (){
                $.each(users, function(i){
                    userView.addUser(users[i]); 
                });
            };
			
			var setupRoleWidgets = function () {
				 $.each(roles, function(i){
					$('#role-widget-list').append(buildRoleWidget(roles[i]));
                });
			}

            var pageLoadOperations = function () {
                setupUserView();
                setupEvents();
				setupRoleWidgets();
            };
			
			var buildRoleWidget = function (role) {

				this.myRole = role;

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
				
				var view = new $.View($(userContainer));
				var model = new $.Model();
				var controller = new $.Controller(model, view);
				
				element.appendChild(head);
				element.appendChild(body);
				element.appendChild(foot);

				return element;
			};

            return {
                init:pageLoadOperations
            };

        }
        

        $(function () {
            var users = <%=Model.Users.ToJson() %>;
			var roles = <%=Model.Roles.ToJson() %>;
			
            var ui = new manageUsersUi(users, roles);
            ui.init();
        });

    </script>
    <style type="text/css">
        .user {width:300px; font-size:1.1em;cursor:-moz-grab;}
        .user > * {display:inline;}
        .grip{cursor:-moz-grabbing;}
        .user-left-side
        {
        	float: left;
            height: 100%;
            width: 300px;
            
        }
        .user-role-area
        {
           margin-left:-300px;
            height: 100%;
            overflow:hidden;
        }
        .user-role-area ul
        {
            height: 100%;
            overflow:hidden;
        }
        .user-left-side ul
        {
            height:100%;
        }
        div.role-body
        {
            height: 200px;
        }
        .role-body .ui-sortable
        {
            height: 100%;
        }
        .ui-sortable .user-placeholder
        {
            background-color: #CC4B3A;
            border-color: #903529 !important;
        }
        .ui-sortable li
        {
            padding: 5px;
        }

        .ui-draggable { padding:5px;}
        .ui-sortable > .user > .delete {display:none;}
		.ui-sortable li span { width:100%;}
		.ui-sortable li { list-style: none;}
		.user-placeholder {background-color:blue; height:30px;}
		ul.ui-sortable-hover {background-color:gray;}
		ul.ui-sortable-error{background:#f1cece !important; color:#bb2222; border: 1px solid #f19696;}
	    .ui-sortable-active{background-color:#B0B1B5;}
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
<div id="ui">
    <div id="project-users-toolbar">
        <%=this.LinkTo(new AddUserToProjectRequest(){Id = Model.ProjectId}).Text("Add User").Id("add-user")%></div>
    
        <div class="user-left-side">
           <ul id="users-list">

            </ul>
        </div>
        <div class="user-role-area">
            <ul id="role-widget-list">
            </ul>
        </div>
</div>
</asp:Content>
