<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.GetProject" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="Kokugen.Web.Actions.TimeRecord"%>
<%@ Import Namespace="Kokugen.Web.Actions.Board"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<asp:Content ID="ProjectListHead" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="ProjectListContent" ContentPlaceHolderID="mainContent" runat="server">
<body>
<div id="Project Name" align=center><h2><%= this.DisplayFor(m => m.Project.Name) %></h2>

<div class="add-caption" ><a href="#" onclick="showTimeRecordForm();"><img src="/content/images/add_button.png" alt="add time record" />Add New TimeRecord</a></div>

<%--<% this.Partial(new TimeRecordFormModel()); %>--%>

<%= this.PartialForEach(p => p.TimeRecords).Using<TimeRecord_Control>() %>
    
    
   <div id ="Board Link"><%= this.LinkTo(new BoardConfigurationModel{ Id = Model.Project.Id}).Text("Configure Board Columns") %></div> 
   
</div>
    
    <%= this.LinkTo(new ViewBoardInputModel{ Id = Model.Project.Id}).Text("View Board") %>
</body>
    
<script type="text/javascript">

    function showTimeRecordForm() {
        $("#timerecord-form-container").dialog('open');
        return false;
    }

    function appendTaskToList(timerecord) {
        var output = "<span class=\"timerecord-name\">" + timerecord.Description + "</span>";

        $(".task-list").append(output);
    }    
        
    </script>
</asp:Content>