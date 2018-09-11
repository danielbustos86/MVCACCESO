$(document).ready(function () {

	$('#fechaInduccion').datetimepicker({
		timepicker: false,
		format: 'd-m-Y',
		mask: '31-12-9999',
		useCurrent: true,
	}).on('change', function () {
		$('.xdsoft_datetimepicker').hide();
		var str = $(this).val();
		str = str.split(".");
		$('#alt_date_field').val(str[2] + '-' + str[1] + '-' + str[0]);
	});


	$('#FECHAINDUCCION_Value').datetimepicker({
		timepicker: false,
		format: 'd-m-Y',
		mask: '31-12-9999',
		useCurrent: true,
	}).on('change', function () {
		$('.xdsoft_datetimepicker').hide();
		var str = $(this).val();
		str = str.split(".");
		$('#alt_date_field').val(str[2] + '-' + str[1] + '-' + str[0]);
	});

	var dateNewFormat, onlyDate, today = new Date();


	onlyDate = today.getDate();

	if (onlyDate.toString().length == 2) {
		dateNewFormat = onlyDate;
	}
	else {
		dateNewFormat = '0' + onlyDate;
	}


	if (today.getMonth().toString().length == 2) {
		dateNewFormat += '-' + (today.getMonth() + 1) + '-' + today.getFullYear();
	} else {
		dateNewFormat += '-0' + (today.getMonth() + 1) + '-' + today.getFullYear();
	}

	$('#fechaInduccion').val(dateNewFormat);

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

		var rut = "0"; 

		if (pasaporte == "") {
			alerta("Ingrese pasaporte")
			return;
		}


		if (BuscaPersonaTablaPasaporte(pasaporte)) {

			alerta("Persona ya existe en el listado actual");
			return false;

		}
		var json = '{"rut":' + rut + ', "pasaporte":"' + pasaporte + '"}';
		
		$.ajax({
			type: "POST",
			url: "../Helpers/GetPersonaInduccion",
			data: json,
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function (response) {

				var respuesta = response;

				if (respuesta.mensaje != "") {
					alerta(respuesta.mensaje)
					return false;
				}


				if (respuesta.existe == "0") {

					$("#txtpasaporte").val(pasaporte);

					alerta("Persona no registrada");
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

			alerta("Persona ya existe en el listado actual");
			return false;

		}
		var pasaporte = "";
		var json = '{"rut":' + rut + ', "pasaporte":"' + pasaporte + '"}';


		$.ajax({
			type: "POST",
			url: "../Helpers/GetPersonaInduccion",
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


	function Agrega_Persona(personal) {



		var contador = 0;
		var body = $('#TABlAPERSONAINDUC').find('tbody');
		var row = ""
		row = '<tr>';

		row = row + '<td class = id_persona>' + personal.Id.toString() + '</td>';
		row = row + '<td class = rut_persona>' + personal.Rut.toString() + '</td>';
		row = row + '<td class = dv>' + personal.Dv.toString() + '</td>';
		row = row + '<td class = pasaporte_persona>' + personal.Pasaporte.toString() + '</td>';
		row = row + '<td>' + personal.Nombre.toString() + ' ' + personal.Apellido.toString() + '</td>';
		row = row + '<td> ' + personal.estadoInduccion.toString() + '</td>';
		row = row + '<td> ' + personal.fechaInduccion.toString() + '</td>';

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
	function BuscaPersonaTablaPasaporte(rut) {
		var listado_rut = $(".pasaporte_persona");
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

	


	$("#btnactualizainduc").click(function (e) {



		Actualizar();
	});


	function Actualizar() {

		var perxml;
		perxml = "<personas>";
		var id="";

		var fecha = $("#fechaInduccion").val();

		if (fecha=="") {
			alerta("Debe ingresar fecha");
			return;
		}
	

		var tableID = "TABlAPERSONAINDUC";
		var table = document.getElementById(tableID);
		var cantidad = table.rows.length;
		
		if (cantidad == 1) {

			alerta("Debe ingresar personas");
			return;
		}

		for (var i = 1; i < table.rows.length; i++) {
			id = table.rows[i].cells[0].innerText;

			perxml += "<persona perid='" + id + "'>";
			perxml += "</persona>";
		}
		perxml += "</personas>"


		var json = '{"fecha":"' + fecha + '", "personas":"' + perxml + '"}';
		alert(json);
		$.ajax({
			type: "POST",
			url: "../PrevencionRiesgos/ActualizaFechaInduccion",
			data: json,
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function (response) {

				if (response.mensaje == "ok") {
			
					alerta("Actualizacion Correcta");
					$("#TABlAPERSONAINDUC > tbody").html("");
					$("#TABlAPERSONAINDUC > tbody").html(response.html);
				}
				else {

					alerta(response.mensaje)
				}

			}
		});



	}





});
