<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.Manage.Users.ProjectUsers"
    MasterPageFile="~/Shared/Project.Master" %>

<%@ Import Namespace="Kokugen.Web.Actions.Project.Manage.Users.Add" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
    <%=this.Script("projectusers.js") %>
    <script type="text/javascript">
      $(function () {

             // bind resizing
        $(window).resize(setUserColumnHeight);

        // Set the initial height of the sortables based on window size
        setUserColumnHeight();

        function setUserColumnHeight(){
            $('#project-users-list').attr("style","min-height:"+ $(window).height() + "px;");
        };

            var users = <%=Model.Users.ToJson() %>;
            
            for(var i in users){
                var user = new User(users[i]);

                _users.push(user);

                $('#project-users-list').append(buildUserWidget(user));
            }

            $('#add-user').ajaxDialog({onComplete:addUserToList,dataType:'json'});

            function addUserToList(data){
                var $data = JSON.parse(data);
                if($data.Success){
                    var user = new User($data.Item);
                    _users.push(user);
                    $('#project-users-list').append(buildUserWidget(user));
                }else{
                    $.jGrowl('Error occured adding the user to the project!', {sticky:true,theme:'jgrowl-error'});
                }
            };

            var roles = <%=Model.Roles.ToJson() %>;

            for(var i in roles){
                var role = new Role(roles[i]);

                _roles.push(role);

                var widget = buildRoleWidget(role);

                $('#role-widget-list').append(widget);
            }

            $(".ui-sortable").sortable({ revert:true, placeholder: 'user-placeholder', forcePlaceholderSize: true});
            $(".ui-draggable").draggable({connectToSortable:".ui-sortable", helper: 'clone', revert:'invalid'});
            $(".ui-sortable").disableSelection();
      });
  
    </script>
    <style type="text/css">
        .user-left-side
        {
        	float: left;
            height: 100%;
            width: 250px;
        }
        .user-role-area
        {
            height: 100%;
            margin-left:250px;
        }
        .user-role-area ul
        {
            height: 100%;
        }
        .user-left-side ul
        {
        	background-color:Red;
            width: 100%;
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
            border: 1px solid #333;
        }
        .ui-draggable 
        {
            padding: 5px;
            border: 1px solid #333;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="mainContent" runat="server">
    <div id="project-users-toolbar">
        <%=this.LinkTo(new AddUserToProjectRequest(){Id = Model.ProjectId}).Text("Add User").Id("add-user")%></div>
    
        <div class="user-left-side">
            <ul id="project-users-list" >
            </ul>
        </div>
        <div class="user-role-area">
            <ul id="role-widget-list">
            </ul>
        </div>
</asp:Content>
