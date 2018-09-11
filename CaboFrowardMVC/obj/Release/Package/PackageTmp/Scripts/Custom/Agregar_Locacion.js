
var carga_loc = 0;
var mi_json;

$(document).ready(function () {
    $("#txt_id").attr("disabled", false);
    $("#txt_descripcion").attr("disabled", true);
    $("#btn_locacion").attr("disabled", true);

                


   
    $.fn.editable.defaults.mode = 'popup';
    $.fn.editable.defaults.ajaxOptions = { type: "post" }

      





    $("#btn_guardar").click(function () {

        var descripcion = $("#txt_descripcion").val();       
        var puerto = $("#cbx_puerto").val();
        var informada = $("#cbx_informada").val();

        if (descripcion == "") {
            alerta("Descripción no puede estar vacio");
            return false;
        }
               
        if (puerto == 0) {
            alerta("Seleccione Puerto")
            return false;
        }                                
       
        var json = '{"puerto":"' + puerto + '", "descripcion":"' + descripcion + '", "locacion":"' + "0" + '", "informada":"' + "NO" + '"}';

        $.ajax({
            type: "POST",
            url: "../Locacion/GuardarLocacion",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.mensaje != "") {
                    alerta(response.mensaje);
                    return false;
                }
                else {
                   
                    FillLocacion(puerto);                    
                    $("#txt_buscar").val("");
                    $("#txt_descripcion").val("");      
                }
            },
            failure: function (response) {
                alert(response.d);
            }
        });
                           
    })


    $("#txt_descripcion_loc").keyup(function (event) {

        var descripcion = $("#txt_descripcion_loc").val().toString().toUpperCase();
        if (descripcion == "") {
            $("#tabla_loc_web tbody tr").show();
        }
        else {
            busquedaAsync(descripcion);
        }
    });


    $("#txt_buscar").keyup(function (event) {

        var descripcion = $("#txt_buscar").val().toString().toUpperCase();
        if (descripcion == "") {
            $("#tabla_locacion tbody tr").show();
        }
        else {
            busquedaAsyncEnc(descripcion);
        }
    });
        
    obtener_puerto();   
    $("#btn_locacion").click(function () {

        $("#txt_descripcion_loc").val("");
        var puerto = $("#cbx_puerto").val();
        var rut;
        if (puerto == 0) {
            alerta("Puerto no valido")
            return false;
        }
        rut = $("#cbx_puerto").find('option:selected').attr("data-rut")
        var json = '{"puerto":"' + puerto + '", "rut":"' + rut + '"}';
        $.ajax({
            type: "POST",
            url: "../LOCACION/LocacionesAPI",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: json,
            success: function (response) {

                var respuesta = response;
                if (respuesta.mensaje != '') {
                    alerta(respuesta.mensaje);
                    return false;
                }
                else {

                    if (respuesta.total == 0) {
                        $("#tabla_loc_web > tbody").html("");
                    }
                    else {

                        $("#tabla_loc_web > tbody").html("");
                        $("#tabla_loc_web > tbody").html(respuesta.html);

                    }

                    $('#tabla_loc_web > tbody > tr > td > a > button').bind("click", function () {
                        var fila_seleccionar = $(this);
                        var id = $(this).parent().parent().parent()["0"].cells[1].innerText;
                        var descripcion = $(this).parent().parent().parent()["0"].cells[2].innerText;
                        var puerto = $("#cbx_puerto").val();
                        var informada = $("#cbx_informada").val();
                        var json = '{"puerto":"' + puerto + '", "descripcion":"' + descripcion + '", "locacion":"' + id + '", "informada":"' + informada+ '"}'; 
                                             
                        $.ajax({
                            type: "POST",
                            url: "../Locacion/GuardarLocacion",
                            data: json,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                if (response.mensaje != "") {
                                    alerta(response.mensaje);
                                    return false;
                                }
                                else {
                                    FillLocacion(puerto);         
                                    $("#ModalLoc").modal("hide");     
                                }
                            }                           
                        });
                                                
                      
                    });
                }
            },           
            complete: function () {
                $("#ModalLoc").modal("show");                            
            }
        });
    })    
    
    $("#cbx_puerto").change(function () {
        $("#txt_buscar").val("");
        var puerto = $("#cbx_puerto").val();     
        if (puerto != 0) {
            FillLocacion(puerto)    
        }
        else {
            $("#tabla_locacion > tbody").html("");
        }
    });
        
    $("#cbx_informada").change(function () {
        var informada = $("#cbx_informada").val();     
        //SI
        if (informada == 1) {

            $("#txt_id").val("");
            $("#txt_descripcion").val("");


            $("#txt_id").attr("disabled", true);
            $("#txt_descripcion").attr("disabled", true);
            $("#btn_locacion").attr("disabled", false);

        }
        else if (informada == 0) {
            $("#txt_id").attr("disabled", true);
            $("#txt_descripcion").attr("disabled", false);
            $("#btn_locacion").attr("disabled", true);
            $("#txt_id").val("");
            $("#txt_descripcion").val("");

        }
        else {

            $("#txt_id").attr("disabled", true);
            $("#txt_descripcion").attr("disabled", true);
            $("#btn_locacion").attr("disabled", true);
            $("#txt_id").val("");
            $("#txt_descripcion").val("");
                            }
    });
          
    
})

