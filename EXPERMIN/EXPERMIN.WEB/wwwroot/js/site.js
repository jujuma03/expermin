// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

jQuery.fn.extend({
    addLoader: function () {
        $(this).addClass("m-loader m-loader--right m-loader--light").attr("disabled", true);
    },
    removeLoader: function () {
        $(this).removeClass("m-loader m-loader--right m-loader--light").attr("disabled", false);
    },
    blockUI: function (message) {
        mApp.block($(this), {
            message: message
        });
    },
    unblockUI: function () {
        mApp.unblock($(this));
    },
    doneTyping: function (callback, timeout) {
        //timeout = timeout || 1e3;
        timeout = timeout || 800;
        let minimunLenght = 2;
        var timeoutReference,
            timeoutReferenceValidate,
            validate = function (el) {
                var input = $(el).parent().parent().find(".general_search_input");
                var quantityNeeded = minimunLenght - $(el).val().length;
                var text = ((quantityNeeded !== minimunLenght && quantityNeeded > 0) || quantityNeeded > minimunLenght) ?
                    `Por favor, introduzca ${quantityNeeded} ${(quantityNeeded === 1 ? 'caracter' : 'caracteres')}` : "";

                if (input.length <= 0) {
                    $(el).parent().parent().append(`<span class='m-form__help general_search_input'></span>`);
                    input = $(el).parent().parent().find(".general_search_input");
                    input.css({ "color": "#7b7e8a", "font-weight": "300", "font-size": ".85rem", "padding-top": "7px", "display": "inline-block" });
                }

                var isValid = (quantityNeeded === minimunLenght || quantityNeeded <= 0) ? true : false;
                input.text(text); isValid ? $(input).addClass("d-none") : $(input).removeClass("d-none");
                return isValid;
            },
            doneTyping = function (el) {
                if (!timeoutReference) return;
                timeoutReference = null;
                if ($(el).val().length >= minimunLenght || $(el).val().length === 0)
                    callback.call(el);
            };

        return this.each(function (i, el) {
            var $el = $(el);
            // Chrome Fix (Use keyup over keypress to detect backspace)
            // thank you @palerdot
            $el.is(':input') && $el.on('keyup keypress paste', function (e) {
                // This catches the backspace button in chrome, but also prevents
                // the event from triggering too preemptively. Without this line,
                // using tab/shift+tab will make the focused element fire the callback.
                if (e.type === 'keyup' && e.keyCode !== 8) return;
                if (timeoutReferenceValidate) clearTimeout(timeoutReferenceValidate);
                timeoutReferenceValidate = setTimeout(function () {
                    //validate that the input has the necessary number of characters
                    if (!validate($el)) return;
                    if (timeoutReference) clearTimeout(timeoutReference);
                    timeoutReference = setTimeout(function () {
                        // if we made it here, our timeout has elapsed. Fire the
                        // callback
                        doneTyping(el);
                    }, timeout);
                }, timeout / 5);
                // Check if timeout has been set. If it has, "reset" the clock and
                // start over again.

            }).on('blur', function () {
                // If we can, fire the event since we're leaving the field
                doneTyping(el);
            });
        });
    },
    doneTypingRange: function (callback, timeout) {
        //timeout = timeout || 1e3;
        timeout = timeout || 800;
        let minimunLenght = 1;
        var timeoutReference,
            timeoutReferenceValidate,
            validate = function (el) {
                var input = $(el).parent().parent().find(".general_search_input");
                var quantityNeeded = minimunLenght - $(el).val().length;
                var text = ((quantityNeeded !== minimunLenght && quantityNeeded > 0) || quantityNeeded > minimunLenght) ?
                    `Por favor, introduzca ${quantityNeeded} ${(quantityNeeded === 1 ? 'caracter' : 'caracteres')}` : "";

                if (input.length <= 0) {
                    $(el).parent().parent().append(`<span class='m-form__help general_search_input'></span>`);
                    input = $(el).parent().parent().find(".general_search_input");
                    input.css({ "color": "#7b7e8a", "font-weight": "300", "font-size": ".85rem", "padding-top": "7px", "display": "inline-block" });
                }

                var isValid = (quantityNeeded === minimunLenght || quantityNeeded <= 0) ? true : false;
                input.text(text); isValid ? $(input).addClass("d-none") : $(input).removeClass("d-none");
                return isValid;
            },
            doneTyping = function (el) {
                if (!timeoutReference) return;
                timeoutReference = null;
                if ($(el).val().length >= minimunLenght || $(el).val().length === 0)
                    callback.call(el);
            };

        return this.each(function (i, el) {
            var $el = $(el);
            // Chrome Fix (Use keyup over keypress to detect backspace)
            // thank you @palerdot
            $el.is(':input') && $el.on('keyup keypress paste', function (e) {
                // This catches the backspace button in chrome, but also prevents
                // the event from triggering too preemptively. Without this line,
                // using tab/shift+tab will make the focused element fire the callback.
                if (e.type === 'keyup' && e.keyCode !== 8) return;
                if (timeoutReferenceValidate) clearTimeout(timeoutReferenceValidate);
                timeoutReferenceValidate = setTimeout(function () {
                    //validate that the input has the necessary number of characters
                    if (!validate($el)) return;
                    if (timeoutReference) clearTimeout(timeoutReference);
                    timeoutReference = setTimeout(function () {
                        // if we made it here, our timeout has elapsed. Fire the
                        // callback
                        doneTyping(el);
                    }, timeout);
                }, timeout / 5);
                // Check if timeout has been set. If it has, "reset" the clock and
                // start over again.

            }).on('blur', function () {
                // If we can, fire the event since we're leaving the field
                doneTyping(el);
            });
        });
    },
});

