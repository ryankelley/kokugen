<%@ Page Language="C#" AutoEventWireup="true" Inherits="Kokugen.Web.Actions.Account.Register.Register" 
 MasterPageFile="~/Shared/Site.Master"
%>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Register"%>

<asp:Content ContentPlaceHolderID=mainContent runat=server>
<%=this.FormFor<RegisterAccountModel>() %>

    <fieldset>
        <legend>Register</legend>
             <%=this.Edit(x => x.User.UserName) %>
             <%=this.Edit(x => x.User.FirstName) %>
             <%=this.Edit(x => x.User.LastName) %>
             <%=this.Edit(x => x.User.Email) %>
             <%=this.Edit(x => x.User.Password) %>
             <%if (Model.Settings.RequiresQuestionAndAnswer)
               {%>
             <%=this.Edit(x => x.User.Question) %>
             <%=this.Edit(x => x.User.Answer) %>
             <%
               }%>

             <input type='submit' value='Register' />
    </fieldset>

<%=this.EndForm() %>

</asp:Content>
