<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.List" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="Kokugen.Web.Actions.Project"%>
<asp:Content ID="ProjectListHead" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="ProjectListContent" ContentPlaceHolderID="mainContent" runat="server">
<body>
<div class="upper-meta">
    <div class="add-caption"><a href="#" onclick="showProjectForm();"><img src="/content/images/add_button.png" alt="add project" />Add New Project</a></div>
</div>
    <ul class="project-list"><%= this.PartialForEach(x => x.Projects).Using<ProjectItem_Control>() %></ul>
    
    <% this.Partial(new ProjectFormModel()); %>
    
    <script type="text/javascript">

        function showProjectForm() {
            $("#project-form-container").dialog('open');
            return false;
        }

        function appendProjectToList(project) {
            var output = "<li class=\"project first\"><a title=\"Click to view this project\" href=\"/project/ "+ project.Id + "\">";
            output = output +  "<span class=\"project-name\">" + project.Name + "</span>";
            output = output +  "<span class=\"project-stats\">Some Status Here</span><div class=\"project-owner\">" + project.Company.Name + "</div></a></li>";

            $(".project-list").append(output);
        }
    
    </script>
</body>
    

</asp:Content>