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
