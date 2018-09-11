$(document).ready(function () {

 
    CargaPerfiles();
    CargaLocaciones();
        

});


function CargaPerfiles() {

    var id = document.getElementById("idusuarios").innerHTML;
    var json = '{"id":' + id + '}';

    $.ajax({
        type: "POST",
        url: "../../Usuarios/GetPerfilUsuario1",
        contentType: "application/json; charset=utf-8",
        data:json,
        dataType: "json",
        success: function (response) {        
            var respuesta = response;
            if (respuesta.mensaje != '') {
                alerta(respuesta.mensaje);
                return false;
            }
            else {

                $("#TablaPerfilUsuario > tbody").html("");
                $("#TablaPerfilUsuario > tbody").html(respuesta.html);
                $('.sel_chk').bootstrapToggle();


                $('.sel_chk').bind('change', function () {
                    var accion = "";
                    if ($(this).prop('checked') == true) {
                        accion = 'I'
                    }
                    else {
                        accion = 'D'
                    }
                    var perfil = $(this).parent().parent().parent()["0"].cells[1].innerText;
                    var idusuario = document.getElementById("idusuarios").innerHTML;
                    var json = '{"idusuario":"' + idusuario + '", "perfil":"' + perfil + '", "accion":"' + accion + '"}';
                    $.ajax({
                        type: "POST",
                        url: "../../Usuarios/ModificaAsignacion",
                        contentType: "application/json; charset=utf-8",
                        data: json,
                        dataType: "json"                                           
                    })
                })
               }
          }       
    });
}



function CargaLocaciones() {

    var id = document.getElementById("idusuarios").innerHTML;
    var json = '{"id":' + id + '}';

    $.ajax({
        type: "POST",
        url: "../../Usuarios/GetUsuarioLocacion",
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

                $("#TablaLocacionUsuario > tbody").html("");
                $("#TablaLocacionUsuario > tbody").html(respuesta.html);
                $('.sel_chk_1').bootstrapToggle();                
                $('.sel_chk_1').bind('change', function () {
                    var accion = "";
                    if ($(this).prop('checked') == true) {

                        accion = 'I'
                    }
                    else {

                        accion = 'D'

                    }
                    var locacion = $(this).parent().parent().parent()["0"].cells[1].innerText;
                    var idusuario = document.getElementById("idusuarios").innerHTML;
                    var json = '{"idusuario":"' + idusuario + '", "locacion":"' + locacion + '", "accion":"' + accion + '"}';

                    $.ajax({
                        type: "POST",
                        url: "../../Usuarios/ModificaLocacion",
                        contentType: "application/json; charset=utf-8",
                        data: json,
                        dataType: "json",
                        success: function (response) {


                        },
                        failure: function (response) {
                            alert(response.d);
                        }



                    })



                })
            }
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}