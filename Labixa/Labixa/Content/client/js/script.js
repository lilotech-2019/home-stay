(function ($) {

	"use strict";

	/* ================ Revolution Slider. ================ */
	if ($('.tp-banner').length > 0) {
		$('.tp-banner').show().revolution({
			delay: 6000,
			startheight: 600,
			startwidth: 1140,
			hideThumbs: 1000,
			navigationType: 'none',
			touchenabled: 'on',
			onHoverStop: 'on',
			navOffsetHorizontal: 0,
			navOffsetVertical: 0,
			dottedOverlay: 'none',
			fullWidth: 'on'
		});
	}
	if ($('.tp-banner-full').length > 0) {
		$('.tp-banner-full').show().revolution({
			delay: 6000,
			hideThumbs: 1000,
			navigationType: 'none',
			touchenabled: 'on',
			onHoverStop: 'on',
			navOffsetHorizontal: 0,
			navOffsetVertical: 0,
			dottedOverlay: 'none',
			fullScreen: 'on'
		});
	}

	/* ================ testimonials ================ */
	$(document).ready(function () {
		$(".owl-carousel").owlCarousel({

			loop: true,
			margin: 10,
			nav: false,
			responsiveClass: true,
			responsive: {
				0: {
					items: 1,
					nav: true
				},
				700: {
					items: 2,
					nav: false
				},
				1170: {
					items: 2,
					nav: true,
					loop: false
				}
			}


		});

		$("#client-slider").owlCarousel({
			loop: true,
			margin: 10,
			nav: false,
			responsiveClass: true,
			responsive: {
				0: {
					items: 1,
					nav: true
				},
				700: {
					items: 4,
					nav: true
				},
				1170: {
					items: 5,
					nav: true,
					loop: true
				}
			}
		});

		$("#team-slider").owlCarousel({
			loop: true,
			margin: 10,
			nav: false,
			responsiveClass: true,
			responsive: {
				0: {
					items: 1,
					nav: true
				},
				700: {
					items: 2,
					nav: true
				},
				1170: {
					items: 3,
					nav: true,
					loop: true
				}
			}
		});

		$(".blogGrid").owlCarousel({
			loop: true,
			margin: 10,
			nav: false,
			responsiveClass: true,
			responsive: {
				0: {
					items: 1,
					nav: true
				},
				700: {
					items: 2,
					nav: true
				},
				1170: {
					items: 3,
					nav: true,
					loop: true
				}
			}
		});

		$('.sub-blog-menu li').on('mouseenter', function () {
			$(this).addClass('open');
		})
		$('.sub-blog-menu li').on('mouseleave', function () {
			$(this).removeClass('open');
		})

// =		rating
// http://rateyo.fundoocode.ninja/#method-rating
		$("#rateYo").rateYo({
			rating: 3.6,
			starWidth: "30px"
		});

		var $rateYo = $("#rateYo").rateYo();

		$("#rateYo").click(function () {

			/* get rating */
			var rating = $rateYo.rateYo("rating");

			window.alert("Its " + rating + " Yo!");
		});

		$("#setRating").click(function () {

			/* set rating */
			var rating = getRandomRating();
			$rateYo.rateYo("rating", rating);
		});

	});
})(jQuery);