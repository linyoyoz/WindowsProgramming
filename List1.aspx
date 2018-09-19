<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="List1.aspx.cs" Inherits="WebSite7_List1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
.style1 {
	margin-top:10px;
    text-align:center;
    vertical-align:middle;
}
.style2{
    padding-left:1000px;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="style1">
    <h1>乘車資訊查詢</h1>
    </div>
        <div class="style1">
            車號&nbsp; 
            <asp:TextBox ID="carname" runat="server"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            <br />
            <br />
        </div>
        <div style=" center;padding-left: 300px ">
            方向:
        </div>
            <div style="padding-left: 350px ">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" >
                     <asp:ListItem Value="go">去程</asp:ListItem>
                    <asp:ListItem Value="back">回程</asp:ListItem>
                </asp:RadioButtonList>
                <asp:Label ID="test" runat="server" Text=""></asp:Label>
            </div>
            <br />
            <br />
        <div class="style1">
            <asp:Button ID="Button1" runat="server" Text="查詢" OnClick="Button1_Click" />
            <br />
            <br />
            <br />
            <asp:ListBox ID="ListBox1" runat="server" Width="190px" Height="83px">
                <asp:ListItem>班次          發車時間</asp:ListItem>
            </asp:ListBox>
&nbsp;&nbsp;
            </div>
</asp:Content>
