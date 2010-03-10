<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.List" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="Kokugen.Web.Actions.Project"%>
<asp:Content ID="ProjectListHead" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
.hidden { display:none;}
input.error 
{
    border: solid thin red;
}
label.error
{
    display:block;
    color:Red;
}
</style>
</asp:Content>
<asp:Content ID="ProjectListContent" ContentPlaceHolderID="mainContent" runat="server">
<body>
    <div><a href="#" onclick="showProjectForm();"><img src="/content/images/add_button.png" alt="add project" /></a></div>
    <ul><%= this.PartialForEach(x => x.Projects).Using<ProjectItem_Control>() %></ul>
    
    <% this.Partial(new ProjectFormModel()); %>
    
    <script type="text/javascript">

        function showProjectForm() {
            $("#project-form-container").dialog('open');
            return false;
        }
       
    
    </script>
</body>
    

</asp:Content>