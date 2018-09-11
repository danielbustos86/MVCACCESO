$(document).ready(function () {

    //$('#SelectPuerto option')[1].selected = true;
    CargarLocaciones();

    $("#btn_busca_empcm").click(function (e) {

        $("#RutBuscador").val('');
        $("#NombreEmpresaBusca").val('');
        $('#ModalEMpresa').modal('show');
        BuscarEmpresa();


    });


    function BuscarEmpresa() {
        var rut = $("#RutBuscador").val();
        var nombre = $("#NombreEmpresaBusca").val();

        if (rut == "") {

            rut = 0;
        }

        var json = '{"rut":' + rut + ', "nombre":"' + nombre + '"}';

        alert(json);
        $.ajax({
            type: "POST",
            url: "../Helpers/GetEmpresasActivas",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                if (response.mensaje != "") {
                    alerta(response.mensaje);
                    return false;

                }
                else {

                    $("#tablaEmpresaCs").html("")
                    $("#tablaEmpresaCs").append(response.html)
                    $("#tablaEmpresaCs").dataTable().fnDestroy()
                    $('#tablaEmpresaCs').DataTable({
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

                    $('#ModalEMpresa').modal('show');

                    $('#tablaEmpresaCs > tbody > tr > td > a > button').bind("click", function () {
                        var fila_seleccionar = $(this);



                        var id = $(this).parent().parent().parent()["0"].cells[1].innerText;

                        $('#SelectEmpresa').val(id);
                        ////$(".fill_locacion_des", $(".sel_chk:checked").parent().parent()).html("");
                        ////$(".fill_locacion_id", $(".sel_chk:checked").parent().parent()).html("");


                        //$(".fill_nave_des", $(".sel_chk:checked").parent().parent()).html(descripcion);
                        //$(".fill_nave_id", $(".sel_chk:checked").parent().parent()).html(id);
                        //limpiar();
                        $('#ModalEMpresa').modal('hide');
                    });

                }
            },
            failure: function (response) {
                alert(response.d);
            }
        });



    }


});



function CargarLocaciones() {


    var puerto = 5655;
    var json = '{"puerto":' + puerto + '}';

    $.ajax({
        type: "POST",
        url: "../CargasMasivas/CargarSelect",
        contentType: "application/json; charset=utf-8",
        data: json,
        dataType: "json",
        success: function (response) {

            var respuesta = response;
            if (respuesta.mensaje != '') {
                alerta(respuesta.mensaje);
                return false;
            }
            else {

                
                $("#Seleclocacioncm").html(respuesta.locaciones);
                ////cargamos los combo box //
                //$("#SelectPuerto").html(respuesta.puerto);
                //$("#SelectEmpresa").html(respuesta.empresa);


                ////$('#SelectEmpresa').selectize({
                ////    create: true,
                ////    sortField: {
                ////        field: 'text',
                ////        direction: 'asc'
                ////    },
                ////    dropdownParent: 'body'
                ////});
                //$("#SelecttipoIngres").html(respuesta.tipo_ingreso);
                //$("#selectnac").html(respuesta.nacion);
                //$("#selecttvcs").html(respuesta.vehiculo);
                //$("#SelectPuerto").focus();
            }
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}