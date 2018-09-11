$(document).ready(function () {


    $("#mensaje_val").hide(0);
    $("#mensaje_val_prin").hide(0);




    $("#btn_login").click(function () {

        var usuario = $("#Usuario").val();
        var clave = $("#Clave").val();              
        $("#mensaje_val_prin").hide(0);
        var json = '{"usuario":"' + usuario + '", "clave":"' + clave  + '"}';                
        $.ajax({
            type: "Post",
            url: '../Login/ValidaInicio',      
            data: json,
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",         
            success: function (response) {
                if (response.mensaje != "") {

                    $("#mensaje_val_prin").show(0);
                    $("#mensaje_error_prin").html(response.mensaje);                
                    return false;
                    
                }
                else {

                    if (response.cambio == "S") {
                        $("#ModalCambio").modal("show");
                        return false;
                    }
                    else {
                        $("#form_login").submit();

                    }
                }                
            },
            failure: function (response) {

                alert(response.d);
            }
        });
                     
    });

    
    $("#btn_aceptar").click(function () {

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
        


        var json = '{"usuario":"' + usuario + '", "clave":"' + contraseña + '", "nueva_clave":"' + contraseña_nueva + '"}';
        $.ajax({
            type: "Post",
            url: '../Login/Activa_inicio',      
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
                    $("#mensaje_error").html(response.mensaje );       
                    return false;
                }
                else {
                    
                    $("#Clave").val(contraseña_nueva);
                    $("#ModalCambio").modal("hide");
                    $("#form_login").submit();

                    }
                
            },
            failure: function (response) {

                alert(response.d);
            }
        });

    });





}); 




