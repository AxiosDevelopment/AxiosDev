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
              <h2>Add/Edit a Physician</h2>
            </div>
            <div class="entry">
              <div id="physicianTable">
                <h4>Search for existing physician:</h4>
                <input type="text" name="searchPhysician" id="searchPhysician" />
                <!--autocomplete begin-->
                <div class="searchAuto hide" id="physicianSearch">
                  <ul class="autoSearch" id="physicianAuto"></ul>
                </div>
                <!--autocomplete end-->
              </div>
              <div class="">
                <h4>Add or edit existing physician:</h4>
                <br />
                <form id="addNewPhysician" action="#" method="post" runat="server">
                  <asp:ValidationSummary ID="AddPhysicianValidationSummary" runat="server" CssClass="ErrorMessage" DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false" HeaderText="Please correct the following errors:" />
                  <div class="row">
                    <div class="left mr_10">
                      <label for="physicianName">Attending Physician</label><br />
                      <asp:TextBox ID="physicianName" runat="server" Width="250"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqPhysicianName" runat="server" ErrorMessage="Attending Physician is required" ControlToValidate="physicianName" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="physicianPhone">Physician Phone</label><br />
                      <asp:TextBox ID="physicianPhone" runat="server" class="physician" Width="100"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqPhysicianPhone" runat="server" ErrorMessage="Physician Phone Number is required" ControlToValidate="physicianPhone" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="RegExPhysicianPhone" ControlToValidate="physicianPhone" runat="server" ErrorMessage="Please enter a valid Phone Number (format: ###-###-####)" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" Display="None"></asp:RegularExpressionValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="physicianPhoneExt">Ext.</label><br />
                      <asp:TextBox ID="physicianPhoneExt" runat="server" class="physician" Width="30"></asp:TextBox>
                    </div>
                    <div class="clearfix"></div>
                    <div class="mTop10">
                      <input type="hidden" name="physicianId" id="physicianId" value="0" runat="server" />
                      <input type="reset" id="clearform" value="Clear Form" />
                      <asp:Button Text="Submit Physician" runat="server" id="SubmitPhysician" />
                    </div>
                  </div>
                </form>
              </div>
            </div>
            <div class="clearfix">&nbsp;</div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
          </div>
        </div>
        <div class="clearfix">&nbsp;</div>
      </div>
    </div>
  </div>
  <div id="footer">
    <p>Copyright (c) 2014 Axios Communications. All rights reserved.</p>
  </div>
  <script type="text/javascript" src="../Scripts/jquery.js"></script>
  <script>
    var delay = (function () {
      var timer = 0;
      return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
      };
    })();
    /** THIS IS THE AUTO COMPLETE FOR PHYSICIAN **/
    $('#searchPhysician').keyup(function () {
      var searchStr = $(this).val();
      console.log(searchStr)
      if (searchStr === '') {
        $('#physicianSearch').hide();
        return;
      }
      delay(function () {
        $.ajax({
          url: "../SearchInformation.aspx?query=" + searchStr + "&queryId=DOCSEARCH",
          cache: false
        })
         .done(function (data) {
           var $physicianAuto = $('#physicianAuto');
           $physicianAuto.html("");
           $.each($.parseJSON(data), function (i, item) {
             var html = '<li><input type="hidden" class="docId" value="' + item.DoctorID + '" />' + item.Name + '</li>';
             $physicianAuto.append(html);
           });
           $('#physicianSearch').show();
         });
      }, 300);
    });
    /** FOR DOCID **/
    $(document).on('click', '#physicianAuto li', function () {
      var result = $(this).children('.docId').val();
      var parent = $(this).parent().attr('id');
      $.ajax({
        url: "../SearchInformation.aspx?busId=" + result + "&queryId=" + parent,
        cache: false
      })
      .done(function (data) {
        var docObj = JSON.parse(data);
        console.log(data);
        $('#physicianName').val(docObj.Name);
        $('#physicianPhone').val(docObj.Phone);
        $('#physicianPhoneExt').val(docObj.PhoneExt);
        $('#physicianSearch').hide();
        $('#physicianId').val(result);
      }).fail(function (data) {
        alert("Update has failed. Please try again.\n(Error: " + data.responseText);
      });
    });
    $('#clearform').on('click', function () {
      $('#physicianId').val(0);
      $('#searchPhysician').val('').focus();
    });
    $(document).on('click', function (e) {
      if (!$(e.target).hasClass('stick')) {
        $('.searchAuto').hide();
      }
    });
  </script>
</body>
</html>

