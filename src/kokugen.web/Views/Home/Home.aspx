<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Kokugen.Web.Views.Home.Home" %>
<%@ Import Namespace="Kokugen.WebBackend.Handlers.Company"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Hello World Home Page</title>
</head>
<body>
    <div>
        <%=Model.Text %>
        <%= this.LinkTo<CompanyListModel>().Text("Company List") %>
        
    </div>
</body>
</html>
