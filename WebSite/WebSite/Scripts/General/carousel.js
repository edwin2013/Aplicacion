
$( document ).ready( function ( elementos )
{
	mostrarTestimonios()
} );

function mostrarCorousel()
{
	$( '#popUpCarouselActividades' ).modal( 'show' );
}

function activarCarouselActividades()
{
	$( "#myCarousel" ).on( "slide.bs.carousel", function ( e )
	{
		var $e = $( e.relatedTarget );
		var idx = $e.index();
		var itemsPerSlide = 3;
		var totalItems = $( ".carousel-item" ).length;

		if ( idx >= totalItems - ( itemsPerSlide - 1 ) )
		{
			var it = itemsPerSlide - ( totalItems - idx );
			for ( var i = 0; i < it; i++ )
			{
				// append slides to end
				if ( e.direction == "left" )
				{
					$( ".carousel-item" )
					  .eq( i )
					  .appendTo( ".carousel-inner" );
				} else
				{
					$( ".carousel-item" )
					  .eq( 0 )
					  .appendTo( $( this ).find( ".carousel-inner" ) );
				}
			}
		}
	} );
}

function mostrarTestimonios()
{
	mostrarLoading();
	$.ajax( {
		type: "POST",
		url: '/Paciente/ObtenerTestimonios',
		data: '',
		dataType: "html",
		success: function ( data )
		{
			ocultarLoading();
			var datos = JSON.parse( data );
			var mensajeError = datos.mensajeError;
			var exitoEnConsulta = mensajeError == '';
			if ( exitoEnConsulta )
			{
				var vista = datos.vistaHtml;
				$( '#divTestimonios' ).empty();
				$( '#divTestimonios' ).html( vista );
				activarCarouselActividades();
			}
			else
			{
				mostrarMensaje( 'Error', mensajeError, 'error' );
			}
		},
		error: function ( data )
		{
			ocultarLoading();
			mostrarMensaje( 'No se pudo realizar la consulta de las actividades', mensajeError, 'error' );
		}
	} );
}

function mostrarMultimediaActividades( informacionId )
{
	mostrarLoading();
	$.ajax( {
		type: "POST",
		url: '/Paciente/ObtenerMultimediaActividades',
		data:  { informacionId: parseInt( informacionId ) } ,
		dataType: "html",
		success: function ( data )
		{
			ocultarLoading();
			var datos = JSON.parse( data );
			var mensajeError = datos.mensajeError;
			var exitoEnConsulta = mensajeError == '';
			if ( exitoEnConsulta )
			{
				var vista = datos.vistaHtml;
				$( '#divMultimediaTestimonios' ).empty();
				$( '#divMultimediaTestimonios' ).html( vista );
				mostrarCorousel();
			}
			else
			{
				mostrarMensaje( 'Error', mensajeError, 'error' );
			}
		},
		error: function ( data )
		{
			ocultarLoading();
			mostrarMensaje( 'Error', 'No se pudo realizar la consulta de las actividades', 'error' );
		}
	} );
}


