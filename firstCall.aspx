<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="firstCall.aspx.vb" Inherits="Axios.firstCall" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title>Axios First Call</title>
  <link href="default.css" rel="stylesheet" type="text/css" media="all" />
  <style type="text/css">
    @import "layout.css";
  </style>
  <script type="text/javascript" src="Scripts/jquery.js"></script>
  <script type="text/javascript" src="Scripts/init.js"></script>
  <script type="text/javascript">
    function messagedSaved() {
      alert("Your first call message was saved successfully!");
      window.location = "Main.aspx";
    }

    function messagedSavedError() {
      alert("There was an error inserting/updating your first call message. Please contact an administrator for assistance.");
    }

    function messageLoadError() {
      alert("There was an error loading the first call page. Please contact an administrator for assistance.");
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
          <li class="first"><a href="main.aspx" title="">Main Menu</a></li>
          <!-- POPUP WITH FORM TO FIND FIRST CALL -->
          <li><a href="#" title="">Search First Calls</a></li>
          <!-- POPUP WITH FORM TO ADD NEW DOCTOR -->
          <li><a id="pFCall" href="#" title="">Print First Call</a></li>
          <li><a id="sci" href="#" title="">Print SCI</a></li>
        </ul>
      </div>
      <div id="page">
        <div id="banner-empty">
          <div class="title">
            <h2 align="center">
              <asp:Literal ID="ClientHeader" runat="server" /></h2>
          </div>
        </div>
        <div id="content">
          <div class="box-style4">
            <div class="title">
              <h2>First Call</h2>
            </div>
            <div class="entry">
              <form id="addFirstCall" method="post" action="#" runat="server">
                <asp:HiddenField ID="FirstCallID" runat="server" />
                <asp:ValidationSummary ID="ValidationSummaryFirstCall" runat="server" CssClass="ErrorMessage" DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false" HeaderText="Please correct the following errors:" />
                <asp:ScriptManager ID="MsgScriptManager" runat="server">
                  <Scripts>
                    <asp:ScriptReference Name="jquery" />
                    <asp:ScriptReference Path="~/Scripts/WebForms/WebUIValidation.js" />
                  </Scripts>
                </asp:ScriptManager>
                <div class="left" id="fCallForm">
                  <div class="row">
                    <div class="left mr_10">
                      <label for="clientId">ClientId</label><br />
                      <asp:TextBox ID="clientId" runat="server" Width="100" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="clientName">Client Name</label><br />
                      <asp:TextBox ID="clientName" runat="server" Width="300" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="msgDate">Date</label><br />
                      <asp:TextBox ID="msgDate" runat="server" Width="80" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="msgTime">Time</label><br />
                      <asp:TextBox ID="msgTime" runat="server" Width="80" ReadOnly="true"></asp:TextBox>
                    </div>
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="reportingName">Reporting Party Name</label><br />
                      <asp:TextBox ID="reportingName" runat="server" Width="250"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqReportingName" runat="server" ErrorMessage="Reporting Party Name is required" ControlToValidate="reportingName" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="deceasedName">Deceased Name</label><br />
                      <asp:TextBox ID="deceasedName" runat="server" Width="200"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqDeceasedName" runat="server" ErrorMessage="Deceased Name is required" ControlToValidate="deceasedName" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="dDate">Date of Death</label><br />
                      <asp:TextBox ID="dDate" runat="server" Width="80"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqDateOfDeath" runat="server" ErrorMessage="Date of Death is required" ControlToValidate="dDate" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="dTime">Time of Death</label><br />
                      <asp:TextBox ID="dTime" runat="server" Width="80"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqTimeOfDeath" runat="server" ErrorMessage="Time of Death is required" ControlToValidate="dTime" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="ssn">SSN</label><br />
                      <asp:TextBox ID="ssn" runat="server" Width="100"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="dob">Date of Birth</label><br />
                      <asp:TextBox ID="dob" runat="server" Width="80"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="weight">Weight</label><br />
                      <asp:TextBox ID="weight" runat="server" Width="50"></asp:TextBox>
                    </div>
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="placeOfDeath">Place of Death</label><br />
                      <asp:TextBox ID="placeOfDeath" runat="server" Width="300"></asp:TextBox>
                      <!--autocomplete begin-->
                      <div class="searchAuto hide" id="podSearch">
                        <ul class="autoSearch" id="podAuto"></ul>
                      </div>
                      <!--autocomplete end-->
                    </div>
                    <div class="left mr_10">
                      <label for="facilityAddr">Address</label><br />
                      <asp:TextBox ID="facilityAddr" class="facility" runat="server" Width="300"></asp:TextBox>
                    </div>
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="facType">Facility Type</label><br />
                     <asp:TextBox ID="facType" class="facility" runat="server" Width="100"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="facCity">City</label><br />
                      <asp:TextBox ID="facCity" class="facility" runat="server" Width="200"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="facState">State</label><br />
                      <asp:TextBox ID="facState" class="facility" runat="server" Width="40"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="facilityCounty">County</label><br />
                      <asp:TextBox ID="facilityCounty" runat="server" class="facility" Width="100"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="facilityZip">Zip</label><br />
                      <asp:TextBox ID="facilityZip" runat="server" class="facility" Width="60"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="facilityPhone">Phone Number</label><br />
                      <asp:TextBox ID="facilityPhone" runat="server" class="facility" Width="90"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="phoneExt">Ext.</label><br />
                      <asp:TextBox ID="phoneExt" runat="server" class="facility" Width="30"></asp:TextBox>
                    </div>
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="partyName">Next of Kin/Responsible Party</label><br />
                      <asp:TextBox ID="partyName" runat="server" Width="200"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="relationship">Relationship</label><br />
                      <asp:DropDownList ID="relationship" runat="server">
                        <asp:ListItem Value="-1" Text="--Select--" />
                        <asp:ListItem Value="1" Text="None" />
                        <asp:ListItem Value="2" Text="Brother" />
                      </asp:DropDownList>
                    </div>
                    <div class="left mr_10">
                      <label for="responsiblePhone">Phone</label><br />
                      <asp:TextBox ID="responsiblePhone" runat="server" Width="90"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="responsiblePhoneExt">Ext.</label><br />
                      <asp:TextBox ID="responsiblePhoneExt" runat="server" Width="30"></asp:TextBox>
                    </div>
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="physicianName">Attending Physician</label><br />
                      <asp:TextBox ID="physicianName" runat="server" Width="250"></asp:TextBox>
                      <!--autocomplete begin-->
                      <div class="searchAuto hide" id="physicianSearch">
                        <ul class="autoSearch" id="physicianAuto">
                          <li>
                            <input type="hidden" class="docId" value="docId" />
                            Doctors Name 
                          </li>
                        </ul>
                      </div>
                      <!--autocomplete end-->
                    </div>
                    <div class="left mr_10">
                      <label for="physicianPhone">Physician Phone</label><br />
                      <asp:TextBox ID="physicianPhone" runat="server" class="physician" Width="90"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="physicianPhoneExt">Ext.</label><br />
                      <asp:TextBox ID="physicianPhoneExt" runat="server" class="physician" Width="30"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="physicianDate">Last Saw Patient</label><br />
                      <asp:TextBox ID="physicianDate" runat="server" Width="100"></asp:TextBox>
                    </div>
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="coronerName">Coroner Name</label><br />
                      <asp:TextBox ID="coronerName" runat="server" Width="200"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="caseNumber">Case Number</label><br />
                      <asp:TextBox ID="caseNumber" runat="server" Width="100"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="counselorName">Counselor Name</label><br />
                      <asp:TextBox ID="counselorName" runat="server" Width="200"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="coronerDate">Date</label><br />
                      <asp:TextBox ID="coronerDate" runat="server" Width="80"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="coronerTime">Time</label><br />
                      <asp:TextBox ID="coronerTime" runat="server" Width="80"></asp:TextBox>
                    </div>
                  </div>
                  <div class="row">
                    <label for="specialInstructionsR">Special Instructions - Reporting Party</label><br />
                    <asp:TextBox ID="specialInstructionsR" runat="server" Width="400" TextMode="MultiLine"></asp:TextBox>
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="specialInstructionsA">Special Instructions - This Account</label><br />
                      <asp:TextBox ID="specialInstructionsA" runat="server" Width="400" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <div class="mTop20">
                        <input type="checkbox" id="deliver" class="left" /><span class="lineH1_6">Deliver Message</span>
                      </div>
                      <div class="mTop5">
                        <input type="checkbox" id="hold" class="left" /><span class="lineH1_6">Hold Message</span>
                      </div>
                      <div class="mTop5">
                        <input type="checkbox" id="remove" class="left" /><span class="lineH1_6">Remove Message</span>
                      </div>
                      <div class="mTop5">
                        <asp:Button ID="submitMessage" runat="server" Text="Submit Message" />
                      </div>
                    </div>
                  </div>
                </div>
                <div class="right" id="fCallNotes">
                  <label for="facilityNotes" class="left pTop5">Facility Notes</label>
                  <asp:Button ID="FacilityNotesUpdate" CssClass="right" runat="server" Text="Update" />
                  <asp:TextBox ID="facilityNotes" runat="server" Width="190" TextMode="MultiLine"></asp:TextBox>
                  <label for="operatorNotes" class="mTop20 left">Operator Notes</label>
                  <asp:Button ID="Button1" runat="server" Text="Update" CssClass="right mTop15" />
                  <asp:TextBox ID="operatorNotes" runat="server" Width="190" TextMode="MultiLine"></asp:TextBox>
                </div>
              </form>
            </div>
            <div class="clearfix">&nbsp;</div>
          </div>
        </div>
        <div class="clearfix">&nbsp;</div>
        <div id="fCall1" class="hide popup">
          <img src="images/exit.png" width="20" class="exit" /><img src="images/fCallAxios.png" />
        </div>
        <div id="axiosFcall" class="hide popup">
          <img src="images/exit.png" width="20" class="exit" /><img src="images/fCall.png" />
        </div>
      </div>
    </div>
  </div>
  <div id="footer">
    <p>Copyright (c) 2014 Axios Communications. All rights reserved.</p>
  </div>
  <div id="containerBg"></div>
</body>
</html>
