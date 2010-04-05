<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.GetProject" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="Kokugen.Web.Actions.TimeRecord"%>
<%@ Import Namespace="Kokugen.Web.Actions.Board"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<asp:Content ID="ProjectListHead" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="ProjectListContent" ContentPlaceHolderID="mainContent" runat="server">
<body>
<div id="Project Name" align=center><h2><%= this.DisplayFor(m => m.Project.Name) %></h2>

<div class="upper-meta">
    <div class="add-caption" ><a href="#" onclick="showTimeRecordForm();"><img src="/content/images/add_button.png" alt="add time record" />Add New TimeRecord</a></div>
</div>
<div class="timerecords">
    <table border=collapse  bgcolor=#DCDCDC bordercolor=black>
        <thead > 
            <tr>
            <h2><%=Model.Project.Name+" "%>Time Records</h2>
            </tr>   
            <tr>
            <td>
                <h3>Description</h3>
            </td>
            <td>
                <h3>Task</h3>
            </td>
            <td>
                <h3>Duration</h3>
            </td>
            <td>
                 <h3>Delete</h3>
            </td>
            </tr>
        </thead>
        <tbody>
            
            <%= this.PartialForEach(p => p.TimeRecords).Using<TimeRecord_Control>() %>
        </tbody>
    </table>
    </div>
    
   <div id ="Board Link"><%= this.LinkTo(new BoardConfigurationModel{ Id = Model.Project.Id}).Text("Configure Board Columns") %></div> 
   
</div>
    
    <%= this.LinkTo(new ViewBoardInputModel{ Id = Model.Project.Id}).NoClosingTag().AddClass("icon") %><img src="/content/images/board_big.png" alt="view board" /></a>
</body>
    
<% this.Partial(new TimeRecordFormModel(){ProjectId = Model.Project.Id}); %>

<script type="text/javascript">

    function showTimeRecordForm() {
        $("#timerecord-form-container").dialog('open');
        return false;
    }

    function appendTaskToList(timerecord) {
        var output = "<span class=\"timerecord-name\">" + timerecord.Description + "</span>";

        $(".timerecord-list").append(output);
    }    
        
    </script>
</body>
    

</asp:Content>