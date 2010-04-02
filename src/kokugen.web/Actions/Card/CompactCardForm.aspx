<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Card.CompactCardForm"%>
<%@ Import Namespace="HtmlTags"%>
<%@ Import Namespace="Kokugen.Web.Actions.Board"%>
<%@ Import Namespace="Kokugen.Web.Conventions"%>

<div id="compact-card-container" class="hidden">
<%= this.FormFor(new BoardColumnInputModel()).Id("card-form-compact")%>
    <div class="add-card-compact">
        <div class="full-width-input">
            <%= this.InputFor(x => x.Card.Title).Hint("Enter description of card") %>
        </div>
        <div class="quarter-input">
            <%= this.InputFor(x => x.Card.Size).Hint("Card Size (Optional)") %>
        </div>
        <div class="quarter-input">
            <%= this.InputFor(x => x.Card.Priority).Hint("Priority") %>
        </div>
        <div class="quarter-input">
            <%= this.InputFor(x => x.Card.Deadline).Hint("Deadline") %>
        </div>
        <div class="quarter-input">
            <%= this.InputFor(x => x.Card.AssignedTo).Hint("Who is Assigned to this") %>
        </div>
        <%= this.InputFor(x => x.Card.Id).Hide() %>
        <%= this.InputFor(x => x.ProjectId).Hide() %>
        <div class="actions">
             <input type="submit" name="Submit" value="Add to Backlog" id="save-button" class="button grn"/>
             <input type="submit" name="Submit" value="Add to Board" id="save-button" class="button orng"/>
         </div>
     </div>
     <div class="bottom-border"></div>
     <div class="clear"></div>
</form>

</div>

<script type="text/javascript">


    $(document).ready(function() {
        $("#card-form-compact").validate({ errorClass: "error" });
        $('#save-button').submit(function() {
        ValidateAndSave(closeColumnDialog, $("#card-form-compact"));
        });
        
    });
</script>

