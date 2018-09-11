$(document).ajaxSend(function (event, request, settings) {
    $("body").append("<li>Ajax Iniciado</li>");
});

$(document).ajaxComplete(function (event, request, settings) {  
    $("body").append("<li>Request Complete.</li>");
});