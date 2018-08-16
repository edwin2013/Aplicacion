$( document ).ready( function ()
{
	obtenerInformacionOrganizacion()
} );

function multimediaSobreOrganizacion()
{
	var informacionId = $( '#hdfActividadId' ).val();
	mostrarPopUpMultimedia( informacionId );
}

function mostrarMultimediaSobreOrganizacion( informacionId )
{
	$.ajax( {
		type: "POST",
		url: '/Mantenimiento/MostrarMultimedia',
		data: { informacionId: parseInt( informacionId ) },
		dataType: "html",
		success: function ( data )
		{
			var datos = JSON.parse( data );
			var mensajeError = datos.mensajeError;
			var exitoEnConsulta = mensajeError == '';
			var poseeDatos = datos.poseeDatos;

			if ( exitoEnConsulta && poseeDatos )
			{
				var vista = datos.vistaHtml;
				$( '#multimediaSobreOrganizacion' ).empty();
				$( '#multimediaSobreOrganizacion' ).html( vista );
			}
			else if ( !exitoEnConsulta )
			{
				mostrarMensaje( 'Error', mensajeError, 'error' );
			}
			else if ( !poseeDatos )
			{
				$( '#multimediaSobreOrganizacion' ).empty();
			}
		},
		error: function ( data )
		{
			ocultarLoading();
			mostrarMensaje( 'Error', 'No se pudo realizar la consulta', 'error' );
		}
	} );
}


function obtenerInformacionOrganizacion()
{
	$.ajax( {
		type: "POST",
		url: '/Mantenimiento/ObtenerInformacion',
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		data: JSON.stringify( { tipo: 1, activo: -1 } ),// 1 SON LOS REGISTROS DE TIPO SOBRE ORGANIZACION.
		success: function ( data )
		{
			var lista = $.parseJSON( data );
			var contieneElementos = lista.length > 0;

			if ( contieneElementos )
			{
				$( '#hdfAccion' ).val( 'A' );//Actualizar la informacion
				var informacion = lista[0];
				mostrarInformacion( informacion );
			}
			else
			{
				$( '#hdfAccion' ).val( 'I' );//Ingresar informacion
				$( '#hdfActividadId' ).val( 0 );
			}
		},
		error: function ( jqXHR, textStatus, errorThrown )
		{
			var responseText = jqXHR.responseText;
			var mensajeError = obtenerMensajeError( responseText );
			mostrarMensaje( 'Error', mensajeError, 'error' );
		}
	} );
}

function mostrarInformacion( informacion )
{
	var fecha = informacion.Fecha;
	var titulo = informacion.Titulo;
	var descripcion = informacion.Descripcion;

    $('#hdfActividadId').val(informacion.InformacionId);
	$( '#txbNombreOrganizacion' ).val( titulo );
	$( '#txaDescripcionOrganizacion' ).val( descripcion );
	$( '#txbFechaFundacion' ).val( fecha );

	mostrarMultimediaSobreOrganizacion( informacion.InformacionId );
}

function mantenimientoOrganizacion()
{
	sobreOrganizacion = new SobreOrganizacion();
	sobreOrganizacion.mantenimientoSobreOrganizacion();
}

var SobreOrganizacion = function ()
{
	this.accion = $( '#hdfAccion' ).val();
	this.actividadId = $( '#hdfActividadId' ).val();
	this.nombre = $( '#txbNombreOrganizacion' ).val();
	this.fecha = $( '#txbFechaFundacion' ).val();
	this.descripcion = $( '#txaDescripcionOrganizacion' ).val();

	this.obtenerDatos = function ()
	{
		var datos = JSON.stringify( {
			Accion: this.accion,
			InformacionId: this.actividadId,
			Fecha: this.fecha,
			Titulo: this.nombre,
			Cupo: 0,
			Descripcion: this.descripcion,
			Activo: true,
			Tipo: 1,// 1 SON LOS REGISTROS DE TIPO SOBRE ORGANIZACION.
		} );

		return datos;
	}

	this.validarFormulario = function ()
	{
		var mensajeError = '';
		var nombreEsValido = this.nombre != '';
		var fechaEsValido = this.fecha != '';
		var descripcionEsValida = this.descripcion != '';

		mensajeError = nombreEsValido ? mensajeError : ( mensajeError + '<p>Digite el nombre.</p>' );
		mensajeError = fechaEsValido ? mensajeError : ( mensajeError + '<p>Seleccione la fecha.</p>' );
		mensajeError = descripcionEsValida ? mensajeError : ( mensajeError + '<p>Digite la descripcion.</p>' );

		var formularioValido = mensajeError == '';

		if ( !formularioValido )
		{
			mostrarMensaje( 'Campos requeridos con *', mensajeError, 'alerta' );
		}

		return formularioValido;
	}

	this.mantenimientoSobreOrganizacion = function ()
	{
		var envioDatosEsValido = this.validarFormulario();

		if ( envioDatosEsValido )
		{
			var datos = this.obtenerDatos()
			mostrarLoading();

			$.ajax( {
				type: "POST",
				url: '/Mantenimiento/MantenimientoInformacion',
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
						obtenerActividades();
						$( '#popUpMantenimientoActividad' ).modal( 'hide' );
						$( '#popUpEliminarActividad' ).modal( 'hide' );
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



