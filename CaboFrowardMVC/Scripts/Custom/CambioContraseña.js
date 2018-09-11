$(document).ready(function () {

    $("#mensaje_val").hide(0);
    $("#lnk_cambio_pass").click(function () {
        
        $("#ModalCambio").modal("show");
    });

    
    $("#btn_cambio_pass").click(function () {

        var contraseña = $("#txt_inicial").val();
        var contraseña_nueva = $("#txt_nueva").val();
        var contraseña_repite = $("#txt_compara").val();
        var usuario = $("#Usuario").val();
        $("#mensaje_val").hide(0);

        if (contraseña == "") {

            $("#mensaje_val").show(0);
            $("#mensaje_error").html("Contraseña actual esta vacia");
            return false;
        }

        if (contraseña_nueva == "") {

            $("#mensaje_val").show(0);
            $("#mensaje_error").html("Contraseña Nueva esta vacia");
            return false;
        }

        if (contraseña_nueva != contraseña_repite) {

            $("#mensaje_val").show(0);
            $("#mensaje_error").html("Contraseña Nueva no coincide");
            return false;
        }

        
        var json = '{"usuario":"' + "" + '", "clave":"' + contraseña + '", "nueva_clave":"' + contraseña_nueva + '"}';
        $.ajax({
            type: "Post",
            url: '../Login/Cambio_clave',
            data: json,
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            headers: {
                "soy_ajax": "SI"
            },
            success: function (response) {
                if (response.mensaje != "") {
                    $("#mensaje_val").show(0);
                    $("#mensaje_error").html(response.mensaje);
                    return false;
                }
                else {

                    $("#frm_logoff").submit();

                }

            },
            failure: function (response) {

                alert(response.d);
            }
        });

    });

    




});
