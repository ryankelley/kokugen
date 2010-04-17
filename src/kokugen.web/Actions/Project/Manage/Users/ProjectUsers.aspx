<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.Manage.Users.ProjectUsers"
 MasterPageFile="~/Shared/Project.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.Project.Manage.Users.Add" %>

 <asp:Content ContentPlaceHolderID=head runat=server>
  <%=this.Script("projectusers.js") %>
  <script type="text/javascript">
      $(function () {
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

      });
  
  </script>
   
 </asp:Content>


 <asp:Content ContentPlaceHolderID="mainContent" runat=server>
    
    <div id="project-users-toolbar"><%=this.LinkTo(new AddUserToProjectRequest(){Id = Model.ProjectId}).Text("Add User").Id("add-user")%></div>

    <ul id="project-users-list"></ul>


 </asp:Content>
