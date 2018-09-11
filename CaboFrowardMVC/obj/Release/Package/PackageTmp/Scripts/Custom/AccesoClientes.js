$(document).ready(function () {

	
	llenaMotivo();

	$("#btn_modificar").click(function () {

		var tipottp = $("#tipoVeAc").val();
	
		if (tipottp == 0) {

			alerta("DEBE SELECCIONAR UN TIPO DE VEHICULO");
			return false;
		} else {


		$('#ModalPatente').modal('show');
			return false;
		}
	});



	//$(".chosen").chosen({ allow_single_deselect: true, disable_search: true });
	$('#btnRegistraSal').attr('disabled', true);
	$('#btnRegistraIng').attr('disabled', true);


	
	
	$("#rutChofer").focus();
	$('#rutChofer').keypress(function (e) {


		if (e.which == 13) {
			buscar_datos();
			return false;
		}
	});



	$("#btnguardarcs").click(function () {

		var patente = $("#txtingresaPatentecs").val();
		var tipottp = $("#tipoVeAc").val();
		var tipo;
		if (tipottp == "1") {

			tipo = "121";
		}

		if (tipottp == "2") {

			tipo = "122";
		}

		if (tipottp == "2") {

			tipo = "122";
		}

		var opcion_seleccionada = "";

		var json = '{"patente":"' + patente + '", "tipo":"' + tipo + '", "descripcion":"' + opcion_seleccionada + '", "id":"' + $("#txt_solicitud").val() + '"}';

		$.ajax({
			type: "POST",
			url: "ModificaVehiculo",
			data: json,
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function (response) {

				if (response.mensaje != "") {
					alerta(response.mensaje);
					return false;
				}
				else {
					$("#cbx_vehiculo").html("");
					$("#cbx_vehiculo").html(response.patente);
					$('#ModalPatente').modal('hide');
				}
			},
			failure: function (response) {

				alert(response.d);
			}
		});
		return false;
	});



	function limpiarcadena(cadena) {

		// Definimos los caracteres que queremos eliminar
		var specialChars = "!@#$^&%*()+=-[]\/{}|:<>?,.";

		for (var i = 0; i < specialChars.length; i++) {
			cadena = cadena.replace(new RegExp("\\" + specialChars[i], 'gi'), '');
		}
		// Quitamos espacios y los sustituimos 
		cadena = cadena.replace(/ /g, "");
		cadena = cadena.substring(0, cadena.length - 1);

		return (cadena);

	}

	function buscar_datos() {

		// Definimos los caracteres que queremos eliminar
		

		DesactivaBotones();


		//fin victor
		var rut_limpio = "";
		var tipodoc = $('#tipodDOC').val();
		var pasaporte = "";
		if (tipodoc == "1") {
			var specialChars = "!@#$^&%*()+=-[]\/{}|:<>?,.";
			var rut = $('#rutChofer').val();
			var rut_limpio = "";
			//indica la posicion en que esta este texto, si no lo encuentra devuelve -1
			var flag = rut.search("https");

			if (flag != -1) //Es QR
			{

				inicio = 52,
					fin = 62,
					rut_con_simbolos = rut.substring(inicio, fin);


				rut_limpio = limpiarcadena(rut_con_simbolos);

				//alert(rut_limpio);

			}

			if (flag == -1) //es PDF417
			{

				rut = rut.replace("-","");
				inicio = 0,
					fin = 9,
					rut_con_simbolos = rut.substring(inicio, fin);
				

				rut_limpio = limpiarcadena(rut_con_simbolos);

				

			}


			$("#rutChofer").val(rut_limpio);
			pasaporte = "";
		} else {
			rut_limpio = 0;
			pasaporte = $("#rutChofer").val();
	

		}
			

		if (rut_limpio == "") {
			rut_limpio = 0;

		} 
		var json = '{"rut":"' + rut_limpio + '", "pasaporte":"' + pasaporte + '"}';


		id_aprobado = 0;
		$.ajax({
			type: "POST",
			url: "../AccesoClientes/VerIngreso",
			data: json,
			async: false,
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function (response) {

				if (response.mensaje != "") {
					alerta(response.mensaje);
					return false;
				}
				else {

					if (response.existe == 1) {
				
						$("#idac").val(response.solicitud.Idsolicitud);
						$("#NombreChofer").val(response.solicitud.Nombre);

						$("#razonSocial").val(response.solicitud.Empresa).trigger('change.select2');
						$("#rutClienteac").val(response.solicitud.Empresa).trigger('change.select2');
						$("#motivoac").val(response.solicitud.Motivo);


						llenaTipoPago(response.solicitud.Motivo);


						$("#naveac").val(response.solicitud.NaveTTP).trigger('change.select2');
						$("#lanchacbx").val(response.solicitud.Lancha).trigger('change.select2');

						$("#tipoVeAc").val(response.solicitud.TipoVehiculo);
						$("#idSolCliente").val(response.solicitud.IdSolicitudCliente);
						$("#idperAprob").val(response.solicitud.Idaprobado);




						$("#txt_solicitud").val(response.solicitud.Idsolicitud);
						$("#cantidadhoras").val(response.solicitud.CantidadHora);
						$("#tipocontado").val(response.solicitud.TipoContado);
						$("#pcac").val(response.solicitud.PC);
						

						var idsol1 = $("#idSolCliente").val();

			

						$("#IdEntradaMov").val(response.solicitud.IdEntrada);
			
						$("#InduccionEstado").val(response.solicitud.Induccion);

						

						//Obtener texto InputSelect
						var linea = $('#naveac option:selected').text()


						$("#tipopagoac").val(response.solicitud.TipoPago);

						$("#cbx_vehiculo").html(response.patente);
						if (response.solicitud.Estado == "FUERA DE PUERTO" && response.solicitud.Autorizacion == "APROBADO") {
					
							//	ACTIVO ENTRADA
							$("#btnRegistraIng").removeClass("btn-default");
							$("#btnRegistraIng").removeClass("btn-success");
							$("#btnRegistraIng").addClass("btn-success");
							$('#btnRegistraSal').attr('disabled', true);
							$('#btnRegistraIng').attr('disabled', false);
							$('.ingreso').show("fast");
							if (idsol1 == '0') {
								$(".Cobro").hide(0);

							} else {
								$(".Cobro").show("fast");

							}

							
						

							//	DESACTIVO SALIDA
							$("#btnRegistraSal").removeClass("btn-default");
							$("#btnRegistraSal").removeClass("btn-danger");
							$('#btnRegistraSal').attr('disabled', true);

							id_aprobado = response.solicitud.Idaprobado;


							$("#btn_modificar").attr("disabled", false)

						}
						else if (response.solicitud.Estado == "EN PUERTO") {
			

							$("#btnRegistraIng").removeClass("btn-default");
							$("#btnRegistraIng").removeClass("btn-success")

							$('#btnRegistraSal').attr('disabled', false);
							$('#btnRegistraIng').attr('disabled', true);
							$(".horas").show("fast");

					

							if (idsol1 == '0') {
								$(".Cobro").hide(0);

							} else {
								$(".Cobro").show("fast");
							


							}


							$('.ingreso').hide("fast");
							if (response.solicitud.TipoContado == "2") {
								$(".boleta").show("fast");
								$(".Cobro").show("fast");
								$(".contado").show("fast");

							}		

							

							//DESACTIVO SALIDA
							$("#btnRegistraSal").removeClass("btn-default");
							$("#btnRegistraSal").removeClass("btn-danger");
							$("#btnRegistraSal").addClass("btn-danger");
							//id_aprobado = response.solicitud.Idaprobado;
							//ValidaVehiculo(id_aprobado);
							//$("#cbx_vehiculo").html(response.patente);
							//$("#btn_modificar").attr("disabled", false)
						}
						else {

							//$("#btn_ingreso").removeClass("btn-default");
							//$("#btn_ingreso").removeClass("btn-success");
							//$("#btn_salida").removeClass("btn-default");
							//$("#btn_salida").removeClass("btn-danger");
							//$("#btn_aprobar").attr("disabled", false);
							//$("#btn_modificar").attr("disabled", true);
							//$("#tabla_aprobadores > tbody").html("")
							//$("#tabla_aprobadores > tbody").html(response.aprobadores)

						
						}

					} else {
					
						alerta("SIN INGRESOS PROGRAMADOS");
					}
				}
			},
			failure: function (response) {
				alert(response.d);
			
			}
		});


	}


	$("#mensaje_exito").hide(0);
	$(".noVisible").hide(0);

	

	$(".select2-single, .select2-multiple").select2({
		theme: "bootstrap",
		placeholder: "Seleccionar",
		maximumSelectionSize: 6,
		containerCssClass: ':all:'
	});

	DesactivarSalida();
	if ($('.chosen-container').length > 0) {
		$('.chosen-container').on('touchstart', function (e) {
			e.stopPropagation(); e.preventDefault();
			// Trigger the mousedown event.
			$(this).trigger('mousedown');
		});
	}



    $('.boleta').hide();
	$('.detalle').hide();
	$('.contado').hide();
	$('.horas').hide();


	


	$('#tipopagoac').on('change', function () {

		
        if (this.value == 1) {
			$('.contado').show("fast");

        
        } else {

			$('.contado').hide("fast");
        }


	});



	
	$('#tipocontado').on('change', function () {
		if (this.value == "1") {
			$('.boleta').show("fast");
		}
		else{
			$('.boleta').hide("fast");

			}
		
	});

	$('#motivoac').on('change', function () {


		if (this.value > 0) {
			$('.ingreso').show("fast");

			
			$('#nboleta').focus();
		} else {
		
			
			$('.ingreso').hide("fast");
			$('.Motivo').show("fast");
		}
	});

	

    $('#detalleac').on('change', function () {


        if (this.value == 2) {
            $('.detalle').show("fast");

	


			$('#nguiaac').focus();
        } else {

            $('.detalle').hide("fast");
        }
	});




	$('#detallesa').on('change', function () {


		if (this.value == 2) {
			$('.detalle').show("fast");




			$('#nguiasa').focus();
		} else {

			$('.detalle').hide("fast");
		}
	});


	$("#lanchacbx").on('change', function () {



	});

	$('#rutClienteac').on('change', function () {

		

		$("#razonSocial").val(this.value).trigger('change.select2');


	});

	$('#razonSocial').on('change', function () {

		


		$("#rutClienteac").val(this.value).trigger('change.select2');
	});


	$("#btnRegistraIng").click(function () {

		var IdSolCli = $("#idSolCliente").val();

		if (IdSolCli > 0) {

		var tipopago = $("#tipopagoac").val();
		if (tipopago == "0") {

			alerta("DEBE INGRESAR TIPO DE PAGO");
			return;
		}

			
		}

		var tipocontado = $("#tipocontado").val();
		var nboleta = $("#nboleta").val();
		if (tipocontado == "1" && nboleta=="") {

			alerta("CUANDO PAGO CONTADO ES AL DIA, DEBE INGRESAR NUMERO DE BOLETA EN EL INGRESO");
			return;
		}
		RegistraMov(1); 
	});


	$("#btnRegistraSal").click(function () {
		var tipoPago = $("#tipopagoac").val();
		var tipocontado = $("#tipocontado").val();
		var nboleta = $("#nboleta").val();

		if (tipoPago == "1" && tipocontado == "2" && nboleta == "") {
			alerta("CUANDO EL PAGO ES CONTADO CON COBRO POR HORAS, ES OBLIGACION INGRESAR NUMERO DE BOLETA EN LA SALIDA");
			return;
		}

		RegistraMov(2);
	});



	$('#Patenteac').keypress(function (e) {

		if (e.which == 13) {
			var patente = $("#Patenteac").val();


			var json = '{"Patente":"' + patente + '"}';


			$.ajax({
				type: "POST",
				url: "../AccesoClientes/RetornaConvenio",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				data: json,
				success: function (response) {
					if (response.mensaje != "") {
						alerta(response.mensaje);

						return false;

					}
					else {

						if (response.cantidad != "0") {

							if (response.datos == "Convenio") {
								$("#tipopagoac").val(5);
							}


							if (response.datos == "Exento") {

								$("#tipopagoac").val(4);
							}



						} else {


							$("#tipopagoac").val(0);

						}

						//ActivarSalida();
						//$("#idsa").val(response.datos);
						//$("#fechainicio").val(response.Fecha);
					}

				},
				failure: function (response) {

					alert(response.d);
				}
			});
			return false;

		}
	});






	$('#Patentesal').keypress(function (e) {

		if (e.which == 13) {
			var patente = $("#Patentesal").val();


			var json = '{"Patente":"' + patente +  '"}';


			$.ajax({
				type: "POST",
				url: "../AccesoClientes/RetornaIngreso",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				data: json,
				success: function (response) {
					if (response.mensaje != "") {
						alerta(response.mensaje);
						DesactivarSalida();
						$('#Patentesal').focus();
						return false;
			
					}
					else {


						ActivarSalida();
						$("#idsa").val(response.datos);
						$("#fechainicio").val(response.Fecha);
					}
				
				},
				failure: function (response) {

					alert(response.d);
				}
			});
			return false;

		}
	});

	function RegistraMov(movimiento) {

		var TipoVehiculo = $("#tipoVeAc").val();
		var Patente = $("#cbx_vehiculo").val();
		var TipoDoc = $("#tipodDOC").val();
		var NdocChofer = $("#rutChofer").val();

		var dv = "1";
		var NombreChofer = $("#NombreChofer").val();
		var ApellidoChofer = "";
		var CodigoCliente = $("#rutClienteac").val();

		var Motivo = $("#motivoac").val();
		var TipoPago = $("#tipopagoac").val();
		var Detalle = $("#detalleac").val();
		var Nave = $("#naveac").val();
		var Nave = $('#naveac option:selected').text();
		var Lancha = $('#lanchacbx option:selected').text();
		var NGuia = $("#nguiaac").val();

		var Toneladas = $("#toneladas").val();
		var Tara = $("#Tara").val();
		var PC = $("#pcac").val();
		var nboleta = $("#nboleta").val();

		var IdSolCli = $("#idSolCliente").val();
		var cantidadhoras = $("#cantidadhoras").val();

		if (cantidadhoras == "") {
			cantidadhoras = "0";
		}

		if (Motivo > 0) {



			if (TipoVehiculo == 0) {
				alerta("DEBE INGRESAR VEHICULO");
				return;
			}



			if (Detalle == 0) {

				alerta("Debe Ingresar Detalle");
				return;
			}

			if (Detalle == 1) {


				NGuia = "";
				Toneladas = 0;
				Tara = 0;
			}


		} else {

			NGuia = "";
			Toneladas = 0;
			TipoVehiculo = 0;
			Motivo = "";
			Tara = "";
		}
		var IdSol = $("#txt_solicitud").val();
		var IdPersonaAproba = $("#idperAprob").val();
		var TipoMov = movimiento;
		var usuario = $("#usuario").val();
		var tipoContado = $("#tipocontado").val();
		
		
		var json = '{"TipoVehiculo":"' + TipoVehiculo + '", "Patente":"' + Patente + '", "TipoDoc":"' + TipoDoc + '", "IdSol":"' + IdSol + '", "IdSolCli":"' + IdSolCli + '", "IdPersonaAproba":"' + IdPersonaAproba + '", "TipoMov":"' + TipoMov + '", "CodigoCliente":"' + CodigoCliente + '", "ApellidoChofer":"' + ApellidoChofer + '", "Motivo":"' + Motivo + '", "TipoPago":"' + TipoPago + '", "Detalle":"' + Detalle + '", "Nave":"' + Nave + '", "NGuia":"' + NGuia + '", "Toneladas":"' + Toneladas + '", "PC":"' + PC + '", "nboleta":"' + nboleta + '", "lancha":"' + Lancha + '", "usuario":"' + usuario + '", "tipoContado":"' + tipoContado + '", "cantidadhoras":"' + cantidadhoras + '", "tara":"' + Tara + '"}';
		//alert(json);

		$.ajax({
			type: "POST",
			url: "../AccesoClientes/RegistrarIngreso",
			data: json,
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function (response) {
				if (response.mensaje == "Error Numero 57-AccesoClienteDAL") {
					alerta(response.mensaje);
					$('#btnRegistraSal').attr('disabled', true);
					$('#btnRegistraIng').attr('disabled', true);
					return false;
		
				}
				else {

					
					$("#IdEntradaMov").val(response.mensaje);
					alerta("Registro exitoso");

					DesactivaBotones();
				//	$("#mensaje_exito").show(0);
					//$("#mensaje_exito").fadeOut(2500);
					//limpiar();
					
				}
			},
			failure: function (response) {
				alert(response.d);
			}
		});



	}

	function limpiar() {
		$("#form1")[0].reset();
		$("#cbx_vehiculo").html("");
		

		$("#motivoac").val("0");
		
		$("#tipopagoac").val("0");
		$("#detalleac").val("0");

		$('.detalle').hide("fast");
		$('.boleta').hide();

		$("#rutChofer").focus();
		$('#btnRegistraSal').attr('disabled', true);
		$('#btnRegistraIng').attr('disabled', true);


	}
	function DesactivaBotones() {
	$('#btnRegistraSal').attr('disabled', true);
	$('#btnRegistraIng').attr('disabled', true);
		$("#btnRegistraSal").removeClass("btn-danger");

	$("#btnRegistraSal").removeClass("btn-default");
	
	$("#btnRegistraIng").removeClass("btn-success");

	$("#btnRegistraIng").removeClass("btn-default");


		$("#btnRegistraIng").addClass("btn-default");

		$("#btnRegistraSal").addClass("btn-default");
	}
	function DesactivarSalida() {





		//$('#detallesa').attr('disabled', true);
		//$('#pcsa').attr('disabled', true);
		//$('#btnRegistraSal').attr('disabled', true);

		//$('#detallesa').val(0);
		//$('#pcsa').val("");
		//$('#toneladassa').val("");
		//$('#nguiasa').val("");
		
		//$("#idsa").val("");
		//$("#fechainicio").val("");
		//$('#Patentesal').val("");
		//$('#Patentesal').focus();

	}


	function ActivarSalida() {
		$('#detallesa').attr('disabled', false);
		$('#pcsa').attr('disabled', false);
		$('#btnRegistraSal').attr('disabled', false);

	}

	function llenaMotivo() {


		//var json = '{"Patente":"' + patente + '"}';


		$.ajax({
			type: "POST",
			url: "../AccesoClientes/CargaMotivo",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			success: function (response) {

				if (response.html != "") {
					$("#motivoac").html("");
					$("#motivoac").html(response.html);

				}
				

			},
			failure: function (response) {

				alert(response.d);
			}
		});


	}

	$('#motivoac').on('change', function () {

		llenaTipoPago(this.value);
	});

		function llenaTipoPago(idMotivo) {

	
			var json = '{"idMotivo":"' + idMotivo + '"}';


			$.ajax({
				type: "POST",
				url: "../AccesoClientes/CargaTipoPago",
				contentType: "application/json; charset=utf-8",
				dataType: "json",
				data: json,
				success: function (response) {

					if (response.html != "") {
						$("#tipopagoac").html("");
						$("#tipopagoac").html(response.html);

					}


				},
				failure: function (response) {

					alert(response.d);
				}
			});



	}

	//$("#btnRegistraSal").click(function () {


	//	//int ano, int id, int DetalleSalida, string guiaSalida, decimal ToneladasSalida, string PCSalida


	//	var codigo = $("#idsa").val().split("-");
	//	var ano = codigo[0];
	//	var id = codigo[1];
	//	var DetalleSalida = $("#detallesa").val();

	//	if (DetalleSalida == 0) {

	//		alerta("Debe indicar Detalle");
	//		return;
	//	}


	//	var guiaSalida = $("#nguiasa").val();
	//	var toneladassa = $("#toneladassa").val();
	//	var PCSalida = $("#pcsa").val();

	//	if (DetalleSalida == 1) {

	//		DetalleSalida = 0;
	//		guiaSalida = "";
	//		toneladassa = 0;
	//	}

	//	if (DetalleSalida == 2) {

	//		DetalleSalida = 1;
	//	}


			
	//	var json = '{"ano":"' + ano + '", "id":"' + id + '", "DetalleSalida":"' + DetalleSalida + '", "guiaSalida":"' + guiaSalida + '", "ToneladasSalida":"' + toneladassa + '", "PCSalida":"' + PCSalida  + '"}';
	

	//	$.ajax({
	//		type: "POST",
	//		url: "../AccesoClientes/RegistraSalida",
	//		data: json,
	//		contentType: "application/json; charset=utf-8",
	//		dataType: "json",
	//		success: function (response) {

	//			if (response.mensaje == "OK") {
	//				alerta("Registro Correcto");
	//				DesactivarSalida()
	//				return false;
		
	//			}
	//			else {

	//				alerta(response.mensaje);
	//				return false;
					
	//			}
	//		},
	//		failure: function (response) {
	//			alert(response.d);
	//		}
	//	});


	//});


});