jQuery.extend(jQuery.validator.messages, {
    required: "Campo requerido.",
    maxlength: jQuery.validator.format("Por favor, no ingresar más de {0} caracteres."),
    max: jQuery.validator.format("Por favor, ingresar un valor menor o igual a {0}."),
    min: jQuery.validator.format("Por favor, ingresar un valor mayor o igual a {0}."),
    equalTo: jQuery.validator.format("Por favor, escriba lo mismo."),
    email: jQuery.validator.format("Debes ingresar un correo electrónico válido."),
    digits: "Por favor solo ingresar dígitos",
    number : "Por favor ingresar un número válido."
});

$.validator.addMethod("alpha", function (value, element) {
    return (/^[a-zA-Z\sáéíóúñÑÁÉÍÓÚüÜ]+$/).test(value);
    // --                                    or leave a space here ^^
},"Por favor ingresar solo letras.");

$.validator.addMethod("exactlength", function (value, element, param) {
    return this.optional(element) || value.length == param;
}, $.validator.format("Por favor ingresar {0} caracteres."));

$.validator.addMethod("alphanumeric", function (value, element) {
    return (/^[a-zA-Z0-9\s]+$/).test(value);
}, "Por favor ingresar solo letras y números.");

// ----------
// Select2
// ----------
(function () {
    if (jQuery && jQuery.fn && jQuery.fn.select2 && jQuery.fn.select2.amd) {
        var e = jQuery.fn.select2.amd;

        return e.define("select2/i18n/es", [], function () {
            return {
                errorLoading: function () {
                    return "No se pudieron cargar los resultados";
                },
                inputTooLong: function (e) {
                    var t = e.input.length - e.maximum,
                        n = "Por favor, elimine " + t + " car";

                    return t === 1 ? n += "ácter" : n += "acteres", n;
                },
                inputTooShort: function (e) {
                    var t = e.minimum - e.input.length,
                        n = "Por favor, introduzca " + t + " car";

                    return t === 1 ? n += "ácter" : n += "acteres", n;
                },
                loadingMore: function () {
                    return "Cargando más resultados…";
                },
                maximumSelected: function (e) {
                    var t = "Sólo puede seleccionar " + e.maximum + " elemento";

                    return e.maximum !== 1 && (t += "s"), t;
                },
                noResults: function () {
                    return "No se encontraron resultados";
                },
                searching: function () {
                    return "Buscando…";
                }
            };
        }), { define: e.define, require: e.require };
    }
})();

$.fn.select2.defaults.set("language", "es");
$.fn.select2.defaults.set("placeholder", "-");
$.fn.select2.defaults.set("width", "100%");

// ----------
// Datepicker
// ----------
var dayNames = ["Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado"];
var monthNames = [
    "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Setiembre", "Octubre", "Noviembre",
    "Diciembre"
];
var dayNamesShort = ["Dom", "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb"];
var monthNamesShort = ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"];

$.fn.datepicker.dates.es = {
    clear: "Borrar",
    days: dayNames,
    daysMin: ["Do", "Lu", "Ma", "Mi", "Ju", "Vi", "Sa"],
    daysShort: dayNamesShort,
    format: "dd/mm/yyyy",
    months: monthNames,
    monthsShort: monthNamesShort,
    monthsTitle: "Meses",
    today: "Hoy",
    weekStart: 1
};

$.fn.datepicker.defaults.autoclose = true;
$.fn.datepicker.defaults.clearBtn = true;
$.fn.datepicker.defaults.language = "es";
$.fn.datepicker.defaults.templates = {
    leftArrow: "<i class=\"la la-angle-left\"></i>",
    rightArrow: "<i class=\"la la-angle-right\"></i>"
};
$.fn.datepicker.defaults.todayHighlight = true;

$.extend($.fn.dataTable.defaults, {
    dom: "<'top'i>rt<'bottom'lp><'clear'>",
    language: {
        "sProcessing": "<div class='m-blockui' style='display: inline; background: none; box-shadow: none;'><span>Cargando datos...</span><span><div class='m-loader  m-loader--brand m-loader--lg'></div></span></div>",
        "sLengthMenu": "Mostrar _MENU_ registros",
        "sZeroRecords": "No se encontraron resultados",
        "sEmptyTable": "Ningún dato disponible en esta tabla",
        "sInfo": "Mostrando _START_ - _END_ de _TOTAL_ registros",
        "sInfoEmpty": "Mostrando 0 - 0 de 0 registros",
        "sInfoFiltered": "(filtrado de _MAX_ registros)",
        "sInfoPostFix": "",
        "sSearch": "Buscar:",
        "sUrl": "",
        "pagingType": "simple_numbers",
        "sInfoThousands": ",",
        "sLoadingRecords": "Cargando...",
        "oPaginate": {
            "sFirst": "<i class='la la-angle-double-left'></i>",
            "sLast": "<i class='la la-angle-double-right'></i>",
            "sNext": "<i class='la la-angle-right'></i>",
            "sPrevious": "<i class='la la-angle-left'></i>"
        },
        "oAria": {
            "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
            "sSortDescending": ": Activar para ordenar la columna de manera descendente"
        }
    },
    lengthMenu: [10, 25, 50],
    lengthChange: false,
    orderMulti: false,
    pagingType: "full_numbers",
    processing: true,
    responsive: true,
    serverSide: true,
    info: true,
    order: [],
    filter: false,
    pageLength: 10,
    paging: true
});

$.ajaxSetup({
    headers: {
        "X-CSRF-TOKEN": $("meta[name=\"csrf-token\"]").attr("content")
    }
});

toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": false,
    "progressBar": false,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

$(document).ready(function () {
    $("#loading").hide();
});

