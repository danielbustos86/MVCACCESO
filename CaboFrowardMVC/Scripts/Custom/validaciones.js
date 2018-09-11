$(document).ready(function () {

    
    var caracteresRUT = 8;
   
    $("#RUT").keyup(function () {
        if ($(this).val().length > caracteresRUT) {
            $(this).val($(this).val().substr(0, caracteresRUT));
        }
    });
   
   
    var caracteresDV = 1;
    
    $("#DV").change(function () {

        if ($(this).val().length > caracteresDV) {
            $(this).val($(this).val().substr(0, caracteresDV));
        }

        RUT = $("#RUT").val();
        DV = $("#DV").val();        
        valor = RUT + DV;     
        cuerpo = valor.slice(0, -1);
        dv = valor.slice(-1).toUpperCase();      
        RUT.value = cuerpo + '-' + dv
       
        if (cuerpo.length < 7) {
            alerta("RUT Incompleto");
            $("#RUT").focus();
            return false;
        }
        suma = 0;
        multiplo = 2;

      
        for (i = 1; i <= cuerpo.length; i++) {

        
            index = multiplo * valor.charAt(cuerpo.length - i);          
            suma = suma + index;                       
            if (multiplo < 7) { multiplo = multiplo + 1; } else { multiplo = 2; }

        }
       
        dvEsperado = 11 - (suma % 11);    
        dv = (dv == 'K') ? 10 : dv;
        dv = (dv == 0) ? 11 : dv;

        
        if (dvEsperado != dv) {
            alerta("RUT Inválido");
            $("#RUT").val('');
            $("#DV").val('');
            $("#RUT").focus();
            return false;
        }
               
        alerta('Rut OK');

    });

    

});