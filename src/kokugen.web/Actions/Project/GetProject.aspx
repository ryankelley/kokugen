<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Project.GetProject" MasterPageFile="~/Shared/Project.Master"%>
<%@ Import Namespace="Kokugen.Web.Actions.TimeRecord"%>
<%@ Import Namespace="Kokugen.Web.Actions.Board"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="HtmlTags" %>
<%@ Import Namespace="Kokugen.Web.Actions.Project.Manage.Menu" %>
<%@ Import Namespace="Kokugen.Web.Actions.Board.Configure" %>
<asp:Content ID="ProjectListHead" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID=extraNavigation runat=server>
    <%this.Partial(new ManageProjectMenuRequest(){Id = Model.ProjectId}); %>
</asp:Content>

<asp:Content ID="ProjectListContent" ContentPlaceHolderID="mainContent" runat="server">
<style type="text/css">
 .content { 
      display: table;
    width: 100%;
  background-image: -moz-linear-gradient(top, #F0F0F0, #FFFFFF 20%); /* FF3.6 */
  background-image: -webkit-gradient(linear,left top,left bottom,color-stop(0, #d0d0d0),color-stop(1, #FFFFFF)); /* Saf4+, Chrome */
            filter:  progid:DXImageTransform.Microsoft.gradient(startColorStr='#d0d0d0', EndColorStr='#FFFFFF'); /* IE6,IE7 */
        -ms-filter: "progid:DXImageTransform.Microsoft.gradient(startColorStr='#d0d0d0', EndColorStr='#FFFFFF')"; /* IE8 */ 
}
ul.timerecords 
{
    display: table;
    width:100%;
}

li.header { text-align: center; font-weight: bold; background-color: #666; color:#FFF; }
ul li { display: table-row; }
ul li span { border-bottom:1px solid #999999;              
border-right:1px solid #999999;
display:table-cell;
padding:7px;
text-align:center; }

ul.timerecords li.even { background-color: #DEDEDE; }
ul.timerecords li:hover { background-color:#91B5D8;
border-color:#233644; }

span.user { min-width: 30px; width: 40px; }

li.completed span { text-decoration: line-through; }
li.header span.user { text-indent: 0px; }
.user a { color: #333; text-decoration: none; }
.user a:hover  
{
    text-indent: 16px;}    

</style>
<script type="text/javascript">

    $(document).ready(function() {
    $(".stop-button").click(function() { 
        
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
    
    function makeStopCall(id){
        
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
<div id="Project Name" align=center><h2><%= this.DisplayFor(m => m.Project.Name) %></h2>

<div class="upper-meta">
    <div class="add-caption" ><a href="#" onclick="showTimeRecordForm();"><img src="/content/images/add_button.png" alt="add time record" />Add New TimeRecord</a></div>
</div>    
        
                                  
          <h2><%=Model.Project.Name+" "%>Time Records</h2>               
                                     
           <ul class="timerecords" >  
         
            
            <li class="header">
            <span class="user">User</span>                
            <span class="description">Description</span>                   
            <span class="start">Start</span>            
            <span class="end">End</span>            
            <span class="duration">Duration</span>            
            <span class="billable">Billable</span>            
            <span class="task">Task</span>            
            <span class="project">Project</span>  
                    
                        
             </li>      
            
            <%= this.PartialForEach(p => p.TimeRecords).Using<TimeRecord_Control>() %>
  </ul>
        
            
            
            <ul id="companyList"></ul>
       
    
    </div>  
   


    <% this.Partial(new StopTimeRecordFormInputModel()); %>

    
<% this.Partial(new ProjectTimeRecordFormModel(){ProjectId = Model.Project.Id}); %>

<script type="text/javascript">

    function showTimeRecordForm() {
        $("#timerecord-form-container").dialog('open');
        return false;
    }

    function appendTimeRecordToList(timerecord) {
        var output = "<li><span class=\"description\">" + timerecord.Description + "</span></li>";

        $(".timerecord-list").append(output);
    }    
        
    </script>

</body>
    

</asp:Content>