

$(document).ready(function () {

	
	
	$('#ID_NACIONALIDAD').val("39");


    function validaRut(campo) {
        if (campo.length == 0) { return false; }
        if (campo.length < 8) { return false; }

        campo = campo.replace('-', '')
        campo = campo.replace(/\./g, '')

        var suma = 0;
        var caracteres = "1234567890kK";
        var contador = 0;
        for (var i = 0; i < campo.length; i++) {
            u = campo.substring(i, i + 1);
            if (caracteres.indexOf(u) != -1)
                contador++;
        }
        if (contador == 0) { return false }

        var rut = campo.substring(0, campo.length - 1)
        var drut = campo.substring(campo.length - 1)
        var dvr = '0';
        var mul = 2;

        for (i = rut.length - 1; i >= 0; i--) {
            suma = suma + rut.charAt(i) * mul
            if (mul == 7) mul = 2
            else mul++
        }
        res = suma % 11
        if (res == 1) dvr = 'k'
        else if (res == 0) dvr = '0'
        else {
            dvi = 11 - res
            dvr = dvi + ""
        }
        if (dvr != drut.toLowerCase()) { return false; }
        else { return true; }
    }
    $("#btn_busca_pasp").click(function (e) {

        var pasaporte = "";
        pasaporte = $("#txtpasaporte1").val();



        if (pasaporte == "") {
            alerta("Ingrese pasaporte")
            return;
        }

       
        var json = '{"pasaporte":"' + pasaporte + '"}';

        $.ajax({
            type: "POST",
            url: "Helpers/GetPersonapasaporte",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                var respuesta = response;

                if (respuesta.mensaje != "") {
                    alerta(mensaje)
                    return false;
                }


                if (respuesta.existe == "0") {
                    LimpiaModalPersona();
                    $("#txtpasaporte").val(pasaporte);

                    $('#ModalPersonacs').modal('show');
                }
                else {

                    Agrega_Persona(respuesta.personal);
                    $("#txtpasaporte1").val("");
                }

            },
            failure: function (response) {

                alert(response.d);
            }
        });

    });

    function BuscaRut1() {

        var rut = "";
        var res = "";
        rut = $("#txtrutcs").val();
        var aux = rut.replace("&", "");
        rut = aux;
        aux = rut.replace("-", "");
        rut = aux;
        res = rut.replace(" ", "");
        rut = res;
        res = rut.replace("'", "");
        rut = res;
        res = rut.replace("/", "");
        rut = res;
        if (validaRut(rut) == false) {
            alerta('rut con formato invalido');
            return;
        }
        res = rut.replace("-", "");
        $("#txtrutcs").val(res);
        rut = res;
        var final = 0;
        final = rut.length;
        final = final - 1;
        var dv = rut.substring(final, final + 1);
        var res = rut.substring(0, final);
        rut = res;


        if (rut == "") {
            alerta("Ingrese Rut Persona")
        }

        if (BuscaPersonaTabla(rut)) {

            alerta("Persona ya existe en la solicitud actual");
            return false;

        }
        var json = '{"rut":"' + rut + '"}';

        $.ajax({
            type: "POST",
            url: "Helpers/GetPersona",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                var respuesta = response;

                if (respuesta.mensaje != "") {
                    alerta(mensaje)
                    return false;
                }


                if (respuesta.existe == "0") {

                    LimpiaModalPersona();
                    $("#txtpersonarutcs").val(rut);
                    $("#txtdvcs").val(dv);
                    $('#ModalPersonacs').modal('show');
                }
                else {


                    if (respuesta.personal.Inactivo) {

                        alerta('Persona se encuentra Inactiva');
                        return;
                    }

                    Agrega_Persona(respuesta.personal);

                    $("#txtrutcs").val("");
                }

            },
            failure: function (response) {

                alert(response.d);
            }
        });

    }
    $("#btn_busca_rut").click(function (e) {

        BuscaRut1();
    });

    $('#txtrutcs').keypress(function (e) {

        if (e.which == 13) {
            BuscaRut1();
            return false;
        }
    });

    $(".quitapersona").click(function (e) {

        var fila = $(this);
        fila.Remove();

    });


    $("#btnguardarpercs").click(function (e) {

        var NACIONALIDAD = $("#selectnac").val();
        var RUT = $("#txtpersonarutcs").val();
        var DV = $("#txtdvcs").val();
        var NOMBRE = $("#txtnombrecs").val();
        var APELLIDOPAT = $("#txtapepatercs").val();
        var APELLIDOMAT = $("#txtapematcs").val();
        var PASAPORTE = $("#txtpasaporte").val();
		var rutcompleto = RUT + DV;
		var FECHAINDUCCION = $("#txtfechaInduccion").val();

        if (RUT == "") {
            RUT = 0;
        }
		var json = '{"nacionalidad":"' + NACIONALIDAD + '", "rut":"' + RUT + '", "dv":"' + DV + '","nombre":"' + NOMBRE + '", "paterno":"' + APELLIDOPAT + '", "materno":"' + APELLIDOMAT + '", "pasaporte":"' + PASAPORTE + '", "fechainduccion":"' + FECHAINDUCCION + '"}';

        $.ajax({
            type: "POST",
            url: "Helpers/GuardaPersona",
            data: json,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                if (response.mensaje != "") {
                    alerta(response.mensaje)
                    return false;
                } else {

                    if (RUT == 0) {
                        $("#btn_busca_pasp").click();
                        $('#ModalPersonacs').modal('hide');
                        
                    } else
                    {
                    $("#txtrutcs").val(rutcompleto);
                    $("#btn_busca_rut").click();
                    $('#ModalPersonacs').modal('hide');
                    $("#txtrutcs").val("");                  
                    }    

                    }
            },
            failure: function (response) {
                alert(response)
            }
        });


    });


    
     
        
});

function LimpiaModalPersona() {
    $("#selectnac").val(39);
    $("#txtpersonarutcs").val("");
    $("#txtdvcs").val("");
    $("#txtnombrecs").val("");
     $("#txtapepatercs").val("");
    $("#txtapematcs").val("");
	$("#txtpasaporte").val("");
	$("#txtfechaInduccion").val("");
}
function Agrega_Persona(personal) {

 

        var contador = 0;
    var body = $('#TABlAPERSONACS').find('tbody');
    var row = ""      
    row = '<tr>';
    row = row + '<td><input class="sel_chk" type="checkbox" name="miCheck"/></td>';
    row = row + '<td class = id_persona>' + personal.Id.toString() + '</td>';
    row = row + '<td class = rut_persona>' + personal.Rut.toString() + '</td>';
    row = row + '<td>' + personal.Nombre.toString() + ' ' + personal.Apellido.toString() + '</td>';
    row = row + '<td class="fill_locacion_des"></td>';
    row = row + '<td class="fill_nave_des"></td>';
    row = row + '<td class="fill_locacion_id" style="display:none"></td>';
    row = row + '<td class="fill_nave_id"  style="display:none"></td>';
    row = row + '<td><input type="Button" value="Quitar" class="quitapersona btn btn-default btn-xs" /></td>';
    body.append(row);
    contador = contador + 1    

    $('.quitapersona').bind('click', function () {  
        var fila = $(this);
        fila.parent().parent().remove();

    });



    
}

function BuscaPersonaTabla(rut) {
    var listado_rut = $(".rut_persona");    
    var existe = false;
    $(listado_rut).each(function () {
        var rut_td = $(this)["0"].innerText;
        if (rut_td == rut) {
            existe = true;
            return false;
        }              
    });
    return existe;
}
