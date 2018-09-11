var aux_solicitud;
var aprobe_algo;
var carga = 0;
var seleccionado;
$(document).ready(function () {

    
    if ($("#txt_acceso").val() == "SIN_ACCESO") {

        ActualizaListado();
        $('#selectAll').click(function (e) {
            $(".sel_chk:enabled").prop("checked", this.checked)
        });
        $("#btn_close_ver").click(function () {

            if (aprobe_algo > 0) {
                ActualizaListado();
            }
        });
        $('#btn_aprobar').click(function (e) {

            if ($(".sel_chk:checked").length == 0) {
                alerta("No existen registros seleccionados");
                return false;
            }
            var rut;
            rut = "";
            var seleccionados = $(".sel_chk:checked");
            $(seleccionados).each(function () {
                rut = rut + "/" + $(this).parent().parent()["0"].cells[1].innerText;
            });

            var json = '{"id":"' + aux_solicitud + '", "rut":"' + rut + '"}';


            $.ajax({
                type: "POST",
                url: "AprobarSolictud",
                data: json,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (response.mensaje != "") {
                        alerta(response.mensaje);
                        return false;
                    }
                    else {
                        aprobe_algo = 1;
                        $(seleccionado).click();
                    }
                },
                failure: function (response) {

                    alert(response.d);
                }
            });

        });
    }


        

});



function ActualizaListado() {

    
    var idUsuario = 0;
    var json = '{"idUsuario":"' + idUsuario + '"}';



    $.ajax({
        type: "POST",
        url: "ListadoSolAprobar",
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
                seleccionado = $(this);

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
                                var solicitud = aux_solicitud;
                                var rut = $(this).parent().parent()["0"].cells[1].innerText;
                                var json = '{"id":"' + solicitud + '", "rut":"' + rut + '"}';
                                var sel = $(this).parent().parent();

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
                            $('#tabla_patente > tbody > tr > td > button').bind("click", function () {

                                var solicitud = aux_solicitud;
                                var patente = $(this).parent().parent()["0"].cells[2].innerText;
                                var json = '{"id":"' + solicitud + '", "patente":"' + patente + '"}';
                                var sel = $(this).parent().parent();

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



function Agregarfilas(jsonAcopio) {
    var data = jsonAcopio;
    var body = $('#TablaListadosolic').find('tbody');
    var row = ""
    var contador = 0;
    $("#TablaListadosolic > tbody").html("");
    $.each(data, function (i, Datos) {
        row = '<tr data-id=' + Datos.Id.toString() + '>';
        row = row + '<td>' + Datos.Id.toString() + '</td>';
        row = row + '<td>' + Datos.FechaCreacion.toString() + '</td>';
        row = row + '<td>' + Datos.Empresa.toString() + '</td>';
        row = row + '<td>' + Datos.Desde.toString() + '</td>';
        row = row + '<td>' + Datos.Hasta.toString() + '</td>';
        row = row + '<td ALIGN="CENTER" >' + Datos.Canttotalpersona.toString() + '</td>';
        row = row + '<td ALIGN="CENTER" >' + Datos.Cantaprobados.toString() + '</td>';
        row = row + "<td ALIGN='CENTER'><a href='javaScript: void (0)'><button type='button' class='btn_detalle'>" + "Ver Detalle" + "</button></a></td>";
        body.append(row);
        contador = contador + 1
    });
    $("#id_total_listado").html("Listar Solicitud(" + contador + ")")
    if (carga == 0) {
        $('#TablaListadosolic').DataTable({ 'language': { 'url': '../content/DatatableUtil/Spanish.json' }, "lengthMenu": [[10], [10]], searching: false, "ordering": false, "bLengthChange": false, info: false });
        carga = 1;
    }

}
