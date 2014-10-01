<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="Axios.Login" %>

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
				
			</ul>
		</div>
		<div id="page">
			<div id="banner-empty"></div>
			<div id="content">
				<div class="box-style4">
					<div class="title">
						<h2>Axios Communications Login</h2>
					</div>
					<div class="entry">
						<div class="login">
                            <form id="loginForm" action="login.aspx" method="post">
							    <div class="error red"></div>
							    <label for="username">Username</label>
							    <input type="text" id="username" name="username" />
							    <label for="password">Password</label>
							    <input type="password" id="password" name="password" />
                                <input type="submit" value="Submit" class="right"/>
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

