$( document ).ready( function ()
{
	ObtenerUsuarios();
} );

function mostrarPopUpMantenimientoOferta()
{
	$( "#hdfAccion" ).val( 'I' );
	$( "#hdfOfertaHorarioId" ).val( 0 );
	$( "#txbDia" ).val( '' );
	$( "#txbHoraInicio" ).val( '' );
	$( "#txbHoraFin" ).val( '' );
	$( '#popUpMantenimientoOfertaHorario' ).modal( 'show' );
}

function mostrarPopUpEliminarOferta( OfertaHorarioId, fecha, horaInicio, horaFin, poseeCitas )
{
	$( "#hdfAccion" ).val( 'E' );
	$( "#hdfOfertaHorarioId" ).val( OfertaHorarioId );
	$( "#txbDia" ).val( '' );
	$( "#txbHoraInicio" ).val( '' );
	$( "#txbHoraFin" ).val( '' );

	$( "#lblAEliminar" ).html( '¿Desea eliminar la oferta de horario Fecha: ' + fecha + ', Hora inicio: ' + horaInicio + ', Hora fin: ' + horaFin + '?' );

	if ( poseeCitas == 'true' )
	{
		$( "#lblTituloPopUp" ).html( 'Eliminar oferta <label style="color:red;">Precaución: la oferta posee citas asociadas</label>' );
	}
	else
	{
		$( "#lblTituloPopUp" ).html( 'Eliminar oferta' );
	}

	$( '#popUpEliminarOferta' ).modal( 'show' );
}

