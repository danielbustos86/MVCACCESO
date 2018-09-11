$(document).ready(function () {

    $("#btn_buscar_patente").click(function () {

        $("#selecttvcs").val('0');
        var patente = $("#txtPatentecs").val();
        if (patente=="") {
            alerta("Ingrese Patente");
            return false;
        }
        
        if (BuscaPatenteTabla(patente)) {

            alerta("Patente ya existe en la solicitud actual");
            return false;
        }
        var json = '{"patente":"' + patente + '"}';

        $.ajax({
            type: "POST",
            url: "Helpers/GetVehiculo",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                var respuesta = response;

                if (respuesta.mensaje != "") {
                    alerta(respuesta.mensaje);
                    return false;
                }


                if (respuesta.existe == "0") {
                    $("#txtingresaPatentecs").val(patente);
                    $('#myModal').modal('show'); 
                }
                else {

                    Agrega_Patente(respuesta.vehiculos);
                    $("#txtPatentecs").val("");
                }

            },
            failure: function (response) {

                alerta(response);
            }
        });

    });


    $("#btnguardarcs").click(function (e) {

        var patente = $("#txtingresaPatentecs").val();
        var tipo = $("#selecttvcs").val();
        var opcion_seleccionada = $("#selecttvcs option:selected").text();
        var json = '{"patente":"' + patente + '", "tipo":"' + tipo + '", "descripcion":"' + opcion_seleccionada + '"}'; 


        if (tipo == 0) {
            alerta("Ingrese Tipo Vehiculo");
            return false;
        }
        if (patente == "") {
            alerta("Ingrese Patente");
            return false;
        }

        $.ajax({
            type: "POST",
            url: "Helpers/GuardaVehiculo",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                      
                if (response.mensaje != "") {

                    alerta(response.mensaje);
                    return false;

                }
                else {
                    $('#myModal').modal('hide');
                    $("#txtPatentecs").val(patente);
                    $("#btn_buscar_patente").click();
                    $("#txtingresaPatentecs").val('');
                    $("#selecttvcs").val('0');
                                              
                }
            },
            failure: function (response) {

                alert(response);
            }
        });

    });


});


function Agrega_Patente(patente) {

    
    var body = $('#TABlAVEHICULOCS').find('tbody');
    var row = ""


        row = '<tr>';
        row = row + '<td>' + patente.Id.toString() + '</td>';
        row = row + '<td class =patente_vehiculo>' + patente.Patente.toString() + '</td>';
        row = row + '<td>' + patente.Tipo.toString() + '</td>';
        row = row + '<td><div id="' + patente.Id.toString() + '"><input type="Button" data="' + patente.Patente.toString() + '" value="Quitar" class="quitavehiculo btn btn-default btn-xs" /></div></td>';

        body.append(row);
   

        $('.quitavehiculo').bind('click', function () {
        var fila = $(this);
        fila.parent().parent().parent().remove();

    });
    

}

function BuscaPatenteTabla(patente) {
    var listado_patente = $(".patente_vehiculo");
    var existe = false;
    $(listado_patente).each(function () {
        var patente_aux = $(this)["0"].innerText;
        if (patente_aux == patente) {
            existe = true;
            return false;
        }
    });
    return existe;
}