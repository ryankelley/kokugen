<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Metrics.Get" AutoEventWireup="true" MasterPageFile="~/Shared/Project.Master"%>
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
            <div id="cflow" class="chart"></div>
            <div id="phasedown" class="chart"><div class="task-toolbar"><button id="add-task" class="add-task">Add Task</button></div><ul id="task-container"></ul></div>
        </div>
</asp:Content>