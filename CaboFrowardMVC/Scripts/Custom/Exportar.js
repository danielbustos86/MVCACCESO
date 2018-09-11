$("#btn_imprime_informexlugar").click(function () {
    var fecha = $("#txtFecha").val();
    var turno = $("#txtTurno").val();

    var tmpElemento = document.createElement('a');
    var data_type = 'data:application/vnd.ms-excel';
    var tabla_div = document.getElementById('seleccionsolicitudxlugar');
    var tabla_html = tabla_div.outerHTML.replace(/ /g, '%20');
    tmpElemento.href = data_type + ', ' + tabla_html;

    tmpElemento.download = 'Solicitud-' + fecha + '_' + turno + '.xls';
    tmpElemento.click();
});