jssor_1_slider_init = function ()
{
	var jssor_1_SlideshowTransitions = [
	  { $Duration: 1400, x: 0.25, $Zoom: 1.5, $Easing: { $Left: $Jease$.$InWave, $Zoom: $Jease$.$InSine }, $Opacity: 2, $ZIndex: -10, $Brother: { $Duration: 1400, x: -0.25, $Zoom: 1.5, $Easing: { $Left: $Jease$.$InWave, $Zoom: $Jease$.$InSine }, $Opacity: 2, $ZIndex: -10 } },
	  { $Duration: 1500, x: 0.5, $Cols: 2, $ChessMode: { $Column: 3 }, $Easing: { $Left: $Jease$.$InOutCubic }, $Opacity: 2, $Brother: { $Duration: 1500, $Opacity: 2 } },
	  { $Duration: 1500, x: 0.3, $During: { $Left: [0.6, 0.4] }, $Easing: { $Left: $Jease$.$InQuad, $Opacity: $Jease$.$Linear }, $Opacity: 2, $Outside: true, $Brother: { $Duration: 1000, x: -0.3, $Easing: { $Left: $Jease$.$InQuad, $Opacity: $Jease$.$Linear }, $Opacity: 2 } },
	  { $Duration: 1200, x: 0.25, y: 0.5, $Rotate: -0.1, $Easing: { $Left: $Jease$.$InQuad, $Top: $Jease$.$InQuad, $Opacity: $Jease$.$Linear, $Rotate: $Jease$.$InQuad }, $Opacity: 2, $Brother: { $Duration: 1200, x: -0.1, y: -0.7, $Rotate: 0.1, $Easing: { $Left: $Jease$.$InQuad, $Top: $Jease$.$InQuad, $Opacity: $Jease$.$Linear, $Rotate: $Jease$.$InQuad }, $Opacity: 2 } },
	  { $Duration: 1600, x: 1, $Rows: 2, $ChessMode: { $Row: 3 }, $Easing: { $Left: $Jease$.$InOutQuart, $Opacity: $Jease$.$Linear }, $Opacity: 2, $Brother: { $Duration: 1600, x: -1, $Rows: 2, $ChessMode: { $Row: 3 }, $Easing: { $Left: $Jease$.$InOutQuart, $Opacity: $Jease$.$Linear }, $Opacity: 2 } },
	  { $Duration: 1600, y: -1, $Cols: 2, $ChessMode: { $Column: 12 }, $Easing: { $Top: $Jease$.$InOutQuart, $Opacity: $Jease$.$Linear }, $Opacity: 2, $Brother: { $Duration: 1600, y: 1, $Cols: 2, $ChessMode: { $Column: 12 }, $Easing: { $Top: $Jease$.$InOutQuart, $Opacity: $Jease$.$Linear }, $Opacity: 2 } },
	  { $Duration: 1200, y: 1, $Easing: { $Top: $Jease$.$InOutQuart, $Opacity: $Jease$.$Linear }, $Opacity: 2, $Brother: { $Duration: 1200, y: -1, $Easing: { $Top: $Jease$.$InOutQuart, $Opacity: $Jease$.$Linear }, $Opacity: 2 } },
	  { $Duration: 1500, x: -0.1, y: -0.7, $Rotate: 0.1, $During: { $Left: [0.6, 0.4], $Top: [0.6, 0.4], $Rotate: [0.6, 0.4] }, $Easing: { $Left: $Jease$.$InQuad, $Top: $Jease$.$InQuad, $Opacity: $Jease$.$Linear, $Rotate: $Jease$.$InQuad }, $Opacity: 2, $Brother: { $Duration: 1000, x: 0.2, y: 0.5, $Rotate: -0.1, $Easing: { $Left: $Jease$.$InQuad, $Top: $Jease$.$InQuad, $Opacity: $Jease$.$Linear, $Rotate: $Jease$.$InQuad }, $Opacity: 2 } },
	  { $Duration: 1600, x: -0.2, $Delay: 40, $Cols: 12, $During: { $Left: [0.4, 0.6] }, $SlideOut: true, $Formation: $JssorSlideshowFormations$.$FormationStraight, $Assembly: 260, $Easing: { $Left: $Jease$.$InOutExpo, $Opacity: $Jease$.$InOutQuad }, $Opacity: 2, $Outside: true, $Round: { $Top: 0.5 }, $Brother: { $Duration: 1000, x: 0.2, $Delay: 40, $Cols: 12, $Formation: $JssorSlideshowFormations$.$FormationStraight, $Assembly: 1028, $Easing: { $Left: $Jease$.$InOutExpo, $Opacity: $Jease$.$InOutQuad }, $Opacity: 2, $Round: { $Top: 0.5 } } },
	  { $Duration: 700, $Opacity: 2, $Brother: { $Duration: 1000, $Opacity: 2 } },
	  { $Duration: 1200, x: 1, $Easing: { $Left: $Jease$.$InOutQuart, $Opacity: $Jease$.$Linear }, $Opacity: 2, $Brother: { $Duration: 1200, x: -1, $Easing: { $Left: $Jease$.$InOutQuart, $Opacity: $Jease$.$Linear }, $Opacity: 2 } }
	];

	var jssor_1_options = {
		$AutoPlay: false,
		$FillMode: 5,
		$SlideshowOptions: {
			$Class: $JssorSlideshowRunner$,
			$Transitions: jssor_1_SlideshowTransitions,
			$TransitionsOrder: 1
		},
		$ArrowNavigatorOptions: {
			$Class: $JssorArrowNavigator$
		},
		$BulletNavigatorOptions: {
			$Class: $JssorBulletNavigator$
		}
	};

	var jssor_1_slider = new $JssorSlider$( "jssor_1", jssor_1_options );

	/*#region responsive code begin*/

	var MAX_WIDTH = 600;

	function ScaleSlider()
	{
		var containerElement = jssor_1_slider.$Elmt.parentNode;
		var containerWidth = containerElement.clientWidth;

		if ( containerWidth )
		{

			var expectedWidth = Math.min( MAX_WIDTH || containerWidth, containerWidth );

			jssor_1_slider.$ScaleWidth( expectedWidth );
		}
		else
		{
			window.setTimeout( ScaleSlider, 30 );
		}
	}

	ScaleSlider();

	$Jssor$.$AddEvent( window, "load", ScaleSlider );
	$Jssor$.$AddEvent( window, "resize", ScaleSlider );
	$Jssor$.$AddEvent( window, "orientationchange", ScaleSlider );
	/*#endregion responsive code end*/
};