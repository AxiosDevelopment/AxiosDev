var delay = (function(){
  var timer = 0;
  return function(callback, ms){
	clearTimeout (timer);
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
    $('#searchMessages').on('click', function (e) { 
        e.preventDefault();
        var window = $('#messageContainer');
        openWindow(window);
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
    $('#MsgTo').keyup(function () {
        var searchStr = $(this).val();
        $('#searchAuto p').remove();
        if (searchStr === '') {
            $('#searchAuto').hide();
            return;
        }
        $('#searchAuto').prepend('<p align="center"><img src="images/loading.gif" width="60"/></p>')
        $('#searchAuto').show();
        delay(function () {
            $.ajax({
                url: "FromAutoSearch.aspx?query=" + searchStr,
                cache: false
            })
            .done(function (data) {
                $('#searchAuto p').remove();
                $('#autoSearch').html(data);
            });
        }, 1000);
    })
    $(document).on('click', '#autoSearch li', function () {
        var result = $(this).text();
        $('#MsgTo').val(result);
    });

    /** THIS triggers a save for the various "update buttons" and their associated textareas **/
    $('.update').on('click', function () {
        var updateId = $(this).attr('id'); //gets the ID of the textarea
        var dataFieldValue = $(this).next('textarea').val(); //Gets the value of the data field associated with the update
        $.ajax({
            url: "updateInfo.aspx?info=" + dataFieldValue + "&id=" + updateId,
            cache: false
        }).done(function (data) {
            alert("Updated");
        });
    });

    $(document).on('click', function (e) {
        if (!$(e.target).hasClass('stick')) {
            $('#searchAuto').hide();
        }
    });
});
