<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Main.aspx.vb" Inherits="Axios.Main" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Axios Main Menu</title>
<link href="default.css" rel="stylesheet" type="text/css" media="all" />
<style type="text/css">
@import "layout.css";
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
				<li class="first active"><a href="#" title="">Main Menu</a></li>
                <li><a href="/Admin/AddClient.aspx" title="">Manage Clients</a></li>
				<!-- POPUP WITH FORM TO ADD NEW FACILITY -->
				<li><a href="/Admin/AddFacility.aspx" title="">Manage Facilities</a></li>
				<!-- POPUP WITH FORM TO ADD NEW DOCTOR -->
				<li><a href="/Admin/AddPhysician.aspx" title="">Manage Doctors</a></li>
			</ul>
		</div>
		<div id="page">
			<div id="banner-empty"></div>
			<div id="content">
				<div class="box-style4">
					<div class="title">
						<h2>Main Menu</h2>
					</div>
					<div class="entry">
						<div class="right" id="messages">
                        <h2 class="mTop10 ml_10">Messages on hold</h2>
						<table width="100%" id="MessageTable" runat="server">
							<tr>
								<th id="firstCall">First Call</th>
								<th id="client">Client</th>
							</tr>
						</table>
						</div>
						<div class="left" id="clients">
                            <h2 class="mTop10 ml_10">Client List</h2>
							<table width="100%" id="ClientTable" runat="server">
							<tr>
								<th id="clientName">Client Name</th>
								<th id="onCall">On Call</th>
                <th id="lastUpdated">Last Updated</th>
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
