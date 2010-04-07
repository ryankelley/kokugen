<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TimeRecord.List" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="Kokugen.Web.Actions.TimeRecord"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="TimeRecordListHead" ContentPlaceHolderID="mainContent" runat="server">
<body>
<div class="upper-meta">
    <div class="add-caption" ><a href="#" onclick="showTimeRecordForm();"><img src="/content/images/add_button.png" alt="add time record" />Add New TimeRecord</a></div>
</div>

<div class="timerecords">  
        <table> 
            <tr>
                <td>
                    <h3>
                        User
                    </h3>
                </td>
                <td>
                    <h3>
                        Description
                    </h3>
                </td>
                <td>
                    <h3>
                        Start
                    </h3>
                </td>
                <td>
                    <h3>
                        End
                    </h3>
                </td>
                <td>
                    <h3>
                        Duration
                    </h3>
                </td>
                <td>
                    <h3>
                        Billable
                     </h3>
                </td>
                <td>
                    <h3>
                    Task
                                        
                    </h3>
                </td>
            <tr>
       </table>
        
            
            <%= this.PartialForEach(p => p.TimeRecords).Using<TimeRecord_Control>() %>
 </div>
            
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
<% this.Partial(new TimeRecordFormModel(){}); %>
</asp:Content>