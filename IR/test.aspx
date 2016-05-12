<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="IR.test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtpass" runat="server"></asp:TextBox>
        <asp:Button ID="btnclick" runat="server" Text="Click" OnClick="btnclick_Click" />
        <asp:Label ID="lbl" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
