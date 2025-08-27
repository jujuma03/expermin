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

    var carousels = {
        init: function () {
            $('#owl1').owlCarousel({
                autoplay: true,
                loop: true,
                margin: 24,
                nav: true,
                dots: false,
                autoplayTimeout: 5000,
                autoplayHoverPause: true,
                smartSpeed: 600,
                stagePadding: 16,
                responsive: {
                    0: { items: 1 },
                    576: { items: 1 },
                    768: { items: 2 },
                    992: { items: 3 } // desktop: 3 por vista
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

                $(".carousel-products-owl").owlCarousel({
                    items: 3,
                    autoplay: true,
                    loop: true,
                    margin: 0,
                    nav: true,
                    dots: false,
                    autoplayTimeout: 5000,
                    autoplayHoverPause: true,
                    smartSpeed: 600,
                    stagePadding: 0,
                    responsive: {
                        0: { items: 1 },
                        576: { items: 1 },
                        768: { items: 2 },
                        992: { items: 3 } // desktop: 3 por vista
                    }
                });
            })
            .fail(function () {
                $("#products").html("<p>Error al cargar productos.</p>");
            });
        }
    };
    var testimonies = {
        init: function () {
            $.ajax({
                url: `/get-testimonies`,
                type: "Get"
            }).done(function (result) {
                $("#testimonies").html(result);

                $(".carousel-testimonies-owl").owlCarousel({
                    items: 3,
                    autoplay: true,
                    loop: true,
                    margin: 0,
                    nav: true,
                    dots: false,
                    autoplayTimeout: 5000,
                    autoplayHoverPause: true,
                    smartSpeed: 600,
                    stagePadding: 0,
                    responsive: {
                        0: { items: 1 },
                        576: { items: 1 },
                        768: { items: 2 },
                        992: { items: 3 } // desktop: 3 por vista
                    }
                });
            })
                .fail(function () {
                    $("#testimonies").html("<p>Error al cargar testimonios.</p>");
                });
        }
    };

    var collaborators = {
        init: function () {
            $.ajax({
                url: `/get-collaborators`,
                type: "Get"
            }).done(function (result) {
                $("#collaborators").html(result);
            })
                .fail(function () {
                    $("#collaborators").html("<p>Error al cargar categorias.</p>");
                });
        }
    };

    return {
        init: function () {
            events.init();
            products.init();
            testimonies.init();
            carousels.init();
            collaborators.init();

        }
    };
}();


$(function () {
    Home.init();
});
