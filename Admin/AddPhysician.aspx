<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AddPhysician.aspx.vb" Inherits="Axios.AddPhysician" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title>Axios Main Menu</title>
  <link href="../default.css" rel="stylesheet" type="text/css" media="all" />
  <style type="text/css">
    @import "../layout.css";
  </style>
   <script type="text/javascript">
     function messagedSaved() {
       alert("The Physician was saved successfully!");
       window.location = "AddPhysician.aspx";
     }

     function messagedSavedError() {
       alert("There was an error inserting/updating the Physician. Please contact an administrator for assistance.");
     }

     function messageLoadError() {
       alert("There was an error loading the Physicians page. Please contact an administrator for assistance.");
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
          <li><a href="AddClient.aspx" title="">Add Client</a></li>
        </ul>
      </div>
      <div id="page">
        <div id="banner-empty"></div>
        <div id="content">
          <div class="box-style4">
            <div class="title">
              <h2>Add a Physician</h2>
            </div>
            <div class="entry">
              <div class="left leftCol">
                <div id="physicianTable">
                  <table width="100%" id="PhysiciansTable" runat="server">
                    <caption>All Physicians</caption>
                    <tr>
                      <th id="pName">Name</th>
                      <th id="pPhone">Phone</th>
                      <th id="pExt">Ext.</th>
                    </tr>
                  </table>
                </div>
              </div>
              <div class="right rightCol">
                <form id="addNewPhysician" action="#" method="post" runat="server">
                  <div class="row">
                    <div class="left mr_10">
                      <label for="physicianFname">First Name</label><br />
                      <asp:TextBox ID="physicianFname" runat="server" Width="100"></asp:TextBox>
                    </div>
                    <div class="left">
                      <label for="physicianLname">Last Name</label><br />
                      <asp:TextBox ID="physicianLname" runat="server" Width="100"></asp:TextBox>
                    </div>
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="physicianPhone">Physician Phone</label><br />
                      <asp:TextBox ID="physicianPhone" runat="server" Width="100"></asp:TextBox>
                    </div>
                    <div class="left">
                      <label for="physicianExt">Phone Ext.</label><br />
                      <asp:TextBox ID="physicianExt" runat="server" Width="50"></asp:TextBox>
                    </div>
                  </div>
                  <input type="submit" value="Submit Physician" />
                </form>
              </div>
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

