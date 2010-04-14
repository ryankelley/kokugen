<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TimeRecord.List" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="Kokugen.Web.Actions.TimeRecord"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="TimeRecordListHead" ContentPlaceHolderID="mainContent" runat="server">

<style type="text/css">
    table
        {
            border-collapse:collapse;
            width:1024px;
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
</style>
<script type="text/javascript">

    $(document).ready(function () {
        $(".stop-button").click(function () {

            makeStopCall($(this).attr("data"));

        });
    });

    function showExtraDialog(response) {

        if (response.Success) {
            $("#time-record-billable").val(response.Item.Billable);
            $("#time-record-duration").html(response.Item.Duration);
            $("#time-record-id").val(response.Item.Id);

            $("#timerecord-stop-form-container").dialog('open');
        }
    }

    function makeStopCall(id) {

        $.ajax({
            url: "/timerecord/stop",
            data: { Id: id },
            dataType: "json",
            type: "POST",
            success: showExtraDialog
        });


    }

</script>
<body>
<li>
</li>
<div class="upper-meta" align=center>
    <div class="add-caption" ><a href="#" onclick="showTimeRecordForm();"><img src="/content/images/add_button.png" alt="add time record" />Add New TimeRecord</a></div>
</div>

<div class="timerecords" align=center>  
        <table> 
            
            <tr>
                <th>
                    <h3>
                        User
                    </h3>
                </th>
                <th>
                    <h3>
                        Description
                    </h3>
                </th>
                <th>
                    <h3>
                        Start
                    </h3>
                </th>
                <th>
                    <h3>
                        End
                    </h3>
                </th>
                <th>
                    <h3>
                        Duration
                    </h3>
                </th>
                <th>
                    <h3>
                        Billable
                     </h3>
                </th>
                <th>
                    <h3>
                    Task
                                        
                    </h3>
                </th>
                <th>
                    <h3>
                        Project
                    </h3>
                </th>        
            </tr>
            
            <%= this.PartialForEach(p => p.TimeRecords).Using<TimeRecord_Control>() %>
       </table>
        
            
           
 </div>
            
    <script type="text/javascript">

        function showTimeRecordForm() {
            $("#timerecord-form-container").dialog('open');
            return false;
        }

        function appendTimeRecordToList(timerecord) {
            var output = "<td class=\"description\">" + timerecord.Description + "</td>";

            $(".timerecord-list").append(output);
        }    
        
    </script>
    <% this.Partial(new StopTimeRecordFormInputModel()); %>
</body>
<% this.Partial(new TimeRecordFormModel(){}); %>
</asp:Content>