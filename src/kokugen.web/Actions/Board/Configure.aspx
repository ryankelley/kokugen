<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Board.Configure" AutoEventWireup="true" MasterPageFile="~/Shared/Project.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.BoardColumn"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="Kokugen.Web.Actions.Company"%>
<%@ Import Namespace="Kokugen.Web.Actions.Board"%>
<asp:Content ID="columnconfigHead" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript">

    var fixedclass = "";
    var removeColumnUrl = "<%= Get<IUrlRegistry>().UrlFor(new DeleteColumnInputModel()) %>";
    var columns = <%= Model.BoardColumns.ToJson() %>;
    var boardColumns = new Array();
    var BoardColumn = function(column) {
        this.Id = column.Id;
        this.Name = column.Name;
        this.Order = column.Order;
        this.Description = column.Description;
        this.Limit = column.Limit;
        this.Element = buildColumn(this);
        
        var self = this;
        
        this.Edit = function() {
            $('#column-form [name=ColumnName]').val(self.Name);
            $('#column-form [name=ColumnDescription]').val(self.Description);
            $('#column-form [name=ColumnLimit]').val(self.Limit);
            $('#column-form [name=Id]').val(self.Id);
            $('#column-form [name=ProjectId]').val(self.ProjectId);
            showColumnForm();
            updateColumns();
            return false;
        };
        
        this.Remove = function() {
             $.ajax({
                url: removeColumnUrl,
                data: { Id: this.Id },
                dataType: "json",
                type: "DELETE"
            });
            $(this.Element).remove();
            return false;
        };
    };

    var ColumnOrderDTO = function(boardColumn) {
        if (!(boardColumn instanceof BoardColumn)) {
            throw("boardColumn is not an instance of BoardColumn");
        }
        
        this.Id = boardColumn.Id;
        this.ColumnOrder = boardColumn.Order;
    }

    function bindList(data, template, output) {
        for(var i=0; i < data.length; i++) {         
            var col = new BoardColumn(data[i]);
            var board = $('#board-columns');
            var html = col.Element;
            board.append(html);
            fixedClass = i == 0 || i == columns.length-1 ? "fixed" : "draggable";
            $(html).addClass(fixedClass).addClass('phase');
            boardColumns.push(col);
        }
        fixedclass = "draggable"; // Setting this back to draggable so any new items will have that class added.
    }

    function updateColumns() {
        var count = 1;
        var order = $('#board-columns li').each(function() {
            this.UpdateOrder(count++);
        });
        
        boardColumns.sort(function(a, b) {
            return a.Order - b.Order;
        });
        
        var cols = new Array();
        $(boardColumns).each(function() {
            cols.push(new ColumnOrderDTO(this));
        });
        
        var data = new Object();
        data.ProjectId = "<%= Model.Id %>";
        data.columns = $.toJSON(cols);
        
        $.ajax({ type: 'POST', url: '/board/reorder', data: data, dataType: 'JSON' });
        //$("#info").load("process-sortable.php?" + order);
    }
    
    function bindSortingAndButtons(){
        $('#board-columns').sortable({
            items: '> *:not(".fixed")', placeholder: 'phase-placeholder', forcePlaceholderSize: true,
            update: updateColumns
        });
        $('#board-columns').disableSelection();

       
        
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
    
    var buildColumn = function(boardColumn) {
        if (!(boardColumn instanceof BoardColumn)) {
            throw("boardColumn is not an instance of type boardColumn");
        }
        
        var element = document.createElement('li');
        var title = document.createElement('h4');
        var description = document.createElement('div');
        $(description).addClass('col-desc');
        var links = document.createElement('div');
        var editLink = document.createElement('a');
        var removeLink = document.createElement('a');
        
        var titleText = boardColumn.Name;
        if (boardColumn.Limit != 0) titleText = titleText + ' (' + boardColumn.Limit + ')';
        
        title.appendChild(document.createTextNode(titleText));
        
        description.appendChild(document.createTextNode(boardColumn.Description));
        
        element.appendChild(title);
        element.appendChild(description);
        description.appendChild(links);
        links.appendChild(editLink);
        links.appendChild(removeLink);
        $(links).addClass('col-links').addClass('hidden');
        
        editLink.setAttribute('href', '#');
        editLink.appendChild(document.createTextNode('Edit'));
        $(editLink).addClass('edit-link');
        $(removeLink).addClass('remove-link');
        editLink.onclick = function() {
            boardColumn.Edit();
            return false;
        };
        
        removeLink.setAttribute('href', '#');
        removeLink.appendChild(document.createTextNode('Remove'));
        removeLink.onclick = function() {
            boardColumn.Remove();
            return false;
        };
        
        element.UpdateOrder = function(order) {
            boardColumn.Order = order;
        }
        
        
         $(description).hover(function() {
            $(this).children('.col-links').fadeIn(500);
        }, function() {
            $(this).children('.col-links').fadeOut(100);
        });
        
        return element;
    };
</script>

</asp:Content>
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">

<div><a href="#" onclick="addColumn();"><img src="/content/images/add_button.png" alt="add column" />Add Column</a></div>
        

<% this.Partial(new BoardColumnEditModel { ProjectId=Model.Id}); %>


<div class="board-column-configure ui-sortable">
    <ul id="board-columns">
        
    </ul>
</div>

</asp:Content>