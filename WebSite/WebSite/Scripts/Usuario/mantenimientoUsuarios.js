$( document ).ready( function ()
{
	obtenerUsuarios();
	obtenerRoles();
	obtenerCarreras();
} );

function mostrarMantenimientoUsuariosCrear()
{
	$( "#hdfAccion" ).val( 'I' );
	$( "#hdfUsuarioId" ).val( 0 );
	$( "#txbNombre" ).val( '' );
	$( "#txbApellidos" ).val( '' );
	$( "#txbPassword" ).val( 'PasswordDefault01' );
	$( "#txbIdentificacion" ).val( '' );
	$( "#ddlRol" ).val( '-1' );
	$( "#ddlCarrera" ).val( '-1' );
	$( "#txbFechaInicio" ).val( '' );
	$( "#txbFechaFin" ).val( '' );
	$( "#divFechas" ).hide();
	$( "#lblTituloMantenimiento" ).html('Crear usuario');

	$( '#popUpMantenimientoUsurios' ).modal( 'show' );
}

function mostrarMantenimeintoUsuariosEditar( UsuarioId, nombre, apellidos, identificacion, rolId, password, carreraId, inicioPractica, finPractica )
{
	$( "#hdfAccion" ).val( 'A' );
	$( "#hdfUsuarioId" ).val( UsuarioId );
	$( "#txbNombre" ).val( nombre );
	$( "#txbApellidos" ).val( apellidos );
	$( "#txbPassword" ).val( password );
	$( "#txbIdentificacion" ).val( identificacion );
	$( "#ddlRol" ).val( rolId );
	$( "#ddlCarrera" ).val( carreraId );
	$( "#lblTituloMantenimiento" ).html( 'Editar usuario' );

	var esRolPracticante = rolId == '3';

	if ( esRolPracticante )
	{
		$( "#txbFechaInicio" ).val( inicioPractica );
		$( "#txbFechaFin" ).val( finPractica );
		$( "#divFechas" ).show();
	}
	else
	{
		$( "#txbFechaInicio" ).val( '' );
		$( "#txbFechaFin" ).val( '' );
		$( "#divFechas" ).hide();
	}

	$( '#popUpMantenimientoUsurios' ).modal( 'show' );
}

function mostrarPopUpEliminarUsuario( usuarioId, nombre, apellidos )
{
	$( "#hdfAccion" ).val( 'E' );
	$( "#hdfUsuarioId" ).val( usuarioId );

	$( "#txbNombre" ).val( '' );
	$( "#txbApellidos" ).val( '' );
	$( "#txbPassword" ).val( 'PasswordDefault01' );
	$( "#txbIdentificacion" ).val( '' );
	$( "#ddlRol" ).val( '-1' );
	$( "#ddlCarrera" ).val( '-1' );
	$( "#txbFechaInicio" ).val( '' );
	$( "#txbFechaFin" ).val( '' );

	$( "#lblAEliminar" ).html( '¿Desea eliminar el usuario: ' + nombre + ' ' + apellidos + '?' );

	$( '#popUpEliminarUsuario' ).modal( 'show' );
}

