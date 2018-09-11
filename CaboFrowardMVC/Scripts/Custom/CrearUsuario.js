var id_persona = 0;

$(document).ready(function () {
    
    $("#div_limpia").hide(0);



    


    $("#btn_aceptar").click(function () {





        $("#Rut").val("");
        $("#txt_persona").val("");
        id_persona = 0
        $("#Rut").attr("disabled", false)
        $("#btn_buscar").show(0);
        $("#div_limpia").hide(0);
        $("#Id").val(0);



    });   



    $("#btn_buscar").click(function () {
        var rut = $("#Rut").val();
        if (rut == "") {
            alerta("Ingrese Rut");
            return false;
        }       
        
        var json = '{"rut":"' + rut  + '"}';

        $.ajax({
            type: "POST",
            url: "RecuperaNombre",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                if (response.mensaje != "") {
                    alerta(response.mensaje);
                    return false;
                }
                else {
                    if (response.id!=0) {
                        $("#Rut").val(response.rut);
                        $("#txt_persona").val(response.nombre);
                        id_persona = response.id
                        $("#Id").val(id_persona);                      
                        $("#Rut").attr("disabled", true)
                        $("#btn_buscar").hide(0);
                        $("#div_limpia").show(0);


                    }
                    else {
                       
                        $("#txt_persona").val("");
                        id_persona = 0                            
                    }                   
                 
                }

            },
            failure: function (response) {

                alert(response.d);
            }
        });

                

    });   
                
    $("#btn_limpia").click(function () {
        $("#Rut").val("");
        $("#txt_persona").val("");
        id_persona = 0
        $("#Rut").attr("disabled", false)
        $("#btn_buscar").show(0);
        $("#div_limpia").hide(0);
        $("#Id").val(0);        
    });   
        
    $("#btn_guarda").click(function () {
      
        if (id_persona==0) {
            alerta("Validar Rut Persona");
            return false;
        }

        if ($("#Usuario").val() =="") {
            alerta("Ingrese nombre de usuario");
            return false;
        }

        if ($("#Telefono").val() == "") {
            alerta("Ingrese Teléfono");
            return false;
        }
        
        if ($("#Clave").val() == "") {
            alerta("Ingrese Clave Inicial");
            return false;
        }




    });   
       

});