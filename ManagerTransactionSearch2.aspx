﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ManagerMaster.Master" CodeBehind="ManagerTransactionSearch2.aspx.vb" Inherits="KProject.ManagerTransactionSearch2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <link rel="shortcut icon" href="/favicon-4.ico" type="image/x-icon" />
    <link rel="shortcut icon" href="http://localhost:51539/EmployeeManageCustomers.aspx" />
    <link href="ContentStyle.css" rel="stylesheet" type="text/css" />
        

<asp:Panel ID="TransactionSearch" runat="server" Visible ="true">

                 <div id="title">
            Transaction Search<br />
            <asp:Label ID="lblAccountNumber" runat="server"></asp:Label>
            <br />
            <br />
        </div>

       
        <div id="a1">
            <strong>Search by Description:</strong><br />
&nbsp;<asp:TextBox ID="txtDescriptionSearch" runat="server"></asp:TextBox>
           <div id="content2">
           <div id="radiobuttonfix">
             <asp:RadioButtonList ID="rblDescription" runat="server" style="text-align: left">
                <asp:ListItem>Partial Search</asp:ListItem>
                <asp:ListItem>Keyword Search</asp:ListItem>
            </asp:RadioButtonList>
               </div>
                </div>
        </div>
        

        <div id="b1">
            <strong>Search by Transaction Type:
            </strong>
            <br />
            <asp:DropDownList ID="ddlSearchByTransactionType" runat="server">
                <asp:ListItem>Deposit</asp:ListItem>
                <asp:ListItem>Withdrawal</asp:ListItem>
                <asp:ListItem>Transfer</asp:ListItem>
                <asp:ListItem>Payment</asp:ListItem>
                <asp:ListItem>Fee</asp:ListItem>
                <asp:ListItem>eBill Payment</asp:ListItem>
            </asp:DropDownList>
            <br />
        </div>


        <div id="c1">
            <strong>Search by Price:</strong><br />
            <div id="Div1">
               <div id="Div2">
            <asp:RadioButtonList ID="rblPrice" runat="server" style="text-align: left">
                <asp:ListItem>0-$100</asp:ListItem>
                <asp:ListItem>101 - $200</asp:ListItem>
                <asp:ListItem>201 - $300</asp:ListItem>
                <asp:ListItem>$300+</asp:ListItem>
                <asp:ListItem>Custom Range</asp:ListItem>
            </asp:RadioButtonList>
                   </div>
                </div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            Between 
            <asp:TextBox ID="txtSearchByOtherPriceMin" runat="server" Width="65px"></asp:TextBox>
            And
            <asp:TextBox ID="txtSearchByOtherPriceMax" runat="server" Width="65px"></asp:TextBox>
            <br />
            <br />
        </div>


        <div id="d1">
            <strong>Search by Transaction Number:</strong><br />
&nbsp;<asp:TextBox ID="txtSearchByTransactionNumber" runat="server" Width="166px"></asp:TextBox>
        </div>


        <div id="e1">
            <strong>Search by Date:</strong><br />
            <div id="Div3">
               <div id="Div4">
            <asp:RadioButtonList ID="rblDate" runat="server" style="text-align: left">
                <asp:ListItem>Last 15 Days</asp:ListItem>
                <asp:ListItem>Last 30 Days</asp:ListItem>
                <asp:ListItem>Last 60 Days</asp:ListItem>
                <asp:ListItem>All Available</asp:ListItem>
                <asp:ListItem>Custom Date Range</asp:ListItem>
            </asp:RadioButtonList>
                   </div>
                </div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            Between&nbsp; 
            <asp:Calendar ID="calFirstDate" runat="server"></asp:Calendar>
            &nbsp;And
            <asp:Calendar ID="calSecondDate" runat="server"></asp:Calendar>
            Days
            <br />
            <br />
            <br />
        </div>


        <div id ="footer">
            <div id ="a">
                <div id ="Div5">
                Select which parameters you would like to search by:
                <asp:CheckBoxList ID="cblParameters" runat="server">
                <asp:ListItem>Description</asp:ListItem>
                <asp:ListItem>Transaction Type</asp:ListItem>
                <asp:ListItem>Price</asp:ListItem>
                <asp:ListItem>Transaction Number</asp:ListItem>
                <asp:ListItem>Date</asp:ListItem>
            </asp:CheckBoxList>
            </div>
                </div>
            <div id ="b">
              Select an order for your results:
            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                <asp:ListItem>Ascending</asp:ListItem>
                <asp:ListItem>Descending</asp:ListItem>
            </asp:RadioButtonList>
            </div>
        <div id="c">
            <br />
            <asp:Button ID="btnSearch" runat="server" Text="Search" />
            <asp:Button ID="btnClear" runat="server" Text="Clear Selections / Show All" Width="295px" />
            <br />
            <br />
            </div>
            <div id ="d">
&nbsp;<asp:Label ID="lblNumberOfTransactions" runat="server"></asp:Label>
            <br />
                <asp:Label ID="lblError" runat="server"></asp:Label>
            <br />
                </div>        
        </div>
        <div id ="Div6">
            <asp:GridView ID="gvTransactionSearch" runat="server">
                <Columns>
                    <asp:CommandField HeaderText="View Details" ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </div>

     </asp:Panel>
    </asp:content>