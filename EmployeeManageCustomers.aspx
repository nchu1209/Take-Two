<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/EmployeeMaster.Master" CodeBehind="EmployeeManageCustomers.aspx.vb" Inherits="KProject.EmployeeManageCustomers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link rel="shortcut icon" href="/favicon-4.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="http://localhost:51539/EmployeeManageCustomers.aspx" />
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
    
       

    
    <div id ="title">
        Manage Customer&#39;s Account
        <br />
        <br />
    </div>

    <asp:Panel ID="SearchForCustomers" runat="server" Visible ="true">
    <div id="SearchForCustomer">

        Search for Customer(Customer Number):<asp:TextBox ID="txtCustomerNumber" runat="server"></asp:TextBox>

        <br />
        <asp:Button ID="btnFind" runat="server" Text="Find Customer Information" />
        <br />
        <asp:Label ID="lblCustomerNumberError" runat="server"></asp:Label>

        <br />

    </div>

        
    </asp:Panel>

     <asp:Panel ID="ModifyProfile" runat="server" Visible ="true">

    <div id ="lefthalf">
    <div id ="subtitle">
        Modify Profile
        <br />
        <br />
    </div>
    <div id ="label2">
        <asp:Label ID="Label3" runat="server" Text="Last Name:"></asp:Label>
            <br />
            <asp:Label ID="Label4" runat="server" Text="First Name:"></asp:Label>
            <br />
            <asp:Label ID="Label5" runat="server" Text="Middle Initial:"></asp:Label>
            <br />
            <asp:Label ID="Label7" runat="server" Text="Password:"></asp:Label>
            <br />
            <asp:Label ID="Label8" runat="server" Text="Address:"></asp:Label>
            <br />
            <asp:Label ID="Label9" runat="server" Text="City:"></asp:Label>
            <br />
            <asp:Label ID="Label10" runat="server" Text="State:"></asp:Label>
            <br />
            <asp:Label ID="Label11" runat="server" Text="Zip Code:"></asp:Label>
            <br />
            <asp:Label ID="Label12" runat="server" Text="Email:"></asp:Label>
            <br />
            <asp:Label ID="Label13" runat="server" Text="Phone Number:"></asp:Label>
            <br />
    </div>

          
    <div id ="textbox">
        <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLastName" ErrorMessage="ERROR: Last Name Required" ClientIDMode="Static">*</asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ClientIDMode="Static" ControlToValidate="txtFirstName" ErrorMessage="ERROR: First Name Required">*</asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtInitial" runat="server" MaxLength="1"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtPassword" runat="server" MaxLength="10"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ClientIDMode="Static" ControlToValidate="txtPassword" ErrorMessage="ERROR: Password Required">*</asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ClientIDMode="Static" ControlToValidate="txtAddress" ErrorMessage="ERROR: Address Required">*</asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtCity" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtState" runat="server" MaxLength="2" ReadOnly="True"></asp:TextBox>
            <br />
            <asp:TextBox ID="txtZip" runat="server" MaxLength="5"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ClientIDMode="Static" ControlToValidate="txtZip" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ClientIDMode="Static" ControlToValidate="txtZip" ErrorMessage="ERROR: Incorrect Zip Code Format" ValidationExpression="\d{5}">*</asp:RegularExpressionValidator>
            <br />
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ClientIDMode="Static" ControlToValidate="txtEmail" ErrorMessage="ERROR: Email Required">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ClientIDMode="Static" ControlToValidate="txtEmail" ErrorMessage="ERROR: Invalid Email." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
            <br />
            <asp:TextBox ID="txtPhone" runat="server" MaxLength="10"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ClientIDMode="Static" ControlToValidate="txtPhone" ErrorMessage="ERROR: Phone Number">*</asp:RequiredFieldValidator>
            <br />
            <br />
       </div>
        <div id="content">
        <br />
            <asp:Button ID="btnSaveProfile" runat="server" Text="Save" />
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancelProfile" runat="server" Text="Cancel" CausesValidation="False" />
        <br />
        <br />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <asp:Label ID="lblError" runat="server"></asp:Label>
            <br />
    </div>

        <br/>
        <div id="content">
        <div id="subtitle">
            Customer Status
        </div>

        <div id="content">
            <asp:Label ID="lblAccountStatus" runat="server"></asp:Label>
            <br />
            </div>
           <%-- <div id="content">
                <div id="textbox">
                    <br />
                    <br />
                    <br />
                    <br/>
                    </div>
                <div id="label2">
            <asp:RadioButtonList ID="radEnableDisable" runat="server" AutoPostBack="True" Width="67px">
                <asp:ListItem>Enable</asp:ListItem>
                <asp:ListItem>Disable</asp:ListItem>
            </asp:RadioButtonList>
                    </div>
        
                </div>--%>
            <br />
            <div id="content">
            <asp:Button ID="btnChangeStatus" runat="server" Text="Change Status" />
            </div>
        
        </div>
        <br/>
    </div>






    </asp:Panel>

    <asp:Panel ID="AccountNames" runat="server" Visible ="true">

    <div id ="righthalf">
        <div id ="subtitle">
        Change Account Names<br />
        <br />
    </div>
    <div id ="Div2">
        
        <div id ="label2">
             <asp:Label ID="Label14" runat="server" Text="Choose Account: "></asp:Label>
             <br />
        <asp:Label ID="Label1" runat="server" Text="Change Name To: "></asp:Label>
             </div>
       
        <div id="textbox">
        <asp:DropDownList ID="ddlAccounts" runat="server" AutoPostBack="True" Height="25px">
        </asp:DropDownList>
        <br />
        <asp:TextBox ID="txtChangeName" runat="server"></asp:TextBox>
            <br />
        <br />
        </div>


        <div id="center">
               <br/>
        <asp:Button ID="btnSaveAccountName" runat="server" CausesValidation="False" Text="Save" />
        <asp:Button ID="btnCancelAccountName" runat="server" CausesValidation="False" Text="Cancel" />
            <br />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        
    </div>
                   
        <br />
        <br />
        <br />
        <br />
    </div>
        </asp:Panel>
    <asp:Panel ID="Password" runat="server" Visible ="false">
        <div id ="Div6">
        <br />
        <br />
    </div>
        <br />
    </asp:Panel>
     <br />
     <br />
     <br />
     <br />
    <br />
     <br />
     <br />
<br />
<br />
<br />
<br />
<br />
<br />
     <br />
     <br />
<br />
     <br />
</asp:Content>