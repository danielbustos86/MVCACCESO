$(document).ready(function () {


    $("#btn_nave").click(function () {
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
        if ($("#txtcodigoNavecs").val().length > 0) {
            CODIGO = $("#txtcodigoNavecs").val();
        }        
        var NOMBRE = $("#txtNombreNavecs").val();
        var ID_PUERTO = $("#SelectPuerto").val();
        var json = '{"codigo":' + CODIGO + ', "nombre":"' + NOMBRE + '", "puerto":' + ID_PUERTO + '}';
        
        $.ajax({
            type: "POST",
            url: "Helpers/GetNavesActivas",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                if (response.mensaje != "") {
                    alerta(response.mensaje);
                    return false;

                }
                else {

                    $("#tablaNaveCs").html("")
                    $("#tablaNaveCs").append(response.html)
                    $("#tablaNaveCs").dataTable().fnDestroy()
                    $('#tablaNaveCs').DataTable({
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

                    $('#ModalNave').modal('show');

                    $('#tablaNaveCs > tbody > tr > td > a > button').bind("click", function () {
                        var fila_seleccionar = $(this);



                        var id = $(this).parent().parent().parent()["0"].cells[1].innerText;
                        var descripcion = $(this).parent().parent().parent()["0"].cells[2].innerText;
                        

                        //$(".fill_locacion_des", $(".sel_chk:checked").parent().parent()).html("");
                        //$(".fill_locacion_id", $(".sel_chk:checked").parent().parent()).html("");


                        $(".fill_nave_des", $(".sel_chk:checked").parent().parent()).html(descripcion);
                        $(".fill_nave_id", $(".sel_chk:checked").parent().parent()).html(id);
                        limpiar();
                        $('#ModalNave').modal('hide');
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


    $("#btnbuscarnave").click(function () {

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
            if ($("#txtcodigoNavecs").val().length > 0) {
                CODIGO = $("#txtcodigoNavecs").val();
            }
            var NOMBRE = $("#txtNombreNavecs").val();
            var ID_PUERTO = $("#SelectPuerto").val();
            var json = '{"codigo":' + CODIGO + ', "nombre":"' + NOMBRE + '", "puerto":' + ID_PUERTO + '}';

            $.ajax({
                type: "POST",
                url: "Helpers/GetNaves",
                data: json,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {

                    if (response.mensaje != "") {
                        alerta(response.mensaje);
                        return false;

                    }
                    else {

                        $("#tablaNaveCs").html("")
                        $("#tablaNaveCs").append(response.html)
                        $("#tablaNaveCs").dataTable().fnDestroy()
                        $('#tablaNaveCs').DataTable({
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

                        $('#tablaNaveCs > tbody > tr > td > a > button').bind("click", function () {
                            var fila_seleccionar = $(this);



                            var id = $(this).parent().parent().parent()["0"].cells[1].innerText;
                            var descripcion = $(this).parent().parent().parent()["0"].cells[2].innerText;


                            $(".fill_locacion_des", $(".sel_chk:checked").parent().parent()).html("");
                            $(".fill_locacion_id", $(".sel_chk:checked").parent().parent()).html("");


                            $(".fill_nave_des", $(".sel_chk:checked").parent().parent()).html(descripcion);
                            $(".fill_nave_id", $(".sel_chk:checked").parent().parent()).html(id);
                            $('#ModalNave').modal('hide');
                        });

                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });

    });

});
