<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Board.EditBoardColumnForm"%>
<%@ Import Namespace="HtmlTags"%>
<%@ Import Namespace="Kokugen.Web.Actions.Board"%>
<%@ Import Namespace="Kokugen.Web.Conventions"%>

<div id="new-column-container" class="hidden">
<%= this.FormFor(new BoardColumnInputModel()).Id("column-form")%>
       
    <%= this.Edit(x => x.Column.Name) %>
    <%= this.Edit(x => x.Column.Description)%>
    <%= this.Edit(x => x.Column.Limit)%>
    <%= this.InputFor(x => x.Id).Hide() %>
    <%= this.InputFor(x => x.ProjectId) %>
    
     <input type="submit" name="Submit" value="Save" id="col-save-button"/>
</form>

</div>

<script type="text/javascript">

    function showColumnForm() {
        $("#new-column-container").slideToggle('slow');
    }
    
    function closeColumnDialog(response) {
        var html = tmpl($("#ItemTemplate").html(), response.Item);
        $("#new-column-container").slideToggle('slow');

        $(html).insertBefore($("#board-columns li:last"));
        updateColumns();
        bindSortingAndButtons();
    }

    $(document).ready(function() {
        $("#column-form").validate({ errorClass: "error" });
        $('#col-save-button').submit(ValidateAndSave(closeColumnDialog, $("#column-form")));
    });
</script>

