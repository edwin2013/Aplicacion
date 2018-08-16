

function consultarInformacionOrganizacion() {
    $.ajax({
        type: "POST",
        url: '/Mantenimiento/ObtenerInformacion',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ tipo: 1, activo: 1 }),// 1 SON LOS REGISTROS DE TIPO SOBRE ORGANIZACION.
        success: function (data) {
            var lista = $.parseJSON(data);
            var contieneElementos = lista.length > 0;

            if (contieneElementos) {
                var informacion = lista[0];
                mostrarInformacionOrganizacion(informacion);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var responseText = jqXHR.responseText;
            var mensajeError = obtenerMensajeError(responseText);
            mostrarMensaje('Error', mensajeError, 'error');
        }
    });
}

function mostrarInformacionOrganizacion(informacion) {
    var fecha = informacion.Fecha;
    var titulo = informacion.Titulo;
    var descripcion = 'Fundada el ' + fecha + ' ,' + informacion.Descripcion;

    $('#lblNombreorganizacion').html(titulo);
    $('#lblDescripcionOrganizacion').html(descripcion);
}