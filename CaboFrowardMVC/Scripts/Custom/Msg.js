
var mi_seleccionado;


$(document).on('hidden.bs.modal', '.modal', function () {
    $('.modal:visible').length && $(document.body).addClass('modal-open');
});

//alert + confirm + oricesar

$(document).ready(function () {
    $("body").append("<div id='ModalMsg' class='modal' data-backdrop='static' data-keyboard='false' role='dialog' tabindex='-1'>" +
        "<div class='modal-dialog'>" +
        "<div class='modal-content'>" +
        "<div class='modal-header' style='color: #fff;background-color: #337ab7; border-color: #2e6da4;'>" +
        "<button type='button' class='close' data-dismiss='modal'>&times;</button>" +
        "<h4 class='modal-title'>Atención</h4>" +
        "</div>" +
        "<div class='modal-body'>" +
        "<p> <label class='lbl_msg'></label></p>" +
        "</div>" +
        "<div class='modal-footer'>" +
        "<button id='modal_msg_ok' type='button' class='btn btn-primary' data-dismiss='modal'>Ok</button>" +
        "</div>" +
        "</div>" +
        " </div>" +
        "</div>" +
        "<div id='ModalConfirm' class='modal'  data-backdrop='static' data-keyboard='false' role='dialog'>" +
        "<div class='modal-dialog'>" +
        "<div class='modal-content'>" +
        " <div class='modal-header' style='color: #fff;background-color: #337ab7; border-color: #2e6da4;'>" +
        " <button type='button' class='close' data-dismiss='modal'>&times;</button>" +
        " <h4 class='modal-title'>Atencion</h4>" +
        "</div>" +
        "<div class='modal-body'>" +
        " <p> <label class='lbl_msg'></label></p>" +
        " </div>" +
        "<div class='modal-footer'>" +
        " <button type='button' class='btn btn-primary ok1' data-dismiss='modal' style='visibility:hidden;display:none'>Continuar</button>" +
        " <button type='button' class='btn btn-primary ok2' data-dismiss='modal' style='visibility:hidden;display:none'>Continuar</button>" +
        " <button type='button' class='btn btn-primary ok3' data-dismiss='modal' style='visibility:hidden;display:none'>Continuar</button>" +
        " <button type='button' class='btn btn-danger' data-dismiss='modal'>Salir</button>" +
        "</div>" +
        "</div>" +
        "</div>" +
        " </div>" +
        "<div id='modal_cargando'  class='modal' data-backdrop='static'  data-keyboard='false' role='dialog'>" +
        "<div class='modal-dialog'>" +
        "<div class='modal-content'>" +
        "<div class='modal-header'>" +
        "<h3 style='margin: 0;'>Procesando</h3>" +
        "</div>" +
        "<div class='modal-body'>" +
        "<div class='progress progress-striped active' style='margin-bottom: 0;'>" +
        "<div class='progress-bar' style='width: 100%'></div>" +
        "</div>" +
        "</div>" +
        "</div>" +
        "</div>" +
        "</div>")
});


function alerta(mensaje) {
    $(".lbl_msg").html(mensaje);
    $("#modal_msg_ok").show();
    $("#ModalMsg").modal("show");
}

function alerta_settimeout(mensaje) {
    $(".lbl_msg").html(mensaje);
    $("#modal_msg_ok").hide();
    $("#ModalMsg").modal("show");
    setTimeout(ocultarMensaje, 2000);

}

function ocultarMensaje() {
    $("#ModalMsg").modal("hide");
}


function confirma(mensaje, nro, mi_seleccion) {

    if (mi_seleccion != 'undefined') {
        mi_seleccionado = mi_seleccion
    }

    if (nro == '1') {
        $(".ok1").css("visibility", "visible");
        $(".ok2").css("visibility", "hidden");
        $(".ok3").css("visibility", "hidden");

        $(".ok1").css("display", "inline");
        $(".ok2").css("display", "none");
        $(".ok3").css("display", "none");
    }

    if (nro == '2') {
        $(".ok1").css("visibility", "hidden");
        $(".ok2").css("visibility", "visible");
        $(".ok3").css("visibility", "hidden");

        $(".ok1").css("display", "none");
        $(".ok2").css("display", "inline");
        $(".ok3").css("display", "none");
    }

    if (nro == '3') {
        $(".ok1").css("visibility", "hidden");
        $(".ok3").css("visibility", "hidden");
        $(".ok3").css("visibility", "visible");

        $(".ok1").css("display", "none");
        $(".ok2").css("display", "none");
        $(".ok3").css("display", "inline");
    }
    $(".lbl_msg").html(mensaje);
    $('#ModalConfirm').modal("show");
}
