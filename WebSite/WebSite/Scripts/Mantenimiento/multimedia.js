

$( document ).ready( function ()
{
	$( document ).on( 'change', '.btn-file :file', function ()
	{
		var input = $( this ), label = input.val().replace( /\\/g, '/' ).replace( /.*\//, '' );
		input.trigger( 'fileselect', [label] );

	} );

	$( '.btn-file :file' ).on( 'fileselect', function ( event, label )
	{
		var input = $( this ).parents( '.input-group' ).find( ':text' ),
			log = label;

		if ( input.length )
		{
			input.val( log );
		}

		var extensionInvalida = !( validarExtensionImagen() );
		if ( extensionInvalida )
		{
			$( '#txbImagen' ).val( '' );
			var $image = $( '#img-upload' );
			$image.removeAttr( 'src' ).replaceWith( $image.clone() );
			mostrarMensaje( 'Error', 'Profavor seleccione una imagen valida (jpg, jpeg, png o gif)', 'error' );
		}
	} );

	function readURL( input )
	{
		if ( input.files && input.files[0] )
		{
			var reader = new FileReader();

			reader.onload = function ( e )
			{
				$( '#img-upload' ).attr( 'src', e.target.result );
			}

			reader.readAsDataURL( input.files[0] );
		}
	}

	$( "#imgInp" ).change( function ()
	{
		readURL( this );
	} );
} );


function mantenimientoMultimedia()
{
	multimedia = new Multimedia();
	multimedia.mantenimientoMultimediaInformacion();
}

function establecerPagina( paginaActual )
{
	$( '#hdfPaginaActual' ).val( paginaActual );
}

function validarExtensionImagen()
{
	var ext = $( '#txbImagen' ).val().split( '.' ).pop().toLowerCase();
	var extensionValida = $.inArray( ext, ['gif', 'png', 'jpg', 'jpeg'] ) != -1;

	return extensionValida;
}

function mostrarPopUpMultimedia( informacionId )
{
	$( "#hdfAccion" ).val( 'I' );
	$( "#hdfInformacionId" ).val( informacionId );
	$( "#ddlTipoArchivo" ).val( '-1' );
	$( "#txbUrlVideo" ).val( '' );
	$( "#txbImagen" ).val( '' );
	$( '#hdfMultimediaId' ).val( 0 )

	var $image = $( '#img-upload' );
	$image.removeAttr( 'src' ).replaceWith( $image.clone() );

	$( "#divImagen" ).hide();
	$( "#divVideo" ).hide();

	$( '#popUpMultimedia' ).modal( 'show' );
}

function mostrarPopUpMultimediaEliminar( multimediaId, tipoArchivo )
{
	$( "#hdfAccion" ).val( 'E' );
	$( "#hdfMultimediaId" ).val( multimediaId );
	$( "#ddlTipoArchivo" ).val( '-1' );
	$( "#txbUrlVideo" ).val( '' );
	$( "#txbImagen" ).val( '' );
	$( "#lblAEliminarMultimedia" ).html( '¿Desea eliminar el archivo de tipo ' + tipoArchivo + '?' );

	var $image = $( '#img-upload' );
	$image.removeAttr( 'src' ).replaceWith( $image.clone() );

	$( "#divImagen" ).hide();
	$( "#divVideo" ).hide();

	$( '#popUpEliminarMultimedia' ).modal( 'show' );
}

var Multimedia = function ()
{
	this.accion = $( '#hdfAccion' ).val();
	this.informacionId = $( '#hdfInformacionId' ).val();
	this.multimediaInformacionId = $( '#hdfMultimediaId' ).val();
	this.rutaVideo = $( '#txbUrlVideo' ).val();
	this.tipoArchivo = $( '#ddlTipoArchivo' ).val();
	this.esTipoImagen = this.tipoArchivo == '1';
	this.nombreImagen = $( '#txbImagen' ).val();

	this.obtenerDatos = function ()
	{
		var datos = JSON.stringify( {
			Accion: this.accion,
			MultimediaInformacionId: this.multimediaInformacionId,
			Datos: '',
			Ruta: this.rutaVideo,
			InformacionId: this.informacionId,
			Tipo: this.tipoArchivo
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
		var tipoEsValido = this.tipoArchivo != '-1';
		mensajeError = tipoEsValido ? mensajeError : ( mensajeError + '<p>Seleccione el tipo de archivo.</p>' );

		if ( this.esTipoImagen )
		{
			var imagenEsValida = this.nombreImagen != '';
			mensajeError = imagenEsValida ? mensajeError : ( mensajeError + '<p>Seleccione la imagen.</p>' );
		}
		else
		{
			var rutaVideoEsValida = this.rutaVideo != '';
			mensajeError = rutaVideoEsValida ? mensajeError : ( mensajeError + '<p>Seleccione la ruta del video.</p>' );
		}

		var formularioValido = mensajeError == '';

		if ( !formularioValido )
		{
			mostrarMensaje( 'Campos requeridos con *', mensajeError, 'alerta' );
		}

		return formularioValido;
	}

	this.mantenimientoMultimediaInformacion = function ()
	{
		var envioDatosEsValido = this.validarEnvioDatos();

		if ( envioDatosEsValido )
		{
			var formdata = new FormData();
			var fileInput = document.getElementById( 'imgInp' );
			formdata.append( 'imagen', fileInput.files[0] );
			formdata.append( 'Accion', this.accion );
			formdata.append( 'MultimediaInformacionId', this.multimediaInformacionId );
			formdata.append( 'Ruta', this.rutaVideo );
			formdata.append( 'InformacionId', this.informacionId );
			formdata.append( 'Tipo', this.tipoArchivo );

			mostrarLoading();

			$.ajax( {
				type: "POST",
				url: '/Mantenimiento/MantenimientoMultimediaInformacion',
				dataType: "json",
				data: formdata,
				cache: false,
				contentType: false,
				processData: false,
				success: function ( data )
				{
					ocultarLoading();
					var respuesta = $.parseJSON( data );
					var exito = respuesta.Exito;
					var mensaje = respuesta.Respuesta;
					if ( exito )
					{
						var esAccionEliminar = this.accion == 'E';


						$( '#popUpEliminarMultimedia' ).modal( 'hide' );

						$( '#popUpMultimedia' ).modal( 'hide' );


						mostrarMensaje( 'Éxito', mensaje, 'exito' );
						actualizarMultimedia();
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

function actualizarMultimedia()
{
	var paginaActual = $( '#hdfPaginaActual' ).val();
	var informacionId = $( '#hdfInformacionId' ).val();

	if ( paginaActual == 'sobreOrganizacion' )
	{
		mostrarMultimediaSobreOrganizacion( informacionId );
	}
}

function mostrarTipoArchivo()
{
	var tipoArchivo = $( '#ddlTipoArchivo' ).val();
	var esTipoImagen = tipoArchivo == '1';
	var esTipoVideo = tipoArchivo == '2';

	if ( esTipoImagen )
	{
		$( "#divImagen" ).show();
		$( "#divVideo" ).hide();
	}
	else if ( esTipoVideo )
	{
		$( "#divImagen" ).hide();
		$( "#divVideo" ).show();
	}
}
