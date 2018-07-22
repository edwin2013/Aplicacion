$(document).ready(function () {
    $("#txbMes").datepicker({
        format: "mm/yyyy",
        autoclose: true,
        minViewMode: 1,
    }).on('changeDate', function (ev) {
        var dia = $('#txbMes').val();
        var diaValido = dia != '';
        if (diaValido) {
            obtenerDiasOfertaMes('-1');
        }
    });

    limpiarCampos();
});

var CitaModelo = function () {

    this.dia = $('#ddlDias').val();
    this.hora = $('#ddlHoras').val();
    this.mes = $('#txbMes').val();
    this.citaId = $('#hdfCitaId').val();
    this.pacienteId = $('#hdfPacienteId').val();

    this.obtenerDatosCita = function () {
        var datos = JSON.stringify({
            Dia: this.dia,
            Hora: this.hora,
            CitaId: this.citaId,
            PacienteId: this.pacienteId,
            DetalleHora: $("#ddlHoras option:selected").text()
        });

        return datos;
    }

    this.validarFormulario = function () {
        var mensajeError = '';
        var diaEsValido = this.dia != '' && this.dia !== null && this.dia != '-1';
        var horaEsValido = this.hora != '' && this.hora !== null && this.hora != '-1';
        var mesEsValido = this.mes != '' && this.mes !== null && this.mes != '-1';

        mensajeError = mesEsValido ? mensajeError : (mensajeError + '<p>Seleccione el mes de la cita.</p>');
        mensajeError = diaEsValido ? mensajeError : (mensajeError + '<p>Seleccione el dia de la cita.</p>');
        mensajeError = horaEsValido ? mensajeError : (mensajeError + '<p>Seleccione la hora de la cita.</p>');

        var formularioValido = mensajeError == '';

        if (!formularioValido) {
            mostrarMensaje('Campos requeridos con *', mensajeError, 'alerta');
        }

        return formularioValido;
    }
};

function mostrarPopUpCambiarHorarioCita(citaId, fecha, hora, paciente, pacienteId) {
    $('#hdfCitaId').val(citaId);
    $('#hdfPacienteId').val(pacienteId);
    var arreglofecha = fecha.split('/');
    var mes = arreglofecha[1] + '/' + arreglofecha[2];
    $('#txbMes').val(mes);
    obtenerDiasOfertaMes(fecha);

    $('#lblTituloPopUpCambiarHorario').html('Cambiar cita de ' + paciente + ' el dia ' + fecha + ', ' + hora);
    $('#popUpCambiarHorarioCita').modal('show');
}

function actualizarHorarioCita() {
    citaModelo = new CitaModelo();
    var envioDatosEsValido = citaModelo.validarFormulario();

    if (envioDatosEsValido) {
        var datos = citaModelo.obtenerDatosCita()
        mostrarLoading();

        $.ajax({
            type: "POST",
            url: '/Practicante/ActualizarHorarioCita',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: datos,
            success: function (data) {
                ocultarLoading();
                var respuesta = $.parseJSON(data);
                var exito = respuesta.Exito;
                var mensaje = respuesta.Respuesta;
                if (exito) {
                    $('#popUpCambiarHorarioCita').modal('hide');
                    obtenerCitasPracticante();
                    mostrarMensaje('Éxito', mensaje, 'exito');
                }
                else {
                    mostrarMensaje('Error', mensaje, 'error');
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                ocultarLoading();
                var responseText = jqXHR.responseText;
                var mensajeError = obtenerMensajeError(responseText);
                mostrarMensaje('Error', mensajeError, 'error');
            }
        });
    }
}

function obtenerDiasOfertaMes(fecha) {
    var dia = $('#txbMes').val();

    var diaValido = dia != '';
    if (diaValido) {

        mostrarLoading();

        var datos = JSON.stringify({
            fechaOferta: $('#txbMes').val()
        });

        $.ajax({
            type: "POST",
            url: '/Paciente/ObtenerDiasOfertaMes',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: datos,
            success: function (data) {
                ocultarLoading();
                var listaDiasOfertaMes = $.parseJSON(data);
                llenarComboDias(listaDiasOfertaMes, fecha);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                ocultarLoading();
                var responseText = jqXHR.responseText;
                var mensajeError = obtenerMensajeError(responseText);
                mostrarMensaje('Error', mensajeError, 'error');
            }
        });
    }
}

function llenarComboDias(listaDiasOfertaMes, fecha) {
    var comboDias = $("#ddlDias");
    comboDias.empty();

    var comboSesiones = $("#ddlHoras");
    comboSesiones.empty();
    comboSesiones.append($("<option />").val('-1').text('Sin datos'));

    var poseeDiasOfertaMes = listaDiasOfertaMes.length > 0;

    if (poseeDiasOfertaMes) {
        comboDias.append($("<option />").val('-1').text('Seleccione'));

        $.each(listaDiasOfertaMes, function () {
            var diaOferta = this.DiaOferta;
            var detalleDiaOferta = this.DetalleDiaOferta;
            var fechaDiaOferta = this.FechaDiaOferta;
            comboDias.append($("<option />").val(fechaDiaOferta).text(detalleDiaOferta + ', ' + diaOferta));
        });

        var existeFecha = fecha != '-1';
        if (existeFecha) {
            comboDias.val(fecha);
            obtenerSesionesActivas();
        }
    }
    else {
        comboDias.append($("<option />").val('-1').text('Sin datos'));
    }
}

function obtenerSesionesActivas() {
    mostrarLoading();
    var fechaDia = $('#ddlDias').val();
    var datos = JSON.stringify({
        fechaDia: fechaDia
    });

    var diaValido = fechaDia != '-1';

    if (diaValido) {
        $.ajax({
            type: "POST",
            url: '/Paciente/ObtenerSesionesActivas',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: datos,
            success: function (data) {
                ocultarLoading();
                var listaSesionesActivas = $.parseJSON(data);
                llenarComboHoras(listaSesionesActivas);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                ocultarLoading();
                var responseText = jqXHR.responseText;
                var mensajeError = obtenerMensajeError(responseText);
                mostrarMensaje('Error', mensajeError, 'error');
            }
        });
    }
}

function llenarComboHoras(listaSesionesActivas) {
    var combo = $("#ddlHoras");
    combo.empty();

    var poseeSesionesActivas = listaSesionesActivas.length > 0;
    if (poseeSesionesActivas) {
        combo.append($("<option />").val('-1').text('Seleccione'));

        $.each(listaSesionesActivas, function () {
            var horaOfeta = this.Hora;
            var detalleHora = this.DetalleHora;
            combo.append($("<option />").val(horaOfeta).text(detalleHora));
        });
    }
    else {
        combo.append($("<option />").val('-1').text('Sin datos'));
    }


}

function limpiarCampos() {
    $('#txbMes').val('');
    $('#ddlDias').val('');
    $('#ddlHoras').val('');
}