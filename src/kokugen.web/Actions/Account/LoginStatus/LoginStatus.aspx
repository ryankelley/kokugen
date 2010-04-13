<%@ Page Language="C#" Inherits="Kokugen.Web.Actions.Account.LoginStatus.LoginStatus" %>
<%@ Import Namespace="Kokugen.Web.Actions.Account.Register" %>

<%if(Model.IsAuthenticated) { %>
      Welcome <b><%=Model.UserName %></b>!
      [ <a href="<%=Model.RawUrl %>">Log Off</a> ]
<%} else {%>
      [ <a href="<%=Model.RawUrl %>">Log In</a> ]
      <span><%=this.LinkTo(new RegisterAccountRequest()).Text("Click Here to Register") %></span>
<%} %>

