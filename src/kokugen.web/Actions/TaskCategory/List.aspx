<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TaskCategory.List" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="Kokugen.Web.Actions.TaskCategory"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>

    
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">
    <div class="content"align=center>

        
        <div class="add-caption" ><a href="#" onclick="showTaskForm();"><img src="/content/images/add_button.png" alt="add project" />Add New Task</a></div>
            
        <h2>Tasks</h2>
        <%= this.PartialForEach(m => m.TaskCategories).Using<TaskItem_Control>() %>
    </div>
    
    <% this.Partial(new TaskFormModel()); %>
   
   
<script type="text/javascript">

        function showTaskForm() {
            $("#task-form-container").dialog('open');
            return false;
        }

        function appendTaskToList(task) {
            var output = "<li class=\"task first\"><a title=\"Click to view this project\" href=\"/taskcategory\">";
            output = output + "<span class=\"task-name\">" + task.Name + "</span>";
            output = output + "<span class=\"task-stats\">Some Status Here</span><div class=\"task-owner\">" + task.Name + "</div></a></li>";

            $(".task-list").append(output);
        }    
        
    </script>

</asp:Content>