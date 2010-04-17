<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TaskCategory.List" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="Kokugen.Web.Actions.TaskCategory"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>

    
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">
<%= this.Script("task.js") %>
    <div class="content"align=center  margin-left=auto margin-right=auto width=6em>
    <style type="text/css">
    
    h2
    {
    color:#333;	
    
    }
    
    table
        {
            border-collapse:collapse;
            width:250px;
            padding:10px;
            border:5px solid gray;
            margin:10px;
        }
table, td, th
    {
        border:3px solid black;
        border-style:inset;
        text-align:center;
        background-color:#ddd;
        color:#555;
    }
th
    {
        background-color:#888;
        color:#222;
        text-shadow: 0 1px #bbb;
    }
</style>


        <div class="add-caption" ><a href="#" onclick="showTaskForm(null);"><img src="/content/images/add_button.png" alt="add project" />Add New Task</a></div>
            
        <h2>Tasks</h2>
        <table class="taskList" id="taskList">
       <tr>       
            <th> 
            Task
            </th>
            <th>
            Edit
            </th>
        </tr>
        
        </table>
        
    </div>
    
    <% this.Partial(new TaskFormModel()); %>
   
   
<script type="text/javascript">


    function showTaskForm(task) {

        if (task == null) {
            $("#task-form-container").dialog('open');
            return false;
        }

        $("#task-name").val(task.Name);
        $("#task-edit-form-id").val(task.Id);
        $("#" + task.Id).remove();
        $("#task-form-container").dialog('open');  
            
            
            return false;
        }
                
          
        
    </script>
    <script type="text/javascript">
    var addtaskUrl = "<%= Get<IUrlRegistry>().UrlFor(new AddTaskModel()) %>";
    var removetaskUrl = "<%= Get<IUrlRegistry>().UrlFor(new RemoveTaskInput()) %>";
    var tasks = <%= Model.TaskCategories.ToJson() %>;
    var taskList = $("#taskList");
    function addtaskToList(task){
            var t = new Task(task);
            var element = buildtaskDisplay(t);
            taskList.append( element );
        }

    $(document).ready(function(){
        taskList = $("#taskList");

        $.each(tasks, function(i, elem){
            addtaskToList(elem);
        });
    });


</script>
</asp:Content>