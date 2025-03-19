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
//function googleTranslateElementInit() {
//    new google.translate.TranslateElement({pageLanguage: 'en'}, 'google_translate_element');
//}

// Supported languages (country code, language code, name)
        const languages = [
            { code: 'af', country: 'za', name: 'Afrikaans' },
            { code: 'sq', country: 'al', name: 'Albanian' },
            { code: 'am', country: 'et', name: 'Amharic' },
            { code: 'ar', country: 'sa', name: 'Arabic' },
            { code: 'hy', country: 'am', name: 'Armenian' },
            { code: 'az', country: 'az', name: 'Azerbaijani' },
            { code: 'eu', country: 'es', name: 'Basque' },
            { code: 'be', country: 'by', name: 'Belarusian' },
            { code: 'bn', country: 'bd', name: 'Bengali' },
            { code: 'bs', country: 'ba', name: 'Bosnian' },
            { code: 'bg', country: 'bg', name: 'Bulgarian' },
            { code: 'ca', country: 'ad', name: 'Catalan' },
            { code: 'ceb', country: 'ph', name: 'Cebuano' },
            { code: 'zh-CN', country: 'cn', name: 'Chinese (Simplified)' },
            { code: 'zh-TW', country: 'tw', name: 'Chinese (Traditional)' },
            { code: 'co', country: 'fr', name: 'Corsican' },
            { code: 'hr', country: 'hr', name: 'Croatian' },
            { code: 'cs', country: 'cz', name: 'Czech' },
            { code: 'da', country: 'dk', name: 'Danish' },
            { code: 'nl', country: 'nl', name: 'Dutch' },
            { code: 'en', country: 'us', name: 'English' },
            { code: 'eo', country: 'eo', name: 'Esperanto' },
            { code: 'et', country: 'ee', name: 'Estonian' },
            { code: 'fi', country: 'fi', name: 'Finnish' },
            { code: 'fr', country: 'fr', name: 'French' },
            { code: 'fy', country: 'nl', name: 'Frisian' },
            { code: 'gl', country: 'es', name: 'Galician' },
            { code: 'ka', country: 'ge', name: 'Georgian' },
            { code: 'de', country: 'de', name: 'German' },
            { code: 'el', country: 'gr', name: 'Greek' },
            { code: 'gu', country: 'in', name: 'Gujarati' },
            { code: 'ht', country: 'ht', name: 'Haitian Creole' },
            { code: 'ha', country: 'ng', name: 'Hausa' },
            { code: 'haw', country: 'us', name: 'Hawaiian' },
            { code: 'he', country: 'il', name: 'Hebrew' },
            { code: 'hi', country: 'in', name: 'Hindi' },
            { code: 'hmn', country: 'cn', name: 'Hmong' },
            { code: 'hu', country: 'hu', name: 'Hungarian' },
            { code: 'is', country: 'is', name: 'Icelandic' },
            { code: 'ig', country: 'ng', name: 'Igbo' },
            { code: 'id', country: 'id', name: 'Indonesian' },
            { code: 'ga', country: 'ie', name: 'Irish' },
            { code: 'it', country: 'it', name: 'Italian' },
            { code: 'ja', country: 'jp', name: 'Japanese' },
            { code: 'jv', country: 'id', name: 'Javanese' },
            { code: 'kn', country: 'in', name: 'Kannada' },
            { code: 'kk', country: 'kz', name: 'Kazakh' },
            { code: 'km', country: 'kh', name: 'Khmer' },
            { code: 'rw', country: 'rw', name: 'Kinyarwanda' },
            { code: 'ko', country: 'kr', name: 'Korean' },
            { code: 'ku', country: 'iq', name: 'Kurdish' },
            { code: 'ky', country: 'kg', name: 'Kyrgyz' },
            { code: 'lo', country: 'la', name: 'Lao' },
            { code: 'la', country: 'va', name: 'Latin' },
            { code: 'lv', country: 'lv', name: 'Latvian' },
            { code: 'lt', country: 'lt', name: 'Lithuanian' },
            { code: 'lb', country: 'lu', name: 'Luxembourgish' },
            { code: 'mk', country: 'mk', name: 'Macedonian' },
            { code: 'mg', country: 'mg', name: 'Malagasy' },
            { code: 'ms', country: 'my', name: 'Malay' },
            { code: 'ml', country: 'in', name: 'Malayalam' },
            { code: 'mt', country: 'mt', name: 'Maltese' },
            { code: 'mi', country: 'nz', name: 'M?ori' },
            { code: 'mr', country: 'in', name: 'Marathi' },
            { code: 'mn', country: 'mn', name: 'Mongolian' },
            { code: 'my', country: 'mm', name: 'Myanmar (Burmese)' },
            { code: 'ne', country: 'np', name: 'Nepali' },
            { code: 'no', country: 'no', name: 'Norwegian' },
            { code: 'ny', country: 'mw', name: 'Nyanja' },
            { code: 'or', country: 'in', name: 'Odia' },
            { code: 'ps', country: 'af', name: 'Pashto' },
            { code: 'fa', country: 'ir', name: 'Persian' },
            { code: 'pl', country: 'pl', name: 'Polish' },
            { code: 'pt', country: 'pt', name: 'Portuguese' },
            { code: 'pa', country: 'in', name: 'Punjabi' },
            { code: 'ro', country: 'ro', name: 'Romanian' },
            { code: 'ru', country: 'ru', name: 'Russian' },
            { code: 'sm', country: 'ws', name: 'Samoan' },
            { code: 'gd', country: 'gb', name: 'Scottish Gaelic' },
            { code: 'sr', country: 'rs', name: 'Serbian' },
            { code: 'st', country: 'ls', name: 'Sesotho' },
            { code: 'sn', country: 'zw', name: 'Shona' },
            { code: 'sd', country: 'pk', name: 'Sindhi' },
            { code: 'si', country: 'lk', name: 'Sinhala' },
            { code: 'sk', country: 'sk', name: 'Slovak' },
            { code: 'sl', country: 'si', name: 'Slovenian' },
            { code: 'so', country: 'so', name: 'Somali' },
            { code: 'es', country: 'es', name: 'Spanish' },
            { code: 'su', country: 'id', name: 'Sundanese' },
            { code: 'sw', country: 'tz', name: 'Swahili' },
            { code: 'sv', country: 'se', name: 'Swedish' },
            { code: 'tl', country: 'ph', name: 'Tagalog' },
            { code: 'tg', country: 'tj', name: 'Tajik' },
            { code: 'ta', country: 'in', name: 'Tamil' },
            { code: 'tt', country: 'ru', name: 'Tatar' },
            { code: 'te', country: 'in', name: 'Telugu' },
            { code: 'th', country: 'th', name: 'Thai' },
            { code: 'tr', country: 'tr', name: 'Turkish' },
            { code: 'tk', country: 'tm', name: 'Turkmen' },
            { code: 'uk', country: 'ua', name: 'Ukrainian' },
            { code: 'ur', country: 'pk', name: 'Urdu' },
            { code: 'ug', country: 'cn', name: 'Uyghur' },
            { code: 'uz', country: 'uz', name: 'Uzbek' },
            { code: 'vi', country: 'vn', name: 'Vietnamese' },
            { code: 'cy', country: 'gb', name: 'Welsh' },
            { code: 'xh', country: 'za', name: 'Xhosa' },
            { code: 'yi', country: 'il', name: 'Yiddish' },
            { code: 'yo', country: 'ng', name: 'Yoruba' },
            { code: 'zu', country: 'za', name: 'Zulu' }
        ];

        // Initialize Google Translate
        function googleTranslateElementInit() {
            new google.translate.TranslateElement({
                pageLanguage: 'auto',
                includedLanguages: languages.map(l => l.code).join(','),
                layout: google.translate.TranslateElement.InlineLayout.HORIZONTAL
            }, 'google_translate_element');

            // Populate dropdown
            const dropdown = document.getElementById('flagDropdown');
            languages.forEach(lang => {
                const item = document.createElement('div');
                item.className = 'flag-item';
                item.innerHTML = `
                    <img src="https://flagcdn.com/w40/${lang.country}.png" 
                         alt="${lang.name}"
                         data-lang="${lang.code}">
                    <span>${lang.name}</span>
                `;
                item.addEventListener('click', () => handleLanguageSelect(lang));
                dropdown.appendChild(item);
            });

            // Force Google Translate to initialize
            setTimeout(triggerGoogleTranslate, 500);

            const checkSelect = setInterval(() => {
                const select = document.querySelector('.goog-te-combo');
                if (select && select.value) {
                    // Select element is ready and has a value
                    clearInterval(checkSelect); // Stop checking
                    const currentLangCode = select.value; // e.g., 'fr'
                    const currentLang = languages.find(lang => lang.code === currentLangCode);
                    if (currentLang) {
                        // Update the flag image
                        document.getElementById('current-flag').src = `https://flagcdn.com/w40/${currentLang.country}.png`;
                    }
                }
            }, 100);
        }

        function handleLanguageSelect(lang) {
            // Get Google Translate select element
            const select = document.querySelector('.goog-te-combo');
            if (!select) return;

            // Force new translation even if same language is selected
            select.value = lang.code;
    
            // Create a custom event with re-translation trigger
            const event = new Event('change', {
                bubbles: true,
                cancelable: true
            });
    
            // Add translation promise
            event.translationPromise = new Promise((resolve) => {
                window.googleTranslateElementInit = () => resolve();
            });
    
            select.dispatchEvent(event);

            // Update UI
            document.getElementById('current-flag').src = `https://flagcdn.com/w40/${lang.country}.png`;
            toggleDropdown();

            // Re-translate all content
            if (window.google && google.translate) {
                google.translate.TranslateService.TranslatePage(
                    lang.code,
                    () => console.log('Retranslation complete')
                );
            }
        }

        function triggerGoogleTranslate() {
            // Ensure Google Translate is properly initialized
            const select = document.querySelector('.goog-te-combo');
            if (select && !select.value) {
                select.value = 'en';
                select.dispatchEvent(new Event('change'));
            }
        }

        function toggleDropdown() {
            const dropdown = document.getElementById('flagDropdown');
            dropdown.style.display = dropdown.style.display === 'block' ? 'none' : 'block';
        }

        // Close dropdown when clicking outside
        document.addEventListener('click', (e) => {
            if (!e.target.closest('.flag-selector')) {
                document.getElementById('flagDropdown').style.display = 'none';
            }
        });

        // Handle Google Translate errors
        window.onerror = function(msg, url, line) {
            console.error('Translation error:', msg);
            return true;
        };

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