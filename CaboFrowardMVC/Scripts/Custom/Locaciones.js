
$(document).ready(function () {

    $("#btn_locacion").click(function () {
        var ID_PUERTO = $("#SelectPuerto").val();

        if (ID_PUERTO == 0) {
            alerta("Debe seleccionar un puerto");
            return false;
        }

        if ($(".sel_chk:checked").length == 0) {
            alerta("Debe seleccionar personas");
            return false;
        }

        var CODIGO = 0;
        if ($("#CodigoLocacionCS").val().length > 0) {
            CODIGO = $("#CodigoLocacionCS").val();
        }

        var NOMBRE = $("#Nombrelocacioncs").val();
        var ID_PUERTO = $("#SelectPuerto").val();
        var json = '{"codigo":' + CODIGO + ', "nombre":"' + NOMBRE + '", "puerto":' + ID_PUERTO + '}';


        $.ajax({
            type: "POST",
            url: "Helpers/GetLocacion",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {


                if (response.mensaje != "") {
                    alerta(response.mensaje);
                    return false;

                }
                else {


                    $("#tablalocacionescs").html("")
                    $("#tablalocacionescs").append(response.html)
                    $("#tablalocacionescs").dataTable().fnDestroy()
                    $('#tablalocacionescs').DataTable({
                        'language': {
                            "sProcessing": "Procesando...",
                            "sLengthMenu": "Mostrar _MENU_ registros",
                            "sZeroRecords": "No se encontraron resultados",
                            "sEmptyTable": "Ningún dato disponible en esta tabla",
                            "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                            "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                            "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                            "sInfoPostFix": "",
                            "sSearch": "Buscar:",
                            "sUrl": "",
                            "sInfoThousands": ",",
                            "sLoadingRecords": "Cargando...",
                            "oPaginate": {
                                "sFirst": "Primero",
                                "sLast": "Último",
                                "sNext": "Siguiente",
                                "sPrevious": "Anterior"
                            },
                            "oAria": {
                                "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                                "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                            }
                        }, "lengthMenu": [[5], [5]], searching: false, "ordering": false, "bLengthChange": false, info: false
                    });

                    $('#ModalLocacion').modal('show');

                    $('#tablalocacionescs > tbody > tr > td > a > button').bind("click", function () {
                        var fila_seleccionar = $(this);
                        var id = $(this).parent().parent().parent()["0"].cells[1].innerText;
                        var descripcion = $(this).parent().parent().parent()["0"].cells[2].innerText;
                        $(".fill_locacion_des", $(".sel_chk:checked").parent().parent()).html(descripcion);
                        $(".fill_locacion_id", $(".sel_chk:checked").parent().parent()).html(id);
                        //$(".fill_nave_id", $(".sel_chk:checked").parent().parent()).html("");

                        //$(".fill_nave_des", $(".sel_chk:checked").parent().parent()).html("");
                        limpiar();
                        $('#ModalLocacion').modal('hide');

                    });

                }
            },
            failure: function (response) {

                alert(response.d);
            }
        });


    });

    function limpiar() {
        inputs = $('table').find('input').filter('[type=checkbox]');
        $("#txtcodigoNavecs").val('');
        $("#txtNombreNavecs").val('');
        if ($(this).attr("checked")) {
            inputs.attr('checked', true);
        }
        else {
            inputs.attr('checked', false);
        }

    };


    $("#btn_buscarloca").click(function () {


        var CODIGO = 0;
        if ($("#CodigoLocacionCS").val().length > 0) {
            CODIGO = $("#CodigoLocacionCS").val();
        }

        var NOMBRE = $("#Nombrelocacioncs").val();
        var ID_PUERTO = $("#SelectPuerto").val();
        var json = '{"codigo":' + CODIGO + ', "nombre":"' + NOMBRE + '", "puerto":' + ID_PUERTO + '}';


        $.ajax({
            type: "POST",
            url: "Helpers/GetLocacion",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {


                if (response.mensaje != "") {
                    alerta(response.mensaje);
                    return false;

                }
                else {


                    $("#tablalocacionescs").html("")
                    $("#tablalocacionescs").append(response.html)
                    $('#ModalLocacion').modal('show');

                    $('#tablalocacionescs > tbody > tr > td > a > button').bind("click", function () {
                        var fila_seleccionar = $(this);
                        var id = $(this).parent().parent().parent()["0"].cells[1].innerText;
                        var descripcion = $(this).parent().parent().parent()["0"].cells[2].innerText;

                        $(".fill_locacion_des", $(".sel_chk:checked").parent().parent()).html(descripcion);
                        $(".fill_locacion_id", $(".sel_chk:checked").parent().parent()).html(id);

                        $(".fill_nave_des", $(".sel_chk:checked").parent().parent()).html("");
                        $(".fill_nave_id", $(".sel_chk:checked").parent().parent()).html("");


                        $('#ModalLocacion').modal('hide');
                    });

                }
            },
            failure: function (response) {

                alert(response.d);
            }
        });

        




    });






});

