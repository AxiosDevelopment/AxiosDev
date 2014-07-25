<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="firstCall.aspx.vb" Inherits="Axios.firstCall" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Axios First Call</title>
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
				<li class="first"><a href="main.html" title="">Main Menu</a></li>
				<li><a href="#" title="">Facility Messages</a></li>
				<!-- POPUP WITH FORM TO FIND FIRST CALL -->
				<li><a href="#" title="">Search First Calls</a></li>
				<!-- POPUP WITH FORM TO ADD NEW DOCTOR -->
				<li><a href="#" title="">Fax First Call</a></li>
				<li><a href="#" title="">Doctors</a></li>
				<li><a href="#" title="">Facilities</a></li>
			</ul>
		</div>
		<div id="page">
			<div id="banner-empty">
				<div class="title">
					<h2 align="center">Mortuary Name Here</h2>
				</div>
			</div>
			<div id="content">
				<div class="box-style4">
					<div class="title">
						<h2>First Call</h2>
					</div>
					<div class="entry">
						<div class="left" id="fCallForm">
							<div class="row">
								<div class="left mr_10">
									<label for="clientId">ClientId</label><br/>
									<input type="text" name="clientId" id=" clientId" size="7" />
								</div>
								<div class="left mr_10">
									<label for="clientName">Client Name</label></br>
									<input type="text" name="clientName" id="clientName" size="65" />
								</div>
								<div class="left mr_10">
									<label for="msgDate">Date</label><br/>
									<input type="text" id="msgDate" name="msgDate" size="10" />
								</div>
								<div class="left mr_10">
									<label for="msgTime">Time</label><br/>
									<input type="text" id="msgTime" name="msgTime" size="10" />
								</div>
							</div>
							<div class="row">
								<div class="left mr_10">
									<label for="reportingName">Reporting Party Name</label><br/>
									<input type="text" name="reportingName" id="reportingName" size="36" />
								</div>
								<div class="left mr_10">
									<label for="deceasedName">Client Name</label></br>
									<input type="text" name="deceasedName" id="deceasedName" size="36" />
								</div>
								<div class="left mr_10">
									<label for="dDate">Date of Death</label><br/>
									<input type="text" id="dDate" name="dDate" size="10" />
								</div>
								<div class="left mr_10">
									<label for="dTime">Time of Death</label><br/>
									<input type="text" id="dTime" name="dTime" size="10" />
								</div>
							</div>
							<div class="row">
								<div class="left mr_10">
									<label for="placeofD">Place of Death</label><br/>
									<select id="placeofD">
										<option value="1">Kaiser Fontana CA</option>
									</select>
								</div>
								<div class="left mr_10">
									<label for="facilityAddr">Address</label></br>
									<input type="text" name="facilityAddr" id="facilityAddr" size="56" />
								</div>
							</div>
							<div class="row">
								<div class="left mr_10">
									<label for="facilityType">Facility Type</label><br/>
									<select id="facilityType">
										<option value="1">Hospital</option>
										<option value="2">Residence</option>
									</select>
								</div>
								<div class="left mr_10">
									<label for="facilityCity">City</label><br/>
									<select id="facilityCity">
										<option value="1">Anaheim</option>
										<option value="2">City of Industry</option>
									</select>
								</div>
								<div class="left mr_10">
									<label for="facilityState">State</label><br/>
									<select id="facilityState">
										<option value="1">AZ</option>
										<option value="2">CA</option>
									</select>
								</div>
								<div class="left mr_10">
									<label for="facilityCounty">County</label></br>
									<input type="text" name="facilityCounty" id="facilityCounty" size="12" />
								</div>
								<div class="left mr_10">
									<label for="facilityZip">Zip</label></br>
									<input type="text" name="facilityZip" id="facilityZip" size="5" />
								</div>
								<div class="left mr_10">
									<label for="facilityPhone">Phone Number</label></br>
									<input type="text" name="facilityPhone" id="facilityPhone" size="17" />
								</div>
								<div class="left mr_10">
									<label for="phoneExt">Ext.</label></br>
									<input type="text" name="phoneExt" id="phoneExt" size="4" />
								</div>
							</div>
							<div class="row">
								<div class="left mr_10">
									<label for="partyName">Next of Kin/Responsible Party</label><br/>
									<input type="text" name="partyName" id="partyName" size="30" /> 
								</div>
								<div class="left mr_10">
									<label for="relationship">Relationship</label></br>
									<select id="relationship">
										<option value="1">None</option>
										<option value="2">Brother</option>
									</select>
								</div>
								<div class="left mr_10">
									<label for="responsiblePhone">Phone</label></br>
									<input type="text" name="responsiblePhone" id="responsiblePhone" size="17" />
								</div>
								<div class="left mr_10">
									<label for="responsiblePhoneExt">Ext.</label></br>
									<input type="text" name="responsiblePhoneExt" id="responsiblePhoneExt" size="4" />
								</div>
							</div>
							<div class="row">
								<div class="left mr_10">
									<label for="physicianName">Attending Physician</label><br/>
									<input type="text" name="physicianName" id="physicianName" size="36" /> <!--AUTOCOMPLETE -->
								</div>
								<div class="left mr_10">
									<label for="physicianPhone">Physician Phone</label></br>
									<input type="text" name="physicianPhone" id="physicianPhone" size="17" />
								</div>
								<div class="left mr_10">
									<label for="physicianPhoneExt">Ext.</label></br>
									<input type="text" name="physicianPhoneExt" id="physicianPhoneExt" size="4" />
								</div>
								<div class="left mr_10">
									<label for="physicianDate">Last Saw Patient</label></br>
									<input type="text" name="physicianDate" id="physicianDate" size="15" />
								</div>
							</div>
							<div class="row">
								<div class="left mr_10">
									<label for="coronerName">Coroner Name</label><br/>
									<input type="text" name="coronerName" id="coronerName" size="27" /> 
								</div>
								<div class="left mr_10">
									<label for="caseNumber">Case Number</label></br>
									<input type="text" name="caseNumber" id="caseNumber" size="16" />
								</div>
								<div class="left mr_10">
									<label for="counselorName">Counselor Name</label></br>
									<input type="text" name="counselorName" id="counselorName" size="28" />
								</div>
								<div class="left mr_10">
									<label for="coronerDate">Date</label></br>
									<input type="text" name="coronerDate" id="coronerDate" size="10" />
								</div>
								<div class="left mr_10">
									<label for="coronerTime">Time</label></br>
									<input type="text" name="coronerTime" id="coronerTime" size="5" />
								</div>
							</div>
							<div class="row">
								<div class="left mr_10">
									<label for="ssn">SSN</label><br/>
									<input type="text" name="ssn" id="ssn" size="12" /> 
								</div>
								<div class="left mr_10">
									<label for="dob">Date of Birth</label></br>
									<input type="text" name="dob" id="dob" size="12" />
								</div>
								<div class="left mr_10">
									<label for="weight">Weight</label></br>
									<input type="text" name="weight" id="weight" size="10" />
								</div>
							</div>
							<div class="row">
								<label for="specialInstructionsR">Special Instructions - Reporting Party</label><br/>
								<textarea id="specialInstructionsR"></textarea>
							</div>
							<div class="row">
								<div class="left mr_10">
									<label for="specialInstructionsA">Special Instructions - This Account</label><br/>
									<textarea id="specialInstructionsA"></textarea>
								</div>
								<div class="left mr_10">
									<div class="mTop20">
										<input type="checkbox" id="deliver" class="left"><span class="lineH1_6">Deliver Message</span>
									</div>
									<div class="mTop5">
										<input type="checkbox" id="hold" class="left"><span class="lineH1_6">Hold Message</span>
									</div>
									<div class="mTop5">
										<input type="checkbox" id="remove" class="left"><span class="lineH1_6">Remove Message</span>
									</div>
									<div class="mTop5">
										<input type="submit" id="submitMessage" name="submitMessage" value="Submit Message"/>
									</div>
								</div>
							</div>
						</div>
						<div class="right" id="fCallNotes">
							<label for="facilityNotes" class="left pTop5">Facility Notes</label>
							<button class="right">Update</button>
							<textarea id="facilityNotes"></textarea>
							<label for="operatorNotes" class="mTop20 left">Operator Notes</label>
							<button class="right mTop15">Update</button>
							<textarea id="operatorNotes"></textarea>
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
