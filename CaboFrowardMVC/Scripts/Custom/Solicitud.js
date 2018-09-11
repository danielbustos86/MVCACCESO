var aux_solicitud;
var elimine_algo;
var carga = 0;

$(document).ready(function () {


	$(".select2-single, .select2-multiple").select2({
		theme: "bootstrap",
		placeholder: "Seleccionar",
		maximumSelectionSize: 6,
		containerCssClass: ':all:'
	});

    $('#fechainicio').datetimepicker({
        timepicker: true,
        mask: true, // '9999/19/39 29:59' - digit is the maximum possible for a cell
    });

    $('#fechatermino').datetimepicker({
        timepicker: true,
        mask: true, // '9999/19/39 29:59' - digit is the maximum possible for a cell
    });

	
    $('#SelecttipoIngres').on('change', function () {


        //if (this.value == 146) {
       
        //    var texto_de_option = '96723320-K';
        //    $("#SelectEmpresa").find('option:contains("' + texto_de_option + '")').prop('selected', true);
        //} 
    });




    $("#btn_busca_emp").click(function (e) {

        $("#RutBuscador").val('');
        $("#NombreEmpresaBusca").val('');
        $('#ModalEMpresa').modal('show');
        BuscarEmpresa();
        

    });

    $("#BtnBuscarEmpresa").click(function (e) {


        BuscarEmpresa(); 



    });

    

    $("#btnListarSol").hide();
    $("#mensajeok").hide(0);

    var fecha = MaxDate();
    $("#fechainicio").attr("min", fecha);
    
    $('#selectAll').click(function (e) {
        var table = $(e.target).closest('table');
        $('td input:checkbox', table).prop('checked', this.checked);
    });
        
    $("#btn_close_ver").click(function () {

        if (elimine_algo > 0) {                 
            ActualizaListado();
        }

    });
                   
    $("#btncrearsoli").click(function () {
        var perxml;
        perxml = "<personas>";
        var rut;
        var locacion;
        var nave;


        var tableID = "TABlAPERSONACS";
        var table = document.getElementById(tableID);
        
        for (var i = 1; i < table.rows.length; i++) {
            rut = table.rows[i].cells[1].innerText;
            locacion = table.rows[i].cells[6].innerText;
            nave = table.rows[i].cells[7].innerText;
            if (locacion == "") {
                locacion = 0
            }

            if (nave == "") {
                nave = 0;
            }

            perxml += "<persona perid='" + rut + "' locacion='" + locacion + "' nave='" + nave + "'>";
            perxml += "</persona>";
        }
        perxml += "</personas>"
        
        var tableID = "TABlAVEHICULOCS";
        var table = document.getElementById(tableID);
        var patente;
        var vehiculoxml;
        vehiculoxml = "<vehiculos>";

        for (var i = 1; i < table.rows.length; i++) {
            patente = table.rows[i].cells[1].innerText;
            vehiculoxml += "<vehiculo patente='" + patente + "'></vehiculo>";
        }

        vehiculoxml += "</vehiculos>";
        var puerto = $("#SelectPuerto").val();
        var fechainicio = $("#fechainicio").val();
        var fechafin = $("#fechatermino").val();
        var tingreso = $("#SelecttipoIngres").val();
        var empresa = $("#SelectEmpresa").val();
      

        var json = '{"puerto":"' + puerto + '", "fechainicio":"' + fechainicio + '", "fechafin":"' + fechafin + '","tingreso":"' + tingreso + '", "empresa":"' + empresa + '", "perxml":"' + perxml + '", "vehiculoxml":"' + vehiculoxml + '"}';
     
        $.ajax({
            type: "POST",
            url: "Solicitud/GuadarSolicitud",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                
                if (response.mensaje!="") {
                    alerta(response.mensaje)

                }
                else {

                    if (response.html != "") {
                        
                        $("#tablaProblema").html("")
                        $("#tablaProblema").append(response.html)
                        $('#ModalProblemas').modal('show');

                        
                        $('#tablaProblema > tbody > tr > td > a > button').bind("click", function () {                            
                            EliminaPersona($(this).parent().parent().parent()["0"].cells[3].innerText);
                            $(this).parent().parent().parent().remove();
                            $('#ModalLocacion').modal('hide');
                        });
                    }
                    else {

                        $('#mensajeok').show();
                        $("#mensajeok").fadeOut(3000);
                        setTimeout('document.location.reload()', 2000);
                                           
                    }                                                         

                }
            }
           
        });
    })




});

function BuscarEmpresa() {
    var rut = $("#RutBuscador").val();
    var nombre = $("#NombreEmpresaBusca").val();

    if (rut == "") {

        rut = 0;
    }

    var json = '{"rut":' + rut + ', "nombre":"' + nombre + '"}';

   
    $.ajax({
        type: "POST",
        url: "Helpers/GetEmpresasActivas",
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

function MaxDate() {
    var d = new Date();
    var month = d.getMonth() + 1;
    var day = d.getDate();
    var output = d.getFullYear() + '-' +
        (('' + month).length < 2 ? '0' : '') + month + '-' +
        (('' + day).length < 2 ? '0' : '') + day;
    return output;
}

function ActualizaListado() {
       
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
                                    }
                                   
                                });                              
                            });                            
                        }
                    }
                });                                
            })            
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
    $("#id_total_listado").html("Listar Solicitud(" + contador +")") 
    if (carga == 0) {
        $('#TablaListadosolic').DataTable({
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
            }, "lengthMenu": [[30], [30]], searching: false, "ordering": false, "bLengthChange": false, info: false
        });
        carga = 1;
    }
  
}

function EliminaPersona(rut) {
    var listado_rut = $(".rut_persona");
    var existe = false;
    $(listado_rut).each(function () {
        var rut_td = $(this)["0"].innerText;
        if (rut_td == rut) {
            $(this).parent().remove();  
            return false;
        }
    });



    //$('#SelectEmpresa').selectize({
    //    create: true,
    //    sortField: 'text'
    //});



}
