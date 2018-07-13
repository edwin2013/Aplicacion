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