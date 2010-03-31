<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Board.Configure" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.BoardColumn"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="Kokugen.Web.Actions.Company"%>
<%@ Import Namespace="Kokugen.Web.Actions.Board"%>
<asp:Content ID="columnconfigHead" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript">

    var boardColumn = function(id, order) {
        //var self = this;
        this.Id = id;
        this.ColumnOrder = order;
        //this.Element = document.createElement('li');
    }

    function updateColumns() {
        var count = 1;
        var boardColumns = new Array();
        var order = $('#board-columns li').each(function() {
            
            if ($(this).attr('id') != "") {

                var col = new boardColumn($(this).attr('id'), count);
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

    function showColumnForm() {
        $("#new-column-container").slideToggle('slow');
    }
    var removeColumnUrl = "<%= Get<IUrlRegistry>().UrlFor(new DeleteColumnInputModel()) %>";
    $(document).ready(function() {
        $('#board-columns').sortable({
            items: '> *:not(".fixed")', placeholder: 'phase-placeholder', forcePlaceholderSize: true,
            update: updateColumns
        });
        $('#board-columns').disableSelection();

        $("li.draggable .col-desc").hover(function() {
            $(this).children('.col-links').fadeIn(500);
        }, function() {
            $(this).children('.col-links').fadeOut(300);
        });

        $(".removeLink").live("click", function() {
            var link = $(this);
            var columnId = link.attr("data");

            var onSuccess = function(data) {
                if (data.Success !== true) {
                    alert("failed to remove");
                    return;
                }

                $('#'+data.Item).remove();
            }

            $.ajax({
                url: removeColumnUrl,
                data: { Id: columnId },
                success: onSuccess,
                dataType: "json",
                type: "DELETE"
            });
        });
    });
    
    
</script>

</asp:Content>
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">

<div><a href="#" onclick="showColumnForm();"><img src="/content/images/add_button.png" alt="add column" />Add Column</a></div>
        

<% this.Partial(new BoardColumnEditModel { ProjectId=Model.Id}); %>


<div class="board-column-configure ui-sortable">
    <ul id="board-columns">
        <%= this.PartialForEach(m => m.BoardColumns).Using<BoardItem_Control>() %>
    </ul>
</div>
</asp:Content>