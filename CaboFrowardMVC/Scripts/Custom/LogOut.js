var inicio = 3


$(document).ready(function () {
    
    $("#lnk_logoff").click(function (e) {
        e.preventDefault();
        $("#frm_logoff").submit();
    });

    setInterval(revisar_permiso, 1000);
    
});

function revisar_permiso() {

    

    if ($("#txt_acceso").val() == "SIN_ACCESO") {
        inicio = inicio - 1;
        if (inicio == 0) {
            $("#lnk_logoff").click();

        }
        else {
            $("#txt_second").html(inicio);
        }
       
    }

}


