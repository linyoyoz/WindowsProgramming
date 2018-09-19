<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DetailManageMent.aspx.cs" Inherits="DetailManageMent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>日期:
        <asp:Label ID="Label1" runat="server" Text="">

        </asp:Label>

    </p>
    <p>路線代碼:
        <asp:Label ID="Label2" runat="server" Text="">

        </asp:Label>
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="CaseID" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="CaseID" HeaderText="CaseID" ReadOnly="True" SortExpression="CaseID" />
                <asp:TemplateField HeaderText="StartTime" SortExpression="StartTime">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("StartTime") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("StartTime") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
                <asp:BoundField DataField="CardID" HeaderText="CardID" SortExpression="CardID" />
                <asp:CheckBoxField DataField="State" HeaderText="State" SortExpression="State" />
            </Columns>
        </asp:GridView>
    </p>
    <p>
        &nbsp;</p>
    
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:bustest001ConnectionString %>" 
        SelectCommand="
        select CaseID,ConstantFrequency.StartTime,Driver.name,Bus.CardID,State
        from Cases,ConstantFrequency,Driver,Bus
        where Cases.ConstantFrequencyID=ConstantFrequency.ConstantFrequencyID
        and ConstantFrequency.RouteID=@RouteID
        and Cases.USeDate=@date
        and Driver.DriverID = Cases.DriverID
        and Bus.BusID = Cases.BusID
" 
        UpdateCommand="
        UPDATE Cases 
        SET BusID = @CardID,  DriverID =@name, State =@State
        FROM Cases,ConstantFrequency
        where CaseID =@CaseID" >
        <SelectParameters>
            <asp:ControlParameter ControlID="Label2" Name="RouteID" PropertyName="Text" />
            <asp:ControlParameter ControlID="Label1" Name="date" PropertyName="Text" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="CardID" Type="Int32" />
            <asp:Parameter Name="name" Type="Int32" />
            <asp:Parameter Name="State" Type="Boolean" />

            <asp:Parameter Name="CaseID" Type="Int32" />

        </UpdateParameters>
    </asp:SqlDataSource>
    
    <br />
    <br />
</asp:Content>

