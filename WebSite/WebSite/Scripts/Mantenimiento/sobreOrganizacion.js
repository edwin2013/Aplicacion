$( document ).ready( function ()
{
	obtenerInformacionOrganizacion()
} );

function mantenimientoActividad()
{
	actividad = new Actividad();
	actividad.mantenimientoActividad();
}

function mostrarMantenimientoActividadCrear()
{
	$( "#hdfAccion" ).val( 'I' );
	$( "#hdfActividadId" ).val( 0 );
	$( "#txbTitulo" ).val( '' );
	$( "#txbFecha" ).val( '' );
	$( "#txbCupo" ).val( 0 );
	$( "#checkActivo" ).prop( 'checked', true );
	$( "#txaDescripcion" ).val( '' );
	$( "#lblTituloMantenimiento" ).html( 'Crear actividad' );

	$( '#popUpMantenimientoActividad' ).modal( 'show' );
}

function mostrarMantenimientoActividad( actividadId, titulo, fecha, cupo, activo, descripcion )
{
	$( "#hdfAccion" ).val( 'A' );
	$( "#hdfActividadId" ).val( actividadId );
	$( "#txbTitulo" ).val( titulo );
	$( "#txbFecha" ).val( fecha );
	$( "#txbCupo" ).val( cupo );
	$( "#checkActivo" ).prop( 'checked', ( activo == 'true' ) );
	$( "#txaDescripcion" ).val( descripcion );
	$( "#lblTituloMantenimiento" ).html( 'Editar actividad' );

	$( '#popUpMantenimientoActividad' ).modal( 'show' );
}

function mostrarPopUpEliminarActividad( actividadId, titulo )
{
	$( "#hdfAccion" ).val( 'E' );
	$( "#hdfActividadId" ).val( actividadId );
	$( "#txbTitulo" ).val( '' );
	$( "#txbFecha" ).val( '' );
	$( "#txbCupo" ).val( 0 );
	$( "#checkActivo" ).prop( 'checked', false );
	$( "#txaDescripcion" ).val( '' );

	$( "#lblAEliminar" ).html( '¿Desea eliminar la actividad: ' + titulo + '?' );
	$( '#popUpEliminarActividad' ).modal( 'show' );
}

var Actividad = function ()
{
	this.accion = $( '#hdfAccion' ).val();
	this.actividadId = $( '#hdfActividadId' ).val();
	this.titulo = $( '#txbTitulo' ).val();
	this.fecha = $( '#txbFecha' ).val();
	this.cupo = $( '#txbCupo' ).val();
	this.activo = $( "#checkActivo" ).is( ":checked" );
	this.descripcion = $( '#txaDescripcion' ).val();

	this.obtenerDatos = function ()
	{
		var datos = JSON.stringify( {
			Accion: this.accion,
			InformacionId: this.actividadId,
			Fecha: this.fecha,
			Titulo: this.titulo,
			Cupo: this.cupo,
			Descripcion: this.descripcion,
			Activo: this.activo,
			Tipo: 3,
		} );

		return datos;
	}

	this.validarEnvioDatos = function ()
	{
		var esAccionEliminar = this.accion == 'E';
		var envioDatosValido = esAccionEliminar ? true : this.validarFormulario();

		return envioDatosValido;
	}

	this.validarFormulario = function ()
	{
		var mensajeError = '';
		var tituloEsValido = this.titulo != '';
		var fechaEsValido = this.fecha != '';
		var cupoEsValido = this.cupo != '' && this.cupo > 0;
		var descripcionEsValida = this.descripcion != '';

		mensajeError = tituloEsValido ? mensajeError : ( mensajeError + '<p>Digite el titulo.</p>' );
		mensajeError = fechaEsValido ? mensajeError : ( mensajeError + '<p>Seleccione la fecha.</p>' );
		mensajeError = cupoEsValido ? mensajeError : ( mensajeError + '<p>Digite el cupo.</p>' );
		mensajeError = descripcionEsValida ? mensajeError : ( mensajeError + '<p>Digite la descripcion.</p>' );

		var formularioValido = mensajeError == '';

		if ( !formularioValido )
		{
			mostrarMensaje( 'Campos requeridos con *', mensajeError, 'alerta' );
		}

		return formularioValido;
	}

	this.mantenimientoActividad = function ()
	{
		var envioDatosEsValido = this.validarEnvioDatos();

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

function obtenerInformacionOrganizacion()
{
	$.ajax( {
		type: "POST",
		url: '/Mantenimiento/ObtenerInformacion',
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		data: JSON.stringify( { tipo: 3, activo: -1 } ),// 3 SON LOS REGISTROS DE TIPO SOBRE ORGANIZACION.
		success: function ( data )
		{
			ocultarLoading();
			var lista = $.parseJSON( data );
			var contieneElementos = lista.length > 0;

			if ( contieneElementos )
			{
				var informacion = lista[0];
				mostrarInformacion( informacion );
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

function mostrarInformacion( informacion )
{
	var fecha = informacion.Fecha;
	var titulo = informacion.Titulo;
	var descripcion = informacion.Descripcion;

	$( '#txbNombreOrganizacion' ).val( titulo );
	$( '#txaDescripcionOrganizacion' ).val( descripcion );
	$( '#txbFechaFundacion' ).val( fecha );
}