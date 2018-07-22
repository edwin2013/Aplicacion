$(document).ready(function () {
    $("#txbMes").datepicker({
        format: "mm/yyyy",
        autoclose: true,
        minViewMode: 1,
    }).on('changeDate', function (ev) {
        var dia = $('#txbMes').val();
        var diaValido = dia != '';
        if (diaValido) {
            obtenerDiasOfertaMes();
        }
    });

    limpiarCampos();
});


var ModeloCrearCita = function ()
{
	this.nombre = $( '#txbNombre' ).val();
	this.apellidos = $( '#txbApellidos' ).val();
	this.telefono = $( '#txbTelefono' ).val();
	this.correo = $( '#txbCorreo' ).val();
	this.identificacion = $( '#txbIdentificacion' ).val();

	this.obtenerDatosPaciente = function ()
	{
		var datos = {
			Nombre: this.nombre,
			Apellidos: this.apellidos,
			CorreoElectronico: this.correo,
			Telefono: this.telefono,
			Nacionalidad: '',
			Identificacion: this.identificacion,
		};

		return datos;
	}

	this.dia = $( '#ddlDias' ).val();
	this.hora = $( '#ddlHoras' ).val();
	this.mes = $( '#txbMes' ).val();

	this.obtenerDatosCita = function ()
	{
		var datos = {
			Dia: this.dia,
			Hora: this.hora,
			DetalleHora: $( "#ddlHoras option:selected" ).text()
		};

		return datos;
	}

	this.obtenerDatosCitaModelo = function ()
	{
		var datos = JSON.stringify( {
			CitaModelo: this.obtenerDatosCita(),
			PacienteModelo: this.obtenerDatosPaciente(),
		} );

		return datos;
	}

	this.validarFormulario = function ()
	{
		var mensajeError = '';
		var nombreEsValido = this.nombre != '';
		var apellidoEsValido = this.apellidos != '';
		var telefonoEsValido = this.telefono != '';
		var correoEsVacio = this.correo != '';
		var identificacionEsValida = this.identificacion != '';
		var formatoCorreoValido = validarCorreo( this.correo );

		var diaEsValido = this.dia != '' && this.dia !== null && this.dia != '-1';
		var horaEsValido = this.hora != '' && this.hora !== null && this.hora != '-1';
		var mesEsValido = this.mes != '' && this.mes !== null && this.mes != '-1';

		mensajeError = mesEsValido ? mensajeError : ( mensajeError + '<p>Seleccione el mes de la cita.</p>' );
		mensajeError = diaEsValido ? mensajeError : ( mensajeError + '<p>Seleccione el dia de la cita.</p>' );
		mensajeError = horaEsValido ? mensajeError : ( mensajeError + '<p>Seleccione la hora de la cita.</p>' );

		mensajeError = nombreEsValido ? mensajeError : ( mensajeError + '<p>Digite el nombre.</p>' );
		mensajeError = apellidoEsValido ? mensajeError : ( mensajeError + '<p>Digite los apellidos.</p>' );
		mensajeError = telefonoEsValido ? mensajeError : ( mensajeError + '<p>Digite el teléfono.</p>' );
		mensajeError = correoEsVacio ? mensajeError : ( mensajeError + '<p>Digite el correo.</p>' );
		mensajeError = identificacionEsValida ? mensajeError : ( mensajeError + '<p>Digite la identificación.</p>' );

		if ( correoEsVacio )
		{
			mensajeError = formatoCorreoValido ? mensajeError : ( mensajeError + '<p>Digite un formato de correo válido (correo@gmail.com).</p>' );
		}

		var formularioValido = mensajeError == '';

		if ( !formularioValido )
		{
			mostrarMensaje( 'Campos requeridos con *', mensajeError, 'alerta' );
		}

		return formularioValido;
	}
};

function crearCita()
{
	modeloCrearCita = new ModeloCrearCita();
	var envioDatosEsValido = modeloCrearCita.validarFormulario();

	if ( envioDatosEsValido )
	{
		var datos = modeloCrearCita.obtenerDatosCitaModelo()
		mostrarLoading();

		$.ajax( {
			type: "POST",
			url: '/Paciente/CrearCita',
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: datos,
			success: function ( data )
			{
				ocultarLoading();
				var respuesta = $.parseJSON( data );
				var exito = respuesta.Exito;
				var mensaje = respuesta.Respuesta;
				if ( exito )
				{
					$( '#popUpMantenimientoOfertaHorario' ).modal( 'hide' );
					obtenerDiasOfertaMes();
					mostrarMensaje( 'Éxito', mensaje, 'exito' );
				}
				else
				{
					mostrarMensaje( 'Error', mensaje, 'error' );
				}
			},
			error: function ( jqXHR, textStatus, errorThrown )
			{
				ocultarLoading();
				var responseText = jqXHR.responseText;
				var mensajeError = obtenerMensajeError( responseText );
				mostrarMensaje( 'Error', mensajeError, 'error' );
			}
		} );
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
    }
    else {
        combo.append($("<option />").val('-1').text('Sin datos'));
    }

    $.each(listaSesionesActivas, function () {
        var horaOfeta = this.Hora;
        var detalleHora = this.DetalleHora;
        combo.append($("<option />").val(horaOfeta).text(detalleHora));
    });
}

function obtenerDiasOfertaMes() {
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
                llenarComboDias(listaDiasOfertaMes);
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

function llenarComboDias(listaDiasOfertaMes) {
    var comboDias = $("#ddlDias");
    comboDias.empty();

    var comboSesiones = $("#ddlHoras");
    comboSesiones.empty();
    comboSesiones.append($("<option />").val('-1').text('Sin datos'));

    var poseeDiasOfertaMes = listaDiasOfertaMes.length > 0;

    if (poseeDiasOfertaMes) {
        comboDias.append($("<option />").val('-1').text('Seleccione'));
    }
    else {
        comboDias.append($("<option />").val('-1').text('Sin datos'));
    }

    $.each(listaDiasOfertaMes, function () {
        var diaOferta = this.DiaOferta;
        var detalleDiaOferta = this.DetalleDiaOferta;
        var fechaDiaOferta = this.FechaDiaOferta;
        comboDias.append($("<option />").val(fechaDiaOferta).text(detalleDiaOferta + ', ' + diaOferta));
    });
}

function limpiarCampos()
{
	$( '#txbMes' ).val( '' );
	$( '#ddlDias' ).val( '' );
	$( '#ddlHoras' ).val( '' );
}