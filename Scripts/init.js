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
  var clientId = $('#clientMessageId').text();
  $('#searchMessages').on('click', function (e) {
    e.preventDefault();
    var window = $('#messageContainer');
    $.ajax({
      url: "retrieveAllMessagesForClient.aspx",
      cache: false
    })
    .done(function (data) {
      $('#allMessages').html(data);
      openWindow(window);
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
      return;
    }
    delay(function () {
      $.ajax({
        url: "FromAutoSearch.aspx?query=" + searchStr + "&queryId=BUSSEARCH",
        cache: false
      })
       .done(function (data) {
         $('#podSearch').show();
         $('#podAuto').html(data);
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
        url: "FromAutoSearch.aspx?query=" + searchStr + "&queryId=DOCSEARCH",
        cache: false
      })
       .done(function (data) {
         $('#physicianSearch').show();
         $('#physicianAuto').html(data);
       });
    }, 300);
  })

  /** THIS WILL GET THE BUSINESS ID ONCE CLICKED AND SEND AJAX CALL TO RETRIEVE INFO **/
  /* THIS FUNCTION WILL WORK FOR BOTH AUTO COMPLETES IN THE FIRST CALL PAGE. IT SENDS AN ID AND WILL SEND ANOTHER VARIABLE TO HELP DETERMINE WHICH QUERY TO RUN */
  $(document).on('click', '.autoSearch li', function () {
    var result = $(this).children('.busId').val();
    var parent = $(this).parent().attr('id');
    $.ajax({
      url: "FromAutoSearch.aspx?busId=" + result + "&queryId=" + parent,
      cache: false
    })
    .done(function (data) {
      var busObj = JSON.parse(data); 
      $('#placeOfDeath').val(busObj.BusinessName);

      if (busObj.BusinessName.toUpperCase() != "RESIDENCE") { /*Need to test either name or id to validate if we leave rest of fields read-only*/
        $('#facilityAddr').val(busObj.BusAddress).attr('readonly', 'true');
        $('#facilityCounty').val(busObj.BusCounty).attr('readonly', 'true');
        $('#facilityZip').val(busObj.BusZip).attr('readonly', 'true');
        $('#facilityPhone').val(busObj.BusPhone).attr('readonly', 'true');
        $('#phoneExt').val(busObj.BusExt).attr('readonly', 'true');
      }
      else
      {
        $('#facilityAddr').val(busObj.BusAddress).removeAttr('readonly');
        $('#facilityCounty').val(busObj.BusCounty).removeAttr('readonly');
        $('#facilityZip').val(busObj.BusZip).removeAttr('readonly');
        $('#facilityPhone').val(busObj.BusPhone).removeAttr('readonly');
        $('#phoneExt').val(busObj.BusExt).removeAttr('readonly');
      }
      $('#podSearch').hide();
    }).fail(function (data) {
      alert("Update has failed. Please try again.\n(Error: " + data.responseText);
    });
  });

  /** THIS triggers a save for the various "update buttons" and their associated textareas **/
  $('.update').on('click', function () {
    var updateId = $(this).attr('id'); //gets the ID of the textarea
    var dataFieldValue = $(this).next('textarea').val(); //Gets the value of the data field associated with the update
    $.ajax({
      url: "updateInfo.aspx",
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
      url: "updateInfo.aspx?",
      data: "contactName=" + contactName + "&contactNumber=" + contactNumber + "&clientId=" + clientId + "&updateId=" + updateId,
      dataType: "text",
      cache: false
    }).done(function (data) {
      alert("Updated Successfully");
    }).fail(function (data) {
      alert("Update has failed. Please try again.\n(Error: " + data.responseText);
    });
  });
  $(document).on('click', function (e) {
    if (!$(e.target).hasClass('stick')) {
      $('#searchAuto').hide();
    }
  });
});
