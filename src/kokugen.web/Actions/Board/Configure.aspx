<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Board.Configure" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.BoardColumn"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="Kokugen.Web.Actions.Company"%>
<%@ Import Namespace="Kokugen.Web.Actions.Board"%>
<asp:Content ID="columnconfigHead" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript">

var fixedclass = "";
var removeColumnUrl = "<%= Get<IUrlRegistry>().UrlFor(new DeleteColumnInputModel()) %>";
var columns = <%= Model.BoardColumns.ToJson() %>;
var boardColumn = function(id, order) {
        //var self = this;
        this.Id = id;
        this.ColumnOrder = order;
        this.Description;
        this.Limit;
        //this.Element = document.createElement('li');
    }


    function bindList(data, template, output) {
            // Stuff data in the template
            
            for(var i=0; i < data.length; i++) {         
                var col = data[i]; 
                fixedclass = i == 0 || i == columns.length-1 ? "fixed" : "draggable";
                var html = tmpl(template, col);
                $(html).appendTo(output);
            // Append the templated item(s) to the output container
            }
            fixedclass = "draggable"; // Setting this back to draggable so any new items will have that class added.
        }

    function updateColumns() {
        var count = 1;
        var boardColumns = new Array();
        var order = $('#board-columns li').each(function() {
            
            if ($(this).attr('id') != "") {
            
                var id = $(this).attr('id').replace("_COL", "");

                var col = new boardColumn(id, count);
                boardColumns.push(col);

                count = count + 1;
            }
        });

        var raw = new Object();
        raw.ProjectId = "<%= Model.Id %>";
        raw.columns = $.toJSON(boardColumns);
        
        //var data = $.toJSON(raw);

        $.ajax({ type: 'POST', url: '/board/reorder', data: raw, dataType: 'JSON' });
        //$("#info").load("process-sortable.php?" + order);
    }
    
    function bindSortingAndButtons(){
    $('#board-columns').sortable({
            items: '> *:not(".fixed")', placeholder: 'phase-placeholder', forcePlaceholderSize: true,
            update: updateColumns
        });
        $('#board-columns').disableSelection();

        $("li.draggable .col-desc").hover(function() {
            $(this).children('.col-links').fadeIn(500);
        }, function() {
            $(this).children('.col-links').fadeOut(100);
        });
        
        $(".removeLink").live("click", function() {
            var link = $(this);
            var columnId = link.attr("data");

            var onSuccess = function(data) {
                if (data.Success !== true) {
                    alert("failed to remove");
                    return;
                }

                $('#'+data.Item + '_COL').remove();
            }

            $.ajax({
                url: removeColumnUrl,
                data: { Id: columnId },
                success: onSuccess,
                dataType: "json",
                type: "DELETE"
            });
        });
    }
        

    
    $(document).ready(function() {
        
        bindList(columns, $("#ItemTemplate").html(), $("#board-columns"));
        
        bindSortingAndButtons();
    });
    
    
</script>

</asp:Content>
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">

<div><a href="#" onclick="showColumnForm();"><img src="/content/images/add_button.png" alt="add column" />Add Column</a></div>
        

<% this.Partial(new BoardColumnEditModel { ProjectId=Model.Id}); %>


<div class="board-column-configure ui-sortable">
    <ul id="board-columns">
        
    </ul>
</div>


<script id="ItemTemplate" type="text/html">

<li class="<#= fixedclass #> phase" id="<#= Id #>_COL">
<div class="col-title"><#= Name #><# var limit = Limit != 0 ? "(" + Limit + ")" : "";  #> <#= limit #></div>
<div class="col-desc"><#= Description #>
    <div class="col-links hidden">
	    <a href="#" data="<#= Id #>" class="editLink"><img src="/content/images/card_edit.png" alt="Edit Column" /></a>
	    <a href="#" data="<#= Id #>" class="removeLink"><img src="/content/images/card_delete.png" alt="Delete Column" /></a>
    </div>
</div>
</li>

</script>

</asp:Content>