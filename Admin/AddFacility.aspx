<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AddFacility.aspx.vb" Inherits="Axios.AddFacility" %>

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
       alert("The Facility was saved successfully!");
       window.location = "AddFacility.aspx";
     }

     function messagedSavedError() {
       alert("There was an error inserting/updating the Facility. Please contact an administrator for assistance.");
     }

     function messageLoadError() {
       alert("There was an error loading the Facilities page. Please contact an administrator for assistance.");
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
          <li><a href="AddClient.aspx" title="">Manage Clients</a></li>
          <!-- POPUP WITH FORM TO ADD NEW DOCTOR -->
          <li><a href="AddPhysician.aspx" title="">Manage Doctors</a></li>
        </ul>
      </div>
      <div id="page">
        <div id="banner-empty"></div>
        <div id="content">
          <div class="box-style4">
            <div class="title">
              <h2>Add/Edit a Facility</h2>
            </div>
            <div class="entry">
              <div id="facilityTable">
                <h4>Search for existing facility:</h4>
                <input type="text" name="facilitySearch" id="facilitySearch" />
                <!--autocomplete begin-->
                <div class="searchAuto hide" id="podSearch">
                  <ul class="autoSearch" id="podAuto"></ul>
                </div>
                <!--autocomplete end-->
              </div>
              <div class="clearfix"></div>
              <div id="newFacility">
                <h4>Add or edit facility:</h4>
                <br />
                <form id="addNewFacility" action="#" method="post" runat="server">
                  <asp:ValidationSummary ID="AddFacilityValidationSummary" runat="server" CssClass="ErrorMessage" DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false" HeaderText="Please correct the following errors:" />
                  <div class="row">
                    <div class="left mr_10">
                      <label for="placeOfDeath">Place of Death</label><br />
                      <asp:TextBox ID="placeOfDeath" runat="server" Width="300"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqPlaceOfDeath" runat="server" ErrorMessage="Place of Death is required" ControlToValidate="placeOfDeath" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="facilityAddr">Address</label><br />
                      <asp:TextBox ID="facilityAddr" class="facility" runat="server" Width="300"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqFacAddr" runat="server" ErrorMessage="Place of Death Address is required" ControlToValidate="placeOfDeath" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                  </div>
                  <div class="row">
                    <div class="left mr_10">
                      <label for="facType">Facility Type</label><br />
                      <asp:DropDownList ID="FacilityType" runat="server" class="facility">
                        <asp:ListItem Value="-1" Text="--Select--" />
                      </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="ReqFacType" runat="server" ErrorMessage="Facility Type is required" ControlToValidate="FacilityType" CssClass="ErrorMessage" Display="None" Text="*" InitialValue="-1"></asp:RequiredFieldValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="facCity">City</label><br />
                      <asp:TextBox ID="facCity" class="facility" runat="server" Width="180"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqFacCity" runat="server" ErrorMessage="Facility City is required" ControlToValidate="facCity" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="facState">State</label><br />
                      <asp:TextBox ID="facState" class="facility" runat="server" Width="30"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqFacState" runat="server" ErrorMessage="Facility State is required" ControlToValidate="facState" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="ReqExFacState" ControlToValidate="facState" runat="server" ErrorMessage="Please enter a State appreviation (format: CA)" ValidationExpression="^((AL)|(AK)|(AS)|(AZ)|(AR)|(CA)|(CO)|(CT)|(DE)|(DC)|(FM)|(FL)|(GA)|(GU)|(HI)|(ID)|(IL)|(IN)|(IA)|(KS)|(KY)|(LA)|(ME)|(MH)|(MD)|(MA)|(MI)|(MN)|(MS)|(MO)|(MT)|(NE)|(NV)|(NH)|(NJ)|(NM)|(NY)|(NC)|(ND)|(MP)|(OH)|(OK)|(OR)|(PW)|(PA)|(PR)|(RI)|(SC)|(SD)|(TN)|(TX)|(UT)|(VT)|(VI)|(VA)|(WA)|(WV)|(WI)|(WY))$" Display="None"></asp:RegularExpressionValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="facilityCounty">County</label><br />
                      <asp:TextBox ID="facilityCounty" runat="server" class="facility" Width="100"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqFacCounty" runat="server" ErrorMessage="Facility County is required" ControlToValidate="facilityCounty" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="facilityZip">Zip</label><br />
                      <asp:TextBox ID="facilityZip" runat="server" class="facility" Width="50"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqFacZip" runat="server" ErrorMessage="Facility Zip is required" ControlToValidate="facilityZip" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="ReqExFacZip" ControlToValidate="facilityZip" runat="server" ErrorMessage="Please enter a valid Zip (format: #####)" ValidationExpression="^\d{5}$|^\d{5}-\d{4}$" Display="None"></asp:RegularExpressionValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="facilityPhone">Phone Number</label><br />
                      <asp:TextBox ID="facilityPhone" runat="server" class="facility" Width="100"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="ReqFacPhone" runat="server" ErrorMessage="Facility Phone is required" ControlToValidate="facilityPhone" CssClass="ErrorMessage" Display="None" Text="*"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="ReqExFacPhone" ControlToValidate="facilityPhone" runat="server" ErrorMessage="Please enter a valid Phone Number (format: ###-###-####)" ValidationExpression="((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}" Display="None"></asp:RegularExpressionValidator>
                    </div>
                    <div class="left mr_10">
                      <label for="phoneExt">Ext.</label><br />
                      <asp:TextBox ID="phoneExt" runat="server" class="facility" Width="30"></asp:TextBox>
                    </div>
                  </div>
                  <div class="row">
                   <div class="left mr_10">
                      <label for="facilityNotesA">Notes</label><br />
                      <asp:TextBox ID="facilityNotesA" runat="server" class="facility" Width="300" Height="100" TextMode="MultiLine"></asp:TextBox>
                    </div>
                  </div>
                  <input type="hidden" name="facilityId" id="facilityId" value="0" runat="server" />
                  <input type="reset" id="clearform" value="Clear Form" />
                  <asp:Button Text="Submit Facility" runat="server" id="SubmitFacility" />
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
  <script type="text/javascript" src="../Scripts/jquery.js"></script>
  <script>
    var delay = (function () {
      var timer = 0;
      return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
      };
    })();
    $('#facilitySearch').keyup(function (e) {
      var keycode = (e.which);
      var searchStr = $(this).val();
      if (searchStr === '') {
        $('#podSearch').hide();
        $('.facility').prop('disabled', false);
        return;
      }
      if (keycode == "13") {
          e.preventDefault();
          if ($('#podAuto li').hasClass('active')) {
              var result = $('#podAuto li.active').children('.busId').val();
              $.ajax({
                  url: "../SearchInformation.aspx?busId=" + result + "&queryId=podAuto",
                  cache: false
              })
              .done(function (data) {
                  var busObj = JSON.parse(data);
                  $('#placeOfDeath').val(busObj.Name);
                  $('#facilityAddr').val(busObj.Address);
                  $('#FacilityType').val(busObj.TypeID);
                  $('#facilityCounty').val(busObj.County);
                  $('#facState').val(busObj.State);
                  $('#facCity').val(busObj.City);
                  $('#facilityZip').val(busObj.Zip);
                  $('#facilityPhone').val(busObj.Phone);
                  $('#phoneExt').val(busObj.PhoneExt);
                  $('#facilityNotesA').val(busObj.Notes);
                  $('#facilityId').val(result);
                  $('#podSearch').hide();
              }).fail(function (data) {
                  alert("Update has failed. Please try again.\n(Error: " + data.responseText);
              });
              return false;
          }
          else return;
      }
      delay(function () {
          if (keycode != "40" && keycode != "38") {
              $.ajax({
                  url: "../SearchInformation.aspx?query=" + searchStr + "&queryId=BUSSEARCH",
                  cache: false
              })
              .done(function (data) {
                  var $podAuto = $('#podAuto');
                  $podAuto.html("");
                  $.each($.parseJSON(data), function (i, item) {
                      var busNameCity;
                      if (item.City === "") {
                          busNameCity = item.Name;
                      } else {
                          busNameCity = item.Name + " - " + item.City;
                      }
                      var html = '<li><input type="hidden" class="busId" value="' + item.BusinessID + '" />' + busNameCity + '</li>';
                      $podAuto.append(html);
                  });
                  $('#podSearch').show();
                  $('.facility').prop('disabled', false);
              });
          }
      }, 300);
      if ($('#podSearch').is(':visible') && ($('#podAuto li').length > 0)) {
          var text = "";
          var active = $('#podAuto li.active');
          if (keycode == "40") {
              if ((active.next('li').length === 0)) {
                  $('#podAuto li').removeClass('active');
                  $('#podAuto li').first().addClass('active');
                  text = $('#podAuto li').first().text();
              }
              else {
                  active.removeClass('active').next('li').addClass('active');
                  text = active.removeClass('active').next('li').text();
              }
              $('#facilitySearch').val(text);
          }
          if (keycode == "38") {
              console.log('38')
              if ((active.prev('li').length === 0)) {
                  $('#podAuto li').removeClass('active');
                  $('#podAuto li').last().addClass('active');
                  text = $('#podAuto li').last().text();
              }
              else {
                  active.removeClass('active').prev('li').addClass('active');
                  text = active.removeClass('active').prev('li').text();
              }
              $('#facilitySearch').val(text);
          }
      }
    });
    $(document).on('click', '#podAuto li', function () {
      var result = $(this).children('.busId').val();
      var parent = $(this).parent().attr('id');
      $.ajax({
        url: "../SearchInformation.aspx?busId=" + result + "&queryId=" + parent,
        cache: false
      })
      .done(function (data) {
        var busObj = JSON.parse(data);
        $('#placeOfDeath').val(busObj.Name);
        $('#facilityAddr').val(busObj.Address);
        $('#FacilityType').val(busObj.TypeID);
        $('#facilityCounty').val(busObj.County);
        $('#facState').val(busObj.State);
        $('#facCity').val(busObj.City);
        $('#facilityZip').val(busObj.Zip);
        $('#facilityPhone').val(busObj.Phone);
        $('#phoneExt').val(busObj.PhoneExt);
        $('#facilityNotesA').val(busObj.Notes);
        $('#facilityId').val(result);
        $('#podSearch').hide();
      }).fail(function (data) {
        alert("Update has failed. Please try again.\n(Error: " + data.responseText);
      });
    });
    $('#clearform').on('click', function () {
      $('#facilityId').val(0);
      $('#facilitySearch').val('').focus();
    });
    $(document).on('click', function (e) {
      if (!$(e.target).hasClass('stick')) {
        $('.searchAuto').hide();
      }
    });
  </script>
</body>
</html>

