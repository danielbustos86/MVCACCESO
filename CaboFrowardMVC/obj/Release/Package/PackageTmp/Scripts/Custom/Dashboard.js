var carga_sol = 0


$(document).ready(function () {
    actualiza_listados();
    //setTimeout(actualiza_listados, 10000);

    $("#tabla_vehiculos").hide(0);
    $("#tabla_ingresos").hide(0);
    $("#tabla_solicitudes").hide(0);

    $('#SelectPuerto').on('change', function () {

    
        actualiza_listados();
    
        $("#tabla_ingresos").hide(0);
        $("#tabla_solicitudes").hide(0);

        $("#tabla_solicitudes > tbody").html("");
        $("#tabla_ingresos > tbody").html("");
    });

    $("#div_vehiculos").click(function () {

      
        if ($("#tabla_vehiculos").is(":visible")) {
            $("#tabla_vehiculos").hide(0);
        }
        else {

            $("#tabla_solicitudes").hide(0);
            $("#tabla_ingresos").hide(0);
            $("#tabla_vehiculos").show(0);
            get_vehiculos();    

        }

    });

    $("#div_ingresos").click(function () {

        get_ingresos();  

        //if ($("#tabla_ingresos").is(":visible")) {
        //    $("#tabla_ingresos").hide(0);
        //}
        //else {
         
        //    $("#tabla_solicitudes").hide(0);
        //    //$("#tabla_vehiculos").hide(0);
        //    $("#tabla_ingresos").show(0);

        //}



    });

    $("#div_solicitudes").click(function () {
        get_solicitudes();

        //if ($("#tabla_solicitudes").is(":visible")) {
        //    $("#tabla_solicitudes").hide(0);
        //}
        //else {
        //    //$("#tabla_vehiculos").hide(0);

        //    $("#tabla_ingresos").hide(0);
        //    $("#tabla_solicitudes").show(0);
      
        //}
      

     
    });

});




function actualiza_listados() {  


    var puerto = $("#SelectPuerto").val();
    if (puerto == null) {
        puerto = 0;
    }

    var json = '{"puerto":"' + puerto + '"}';
    
    $.ajax({
        type: "POST",
        url: "../Menu/Resumen",        
        data: json,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            if (response.mensaje != "") {
                alerta(response.mensaje);
                return false;
            }
            else {
                $("#res_vehiculos").html(response.vehiculos);
                $("#res_solicitudes").html(response.solicitudes);
                $("#res_ingresos").html(response.ingresos);              
            }
        },
        failure: function (response) {

            alert(response.d);
        }
    });
    return false;
}



function get_solicitudes() {



    var puerto = $("#SelectPuerto").val();
    if (puerto == null) {
        puerto = 0;
    }

    var json = '{"puerto":"' + puerto + '"}';

    //$('#tabla_solicitudes_paginate').hide(0);
    $('#tabla_ingresos_paginate').hide(0);
    $('#tabla_vehiculos_paginate').hide(0);
    $("#tabla_ingresos").hide(0);
    $("#tabla_solicitudes").show(0);
    
    $.ajax({
        type: "POST",
        url: "../Menu/GetSolicitudesDia",
        data: json,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#tabla_solicitudes > tbody").html("");

            if (response.mensaje != "") {
                
                alerta(response.mensaje);
                return false;
            }
            else {
                $("#tabla_solicitudes > tbody").html("");
                $("#tabla_solicitudes > tbody").html(response.html);

                //$("#tabla_solicitudes").dataTable().fnDestroy()
                //$('#tabla_solicitudes').DataTable({ 'language': { 'url': '../content/DatatableUtil/Spanish.json' }, "lengthMenu": [[30], [30]], searching: false, "ordering": false, "bLengthChange": false, info: false });

          
            }
        },
        failure: function (response) {

            alert(response.d);
        }
    });
    return false;
}

function get_vehiculos() {

    $('#tabla_solicitudes_paginate').hide(0);
    $('#tabla_ingresos_paginate').hide(0);
    //$('#tabla_vehiculos_paginate').hide(0);
    $.ajax({
        type: "POST",
        url: "../Menu/GetVehiculosDia",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            if (response.mensaje != "") {
                alerta(response.mensaje);
                return false;
            }
            else {
                $("#tabla_vehiculos > tbody").html("");
                $("#tabla_vehiculos > tbody").html(response.html);

             
                $("#tabla_vehiculos").dataTable().fnDestroy()
                $('#tabla_vehiculos').DataTable({ 'language': { 'url': '../content/DatatableUtil/Spanish.json' }, "lengthMenu": [[30], [30]], searching: false, "ordering": false, "bLengthChange": false, info: false });
                  
            
            }
        },
        failure: function (response) {

            alert(response.d);
        }
    });
    return false;
}

function get_ingresos() {
    var puerto = $("#SelectPuerto").val();
    if (puerto == null) {
        puerto = 0;
    }

    var json = '{"puerto":"' + puerto + '"}';
    $("#tabla_solicitudes").hide(0);


    $('#tabla_solicitudes_paginate').hide(0);
    //$('#tabla_ingresos_paginate').hide(0);
    $('#tabla_vehiculos_paginate').hide(0);
    $.ajax({
        type: "POST",
        url: "../Menu/GetIgresosDia",
        data: json,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            $("#tabla_ingresos > tbody").html("");

            if (response.mensaje != "") {
                alerta(response.mensaje);
                return false;
            }
            else {

      
                $("#tabla_ingresos > tbody").html("");
                $("#tabla_ingresos > tbody").html(response.html);

                //$("#tabla_ingresos").dataTable().fnDestroy()
                //$('#tabla_ingresos').DataTable({ 'language': { 'url': '../content/DatatableUtil/Spanish.json' }, "lengthMenu": [[30], [30]], searching: false, "ordering": false, "bLengthChange": false, info: false });
                $("#tabla_ingresos").show(0);
            }
        },
        failure: function (response) {

            alert(response.d);
        }
    });
    return false;
}





