var ROLPACIENTE = '3';

$( document ).ready( function ()
{
	usuario = new UsuarioSesion();
	usuario.consultarDatosUsuario();
} );

var UsuarioSesion = function ()
{
	this.password = $( '#txbPasswordCambio' ).val();
	this.passwordConfirmacion = $( '#txbConfirmarPasswordCambio' ).val();

	this.obtenerDatos = function ()
	{
		var datos = JSON.stringify( {
			Password: this.password,
		} );

		return datos;
	}

	this.validarFormulario = function ()
	{
		var mensajeError = '';

		var passwordNoEsVacio = this.password != '';
		var passwordEsValido = this.password.length >= 8;
		var passwordConfirmado = this.password == this.passwordConfirmacion;

		mensajeError = passwordNoEsVacio ? mensajeError : ( mensajeError + '<p>Digite el password.</p>' );
		mensajeError = passwordEsValido ? mensajeError : ( mensajeError + '<p>El password debe ser de al menos 8 caracteres.</p>' );
		mensajeError = passwordConfirmado ? mensajeError : ( mensajeError + '<p>La confirmación del password no coincide.</p>' );

		var formularioValido = mensajeError == '';

		if ( !formularioValido )
		{
			mostrarMensaje( 'Campos requeridos con *', mensajeError, 'alerta' );
		}

		return formularioValido;
	}

	this.consultarDatosUsuario = function ()
	{
		$.ajax( {
			type: "POST",
			url: '/Usuario/ConsultarUsuarioSesion',
			contentType: "application/json; charset=utf-8",
			dataType: "json",
			data: '',
			success: function ( data )
			{
				var usuario = $.parseJSON( data );

				if ( usuario.SolicitarCambioPassword )
				{
					$( '#popUpCambioPassword' ).modal( 'show' );
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

	this.actualizarPassword = function ()
	{
		var envioDatosEsValido = this.validarFormulario();

		if ( envioDatosEsValido )
		{
			var datos = this.obtenerDatos()
			mostrarLoading();

			$.ajax( {
				type: "POST",
				url: '/Usuario/ActualizarPassword',
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
						$( '#popUpCambioPassword' ).modal( 'hide' );
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
};

function actualizarPassword()
{
	usuario = new UsuarioSesion();
	usuario.actualizarPassword();
}


function obtenerHoraMilitar( horaEstandar )
{
	return parseInt( horaEstandar.split( ':' )[0] ) * 100;
}

function mostrarMensaje( headerMensaje, bodyMensaje, tipoMensaje )
{
	var esTipoAlerta = tipoMensaje == 'alerta';
	var esTipoError = tipoMensaje == 'error';
	var esTipoExito = tipoMensaje == 'exito';
	var esTipoInformacion = tipoMensaje == 'informacion';

	if ( esTipoAlerta )
	{
		$( '#popUpMensaje .modal-header' ).css( 'background-color', '#FEEFB3' );
		$( '#headerPopUp' ).css( 'color', '#9F6000' );
	}
	else if ( esTipoError )
	{
		$( '#popUpMensaje .modal-header' ).css( 'background-color', '#FFD2D2' );
		$( '#headerPopUp' ).css( 'color', '#D8000C' );
	}
	else if ( esTipoExito )
	{
		$( '#popUpMensaje .modal-header' ).css( 'background-color', '#DFF2BF' );
		$( '#headerPopUp' ).css( 'color', '#4F8A10' );
	}
	else
	{
		$( '#popUpMensaje .modal-header' ).css( 'background-color', '#BDE5F8' );
		$( '#headerPopUp' ).css( 'color', '#00529B' );
	}

	$( '#headerPopUp' ).html( headerMensaje );
	$( '#divMensajePopUp' ).html( bodyMensaje );
	document.getElementById( "popUpMensaje" ).style.zIndex = 2000;
	$( '#popUpMensaje' ).modal( 'show' );
}

function mostrarLoading()
{

	$( '.modal_loading' ).show();
	$( 'body' ).addClass( "loading" );
}

function ocultarLoading()
{

	window.setTimeout( function ()
	{
		$( 'body' ).removeClass( "loading" );
		$( '.modal_loading' ).hide();
	}, 100 );

}

function validarCorreo( email )
{
	var emailReg = new RegExp( /^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i );
	return emailReg.test( email );
}



function ConfiguracionGrid( grid, columnaOrdenamiento )
{
	var instanciaDatatable = '';
	instanciaDatatable = grid.dataTable( {
		'paging': true,  // Table pagination
		'ordering': true,  // Column ordering 
		'info': true,  // Bottom left status text
		'destroy': true,  // Bottom left status text

		"language": {
			"lengthMenu": "Mostrar _MENU_ registros por página",
			"zeroRecords": "No se encontraron datos",
			"info": "Mostrando la página _PAGE_ de _PAGES_",
			"infoEmpty": "No hay registros disponibles",
			"infoFiltered": "(Filtrado de _MAX_ registros totales)",
			"search": "Buscar:  ",
			"paginate": {
				"previous": "Anterior",
				"next": "Siguiente",

			}
		},
		"order": [[columnaOrdenamiento, "desc"]]
	} );

}

function obtenerFechaActual()
{
	var diaActual = new Date();
	var diaMenorADiez = diaActual.getDate() < 10;
	var mesMenorADiez = ( diaActual.getMonth() + 1 ) < 10;

	var dia = diaMenorADiez ? ( '0' + diaActual.getDate() ) : diaActual.getDate();
	var mes = mesMenorADiez ? '0' + ( diaActual.getMonth() + 1 ) : ( diaActual.getMonth() + 1 );
	var annio = diaActual.getFullYear();


	var formatoFecha = dia + "/" + mes + "/" + annio

	return formatoFecha;
}


function comprarFechasInicioFinal( fechaInicio, fechaFin )
{
	var inicio = convertirValorADate( fechaInicio );
	var fin = convertirValorADate( fechaFin );

	var fechasValidas = ( inicio <= fin );

	return fechasValidas;
}

function convertirValorADate( fechaValor )
{
	var dateElement = fechaValor.split( "/" );
	var dateFormat = dateElement[1] + '/' + dateElement[0] + '/' + dateElement[2];
	var fecha = new Date( dateFormat );

	return fecha;
}


