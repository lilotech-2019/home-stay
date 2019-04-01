jQuery(function ($) {
	
		$j('#layout-panel .btn-toggle').click(function(e){
			e.preventDefault();
			if ($j('#layout-panel').hasClass('open')){
				$j('#layout-panel').removeClass('open').animate({ 'right':-320 },600);
			} else {
				$j('#layout-panel').addClass('open').animate({ 'right':0 },400);
			}
		});
				
		$j('#layout-panel .demos').css({'height': $j('#layout-panel').height()} )
		
		
		function debouncer( func , timeout ) {
		   var timeoutID , timeout = timeout || 200;
		   return function () {
			  var scope = this , args = arguments;
			  clearTimeout( timeoutID );
			  timeoutID = setTimeout( function () {
				  func.apply( scope , Array.prototype.slice.call( args ) );
			  } , timeout );
		   }
		}
		
		
		$j( window ).resize( debouncer( function ( e ) {
			$j('#layout-panel .demos').css({'height': $j('#layout-panel').height()} )
		} ) );

});
