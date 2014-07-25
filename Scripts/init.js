var delay = (function(){
  var timer = 0;
  return function(callback, ms){
	clearTimeout (timer);
	timer = setTimeout(callback, ms);
  };
})();

$(function() {
	$('#search-text').keyup(function () {
		var searchStr = $(this).val();
		if (searchStr === '') {
			$('#searchAuto').hide();
			return;
		}
		$('#searchAuto').show();
		delay(function () {
			console.log('do search');
			/*
			$.ajax({
				url: "pageThatProcessesRequest.aspx?query=" + searchStr,
				cache: false
			})
			.done(function(data) {
				$('#autoSearch).html(data);     
			});
			*/
		}, 1000);
	})
});
