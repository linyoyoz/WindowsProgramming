<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Select.aspx.cs" Inherits="Select" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:bustest001ConnectionString %>" SelectCommand="SELECT * FROM [Bus]"></asp:SqlDataSource>
    </div>
        <p>起點:
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            終點:
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </p>
    <p>時間:<asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem Value="-1">-</asp:ListItem>
        <asp:ListItem>01</asp:ListItem>
        <asp:ListItem>02</asp:ListItem>
        <asp:ListItem>03</asp:ListItem>
        <asp:ListItem>04</asp:ListItem>
        <asp:ListItem>05</asp:ListItem>
        <asp:ListItem Value="6">06</asp:ListItem>
        <asp:ListItem>07</asp:ListItem>
        <asp:ListItem>08</asp:ListItem>
        <asp:ListItem Value="9">09</asp:ListItem>
        <asp:ListItem>10</asp:ListItem>
        <asp:ListItem>11</asp:ListItem>
        <asp:ListItem>12</asp:ListItem>
        </asp:DropDownList>
        時<asp:DropDownList ID="DropDownList2" runat="server">
            <asp:ListItem Value="-1">-</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>20</asp:ListItem>
            <asp:ListItem>30</asp:ListItem>
            <asp:ListItem>40</asp:ListItem>
            <asp:ListItem>50</asp:ListItem>
            <asp:ListItem Value="0">00</asp:ListItem>
        </asp:DropDownList>
        分</p>
    <p>時間限制:<asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Value="0">不限制</asp:ListItem>
        <asp:ListItem Value="10">10分鐘</asp:ListItem>
        <asp:ListItem Value="30">30分鐘</asp:ListItem>
        <asp:ListItem Value="60">1小時</asp:ListItem>
        <asp:ListItem Value="120">2小時</asp:ListItem>
        <asp:ListItem Value="180">3小時</asp:ListItem>
        </asp:RadioButtonList>
    </p>
    <p>
        <asp:Button ID="Button1" runat="server" Text="搜尋" OnClick="Button1_Click" />
        </p>
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
</asp:Content>
