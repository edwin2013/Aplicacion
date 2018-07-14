
//test
function actualizarPaciente()
{
	paciente = new PacienteModelo();
	var envioDatosEsValido = paciente.validarFormulario();

	if ( envioDatosEsValido )
	{
		var datos = paciente.obtenerDatos()
		mostrarLoading();

		$.ajax( {
			type: "POST",
			url: '/Practicante/ActualizarPaciente',
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
					$( '#popUpMostrarInformacionPaciente' ).modal( 'hide' );
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

var PacienteModelo = function ()
{
	this.pacienteId = $( '#hdfPacienteId' ).val();
	this.nombre = $( '#txbNombrePaciente' ).val();
	this.apellidos = $( '#txbApellidosPaciente' ).val();
	this.telefono = $( '#txbTelefonoPaciente' ).val();
	this.correoElectronico = $( '#txbCoreoElectronicoPaciente' ).val();
	this.identificacion = $( '#txbIdentificadorPaciente' ).val();
	this.estadoCivil = $( '#ddlEstadoCivilPaciente' ).val();
	this.nacionalidad = $( '#txbNacionalidadPaciente' ).val();
	this.edad = $( '#txbEdadPaciente' ).val();
	this.hijos = $( '#txbHijosPaciente' ).val();

	this.obtenerDatos = function ()
	{
		var datos = JSON.stringify( {
			PacienteId: this.pacienteId,
			Nombre: this.nombre,
			Apellidos: this.apellidos,
			CorreoElectronico: this.correoElectronico,
			Telefono: this.telefono,
			Nacionalidad: this.nacionalidad,
			Identificacion: this.identificacion,
			EstadoCivil: this.estadoCivil,
			Edad: this.edad,
			CantidadHijos: this.hijos,
		} );

		return datos;
	}

	this.validarFormulario = function ()
	{
		var mensajeError = '';
		var nombreEsValido = this.nombre != '';
		var apellidoEsValido = this.apellidos != '';
		var correoElectronicoEsValido = this.correoElectronico != '';
		var telefonoEsValido = this.telefono != '';
		var nacionalidadEsValido = this.nacionalidad != '';
		var identificacionEsValido = this.identificacion != '';
		var estadoCivilEsValido = this.estadoCivil != 0;
		var edadEsValido = this.edad != '';
		var hijosEsValido = this.hijos != '';

		mensajeError = nombreEsValido ? mensajeError : ( mensajeError + '<p>Digite el nombre.</p>' );
		mensajeError = apellidoEsValido ? mensajeError : ( mensajeError + '<p>Digite los apellidos.</p>' );
		mensajeError = correoElectronicoEsValido ? mensajeError : ( mensajeError + '<p>Digite el correo electronico.</p>' );
		mensajeError = telefonoEsValido ? mensajeError : ( mensajeError + '<p>Digite el telefono.</p>' );
		mensajeError = nacionalidadEsValido ? mensajeError : ( mensajeError + '<p>Digite la nacionalidad.</p>' );
		mensajeError = identificacionEsValido ? mensajeError : ( mensajeError + '<p>Digite los identificacion.</p>' );
		mensajeError = estadoCivilEsValido ? mensajeError : ( mensajeError + '<p>Seleccione el estado civil.</p>' );
		mensajeError = edadEsValido ? mensajeError : ( mensajeError + '<p>Digite la edad del paciente.</p>' );
		mensajeError = hijosEsValido ? mensajeError : ( mensajeError + '<p>Digite la cantidad de hijos del paciente.</p>' );

		var formularioValido = mensajeError == '';

		if ( !formularioValido )
		{
			mostrarMensaje( 'Campos requeridos con *', mensajeError, 'alerta' );
		}

		return formularioValido;
	}
};

function obtenerPaciente( pacienteId )
{
	mostrarLoading();

	$.ajax( {
		type: "POST",
		url: '/Practicante/ObtenerPacientes',
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		data: JSON.stringify( { pacienteId: pacienteId } ),
		success: function ( data )
		{
			ocultarLoading();
			var paciente = $.parseJSON( data );
			llenarFormularioPaciente( paciente );
			$( '#popUpMostrarInformacionPaciente' ).modal( 'show' );

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

function llenarFormularioPaciente( paciente )
{
	$( '#txbNombrePaciente' ).val( paciente.Nombre );
	$( '#txbApellidosPaciente' ).val( paciente.Apellidos );
	$( '#txbTelefonoPaciente' ).val( paciente.Telefono );
	$( '#txbCoreoElectronicoPaciente' ).val( paciente.CorreoElectronico );
	$( '#txbIdentificadorPaciente' ).val( paciente.Identificacion );
	$( '#ddlEstadoCivilPaciente' ).val( paciente.EstadoCivil );
	$( '#txbNacionalidadPaciente' ).val( paciente.Nacionalidad );
	$( '#txbEdadPaciente' ).val( paciente.Edad );
	$( '#txbHijosPaciente' ).val( paciente.CantidadHijos );
	$( '#hdfPacienteId' ).val( paciente.PacienteId )
}