<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Company.List" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Kokugen.Web.Conventions"%>
<%@ Import Namespace="Kokugen.Web.Actions.Company"%>
<%@ Import Namespace="Kokugen.Core"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="HtmlTags"%>
<asp:Content ID="CompanyListHead" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
body
{
    background-color: Black;
    font-family:arial,helvetica,sans-serif;
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
</asp:Content>
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">

    <div class="content">
        <h2>Companies</h2>

        
        
        <%= this.Edit(x => x.Company.Name) %>
        <%= this.Edit(x => x.Company.Address.StreetLine1)%>
        <%= this.Edit(x => x.Company.Address.StreetLine2)%>
        <%= this.Edit(x => x.Company.Address.City)%>
        <%= this.Edit(x => x.Company.Address.State)%>
        <%= this.Edit(x => x.Company.Address.ZipCode)%>
        
        
        
            <input type="button" value="Add" id="addCompanyButton" />
        
        <ul id="companyList"></ul>
    </div>
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
        
        var saveNewCompany = function(){
            var name = $("#company-name").val();
            $.post(addCompanyUrl, {CompanyName: name}, saveCompanyResponse, "json");
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
        
        $("#addCompanyButton").click(saveNewCompany);
    });


</script>


</asp:Content>