var OfertaHorario = function ()
{
	this.accion = $( '#hdfAccion' ).val();
	this.ofertaHorarioId = $( '#hdfOfertaHorarioId' ).val();
	this.dia = $( '#txbDia' ).val();
	this.horaInicio = $( '#txbHoraInicio' ).val();
	this.horaFin = $( '#txbHoraFin' ).val();
	this.practicanteId = $( '#ddlPracticante' ).val();

	this.obtenerDatos = function ()
	{
		var horaInicioMilitar = obtenerHoraMilitar( this.horaInicio );
		var horaFinMilitar = obtenerHoraMilitar( this.horaFin );

		var datos = JSON.stringify( {
			Accion: this.accion,
			OfertaHorarioId: this.ofertaHorarioId,
			Dia: this.dia,
			HoraInicio: horaInicioMilitar,
			HoraFin: horaFinMilitar,
			UsuarioId: this.practicanteId,
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
		var diaEsValido = this.dia != '';
		var horaInicioEsValida = this.horaInicio != '';
		var horaFinEsValida = this.horaFin != '';
		var practicanteValido = this.practicanteId != '' && this.practicanteId != '-1';

		var horaInicioMilitar = obtenerHoraMilitar( this.horaInicio );
		var horaFinMilitar = obtenerHoraMilitar( this.horaFin );

		var rangoHorasValido = horaFinMilitar > horaInicioMilitar;

		mensajeError = diaEsValido ? mensajeError : ( mensajeError + '<p>Digite el dia.</p>' );
		mensajeError = horaInicioEsValida ? mensajeError : ( mensajeError + '<p>Digite la hora de inicio.</p>' );
		mensajeError = horaFinEsValida ? mensajeError : ( mensajeError + '<p>Digite la hora final.</p>' );
		mensajeError = rangoHorasValido ? mensajeError : ( mensajeError + '<p>La hora inicial debe ser mayor a la hora final.</p>' );
		mensajeError = practicanteValido ? mensajeError : ( mensajeError + '<p>Por favor seleccione un practicante.</p>' );

		var formularioValido = mensajeError == '';

		if ( !formularioValido )
		{
			mostrarMensaje( 'Campos requeridos con *', mensajeError, 'alerta' );
		}

		return formularioValido;
	}
};

function mantenimientoOfertaHorario()
{
	ofertaHorario = new OfertaHorario();
	var envioDatosEsValido = ofertaHorario.validarEnvioDatos();

	if ( envioDatosEsValido )
	{
		var datos = ofertaHorario.obtenerDatos()
		mostrarLoading();

		$.ajax( {
			type: "POST",
			url: '/Practicante/MantenimientoOfertaHorario',
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
					obtenerOfertasPracticante();
					$( '#popUpMantenimientoOfertaHorario' ).modal( 'hide' );
					$( '#popUpEliminarOferta' ).modal( 'hide' );
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

function obtenerUsuariosPorRol()
{
	mostrarLoading();
	var rolPacienteId = 3;

	$.ajax( {
		type: "POST",
		url: '/Usuario/ObtenerUsuariosPorRol',
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		data: JSON.stringify( { rolId: rolPacienteId } ),
		success: function ( data )
		{
			ocultarLoading();
			var datos = $.parseJSON( data );
			var listaUsuarios = datos.ListaUsuarios;
			var esRolPracticante = datos.EsRolPracticante;
			var usuarioId = datos.UsuarioId;
			llenarComboUsuarios( listaUsuarios, esRolPracticante, usuarioId );
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

function llenarComboUsuarios( listaUsuarios, esRolPracticante, usuarioId )
{
	debugger
	var combo = $( "#ddlUsuario" );
	combo.empty();

	var comboPracticante = $( "#ddlPracticante" );
	comboPracticante.empty();

	var poseeDatos = listaUsuarios.length > 0;

	if ( poseeDatos )
	{
		combo.append( $( "<option />" ).val( '-1' ).text( 'Seleccione' ) );
		comboPracticante.append( $( "<option />" ).val( '-1' ).text( 'Seleccione' ) );
	}
	else
	{
		combo.append( $( "<option />" ).val( '-1' ).text( 'Sin datos' ) );
		comboPracticante.append( $( "<option />" ).val( '-1' ).text( 'Sin datos' ) );
	}

	$.each( listaUsuarios, function ( indice, elemento )
	{
		var nombre = elemento.Nombre + ' ' + elemento.Apellidos;
		var usuarioId = elemento.UsuarioId;
		combo.append( $( "<option />" ).val( usuarioId ).text( nombre ) );
		comboPracticante.append( $( "<option />" ).val( usuarioId ).text( nombre ) );
	} );

	if ( esRolPracticante )
	{
		combo.val( usuarioId );
		combo.prop( 'disabled', true );

		comboPracticante.val( usuarioId );
		comboPracticante.prop( 'disabled', true );
	}

	obtenerOfertasPracticante();
}


function obtenerDatosFiltro()
{
	var datos = JSON.stringify( {
		FechaInicio: $( '#txbFechaInicio' ).val() != '' ? $( '#txbFechaInicio' ).val() : '-1',
		FechaFin: $( '#txbFechaFin' ).val() != '' ? $( '#txbFechaFin' ).val() : '-1',
		UsuarioId: $( '#ddlUsuario' ).val() != '' ? $( '#ddlUsuario' ).val() : '-1',
		Apellidos: $( '#txbApellidos' ).val() != '' ? $( '#txbApellidos' ).val() : '-1',
		Identificacion: $( '#txbIdentificacion' ).val() != '' ? $( '#txbIdentificacion' ).val() : '-1'
	} );

	return datos;
}

function validarConsulta()
{
	var fechaInicioValida = $( '#txbFechaInicio' ).val() != '' && $( '#txbFechaInicio' ).val() !== null;
	var fechaFinValida = $( '#txbFechaFin' ).val() != '' && $( '#txbFechaFin' ).val() !== null;
	var rangoFechasValido = comprarFechasInicioFinal( $( '#txbFechaInicio' ).val(), $( '#txbFechaFin' ).val() );

	var mensajeError = '';

	mensajeError = fechaInicioValida ? mensajeError : ( mensajeError + '<p>Digite la fecha inicial.</p>' );
	mensajeError = fechaFinValida ? mensajeError : ( mensajeError + '<p>Digite la fecha final.</p>' );
	mensajeError = rangoFechasValido ? mensajeError : ( mensajeError + '<p>La fecha final debe ser mayor a la fecha inicial.</p>' );

	var formularioValido = mensajeError == '';

	if ( !formularioValido )
	{
		mostrarMensaje( 'Campos requeridos para la consulta', mensajeError, 'alerta' );
	}

	return formularioValido;
}



function ObtenerUsuarios()
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
							  '<th>Nombre</th>' +
                              '<th>Apellidos</th>' +
                              '<th>Identificacion</th>' +
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

		var botonEliminar = '<i class="fa fa-trash-o" style="font-size: x-large;color:red;cursor: pointer;" aria-hidden="true" onclick="mostrarPopUpEliminarOferta(' + item.usuarioId + ',' + nombre + ',' + apellidos + ');"></i>';

		var citasDetalle = item.PoseeCitas ? "<b style='color:red;'>Sí</b>" : "No";

		var fila =
        '<tr>' +
		'<td>' + botonEliminar + '</td>' +
		 '<td>' + item.Nombre + '</td>' +
        '<td>' + item.Apellidos + '</td>' +
        '<td>' + item.Identificacion + '</td>' +
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