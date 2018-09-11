var mi_tabla;
var mi_json;
$(document).ready(function () {
      
  
    $.fn.editable.defaults.mode = 'inline';
    $.fn.editable.defaults.ajaxOptions = { type: "post" }
    

    $('#txt_buscar').on('keyup keypress change', function () {      
        var filtro = $("[type=search]");
        filtro.val($("#txt_buscar").val());
        filtro.keyup();
    });




    FillNaves();
    $("#txt_descripcion_naves").keyup(function (event) {

        var descripcion = $("#txt_descripcion_naves").val().toString().toUpperCase();
        if (descripcion == "") {
            $("#tabla_naves tbody tr").show();
        }
        else {
            busquedaAsync(descripcion);
        }
        

    });

    $("#btn_naves").click(function () {



        $.ajax({
            type: "POST",
            url: "../Naves/NavesAPI",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                var respuesta = response;
                if (respuesta.mensaje != '') {
                    alerta(respuesta.mensaje);
                    return false;
                }
                else {

                    if (respuesta.total == 0) {

                        $("#tabla_naves > tbody").html("");

                    }
                    else {

                        $("#tabla_naves > tbody").html("");
                        $("#tabla_naves > tbody").html(respuesta.html);                     
                        $('.sel_activo').bootstrapToggle();
                        $('.sel_activo').bind('change', function () {
                            var accion = "";
                            if ($(this).prop('checked') == true) {

                                accion = 0
                            }
                            else {

                                accion = 1
                            }
                            var nave = $(this).parent().parent().parent()["0"].cells[1].innerText;
                            var json_estado = '{"nave":"' + nave + '", "opcion":"' + accion + '"}';

                            $.ajax({
                                type: "POST",
                                url: " ../Naves/ActualizaEstadoNaves",
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
                                validate: function (value) {
                                    if ($.trim(value) == '') {
                                        return 'Campo Requerido';
                                    }
                                    else {
                                        var aux = mi_json.replace("aux_descripcion", $.trim(value));
                                        $.ajax({
                                            type: "POST",
                                            url: " ../Naves/ActualizaDescripcionNave",
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

                        $(".editar").bind("click", function () {                           
                            var id = $(this).parent().parent()["0"].cells["0"].innerText;
                            mi_json = '{"nave":"' + id + '", "descripcion":"' + "aux_descripcion" +  '"}';

                        });

                
                    }

                    $('#tabla_naves > tbody > tr > td > a > button').bind("click", function () {
                        var fila_seleccionar = $(this);
                        var id = $(this).parent().parent().parent()["0"].cells[1].innerText;
                        var descripcion = $(this).parent().parent().parent()["0"].cells[2].innerText;
                        $("#txt_id").val(id);
                        $("#txt_descripcion").val(descripcion);
                        $("#ModalNaves").modal("hide");

                    });
                }
            },
            failure: function (response) {
                alert(response.d);
            },
            complete: function () {
                $("#ModalNaves").modal("show");
            }
        });
    })    

    $("#btn_guarda_nave").click(function () {

        var id = $("#txt_id").val();
        var descripcion = $("#txt_descripcion").val();

        if (descripcion == "") {
            alerta("Ingrese ID/Descripción");
            return false;
        }
        
        
        var json = '{"id":"' + id + '", "nombre":"' + descripcion  + '"}';

        $.ajax({
            type: "POST",
            url: "../Naves/GuardarNaves",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response.mensaje != "") {
                    alerta(response.mensaje);
                    return false;
                    alert("mensaje");
                }
                else {
             
                    FillNaves();
                  
                    $("#txt_descripcion").val("");
                    $("#txt_id").val("");
                    alerta("Registro Exitoso");
                    window.location.reload(true);
                }
            },
            failure: function (response) {
                alert(response.d);
            }
        });


    });



});


function busquedaAsync(descripcion) {
    setTimeout(BuscarLoc(descripcion), 500);
}


function BuscarLoc(descripcion) {
    if (descripcion == "") {
        $("#tabla_naves tbody tr").show();
    }
    else {
        $("#tabla_naves tbody tr").hide();
        $("#tabla_naves .campoDesc:contains('" + descripcion + "')").parents("tr").show();
    }
}


function FillNaves() {

    

    $.ajax({
        type: "POST",
        url: "../Naves/GetNavesPrincipal",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {


            if (response.mensaje != "") {
                alerta(response.mensaje);
    

                return false;
                
            }
            else {
        
                $("#tabla_naves_sel > tbody").html("");
                $("#tabla_naves_sel > tbody").html(response.html);



                $('.sel_activo').bootstrapToggle();
                $('.sel_activo').bind('change', function () {
                    var accion = "";
                    if ($(this).prop('checked') == true) {

                        accion = 0
                    }
                    else {

                        accion = 1
                    }
                    var nave = $(this).parent().parent().parent()["0"].cells[0].innerText;
                    var json_estado = '{"nave":"' + nave + '", "opcion":"' + accion + '"}';

                    $.ajax({
                        type: "POST",
                        url: " ../Naves/ActualizaEstadoNaves",
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
                        validate: function (value) {
                            if ($.trim(value) == '') {
                                return 'Campo Requerido';
                            }
                            else {
                                var aux = mi_json.replace("aux_descripcion", $.trim(value));
                                $.ajax({
                                    type: "POST",
                                    url: " ../Naves/ActualizaDescripcionNave",
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
                $(".editar").bind("click", function () {                  
                    var id = $(this).parent().parent()["0"].cells["0"].innerText;
                    mi_json = '{"nave":"' + id + '", "descripcion":"' + "aux_descripcion" + '"}';

                });
                               



                $("#tabla_naves_sel").dataTable().fnDestroy()              
                $('#tabla_naves_sel').DataTable({
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
                    }, "lengthMenu": [[30], [30]], searching: true, "ordering": false, "bLengthChange": false, info: false, "initComplete": function () {
                        $("#tabla_naves_sel_filter").hide(0)
                    }
                });
                $('.sel_activo').bootstrapToggle();             
               

            }
        },        
        failure: function (response) {
            alert(response.d);
        }
    });



}