<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerMaster.Master" CodeBehind="CustomerPerformTransaction.aspx.vb" Inherits="KProject.CustomerPerformTransaction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link rel="shortcut icon" href="/favicon-4.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="http://localhost:51539/CustomerPerformTransaction.aspx" />
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
     <div id ="title">
        Perform Transaction
         <br />
         <br />
    </div>
    <div id="center">
        <asp:DropDownList ID="ddlTransactions" runat="server" AutoPostBack="True">
            <asp:ListItem>Pick a transaction type</asp:ListItem>
            <asp:ListItem>Withdrawal</asp:ListItem>
            <asp:ListItem>Deposit</asp:ListItem>
            <asp:ListItem>Transfer</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <br />
       
        <%-- Make sure to not have the 2 accounts the same --%><%-- because the money comes from an outside source, any amount is allowed --%>
         <asp:Panel ID="TransferPanel" runat="server" Visible =" false">
         <div id ="label">
             <asp:Label ID="Label1" runat="server" Text="Transfer Amount: "></asp:Label>
             <br />

             <asp:Label ID="Label2" runat="server" Text="Transfer From: "></asp:Label>
             <br />

             <asp:Label ID="Label7" runat="server" Text="Transfer To: "></asp:Label>
             <br />
             <%-- Default to Current Date --%>
             <asp:Label ID="Label4" runat="server" Text="Date:"></asp:Label>
             <br />
             <br />
             
             <br />
             <br />
             <br />
          </div>
        
             <div id="textbox">
                 <asp:TextBox ID="txtAmountTransfer" runat="server"></asp:TextBox>
             <br />
                 <asp:DropDownList ID="ddlFromAccount" runat="server">
             </asp:DropDownList>
                 <br />
              <%-- Make sure to not have the 2 accounts the same --%>
                 <asp:DropDownList ID="ddlTransferTo" runat="server">
                 </asp:DropDownList>
              
                 <br />
                 <asp:TextBox ID="txtTransferDate" runat="server"></asp:TextBox>
              
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ClientIDMode="Static" ControlToValidate="txtTransferDate" ErrorMessage="ERROR: Transfer Date Required">*</asp:RequiredFieldValidator>
              
                 <br />
                 <br />
             
                 <asp:Calendar ID="TransferCalendar" runat="server"></asp:Calendar>
             
                 <br />
                 <asp:Button ID="btnTransfer" runat="server" Text="Confirm Transfer" />
                 <br />
                 <asp:Label ID="lblErrorTransfer" runat="server"></asp:Label>
                 <br />
                 <asp:ValidationSummary ID="ValidationSummary3" runat="server" />
                 <br />
                 
             </div>
             
             

        
             

        
        </asp:Panel>


        <%-- because the money comes from an outside source, any amount is allowed --%>

<asp:Panel ID="DepositPanel" runat="server" Visible =" false">
         <div id ="label">
             <asp:Label ID="Label8" runat="server" Text="Deposit Amount: "></asp:Label>
             <br />

             <asp:Label ID="Label9" runat="server" Text="Deposit Into: "></asp:Label>
             <br />
             <asp:Label ID="Label10" runat="server" Text="Date:"></asp:Label>
             <br />
             
          </div>
        
             <div id="textbox">
                 <asp:TextBox ID="txtDepositAmount" runat="server"></asp:TextBox>
             <br />
                 <asp:DropDownList ID="ddlDeposit" runat="server">
             </asp:DropDownList>
                 <br />
              <%-- Default to Current Date --%>
                  <asp:TextBox ID="txtDepositDate" runat="server"></asp:TextBox>
              
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ClientIDMode="Static" ControlToValidate="txtDepositDate" ErrorMessage="ERROR: Deposit Date Required">*</asp:RequiredFieldValidator>
              
                 <br />
             
                 <asp:Calendar ID="DepositCalendar" runat="server"></asp:Calendar>
             
                 <br />
                 <asp:Button ID="btnDeposit" runat="server" Text="Confirm Deposit" />
                 <br />
                 <asp:Label ID="lblErrorDeposit" runat="server"></asp:Label>
                 <br />
                 <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                 <br />
             </div>
             
        </asp:Panel>

<asp:Panel ID="WithdrawalPanel" runat="server" Visible =" false">
         <br />
         <div id="label">

       <div id ="Div1">
             <asp:Label ID="Label13" runat="server" Text="Withdrawal Amount: "></asp:Label>
             <br />

             <asp:Label ID="Label14" runat="server" Text="Withdrawal From: "></asp:Label>
             <br />
             <%-- Default to Current Date --%>
             <asp:Label ID="Label15" runat="server" Text="Date:"></asp:Label>
             <br />
             <br />
           
          </div>
        


         </div>
        
             <div id="textbox">
                 <asp:TextBox ID="txtWithdrawalAmount" runat="server"></asp:TextBox>
             <br />
                 <asp:DropDownList ID="ddlWithdrawal" runat="server">
             </asp:DropDownList>
                 <br />
                <asp:TextBox ID="txtWithdrawalDate" runat="server"></asp:TextBox>
              
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ClientIDMode="Static" ControlToValidate="txtWithdrawalDate" ErrorMessage="ERROR: Withdrawal Date Required">*</asp:RequiredFieldValidator>
              
                 <br />
                 <br />
             
                 <asp:Calendar ID="WithdrawalCalendar" runat="server"></asp:Calendar>
                 
                 <br />
                 <asp:Button ID="btnWithdrawal" runat="server" Text="Confirm Withdrawal" />
                 
                 <br />
                 <asp:Label ID="lblErrorWithdrawal" runat="server"></asp:Label>
                 <br />
                 <asp:ValidationSummary ID="ValidationSummary2" runat="server" />
                 <br />
                 
             </div>
             

        
         <br />
         <br />
             

        
        </asp:Panel>

        <asp:Panel ID="IRAFeeChoicePanel" runat="server" Visible =" false">
         <div id ="div2">
             You are younger than 65, and are therefore attempting to make an unqualified distribution from your IRA acocunt.<br /> There is a $30 service fee associated with unqualified distributions.<br /> Would you like to add the fee to your withdrawal amount or have the total amount entered include the fee?<br />
             <br />
             <asp:Button ID="btnAddFee" runat="server" Text="Add Fee" />
             <asp:Button ID="btnIncludeFee" runat="server" Text="Include Fee" />
             <br />
             <br />
             <asp:Label ID="lblIRA" runat="server"></asp:Label>
             <br />
             
          </div>       
        </asp:Panel>




        <br />
        <br />



    </div>
    

     <br />
     <br />
     <br />
     <br />
     <br />
     <br />
    

</asp:Content>

