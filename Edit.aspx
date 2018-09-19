<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style2 
        {
            text-align: center;
        }
        .新增樣式1 {
            color: #82D2F7;
        }
        .auto-style3 {
            color: #82D2F7;
            font-size: x-large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    
    <table style="width:100%;">
        <tr>
            <td class="auto-style2">
                <a href="EditRoute.aspx"><img alt="" src="images/image005.png"/></a>
                <br />
                <span class="auto-style3"><strong>路線編輯</strong></span></td>
            <td class="auto-style2">
                <a href="EditStop.aspx"><img alt="" src="images/image006.png"/></a>
                <span class="auto-style3"><strong>
                <br />
                站牌編輯</strong></span>
            </td>
            <td class="auto-style2">
                <a href="EditBus.aspx"><img alt="" src="images/image007.png" /></a>
                <span class="auto-style3"><strong>
                <br />
                車次編輯</strong></span>
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <a href="InsertRoute.aspx"><img alt="" src="images/image008.png" /></a>
                <span class="auto-style3"><strong>
                <br />
                路線新增</strong></span>
            </td>
            <td class="auto-style2">
                <a href="InsertStop.aspx"><img alt="" src="images/image009.png" /></a>
                <span class="auto-style3"><strong>
                <br />
                站牌新增</strong></span>
            </td>
            <td class="auto-style2">
                <a href="InsertBus.aspx"><img alt="" src="images/image010.png" /></a>
                <span class="auto-style3"><strong>
                <br />
                車次新增</strong></span>
            </td>
        </tr>
        
    </table>
    
    
    
</asp:Content>

