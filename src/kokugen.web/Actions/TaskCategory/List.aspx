<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TaskCategory.List" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="Kokugen.Web.Actions.TaskCategory"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>

    
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">
    <div class="content"align=center  margin-left=auto margin-right=auto width=6em>
    <style type="text/css">
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
        background-color:#CEBEB4;
        color:#49657D;
    }
th
    {
        background-color:#9e9993;
        color:black;
    }
</style>

        <script type="text/javascript">

            $(document).ready(function () {
                $(".delete-button").click(function () {

                    makeDeleteCall($(this).attr("data"));

                });
            });

            var onSuccess = function (data) {
                if (data.Success !== true) {
                    alert("failed to remove");
                    return;
                }
                var link = $(this);

                var listItem = link.parent("tr");
                listItem.remove();
            }

            function makeDeleteCall(id) {

                $.ajax({
                    url: "<%= Get<IUrlRegistry>().UrlFor(new RemoveTaskInput()) %>",
                    data: { Id: id },
                    dataType: "json",
                    type: "DELETE",
                    success: onSuccess
                });
                
                return false;
            }
            $(document).ready(function () {
                $(".edit-button").click(function () {

                    makeEditCall($(this).attr("name"), $(this).attr("id"));

                });
            });

           
            function makeEditCall(name, id) {
            
            showTaskForm(name, id);

                return false;
            }
        </script>
        <div class="add-caption" ><a href="#" onclick="showTaskForm(null, null);"><img src="/content/images/add_button.png" alt="add project" />Add New Task</a></div>
            
        <h2>Tasks</h2>
        <table>
       <tr>       
            <th> 
            Task
            </th>
            <th>
            Edit
            </th>
        </tr>
        <%= this.PartialForEach(m => m.TaskCategories).Using<TaskItem_Control>() %>
        </table>
        
    </div>
    
    <% this.Partial(new TaskFormModel()); %>
   
   
<script type="text/javascript">


    function showTaskForm(name, id) {

        $("#task-name").val(name);
        $("#task-edit-form-id").val(id);
        $("#task-form-container").dialog('open');  
            
            
            return false;
        }

        function appendTaskToList(task) {
            var output = "<span class=\"task-name\">" + task.Name + "</span>";
            
            $(".task-list").append(output);
        }    
        
    </script>

</asp:Content>