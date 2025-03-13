;(function () {

    'use strict';

    var owlCarousel = function(){

        $('#slider1').owlCarousel({
            loop: true,
            slideBy: 3,
            margin: 10,
            dots: false,
            nav: true,
            navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 3
                },
                1000: {
                    items: 4
                }
            }
        });

        $('#slider2').owlCarousel({
            loop: true,
            slideBy: 3,
            margin: 10,
            dots: false,
            nav: true,
            navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 2
                },
                1000: {
                    items: 3
                }
            }
        });

        $('#slider3').owlCarousel({
            loop: true,
            slideBy: 3,
            margin: 10,
            dots: false,
            nav: true,
            navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 2
                },
                1000: {
                    items: 3
                }
            }
        });

        $('#slider4').owlCarousel({
            loop: true,
            slideBy: 3,
            margin: 10,
            dots: false,
            nav: true,
            navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 2
                },
                1000: {
                    items: 3
                }
            }
        });

    };


    var videos = function() {


        $(document).ready(function () {
            $('#play-video').on('click', function (ev) {
                $(".fh5co_hide").fadeOut();
                $("#video")[0].src += "&autoplay=1";
                ev.preventDefault();

            });
        });


        $(document).ready(function () {
            $('#play-video_2').on('click', function (ev) {
                $(".fh5co_hide_2").fadeOut();
                $("#video_2")[0].src += "&autoplay=1";
                ev.preventDefault();

            });
        });

        $(document).ready(function () {
            $('#play-video_3').on('click', function (ev) {
                $(".fh5co_hide_3").fadeOut();
                $("#video_3")[0].src += "&autoplay=1";
                ev.preventDefault();

            });
        });


        $(document).ready(function () {
            $('#play-video_4').on('click', function (ev) {
                $(".fh5co_hide_4").fadeOut();
                $("#video_4")[0].src += "&autoplay=1";
                ev.preventDefault();

            });
        });
    };

    var googleTranslateFormStyling = function() {
        $(window).on('load', function () {
            $('.goog-te-combo').addClass('form-control');
        });
    };


    var contentWayPoint = function() {
        var i = 0;

        $('.animate-box').waypoint( function( direction ) {

            if( direction === 'down' && !$(this.element).hasClass('animated-fast') ) {

                i++;

                $(this.element).addClass('item-animate');
                setTimeout(function(){

                    $('body .animate-box.item-animate').each(function(k){
                        var el = $(this);
                        setTimeout( function () {
                            var effect = el.data('animate-effect');
                            if ( effect === 'fadeIn') {
                                el.addClass('fadeIn animated-fast');
                            } else if ( effect === 'fadeInLeft') {
                                el.addClass('fadeInLeft animated-fast');
                            } else if ( effect === 'fadeInRight') {
                                el.addClass('fadeInRight animated-fast');
                            } else {
                                el.addClass('fadeInUp animated-fast');
                            }

                            el.removeClass('item-animate');
                        },  k * 50, 'easeInOutExpo' );
                    });

                }, 100);

            }

        } , { offset: '85%' } );
        // }, { offset: '90%'} );
    };


	var goToTop = function() {

		$('.js-gotop').on('click', function(event){
			
			event.preventDefault();

			$('html, body').animate({
				scrollTop: $('html').offset().top
			}, 500, 'swing');
			
			return false;
		});

		$(window).scroll(function(){

			var $win = $(window);
			if ($win.scrollTop() > 200) {
				$('.js-top').addClass('active');
			} else {
				$('.js-top').removeClass('active');
			}

		});
	
	};

	
	$(function(){
		owlCarousel();
		videos();
        contentWayPoint();
		goToTop();
		googleTranslateFormStyling();
	});

}());
function googleTranslateElementInit() {
    new google.translate.TranslateElement({pageLanguage: 'en'}, 'google_translate_element');
}

//character counter
const textareaEl = document.getElementById("message");
const totalCounterEl = document.getElementById("total-counter");
const remainingCounterEl = document.getElementById("remaining-counter");

textareaEl.addEventListener("keyup", () => {
  updateCounter();
});

updateCounter()

function updateCounter() {
  totalCounterEl.innerText = textareaEl.value.length;
  remainingCounterEl.innerText =
    textareaEl.getAttribute("maxLength") - textareaEl.value.length;
}

$(document).ready(function(){
            setTimeout(function(){
                $("#msg").fadeOut();
            }, 3000);
});

//active navbar
    //document.addEventListener("DOMContentLoaded", function () {
    //    // Get all navbar items
    //    const navItems = document.querySelectorAll('.nav-item');

    //    // Add click event listener to each navbar item
    //    navItems.forEach(item => {
    //        item.addEventListener('click', function (event) {
    //            // Prevent default behavior if it's a dropdown toggle
    //            if (item.classList.contains('dropdown')) {
    //                event.preventDefault();
    //            }

    //            // Remove active class from all items
    //            navItems.forEach(navItem => navItem.classList.remove('active'));

    //            // Add active class to the clicked item
    //            item.classList.add('active');
    //        });
    //    });

    //    // Highlight the active page on page load
    //    const currentPage = window.location.pathname.split('/').pop() || 'Index'; // Default to 'Index' if no path
    //    const activeItem = document.querySelector(`.nav-item a[href*="${currentPage}"]`);
    //    if (activeItem) {
    //        // Remove active class from all items
    //        navItems.forEach(navItem => navItem.classList.remove('active'));

    //        // Add active class to the current page's nav item
    //        activeItem.parentElement.classList.add('active');
    //    } else {
    //        // If no matching page is found, ensure 'Home' is active by default
    //        document.getElementById('home').classList.add('active');
    //    }
//});

$(document).ready(function(){
  $('.search-txt').on('blur', function(){
    var $this = $(this);
    setTimeout(function(){
      if ($this.val().trim() === "") {
        $this.css({ width: '0', padding: '0' });
      }
    }, 200);
  });
  $('.search-txt').on('focus', function(){
    $(this).css({ width: '500px', padding: '0 6px', border: '1px solid red', borderRadius: '40px' });
  });
});
