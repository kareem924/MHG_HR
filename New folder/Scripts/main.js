(function ($) {
    $(function () {

        // easings
        /* linear ease in out in-out snap easeOutCubic easeInOutCubic easeInCirc easeOutCirc easeInOutCirc easeInExpo easeOutExpo easeInOutExpo 
        easeInQuad easeOutQuad easeInOutQuad easeInQuart easeOutQuart easeInOutQuart easeInQuint easeOutQuint easeInOutQuint 
        easeInSine easeOutSine easeInOutSine easeInBack easeOutBack easeInOutBack */

        var $mainWrap = $('.main-wrap');
        var $pagesWrap = $('.pages-wrap');
        var to1 = null;
        var scrollToPage = null;
        var isMobile = false;
        var noTransit = false;
        var isLoggedIn = false;

        if (!$.support.transition) {
            $.fn.transition = $.fn.animate;
            noTransit = true;
        }

        var _dir = 1;
        if (isRTL) _dir = -1;

        // mobile / desktop switch
        isHome();
        $(window).on('load', function () {
            var _w = $(this).width();
            if (_w <= 768) {
                $('body').removeClass('desktop').addClass('mobile');
                initMobile();
                isMobile = true;
            }
            else {
                $('body').removeClass('mobile').addClass('desktop');
                initDesktop();
            }
        });
        $(window).on('resize', function () {
            var _w = $(this).width();
            if (_w <= 768 && $('body').is('.desktop')) location.reload();
            if (_w > 768 && $('body').is('.mobile')) location.reload();
        });

        // initialize Desktop
        function initDesktop() {

            // determine screen ratio
            var _screenRatio = 1;
            function setScreenRatio() {
                var $w = $(this);
                _screenRatio = $w.width() / $w.height();
                // console.log('ratio:' + _screenRatio);
                if (_screenRatio > 1.5) $('body').addClass('wide');
                else $('body').removeClass('wide');
                if (_screenRatio >= 1.9) $('body').addClass('extra-wide');
                else $('body').removeClass('extra-wide');
            }
            setScreenRatio();
            $(window).on('resize', function () {
                setScreenRatio();
            });

            isHome(); //check if top of page

            function applyScrollBar() {
                $('.current-page').not('#media').find('.thecopy').each(function () {
                    var $me = $(this), $p = $me.parent();
                    var $scrollable = $('<div class="scrollable" />');

                    if ($me.find('.scrollable').length) {
                        $me.find('.scrollable').jScrollPane().data().jsp.destroy();
                    }
                    else $me.wrapInner($scrollable);
                    $me.find('.scrollable').height($p.height());
                    $me.find('.scrollable').off('mousewheel').on('mousewheel', function () {
                        if ($(this).is('.jspScrollable')) return false;
                    }).jScrollPane({
                        autoReinitialise: true,
                        verticalGutter: 10,
                        verticalDragMaxHeight: 130,
                        hideFocus: true
                    }).data().jsp.scrollToY(0);
                });
            }

            // mousewheell scrolling
            $('.pages-wrap').off('mousewheel').on('mousewheel', function (e) {


                if ($mainWrap.is('.locked') || e.deltaFactor < 2) { e.preventDefault(); return false };

                $mainWrap.addClass('locked');

                if (e.deltaY < 0) {
                    $('.bullets a.active').next().click();
                    if ($('#media').is('.current-page')) {
                        var _dropEffect = 'cubic-bezier(.52,.22,.35,1.8)';
                        if (noTransit) _dropEffect = 'easeInExpo';
                        $('#media').stop().transition({ y: -100 }, 300, 'easeOutQuad').transition({ y: 0 }, 200, _dropEffect);
                    }
                }
                else {
                    $('.bullets a.active').prev().click();
                    if ($('#home').is('.current-page')) {
                        var _dropEffect = 'cubic-bezier(.52,.22,.35,1.8)';
                        if (noTransit) _dropEffect = 'easeInExpo';
                        $('#home').stop().transition({ y: 75 }, 300, 'easeOutQuad').transition({ y: 0 }, 200, _dropEffect);
                    }
                }

                to1 = setTimeout(function () {
                    if ($('.current-page').is('#media')) $('.scroll-hint').addClass('go-top');
                    if ($('.history-wrap').is(':visible')) {
                        $('.scroll-hint').fadeOut();
                    }
                    $mainWrap.removeClass('locked');
                }, 1200);

            });

            // keyboard scrolling
            $(document).on('keyup', function (e) {
                if ($mainWrap.is('.locked')) return false;
                if (e.which == 40 || e.which == 34) $('.bullets a.active').next().click();
                if (e.which == 38 || e.which == 33) $('.bullets a.active').prev().click();
            });

            // touch scrolling
            if (Modernizr.touch) {
                var hammertime = new Hammer($('.pages-wrap')[0]);
                hammertime.get('swipe').set({ direction: Hammer.DIRECTION_VERTICAL, threshold: 5, velocity: .5 });
                hammertime.on('swipeup', function (ev) {
                    if ($mainWrap.is('.locked')) return false;
                    if ($('.bullets a.active').next().length) {
                        $mainWrap.addClass('locked');
                        $('.bullets a.active').next().click();
                    }
                }).on('swipedown', function (ev) {
                    if ($mainWrap.is('.locked')) return false;
                    if ($('.bullets a.active').prev().length) {
                        $mainWrap.addClass('locked');
                        $('.bullets a.active').prev().click();
                    }
                });
            }


            // pages
            $('.pages-wrap').each(function () {
                var $me = $(this), $page = $me.find('> .page');
                var to2 = null;
                var isIpad = navigator.userAgent.match(/iPad;.*CPU.*OS 7_\d/i) && !window.navigator.standalone;

                function adjustBoxHeight() {
                    var _h = 0;
                    var $win = $(window);
                    var $current = $('.current-page');
                    var _fix = 0;
                    if (isIpad && $win.width() == 1024) _fix = 20; // fix for iPad landscape issue
                    $page.each(function () {
                        var $t = $(this);
                        $t.height($win.height() - _fix);
                        _h += $t.height();
                    });

                    $me.height(_h);

                    // adjust position of current page
                    clearTimeout(to2);
                    to2 = setTimeout(function () {
                        if ($current.length) scrollToPage($current, 200);
                    }, 300);
                }
                adjustBoxHeight();

                $(window).on('resize', function () {
                    adjustBoxHeight();
                });

            });

            function animatePage($page) {

                if ($page.is('#home')) {
                    $page.find('.home-slogan').css({ transformOrigin: 'center bottom', scale: .7, opacity: .2 }).delay(400).transition({ scale: 1, opacity: 1 }, 800);
                }

                if ($page.is('#spotlight')) {
                    $page.find('.spot-item').eq(0).css({ x: '-100%', opacity: .5 }).delay(400).transition({ opacity: 1, x: 0 }, 1000, 'easeOutCubic');
                    $page.find('.spot-item').eq(1).css({ x: '100%', opacity: .5 }).delay(400).transition({ opacity: 1, x: 0 }, 1000, 'easeOutCubic');
                }

                $page.find('.page-title').css({ x: _dir * 500, opacity: 0 }).delay(800).transition({ x: 0, opacity: 1 }, 800, 'easeOutCubic');
                $page.find('.copy-wrap').css({ y: '100%', opacity: 0 }).delay(1200).transition({ y: 0, opacity: 1 }, 800, 'easeOutCubic');
                $page.find('.side-menu, .dummy-box').css({ x: _dir * -400 }).delay(1800).transition({ x: 0 }, 200, 'easeOutCubic');
                $page.find('.side-cta').css({ x: _dir * 400 }).delay(2000).transition({ x: 0 }, 200, 'easeOutCubic');
                $page.find('.side-thumb').css({ x: (_dir * 100) + '%' }).delay(2000).transition({ x: 0 }, 200, 'easeOutCubic');

                if ($page.is('#impact')) {
                    $page.find('.impact-wrap').css({ y: '100%' }).delay(400).transition({ y: 0 }, 800, 'easeOutCubic');
                    // do boxes animation
                    $page.find('.boxes-wrap .cta-box').each(function (i) {
                        var $t = $(this);
                        $t.css({ x: _dir * 100, opacity: 0 }).delay(1200 + i * 150).transition({ opacity: 1, x: 0 }, 100 + 200 * i);
                    });
                }

            }

            scrollToPage = function ($page, _speed) {
                var _y = $page.position().top, _currentTop = $('.current-page').length ? $('.current-page').position().top : 0;
                if (!_speed) _speed = 1000;

                var _delta = (Math.abs(_y - _currentTop) / $page.height()) * 300;

                if (_speed == 200) { }
                else {
                    setTimeout(function () {
                        animatePage($page);
                    }, _delta - 300);
                }


                resetSubpage($page);

                // close history
                if ($('.history-wrap').is(':visible')) closeHistory(true);

                // hide contact form
                $('.contact-form-wrap:visible .close').click();

                // hide T and C / Privacy Policy
                $('.overlay3 .close').click();

                // now scroll to page
                $('.pages-wrap').stop().transition({ x: 0, y: _y * -1 }, _delta + _speed, 'easeOutCubic', function () {
                    isHome();
                    $('.page').removeClass('current-page');
                    $page.addClass('current-page');

                    if (!$page.is('#media')) $('.scroll-hint').removeClass('go-top').fadeIn();
                    $mainWrap.removeClass('locked');

                    // lock scroll if subpage is visible
                    if (parseInt($page.find('.subpage-wrap').css('x')) < 0) {
                        $mainWrap.addClass('locked');
                    }

                    // apply scrollbar
                    applyScrollBar();
                });

                // hide main menu
                if ($('.main-menu').is('.main-menu-expanded')) $('.main-menu .cta')[0].click();
            }
            // scrollToPage($('.page').eq(6));

            function gotoThePage($target) {

                if ($target.length) {
                    scrollToPage($target);

                    // set active bullet
                    $('.bullets a').removeClass('active').filter('a[href=#' + $target.attr('id') + ']').addClass('active');

                    // hide scroll down hint
                    if ($target.is('#media')) $('.scroll-hint').addClass('go-top');
                    else $('.scroll-hint').removeClass('go-top');
                }

            }

            // deep linking
            $.address.change(function (e) {
                var _hash = e.value;
                var _anchor = '#' + _hash.replace('/', '');
                var $target = $(_anchor);
                gotoThePage($target);
                $('.themenu li').removeClass('active').find('a[href=' + _anchor + ']').parent().addClass('active');
            });
            // scroll to hashed page on load
            var _hash = window.location.hash;
            if (_hash != '') {
                var _anchor = _hash.replace('/', '');
                var $target = $(_anchor);
                gotoThePage($target);
                $('.themenu li').removeClass('active').find('a[href=' + _anchor + ']').parent().addClass('active');
            }




            function resetSubpage($page) {
                $page.find('.subpage-wrap, .impact-wrap, .copy-wrap').css({ x: 0, opacity: 1 });
            }

            // bullets & main menu links when click
            $('.bullets a, .header h1 a, .themenu a').on('click', function (e) {
                if ($(this).is('[target=_blank]')) return true;
                $.address.value($(this).attr('href').replace('#', ''));
                e.preventDefault();
                return false;
            });


            // spotlight section
            $('.spot-wrap .spot-item').each(function () {
                var $t = $(this), _img = $t.find('.keyimage > img').attr('src');
                $t.css({ background: 'url(' + _img + ') 50% 0 no-repeat', backgroundSize: 'cover' });
            });


            // adjust height of square boxes
            $('.side-cta, .side-menu, .article-nav').each(function () {
                var $t = $(this);
                function adjustHeight() {
                    $t.height($t.width());
                    $t.parent().outerHeight($t.outerHeight() * 2);
                }
                adjustHeight();
                $(window).on('resize', function () {
                    adjustHeight();
                });

            });
            $('.impact-logo').each(function () {
                var $t = $(this);
                function adjustHeight() {
                    $t.parent().height($t.next().find('.cta-box').width() * 2);
                }
                adjustHeight();
                $(window).on('resize', function () {
                    adjustHeight();
                });
            });

            // scroll hint click
            $('.scroll-hint').on('click', function () {
                var $t = $(this);
                if ($t.is('.go-top')) $('.bullets a:eq(0)').click();
                else $('.bullets a.active').next().click();
            });



            // boxes2
            $('.boxes-wrap2').each(function () {
                var $me = $(this), $box = $me.find('.box');

                function adjustHeight() {
                    $box.height($box.width() * 2);
                }
                adjustHeight();
                $(window).on('resize', function () {
                    adjustHeight();
                });
            });

            function applyScrollBar2($boxContent) {
                $boxContent.find('.box-content-item, .box-content-article').each(function () {
                    var $me = $(this), $p = $me.parent();
                    var $scrollable = $('<div class="scrollable" />');

                    if ($me.find('.scrollable').length) {
                        $me.find('.scrollable').jScrollPane().data().jsp.destroy();
                    }
                    else $me.wrapInner($scrollable);
                    $me.find('.scrollable').height($p.height() - 50);
                    $me.find('.scrollable').off('mousewheel').on('mousewheel', function () {
                        if ($(this).is('.jspScrollable')) return false;
                    }).jScrollPane({
                        autoReinitialise: true,
                        verticalGutter: 10,
                        verticalDragMaxHeight: 130,
                        hideFocus: true
                    }).data().jsp.scrollToY(0);
                });
            }

            // Our Group Section
            $('#group .subpage-wrap').each(function () {
                var $me = $(this), $copyWrap = $me.prev('.copy-wrap'), $cta = $copyWrap.find('.side-cta');
                var $subpage = $me.find('.subpage');

                var $boxes = $me.find('.subpage-boxes');
                var $detail = $me.find('.subpage-detail');

                // create dummy box
                $cta.after('<div class="dummy-box" />');

                // side-cta on click
                $cta.on('click', function () {
                    var $t = $(this), _w = $(window).width();

                    $mainWrap.addClass('locked');

                    $copyWrap.transition({ x: _dir * _w * -1, opacity: 0 }, 800);
                    $me.transition({ x: _dir * _w * -1 }, 800);
                    // do boxes animation
                    $me.find('.subpage-boxes .boxes-wrap2 .box').each(function (i) {
                        var $t = $(this);
                        $t.css({ x: _dir * 100, opacity: 0 }).delay(300 + i * 150).transition({ opacity: 1, x: 0 }, 100 + 200 * i);
                    });
                    $me.find('.subpage-boxes .back').css({ opacity: 0, x: (_dir * 50) + '%' }).delay(1400).transition({ opacity: 1, x: 0 });

                    return false;
                });

                $subpage.each(function () {
                    var $t = $(this), $back = $t.find('> .back');

                    $back.on('click', function () {
                        var $t = $(this);
                        var _w = $(window).width();
                        $me.transition({ x: '+=' + (_dir * _w) + 'px' }, 800);
                        if ($t.parent().is('.subpage-boxes')) {
                            $copyWrap.transition({ x: 0, opacity: 1 }, 800, function () {
                                $mainWrap.removeClass('locked');
                            });
                        }
                        return false;
                    });

                    if ($t.is($boxes)) {
                        // when boxes are clicked
                        $t.find('.box').not('.box-history').on('click', function () {
                            var _w = $(window).width();
                            var $b = $(this), i = $t.find('.box').index($b);
                            $me.transition({ x: '-=' + (_dir * _w) + 'px' }, 800);

                            // do boxes animation
                            $me.find('.subpage-detail .boxes-wrap2 .box').each(function (i) {
                                var $t = $(this);
                                $t.css({ y: '100%' }).delay(300 + i * 200).transition({ y: 0 }, 300, 'easeOutCubic');
                            });
                            $me.find('.subpage-detail .back').css({ opacity: 0, x: (_dir * 50) + '%' }).delay(1000).transition({ opacity: 1, x: 0 });

                            // set active menu & content
                            $detail.find('.side-menu2 li').removeClass('active').eq(i).addClass('active');
                            $detail.find('.box-content-item').hide().removeClass('active').eq(i).addClass('active').show();

                            applyScrollBar2($detail.find('.box-content'));

                            populateThumbnails($detail);

                            return false;
                        });

                        $t.find('.box-history').on('click', function () {
                            loadHistory();
                            return false;
                        });
                    }
                });

                // side menu2
                var $a = $detail.find('.side-menu2 li a');
                $a.on('click', function () {
                    var $t = $(this), i = $a.index($t);
                    if ($t.parent().is('.active')) return false;
                    if ($t.is('a[rel=history]')) return;
                    $detail.find('.box-content > .active').fadeOut();
                    $detail.find('.box-content-item').eq(i).css({ display: 'block', opacity: 0, y: 300 }).delay(200).transition({ opacity: 1, y: 0 }, 500, function () {
                        $detail.find('.box-content-item').removeClass('active');
                        $(this).addClass('active');
                        populateThumbnails($detail);
                    });
                    $a.parent().removeClass('active');
                    $t.parent().addClass('active');
                    return false;
                });
                $detail.find('a[rel=history]').on('click', function () {
                    loadHistory();
                    return false;
                });

            });


            // Other Sections
            $('#retail, #activities, #impact, #media').find('.subpage-wrap').each(function () {
                var $me = $(this), $copyWrap = $me.prev('.copy-wrap, .impact-wrap'), $detail = $me.find('.subpage-detail');
                var $menu = $copyWrap.find('.side-menu, .boxes-wrap');
                var $page = $me.parents('.page');

                // select by default recent articles
                $('#media .side-menu').find('li:eq(0)').addClass('active');

                $menu.find('li a, .cta-box').on('click', function () {
                    var $t = $(this), i = $menu.find('li a, .cta-box').index($t);

                    var _w = $(window).width();


                    // init Article
                    if ($me.parent().parent().is('#media')) {
                        if ($t.is('[href=#recent]')) return false;
                        if ($t.is('[href=#press]')) initArticles();
                    }

                    $mainWrap.addClass('locked');

                    $copyWrap.transition({ x: _dir * _w * -1, opacity: 0 }, 800);
                    $me.transition({ x: _dir * _w * -1 }, 800);

                    // do boxes animation
                    $me.find('.boxes-wrap2 .box').each(function (i) {
                        var $t = $(this);
                        $t.css({ y: '100%' }).delay(600 + i * 200).transition({ y: 0 }, 300, 'easeOutCubic');
                    });
                    $me.find('.back').css({ opacity: 0, y: '50%' }).delay(1400).transition({ opacity: 1, y: 0 });

                    // set active menu & content
                    $detail.find('.side-menu2 li').removeClass('active').eq(i).addClass('active');
                    $detail.find('.box-content-item').hide().removeClass('active').eq(i).addClass('active').show();

                    applyScrollBar2($detail.find('.box-content'));

                    populateThumbnails($detail);

                    // hide thumbnails
                    if ($page.is('#media')) $('#media .boxes-wrap2').find('.box-double-thumb .thumb + .thumb').hide();

                    return false;
                });

                $detail.find('> .back').on('click', function () {
                    if ($('.box-content-article').is(':visible')) {
                        unloadArticle();
                        return false;
                    }
                    $copyWrap.transition({ x: 0, opacity: 1 }, 800, function () {
                        $mainWrap.removeClass('locked');
                        if ($page.is('#media')) {
                            $page.find('.news-list').data('jsp').scrollToY(0);
                            $menu.find('li:eq(0)').addClass('active');
                        }

                    });
                    $me.transition({ x: 0 }, 800);
                    return false;
                });

                // side menu2
                var $a = $detail.find('.side-menu2 li a');
                $a.on('click', function () {
                    var $t = $(this), i = $a.index($t);
                    if ($t.parent().is('.active')) return false;

                    if ($page.is('#media') && !$t.is('[href=#press]')) {
                        $('.box-content-article').prev('.box-content-item').hide();
                        $('.box-content-article, .article-nav').css({ display: 'none' });
                        if (i == 0) {
                            $detail.find('> .back').click();
                        }
                    }
                    $detail.find('.box-content > .active').fadeOut();
                    $detail.find('.box-content-item').eq(i).css({ display: 'block', opacity: 0, y: 300 }).delay(200).transition({ opacity: 1, y: 0 }, 500, function () {
                        $detail.find('.box-content-item').removeClass('active');
                        $(this).addClass('active');
                        populateThumbnails($detail);

                    });
                    $a.parent().removeClass('active');
                    $t.parent().addClass('active');

                    if (i == 1) { initArticles() };

                    // hide thumbnails
                    if ($page.is('#media')) $('#media .boxes-wrap2').find('.box-double-thumb .thumb + .thumb').hide();

                    return false;
                });
            });

        } //end of desktop

        // initialize Mobile
        function initMobile() {

            FastClick.attach(document.body);

            $('.header').insertBefore($('.main-wrap'));

            // adjust homepage height
            var $w = $(window);
            if ($w.height() > $w.width()) {
                $('.page-home').height($w.height() - ($w.width() / 2));
            } else {
                $('.page-home').height($w.height());
            }

            // relocate util
            $('.util').insertAfter($('.main-menu'));

            // central back button
            $('.header').append('<span class="back" />');

            // back buttons for sections
            var $back = $('<span class="back" />');
            $back.on('click', function () {
                $('.header .back').click();
                return false;
            });
            $('.side-menu').before($back.clone(true));
            $('.impact-wrap .boxes-wrap').prepend($back.clone(true));
            $('#group .subpage-boxes > .back').on('click', function () {
                $('.header .back').click();
            });

            $('.header h1 a').on('click', function () {
                $(this).parents('.header').find('.back').click();
                isHome2();
                return false;
            });

            // show hide main logo row
            isHome2();
            var to = null;
            $mainWrap.on('scroll', function () {
                clearTimeout(to);
                to = setTimeout(function () {
                    isHome2();
                }, 300);
            });

            // main drop down menu
            $('.themenu li a').on('click', function () {
                var $t = $(this), _href = $t.attr('href');
                if ($t.is('[target=_blank]')) {
                    $('.themenu').prev('.cta').click();
                    window.open(_href);
                    return;
                }
                loadSection($(_href));
                $('.themenu').prev('.cta').click();
                return false;
            });

            // build section menu
            var $secMenu = $('<div class="sec-menu" />');
            $('.page-home').after($secMenu);

            $('.page').not('.page-home, .page-spotlight').each(function () {
                var $me = $(this), _label = $me.find('.page-title').text();
                var _bg = $me.css('background-image');
                var $secCTA = $('<div class="sec-cta"><div class="thumb" /><div class="label"><span>' + _label + '</span></div>');

                $secMenu.append($secCTA);
                $secCTA.find('.thumb').css({ backgroundImage: _bg });

                $secCTA.on('click', function () {
                    loadSection($me);
                    return false;
                });
            });

            var $homepage = $('.page-home, .sec-menu, .footer-wrap');
            $('.page-home').addClass('current-page');
            function loadSection($page) {
                if ($page.is('.current-page')) return;
                if ($('.page-home').is('.current-page')) $homepage.transition({ x: (_dir * -100) + '%' }, 800, function () {
                    $('.page-home').removeClass('current-page');
                });
                else {
                    $('.current-page').transition({ x: (_dir * -100) + '%' }, 800, function () {
                        $(this).removeClass('current-page').css({ display: 'none' });
                    });
                }
                $page.css({ display: 'block', top: $mainWrap.scrollTop(), x: (_dir * 100) + '%', height: $(window).height() }).transition({ x: 0 }, 800, function () {
                    $('.header .back').show();
                    setTimeout(function () {
                        $mainWrap.removeClass('is-home').addClass('locked');
                        $homepage.hide();
                    }, 200);
                });
                $page.addClass('current-page');
                $('body').removeClass('is-home');
            }
            function unLoadSection() {
                $homepage.show();
                $('.header .back').hide();
                $('.current-page').transition({ x: (_dir * 100) + '%' }, 500, function () {
                    $(this).css({ display: 'none' });
                }).removeClass('current-page');
                $homepage.transition({ x: 0 }, 500);
                $mainWrap.removeClass('locked');
                $('.page-home').addClass('current-page');
                isHome2();
            }

            $('.header').on('click', '.back', function () {
                var $history = $('.history-wrap');
                if ($history.is(':visible')) $history.find('.back').eq(0).click();
                unLoadSection();
            });

            // Our Group boxes menu
            $('.page-our-group .subpage-wrap').each(function () {
                var $me = $(this), $boxesWrap = $me.find('.subpage-boxes'), $detailWrap = $me.find('.subpage-detail');
                var $page = $me.parents('.page');

                $boxesWrap.find('.box').not('.box-history').on('click', function () {
                    var i = $boxesWrap.find('.box').index($(this));
                    loadDetail(i);
                    return false;
                });

                $detailWrap.on('click', '.back', function () {
                    unloadDetail();
                    return false;
                });

                $me.find('.box-history, a[rel=history]').on('click', function () {
                    loadHistory();
                    return false;
                });

                function loadDetail(i) {
                    // adjust height first
                    var $box = $detailWrap.find('.box .thumb').eq(0), $menu2 = $detailWrap.find('.side-menu2');

                    // show content
                    setTimeout(function () {
                        $detailWrap.find('.side-menu2 li').eq(i).find('a').click();
                    }, 800);

                    // adjust boxes height
                    function adjustHeight() {
                        $detailWrap.find('.box').not('.box-content').height($box.width());
                    }

                    // slide the panes
                    $boxesWrap.transition({ x: (_dir * -100) + '%' }, 800, function () {
                        $(this).css({ display: 'none' });
                    });

                    $detailWrap.css({ position: 'absolute', top: 0, left: 0, x: (_dir * 100) + '%', display: 'block' });
                    adjustHeight();
                    $detailWrap.transition({ x: 0 }, 800, function () {
                        $(this).css({ position: 'static' });
                        // $page.scrollTo(0, 300);
                    });
                }

                function unloadDetail() {
                    $detailWrap.css({ position: 'absolute' }).transition({ x: (_dir * 100) + '%' }, 800, function () {
                        $(this).css({ display: 'none' });
                    });
                    $boxesWrap.css({ display: 'block' }).transition({ x: 0 }, 800, function () {

                    });
                }
            });

            function adjustSecBoxesHeight() {
                $secMenu.find('.sec-cta').each(function () {
                    var $t = $(this);
                    $t.height($t.width() / 2);
                });
            }
            adjustSecBoxesHeight();
            $(window).on('resize', function () {
                adjustSecBoxesHeight();
            });


            function initPageHeight() {
                $('.page').not('.page-home, .page-spotlight').each(function () {
                    var $t = $(this), _h = $(window).height();
                    $t.height(_h);
                    $t.css({ backgroundSize: 'auto ' + _h + 'px' });
                });
            }
            initPageHeight();
            $(window).on('resize', function () {
                initPageHeight();
            });

            // boxes-wrap2
            $('.boxes-wrap2').each(function () {
                var $t = $(this), $box = $t.find('.box .thumb').eq(0), $menu2 = $t.find('.side-menu2');
                var $page = $t.parents('.page');

                // adjust boxes height
                function adjustHeight() {
                    $t.find('.box').not('.box-content').height($box.width());
                }
                adjustHeight();
                $(window).on('resize', function () {
                    adjustHeight();
                });

                // side menu click handler
                $menu2.find('li > a').on('click', function () {
                    var $a = $(this), i = $menu2.find('li > a').index($a);

                    if ($a.is('[rel=history]')) return false;

                    if ($a.is('[href=#recent]')) {
                        $t.parent().find('> .back').click();
                        return false;
                    }

                    $menu2.find('li').removeClass('active').eq(i).addClass('active');

                    $t.find('.box-content-item').css({ display: 'none' }).removeClass('active');
                    $t.find('.box-content-item').eq(i).css({ display: 'block' }).addClass('active');

                    populateThumbnails($t.parent());

                    setTimeout(function () {
                        $page.scrollTo(0, 600, { offset: -80 });
                    }, 200);
                    return false;
                });
            });

            function initDetailPage() {
                $('.subpage-detail').each(function () {
                    var $t = $(this);
                    $t.find('.box-content').insertBefore($t.find('.box-menu'));
                    $t.find('.box-menu').insertAfter($t.find('.box-double-thumb'));

                    $t.find('.box-content-item').eq(0).show();
                });
            }
            initDetailPage();

            // impact page
            $('.impact-wrap').each(function () {
                var $me = $(this), $box = $me.find('.cta-box .thumb').eq(0), $ctaBox = $me.find('.cta-box'), $menu2 = $me.next('.subpage-wrap').find('.side-menu2');
                var $page = $me.parent(), $detail = $page.find('.subpage-detail'), $back = $me.parent().find('.back');

                // adjust boxes height
                function adjustHeight() {
                    $ctaBox.height($box.width());
                }
                adjustHeight();
                $(window).on('resize', function () {
                    adjustHeight();
                });

                // boxes click handler
                $ctaBox.on('click', function () {
                    var $b = $(this), i = $ctaBox.index($b);
                    var _delta = $me.find('.impact-logo').height();

                    // show content
                    setTimeout(function () {
                        $menu2.find('li').eq(i).find('a').click();
                    }, 600);

                    // adjust boxes height
                    $detail.find('.box').not('.box-content').each(function () {
                        $(this).height($page.width() / 2);
                    });

                    // transit
                    $me.css({ position: 'absolute', top: 0 }).transition({ x: (_dir * -100) + '%' }, 800);
                    $detail.css({ display: 'block', position: 'absolute', x: (_dir * 100) + '%', left: 0, top: _delta / 2 }).transition({ x: 0 }, 800);

                    return false;
                });

                // side menu click handler
                var $t = $me.find('.boxes-wrap2');
                $menu2.find('li > a').on('click', function () {
                    var $a = $(this), i = $menu2.find('li > a').index($a);

                    $t.find('.box-content-item').css({ display: 'none' }).removeClass('active');
                    $t.find('.box-content-item').eq(i).css({ display: 'block' }).addClass('active');

                    populateThumbnails($t.parent());

                    setTimeout(function () {
                        $page.scrollTo(0, 600, { offset: -80 });
                    }, 200);
                    return false;
                });

                // back
                $back.on('click', function () {
                    var _delta = $me.find('.impact-logo').height();
                    $detail.transition({ x: (_dir * 100) + '%' }, 800, function () {
                        $(this).css({ display: 'none' });
                    });
                    $me.css({ y: 0 }).transition({ x: 0 }, 800, function () {
                        $(this).css({ position: 'static', y: 0 });
                    });
                    return false;
                });
            });

            // media page
            $('.page-media').each(function () {
                var $me = $(this);

                // adjust boxes height
                function adjustHeight() {
                    // if($(window).width() <= 480) return;
                    $me.find('.side-menu, .thecopy > .news-list-wrap > .list-filter').each(function () {
                        var $t = $(this);
                        $t.height($t.width());
                    });
                    $me.find('.article-nav').each(function () {
                        var $t = $(this);
                        $t.height($t.width());
                    });
                }
                adjustHeight();
                $(window).on('resize', function () {
                    adjustHeight();
                });
            });

            // hide pages on load
            $('.page').not('.page-home, .page-spotlight').css({ opacity: 1, display: 'none' });


            // Side menu
            $('#media .side-menu li:eq(0)').addClass('active');
            $('.side-menu').each(function () {
                var $me = $(this), $parent = $me.parent(), $detail = $me.parents('.page').find('.subpage-wrap .subpage-detail');
                var $page = $me.parents('.page');

                // adjust box heights
                var $boxes = $detail.find('.box').not('.box-content');
                function adjustHeight() {
                    $boxes.each(function () {
                        $(this).height($boxes.eq(0).width() / 2);
                    });
                    $detail.find('.article-nav').each(function () {
                        $(this).height($(this).width());
                    });
                }

                $me.find('li a').on('click', function () {
                    var $t = $(this), i = $me.find('li a').index($t);

                    $parent.css({ display: 'block' });

                    if ($t.is('[href=#recent]')) return false;

                    if ($t.is('[href=#press]')) {
                        initArticles();
                    }
                    $me.parents('.page-content').find('.box-content-article, .article-nav').hide();

                    $detail.find('.side-menu2 li').eq(i).find('a').click();

                    // transit
                    $parent.css({ position: 'absolute', top: 'auto', left: 'auto', bottom: 'auto' }).transition({ x: (_dir * -100) + '%' }, 800, function () {
                        $parent.css({ display: 'none' });
                    });
                    $detail.css({ x: (_dir * 100) + '%', position: 'absolute', display: 'block', left: 0, top: 0 }).transition({ x: 0 }, 800, function () {
                        adjustHeight();
                    });

                    // hide thumbnails
                    if ($page.is('#media')) $('#media .boxes-wrap2').find('.box-double-thumb .thumb + .thumb').hide();


                    return false;
                });

                $detail.find('.back').on('click', function () {
                    $parent.css({ display: 'block' });
                    $detail.transition({ x: (_dir * 100) + '%' }, 800, function () {
                        $(this).css({ display: 'none' });
                    });
                    $parent.transition({ x: 0 }, 800, function () {
                        $(this).css({ position: 'static' });
                    })
                    return false;
                });

            });


        }

        /* Common Routine */

        // hide page loader
        /*$(window).on('load', function(){
        });*/
        $('.loader-page').fadeOut(500);

        // main menu expand/collapse
        $('.main-menu').each(function () {
            var $me = $(this), $cta = $me.find('.cta');
            var $util = $('.header').find('.util');

            var _origin = $me.position().left;
            if (isRTL) _origin = parseInt($me.css('right'));
            $me.data('origin', _origin);


            $cta.on('click', function () {
                var $h1 = $me.prev('h1');


                if (!$me.is('.main-menu-expanded')) {
                    if (isRTL) $me.transition({ right: 0 });
                    else $me.transition({ left: 0 });

                } else {
                    if (isRTL) $me.transition({ right: _origin });
                    else $me.transition({ left: _origin });
                }

                $me.toggleClass('main-menu-expanded');

                if ($me.is('.main-menu-expanded')) $h1.css({ top: 0 });
                else $h1.removeAttr('style');

                if (isMobile) {
                    if ($me.is('.main-menu-expanded')) {
                        var _top = $h1.outerHeight() + $me.find('.themenu').outerHeight();
                        $util.css({ top: _top });
                    }
                    else {
                        $util.removeAttr('style');
                    }
                }

                return false;
            });

            $('body').on('click', function () {
                if ($me.is('.main-menu-expanded')) $cta.click();
            });
            $me.on('click', function (e) {
                if ($(e.target).is('a[target=_blank]')) return true;
                e.preventDefault();
                e.stopPropagation();
                return false;
            });
        });

        // is home
        function isHome() {
            if (parseInt($('.pages-wrap').css('y')) >= 0) {
                $mainWrap.addClass('is-home');
                return true;
            }
            else {
                $mainWrap.removeClass('is-home');
                return false;
            }
        }

        function isHome2() {
            if (parseInt($mainWrap.scrollTop()) >= 80) {
                $('body').removeClass('is-home');
                return true;
            }
            else {
                $('body').addClass('is-home');
                return false;
            }
        }

        // home slogan
        $('.home-slogan .slogan strong').fitText(1.9, {
            maxFontSize: '32px'
        });

        // spotlight CTA
        /*$('.spot-wrap .spot-item').each(function(){
            var $t = $(this);
    
            $t.on('click','a.cta-box', function(){
                $('.themenu a[href=#group]').click();
                setTimeout(function(){
                    $('#group .side-cta').click();
                    // show Expertise page
                    $('#group .subpage-boxes .boxes-wrap2 .box:eq(3)').click();
                },2500);
                return false;
            });
        });*/

        // adjust height of video player
        $('.video-wrap').each(function () {
            var $me = $(this), $close = $me.find('.close'), $iframe = $me.find('iframe');

            function adjustHeight() {
                $me.height($me.width() * (540 / 960));
            }
            // adjustHeight();
            $(window).on('load resize', function () {
                adjustHeight();
            });

            $close.on('click', function () {
                $('.overlay').fadeOut(function () {
                    $iframe.attr('src', $iframe.attr('src'));
                });
            });
            // home slogan video CTA
            $('.home-slogan .cta').on('click', function () {
                $('.overlay').fadeIn();
                adjustHeight();
                return false;
            });
        });


        // Terms and Conditions
        $('.overlay-tandc, .overlay-privacy').each(function () {
            var $me = $(this), $close = $me.find('.close');

            $close.on('click', function () {
                $me.fadeOut();
                if (!$mainWrap.is('.locked')) $('.scroll-hint, .bullets').show();
                return false;
            });
        });
        $('.footer-wrap li:eq(1) a').on('click', function () {
            $('.scroll-hint, .bullets').hide();
            $('.overlay-tandc .generic-wrap').css({ y: '100%' }).delay(600).transition({ y: 0 });
            $('.overlay-tandc').fadeIn(600, function () {
                var $copyWrap = $(this).find('.generic-wrap');
                var $copy = $(this).find('.generic-copy');
                $copyWrap.height($copyWrap.height());
                if (!Modernizr.touch) {

                    $copy.jScrollPane({
                        autoReinitialise: true,
                        verticalGutter: 20,
                        animateScroll: true,
                        animateDuration: 200,
                        arrowButtonSpeed: 100,
                        verticalDragMaxHeight: 130,
                        hideFocus: true
                    });
                };
            });
            return false;
        });
        $('.footer-wrap li:eq(2) a').on('click', function () {
            $('.scroll-hint, .bullets').hide();
            $('.overlay-privacy .generic-wrap').css({ y: '100%' }).delay(600).transition({ y: 0 });
            $('.overlay-privacy').fadeIn(600, function () {
                var $copyWrap = $(this).find('.generic-wrap');
                $copyWrap.height($copyWrap.height());

                if (!Modernizr.touch) {

                    $(this).find('.generic-copy').jScrollPane({
                        autoReinitialise: true,
                        verticalGutter: 20,
                        animateScroll: true,
                        animateDuration: 200,
                        arrowButtonSpeed: 100,
                        verticalDragMaxHeight: 130,
                        hideFocus: true
                    });
                };
            });
            return false;
        });

        function populateThumbnails($detail) {
            var $box3 = $detail.find('.box-menu .thumb');
            var $box2 = $detail.find('.box-double-thumb .thumb:eq(0)');
            var $box1 = $detail.find('.box-double-thumb .thumb:eq(1)');
            var $srcWrap = $detail.find('.box-content .active .thumbs-wrap, .box-content-article .current .thumbs-wrap');
            // remove previously populated
            $detail.find('.box .thumb img.temp').remove();

            // populate
            $box1.append($srcWrap.find('.thumb1 img').clone().addClass('temp'));
            $box2.append($srcWrap.find('.thumb2 img').clone().addClass('temp'));
            $box3.append($srcWrap.find('.thumb3 img').clone().addClass('temp'));
        }

        // initialize Articles
        initArticles = function () { };
        var updateNavOutside = null;

        $('.article-wrap').each(function () {
            var $me = $(this), $articles = $me.find('.article-detail'), $nav = $('.article-nav a'), $next = $nav.filter('.next'), $prev = $nav.filter('.prev');

            $next.on('click', function () {
                var $t = $(this);
                if ($t.is('.disabled, .locked')) return false;
                $t.addClass('locked');
                transitArticle(getNextArticle(), 1, function () {
                    updateNav();
                    $t.removeClass('locked');
                });
                return false;
            });
            $prev.on('click', function () {
                var $t = $(this);
                if ($t.is('.disabled, .locked')) return false;
                $t.addClass('locked');
                transitArticle(getPrevArticle(), -1, function () {
                    updateNav();
                    $t.removeClass('locked');
                });
                return false;
            });

            initArticles = function () {
                var $current = getCurrent();
                if (!$current.length) $articles.eq(0).addClass('current');
                updateNav();
                $('.article-nav').css({ display: 'none' });
            }

            function getCurrent() {
                return $me.find('.current');
            }
            function getNextArticle() {
                return $me.find('.current').next('.article-detail');
            }
            function getPrevArticle() {
                return $me.find('.current').prev('.article-detail');
            }
            function transitArticle($target, _dir, fn) {
                var $current = getCurrent();
                $me.parent().next('.jspVerticalBar').hide();
                $target.css({ display: 'block', x: _dir > 0 ? '100%' : '-100%', position: 'absolute', top: 0 }).transition({ x: 0 }, 600, function () {
                    $target.css({ position: 'static' }).addClass('current');

                    // put image on the box menu
                    var _img = $target.find('.thumb1 img').attr('src');
                    $('#media .boxes-wrap2 .box-double-thumb .thumb:eq(1) img').attr('src', _img);

                    // if mobile scroll to top
                    if (isMobile) $me.parents('.page').scrollTo(0, 500);
                    $me.parent().next('.jspVerticalBar').show();
                });
                $current.transition({ x: _dir > 0 ? '-110%' : '110%' }, 600, function () {
                    $current.css({ display: 'none' }).removeClass('current');
                    if (fn) fn();
                });
            }
            function updateNav() {
                var $current = getCurrent();
                $nav.removeClass('disabled');

                if ($current.is('.article-detail:first-child')) $prev.addClass('disabled');
                else $prev.removeClass('disabled');
                if ($current.is('.article-detail:last-child')) $next.addClass('disabled');
                else $next.removeClass('disabled');
            }
            updateNavOutside = updateNav;
        });



        // accordion
        $('.accordion').each(function () {
            var $me = $(this), $item = $me.find('.accord-item');

            $item.find('>h4').on('click', function () {
                var api = $me.parents('.scrollable').data('jsp');
                var $t = $(this), $p = $t.parent();
                /*$item.not($p).find('.accord-body').slideUp(function(){
                });*/
                if ($p.is('.expanded')) {
                    $p.find('.accord-body').slideUp(function () {
                        $p.removeClass('expanded');
                    });
                }
                else {
                    $p.find('.accord-body').slideDown(function () {
                        $p.addClass('expanded');
                        // $item.not($p).removeClass('expanded').find('.accord-body').slideUp();
                        setTimeout(function () {
                            if (!isMobile) api.scrollToElement($t, true, true);
                        }, 500);
                    });
                }
            });

            // $item.eq(0).find('>h4').click();
        });

        // contact form
        $('.util .links a[rel=contact]').on('click', function () {
            $('.overlay3 .close').click();
            if (isMobile) $('.main-menu .cta').click();
            $('.overlay2').fadeIn();
            var $wrap = $('.contact-form-wrap');
            $wrap.height($wrap.height()).css({ y: '100%' }).delay(300).transition({ y: 0 }, function () {
                // set info height
                $wrap.find('.info-wrap').height($wrap.find('.field-wrap').height() - 75);
            });
            $mainWrap.addClass('locked');
            return false;
        });
        $('.contact-form-wrap').each(function () {
            var $me = $(this), $close = $me.find('> .close'), $frm = $me.find('form');

            $close.on('click', function () {
                $me.transition({ y: '100%' }, function () {
                    $('.overlay2').fadeOut(function () {
                        // reload & reset form
                        $me.find('.field-wrap').fadeIn();
                        $me.find('.thank-you-wrap').fadeOut();
                        $frm[0].reset();
                    });
                });
                $mainWrap.removeClass('locked');
            });

            $frm.on('submit', function () {
                if (isValidForm()) {
                    // post data here
                    var qString = $frm.serialize();
                    var postData = $frm.serializeArray();

                    // $.post(base_url + '/main/contact_us?' + qString, function(resp){
                    $.post(base_url + '/main/contact_us', postData, function (resp) {

                        showThankYou();
                    });

                    function showThankYou() {
                        $me.find('.field-wrap').fadeOut(function () {
                            $me.find('.thank-you-wrap').fadeIn();
                        });
                    }
                }
                return false;
            });
            function isValidForm() {
                $me.find('.error').removeClass('error');

                $me.find('.txt1').each(function () {
                    var $t = $(this);
                    if ($t.val() == '') $t.parent().addClass('error');
                    if ($t.is('[name=contact_email]') && $t.val() != '' && !isEmail($t.val())) $t.parent().addClass('error');
                });

                if ($me.find('.error').length) return false;
                else return true;
            }


        });

        // press kit login
        var loggedIn = false;
        $('.media-kit').each(function () {
            var $me = $(this), $register = $me.parent().find('.register-wrap'), $frm_login = $me.find('.login form');
            var $kit = $me.parent().find('.kit-download'), $login = $me.find('.login'), $forgotLink = $login.find('p a');
            var $forgotDiv = $me.find('.forgot-password'), $frm_forgot = $forgotDiv.find('form'), $backToLogin = $forgotDiv.find('p > a');
            var $invalid = $login.find('.invalid-login');
            var $invalid2 = $forgotDiv.find('.invalid-login');

            $frm_login.on('submit', function () {
                $login.find('.error').removeClass('error');
                $login.find('.txt1').each(function () {
                    var $t = $(this);
                    if ($t.val() == '') $t.addClass('error');
                    if ($t.is('.email') && $t.val() != '' && !isEmail($t.val())) $t.addClass('error');
                });

                if ($login.find('.error').length < 1) {
                    // post username & password
                    var postData = $frm_login.serializeArray();
                    $.post(base_url + '/account/login', postData, function (resp) {
                        if (resp == 'success') {
                            // if creds are correct
                            $('.overlay4').fadeOut(function () {
                                enableDownloads();
                            });

                        }
                        else {
                            $invalid.fadeIn();
                            setTimeout(function () {
                                $invalid.fadeOut();
                                $login.find('.txt1').eq(0).focus();
                            }, 3000);
                        }
                    });


                } else {
                    $login.find('.error').eq(0).focus();
                }

                return false;
            });

            $frm_forgot.on('submit', function () {
                var $t = $(this), $email = $t.find('input[name=login_email]');
                $t.find('.error').removeClass('error');

                if ($email.val() == '' || !isEmail($email.val())) {

                    $email.addClass('error').focus();
                    return false;

                } else {
                    // submit email
                    var postData = $frm_forgot.serializeArray();
                    $.post(base_url + '/account/forgot_password', postData, function (resp) {
                        if (resp == 'failed') {
                            $invalid2.fadeIn();
                            setTimeout(function () {
                                $invalid2.fadeOut();
                                $forgotDiv.find('.txt1').eq(0).focus();
                            }, 3000);
                        }
                        if (resp == 'success') {
                            $t.fadeOut();
                            $t.parent().find('>p').eq(0).text($forgotDiv.data('success-msg'));
                        }
                    });
                }
                return false;
            });

            $me.find('.request-login a').on('click', function () {
                $me.fadeOut(function () {
                    $register.fadeIn(function () {
                        $register.find('.form-wrap').height($register.parent().outerHeight() - 110).jScrollPane({
                            autoReinitialise: true,
                            verticalGutter: 10,
                            verticalDragMaxHeight: 130,
                            hideFocus: true
                        });
                    });
                });
                return false;
            });

            $forgotLink.click(function () {
                $login.fadeOut(function () {
                    $forgotDiv.fadeIn();
                });
                return false;
            });
            $backToLogin.click(function () {
                $forgotDiv.fadeOut(function () {
                    $login.fadeIn();
                });
                return false;
            });
        });

        // download kit
        $('.kit-download').each(function () {
            var $me = $(this), $logout = $me.find('.logout');
            var $moreLinks = $me.find('.more');

            // on load set
            $moreLinks.each(function () {
                var $t = $(this), _href = $t.attr('href');
                $t.data('orig-href', _href);
                $t.attr('href', '');

                $t.click(function () {
                    if ($me.is('.kit-disabled')) $logout.click();
                    else return true;
                    return false;
                });
            });

            $logout.on('click', function () {

                if ($me.is('.kit-disabled')) {
                    showLogin();
                } else {
                    disableDownloads();
                }

                return false;
            });

        });

        function showLogin() {
            var $overlay = $('.overlay4'), $close = $overlay.find('.close');
            $overlay.find('.generic-wrap').css({ y: '100%' });
            $overlay.fadeIn(function () {
                $(this).find('.generic-wrap').transition({ y: 0 });
            });

            $close.off('click').on('click', function () {
                $overlay.fadeOut();
                return false;
            });
        }

        function enableDownloads() {
            $('.kit-download').each(function () {
                var $k = $(this).removeClass('kit-disabled');
                var $logout = $k.find('.logout');
                $logout.data('orig-text', $logout.text());
                $logout.text($logout.data('other-text'));
                $('.overlay4').fadeOut();

                $k.find('.more').each(function () {
                    var $t = $(this);
                    $t.attr('href', $t.data('orig-href'));
                });
            });
        }
        function disableDownloads() {
            $('.kit-download').each(function () {
                var $k = $(this).addClass('kit-disabled');
                var $logout = $k.find('.logout');
                $logout.text($logout.data('orig-text'));

                $k.find('.more').each(function () {
                    var $t = $(this), _href = $t.attr('href');
                    $t.data('orig-href', _href);
                    $t.attr('href', '');
                });
            });
        }

        // ragistration form
        $('.register-wrap').each(function () {
            var $me = $(this), $frm = $me.find('form'), $showLogin = $me.find('.show-login'), $back = $me.find('.go-back');

            $frm.on('submit', function () {
                if (isValidForm()) {
                    // post data here
                    var qString = $frm.serialize();
                    var postData = $frm.serializeArray();

                    // $.post(base_url + '/account/index?' + qString, function(resp){
                    $.post(base_url + '/account/index', postData, function (resp) {
                        if (resp == 'failed') {
                            alert('This email is already registered. Please use another email.');
                            $me.find('.email').focus();
                        }
                        else showThankYou();
                    });

                    function showThankYou() {
                        $me.find('.form-wrap').fadeOut(function () {
                            $me.find('.thank-you-wrap').fadeIn();
                        });
                    }

                }
                else {
                    $me.find('.error').eq(0).find('.txt1').focus();
                }
                return false;
            });
            function isValidForm() {
                $me.find('.error').removeClass('error');

                $me.find('.req .txt1').each(function () {
                    var $t = $(this);
                    if ($t.val() == '') $t.parent().addClass('error');
                });

                $me.find('.email').each(function () {
                    var $t = $(this);
                    if ($t.val() != '' && !isEmail($t.val())) $t.parent().addClass('error');
                });
                $me.find('.phone').each(function () {
                    var $t = $(this);
                    if ($t.val() != '' && !isPhoneNumber($t.val().replace(' ', ''))) $t.parent().addClass('error');
                });

                var pwd1 = $me.find('.pwd1').val();
                var pwd2 = $me.find('.pwd2').val();
                if (pwd1 !== pwd2) $me.find('.pwd2').parent().removeClass('minchar').addClass('error mismatch');
                if (pwd1.length < 6) $me.find('.pwd2').parent().removeClass('mismatch').addClass('error minchar');

                if ($me.find('.error').length) return false;
                else return true;
            }

            $back.on('click', function () {
                $me.fadeOut(function () {
                    $me.parent().find('.media-kit').fadeIn();
                });
                return false;
            });
            $showLogin.on('click', function () {
                $back.click();
                return false;
            });
        });


        function loadArticle($boxParent, _imgThumb) {
            $boxParent.fadeOut(function () {
                $('.box-content-article, .article-nav').fadeIn();
            });
            var $thumb = $boxParent.parents('.boxes-wrap2').find('.box-double-thumb .thumb + .thumb');
            if (_imgThumb) {
                $thumb.find('img:last-child').attr('src', _imgThumb);
            }
            $thumb.show();

        }
        function unloadArticle(fn) {
            $('.box-content-article, .article-nav').fadeOut(function () {
                $('.box-content-article').prev('.box-content-item').fadeIn();
                if (fn) fn();
            });
            $('#media .boxes-wrap2').find('.box-double-thumb .thumb + .thumb').hide();
        }

        // news list
        $('.news-list').each(function () {
            var $me = $(this).addClass('clearfix'), $li = $me.find('> ul > li'), $links = $me.find('li a.more'), $articleWrap = $('.article-wrap');

            var $sideMenu = $me.parent().prev('.side-menu');

            $links.on('click', function () {
                var $t = $(this), id = $t.data('target');
                if (!id) return;

                var $boxParent = $me.parents('.box-content-item');

                $articleWrap.find('.article-detail').css({ display: 'none' }).removeClass('current').filter('[data-article-id=' + id + ']').addClass('current').css({ x: 0, display: 'block' });
                // $sideMenu.find('a[href=#recent]').click();
                var _img = $t.parent().parent().find('.thumb img').attr('src');

                loadArticle($boxParent, _img);
                updateNavOutside();

                return false;
            });

            $me.find('li').each(function () {
                var $t = $(this);
                $t.find('.thumb, h4').css({ cursor: 'pointer' });
                $t.on('click', '.thumb, h4', function () {
                    $t.find('.more')[0].click();
                    return false;
                });
            });

            // list paging
            $(window).on('load', function () {

                if (!isMobile) {

                    var $pager = $('<div class="pager" />');
                    var _visible = 4;
                    if ($me.parent().is('.news-list-wrap')) _visible = 6;
                    var _pageCount = Math.ceil($li.length / _visible);
                    for (var i = 0; i < _pageCount; i++) {
                        if (i == 0) $pager.append('<span data-target="grp' + i + '" class="active">' + (i + 1) + '</span>');
                        else $pager.append('<span data-target="grp' + i + '">' + (i + 1) + '</span>');
                    };
                    $li.each(function (i) {
                        var _x = Math.floor(i / _visible);
                        $(this).addClass('grp' + _x);
                    });

                    $pager.on('click', 'span', function () {
                        var $t = $(this), i = $pager.find('span').index($t), _target = $t.data('target');
                        if ($t.is('.active')) return false;
                        var currIndex = $pager.find('span').index($pager.find('.active'));
                        var dir = i > currIndex ? (_dir * 200) + '%' : (_dir * -200) + '%';
                        $li.css({ display: 'none' }).filter('.' + _target).css({ x: dir, opacity: 0, display: 'block' }).delay(50).transition({ x: 0, opacity: 1 }, 500);
                        $pager.find('span').removeClass('active');
                        $(this).addClass('active');
                        // set scroll to Top
                        $me.data('jsp').scrollToY(0);
                        return false;
                    });

                    $me.before($pager);

                    var $copyWrap = $me.parents('.copy-wrap');

                    if ($me.parent().is('.news-list-wrap')) {
                        function adjustListHeight() {
                            $me.height($copyWrap.height() - 80);
                        }
                        $(window).on('resize', function () {
                            adjustListHeight();
                        });
                        adjustListHeight();
                        // invoke scrollbar here...
                        $me.off('mousewheel').on('mousewheel', function () {
                            if ($(this).is('.jspScrollable')) return false;
                        })
                        .jScrollPane({
                            autoReinitialise: true,
                            verticalGutter: 10,
                            verticalDragMaxHeight: 130,
                            hideFocus: true
                        });
                    }

                }
            });
        });


        // load history - defined below inside history wrap closure
        var loadHistory = function () { };
        var closeHistory = function () { };

        // history
        $('.history-wrap').each(function () {
            var $me = $(this), $theList = $me.find('.history-list'), $bgImage = $me.find('.bg-image'), $items = $me.find('.history-item'), $timelist = $me.find('.time-list');


            // load images/ thumbnails
            var $thumbWraps = $me.find('.thumb2, .video-thumb');

            function loadImages() {
                $bgImage.each(function () {
                    var $t = $(this), _img = $t.data('img-src') || $t.find('>img').attr('src');
                    $t.css({ backgroundImage: 'url(' + _img + ')' });
                    $t.find('img').css({ display: 'none' });
                });

                $thumbWraps.add($me.find('.thumb1')).each(function () {
                    var $t = $(this), _img = $t.data('img-src') || $t.find('>img').attr('src');
                    if (_img) $t.css({ backgroundImage: 'url(' + _img + ')' });
                    // if(_img && $t.is('.video-thumb')) $t.addClass('with-thumb');
                    if ($t.is('.thumb2') && _img) $t.next('.video-thumb').addClass('video-thumb-bg');
                    $t.find('img').css({ display: 'none' });
                });

                if ($timelist.find('li.active').length < 1) $timelist.find('li:eq(0)').addClass('active');
            }

            $(window).on('resize', function () {
                adjustThumbsHeight();
                setTheListHeight();
                adjustTimeList();
            });
            function adjustThumbsHeight() {
                if (isMobile) {
                    $me.find('.thumb1').each(function () {
                        $(this).height($(this).width());
                    });
                }
                $thumbWraps.each(function () {
                    $(this).height($(this).width());
                });
            }
            function setTheListHeight() {
                var _len = $theList.find('> li').length;
                var _h = $(window).height();
                $theList.find('> li').height(_h);
                $theList.height(_len * _h);
            }
            adjustThumbsHeight();
            setTheListHeight();

            // generate timeline
            $items.each(function (i) {
                var $t = $(this), $year = $t.find('.year'), _img = $t.find('.thumb1').data('img-src') || $t.find('.thumb1 img').attr('src'), $close = $t.find('.back, .close');
                var $preview = $('<li><div class="preview"><span class="year">' + $year.text() + '</span><span class="thumb"/></div></li>');
                $preview.find('.thumb').css({ backgroundImage: 'url(' + _img + ')' });
                $timelist.append($preview);

                $close.on('click', function () {
                    closeHistory();
                    return false;
                });

                if (i % 2 && isMobile) $t.parent().addClass('odd');
            });

            function adjustTimeList() {
                $timelist.css({ marginTop: $('.header h1').height() + 30 });
                $timelist.find('>li').height($(window).height() * .054);
            }
            adjustTimeList();

            // time line click
            $timelist.find('> li').on('click', function () {
                var $t = $(this), i = $timelist.find('> li').index($t);
                scrollToHistory($items.eq(i).parent(), function () {
                    $timelist.find('> li').removeClass('active');
                    $t.addClass('active');
                    $items.eq(i).addClass('current');
                });
                return false;
            });

            function scrollToHistory($page, fn) {
                var _yTarget = $page.position().top;
                var _pctDelta = (_yTarget / $page.parent().height() * 100).toFixed(2);
                var _currentTop = Math.abs(parseInt($theList.css('y')));
                var dir = _currentTop < _yTarget ? '' : '-';

                $theList.transition({ y: _yTarget * -1 }, 1200, 'easeOutCubic');
                if (fn) fn();

                animateHistoryPage($page, dir);

                if ($timelist.find('> li:last-child').is('.active')) $('.scroll-hint2').addClass('go-top');
                else $('.scroll-hint2').removeClass('go-top');

                // adjust timeline ruler view
                if ($timelist.outerHeight() > $timelist.parent().height()) $timelist.transition({ y: (_pctDelta / 2).toFixed(2) * -1 + '%' }, 1200);
            }
            function animateHistoryPage($page, dir) {

                $me.find('.bg-image, .history-copy, .thumb1, .video-thumb, .thumb2, .back').stop();

                $page.find('.bg-image').css({ opacity: 1, scale: 1.3 }).delay(300).transition({ opacity: .5, scale: 1.1 }, 2500, 'easeOutCubic');
                $page.find('.history-copy').css({ y: dir + '150%', opacity: 0 }).delay(300).transition({ y: 0, opacity: 1 }, 800);
                if ($page.is('.odd')) {
                    $page.find('.thumb1').css({ x: '150%', opacity: 0 }).delay(800).transition({ x: 0, opacity: 1 }, 500);
                    $page.find('.video-thumb').css({ x: '150%', opacity: 0 }).delay(1400).transition({ x: 0, opacity: 1 }, 500);

                }
                else {
                    $page.find('.thumb1').css({ x: '-150%', opacity: 0 }).delay(800).transition({ x: 0, opacity: 1 }, 500);
                    $page.find('.video-thumb').css({ x: '-150%', opacity: 0 }).delay(1400).transition({ x: 0, opacity: 1 }, 500);
                }
                $page.find('.thumb2').css({ y: '100%', opacity: 0 }).delay(1200).transition({ y: 0, opacity: 1 }, 500);
                $page.find('.back').css({ y: '150%', opacity: 0 }).delay(1500).transition({ y: 0, opacity: 1 }, 500);
            }

            // loadHistory implementation
            loadHistory = function () {
                /*$me.fadeIn(800, function(){
                    adjustThumbsHeight();
                });*/
                loadImages();

                $pagesWrap.transition({ x: (_dir * -100) + '%' }, 1200, 'easeOutCubic');
                $me.css({ x: (_dir * 100) + '%', display: 'block' }).transition({ x: 0 }, 1200, 'easeOutCubic');
                adjustThumbsHeight();
                $items.prev('.bg-image').css({ opacity: .5 });

                $('.history-copy h3').fitText(.5, {
                    maxFontSize: isRTL ? '24px' : '30px'
                });

            }
            closeHistory = function (noTrans) {
                // $me.fadeOut(function(){});
                var _speed = 1200;
                if (noTrans) {
                    $pagesWrap.css({ x: 0 });
                    _speed = 500;
                }
                else $pagesWrap.transition({ x: 0 }, _speed, 'easeOutCubic');

                $me.transition({ x: (_dir * 100) + '%' }, _speed, 'easeOutCubic', function () {
                    $(this).css({ display: 'none' });
                });
            }

            // mousewheel scroll
            $me.off('mousewheel').on('mousewheel', function (e) {
                if ($me.is('.locked')) {
                    e.preventDefault();
                    return false;
                }
                $me.addClass('locked');
                if (e.deltaY < 0) {
                    $timelist.find('li.active').next().click();
                }
                else {
                    $timelist.find('li.active').prev().click();
                }
                setTimeout(function () {
                    $me.removeClass('locked');
                }, 2000);
            });

            // touch swipe
            if (Modernizr.touch) {
                var hammertime = new Hammer($me[0]);
                hammertime.get('swipe').set({ direction: Hammer.DIRECTION_VERTICAL, threshold: 5, velocity: .5 });
                hammertime.on('swipeup', function (ev) {
                    if ($me.is('.locked')) return false;
                    $me.addClass('locked');
                    $timelist.find('li.active').next().click();
                    setTimeout(function () {
                        $me.removeClass('locked');
                    }, 2000);
                }).on('swipedown', function (ev) {
                    if ($me.is('.locked')) return false;
                    $me.addClass('locked');
                    $timelist.find('li.active').prev().click();
                    setTimeout(function () {
                        $me.removeClass('locked');
                    }, 2000);
                });
            }

            $('.scroll-hint2').on('click', function () {
                var $t = $(this);
                if ($t.is('.go-top')) {
                    $timelist.find('li:eq(0)').click();
                    return false;
                }
                $timelist.find('li.active').next().click();
                return false;
            });

            var to = null;
            var $videoBg = $('.video-bg');
            var $video = $('.video-bg video');

            var $w = $(this);
            clearTimeout(to);
            to = setTimeout(function () {
                var ratio = (960 / 540);
                if ($videoBg.width() / $videoBg.height() >= ratio) {
                    $videoBg.height($w.width() / ratio);
                    $video.height($w.width() / ratio);
                    $video.width($w.width());
                }
                else {
                    // $videoBg.width($videoBg.height() * ratio);
                    $video.height($w.height());
                    $video.width($w.height() * ratio);
                }
            }, 400);

        });


        // apply class names to video containers
        $('.box-content-item div > iframe').each(function () {
            var $t = $(this), $div = $t.parent();

            $div.addClass('video-container');
        });

    });
})(jQuery);

// email validation
var isEmail = function (sEmail) {
    return sEmail.match(/(^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$)/gi);
}

// phone validation
var isPhoneNumber = function (sPhone) {
    return sPhone.match(/(^\+[0-9]{2}|^\+[0-9]{2}\(0\)|^\(\+[0-9]{2}\)\(0\)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9\-\s]{10}$)/gi);
    // return sPhone.match(/(^\+[0-9]{2}|^\+[0-9]{2}\(0\)|^\(\+[0-9]{2}\)\(0\)|^00[0-9]{2})([0-9]{9}$|[0-9\-\s]{10}$)/gi);
}
// valid date
var isValidDate = function (d) {
    return (new Date(d));
}