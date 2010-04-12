<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.TimeRecord.StopTimeRecordForm"%>
<%@ Import Namespace="Kokugen.Web.Actions.TimeRecord"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="Kokugen.Web.Conventions"%>


<div id="timerecord-stop-form-container" class="hidden">
<%= this.FormFor(new StopTimeRecordModel() {Id= Model.TimeRecord.Id})%>
    <div class="form-item">
    <label >Duration</label></div>
    <%= this.DisplayFor(x => x.TimeRecord.Duration).Id("time-record-duration") %>
    <%= this.Edit(x =>x.TimeRecord.Billable) %>
    <%= this.InputFor(x => x.TimeRecord.Id).Id("time-record-id") %>
    
    
</form>
   
</div>
<script type="text/javascript">

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
         span.appendChild(document.createTextNode(date));
         
         parent.append(span);
         
         parent.siblings('.billable').children('span').html(response.Item.Billable);
         parent.siblings('.duration').children('span').html(response.Item.Duration);
         
         

        $("#timerecord-stop-form-container").dialog('close');
        // would want to update list here too
    }
    function validateAndStopSave() {
            var billable = $("#time-record-billable").val();
            $.ajax({
            url: "/timerecord/stop",
            data: { Id: $('#time-record-id').val(), Billable: billable },
            success: closeStopDialog,
            type:'POST',
            dataType: 'json',
            });
            
            return false;
    }

    $(document).ready(function() {
    $("#timerecord-stop-form-container").dialog({ title: "Stop Time Record", autoOpen: false, buttons: { "Save": validateAndStopSave} });
    $("#timerecord-stop-form-container form").submit(validateAndStopSave);
    });
</script>
