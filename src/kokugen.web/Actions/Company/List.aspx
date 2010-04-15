<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Company.List" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.Company"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<asp:Content ID="CompanyListHead" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
body
{
    background-color: Black;
    font-family:arial,helvetica,sans-serif;
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
        background-color:#CEBEB4;
        color:#49657D;
    }
th
    {
        background-color:#9e9993;
        color:black;
    }

.content
{
    background-color: White;
    height: 400px;
    width: 600px;
    margin-left:auto;
    margin-right:auto;
    padding: 0px 4px 2px 4px;
}

.removeLink
{
    margin-left:3px;
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
                        url: "<%= Get<IUrlRegistry>().UrlFor(new RemoveCompanyInput()) %>",
                        data: { Id: id },
                        dataType: "json",
                        type: "DELETE",
                        success: onSuccess
                    });

                    return false;
                }
                $(document).ready(function () {
                    $(".edit-button").click(function () {

                        makeEditCall($(this).attr("data"));

                    });
                });


                function makeEditCall(name, id) {

                    showCompanyForm(name, id);

                    return false;
                }
        </script>
</asp:Content>
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">

    <div class="content" align=center>
    <div><a href="#" onclick="showCompanyForm(null);"><img src="/content/images/add_button.png" alt="add company" />Add New Company </a></div>
        <h2>Companies</h2>
        <table>
        <tr>
        <th>
            Company
        </th>
        <th>
            Edit
        </th>
        </tr>
        <%= this.PartialForEach(c => c.Companies).Using<CompanyItem_Control>()%>   
        </table>
        
    </div>
    
    
    <% this.Partial(new CompanyFormModel()); %>


    <script type="text/javascript">

        function showCompanyForm(companyId) {
            for (var i in companies) {
                if (companies[i].Id == companyId) {
                    var comp = companies[i];
                }
            }

            if (comp == null) {
                $("#company-form-container").dialog('open');
                return false;
              }

            $("#company-name").val(comp.Name);
            $("#company-edit-form-id").val(comp.Id);
            $("#company-address-street-line-1").val(comp.Address.StreetLine1);
            $("#company-address-street-line-2").val(comp.Address.StreetLine2);
            $("#company-address-city").val(comp.Address.City);
            $("#company-address-state").val(comp.Address.State);
            $("#company-address-zip-code").val(comp.Address.ZipCode);
            $("#company-form-container").dialog('open');
            return false;
        }
       
    
    </script>
    <script type="text/javascript">
    var addCompanyUrl = "<%= Get<IUrlRegistry>().UrlFor(new AddCompanyInput()) %>";
    var removeCompanyUrl = "<%= Get<IUrlRegistry>().UrlFor(new RemoveCompanyInput()) %>";
    var companies = <%= Model.Companies.ToJson() %>;

    $(document).ready(function(){
        var companyList = $("#companyList");

        var addCompanyToList = function(company){
            var listItem = $("<li>").text(company.Name);
            listItem.append( $("<a>").text("x")
                .attr("href", "#")
                .addClass("removeLink")
                .data("companyId", company.Id) );
            companyList.append( listItem );
        };
        
          var saveCompanyResponse = function(data){
            if (data.Success !== true) {
                alert("failed to add your company");
                return;
            }
            
            $("#company-name").val("");
            addCompanyToList(data.Item);
        };
        
        $.each(companies, function(i, elem){
            addCompanyToList(elem);
        });
        
        $(".removeLink").live("click", function(){
            var link = $(this);
            var companyId = link.data("companyId");
            
            var onSuccess = function(data){
                if (data.Success !== true){
                    alert("failed to remove");
                    return;
                }
                
                var listItem = link.parent("li");
                listItem.remove();
            }
            
            $.ajax({
                url: removeCompanyUrl,
                data: {Id: companyId},
                success: onSuccess,
                dataType: "json",
                type: "DELETE"
            });
        });
        
        
    });


</script>


</asp:Content>