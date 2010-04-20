<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Company.List" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.Company"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<asp:Content ID="CompanyListHead" ContentPlaceHolderID="head" runat="server">
<%= this.Script("company.js") %>
<style type="text/css">
body
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

            
</asp:Content>
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">

    <div class="content" align=center>
    <div class="add-caption"><a href="#" onclick="showCompanyForm(null);"><img src="/content/images/add_button.png" alt="add company" />Add New Company </a></div>
        <h2>Clients</h2>
        <table class="companyList" id="companyList">
        <tr>
        <th>
            Client Name
        </th>
        <th>
            Edit
        </th>
        </tr>
         
        </table>
        
    </div>
    
    
    <% this.Partial(new CompanyFormModel()); %>


    <script type="text/javascript">

        function showCompanyForm(comp) {
            
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
            $("#"+ comp.Id).remove();
            $("#company-form-container").dialog('open');
            return false;
        }
       
    
    </script>
    <script type="text/javascript">
    var addCompanyUrl = "<%= Get<IUrlRegistry>().UrlFor(new AddCompanyInput()) %>";
    var removeCompanyUrl = "<%= Get<IUrlRegistry>().UrlFor(new RemoveCompanyInput()) %>";
    var companies = <%= Model.Companies.ToJson() %>;
    var companyList = $("#companyList");
    function addCompanyToList(company){
            var comp = new Company(company);
            var element = buildCompanyDisplay(comp);
            companyList.append( element );
        }

    $(document).ready(function(){
        companyList = $("#companyList");

        $.each(companies, function(i, elem){
            addCompanyToList(elem);
        });
    });


</script>


</asp:Content>