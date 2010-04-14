<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Account.LoginStatus.LoginStatus" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Register" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Manage" %>

<%if(Model.IsAuthenticated) { %>
      Welcome <b><%=this.LinkTo(new ManageAccountRequest(){UserName = Model.UserName}).Text(Model.UserName).Title("Manage Account") %></b>!
      [ <a href="<%=Model.RawUrl %>">Log Off</a> ]
<%} else {%>
      [ <a href="<%=Model.RawUrl %>">Log In</a> ]
      <span><%=this.LinkTo(new RegisterAccountRequest()).Text("Register") %></span>
<%} %>

