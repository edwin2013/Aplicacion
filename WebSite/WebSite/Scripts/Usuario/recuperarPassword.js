



var RecuperarPassword = function ()
{
	this.correo = $( '#txbCorreoRecuperar' ).val();

	this.obtenerDatos = function ()
	{
		var datos = JSON.stringify( {
			Correo: this.correo
		} );

		return datos;
	}

	this.validarFormulario = function ()
	{
		var mensajeError = '';

		var correoEsVacio = this.correo != '';
		var formatoCorreoValido = validarCorreo( this.correo );

		mensajeError = correoEsVacio ? mensajeError : ( mensajeError + '<p>Digite el correo.</p>' );

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

	this.recuperarPassword = function ()
	{
		var envioDatosEsValido = this.validarFormulario();

		if ( envioDatosEsValido )
		{
			var datos = this.obtenerDatos()
			mostrarLoading();

			$.ajax( {
				type: "POST",
				url: '/Login/RecuperarPassword',
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
						mostrarMensaje( 'Éxito', mensaje, 'exito' );
						$( '#popUpRecuperarPassword' ).modal( 'hide' );
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
					mostrarMensaje( 'Error', responseText, 'error' );
				}
			} );
		}
	}
} ;

function recuperarPassword()
{
	recuperacion = new RecuperarPassword();
	recuperacion.recuperarPassword();
}

function mostrarPopUpRecuperarPassword()
{
	$( '#txbCorreoRecuperar' ).val('');
	$( '#popUpRecuperarPassword' ).modal( 'show' );
}