<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Board.Configure" AutoEventWireup="true" MasterPageFile="~/Shared/Site.Master" %>
<%@ Import Namespace="Kokugen.Web.Actions.Board"%>
<asp:Content ID="columnconfigHead" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript">

    $(document).ready(function() {
    $('#board-columns').sortable({
    items: '> *:not(".fixed")', placeholder: 'phase-placeholder', forcePlaceholderSize: true
        });
    });
</script>

</asp:Content>
<asp:Content ID="THISCONTENTAREAID" ContentPlaceHolderID="mainContent" runat="server">
<div class="board-column-configure ui-sortable">
    <ul id="board-columns">
        <%= this.PartialForEach(m => m.BoardColumns).Using<BoardItem_Control>() %>
    </ul>
</div>
</asp:Content>