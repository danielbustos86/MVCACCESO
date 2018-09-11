
$(document).ready(function () {

    //document.getElementById("fechainicioNombrada").value = today;

    var date = new Date();

    var day = date.getDate();
    var month = date.getMonth() + 1;
    var year = date.getFullYear();

    if (month < 10) month = "0" + month;
    if (day < 10) day = "0" + day;

    var today = year + "-" + month + "-" + day;


    document.getElementById('fechainicioNombrada').value = today;

    $("#btncargarNombrada").click(function () {

        var fecha = $("#fechainicioNombrada").val();
        var turno = $("#SelectTurno").val();


        var json = '{"fecha":"' + fecha + '", "turno":"' + turno  + '"}';

        $.ajax({
            type: "POST",
            url: "RecuperaNombrada",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                if (response.mensaje != "") {
                    alerta(response.mensaje);
                    return false;
                }


                else {
                    $("#TablaNombrada > tbody").html("");
                    $("#TablaNombrada > tbody").html(response.nombrada);
     
                }
                //    
                //    $("#cbx_vehiculo").html(response.patente);
                //    $('#ModalPatente').modal('hide');
                //}
            },
            failure: function (response) {

                alert(response.d);
            }
        });                      
    });  


    $("#btnBuscaNombrada").click(function () {

        var fecha = $("#fechainicioNombrada").val();
        var turno = $("#SelectTurno").val();


        var json = '{"fecha":"' + fecha + '", "turno":"' + turno + '"}';

        $.ajax({
            type: "POST",
            url: "GetNombradas",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                if (response.mensaje != "") {
                    alerta(response.mensaje);
                    return false;
                }


                else {
                    $("#TablaNombrada > tbody").html("");
                    $("#TablaNombrada > tbody").html(response.html);

                }
                //    
                //    $("#cbx_vehiculo").html(response.patente);
                //    $('#ModalPatente').modal('hide');
                //}
            },
            failure: function (response) {

                alert(response.d);
            }
        });
    }); 

});