<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.DailyTimeRecord.List" MasterPageFile="~/Shared/Site.Master"%>
<%@ Import Namespace="Kokugen.Web.Actions.DailyTimeRecord"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="TimeRecordListHead" ContentPlaceHolderID="mainContent" runat="server">

<style type="text/css">
    
    h2
    {
    color:#333;	
    
    }
    
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
            </tr>
            
            <%= this.PartialForEach(p => p.DailyTimeRecords).Using<DailyTimeRecord_Control>() %>
       </table>
        
            
           
 </div>
            
    <script type="text/javascript">

        function showTimeRecordForm(data) {
            $("#timerecord-form-container").dialog('open');
            return false;
        }

        function appendTimeRecordToList(timerecord) {
            var output = "<td class=\"description\">" + timerecord.Description + "</td>";

            $(".timerecord-list").append(output);
        }    
        
    </script>
   
</asp:Content>