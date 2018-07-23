
var Usuario = function () {
    this.password = $('#txbPassword').val();
    this.correo = $('#txbCorreo').val();

    this.obtenerDatos = function () {
        var datos = JSON.stringify({
            Password: this.password,
            Correo: this.correo
        });

        return datos;
    }

    this.validarFormulario = function () {
        var mensajeError = '';

        var passwordEsValido = this.password != '';
        var correoEsVacio = this.correo != '';
        var formatoCorreoValido = validarCorreo(this.correo);

        mensajeError = passwordEsValido ? mensajeError : (mensajeError + '<p>Digite el password.</p>');
        mensajeError = correoEsVacio ? mensajeError : (mensajeError + '<p>Digite el correo.</p>');

        if (correoEsVacio) {
            mensajeError = formatoCorreoValido ? mensajeError : (mensajeError + '<p>Digite un formato de correo válido (correo@gmail.com).</p>');
        }

        var formularioValido = mensajeError == '';

        if (!formularioValido) {
            mostrarMensaje('Campos requeridos con *', mensajeError, 'alerta');
        }

        return formularioValido;
    }

    this.validarCredenciales = function () {
        var envioDatosEsValido = this.validarFormulario();

        if (envioDatosEsValido) {
            var datos = this.obtenerDatos()
            mostrarLoading();

            $.ajax({
                type: "POST",
                url: '/Usuario/ValidarCredenciales',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: datos,
                success: function (data) {
                    ocultarLoading();
                    var respuesta = $.parseJSON(data);
                    var exito = respuesta.Exito;
                    var mensaje = respuesta.Respuesta;
                    if (exito) {
                        mostrarMensaje('Éxito', mensaje, 'exito');
                        window.location.href = '/Practicante/CitasPracticante/'
                    }
                    else {
                        mostrarMensaje('Error', mensaje, 'error');
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    ocultarLoading();
                    var responseText = jqXHR.responseText;
                    //var mensajeError = obtenerMensajeError(responseText);
                    mostrarMensaje('Error', responseText, 'error');
                }
            });
        }
    }
};


function validarCredenciales() {
    usuario = new Usuario();
    usuario.validarCredenciales();
}