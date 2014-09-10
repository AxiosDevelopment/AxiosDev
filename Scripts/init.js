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

  $('#printMessage').on('click', function (e) {
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
  });
  $('.exit').click(closeWindow);

  /** THIS IS THE AUTO COMPLETE FOR PLACE OF DEATH **/
  $('#placeOfDeath').keyup(function () {
    var searchStr = $(this).val();
    if (searchStr === '') {
      $('#podSearch').hide();
      $('.facility').prop('disabled', false);
      return;
    }
    delay(function () {
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
    }, 300);
  })

  /** THIS IS THE AUTO COMPLETE FOR PHYSICIAN **/
  $('#physicianName').keyup(function () {
    var searchStr = $(this).val();
    console.log(searchStr)
    if (searchStr === '') {
      $('#physicianSearch').hide();
      return;
    }
    delay(function () {
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
    }, 300);
  });

    /** CLIENT SEARCH */
  $('#searchClient').keyup(function () {
      var searchStr = $(this).val();
      console.log(searchStr)
      if (searchStr === '') {
          $('#clientSearch').hide();
          return;
      }
      delay(function () {
          $.ajax({
              url: "SearchInformation.aspx?query=" + searchStr + "&queryId=CLIENTSEARCH",
              cache: false
          })
           .done(function (data) {
               var $clientAuto = $('#clientAuto');
               $physicianAuto.html("");
               $.each($.parseJSON(data), function (i, item) {
                   var html = '<li><input type="hidden" class="clientId" value="' + item.DoctorID + '" />' + item.Name + '</li>';
                   $clientAuto.append(html);
               });
               $('#clientSearch').show();
           });
      }, 300);
  });

  /** CLIENT ClICK */
  $(document).on('click', '#clientAuto li', function () {
      var result = $(this).children('.busId').val();
      var parent = $(this).parent().attr('id');
      $.ajax({
          url: "SearchInformation.aspx?busId=" + result + "&queryId=" + parent,
          cache: false
      })
      .done(function (data) {
          var busObj = JSON.parse(data);
          //JSON FOR CLIENT EDIT HERE
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
      url: "SearchInformation.aspx?busId=" + result + "&queryId=" + parent,
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
        $('#phoneExt').val(busObj.Ext);
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
  $('.update').on('click', function () {
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
    }).fail(function (data) {
      alert("Update has failed. Please try again.\n(Error: " + data.responseText);
    });
  });
  $('.updateCounselor').on('click', function () {
    var updateId = $(this).attr('id');
    var contactName = $(this).next('input').val();
    var contactNumber = $(this).next().next('input').val();
    $.ajax({
      url: "UpdateInformation.aspx?",
      data: "contactName=" + contactName + "&contactNumber=" + contactNumber + "&clientId=" + clientId + "&updateId=" + updateId,
      dataType: "text",
      cache: false
    }).done(function (data) {
      if (updateId === 'updateMainCounselor') {
        $('#updateMainCounselor').attr('disabled', true);
      } else {
        $('#updateSecondaryCounselor').attr('disabled', true);
      }
      alert("Updated Successfully");
    }).fail(function (data) {
      alert("Update has failed. Please try again.\n(Error: " + data.responseText);
    });
  });
  $(document).on('click', function (e) {
    if (!$(e.target).hasClass('stick')) {
      $('.searchAuto').hide();
    }
  });

  /** THIS IS FOR THE COUNSELOR ON CALL TO CONTROL THE BUTTON ONLY WHEN CHANGES **/
  $('#updateMainCounselor').attr('disabled', true);
  var priOnCall = $('#primaryOnCall');
  var priContact = $('#primaryContact');

  // on keyup of the first textbox for Counselor On Call - enable Update buttons
  priOnCall.keyup(function () {
    $('#updateMainCounselor').attr('disabled', false);
  })
  // on keyup of the second textbox for Counselor On Call - enable Update buttons
  priContact.keyup(function () {
    $('#updateMainCounselor').attr('disabled', false);
  })


  /** THIS IS FOR THE SECONDARY ON CALL TO CONTROL THE BUTTONS **/
  $('#updateSecondaryCounselor').attr('disabled', true);
  $('#clearSecondaryCounselor').attr('disabled', true);
  var secOnCall = $('#secondaryOnCall');
  var secContact = $('#secondaryContact');
  // if no values exist, diable the Clear button
  if (secOnCall.val() != '' || secContact.val() != '') {
    $('#clearSecondaryCounselor').attr('disabled', false);
  }
  // on keyup of the first textbox for Counselor On Call - enable Clear and Update buttons
  secOnCall.keyup(function () {
    $('#clearSecondaryCounselor').attr('disabled', false);
    $('#updateSecondaryCounselor').attr('disabled', false);
  })
  // on keyup of the second textbox for Counselor On Call - enable Clear and Update buttons
  secContact.keyup(function () {
    $('#clearSecondaryCounselor').attr('disabled', false);
    $('#updateSecondaryCounselor').attr('disabled', false);
  })
  // clicking the Clear button
  $('#clearSecondaryCounselor').on('click', function () {
    $('#secondaryOnCall').val("");
    $('#secondaryContact').val("");
    $('#updateSecondaryCounselor').attr('disabled', false);
    $('#clearSecondaryCounselor').attr('disabled', true);
  });

  //Message Page / First Call Page - if msg = 0
  var $msgID = $('#MessageID').val();
  var $fcID = $('#FirstCallID').val();
  if ($msgID == 0 || $fcID == 0) {
    $('#RBMessageStatus input:radio[id^=RBMessageStatus]:last').attr('disabled', true);
  }
});
