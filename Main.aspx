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
				<!-- POPUP WITH FORM TO ADD NEW FACILITY -->
				<li><a href="#" title="">Add Facility</a></li>
				<!-- POPUP WITH FORM TO ADD NEW DOCTOR -->
				<li><a href="#" title="">Add Doctor</a></li>
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
						<div class="left" id="messages">
						<table width="100%">
							<tr>
								<th id="msgID"></th>
								<th id="status">Status</th>
								<th id="fCall">FCall</th>
								<th id="client">Client</th>
							</tr>
							<tr>
								<td><a href="#">25590</a></td>
								<td>On Hold</td>
								<td>No</td>
								<td>Valleywide Towing</td>
							</tr>
							<tr>
								<td><a href="#">29950</a></td>
								<td>Not Delivered</td>
								<td>Yes</td>
								<td>Some Mortuary</td>
							</tr>
						</table>
						</div>
						<div class="right" id="clients">
							<table width="100%" id="ClientTable" runat="server">
							<tr>
								<th id="clientID">Clt ID</th>
								<th id="clientName">Client Name</th>
								<th id="onCall">On Call</th>
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
