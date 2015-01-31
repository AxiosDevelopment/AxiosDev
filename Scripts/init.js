var delay = (function () {
  var timer = 0;
  return function (callback, ms) {
    clearTimeout(timer);
    timer = setTimeout(callback, ms);
  };
})();

function closeWindow() {
  $(this).parent().fadeOut('fast');
  $('#containerBg').fadeOut('fast');
};
function openWindow(popUp) {
  var popId = popUp.attr('id');
  var height = $(document).height();
  var wheight;
  var wheight = $('#wrapper').height() / 3;
  var popHeight = popUp.height() / 2;
  var screenWidth = $('#wrapper').width() / 2;
  var popWidth = popUp.width() / 2;
  var hCenter = screenWidth - popWidth;
  var vCenter = wheight - popHeight;
  console.log(wheight);
  console.log(popHeight);
  popUp.css({ 'left': hCenter, 'top': vCenter });
  $('#containerBg').css('height', height);
  $('#containerBg').fadeIn(200); 	//brings up opaque black background
  popUp.fadeIn(200);
  popUp.css('overflow', 'visible');
}

$(function () {
	/** GET CURRENT TIME AND DATE **/
	

     $('#counselorName').on('focus', function () {
         var start = new Date().toString();
         var hours = new Date().getHours();
         var splitDate = start.split(' ');
         var getMonth = {
             "Jan": '1',"Feb": "2","Mar": "3","Apr": "4", "May": "5","Jun": "6","Jul": "7","Aug": "8","Sep": "9","Oct": "10","Nov": "11","Dec": "12"
         }
         if (hours > 12) var timeOfDay = 'pm'
         else var timeOfDay = 'am';
         $('#coronerTime').val(splitDate[4] + timeOfDay);
         $('#coronerDate').val(getMonth[splitDate[1]] + '/' + splitDate[2] + '/' + splitDate[3]);
     });


  var clientId = $('#CompanyID').val();
  $('#searchMessages, #searchFirstCalls').on('click', function (e) {
    e.preventDefault();
    var window = $('#messageContainer');
    var messageSource = $(this).attr('id'); // use this ID to determine which messages need to be retrieved
    $.ajax({
      url: "SearchInformation.aspx?cid=" + clientId + "&queryId=ALLMESSAGES&messageSource=" + messageSource,
      cache: false
    })
    .done(function (data) {
      $('#allMessages').html(data);
      openWindow(window);
    }).fail(function (data) {
      alert("Error retrieving Messages. Please try again.\n(Error: " + data.responseText);
    });
  });

  /*$('#printMessage').on('click', function (e) {
    e.preventDefault();
    var window = $('#messagePop');
    openWindow(window);
  });
  $('#pFCall').on('click', function (e) {
    e.preventDefault();
    var window = $('#fCall1');
    openWindow(window);
  });
  $('#sci').on('click', function (e) {
    e.preventDefault();
    var window = $('#axiosFcall');
    openWindow(window);
  });*/
  $('.exit').click(closeWindow);

  /** THIS IS THE AUTO COMPLETE FOR PLACE OF DEATH **/
  $('#placeOfDeath').bind("keyup keypress", function (e) {
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
                url: "SearchInformation.aspx?busId=" + result + "&queryId=podAuto",
                cache: false
            })
            .done(function (data) {
                var busObj = JSON.parse(data);
                $('#placeOfDeath').val(busObj.Name);
                if (busObj.Name.toUpperCase() != "RESIDENCE") { /*Need to test either name or id to validate if we leave rest of fields read-only*/
                    $('#facilityAddr').val(busObj.Address);
                    $('#facilityCounty').val(busObj.County);
                    $('#facState').val(busObj.State);
                    $('#facCity').val(busObj.City);
                    $('#facilityZip').val(busObj.Zip);
                    $('#facilityPhone').val(busObj.Phone);
                    $('#phoneExt').val(busObj.PhoneExt);
                    $('#facilityNotes').val(busObj.Notes); //This is for "Facility Notes" 
                    $('.facility').prop('readonly', true);
                }
                else {
                    $('.facility').prop('readonly', false);
                }
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
                url: "SearchInformation.aspx?query=" + searchStr + "&queryId=BUSSEARCH",
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
            console.log('40')
            if ((active.next('li').length === 0)) {
                $('#podAuto li').removeClass('active');
                $('#podAuto li').first().addClass('active');
                text = $('#podAuto li').first().text();
            }
            else {
                active.removeClass('active').next('li').addClass('active');
                text = active.removeClass('active').next('li').text();
            }
            $('#placeOfDeath').val(text);
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
            $('#placeOfDeath').val(text);
        }
    }
  })

  /** THIS IS THE AUTO COMPLETE FOR PHYSICIAN **/
  $('#physicianName').bind("keyup keypress",function (e) {
    var keycode = (e.which);
    var searchStr = $(this).val();
    if (searchStr === '') {
      $('#physicianSearch').hide();
      return;
    }
    if (keycode == "13") {
        e.preventDefault();
        if ($('#physicianAuto li').hasClass('active')) {
            var result = $('#physicianAuto li.active').children('.docId').val();
            $.ajax({
                url: "SearchInformation.aspx?busId=" + result + "&queryId=physicianAuto",
                cache: false
            })
            .done(function (data) {
                var docObj = JSON.parse(data);
                $('#physicianName').val(docObj.Name);
                $('#physicianPhone').val(docObj.Phone);
                $('.physician').prop('readonly', true);
                $('#physicianSearch').hide();
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
                url: "SearchInformation.aspx?query=" + searchStr + "&queryId=DOCSEARCH",
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
        }
    }, 300);
    if ($('#physicianSearch').is(':visible') && ($('#physicianAuto li').length > 0)) {
        var text = "";
        var active = $('#physicianAuto li.active');
        if (keycode == "40") {
            if ((active.next('li').length === 0)) {
                $('#physicianAuto li').removeClass('active')
                $('#physicianAuto li').first().addClass('active');
                text = $('#physicianAuto li').first().text();
            }
            else {
                active.removeClass('active').next('li').addClass('active');
                text = active.removeClass('active').next('li').text();
            }
            $('#physicianName').val(text);
        }
        if (keycode == "38") {
            if ((active.prev('li').length === 0)) {
                $('#physicianAuto li').removeClass('active')
                $('#physicianAuto li').last().addClass('active');
                text = $('#physicianAuto li').last().text();
            }
            else {
                active.removeClass('active').prev('li').addClass('active');
                text = active.removeClass('active').prev('li').text();
            }
            $('#physicianName').val(text);
        }
    }
  });

  /** CLIENT SEARCH */
  $('#searchClient').bind("keyup keypress",function (e) {
      var keycode = (e.which);
    var searchStr = $(this).val();
    console.log(searchStr)
    if (searchStr === '') {
      $('#clientSearch').hide();
      return;
    }
    if (keycode == "13") {
        e.preventDefault();
        if ($('#clientAuto li').hasClass('active')) {
            var result = $('#clientAuto li.active').children('.clientId').val();
            $.ajax({
                url: "../SearchInformation.aspx?clientId=" + result + "&queryId=clientAuto",
                cache: false
            })
            .done(function (data) {
                var clientObj = JSON.parse(data);
                //JSON FOR CLIENT EDIT HERE
                $('#nClientName').val(clientObj.Name);
                $('#nClientNumber').val(clientObj.Number);
                $('#ClientType').val(clientObj.TypeID);
                $('#nClientAddress').val(clientObj.Address);
                $('#nClientCity').val(clientObj.City);
                $('#nClientState').val(clientObj.State);
                $('#nClientZip').val(clientObj.Zip);
                $('#nClientPhone').val(clientObj.MainTelephone);
                $('#nClientPhone2').val(clientObj.MainTelephone2nd);
                $('#nBackline1').val(clientObj.Backline1);
                $('#nBackline2').val(clientObj.Backline2);
                $('#nClientFax').val(clientObj.Fax);
                $('#nClientGreeting').val(clientObj.PhoneAnswer);
                $('#nClientHours').val(clientObj.HoursOfOperation);
                $('#nClientAdditionalInformation').val(clientObj.AdditionalNotes);
                $('#nClientSpecialInstructions').val(clientObj.InstructionSheet);
                $('#ClientIDText').val(result);

                __doPostBack("btnTriggerUpdatePanel", "");

                $('#clientSearch').hide();
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
                url: "../SearchInformation.aspx?query=" + searchStr + "&queryId=CLIENTSEARCH",
                cache: false
            })
             .done(function (data) {
                 var $clientAuto = $('#clientAuto');
                 $clientAuto.html("");
                 $.each($.parseJSON(data), function (i, item) {
                     var html = '<li><input type="hidden" class="clientId" value="' + item.CompanyID + '" />' + item.Name + '</li>';
                     $clientAuto.append(html);
                 });
                 $('#clientSearch').show();
             });
        }
    }, 300);
    if ($('#clientSearch').is(':visible') && ($('#clientAuto li').length > 0)) {
        var text = "";
        var active = $('#clientAuto li.active');
        if (keycode == "40") {
            if ((active.next('li').length === 0)) {
                $('#clientAuto li').removeClass('active');
                $('#clientAuto li').first().addClass('active');
                text = $('#clientAuto li').first().text();
            }
            else {
                active.removeClass('active').next('li').addClass('active');
                text = active.removeClass('active').next('li').text();
            }
            $('#searchClient').val(text);
        }
        if (keycode == "38") {
            if ((active.prev('li').length === 0)) {
                $('#clientAuto li').removeClass('active');
                $('#clientAuto li').last().addClass('active');
                text = $('#clientAuto li').last().text();
            }
            else {
                active.removeClass('active').prev('li').addClass('active');
                text = active.removeClass('active').prev('li').text();
            }
            $('#searchClient').val(text);
        }
    }
  });

  /** CLIENT CLICK */
  $(document).on('click', '#clientAuto li', function () {
    var result = $(this).children('.clientId').val();
    var parent = $(this).parent().attr('id');
    $.ajax({
      url: "../SearchInformation.aspx?clientId=" + result + "&queryId=" + parent,
      cache: false
    })
    .done(function (data) {
      var clientObj = JSON.parse(data);
      //JSON FOR CLIENT EDIT HERE
      console.log(clientObj);
      $('#nClientName').val(clientObj.Name);
      $('#nClientNumber').val(clientObj.Number);
      $('#ClientType').val(clientObj.TypeID);
      $('#nClientAddress').val(clientObj.Address);
      $('#nClientCity').val(clientObj.City);
      $('#nClientState').val(clientObj.State);
      $('#nClientZip').val(clientObj.Zip);
      $('#nClientPhone').val(clientObj.MainTelephone);
      $('#nClientPhone2').val(clientObj.MainTelephone2nd);
      $('#nBackline1').val(clientObj.Backline1);
      $('#nBackline2').val(clientObj.Backline2);
      $('#nClientFax').val(clientObj.Fax);
      $('#nClientGreeting').val(clientObj.PhoneAnswer);
      $('#nClientHours').val(clientObj.HoursOfOperation);
      $('#nClientAdditionalInformation').val(clientObj.AdditionalNotes);
      $('#nClientSpecialInstructions').val(clientObj.InstructionSheet);
      $('#ClientIDText').val(result);

      __doPostBack("btnTriggerUpdatePanel", "");

      $('#clientSearch').hide();
    }).fail(function (data) {
      alert("Update has failed. Please try again.\n(Error: " + data.responseText);
    });
  });

  /** THIS WILL GET THE BUSINESS ID ONCE CLICKED AND SEND AJAX CALL TO RETRIEVE INFO **/
  /* THIS FUNCTION WILL WORK FOR BOTH AUTO COMPLETES IN THE FIRST CALL PAGE. IT SENDS AN ID AND WILL SEND ANOTHER VARIABLE TO HELP DETERMINE WHICH QUERY TO RUN */
  $(document).on('click', '#podAuto li', function () {
    var result = $(this).children('.busId').val();
    var parent = $(this).parent().attr('id');
    $.ajax({
      url: "SearchInformation.aspx?busId=" + result + "&queryId=podAuto",
      cache: false
    })
    .done(function (data) {
      var busObj = JSON.parse(data);
      console.log(busObj);
      $('#placeOfDeath').val(busObj.Name);

      if (busObj.Name.toUpperCase() != "RESIDENCE") { /*Need to test either name or id to validate if we leave rest of fields read-only*/
        $('#facilityAddr').val(busObj.Address);
        $('#facilityCounty').val(busObj.County);
        $('#facState').val(busObj.State);
        $('#facCity').val(busObj.City);
        $('#facilityZip').val(busObj.Zip);
        $('#facilityPhone').val(busObj.Phone);
        $('#phoneExt').val(busObj.PhoneExt);
        //$('.facility').prop('disabled', true);
        $('.facility').prop('readonly', true);
      }
      else {
        //$('.facility').prop('disabled', false);
        $('.facility').prop('readonly', false);
      }
      $('#podSearch').hide();
    }).fail(function (data) {
      alert("Update has failed. Please try again.\n(Error: " + data.responseText);
    });
  });


  /** FOR DOCID **/
  $(document).on('click', '#physicianAuto li', function () {
    var result = $(this).children('.docId').val();
    var parent = $(this).parent().attr('id');
    $.ajax({
      url: "SearchInformation.aspx?busId=" + result + "&queryId=" + parent,
      cache: false
    })
    .done(function (data) {
      var docObj = JSON.parse(data);
      console.log(data);
      $('#physicianName').val(docObj.Name);
      $('#physicianPhone').val(docObj.WorkPhone);
      //$('.physician').prop('disabled', true);
      $('.physician').prop('readonly', true);
      $('#physicianSearch').hide();
    }).fail(function (data) {
      alert("Update has failed. Please try again.\n(Error: " + data.responseText);
    });
  });

  /** THIS triggers a save for the various "update buttons" and their associated textareas **/
  /* NOT NEEDED ANYMORE - UPDATE BUTTONS WERE REMOVED */
 /* $('.update').on('click', function () {
    var updateId = $(this).attr('id'); //gets the ID of the textarea
    var dataFieldValue = $(this).next('textarea').val(); //Gets the value of the data field associated with the update
    $.ajax({
      url: "UpdateInformation.aspx",
      type: "POST",
      data: {
        info: dataFieldValue,
        id: updateId,
        clientId: clientId
      },
      dataType: "text",
      cache: false
    }).done(function (data) {
      alert("Updated Successfully");
    }).fail(function (data, status, error) {
      alert(error);
    });
  });
  */

  /* Update Additional Notes */
  //*************************************************************************************/
  
  $('#updateAdditionalNotes').on('click', function () {
      var notes = $('#onCallNotes').val();
      var clientId = $('#CompanyID').val();
    $.ajax({
      url: "UpdateInformation.aspx?",
      data: "clientId=" + clientId + "&notes=" + notes + "&updateId=updateAdditionalNotes",
      dataType: "text",
      cache: false
    }).done(function (data) {
      alert("Updated Successfully");
    }).fail(function (data, status, error) {
      alert(error);
    });
  });

  /* NOT NEEDED ANYMORE - UPDATE BUTTONS WERE REMOVED */
  /** THIS IS FOR THE COUNSELOR ON CALL TO CONTROL THE BUTTON ONLY WHEN CHANGES **/
  //$('#updateMainCounselor').attr('disabled', true);
  //var priOnCall = $('#primaryOnCall');
  //var priContact = $('#primaryContact');

  // on keyup of the first textbox for Counselor On Call - enable Update buttons
  //priOnCall.keyup(function () {
  //  $('#updateMainCounselor').attr('disabled', false);
  //})
  // on keyup of the second textbox for Counselor On Call - enable Update buttons
  //priContact.keyup(function () {
  //  $('#updateMainCounselor').attr('disabled', false);
  //})


  /** THIS IS FOR THE SECONDARY ON CALL TO CONTROL THE BUTTONS **/
  //$('#updateSecondaryCounselor').attr('disabled', true);
  //$('#clearSecondaryCounselor').attr('disabled', true);
  //var secOnCall = $('#secondaryOnCall');
  //var secContact = $('#secondaryContact');
  //// if no values exist, diable the Clear button
  //if (secOnCall.val() != '' || secContact.val() != '') {
  //  $('#clearSecondaryCounselor').attr('disabled', false);
  //}
  // on keyup of the first textbox for Counselor On Call - enable Clear and Update buttons
  //secOnCall.keyup(function () {
  //  $('#clearSecondaryCounselor').attr('disabled', false);
  //  $('#updateSecondaryCounselor').attr('disabled', false);
  //})
  // on keyup of the second textbox for Counselor On Call - enable Clear and Update buttons
  //secContact.keyup(function () {
  //  $('#clearSecondaryCounselor').attr('disabled', false);
  //  $('#updateSecondaryCounselor').attr('disabled', false);
  //})
  // clicking the Clear button
  //$('#clearSecondaryCounselor').on('click', function () {
  //  $('#secondaryOnCall').val("");
  //  $('#secondaryContact').val("");
  //  $('#updateSecondaryCounselor').attr('disabled', false);
  //  $('#clearSecondaryCounselor').attr('disabled', true);
  //});
  //*************************************************************************************/

  //Message Page / First Call Page - if msg = 0
  var $msgID = $('#MessageID').val();
  var $fcID = $('#FirstCallID').val();
  if ($msgID == 0 || $fcID == 0) {
    $('#RBMessageStatus input:radio[id^=RBMessageStatus]:last').attr('readonly', true);
  }
});
