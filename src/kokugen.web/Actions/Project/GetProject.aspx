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
            <ul id="companyList"></ul>
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
<%--    <script type="text/javascript">--%>
<%--    var addCompanyUrl = "<%= Get<IUrlRegistry>().UrlFor(new TimeRecordFormModel()) %>";--%>
<%--    var removeCompanyUrl = "<%= Get<IUrlRegistry>().UrlFor(new RemoveTimeRecordInput()) %>";--%>
<%--    var companies = <%= Model.TimeRecords.ToJson() %>;--%>
<%----%>
<%--    $(document).ready(function(){--%>
<%--        var companyList = $("#companyList");--%>
<%----%>
<%--        var addCompanyToList = function(company){--%>
<%--            var listItem = $("<li>").text(company.Name);--%>
<%--            listItem.append( $("<a>").text("x")--%>
<%--                .attr("href", "#")--%>
<%--                .addClass("removeLink")--%>
<%--                .data("companyId", company.Id) );--%>
<%--            companyList.append( listItem );--%>
<%--        };--%>
<%--        --%>
<%--          var saveCompanyResponse = function(data){--%>
<%--            if (data.Success !== true) {--%>
<%--                alert("failed to add your time record");--%>
<%--                return;--%>
<%--            }--%>
<%--            --%>
<%--            $("#company-name").val("");--%>
<%--            addCompanyToList(data.Item);--%>
<%--        };--%>
<%--        --%>
<%--        $.each(companies, function(i, elem){--%>
<%--            addCompanyToList(elem);--%>
<%--        });--%>
<%--        --%>
<%--        $(".removeLink").live("click", function(){--%>
<%--            var link = $(this);--%>
<%--            var companyId = link.data("companyId");--%>
<%--            --%>
<%--            var onSuccess = function(data){--%>
<%--                if (data.Success !== true){--%>
<%--                    alert("failed to remove");--%>
<%--                    return;--%>
<%--                }--%>
<%--                --%>
<%--                var listItem = link.parent("li");--%>
<%--                listItem.remove();--%>
<%--            }--%>
<%--            --%>
<%--            $.ajax({--%>
<%--                url: removeCompanyUrl,--%>
<%--                data: {Id: companyId},--%>
<%--                success: onSuccess,--%>
<%--                dataType: "json",--%>
<%--                type: "DELETE"--%>
<%--            });--%>
<%--        });--%>
<%--        --%>
<%--        --%>
<%--    });--%>
<%----%>
<%----%>
<%--</script>--%>
</body>
    

</asp:Content>