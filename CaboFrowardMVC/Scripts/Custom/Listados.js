$(document).ready(function () {

	ObtenerListados();           

});




function ObtenerListados() {        
   
    $.ajax({
        type: "POST",
        url: " ../Solicitud/CargarListados",       
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            var respuesta = response;
            if (respuesta.mensaje != '') {
                alerta(respuesta.mensaje);
                return false;
            }
            else {

                //cargamos los combo box //
                $("#SelectPuerto").html(respuesta.puerto);
                $("#SelectEmpresa").html(respuesta.empresa);
       

                //$('#SelectEmpresa').selectize({
                //    create: true,
                //    sortField: {
                //        field: 'text',
                //        direction: 'asc'
                //    },
                //    dropdownParent: 'body'
                //});
                $("#SelecttipoIngres").html(respuesta.tipo_ingreso);
				$("#selectnac").html(respuesta.nacion);

			//	$("#selectnac").val('39').trigger('change.select2');

				document.ready = document.getElementById("selectnac").value = '39';
                $("#selecttvcs").html(respuesta.vehiculo);
                $("#SelectPuerto").focus();
            }          
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}
