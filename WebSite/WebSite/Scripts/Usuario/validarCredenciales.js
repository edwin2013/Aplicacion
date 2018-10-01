
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
                url: '/Login/ValidarCredenciales',
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

function validarCorreo(email) {
    var emailReg = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
    return emailReg.test(email);
}

function mostrarLoading() {

    $('.modal_loading').show();
    $('body').addClass("loading");
}

function ocultarLoading() {

    window.setTimeout(function () {
        $('body').removeClass("loading");
        $('.modal_loading').hide();
    }, 100);

}

function mostrarMensaje(headerMensaje, bodyMensaje, tipoMensaje) {
    var esTipoAlerta = tipoMensaje == 'alerta';
    var esTipoError = tipoMensaje == 'error';
    var esTipoExito = tipoMensaje == 'exito';
    var esTipoInformacion = tipoMensaje == 'informacion';

    if (esTipoAlerta) {
        $('#popUpMensaje .modal-header').css('background-color', '#FEEFB3');
        $('#headerPopUp').css('color', '#9F6000');
    }
    else if (esTipoError) {
        $('#popUpMensaje .modal-header').css('background-color', '#FFD2D2');
        $('#headerPopUp').css('color', '#D8000C');
    }
    else if (esTipoExito) {
        $('#popUpMensaje .modal-header').css('background-color', '#DFF2BF');
        $('#headerPopUp').css('color', '#4F8A10');
    }
    else {
        $('#popUpMensaje .modal-header').css('background-color', '#BDE5F8');
        $('#headerPopUp').css('color', '#00529B');
    }

    $('#headerPopUp').html(headerMensaje);
    $('#divMensajePopUp').html(bodyMensaje);
    document.getElementById("popUpMensaje").style.zIndex = 2000;
    $('#popUpMensaje').modal('show');
}