var Home = function () {
    var count = 2;
    var countText = $("#totalCount").text();
    countText = countText.split('/');
    var events = {
        init: function () {
            $("#carouselExampleIndicators").on("slid.bs.carousel", function () {
                let orderNumber = parseInt($(".carousel-item.active .order").html());
                if (count > parseInt(countText, 10))
                    count = 1;
                $(".m-main-carousel__indicators-page").text(orderNumber);
            });
        }
    };

    var carosel = {
        init: function () {
            "use strict";
            $('.next').click(function () { $('.carousel').carousel('next'); return false; });
            $('.prev').click(function () { $('.carousel').carousel('prev'); return false; });
        }
    }

    var carousels = {
        init: function () {
            $('#owl1').owlCarousel({
                autoplay: true,
                loop: true,
                margin: 50,
                nav: true,
                dots: false,
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

            $('.carousel-top-lawyers-owl').owlCarousel({
                autoplay: true,
                loop: true,
                margin: 200,
                nav: true,
                dots: true,
                responsive: {
                    0: {
                        items: 1
                    },
                    600: {
                        items: 1
                    },
                    1000: {
                        items: 1
                    }
                }
            });
        }
    };

    var products = {
        init: function () {
            $.ajax({
                url: `/get-products`,
                type: "Get"
            }).done(function (result) {
                $("#products").html(result);
            })
            .fail(function () {
                $("#products").html("<p>Error al cargar productos.</p>");
            });
        }
    };
    var lawyerbanner = {
        init: function () {
            $.ajax({
                url: `/get-banner-abogado`,
                type: "Get"
            }).done(function (result) {
                $("#lawyer-banner").html(result);
            });
        }
    };
    return {
        init: function () {
            events.init();
            products.init();
            //lawyerbanner.init();
            carousels.init();

        }
    };
}();


$(function () {
    Home.init();
});
