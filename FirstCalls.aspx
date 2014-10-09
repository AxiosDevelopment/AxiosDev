<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FirstCalls.aspx.vb" Inherits="Axios.FirstCalls" %>

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
          <li><a id="searchFirstCalls" href="#" title="">Search First Calls</a></li>
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
              <form id="addFirstCall" method="post" action="#" runat="server" autocomplete="off">
                <asp:HiddenField ID="CompanyID" runat="server" />
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
                    <asp:HiddenField ID="msgDateTime" runat="server" />
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
                      <asp:TextBox ID="dDate" runat="server" Width="80" autocomplete="off"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqDateOfDeath" runat="server" ErrorMessage="Date of Death is required" ControlToValidate="dDate" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      <asp:CompareValidator ID="DateOfDeathValidator" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="dDate" ErrorMessage="Please enter a valid Date of Death" Display="None"></asp:CompareValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="dTime">Time of Death</label><br />
                      <asp:TextBox ID="dTime" runat="server" Width="80"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqTimeOfDeath" runat="server" ErrorMessage="Time of Death is required" ControlToValidate="dTime" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="RegExValidatorTimeOfDeath" ControlToValidate="dTime" runat="server" ErrorMessage="Please enter a valid time (format: HH:MM:SS PM)" ValidationExpression="^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?( )?(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?))$" Display="None"></asp:RegularExpressionValidator>
                    </div>
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="ssn">SSN</label><br />
                      <asp:TextBox ID="ssn" runat="server" Width="100"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqSSN" runat="server" ErrorMessage="SSN is required" ControlToValidate="ssn" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="RegExValidatorSSN" ControlToValidate="ssn" runat="server" ErrorMessage="Please enter a valid SSN (format: ###-##-####)" ValidationExpression="^(\d{3}-\d{2}-\d{4})|(\d{3}\d{2}\d{4})$" Display="None"></asp:RegularExpressionValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="dob">Date of Birth</label><br />
                      <asp:TextBox ID="dob" runat="server" Width="80"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqDOB" runat="server" ErrorMessage="Date of Birth is required" ControlToValidate="dob" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      <asp:CompareValidator ID="DOBValidator" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="dob" ErrorMessage="Please enter a valid Date of Birth" Display="None"></asp:CompareValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="weight">Weight</label><br />
                      <asp:TextBox ID="weight" runat="server" Width="50"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqWeight" runat="server" ErrorMessage="Weight is required" ControlToValidate="weight" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="RegExWeight" ControlToValidate="weight" runat="server" ErrorMessage="Please enter a valid Weight (format: ###)" ValidationExpression="^\d{1,3}$" Display="None"></asp:RegularExpressionValidator>
                    </div>
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="placeOfDeath">Place of Death</label><br />
                      <asp:TextBox ID="placeOfDeath" runat="server" Width="300"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqPlaceOfDeath" runat="server" ErrorMessage="Place of Death is required" ControlToValidate="placeOfDeath" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      <!--autocomplete begin-->
                      <div class="searchAuto hide" id="podSearch">
                        <ul class="autoSearch" id="podAuto"></ul>
                      </div>
                      <!--autocomplete end-->
                    </div>
                    <div class="left mr_10">
                      <label for="facilityAddr">Address</label><br />
                      <asp:TextBox ID="facilityAddr" class="facility" runat="server" Width="300"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqFacAddr" runat="server" ErrorMessage="Place of Death Address is required" ControlToValidate="placeOfDeath" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="facTypes">Facility Type</label><br />
                      <asp:DropDownList ID="facTypes" runat="server" class="facility" Width="100">
                        <asp:ListItem Value="-1" Text="--Select--" />
                      </asp:DropDownList>
                    </div>
                    <div class="left mr_10">
                      <label for="facCity">City</label><br />
                      <asp:TextBox ID="facCity" class="facility" runat="server" Width="180"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqFacCity" runat="server" ErrorMessage="Facility City is required" ControlToValidate="facCity" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="facState">State</label><br />
                      <asp:TextBox ID="facState" class="facility" runat="server" Width="30"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqFacState" runat="server" ErrorMessage="Facility State is required" ControlToValidate="facState" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="ReqExFacState" ControlToValidate="facState" runat="server" ErrorMessage="Please enter a State appreviation (format: CA)" ValidationExpression="^((AL)|(AK)|(AS)|(AZ)|(AR)|(CA)|(CO)|(CT)|(DE)|(DC)|(FM)|(FL)|(GA)|(GU)|(HI)|(ID)|(IL)|(IN)|(IA)|(KS)|(KY)|(LA)|(ME)|(MH)|(MD)|(MA)|(MI)|(MN)|(MS)|(MO)|(MT)|(NE)|(NV)|(NH)|(NJ)|(NM)|(NY)|(NC)|(ND)|(MP)|(OH)|(OK)|(OR)|(PW)|(PA)|(PR)|(RI)|(SC)|(SD)|(TN)|(TX)|(UT)|(VT)|(VI)|(VA)|(WA)|(WV)|(WI)|(WY))$" Display="None"></asp:RegularExpressionValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="facilityCounty">County</label><br />
                      <asp:TextBox ID="facilityCounty" runat="server" class="facility" Width="100"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqFacCounty" runat="server" ErrorMessage="Facility County is required" ControlToValidate="facilityCounty" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="facilityZip">Zip</label><br />
                      <asp:TextBox ID="facilityZip" runat="server" class="facility" Width="50"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqFacZip" runat="server" ErrorMessage="Facility Zip is required" ControlToValidate="facilityZip" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="ReqExFacZip" ControlToValidate="facilityZip" runat="server" ErrorMessage="Please enter a valid Zip (format: #####)" ValidationExpression="^\d{5}$|^\d{5}-\d{4}$" Display="None"></asp:RegularExpressionValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="facilityPhone">Phone Number</label><br />
                      <asp:TextBox ID="facilityPhone" runat="server" class="facility" Width="100"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqFacPhone" runat="server" ErrorMessage="Facility Phone is required" ControlToValidate="facilityPhone" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="ReqExFacPhone" ControlToValidate="facilityPhone" runat="server" ErrorMessage="Please enter a valid Phone Number (format: ###-###-####)" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" Display="None"></asp:RegularExpressionValidator>
                    </div>
                    <!--<div class="left mr_10">
                      <label for="phoneExt">Ext.</label><br />
                      <asp:TextBox ID="phoneExt" runat="server" class="facility" Width="30"></asp:TextBox>
                    </div>-->
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="partyName">Next of Kin/Responsible Party</label><br />
                      <asp:TextBox ID="partyName" runat="server" Width="200"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqPartyName" runat="server" ErrorMessage="Next of Kin/Responsible Party is required" ControlToValidate="partyName" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="relationship">Relationship</label><br />
                      <asp:DropDownList ID="relationship" runat="server">
                        <asp:ListItem Value="-1" Text="--Select--" />
                      </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="ReqRelationship" runat="server" ErrorMessage="Relationship is required" ControlToValidate="relationship" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="responsiblePhone">Phone</label><br />
                      <asp:TextBox ID="responsiblePhone" runat="server" Width="100"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqResponibleParty" runat="server" ErrorMessage="Responsible Party Phone Number is required" ControlToValidate="responsiblePhone" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="RegExResponsibleParty" ControlToValidate="responsiblePhone" runat="server" ErrorMessage="Please enter a valid Phone Number (format: ###-###-####)" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" Display="None"></asp:RegularExpressionValidator>
                    </div>
                   <!-- <div class="left mr_10">
                      <label for="responsiblePhoneExt">Ext.</label><br />
                      <asp:TextBox ID="responsiblePhoneExt" runat="server" Width="30"></asp:TextBox>
                    </div> -->
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="physicianName">Attending Physician</label><br />
                      <asp:TextBox ID="physicianName" runat="server" Width="250"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqPhysicianName" runat="server" ErrorMessage="Attending Physician is required" ControlToValidate="physicianName" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
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
                      <asp:TextBox ID="physicianPhone" runat="server" class="physician" Width="100"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqPhysicianPhone" runat="server" ErrorMessage="Physician Phone Number is required" ControlToValidate="physicianPhone" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="RegExPhysicianPhone" ControlToValidate="physicianPhone" runat="server" ErrorMessage="Please enter a valid Phone Number (format: ###-###-####)" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" Display="None"></asp:RegularExpressionValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="physicianDate">Last Saw Patient</label><br />
                      <asp:TextBox ID="physicianDate" runat="server" Width="100"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqPhysicianDate" runat="server" ErrorMessage="Last Saw Patient is required" ControlToValidate="physicianDate" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      <asp:CompareValidator ID="LastSawValidator" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="physicianDate" ErrorMessage="Please enter a valid Date for Last Saw Patient" Display="None"></asp:CompareValidator>
                    </div>
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="coronerName">Coroner Name</label><br />
                      <asp:TextBox ID="coronerName" runat="server" Width="200"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqCoronerName" runat="server" ErrorMessage="Coroner Name is required" ControlToValidate="coronerName" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="caseNumber">Case Number</label><br />
                      <asp:TextBox ID="caseNumber" runat="server" Width="100"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqCaseNumber" runat="server" ErrorMessage="Case Number is required" ControlToValidate="caseNumber" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="counselorName">Counselor Name</label><br />
                      <asp:TextBox ID="counselorName" runat="server" Width="200"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqCounselorName" runat="server" ErrorMessage="Counselor Name is required" ControlToValidate="counselorName" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="coronerDate">Date</label><br />
                      <asp:TextBox ID="coronerDate" runat="server" Width="80"></asp:TextBox>
                      <asp:CompareValidator ID="CoronerDateValidator" runat="server" Type="Date" Operator="DataTypeCheck" ControlToValidate="coronerDate" ErrorMessage="Please enter a valid Date for Coroner" Display="None"></asp:CompareValidator>
                      <asp:RequiredFieldValidator ID="ReqCoronerDate" runat="server" ErrorMessage="Date for Coroner is required" ControlToValidate="coronerDate" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="coronerTime">Time</label><br />
                      <asp:TextBox ID="coronerTime" runat="server" Width="80"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqCoronerTime" runat="server" ErrorMessage="Time for Coroner is required" ControlToValidate="coronerTime" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="RegExCoronerTime" ControlToValidate="coronerTime" runat="server" ErrorMessage="Please enter a valid time (format: HH:MM:SS PM)" ValidationExpression="^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?( )?(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?))$" Display="None"></asp:RegularExpressionValidator>
                    </div>
                  </div>
                  <div class="row">
                    <label for="specialInstructionsR">Special Instructions</label><br />
                    <asp:TextBox ID="specialInstructionsR" runat="server" Width="400" TextMode="MultiLine"></asp:TextBox>
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="operatorNotes" class="mTop20 left">Operator Notes</label><br />
                      <asp:TextBox ID="operatorNotes" runat="server" idth="400" TextMode="MultiLine"></asp:TextBox>

                    </div>
                    <div class="left mr_10">
                      <asp:RadioButtonList ID="RBMessageStatus" runat="server" CssClass="RadioListControl mTop20">
                        <asp:ListItem Value="Deliver">Deliver Message</asp:ListItem>
                        <asp:ListItem Value="Hold">Hold Message</asp:ListItem>
                        <asp:ListItem Value="Remove">Remove Message</asp:ListItem>
                      </asp:RadioButtonList>
                      <asp:RequiredFieldValidator runat="server" ID="ValidatorStatusRadio" ControlToValidate="RBMessageStatus" CssClass="ErrorMessage" Display="None" Text="*" ErrorMessage="Deliver, Hold or Remove Message is required"></asp:RequiredFieldValidator>
                      <div class="mTop5">
                        <asp:Button ID="submitMessage" runat="server" Text="Submit Message" />
                      </div>
                    </div>
                  </div>
                </div>
                <div class="right" id="fCallNotes">
                  <label for="facilityNotes" class="left pTop5">Facility Notes</label>
                  <asp:TextBox ID="facilityNotes" runat="server" Width="190" TextMode="MultiLine"></asp:TextBox>

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
    <div id="messageContainer" class="hide popup">
      <img src="images/exit.png" width="20" class="exit" />
      <ul id="allMessages">
        <li><a href="Messages.aspx??MsgId=0&ClientId=2913">
          <span class="to">To: David</span><span class="from">From: Harvey</span><span class="date">Date: 8-2-14</span><span class="time">Time: 1:02pm</span>
        </a></li>
        <li><a href="Messages.aspx??MsgId=0&ClientId=2913">
          <span class="to">To: David</span><span class="from">From: Harvey</span><span class="date">Date: 8-2-14</span><span class="time">Time: 1:02pm</span>
        </a></li>
        <li><a href="Messages.aspx??MsgId=0&ClientId=2913">
          <span class="to">To: David</span><span class="from">From: Harvey</span><span class="date">Date: 8-2-14</span><span class="time">Time: 1:02pm</span>
        </a></li>
      </ul>
    </div>
  </div>
  </div>
  <div id="footer">
    <p>Copyright (c) 2014 Axios Communications. All rights reserved.</p>
  </div>
  <div id="containerBg"></div>
</body>
</html>
