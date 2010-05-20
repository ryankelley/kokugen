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

    function getDateTimeFormat(d) {
var output = "";
var curr_date = d.getDate();
var curr_month = d.getMonth();
curr_month++;
var curr_year = d.getFullYear();
output = output + curr_month + "/" + curr_date + "/" + curr_year;

//time
var a_p = "";

var curr_hour = d.getHours();
if (curr_hour < 12)
   {
   a_p = "AM";
   }
else
   {
   a_p = "PM";
   }
if (curr_hour == 0)
   {
   curr_hour = 12;
   }
if (curr_hour > 12)
   {
   curr_hour = curr_hour - 12;
   }

var curr_min = d.getMinutes();

curr_min = curr_min + "";

if (curr_min.length == 1)
   {
   curr_min = "0" + curr_min;
   }

output = output + " " + curr_hour + ":" + curr_min + " " + a_p;

return output;

}

     function closeStopDialog(response) {
        
         var link = $('[data='+response.Item.Id+']');
         var parent = link.parent();
         link.remove();
         var date = getDateTimeFormat(new Date(parseInt(response.Item.EndTime.replace("/Date(", "").replace(")/", ""), 10)));
         var span = document.createElement('span');
         
         
         parent.append(document.createTextNode(date));
         
         parent.siblings('.duration').html(response.Item.Duration + " hrs");
        
    }


    function makeStopCall(id) {
        
    $.ajax({
            url: "/dailytimerecord/stop",
            data: { Id: id },
            success: closeStopDialog,
            type:'POST',
            dataType: 'json',
            });

            return false;
    }

</script>
<body>
<br />
<div class="upper-meta" align=center>
    <div class="add-caption" ><a href="#" onclick="startTimeRecord();"><img src="/content/images/start.png" alt="start time record" />Start New Daily Time Record</a></div>
</div>
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

        function startTimeRecord(data) {
            $.ajax ({
                success: refreshpage,
                type: 'post',        // 'get' or 'post', override for form's 'method' attribute 
                dataType: 'json'        // 'xml', 'script', or 'json' (expected server response type) 
            })
            return false;
        }

       function refreshpage(){
           window.location.reload(true);
       }

      
          
        
    </script>
   
</asp:Content>