function busquedaAsync(descripcion) {
    setTimeout(BuscarLoc(descripcion), 500);
}
function busquedaAsyncEnc(descripcion) {
    setTimeout(BuscarLocEnc(descripcion), 500);
}

function BuscarLocEnc(descripcion) {
    if (descripcion == "") {
        $("#tabla_locacion tbody tr").show();
    }
    else {
        $("#tabla_locacion tbody tr").hide();
        $("#tabla_locacion .campoDesc:contains('" + descripcion + "')").parents("tr").show();

    }
}

function BuscarLoc(descripcion) {
    if (descripcion == "") {
        $("#tabla_loc_web tbody tr").show();
    }
    else {
        $("#tabla_loc_web tbody tr").hide();
        $("#tabla_loc_web .campoDesc:contains('" + descripcion + "')").parents("tr").show();

    }
}

function obtener_puerto() {

    $.ajax({
        type: "POST",
        url: " ../LOCACION/CargarListados",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            var respuesta = response;
            if (respuesta.mensaje != '') {
                alerta(respuesta.mensaje);
                return false;
            }
            else {

                $("#cbx_puerto").html(respuesta.puerto);
            }
        },
        failure: function (response) {
            alert(response.d);
        }
    });
   }

function FillLocacion(ID_PUERTO) {
    $("#txt_buscar").val("");
   
    var json = '{"puerto":' + ID_PUERTO + '}';
    
    $.ajax({
        type: "POST",
        url: "../Locacion/GetLocacionPuertoPrincipal",      
        data: json,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {


            if (response.mensaje != "") {
                alerta(response.mensaje);
                return false;

            }
            else {
                $("#txt_buscar").val("");
                $("#tabla_locacion > tbody").html("");
                $("#tabla_locacion > tbody").html(response.html);                                
                $('.sel_activo').bootstrapToggle();
                $('.sel_activo').bind('change', function () {
                    var accion = "";
                    if ($(this).prop('checked') == true) {

                        accion = 0
                    }
                    else {

                        accion = 1
                    }
                    var locacion = $(this).parent().parent().parent()["0"].cells[0].innerText;
                    var puerto = $("#cbx_puerto").val();                                       
                    var json_estado = '{"puerto":"' + puerto + '", "locacion":"' + locacion + '", "opcion":"' + accion + '"}';
                                        
                    $.ajax({
                        type: "POST",
                        url: " ../Locacion/ActualizaEstadoLoc",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        data: json_estado,
                        success: function (response) {
                            var respuesta = response;
                            if (respuesta.mensaje != '') {
                                alerta(respuesta.mensaje);
                                return false;
                            }
                        }
                    });                                              
                })
                                              
                                


                var descripciones = $('.editar')                
                $.each(descripciones, function () {                  
                    $(this).editable({
                        title: 'Ingrese Nueva Descripcion',                                                                         
                        validate: function(value) {
                            if ($.trim(value) == '')
                            {
                                return 'Campo Requerido';
                            }
                            else
                            {
                              var aux=  mi_json.replace("aux_descripcion", $.trim(value));                                
                                $.ajax({
                                    type: "POST",
                                    url: " ../Locacion/ActualizaDescripcionLoc",
                                    contentType: "application/json; charset=utf-8",
                                    dataType: "json",
                                    data: aux,                                    
                                    success: function (response) {
                                        var respuesta = response;
                                        if (respuesta.mensaje != '') {
                                            alerta(respuesta.mensaje);
                                            return false;
                                        }                                       
                                    }                                   
                                });

                            }                              

                    }
                    });
                });                                
                $(".editar").bind("click",function() {
                    var puerto = $("#cbx_puerto").val();
                    var id = $(this).parent().parent()["0"].cells["0"].innerText;
                     mi_json = '{"puerto":"' + puerto + '", "descripcion":"' + "aux_descripcion" + '", "locacion":"' + id + '"}'; 
                  
                });




            }
        }
       
    });



}