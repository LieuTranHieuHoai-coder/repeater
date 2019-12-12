<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kiemtra.aspx.cs" Inherits="repeater_2.kiemtra" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="bootstrap4-offline-docs-master/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="bootstrap4-offline-docs-master/dist/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Image ID="Image1" runat="server" />
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem Value="1">Active</asp:ListItem>
                <asp:ListItem Value="0">Disable</asp:ListItem>
            </asp:DropDownList>
            <asp:FileUpload ID="FileUpload1" runat="server" /><br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Them" />
            <asp:Button ID="btn_xoa" runat="server" OnClick="btn_xoa_Click" Text="Xoa" />
            <asp:Button ID="btn_update" runat="server" OnClick="btn_update_Click" Text="Update" />
            <br />
            <br />

            <br />
            <asp:TextBox ID="txt_key" runat="server"></asp:TextBox>
            <asp:Button ID="btn_tim" OnClick="btn_tim_Click" runat="server" Text="Search" />
           
            <table class="table table-bordered table-dark table-striped">
                <tr>
                    <td>ID</td>
                    <td>Image</td>
                    <td>Name</td>
                    <td>Stt</td>
                </tr>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("id") %></td>
                            <td>
                                <img src="img/<%# Eval("img") %>" />
                            </td>
                            <td><%# Eval("Name") %></td>
                            <td><%# Eval("stt").ToString()=="1"?"Active":"Disable" %></td>
                            <td><a href="?id=<%# Eval("id") %>">Edit</a></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>


            </table>

            <asp:Repeater ID="Repeater2" runat="server">

                <ItemTemplate>
                    <a href="?page=<%#Eval("index") %><%if (Request["id"] != null) Response.Write("&id=" + Request["id"]); %>"><%#Eval("index") %> </a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</body>
</html>
