$(document).ready(function ()
{

	$('.CONTENIDOTABLA').hide();
	$('#fechainicio').datetimepicker({
		timepicker: false,
		mask:true, // '9999/19/39 29:59' - digit is the maximum possible for a cell
	});
	$('#fechatermino').datetimepicker({
		timepicker: false,
		mask: true, // '9999/19/39 29:59' - digit is the maximum possible for a cell
	});



	$("#btnreporteCli").click(function () { 
		$('.CONTENIDOTABLA').show();

		var fecha_inicio = $("#fechainicio").val();
		var fecha_fin = $("#fechatermino").val();

		var json = '{"FechaInicio":"' + fecha_inicio + '", "FechaFin":"' + fecha_fin +'"}';


		$.ajax({
			type: "POST",
			url: "../AccesoClientes/Reporte",
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: json,
			success: function (response) {


				if (response.mensaje != "") {
					alerta(response.mensaje);
					$("#tabla_reporte3 > tbody").html("");
				}
				else {

					if (response.html != "") {
						$("#tabla_reporte3 > tbody").html("");
						$("#tabla_reporte3 > tbody").html(response.datos);

						//$("#tabla_reporte1").dataTable().fnDestroy()
						//$('#tabla_reporte1').DataTable({ 'language': { 'url': '../content/DatatableUtil/Spanish.json' }, "lengthMenu": [[30], [30]], searching: false, "ordering": false, "bLengthChange": false, info: false });


					}
				}
			},
			failure: function (response) {

				alert(response.d);
			}
		});
		return false;



	});


	$("#btnexp3").click(function () {

		var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
		var textRange; var j = 0;
		tab = document.getElementById('tabla_reporte3'); // id of table

		for (j = 0; j < tab.rows.length; j++) {
			tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
			//tab_text=tab_text+"</tr>";
		}

		tab_text = tab_text + "</table>";
		tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
		tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
		tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

		var ua = window.navigator.userAgent;
		var msie = ua.indexOf("MSIE ");

		if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
		{
			txtArea1.document.open("txt/html", "replace");
			txtArea1.document.write(tab_text);
			txtArea1.document.close();
			txtArea1.focus();
			sa = txtArea1.document.execCommand("SaveAs", true, "Say Thanks to Sumit.xls");
		}
		else                 //other browser not tested on IE 11
			sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

		return (sa);

	})
});
