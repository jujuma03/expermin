var topbar = function () {

    var notification = {
        load: function () {
            $.ajax({
                url: "/notificaciones/get",
                type: "GET",
                dataType: "HTML"
            })
                .done(function (e) {
                    mApp.block("#notification_item_topbar", {
                        message : "Cargando notificaciones"
                    });

                    $("#notification_item_topbar").html(e);
                });
        },
        read: function () {
            $("#notification_item_topbar").on("click",".m_link_user_notification", function () {
                var id = $(this).data("id");
                var url = $(this).data("url");
                $.ajax({
                    url: `/notificaciones/read-notification?id=${id}`
                }).done(function () {
                    window.location.href = url;
                });
            });
        },
        init: function () {
            this.load();
            this.read();
        }
    };

    var connection = {
        init: function () {
            "use strict";
            var connection = new signalR.HubConnectionBuilder().withUrl("/LegalConnectionHub").build();

            connection.start().then(function () {
                console.log("Legal Connnection Hub Ready");
            }).catch(function (err) {
                return console.error(err.toString());
            });

            connection.on("ReceiveMessage", function (message) {
                toastr.info(message, "Nueva notificacion.");
                notification.load();
            });

            connection.on("closesession", function () {
                $("#logoutForm button").click();
            });
        }
    };

    return {
        init: function () {
            connection.init();
            notification.init();
        }
    };
}();

$(() => {
    topbar.init();
});
