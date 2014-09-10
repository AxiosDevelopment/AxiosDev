﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AddClient.aspx.vb" Inherits="Axios.AddClient" %>

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
                  <ul class="autoSearch" id="clientAuto">
                    <li>
                      <input type="hidden" class="clientId" value="clientId" />
                      Client Name 
                    </li>
                  </ul>
                </div>
                <!--autocomplete end-->
              </div>
              <div id="">
                <div class="row">
                    <label for="nClientName">Client Name</label><br />
                    <input type="text" name="nClientName" id="nClientName"/>
                </div>
                <div class="row"></div>
                    <label for="nClientType">Client Type</label><br />
                    <input type="text" name="nClientType" id="nClientType"/>
                </div>
                <div class="row">
                    <label for="nClientAddress">Client Address</label><br />
                    <input type="text" name="nClientAddress" id="nClientAddress"/>
                </div>
                <div class="row">
                    <label for="nClientCity">Client City</label><br />
                    <input type="text" name="nClientCity" id="nClientCity"/>
                </div>
                <div class="row">
                    <label for="nClientState">Client State</label><br />
                    <input type="text" name="nClientState" id="nClientState"/>
                </div>
                
                <div class="row">
                    <label for="nClientZip">Client Zip</label><br />
                    <input type="text" name="nClientZip" id="nClientZip"/>
                </div>
                
                <div class="row">
                    <label for="nClientPhone">Client Phone</label><br />
                    <input type="text" name="nClientPhone" id="nClientPhone"/>
                </div>
                
                <div class="row">
                    <label for="nClientExt">Client Ext</label><br />
                    <input type="text" name="nClientExt" id="nClientExt"/>
                </div>
                
                <div class="row">
                    <label for="nClientPhone2">Client Phone 2</label><br />
                    <input type="text" name="nClientPhone2" id="nClientPhone2"/>
                </div>
                
                <div class="row">
                    <label for="nClientFax">ClientFax</label><br />
                    <input type="text" name="nClientFax" id="nClientFax"/>
                </div>
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
