<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustomerCreateAccount.aspx.vb" Inherits="KProject.CustomerCreateAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="shortcut icon" href="/favicon-4.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="http://localhost:51539/CustomerCreateAccount.aspx" />
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <div id="lefttopcorner">
            <img class="auto-style1" src="Petunia2.jpg" /><br />
            <br />
            <br />
         </div>


        <div id="rightcornertitle">
            <div id="title">
                <br />
                Set up your new account!<br />
                <br />
                <br />
            </div>
        </div>

       
        <div id="label">
           

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
            <asp:Label ID="Label11" runat="server" Text="Zip Code:"></asp:Label>
            <br />
            <asp:Label ID="Label12" runat="server" Text="Email:"></asp:Label>
            <br />
            <asp:Label ID="Label13" runat="server" Text="Phone Number:"></asp:Label>
            <br />
            Birth Year:<br />
           
           

        </div>


                <div id="textbox2">
            <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ClientIDMode="Static" ControlToValidate="txtLastName" ErrorMessage="ERROR: Last Name Required">*</asp:RequiredFieldValidator>
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ClientIDMode="Static" ControlToValidate="txtAddress" ErrorMessage="ERROR: Address Required">*</asp:RequiredFieldValidator>
            <br />
            <asp:TextBox ID="txtZip" runat="server" MaxLength="5"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ClientIDMode="Static" ControlToValidate="txtZip" ErrorMessage="ERROR: Zip Required">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtZip" ErrorMessage="ERROR: Incorrect Zip Code Format" ValidationExpression="\d{5}">*</asp:RegularExpressionValidator>
            <br />
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ClientIDMode="Static" ControlToValidate="txtEmail" ErrorMessage="ERROR: Email Required">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ClientIDMode="Static" ControlToValidate="txtEmail" ErrorMessage="ERROR: Invalid Email." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
            <br />
            <asp:TextBox ID="txtPhone" runat="server" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ClientIDMode="Static" ControlToValidate="txtPhone" ErrorMessage="ERROR: Phone Number Required">*</asp:RequiredFieldValidator>
                    <br />
            <asp:TextBox ID="txtDOB" runat="server" MaxLength="4"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ClientIDMode="Static" ControlToValidate="txtDOB" ErrorMessage="ERROR: Birth Year Required">*</asp:RequiredFieldValidator>
            <br />
            <br />
            <br />
            <asp:Button ID="btnSaveProfile" runat="server" Text="Save" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnCancelProfile" runat="server" Text="Cancel" />
                    <br />
            <br />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <asp:Label ID="lblError" runat="server"></asp:Label>
                    <br />
                    <br />
        </div>
        
       
        
     
         <div id="footer">
       
            
             <br />
        <br />
        Website Created by One or None, Ltd.
        <br />
        Group 3: Leah Carroll, Nicole Chu, Amy Enrione, Catherine King
        </div>
        

   </form>
  
</body>
</html>
