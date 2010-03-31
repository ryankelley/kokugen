<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TaskCategory.List" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="Kokugen.Web.Actions.TaskCategory"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>

    
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">
    <div class="content">

        <div class="upper-meta">
            <div class="add-caption"><a href="#" onclick="showTaskForm();"><img src="/content/images/add_button.png" alt="add project" />Add New Task</a></div>
        </div>

    
        <h2>Tasks</h2>
        <%= this.PartialForEach(m => m.TaskCategories).Using<TaskItem_Control>() %>
    </div>
   
   
<script type="text/javascript">

        function showTaskForm() {
            $("#task-form-container").dialog('open');
            return false;
        }

       
        
    </script>

</asp:Content>