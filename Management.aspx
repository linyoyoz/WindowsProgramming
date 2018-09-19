<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Management.aspx.cs" Inherits="Management" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        <br />
&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
&nbsp; 路線&nbsp; 
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
            <asp:ListItem Value="1">去</asp:ListItem>
            <asp:ListItem Value="0">回</asp:ListItem>
        </asp:RadioButtonList>
    </p>
    <p>
&nbsp;日期
        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
    </p>
    <p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="查詢" />
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</asp:Content>

