var eliminar_solicitud;
var eliminar_persona;
var eliminar_patente;

$(document).ready(function () {
    ActualizaListado();

    $('#btn_eliminar').click(function (e) {
        confirma("¿Está seguro de eliminar esta solicitud?", 1)
        eliminar_solicitud = $(this)
    });


    $(".ok1").bind("click", function () {

        var id_solicitud = eliminar_solicitud.parent().parent().parent().attr("data-id")
        var json = '{"id":"' + aux_solicitud + '"}';

        $.ajax({
            type: "POST",
            url: "EliminarSolicutud",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                if (response.mensaje != "") {
                    alerta(response.mensaje)
                }
                else {

                    $('#ModalDetalle').modal('hide');
                    ActualizaListado();
                }

            }
        });


    });


    $(".ok2").bind("click", function () {
        
        var solicitud = aux_solicitud;
        var idpersona = eliminar_persona.parent().parent()["0"].cells[1].innerText;
        var json = '{"id":"' + solicitud + '", "idpersona":"' + idpersona + '"}';
        var sel = eliminar_persona.parent().parent();
        
        $.ajax({
            type: "POST",
            url: "EliminarPersonaMiSolicitud",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                if (response.mensaje != "") {
                    alerta(response.mensaje)
                }
                else {
                    elimine_algo = 1;
                    sel.remove();

                }
            },
            failure: function (response) {
                alert(response);
            }
        });

        

    });
    

    $(".ok3").bind("click", function () {

        var solicitud = aux_solicitud;
        var patente = eliminar_patente.parent().parent()["0"].cells[2].innerText;
        var json = '{"id":"' + solicitud + '", "patente":"' + patente + '"}';
        var sel = eliminar_patente.parent().parent();

        $.ajax({
            type: "POST",
            url: "EliminarPatenteSolicitud",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                if (response.mensaje != "") {
                    alerta(response.mensaje)
                }
                else {
                    elimine_algo = 1;
                    sel.remove();

                }
            },
            failure: function (response) {
                alert(response);
            }
        });        
        
    })
    
    
   });  





function ActualizaListado() {

    //    var idUsuario $("#txtrutcs").val();
    var idUsuario = 0;
    var json = '{"idUsuario":"' + idUsuario + '"}';



    $.ajax({
        type: "POST",
        url: "RecuperaSolicitudes",
        data: json,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            Agregarfilas(response);


            $(".btn_detalle").bind("click", function () {

                var id_solicitud = $(this).parent().parent().parent().attr("data-id")
                var json = '{"id":"' + id_solicitud + '"}';
                elimine_algo = 0;
                aux_solicitud = id_solicitud;

                $.ajax({
                    type: "POST",
                    url: "VerSolicitud",
                    data: json,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        if (response.mensaje != "") {
                            alerta(response.mensaje)
                        }
                        else {
                            //$("#fechainicio").val("2018-03-03T23:00");
                            $("#SelectPuerto").val(response.puerto);
                            $("#fechainicio").val(response.desde);
                            $("#fechatermino").val(response.hasta);
                            $("#SelecttipoIngres").val(response.tipo);
                            $("#SelectEmpresa").val(response.empresa);
                            $("#tabla_persona > tbody").html("")
                            $("#tabla_patente > tbody").html("")
                            $("#tabla_persona > tbody").html(response.persona);
                            $("#tabla_patente > tbody").html(response.patente);
                            $("#id_detalle").html("Detalle Solicitud : " + id_solicitud);
                            $('#ModalDetalle').modal('show');

                            $('#tabla_persona > tbody > tr > td > button').bind("click", function () {
                                eliminar_persona = $(this);
                                confirma("¿Eliminar Persona de Solicitud?",2)
                                
                            });
                            $('#tabla_patente > tbody > tr > td > button').bind("click", function () {
                                eliminar_patente = $(this);
                                confirma("¿Eliminar Vehiculo de Solicitud?", 3)
                              });



                        }

                    }
                });



            })



        },
        failure: function (response) {

            alert(response.d);
        },
        complete: function (response) {


        }

    });




}
