var id_aprobado;

$(document).ready(function () {
    $("#txt_rut").focus();
    
    $("#mensajeok").hide(0);
    $("#mensaje_exito").hide(0);
    $("#mensaje_error").hide(0);

    ObtenerListados();

    $('#txt_rut').keypress(function (e) {
        
        if (e.which == 13) {
            buscar_datos();
            return false;
        }                    
    });


    $('#txt_pasaportein').keypress(function (e) {

        if (e.which == 13) {
            buscar_datos();
            return false;
        }
    });
                  
    $("#btn_aprobar").click(function () {
        $('#ModalAprobadores').modal('show');
        return false;
    });
    

    $("#btn_modificar").click(function () {
        $('#ModalPatente').modal('show');
        return false;
    });
    
    $("#btnguardarcs").click(function () {

        var patente = $("#txtingresaPatentecs").val();
        var tipo = $("#selecttvcs").val();           
        var opcion_seleccionada = $("#selecttvcs option:selected").text();      

        if (opcion_seleccionada == 0)
        {
            alerta("Elija tipo de vehiculo")
            return false;
        }
        var json = '{"patente":"' + patente + '", "tipo":"' + tipo + '", "descripcion":"' + opcion_seleccionada + '", "id":"' + $("#txt_solicitud").val() + '"}'; 

       $.ajax({
            type: "POST",
            url: "GuardaVehiculo",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                if (response.mensaje != "") {
                    alerta(response.mensaje);
                    return false;
                }
                else {
                    $("#cbx_vehiculo").html("");
                    $("#cbx_vehiculo").html(response.patente);    
                    $('#ModalPatente').modal('hide');
                    }
            },
            failure: function (response) {

                alert(response.d);
            }
        });                      
        return false;
    });         
});


function limpiarcadena(cadena) {

	// Definimos los caracteres que queremos eliminar
	var specialChars = "!@#$^&%*()+=-[]\/{}|:<>?,.";

	for (var i = 0; i < specialChars.length; i++) {
		cadena = cadena.replace(new RegExp("\\" + specialChars[i], 'gi'), '');
	}
	// Quitamos espacios y los sustituimos 
	cadena = cadena.replace(/ /g, "");
	cadena = cadena.substring(0, cadena.length - 1);

	return (cadena);

}

function buscar_datos() {

	// Definimos los caracteres que queremos eliminar
	var specialChars = "!@#$^&%*()+=-[]\/{}|:<>?,.";
	var rut = $('#txt_rut').val();
	var rut_limpio = "";
	//indica la posicion en que esta este texto, si no lo encuentra devuelve -1
	var flag = rut.search("https");

	if (flag != -1) //Es QR
	{

		inicio = 52,
			fin = 62,
			rut_con_simbolos = rut.substring(inicio, fin);


		rut_limpio = limpiarcadena(rut_con_simbolos);

		//alert(rut_limpio);

	}

	if (flag == -1) //es PDF417
	{

		inicio = 0,
			fin = 9,
			rut_con_simbolos = rut.substring(inicio, fin);


		rut_limpio = limpiarcadena(rut_con_simbolos);

	//	alert(rut_limpio);

	}



	//fin victor




    //var rut = $("#txt_rut").val();
    var aux = rut.replace("&", "");
    rut = aux;
    aux = rut.replace("-", "");
    rut = aux;
    res = rut.replace(" ", "");
    rut = res;
    res = rut.replace("'", "");
    rut = res;
    res = rut.replace("/", "");
	rut = rut_limpio;
    $("#txt_rut").val(rut);

    var pasaporte = "";
	if (rut_limpio == "") {
		rut_limpio = 0;
        pasaporte = $("#txt_pasaportein").val();
	}
    //} else {
   
    //var final = 0;
    //final = rut.length;
    //final = final - 1;
    //var dv = rut.substring(final, final + 1);
    //var res = rut.substring(0, final);
    //    rut = res;
   
    //}
	var json = '{"rut":"' + rut_limpio + '", "pasaporte":"' + pasaporte + '"}'; 
    id_aprobado = 0;
    $.ajax({
        type: "POST",
        url: "VerIngreso",
        data: json,
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            if (response.mensaje != "") {
                alerta(response.mensaje);
                return false;
            }
            else {             
                
                if (response.existe == 1) {                  

                    $("#txt_solicitud").val(response.solicitud.Idsolicitud);
                    $("#txt_nombre").val(response.solicitud.Nombre);
                    $("#txt_estado").val(response.solicitud.Estado);
                    $("#txt_puerto").val(response.solicitud.Puerto);
                    $("#txt_autorizacion").val(response.solicitud.Autorizacion);                  
                    $("#txt_solicitante").val(response.solicitud.Usuario);
                    $("#txt_locacion").val(response.solicitud.Locacion);
                    $("#txt_nave").val(response.solicitud.Nave);
                    $("#txt_inicio").val(response.solicitud.Inicio);
                    $("#txt_fin").val(response.solicitud.Fin);
                    $("#txt_empresa").val(response.solicitud.Empresa);  
                    $("#txt_pasaportein").val(response.solicitud.Pasaporte);  

                    if (response.solicitud.Estado == "FUERA DE PUERTO" && response.solicitud.Autorizacion == "APROBADO") {
                        //ACTIVO ENTRADA
                        $("#btn_ingreso").removeClass("btn-default");
                        $("#btn_ingreso").removeClass("btn-success")
                        $("#btn_ingreso").addClass("btn-success")
                        //DESACTIVO SALIDA
                        $("#btn_salida").removeClass("btn-default");
                        $("#btn_salida").removeClass("btn-danger");
                        id_aprobado = response.solicitud.Idaprobado;                    
      
                        $("#cbx_vehiculo").html(response.patente);                    
                        $("#btn_modificar").attr("disabled", false)

                    }
                    else if (response.solicitud.Estado == "EN PUERTO") {

                        $("#btn_ingreso").removeClass("btn-default");
                        $("#btn_ingreso").removeClass("btn-success")
                        //DESACTIVO SALIDA
                        $("#btn_salida").removeClass("btn-default");
                        $("#btn_salida").removeClass("btn-danger");
                        $("#btn_salida").addClass("btn-danger");
                        id_aprobado = response.solicitud.Idaprobado;
                        ValidaVehiculo(id_aprobado);
                        $("#cbx_vehiculo").html(response.patente);
                        $("#btn_modificar").attr("disabled", false)
                    }
                    else {

                        $("#btn_ingreso").removeClass("btn-default");
                        $("#btn_ingreso").removeClass("btn-success");                           
                        $("#btn_salida").removeClass("btn-default");
                        $("#btn_salida").removeClass("btn-danger");
                        $("#btn_aprobar").attr("disabled", false);
                        $("#btn_modificar").attr("disabled", true);
                        $("#tabla_aprobadores > tbody").html("")
                        $("#tabla_aprobadores > tbody").html(response.aprobadores)
                        $("#cbx_vehiculo").html("");
                    }                   
                
                }
                else {

                    UltimosMov();
                    //$("#mensaje_error").show(0);
                    //$("#mensaje_error").fadeOut(2500);
                    limpiar();


                }       



            }

            $(".btn-success").bind("click", function () {
                Registro(1);
            })
            $(".btn-danger").bind("click", function () {
                Registro(2);
            })

        },
        failure: function (response) {

            alert(response.d);
        }
    });
    
}

