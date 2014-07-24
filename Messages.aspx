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
				<li class="first"><a href="main.html" title="">Main Menu</a></li>
				<!-- CLEAR FORM CREATED NEW MESSAGE ID ON SUBMIT -->
				<li><a href="#" title="">New Message</a></li> 
				<!-- POP UP WITH LINKS FOR ALL MESSAGES ASSOCIATED WITH THIS CLIENT -->
				<li><a href="#" title="">All Messages</a></li> 
				<li><a href="#" title="">First Call</a></li> 
				<li><a href="#" title="">Print Message</a></li> 
			</ul>
		</div>
		<div id="page">
			<div id="banner-empty"><h2 align="center">This is a greeting ...</h2></div>
			<div id="content">
				<div class="box-style4">
					<div class="title">
						<h2><span id="clientMessageId">20085</span> | <span id="clientName">Client Name</span></h2>
					</div>
					<div class="entry">
						<div class="left" id="newMessage">
							<form id="addMessage" method="post" action="#">
								<label for="to">To:</label>
								<input type="text" name="to" id="to" />
								<label for="from">From:</label>
								<input type="from" name="from" id="from" />
								<label for="nMsgPhone">Phone:</label>
								<input type="text" name="nMsgPhone" id="nMsgPhone" />
								<label for="nMsgPhoneX">Ext:</label>
								<input type="text" name="nMsgPhoneX" id="nMsgPhoneX" />
								<label for="nMsgAlt">Alt:</label>
								<input type="text" name="nMsgAlt" id="nMsgAlt" />
								<label for="nMsgAltX">Ext:</label>
								<input type="text" name="nMsgAltX" id="nMsgAltX" />
								<label for="quickMessage">Msg:</label>
								<select id="quickMessage">
									<option>What is even going on right now? </option>
								</select><br/>
								<label for="to">Message:</label><br/>
								<textarea id="message"></textarea>
								<label for="notes">Operator_Notes:</label><br/>
								<textarea id="notes"></textarea><br/>
								<input type="checkbox" id="deliver"><span class="left">Deliver Message</span>
								<input type="checkbox" id="hold"><span class="left">Hold Message</span>
								<input type="checkbox" id="remove"><span class="left">Remove Message</span>
								<input type="submit" id="submitMessage" name="submitMessage" value="Submit Message"/>
							</form>
						</div>
						<div class="right" id="clientInfo">
							<div id="onCall" class="left">
								<label for="onCallInfo">Counselor On Call</label>
								<button class="right">Update</button>
								<textarea id="onCallInfo"></textarea><br/><br/>
								<label for="onCallNotes">Additional Notes</label>
								<button class="right">Update</button>
								<textarea id="onCallNotes"></textarea>
							</div>
							<div id="mainInfo" class="right">
								<label for="clientMainInfo">Client Information</label>
								<button class="right">Update</button>
								<textarea id="clientMainInfo"></textarea><br/>
							</div>
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