var Usuario = function ()
{
	this.accion = $( '#hdfAccion' ).val();
	this.usuarioId = $( '#hdfUsuarioId' ).val();
	this.nombre = $( '#txbNombre' ).val();
	this.apellidos = $( '#txbApellidos' ).val();
	this.password = $( '#txbPassword' ).val();
	this.identificacion = $( '#txbIdentificacion' ).val();
	this.rolId = $( '#ddlRol' ).val();
	this.carreraId = $( '#ddlCarrera' ).val();
	this.fechaInicio = $( '#txbFechaInicio' ).val();
	this.fechaFin = $( '#txbFechaFin' ).val();
	this.esRolPracticante = this.rolId == '3';

	var fechaActual = obtenerFechaActual();

	this.obtenerDatos = function ()
	{
		var datos = JSON.stringify( {
			Accion: this.accion,
			UsuarioId: this.usuarioId,
			Nombre: this.nombre,
			Apellidos: this.apellidos,
			Password: this.password,
			RolId: this.rolId,
			CarreraId: this.carreraId,
			Identificacion: this.identificacion,
			InicioPractica: ( this.esRolPracticante ? this.fechaInicio : fechaActual ),
			FinPractica: ( this.esRolPracticante ? this.fechaFin : fechaActual ),
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
		var nombreEsValido = this.nombre != '';
		var apellidosEsValido = this.apellidos != '';
		var passwordEsValido = this.password != '';
		var identificacionEsValida = this.identificacion != '';
		var rolEsValido = this.rolId != '-1' && this.rolId != '';
		var carreraEsValida = this.carreraId != '' && this.carreraId != '-1';

		mensajeError = nombreEsValido ? mensajeError : ( mensajeError + '<p>Digite el nombre.</p>' );
		mensajeError = apellidosEsValido ? mensajeError : ( mensajeError + '<p>Digite los apellidos.</p>' );
		mensajeError = passwordEsValido ? mensajeError : ( mensajeError + '<p>Digite el password.</p>' );
		mensajeError = identificacionEsValida ? mensajeError : ( mensajeError + '<p>Digite la identificación.</p>' );
		mensajeError = rolEsValido ? mensajeError : ( mensajeError + '<p>Seleccione el rol.</p>' );
		mensajeError = carreraEsValida ? mensajeError : ( mensajeError + '<p>Seleccione la carrera.</p>' );



		if ( this.esRolPracticante )
		{
			var fechaInicioEsValida = this.fechaInicio != '';
			var fechaFinEsValida = this.fechaFin != '';
			var rangoHorasValido = comprarFechasInicioFinal( this.fechaInicio, this.fechaFin );

			mensajeError = fechaInicioEsValida ? mensajeError : ( mensajeError + '<p>Seleccione la fecha de incio.</p>' );
			mensajeError = fechaFinEsValida ? mensajeError : ( mensajeError + '<p>Seleccione la fecha final.</p>' );
			mensajeError = rangoHorasValido ? mensajeError : ( mensajeError + '<p>La fecha inicio debe ser mayor a la fecha fin.</p>' );
		}

		var formularioValido = mensajeError == '';

		if ( !formularioValido )
		{
			mostrarMensaje( 'Campos requeridos con *', mensajeError, 'alerta' );
		}

		return formularioValido;
	}
};

function mantenimientoUsuarios()
{
	usuario = new Usuario();
	var envioDatosEsValido = usuario.validarEnvioDatos();

	if ( envioDatosEsValido )
	{
		var datos = usuario.obtenerDatos()
		mostrarLoading();

		$.ajax( {
			type: "POST",
			url: '/Usuario/MantenimientoUsuarios',
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
					obtenerUsuarios();
					$( '#popUpMantenimientoUsurios' ).modal( 'hide' );
					$( '#popUpEliminarUsuario' ).modal( 'hide' );
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

function obtenerRoles()
{
	mostrarLoading();

	$.ajax( {
		type: "POST",
		url: '/Usuario/ObtenerRoles',
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		data: '',
		success: function ( data )
		{
			ocultarLoading();
			var listaRoles = $.parseJSON( data );
			llenarComboRoles( listaRoles );
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

function llenarComboRoles( listaRoles )
{
	var combo = $( "#ddlRol" );
	combo.empty();

	var poseeDatos = listaRoles.length > 0;

	if ( poseeDatos )
	{
		combo.append( $( "<option />" ).val( '-1' ).text( 'Seleccione' ) );
	}
	else
	{
		combo.append( $( "<option />" ).val( '-1' ).text( 'Sin datos' ) );
	}

	$.each( listaRoles, function ( indice, elemento )
	{
		var descripcion = elemento.Descripcion;
		var RolId = elemento.RolId;
		combo.append( $( "<option />" ).val( RolId ).text( descripcion ) );
	} );
}


function obtenerCarreras()
{
	mostrarLoading();

	$.ajax( {
		type: "POST",
		url: '/Usuario/ObtenerCarreras',
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		data: '',
		success: function ( data )
		{
			ocultarLoading();
			var lista = $.parseJSON( data );
			llenarComboCarreras( lista );
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

function llenarComboCarreras( lista )
{
	var combo = $( "#ddlCarrera" );
	combo.empty();

	var poseeDatos = lista.length > 0;

	if ( poseeDatos )
	{
		combo.append( $( "<option />" ).val( '-1' ).text( 'Seleccione' ) );
	}
	else
	{
		combo.append( $( "<option />" ).val( '-1' ).text( 'Sin datos' ) );
	}

	$.each( lista, function ( indice, elemento )
	{
		var nombre = elemento.Nombre;
		var carreraId = elemento.CarreraId;
		combo.append( $( "<option />" ).val( carreraId ).text( nombre ) );
	} );
}

function mostrarFechasPractica()
{
	var rolId = $( "#ddlRol" ).val();
	var esRolPracticante = rolId == 3;

	if ( esRolPracticante )
	{
		$( "#divFechas" ).show();
	}
	else
	{
		$( "#divFechas" ).hide();
	}
}

function obtenerUsuarios()
{
	$.ajax( {
		type: "POST",
		url: '/Usuario/ObtenerUsuariosPorRol',
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		data: JSON.stringify( { rolId: -1 } ),
		success: function ( data )
		{
			ocultarLoading();
			var lista = $.parseJSON( data );
			crearGridUsuarios( lista );
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

function crearGridUsuarios( lista )
{
	var divContenedor = $( '#divGridUsuarios' );
	divContenedor.empty();

	var tabla = '<table id="gridUsuarios" class="table table-striped table-hover table-bordered"  >' +
                              '<thead>' +
                              '<tr>' +
							  '<th></th>' +
							  '<th></th>' +
							  '<th>Nombre</th>' +
                              '<th>Apellidos</th>' +
                              '<th>Rol</th>' +
                              '<th>Inicio practica</th>' +
                              '<th>Fin practica</th>' +

                              '</tr>' +
                              '</thead>' +
                              '<tbody>' +
                             '</tbody>' +
                             '</table>';

	divContenedor.append( tabla );
	var tBody = divContenedor.children();

	$.each( lista, function ( index, item )
	{
		var usuarioId = "'" + item.UsuarioId + "'";
		var nombre = "'" + item.Nombre + "'";
		var apellidos = "'" + item.Apellidos + "'";
		var identificacion = "'" + item.Identificacion + "'";
		var rolId = "'" + item.RolId + "'";
		var password = "'" + item.Password + "'";
		var carreraId = "'" + item.CarreraId + "'";
		var inicioPractica = "'" + item.InicioPractica + "'";
		var finPractica = "'" + item.FinPractica + "'";

		var botonEliminar = '<i class="fa fa-trash-o" style="font-size: x-large;color:red;cursor: pointer;" aria-hidden="true" onclick="mostrarPopUpEliminarUsuario(' +
			item.UsuarioId + ',' +
			nombre + ',' +
			apellidos +
			');"></i>';

		var botonEditar = '<i class="fa fa-pencil-square-o" style="font-size: x-large; cursor: pointer;" aria-hidden="true" onclick="mostrarMantenimeintoUsuariosEditar(' +
		  item.UsuarioId + ',' +
		  nombre + ',' +
		  apellidos + ',' +
		  identificacion + ',' +
		  rolId + ',' +
		  password + ',' +
		  carreraId + ',' +
		  inicioPractica + ',' +
		  finPractica + ',' +
		  ');"></i>';

		var fila =
        '<tr>' +
		'<td>' + botonEditar + '</td>' +
		'<td>' + botonEliminar + '</td>' +
		'<td>' + item.Nombre + '</td>' +
        '<td>' + item.Apellidos + '</td>' +
        '<td>' + item.DescripcionRol + '</td>' +
        '<td>' + item.InicioPractica + '</td>' +
        '<td>' + item.FinPractica + '</td>' +
        '</tr>';

		tBody.append( fila );
	} );

	var poseeDatos = lista.length > 0;

	if ( poseeDatos )
	{
		var columnaFecha = 4;
		ConfiguracionGrid( $( '#gridUsuarios' ), columnaFecha );
	}
	else
	{
		tBody.append( '<strong>Sin datos para mostrar</strong>' );
	}
}