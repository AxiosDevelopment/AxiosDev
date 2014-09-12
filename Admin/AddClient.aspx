<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AddClient.aspx.vb" Inherits="Axios.AddClient" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title>Axios Main Menu</title>
  <link href="../default.css" rel="stylesheet" type="text/css" media="all" />
  <style type="text/css">
    @import "../layout.css";
  </style>
  <script type="text/javascript" src="../Scripts/jquery.js"></script>
  <script type="text/javascript" src="../Scripts/init.js"></script>
  <script type="text/javascript">
    function messagedSaved() {
      alert("The Client was saved successfully!");
      window.location = "AddClient.aspx";
    }

    function messagedSavedError() {
      alert("There was an error inserting/updating the client. Please contact an administrator for assistance.");
    }

    function messageLoadError() {
      alert("There was an error loading the Add Client page. Please contact an administrator for assistance.");
    }
  </script>
</head>
<body class="single">
  <div id="wrapper">
    <div id="wrapper-bgtop">
      <div id="header">
        <div id="logo">
          <h1><a href="#">Axios Online</a></h1>
          <p></p>
        </div>

      </div>
      <div id="menu">
        <ul>
          <li class="first"><a href="../Main.aspx" title="">Main Menu</a></li>
          <!-- POPUP WITH FORM TO ADD NEW FACILITY -->
          <li><a href="AddFacility.aspx" title="">Add Facility</a></li>
          <!-- POPUP WITH FORM TO ADD NEW DOCTOR -->
          <li><a href="AddPhysician.aspx" title="">Add Doctor</a></li>
        </ul>
      </div>
      <div id="page">
        <div id="banner-empty"></div>
        <div id="content">
          <div class="box-style4">
            <div class="title">
              <h2>Add a Client</h2>
              <div id="clientTable">
                <h4>Search for existing client:</h4>
                <input type="text" name="searchClient" id="searchClient" />
                <!--autocomplete begin-->
                <div class="searchAuto hide" id="clientSearch">
                  <ul class="autoSearch" id="clientAuto"></ul>
                </div>
                <!--autocomplete end-->
              </div>
              <form id="addNewClient" action="#" method="post" runat="server">
                <asp:ValidationSummary ID="ValidationSummaryAddClient" runat="server" CssClass="ErrorMessage" DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false" HeaderText="Please correct the following errors:" />

                <div id="">
                  <div class="row">
                    <label for="nClientName">Client Name</label><br />
                    <asp:TextBox ID="nClientName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqClientName" runat="server" ErrorMessage="Client Name is required" ControlToValidate="nClientName" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientNumber">Client Number</label><br />
                    <asp:TextBox ID="nClientNumber" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqClientNumber" runat="server" ErrorMessage="Client Number is required" ControlToValidate="nClientNumber" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="ClientType">Client Type</label><br />
                    <asp:DropDownList ID="ClientType" runat="server">
                      <asp:ListItem Value="-1" Text="--Select--" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="ReqClientType" runat="server" ErrorMessage="Client Type is required" ControlToValidate="ClientType" CssClass="ErrorMessage" Display="None" Text="*" InitialValue="-1"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientAddress">Client Address</label><br />
                    <asp:TextBox ID="nClientAddress" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqClientAddress" runat="server" ErrorMessage="Client Address is required" ControlToValidate="nClientAddress" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientCity">Client City</label><br />
                    <asp:TextBox ID="nClientCity" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqClientCity" runat="server" ErrorMessage="Client City is required" ControlToValidate="nClientCity" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientState">Client State</label><br />
                    <asp:TextBox ID="nClientState" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqClientState" runat="server" ErrorMessage="Client State is required" ControlToValidate="nClientState" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientZip">Client Zip</label><br />
                    <asp:TextBox ID="nClientZip" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqClientZip" runat="server" ErrorMessage="Client Zip is required" ControlToValidate="nClientZip" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientPhone">Client Phone</label><br />
                    <asp:TextBox ID="nClientPhone" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqClientPhone" runat="server" ErrorMessage="Client Phone is required" ControlToValidate="nClientPhone" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientExt">Client Ext</label><br />
                    <asp:TextBox ID="nClientExt" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqClientExt" runat="server" ErrorMessage="Client Phone Ext is required" ControlToValidate="nClientExt" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientPhone2">Client Phone 2</label><br />
                    <asp:TextBox ID="nClientPhone2" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqPhone2" runat="server" ErrorMessage="Client Phone Alt is required" ControlToValidate="nClientPhone2" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientFax">Client Fax</label><br />
                    <asp:TextBox ID="nClientFax" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqClientFax" runat="server" ErrorMessage="Client Fax is required" ControlToValidate="nClientFax" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientGreeting">Client Greeting</label><br />
                    <asp:TextBox ID="nClientGreeting" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqClientGreeting" runat="server" ErrorMessage="Client Greeting is required" ControlToValidate="nClientGreeting" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientHours">Client Hours of Operation</label><br />
                    <asp:TextBox ID="nClientHours" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqClientHours" runat="server" ErrorMessage="Client Hours is required" ControlToValidate="nClientHours" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientAdditionalInformation">Additional Information</label><br />
                    <asp:TextBox ID="nClientAdditionalInformation" runat="server" TextMode="MultiLine"></asp:TextBox>
                  </div>

                  <h2>Contact Information</h2>
                  <div class="row">
                    <label for="nClientPrimary">Client Primary Contact</label><br />
                    <asp:TextBox ID="nClientPrimary" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Client Primary Contact is required" ControlToValidate="nClientPrimary" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientPrimaryTitle">Client Primary Title</label><br />
                    <asp:TextBox ID="nClientPrimaryTitle" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqClientPrimaryTitle" runat="server" ErrorMessage="Client Primary Contact Title is required" ControlToValidate="nClientPrimaryTitle" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientPrimaryInfo">Client Primary Information (Phone Number, etc...)</label><br />
                    <asp:TextBox ID="nClientPrimaryInfo" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqClientPrimaryInfo" runat="server" ErrorMessage="Client Primary Contact Information is required" ControlToValidate="nClientPrimaryInfo" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientSecondary">Client Secondary Contact</label><br />
                    <asp:TextBox ID="nClientSecondary" runat="server"></asp:TextBox>
                  </div>

                  <div class="row">
                    <label for="nClientSecondaryTitle">Client Secondary Title</label><br />
                    <asp:TextBox ID="nClientSecondaryTitle" runat="server"></asp:TextBox>
                  </div>

                  <div class="row">
                    <label for="nClientSecondaryInfo">Client Secondary Information (Phone Number, etc...)</label><br />
                    <asp:TextBox ID="nClientSecondaryInfo" runat="server"></asp:TextBox>
                  </div>

                  <h2>Billing Info</h2>
                  <div class="row">
                    <label for="nClientBillingContact">Client Billing Contact</label><br />
                    <asp:TextBox ID="nClientBillingContact" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqClientBillingContact" runat="server" ErrorMessage="Client Billing Contact is required" ControlToValidate="nClientBillingContact" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientBillingPhone">Client Billing Phone</label><br />
                    <asp:TextBox ID="nClientBillingPhone" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqClientBillingPhone" runat="server" ErrorMessage="Client Billing Phone is required" ControlToValidate="nClientBillingPhone" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientBillingEmail">Client Billing Email</label><br />
                    <asp:TextBox ID="nClientBillingEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqClientBillingEmail" runat="server" ErrorMessage="Client Billing Email is required" ControlToValidate="nClientBillingEmail" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">
                    <label for="nClientSpecialInstructions">Client Special Instructions</label><br />
                    <asp:TextBox ID="nClientSpecialInstructions" runat="server" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ReqClientSpecialInstructions" runat="server" ErrorMessage="Client Special Instructions is required" ControlToValidate="nClientSpecialInstructions" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <input type="hidden" name="clientId" id="clientId" value="0" runat="server" />
                  <input type="reset" id="clearform" value="Clear Form" />
                  <asp:Button Text="Submit Client" runat="server" ID="SubmitClient" />
                </div>
              </form>
            </div>
          </div>
          <div class="entry">
          </div>
          <div class="clearfix">&nbsp;</div>
        </div>
      </div>
      <div class="clearfix">&nbsp;</div>
    </div>
  </div>
  </div>
  <div id="footer">
    <p>Copyright (c) 2014 Axios Communications. All rights reserved.</p>
  </div>
</body>
</html>