function Registro(tipo) {
    
    var patente = $("#cbx_vehiculo option:selected").val();    
    var json = '{"id":"' + id_aprobado + '", "patente":"' + patente + '", "registro":"' + tipo + '"}'; 
    var me = $(this);


    if (me.data('requestRunning')) {
        return;
    }

    me.data('requestRunning', true);


    $.ajax({
        type: "POST",
        url: "GuardaIngreso",
        data: json,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            if (response.mensaje != "") {
                alerta(response.mensaje);
                return false;
            }
            else {         
                $("#txt_rut").val("");
                $("#mensaje_exito").show(0);
                $("#mensaje_exito").fadeOut(2500);
                limpiar();
            }                   

        },
        failure: function (response) {

            alert(response.d);
            me.data('requestRunning', false);
        },

        complete: function () {
            me.data('requestRunning', false);
        }
    });
    
}

function ValidaVehiculo(idPersonaAprobada) {


    var json = '{"idPersonaAprobada":"' + idPersonaAprobada  + '"}';
  

    $.ajax({
        type: "POST",
        url: "UltimaPatente",
        data: json,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#cbx_vehiculo").val(response.idVehiculo);

        },
        failure: function (response) {

            alert(response.d);
        },

        complete: function () {
        }
    });

}


function limpiar() {
    
    $("#txt_pasaportein").val("");
    $("#txt_solicitud").val("");
    $("#txt_nombre").val("");
    $("#txt_estado").val("");
    $("#txt_autorizacion").val("");
    $("#txt_puerto").val("");
    $("#txt_solicitante").val("");
    $("#txt_locacion").val("");
    $("#txt_nave").val("");
    $("#txt_inicio").val("");
    $("#txt_fin").val("");
    $("#txt_empresa").val("");


    $("#btn_ingreso").removeClass("btn-default");
    $("#btn_ingreso").removeClass("btn-success")
    $("#btn_ingreso").addClass("btn-default");

    $("#btn_salida").removeClass("btn-default");
    $("#btn_salida").removeClass("btn-danger")
    $("#btn_salida").addClass("btn-default");


    $("#cbx_vehiculo").html("");
    $("#btn_aprobar").attr("disabled", true)
    $("#btn_modificar").attr("disabled", true)
    $("#txt_rut").focus();

    $(".btn-success").unbind("click");
    $(".btn-danger").unbind("click");
    $("#btn_ingreso").unbind("click");
    $("#btn_salida").unbind("click");
    
    }

function ObtenerListados() {

    $.ajax({
        type: "POST",
        url: "../Solicitud/CargarListados",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            var respuesta = response;
            if (respuesta.mensaje != '') {
                alerta(respuesta.mensaje);
                return false;
            }
            else {     
                $("#selecttvcs").html(respuesta.vehiculo);             
            }
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}

function UltimosMov() {

    

    var rut = $("#txt_rut").val();
    var pasaporte = "";
    if (rut == "") {
        rut = 0;
        pasaporte = $("#txt_pasaportein").val();

    } else {
        var res = rut.replace("-", "");
        rut = res;
        var final = 0;
        final = rut.length;
        final = final - 1;
        var dv = rut.substring(final, final + 1);
        var res = rut.substring(0, final);
        rut = res;
    }
    var json = '{"rut":"' + rut + '", "pasaporte":"' + pasaporte + '"}'; 
    
    $.ajax({
        type: "POST",
        url: "UltimasSolicitudes",
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

                $("#tabla_ultimas > tbody").html("");
                $("#tabla_ultimas > tbody").html(response.html);        
                $('#title_nombre').html(' LISTADO ÚLTIMAS SOLICITUDES:' + response.nombre);
                $('#ModalUltimo').modal('show');
              
               

            }
        },
        
        failure: function (response) {
            alert(response.d);
        }
    });
    


}
