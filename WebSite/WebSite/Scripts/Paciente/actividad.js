

$(document).ready(function (elementos) {
    mostrarProximasActividades()
});


function mostrarProximasActividades() {
    mostrarLoading();
    $.ajax({
        type: "POST",
        url: '/Paciente/ObtenerActividades',
        data: '',
        dataType: "html",
        success: function (data) {
            ocultarLoading();
            var datos = JSON.parse(data);
            var mensajeError = datos.mensajeError;
            var exitoEnConsulta = mensajeError == '';
            if (exitoEnConsulta) {
                var vista = datos.vistaHtml;
                $('#divActividades').empty();
                $('#divActividades').html(vista);
            }
            else {
                mostrarMensaje('Error', mensajeError, 'error');
            }
        },
        error: function (data) {
            ocultarLoading();
            mostrarMensaje('No se pudo realizar la consulta de las actividades', mensajeError, 'error');
        }
    });
}