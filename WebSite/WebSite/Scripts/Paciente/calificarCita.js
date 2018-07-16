


function calificarCita() {
    debugger;
    var calificacion = $('#hdfCalificacion').val();
    var identificadorGUID = $('#hdfIdentificadorGUID').val();
    var envioDatosEsValido = calificacion != '' && calificacion != '-1';

    if (envioDatosEsValido) {

        var datos = JSON.stringify({
            Calificacion: calificacion,
            IdentificadorGUID: identificadorGUID
        });

        mostrarLoading();

        $.ajax({
            type: "POST",
            url: '/Paciente/EnviarCalificacion',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: datos,
            success: function (data) {
                ocultarLoading();
                var respuesta = $.parseJSON(data);
                var exito = respuesta.Exito;
                var mensaje = respuesta.Respuesta;
                if (exito) {
                    $('#popUpCalificacion').modal('hide');

                    mostrarMensaje('Éxito', mensaje, 'exito');
                }
                else {
                    mostrarMensaje('Error', mensaje, 'error');
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                ocultarLoading();
                mostrarMensaje('Error', 'No se pudo realizar la calificación', 'error');
            }
        });
    }
    else {
        mostrarMensaje('', 'Por favor seleccione una calificación', 'alerta');
    }
}