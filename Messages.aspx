<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Messages.aspx.vb" Inherits="Axios.Messages" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title>Axios Messages</title>
  <link href="default.css" rel="stylesheet" type="text/css" media="all" />
  <style type="text/css">
    @import "layout.css";
  </style>
  <!--jquery.min.js file is referenced in global.asax file and reference in this page thru ScriptManager below -->
  <script type="text/javascript" src="Scripts/jquery.js"></script>
  <script type="text/javascript" src="Scripts/init.js"></script>
  <script type="text/javascript">
    function messagedSaved()
    {
      alert("Your message was saved successfully!");
      window.location = "Main.aspx";
    }

    function messagedSavedError()
    {
      alert("There was an error inserting/updating your message. Please contact an administrator for assistance.");
    }

    function messageLoadError()
    {
      alert("There was an error loading the message page. Please contact an administrator for assistance.");
    }
  </script>
</head>
<body class="single">
  <div id="wrapper">
    <div id="wrapper-bgtop">
      <div id="header">
        <div id="logo">
          <h1><a href="#"></a></h1>
          <p></p>
        </div>
      </div>
      <div id="menu">
        <ul>
          <li class="first"><a href="main.aspx" title="">Main Menu</a></li>
          <!-- CLEAR FORM CREATED NEW MESSAGE ID ON SUBMIT -->
          <li><a href="Messages.aspx?ClientId=<%=cid%>&MsgId=0" title="">New Message</a></li>
          <!-- POP UP WITH LINKS FOR ALL MESSAGES ASSOCIATED WITH THIS CLIENT -->
          <li><a id="searchMessages" href="#" title="">All Messages</a></li>
          <li><a href="FirstCall.aspx?ClientId=<%=cid%>&FirstCallId=0" title="">First Call</a></li>
          <li><a id="printMessage" href="#" title="">Print Message</a></li>
        </ul>
      </div>
      <div id="page">
        <div id="banner-empty">
          <h2 align="center"><%= clientGreeting%></h2>
        </div>
        <div id="content">
          <div class="box-style4">
            <div class="title">
              <h2><span id="clientMessageId" runat="server"></span>| <span id="clientName" runat="server"></span></h2>
            </div>
            <div class="entry">
              <div class="left" id="newMessage">
                <form id="addMessage" method="post" action="#" runat="server">
                  <asp:HiddenField ID="MessageID" runat="server" />
                  <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="ErrorMessage" DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false" HeaderText="Please correct the following errors:" />
                  <asp:ScriptManager ID="MsgScriptManager" runat="server">
                    <Scripts>
                      <asp:ScriptReference Name="jquery" />
                      <asp:ScriptReference Path="~/Scripts/WebForms/WebUIValidation.js" />
                    </Scripts>
                  </asp:ScriptManager>
                  <label for="to">To:</label>
                  <asp:TextBox ID="MsgTo" runat="server"></asp:TextBox>
                  <div id="searchAuto" class="hide" hidden="hidden">
                    <ul id="autoSearch"></ul>
                  </div>
                  <asp:RequiredFieldValidator ID="ReqTo" runat="server" ErrorMessage="To: is required" ControlToValidate="MsgTo" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  <label for="from">From:</label>
                  <asp:TextBox ID="MsgFrom" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="ReqFrom" runat="server" ErrorMessage="From: is required" ControlToValidate="MsgFrom" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  <label for="nMsgPhone">Phone:</label>
                  <asp:TextBox ID="nMsgPhone" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="ReqPhone" runat="server" ErrorMessage="Phone: is required" ControlToValidate="nMsgPhone" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  <label for="nMsgPhoneX">Ext:</label>
                  <asp:TextBox ID="nMsgPhoneX" runat="server"></asp:TextBox>
                  <label for="nMsgAlt">Alt:</label>
                  <asp:TextBox ID="nMsgAlt" runat="server"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="ReqAltPhone" runat="server" ErrorMessage="Alt: is required" ControlToValidate="nMsgAlt" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  <label for="nMsgAltX">Ext:</label>
                  <asp:TextBox ID="nMsgAltX" runat="server"></asp:TextBox>
                  <label for="QwkMessage">Msg:</label>
                  <asp:DropDownList ID="QwkMessage" runat="server">
                    <asp:ListItem Value="-1" Text="--Select--" />
                  </asp:DropDownList>
                  <asp:RequiredFieldValidator ID="ReqQwkMessage" runat="server" ErrorMessage="Msg is required" ControlToValidate="QwkMessage" CssClass="ErrorMessage" Display="None" Text="*" InitialValue="-1"></asp:RequiredFieldValidator>
                  <br />
                  <label for="to">Message:</label><br />
                  <asp:TextBox ID="Message" runat="server" TextMode="MultiLine"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="ReqMessage" runat="server" ErrorMessage="A Message is required" ControlToValidate="Message" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  <label for="notes">Operator_Notes:</label><br />
                  <asp:TextBox ID="Notes" runat="server" TextMode="MultiLine"></asp:TextBox>
                  <br />
                  <input type="checkbox" id="deliver" /><span class="left">Deliver Message</span>
                  <input type="checkbox" id="hold" /><span class="left">Hold Message</span>
                  <input type="checkbox" id="remove" /><span class="left">Remove Message</span>
                  <asp:Button ID="submitMessage" runat="server" Text="Submit Message" />
                </form>
              </div>
              <div class="right" id="clientInfo">
                <div id="onCall" class="left">
                  <label for="onCallInfo">Counselor On Call</label>
                  <button class="right updateCounselor" id="updateMainCounselor">Update</button>
                  <input type="text" id="primaryOnCall" name="onCallInfo" class="onCall" value="<%=PrimaryContactName%>" />
                  <input type="text" id="primaryContact" name="onCallInfo" class="onCall mTop5" value="<%=PrimaryContactInfo%>" /><br />
                  <br />
                  <label for="secondaryOnCallInfo">Secondary</label>
                  <button class="right updateCounselor" id="updateSecondaryCounselor">Update</button>
                  <input type="text" id="secondaryOnCall" name="secondaryOnCall" class="onCall" value="<%=SecondaryContactName%>" />
                  <input type="text" id="secondaryContact" name="secondaryContact" class="onCall mTop5" value="<%=SecondaryContactInfo%>" /><br />
                  <br />
                  <label for="onCallNotes">Additional Notes</label>
                  <button class="right update" id="updateAdditionalNotes">Update</button>
                  <textarea id="onCallNotes" class="onCall"><%=AdditionalNotes%></textarea>
                </div>
                <div id="mainInfo" class="right">
                  <label for="clientMainInfo">Client Information</label>
                  <button class="right update" id="updateClientInfo">Update</button>
                  <textarea id="clientMainInfo"><%=ClientInformation%></textarea><br />
                </div>
              </div>
            </div>
            <div class="clearfix">&nbsp;</div>
          </div>
        </div>
        <div class="clearfix">&nbsp;</div>
        <div id="messagePop" class="hide popup"><img src="images/exit.png" width="20" class="exit" /><img src="images/message.png"/></div>
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
