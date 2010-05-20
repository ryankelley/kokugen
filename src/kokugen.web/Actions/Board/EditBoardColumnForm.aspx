<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Board.EditBoardColumnForm"%>
<%@ Import Namespace="HtmlTags"%>
<%@ Import Namespace="Kokugen.Web.Actions.Board"%>
<%@ Import Namespace="Kokugen.Web.Conventions"%>
<%@ Import Namespace="Kokugen.Web.Actions.Board.SaveColumn" %>

<div id="new-column-container" class="hidden">
<%= this.FormFor(new BoardColumnInputModel()).Id("column-form")%>
       
    <%= this.Edit(x => x.Column.Name) %>
    <%= this.Edit(x => x.Column.Description)%>
    <%= this.Edit(x => x.Column.CardLimit)%>
    <%= this.InputFor(x => x.Id).Hide() %>
    <%= this.InputFor(x => x.ProjectId) %>
    
     <input type="submit" name="Submit" value="Save" id="col-save-button"/>
<%= this.EndForm() %>

</div>

<script type="text/javascript">
    // Array Remove - By John Resig (MIT Licensed)
    Array.prototype.remove = function(from, to) {
        var rest = this.slice((to || from) + 1 || this.length);
        this.length = from < 0 ? this.length + from : from;
        return this.push.apply(this, rest);
    };
    function showColumnForm() {
        
        $("#new-column-container").slideToggle('slow');
    }

    function addColumn() {
        $('#column-form [name=Id]').val(null);
        showColumnForm();
    }
    function closeColumnDialog(response) {

        var column = null;
        
        for (var i in boardColumns) {
            if (boardColumns[i].Id == response.Item.Id) {
                column = boardColumns[i];
                $(column.Element).remove();
                boardColumns.remove(i);
            }
        }


            var item = new BoardColumn(response.Item);
            boardColumns.push(item);
            $(item.Element).addClass("draggable phase");

            $("#new-column-container").slideToggle('slow');

            $(item.Element).insertBefore($("#board-columns li:last"));
            updateColumns();
            bindSortingAndButtons();
    }

    $(document).ready(function() {
        $("#column-form").validate({ errorClass: "error" });
        $('#col-save-button').submit(function() {
            ValidateAndSave(closeColumnDialog, $("#column-form"));
            return false;
        });
    });
</script>

