
$(document).ready(function () {
    obtenerTestimonios()
});

function mantenimientoTestimonio() {
    testimonio = new Testimonio();
    testimonio.mantenimientoTestimonio();
}

function mostrarMantenimientoTestimonioCrear() {
    $("#hdfAccion").val('I');
    $("#hdfTestimonioId").val(0);
    $("#txbTitulo").val('');
    $("#txbFecha").val('');
    $("#txbCupo").val(0);
    $("#checkActivo").prop('checked', true);
    $("#txaDescripcion").val('');
    $("#lblTituloMantenimiento").html('Crear testimonio');

    $('#popUpMantenimientoTestimonio').modal('show');
}

function mostrarMantenimientoTestimonio(testimonioId, titulo, fecha, activo, descripcion) {
    $("#hdfAccion").val('A');
    $("#hdfTestimonioId").val(testimonioId);
    $("#txbTitulo").val(titulo);
    $("#txbFecha").val(fecha);
    $("#checkActivo").prop('checked', (activo == 'true'));
    $("#txaDescripcion").val(descripcion);
    $("#lblTituloMantenimiento").html('Editar testimonio');

    $('#popUpMantenimientoTestimonio').modal('show');
}

function mostrarPopUpEliminarTestimonio(testimonioId, titulo) {
    $("#hdfAccion").val('E');
    $("#hdfTestimonioId").val(testimonioId);
    $("#txbTitulo").val('');
    $("#txbFecha").val('');
    $("#txbCupo").val(0);
    $("#checkActivo").prop('checked', false);
    $("#txaDescripcion").val('');

    $("#lblAEliminar").html('¿Desea eliminar el testimonio: ' + titulo + '?');
    $('#popUpEliminarTestimonio').modal('show');
}

var Testimonio = function () {
    this.accion = $('#hdfAccion').val();
    this.testimonioId = $('#hdfTestimonioId').val();
    this.titulo = $('#txbTitulo').val();
    this.fecha = $('#txbFecha').val();
    this.activo = $("#checkActivo").is(":checked");
    this.descripcion = $('#txaDescripcion').val();

    this.obtenerDatos = function () {
        var datos = JSON.stringify({
            Accion: this.accion,
            InformacionId: this.testimonioId,
            Fecha: this.fecha,
            Titulo: this.titulo,
            Cupo: 0,
            Descripcion: this.descripcion,
            Activo: this.activo,
            Tipo: 2,
        });

        return datos;
    }

    this.validarEnvioDatos = function () {
        var esAccionEliminar = this.accion == 'E';
        var envioDatosValido = esAccionEliminar ? true : this.validarFormulario();

        return envioDatosValido;
    }

    this.validarFormulario = function () {
        var mensajeError = '';
        var tituloEsValido = this.titulo != '';
        var fechaEsValido = this.fecha != '';
        var descripcionEsValida = this.descripcion != '';

        mensajeError = tituloEsValido ? mensajeError : (mensajeError + '<p>Digite el titulo.</p>');
        mensajeError = fechaEsValido ? mensajeError : (mensajeError + '<p>Seleccione la fecha.</p>');
        mensajeError = descripcionEsValida ? mensajeError : (mensajeError + '<p>Digite la descripcion.</p>');

        var formularioValido = mensajeError == '';

        if (!formularioValido) {
            mostrarMensaje('Campos requeridos con *', mensajeError, 'alerta');
        }

        return formularioValido;
    }

    this.mantenimientoTestimonio = function () {
        var envioDatosEsValido = this.validarEnvioDatos();

        if (envioDatosEsValido) {
            var datos = this.obtenerDatos()
            mostrarLoading();

            $.ajax({
                type: "POST",
                url: '/Mantenimiento/MantenimientoInformacion',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: datos,
                success: function (data) {
                    ocultarLoading();
                    var respuesta = $.parseJSON(data);
                    var exito = respuesta.Exito;
                    var mensaje = respuesta.Respuesta;
                    if (exito) {
                        obtenerTestimonios();
                        $('#popUpMantenimientoTestimonio').modal('hide');
                        $('#popUpEliminarTestimonio').modal('hide');
                        mostrarMensaje('Éxito', mensaje, 'exito');
                    }
                    else {
                        mostrarMensaje('Error', mensaje, 'error');
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    ocultarLoading();
                    var responseText = jqXHR.responseText;
                    var mensajeError = obtenerMensajeError(responseText);
                    mostrarMensaje('Error', mensajeError, 'error');
                }
            });
        }
    }
};

function obtenerTestimonios() {
    $.ajax({
        type: "POST",
        url: '/Mantenimiento/ObtenerInformacion',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ tipo: 2, activo: -1 }),
        success: function (data) {
            ocultarLoading();
            var lista = $.parseJSON(data);
            crearGridUsuarios(lista);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            ocultarLoading();
            var responseText = jqXHR.responseText;
            var mensajeError = obtenerMensajeError(responseText);
            mostrarMensaje('Error', mensajeError, 'error');
        }
    });
}

function crearGridUsuarios(lista) {
    var divContenedor = $('#divGridTestimonios');
    divContenedor.empty();

    var tabla = '<table id="gridTestimonios" class="table table-striped table-hover table-bordered"  >' +
        '<thead>' +
        '<tr>' +
        '<th></th>' +
        '<th></th>' +
        '<th></th>' +
        '<th>Título</th>' +
        '<th>Fecha</th>' +
        '<th>Activo</th>' +
        '<th>Descripción</th>' +

        '</tr>' +
        '</thead>' +
        '<tbody>' +
        '</tbody>' +
        '</table>';

    divContenedor.append(tabla);
    var tBody = divContenedor.children();

    $.each(lista, function (index, item) {
        var fecha = "'" + item.Fecha + "'";
        var descripcion = "'" + item.Descripcion + "'";
        var titulo = "'" + item.Titulo + "'";
        var activo = "'" + item.Activo + "'";
        var tipo = "'" + item.tipo + "'";

        var botonEliminar = '<i class="fa fa-trash-o" style="font-size: x-large;color:red;cursor: pointer;" aria-hidden="true" onclick="mostrarPopUpEliminarTestimonio(' +
            item.InformacionId + ',' +
            titulo +
            ');"></i>';

        var botonAgregarMultimedia = '<i class="fa fa-file-image-o" style="font-size: x-large;cursor: pointer;" aria-hidden="true" onclick="mostrarPopUpMultimedia(' +
            item.InformacionId +
            ');"></i>';

        var botonEditar = '<i class="fa fa-pencil-square-o" style="font-size: x-large; cursor: pointer;" aria-hidden="true" onclick="mostrarMantenimientoTestimonio(' +
            item.InformacionId + ',' +
            titulo + ',' +
            fecha + ',' +
            activo + ',' +
            descripcion +
            ');"></i>';

        var fila =
            '<tr>' +
            '<td>' + botonEditar + '</td>' +
            '<td>' + botonAgregarMultimedia + '</td>' +
            '<td>' + botonEliminar + '</td>' +
            '<td>' + item.Titulo + '</td>' +
            '<td>' + item.Fecha + '</td>' +
            '<td>' + (item.Activo ? "Si" : "No") + '</td>' +
            '<td>' + item.Descripcion + '</td>' +
            '</tr>';

        tBody.append(fila);
    });

    var poseeDatos = lista.length > 0;

    if (poseeDatos) {
        var columnaFecha = 4;
        ConfiguracionGrid($('#gridTestimonios'), columnaFecha);
    }
    else {
        tBody.append('<strong>Sin datos para mostrar</strong>');
    }
}