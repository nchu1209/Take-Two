<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerPayBills.aspx.vb" Inherits="KProject.CustomerPayBills" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
    <div id ="title">
        Pay Bills<br />
        <br />
    </div>
    <div id ="center">
        <div id ="subtitle">Your Payees</div>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Select Account:"></asp:Label>
&nbsp;
        <asp:DropDownList ID="ddlAccount" runat="server" AutoPostBack="True">
        </asp:DropDownList>
        <br />
        <br />
        <br />
        </div>
        <div id ="gridviewleft">
        <asp:GridView ID="gvMyPayees" runat="server" AutoGenerateColumns="False" DataKeyNames="PayeeID" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:TemplateField HeaderText="PayeeID" SortExpression="PayeeID">
                    <EditItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("PayeeID") %>'></asp:Label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPayeeID" runat="server" Text='<%# Bind("PayeeID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PayeeName" SortExpression="PayeeName">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("PayeeName") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:hyperlink ID="lnkName" runat="server" NavigateUrl='<%# "CustomerUpdatePayee.aspx?ID=" & Eval("PayeeID")%>' Text='<%# Bind("PayeeName") %>'></asp:hyperlink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PayeeType" HeaderText="PayeeType" SortExpression="PayeeType" />
                <asp:TemplateField HeaderText="eBill">
                    <EditItemTemplate>
                        <asp:Button ID="btnBill" runat="server"></asp:Button>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:ImageButton ID="btnBill" runat="server" CommandName="GoToBill" 
      CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" ImageUrl="~/WhiteSpace.jpg" Enabled ="false" OnCommand="gvmypayees_rowcommand"></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Bill Amount">
                    <EditItemTemplate>
                        <asp:label ID="lblBillAmount" runat="server"></asp:label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:label ID="lblBillAmount" runat="server"></asp:label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Outstanding Balance">
                    <EditItemTemplate>
                        <asp:label ID="lblOutstandingBalance" runat="server"></asp:label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:label ID="lblOutstandingBalance" runat="server"></asp:label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Due Date">
                    <EditItemTemplate>
                        <asp:label ID="lblDueDate" runat="server"></asp:label>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:label ID="lblDueDate" runat="server"></asp:label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Payment Amount">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Payment Date">
                  <EditItemTemplate>
                        <asp:calendar ID="calDate" runat="server"></asp:calendar>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:calendar ID="calDate" runat="server"></asp:calendar>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MIS333K_msbck614ConnectionString2 %>" SelectCommand="usp_innerjoin_customerspayees_payees_by_customernumber" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:SessionParameter Name="customernumber" SessionField="CustomerNumber" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        </div>
        <div id ="gridviewright">
            <div id ="subtitle">Pay Bills Now<br />
            </div>
            <asp:Button ID="btnPay" runat="server" Text="Pay" />
        <br />
            <br />
            <asp:Label ID="lblMessageTotal" runat="server" Text="[]"></asp:Label>
            <br />
        <asp:Label ID="lblMessageFee" runat="server" Text="[]"></asp:Label>
            <br />
            <asp:Label ID="lblMessageFee2" runat="server" Text="[]"></asp:Label>
        <br />
        <br />
            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" />
&nbsp;
            <asp:Button ID="btnAbort" runat="server" Text="Abort" />
        <br />
        <br />
            <asp:Label ID="lblMessageSuccess" runat="server" Text="[]"></asp:Label>
        <br />
        <br />
            <div id ="subtitle">Set Up Minimum Payments<br />
            </div>
            <asp:Panel ID="pnlSetup" runat="server">
                Once you set up minimum payments, we will automatically allocate your chosen amount to pay your bills on the 1st of each month. Funds will be drawn from the account selected from the drop-down list at the top of this page.<br />
                <br />
                Select Amount or Enter Custom Amount:
                <asp:RadioButtonList ID="radAmount" runat="server">
                    <asp:ListItem Value="20.00">$20.00</asp:ListItem>
                    <asp:ListItem Value="30.00">$30.00</asp:ListItem>
                    <asp:ListItem Value="40.00">$40.00</asp:ListItem>
                    <asp:ListItem Value="50.00">$50.00</asp:ListItem>
                    <asp:ListItem Value="Custom">Custom Amount (Enter Below)</asp:ListItem>
                </asp:RadioButtonList>
                <br />
                Custom Amount:
            <asp:TextBox ID="txtCustomAmount" runat="server"></asp:TextBox>
            <br />
            <br />
                <asp:Button ID="btnSetup" runat="server" Text="Set Up Payments" />
                <br />
                <br />
                <asp:Label ID="lblMinimumMessage" runat="server" Text="[]"></asp:Label>
                <br />
            <br />
            </asp:Panel>
            <asp:Panel ID="pnlViewPayment" runat="server">
                <br />Thank you for enrolling in our minimum payments program. We will allocate <asp:Label ID="lblMinimumPayment" runat="server" Text="#"></asp:Label> to your eBills every month.
                <br />
                <br />
                If you would like to change your monthly payment amount, you may do so below.<br />
                <br />
                Select Amount or Enter Custom Amount:<br />
                <asp:RadioButtonList ID="radUpdateAmount" runat="server">
                    <asp:ListItem Value="20.00">$20.00</asp:ListItem>
                    <asp:ListItem Value="30.00">$30.00</asp:ListItem>
                    <asp:ListItem Value="40.00">$40.00</asp:ListItem>
                    <asp:ListItem Value="50.00">$50.00</asp:ListItem>
                    <asp:ListItem Value="Custom">Custom Amount (Enter Below)</asp:ListItem>
                </asp:RadioButtonList>
                <br />
                Custom Amount:
                <asp:TextBox ID="txtUpdateCustomAmount" runat="server"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnUpdate" runat="server" Text="Update Payment Amount" />
                <br />
                <br />
                <asp:Label ID="lblMessageUpdate" runat="server" Text="[]"></asp:Label>
                <br />
                <br />
                If you would like to opt out of our minimum payments program, simply click the button below:<br />
                <br />
                <asp:Button ID="btnOptOut" runat="server" Text="Opt Out" />
                <br />
                <br />
                <asp:Label ID="lblOptOutMessage" runat="server" Text="[]"></asp:Label>
            </asp:Panel>
            <br />
    </div>

</asp:Content>
