﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AddFacility.aspx.vb" Inherits="Axios.AddFacility" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title>Axios Main Menu</title>
  <link href="../default.css" rel="stylesheet" type="text/css" media="all" />
  <style type="text/css">
    @import "../layout.css";
  </style>
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
          <li><a href="AddClient.aspx" title="">Add Client</a></li>
          <!-- POPUP WITH FORM TO ADD NEW DOCTOR -->
          <li><a href="AddPhysician.aspx" title="">Add Doctor</a></li>
        </ul>
      </div>
      <div id="page">
        <div id="banner-empty"></div>
        <div id="content">
          <div class="box-style4">
            <div class="title">
              <h2>Add a Facility</h2>
            </div>
            <div class="entry">
              <div id="newFacility">
                <form id="addNewFacility" action="#" method="post" runat="server">
                  <div class="row">
                    <div class="left mr_10">
                      <label for="facilityName">Name</label><br />
                      <asp:TextBox ID="facilityName" runat="server" Width="100"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="facilityType">FacilityType</label><br />
                      <asp:TextBox ID="facilityType" runat="server" Width="40"></asp:TextBox>
                    </div>
                  </div>
                  <div class="row">
                    <div class="left">
                      <label for="facilityAddress">Address</label><br />
                      <asp:TextBox ID="facilityAddress" runat="server" Width="100"></asp:TextBox>
                    </div>
                    <div class="left">
                      <label for="facilityCity">City</label><br />
                      <asp:TextBox ID="facilityCity" runat="server" Width="50"></asp:TextBox>
                    </div>
                    <div class="left">
                      <label for="facilityState">State</label><br />
                      <asp:TextBox ID="facilityState" runat="server" Width="10"></asp:TextBox>
                    </div>
                    <div class="left">
                      <label for="facilityZip">Zip</label><br />
                      <asp:TextBox ID="facilityZip" runat="server" Width="20"></asp:TextBox>
                    </div>
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="facilityPhone">Phone Number</label><br />
                      <asp:TextBox ID="facilityPhone" runat="server" Width="40"></asp:TextBox>
                    </div>
                    <div class="left mr_10">
                      <label for="facilityExt">Phone Ext.</label><br />
                      <asp:TextBox ID="facilityExt" runat="server" Width="40"></asp:TextBox>
                    </div>
                  </div>
                  <input type="submit" value="Submit Facility" />
                </form>
              </div>
              <div id="facilityTable">
                <table width="100%">
                  <caption>All Facilities</caption>
                  <tr>
                    <th id="facility">Facility</th>
                    <th id="fAddress">Address</th>
                    <th id="fType">Type</th>
                    <th id="fCity">City</th>
                    <th id="fState">State</th>
                    <th id="fCounty">County</th>
                    <th id="fZip">Zip</th>
                    <th id="fPhone">Phone</th>
                    <th id="fExt">Ext</th>
                  </tr>
                  <tr>
                    <td><a href="#">Kaiser Permanente</a></td>
                    <td>1234 Some New St.</td>
                    <td>Hospital</td>
                    <td>Fontana</td>
                    <td>CA</td>
                    <td>San Bernardino</td>
                    <td>92336</td>
                    <td>123-123-1234</td>
                    <td>345</td>
                  </tr>
                  <tr>
                    <td><a href="#">Kaiser Permanente</a></td>
                    <td>1234 Some New St.</td>
                    <td>Hospital</td>
                    <td>Fontana</td>
                    <td>CA</td>
                    <td>San Bernardino</td>
                    <td>92336</td>
                    <td>123-123-1234</td>
                    <td>345</td>
                  </tr>
                  <tr>
                    <td><a href="#">Kaiser Permanente</a></td>
                    <td>1234 Some New St.</td>
                    <td>Hospital</td>
                    <td>Fontana</td>
                    <td>CA</td>
                    <td>San Bernardino</td>
                    <td>92336</td>
                    <td>123-123-1234</td>
                    <td>345</td>
                  </tr>
                  <tr>
                    <td><a href="#">Kaiser Permanente</a></td>
                    <td>1234 Some New St.</td>
                    <td>Hospital</td>
                    <td>Fontana</td>
                    <td>CA</td>
                    <td>San Bernardino</td>
                    <td>92336</td>
                    <td>123-123-1234</td>
                    <td>345</td>
                  </tr>
                </table>
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

