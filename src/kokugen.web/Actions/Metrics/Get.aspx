<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Metrics.Get" AutoEventWireup="true" MasterPageFile="~/Shared/Project.Master"%>
<asp:Content ID="metricsHeader" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    $(function () {
        $('#tabs').tabs();
    }); 
</script>
</asp:Content>
<asp:Content ID="metricsGetMain" ContentPlaceHolderID="mainContent" runat="server">
<div class="metrics-header">
<ul class="metrics">
<li><span class="mhead">Lead Time</span><span class="mdata"><%= Model.Metrics.AverageLeadTime.ToSimpleDisplay() %></span></li>
<li><span class="mhead">Cycle Time</span><span class="mdata"><%= Model.Metrics.AverageCycleTime.ToSimpleDisplay() %></span></li>
<li><span class="mhead">Lead Time</span><span class="mdata"><%= Model.Metrics.AverageWorkTime.ToSimpleDisplay() %></span></li>
<li><span class="mhead">Lead Time</span><span class="mdata"><%= Model.Metrics.AverageIdleTime.ToSimpleDisplay() %></span></li>
<li><span class="mhead">Efficiency</span><span class="mdata"><%= Model.Metrics.Efficiency %>%</span></li>
</ul>
</div>
<div id="tabs">
            <ul>
                <li><a href="#cflow"><span>Cumulative Flow</span></a></li>
                <li><a href="#phasedown"><span>Tasks</span></a></li>
            </ul>
            <div id="cflow" class="chart">
            <!-- amline script-->
  <script type="text/javascript" src="/content/amline/swfobject.js"></script>
	<div id="flashcontent">
		<strong>You need to upgrade your Flash Player</strong>
	</div>

	<script type="text/javascript">
		// <![CDATA[
	    var so = new SWFObject("/content/amline/amline.swf", "cumulativeFlow", "960", "500", "8", "#FFFFFF");
	    so.addVariable("path", "/amline/");
	    so.addVariable("settings_file", encodeURIComponent("/content/amline/cumflow_settings.xml"));                // you can set two or more different settings files here (separated by commas)
	    so.addVariable("data_file", escape("/metrics/cumalativeflow?ProjectId=<%= Model.ProjectId %>"));
	    so.addParam("wmode", "opaque");

	    //	so.addVariable("chart_data", encodeURIComponent("data in CSV or XML format"));                    // you can pass chart data as a string directly from this file
	    //	so.addVariable("chart_settings", encodeURIComponent("<settings>...</settings>"));                 // you can pass chart settings as a string directly from this file
	    //	so.addVariable("additional_chart_settings", encodeURIComponent("<settings>...</settings>"));      // you can append some chart settings to the loaded ones
	    //  so.addVariable("loading_settings", "LOADING SETTINGS");                                           // you can set custom "loading settings" text here
	    //  so.addVariable("loading_data", "LOADING DATA");                                                   // you can set custom "loading data" text here
	    //	so.addVariable("preloader_color", "#999999");
	    //  so.addVariable("error_loading_file", "ERROR LOADING FILE");                                   // you can set custom "error loading file" text here
	    so.write("flashcontent");
		// ]]>
	</script>
<!-- end of amline script -->
            </div>
            <div id="phasedown" class="chart"><div class="task-toolbar"><button id="add-task" class="add-task">Add Task</button></div><ul id="task-container"></ul></div>
        </div>
</asp:Content>