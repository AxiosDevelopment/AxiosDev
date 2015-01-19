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

    function ValidateContactDelete(event) {
      if (confirm('Are you sure you want to delete this contact?')) {
        return true;
      }
      else {
        event.preventDefault();
        return false;
      }
    }

    function NoPrimaryContact() {
      alert("You are required to have at least one Primary Contact. Please add another Primary Contact before deleting the current one.");
    }

    function MustSaveClientToSaveContacts() {
      alert("You will be required to save the client to permanently save the contacts you are adding to this new client.\nClick the Submit Client button to save the Client and Contacts.")
    }

    function resetForms() {
      for (i = 0; i < document.forms.length; i++) {
        document.forms[i].reset();
      }
    }

  </script>
  <script>
    $(function () {
      $('#resetForm').on('click', function () {
        resetForms();
      });

      /*
      //$('#clearform').on('click', function () {
      //  $('#resetContactSession').val('1');
      //  $('#grvContacts').empty();
      //});
  
      $("body").keydown(function (e) {
        if (e.which == 116) {
          //__doPostBack("btnTriggerUpdatePanel", "");
          alert($('#ClientIDText').val());
          alert($('#clientId').val());
        }
      });*/
      
    });
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
          <li><a href="AddFacility.aspx" title="">Manage Facilities</a></li>
          <!-- POPUP WITH FORM TO ADD NEW DOCTOR -->
          <li><a href="AddPhysician.aspx" title="">Manage Doctors</a></li>
        </ul>
      </div>
      <div id="page">
        <div id="banner-empty"></div>
        <div id="content">
          <div class="box-style4">
            <div class="title">
              <h2>Add/Edit a Client</h2>
              <div id="clientTable">
                <h4>Search for existing client:</h4>
                <input type="text" name="searchClient" id="searchClient" class="med" autocomplete="off"/>
                <!--autocomplete begin-->
                <div class="searchAuto hide" id="clientSearch">
                  <ul class="autoSearch" id="clientAuto"></ul>
                </div>
                <!--autocomplete end-->
              </div>
              <form id="addNewClient" runat="server">

                <asp:ScriptManager ID="MsgScriptManager" runat="server"></asp:ScriptManager>
                  <asp:TextBox ID="ClientIDText" runat="server" Style="display: none;" Text="0"></asp:TextBox>
                  <asp:Button ID="btnTriggerUpdatePanel" runat="server" OnClick="btnTriggerUpdatePanel_Click" Style="display: none;" />
                  <asp:UpdatePanel ID="ContactUpdatePanel" runat="server" UpdateMode="Conditional">
                    <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="btnTriggerUpdatePanel" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                      <h2>Individual Contacts</h2>
                      <div class="row">
                        <asp:GridView ID="grvContacts" runat="server" AutoGenerateColumns="False" OnRowEditing="grvContacts_RowEditing" OnRowDeleting="grvContacts_RowDeleting">
                          <Columns>
                            <asp:TemplateField ShowHeader="False">
                              <ItemTemplate>
                                <asp:LinkButton ID="EditLink" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit" CommandArgument='<%#Eval("ContactID")%>'></asp:LinkButton>
                              </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="False">
                              <ItemTemplate>
                                <asp:LinkButton ID="DeleteLink" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" CommandArgument='<%#Eval("ContactID")%>' OnClientClick="return ValidateContactDelete(event);"></asp:LinkButton>
                              </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ContactID" HeaderText="ContactID" SortExpression="ContactID" />
                            <asp:BoundField DataField="CompanyID" HeaderText="CompanyID" SortExpression="CompanyID" />
                            <asp:BoundField DataField="TypeID" HeaderText="TypeID" SortExpression="TypeID" />
                            <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                            <asp:BoundField DataField="AdditionalInformation" HeaderText="AdditionalInformation" SortExpression="AdditionalInformation" />
                            <asp:BoundField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" />
                            <asp:BoundField DataField="UpdatedDateTime" HeaderText="UpdatedDateTime" SortExpression="UpdatedDateTime" />
                            <asp:BoundField DataField="CreatedDateTime" HeaderText="CreatedDateTime" SortExpression="CreatedDateTime" />
                          </Columns>
                        </asp:GridView>
                      </div>
                      <asp:HiddenField ID="hContactID" runat="server" Value="0" />
                      <div class="row">
                          <div class="left mr_10">
                              <label for="ddContactType">Contact Type</label><br />
                              <asp:DropDownList ID="ddContactType" runat="server" CssClass="contacts">
                                <asp:ListItem Value="-1" Text="--Select--" />
                              </asp:DropDownList>
                              <asp:RequiredFieldValidator ValidationGroup="ContactGroup" ID="ReqContactType" runat="server" ErrorMessage="Contact Type is required" ControlToValidate="ddContactType" CssClass="ErrorMessage" Display="None" Text="*" InitialValue="-1"></asp:RequiredFieldValidator>
                          </div>
                          <div class="left mr_10">
                              <label for="nContactName">Contact Name</label><br />
                              <asp:TextBox ID="nContactName" runat="server" CssClass="contacts"></asp:TextBox>
                              <asp:RequiredFieldValidator ValidationGroup="ContactGroup" ID="ReqContactName" runat="server" ErrorMessage="Contact Name is required" ControlToValidate="nContactName" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                          </div>
                          <div class="left mr_10">
                            <label for="nContactJobTitle">Contact Job Title</label><br />
                            <asp:TextBox ID="nContactJobTitle" runat="server" CssClass="contacts"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="ContactGroup" ID="ReqContactJobTitle" runat="server" ErrorMessage="Contact Job Title is required" ControlToValidate="nContactJobTitle" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                          </div>
                      </div>

                      <div class="row">
                          <div class="left mr_10">
                            <label for="nContactPhone">Phone Number (Inc. Ext)</label><br />
                            <asp:TextBox ID="nContactPhone" runat="server" CssClass="contacts"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="ContactGroup" ID="ReqContactPhone" runat="server" ErrorMessage="Contact Phone is required" ControlToValidate="nContactPhone" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                          </div>
                          <div class="left mr_10">
                            <label for="nContactEmail">Contact Email</label><br />
                            <asp:TextBox ID="nContactEmail" runat="server" CssClass="contacts"></asp:TextBox>
                          </div>
                          <div class="left mr_10">
                            <label for="nContactAdditionalInformation">Contact Additional Information</label><br />
                            <asp:TextBox ID="nContactAdditionalInformation" runat="server" CssClass="contacts"></asp:TextBox>
                          </div>
                      </div>
                        
                      <div class="row">
                          <input type="hidden" name="resetContactSession" id="resetContactSession" value="0" runat="server" />
                          <asp:Button ID="ClearContact" runat="server" Text="Clear Contact" CausesValidation="false" />
                          <asp:Button ID="SubmitContact" ValidationGroup="ContactGroup" runat="server" Text="Add/Edit Contact" CausesValidation="true" />
                      </div>
                      <div class="row"></div>
                      <div class="row"></div>
                      <div class="row"></div>
                      
                    </ContentTemplate>
                  </asp:UpdatePanel>
                  

                <asp:ValidationSummary ValidationGroup="ClientGroup" ID="ValidationSummaryAddClient" runat="server" CssClass="ErrorMessage" DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false" HeaderText="Please correct the following errors:" />
                <asp:ValidationSummary ValidationGroup="ContactGroup" ID="ValidationSummaryContact" runat="server" CssClass="ErrorMessage" DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false" HeaderText="Please correct the following errors:" />

                <div id="allClientInfo">
                  <div class="row">
                      <h2>General Info</h2>
                      <div class="left mr_10">
                        <label for="nClientName">Client Name</label><br />
                        <asp:TextBox ID="nClientName" runat="server" class="med"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="ClientGroup" ID="ReqClientName" runat="server" ErrorMessage="Client Name is required" ControlToValidate="nClientName" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                     </div>
                      <div class="left mr_10">
                        <label for="ClientType">Client Type</label><br />
                        <asp:DropDownList ID="ClientType" runat="server">
                          <asp:ListItem Value="-1" Text="--Select--" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ValidationGroup="ClientGroup" ID="ReqClientType" runat="server" ErrorMessage="Client Type is required" ControlToValidate="ClientType" CssClass="ErrorMessage" Display="None" Text="*" InitialValue="-1"></asp:RequiredFieldValidator>
                     </div>
                  </div>

                  <div class="row">
                    <label for="nClientNumber">Client Number</label><br />
                    <asp:TextBox ID="nClientNumber" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="ClientGroup" ID="ReqClientNumber" runat="server" ErrorMessage="Client Number is required" ControlToValidate="nClientNumber" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                  </div>

                  <div class="row">

                      <div class="left mr_10">
                        <label for="nClientAddress">Client Address</label><br />
                        <asp:TextBox ID="nClientAddress" runat="server" class="med"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="ClientGroup" ID="ReqClientAddress" runat="server" ErrorMessage="Client Address is required" ControlToValidate="nClientAddress" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      </div>
                       <div class="left mr_10">
                        <label for="nClientCity">Client City</label><br />
                        <asp:TextBox ID="nClientCity" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="ClientGroup" ID="ReqClientCity" runat="server" ErrorMessage="Client City is required" ControlToValidate="nClientCity" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      </div>
                       <div class="left mr_10">
                        <label for="nClientState">Client State</label><br />
                        <asp:TextBox ID="nClientState" runat="server" CssClass="sm"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="ClientGroup" ID="ReqClientState" runat="server" ErrorMessage="Client State is required" ControlToValidate="nClientState" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      </div>
                       <div class="left mr_10">
                        <label for="nClientZip">Client Zip</label><br />
                        <asp:TextBox ID="nClientZip" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="ClientGroup" ID="ReqClientZip" runat="server" ErrorMessage="Client Zip is required" ControlToValidate="nClientZip" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      </div>
                  </div>
                  <div class="row"></div>
                  <div class="row">
                      <h2>Client Contact Info</h2>
                      <div class="left mr_10">
                        <label for="nClientPhone">Client Phone (incl. Ext)</label><br />
                        <asp:TextBox ID="nClientPhone" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="ClientGroup" ID="ReqClientPhone" runat="server" ErrorMessage="Client Phone is required" ControlToValidate="nClientPhone" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      </div>
                      <div class="left mr_10">
                        <label for="nClientPhone2">Client Phone 2</label><br />
                        <asp:TextBox ID="nClientPhone2" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="ClientGroup" ID="ReqPhone2" runat="server" ErrorMessage="Client Phone Alt is required" ControlToValidate="nClientPhone2" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      </div>
                      <div class="left mr_10">
                        <label for="nClientFax">Client Fax</label><br />
                        <asp:TextBox ID="nClientFax" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="ClientGroup" ID="ReqClientFax" runat="server" ErrorMessage="Client Fax is required" ControlToValidate="nClientFax" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      </div>
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="nBackline1">Backline1 (Inc. Ext)</label><br />
                      <asp:TextBox ID="nBackline1" runat="server" CssClass="ClientGroup"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="nBackline2">Backline2 (Inc. Ext)</label><br />
                      <asp:TextBox ID="nBackline2" runat="server" CssClass="ClientGroup"></asp:TextBox>
                    </div>
                  </div>
                  <div class="row"></div>
                  <div class="row">
                    <h2>Additinal Info</h2>
                    <div class="left mr_10">
                        <label for="nClientGreeting">Client Greeting</label><br />
                        <asp:TextBox ID="nClientGreeting" runat="server" Class="med"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="ClientGroup" ID="ReqClientGreeting" runat="server" ErrorMessage="Client Greeting is required" ControlToValidate="nClientGreeting" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                    
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                        <label for="nClientHours">Client Hours of Operation</label><br />
                        <asp:TextBox ID="nClientHours" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="ClientGroup" ID="ReqClientHours" runat="server" ErrorMessage="Client Hours is required" ControlToValidate="nClientHours" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="left mr_10">
                        <label for="nClientAdditionalInformation">Additional Information</label><br />
                        <asp:TextBox ID="nClientAdditionalInformation" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                        <label for="nClientSpecialInstructions">Other Client Info</label><br />
                        <asp:TextBox ID="nClientSpecialInstructions" runat="server" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="ClientGroup" ID="ReqClientSpecialInstructions" runat="server" ErrorMessage="Client Special Instructions is required" ControlToValidate="nClientSpecialInstructions" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                  </div>

                  <div class="row"></div>
                  <input type="hidden" name="clientId" id="clientId" value="0" runat="server" />
                  <asp:Button ID="resetForm" runat="server" Text="Clear Form" OnClick="resetForm_Click" />
                  <asp:Button Text="Submit Client" ValidationGroup="ClientGroup" runat="server" ID="SubmitClient" CausesValidation="true" />
                  
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
