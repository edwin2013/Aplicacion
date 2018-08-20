$(document).ready(function () {
    var fechaActual = obtenerFechaActual();
    $('#txbFechaFin').val(fechaActual);
    $('#txbFechaInicio').val(fechaActual);

    obtenerUsuariosPorRol();
});

function mostrarPopUpEditarCita(citaId, recomendaciones, antecedentes, paciente, correoPaciente, identificadorGUID) {
    $('#txaAntecedentes').val(antecedentes);
    $('#txaRecomendaciones').val(recomendaciones);
    $('#hdfCitaId').val(citaId);
    $('#hdfAccion').val('A');
    $('#hdfCorreoPaciente').val(correoPaciente);
    $('#hdfNombrePaciente').val(paciente);
    $('#hdfIdentificadorGUID').val(identificadorGUID);
    $('#tituloPopUp').html('Cita del paciente ' + paciente);

    $('#popUpEditarCita').modal('show');
}

function mostrarPopUpEliminarCita(citaId, fecha, hora, paciente) {
    $('#txaAntecedentes').val('');
    $('#txaRecomendaciones').val('');
    $('#hdfCitaId').val(citaId);
    $('#hdfAccion').val('E');
    $('#hdfCorreoPaciente').val('');
    $('#lblAEliminar').html('¿Desea eliminar la cita del paciente ' + paciente + ' el dia ' + fecha + ', a las ' + hora + '  horas?');

    $('#popUpEliminarCita').modal('show');
}

function mostrarPopUpInformacionPaciente(pacienteId, citaId) {
    $('#hdfCitaId').val(citaId);
    obtenerPaciente(pacienteId);//ESTA FUNCION SE ENCUENTRA EN EL ARCHIVO ACTUALIZARPACIENTE.JS
}

var CitaMantenimientoModelo = function () {
    this.accion = $('#hdfAccion').val();
    this.citaId = $('#hdfCitaId').val();
    this.correo = $('#hdfCorreoPaciente').val();
    this.recomendaciones = $('#txaRecomendaciones').val();
    this.antecedentes = $('#txaAntecedentes').val();
    this.nombre = $('#hdfNombrePaciente').val();
    this.identificadorGUID = $('#hdfIdentificadorGUID').val();

    this.obtenerDatos = function () {
        var datos = JSON.stringify({
            Accion: this.accion,
            CitaId: this.citaId,
            CorreoElectronico: this.correo,
            Paciente: this.nombre,
            IdentificadorGUID: this.identificadorGUID,
            Antecedentes: this.antecedentes,
            Recomendaciones: this.recomendaciones,
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
        var antecedenteEsValido = this.antecedentes != '';
        var recomandacionEsValida = this.recomendaciones != '';

        mensajeError = recomandacionEsValida ? mensajeError : (mensajeError + '<p>Digite las recomendaciones.</p>');
        mensajeError = antecedenteEsValido ? mensajeError : (mensajeError + '<p>Digite los antecedentes.</p>');

        var formularioValido = mensajeError == '';

        if (!formularioValido) {
            mostrarMensaje('Campos requeridos con *', mensajeError, 'alerta');
        }

        return formularioValido;
    }
};

function mantenimientoCita() {
    citaMantenimientoModelo = new CitaMantenimientoModelo();
    var envioDatosEsValido = citaMantenimientoModelo.validarEnvioDatos();

    if (envioDatosEsValido) {
        var datos = citaMantenimientoModelo.obtenerDatos()
        mostrarLoading();

        $.ajax({
            type: "POST",
            url: '/Practicante/MantenimientoCita',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: datos,
            success: function (data) {
                obtenerCitasPracticante();
                ocultarLoading();
                var respuesta = $.parseJSON(data);
                var exito = respuesta.Exito;
                var mensaje = respuesta.Respuesta;
                if (exito) {
                    $('#popUpEditarCita').modal('hide');
                    $('#popUpEliminarCita').modal('hide');
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

function obtenerDatosFiltro() {
    var datos = JSON.stringify({
        FechaInicio: $('#txbFechaInicio').val() != '' ? $('#txbFechaInicio').val() : '-1',
        FechaFin: $('#txbFechaFin').val() != '' ? $('#txbFechaFin').val() : '-1',
        Apellidos: $('#txbApellidos').val() != '' ? $('#txbApellidos').val() : '-1',
        Identificacion: $('#txbIdentificacion').val() != '' ? $('#txbIdentificacion').val() : '-1',
        UsuarioId: $('#ddlUsuario').val() != '' ? $('#ddlUsuario').val() : '-1'
    });

    return datos;
}

function validarConsulta() {
    var fechaInicioValida = $('#txbFechaInicio').val() != '' && $('#txbFechaInicio').val() !== null;
    var fechaFinValida = $('#txbFechaFin').val() != '' && $('#txbFechaFin').val() !== null;
    var rangoFechasValido = comprarFechasInicioFinal($('#txbFechaInicio').val(), $('#txbFechaFin').val());
    var practicanteValido = $('#ddlUsuario').val() != '';

    var mensajeError = '';

    mensajeError = fechaInicioValida ? mensajeError : (mensajeError + '<p>Digite la fecha inicial.</p>');
    mensajeError = fechaFinValida ? mensajeError : (mensajeError + '<p>Digite la fecha final.</p>');
    mensajeError = rangoFechasValido ? mensajeError : (mensajeError + '<p>La fecha final debe ser mayor a la fecha inicial.</p>');
    mensajeError = practicanteValido ? mensajeError : (mensajeError + '<p>Por favor seleccione un practicante.</p>');

    var formularioValido = mensajeError == '';

    if (!formularioValido) {
        mostrarMensaje('Campos requeridos para la consulta', mensajeError, 'alerta');
    }

    return formularioValido;
}

function obtenerCitasPracticante() {
    var envioDatosEsValido = validarConsulta();

    if (envioDatosEsValido) {
        var datos = obtenerDatosFiltro()
        mostrarLoading();

        $.ajax({
            type: "POST",
            url: '/Practicante/ObtenerCitasPracticante',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: datos,
            success: function (data) {
                ocultarLoading();
                var lista = $.parseJSON(data);
                crearGridCitas(lista);
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

function crearGridCitas(lista) {
    var divContenedor = $('#divGridCitas');
    divContenedor.empty();

    var tabla = '<table id="gridCitas" class="table table-striped table-hover table-bordered"  >' +
        '<thead>' +
        '<tr>' +
        '<th></th>' +
        '<th></th>' +
        '<th></th>' +
        '<th></th>' +
        '<th>Paciente</th>' +
        '<th>Practicante</th>' +
        '<th>Fecha</th>' +
        '<th>Hora</th>' +
        '<th>Identificacion</th>' +
        '<th>Telefono</th>' +
        '<th>Estado cita</th>' +


        '</tr>' +
        '</thead>' +
        '<tbody>' +
        '</tbody>' +
        '</table>';

    divContenedor.append(tabla);
    var tBody = divContenedor.children();

    $.each(lista, function (index, item) {
       
        var fecha = "'" + item.FechaCita + "'";
        var hora = "'" + item.HoraCita + "'";
        var horaEntero = "'" + item.HoraEntero + "'";
        var paciente = "'" + item.Paciente + "'";
        var practicante = "'" + item.Practicante + "'";
        var pacienteId = "'" + item.PacienteId + "'";
        var citaId = "'" + item.CitaId + "'";
        var identificacion = "'" + item.Identificacion + "'";
        var telefono = "'" + item.Telefono + "'";
        var correo = "'" + item.CorreoElectronico + "'";
        var estado = "'" + item.EstadoCita + "'";
        var recomendaciones = "'" + item.Recomendaciones + "'";
        var antecedentes = "'" + item.Antecedentes + "'";
        var identificadorGUID = "'" + item.IdentificadorGUID + "'";

        var botonEditar = '<i class="fa fa-pencil-square-o AgregarRecomendacionesPaciente" style="font-size: x-large; cursor: pointer;" aria-hidden="true" onclick="mostrarPopUpEditarCita(' + item.CitaId + ',' +
            recomendaciones + ',' +
            antecedentes + ',' +
            paciente + ',' +
            correo + ',' +
            identificadorGUID +
            ');"></i>';

        var botonEliminar =
            '<i class="fa fa-trash-o EliminarCitasPaciente" style="font-size: x-large;color:red;cursor: pointer;" aria-hidden="true" onclick="mostrarPopUpEliminarCita(' +
            item.CitaId + ',' +
            fecha + ',' +
            hora + ',' +
            paciente + ');"></i>';

        //LA FUNCION mostrarPopUpCambiarHorarioCita SE ENCUENTRA EN EL ARCHIVO cambiarHorarioCita.jsm
        var botonCambiarHorarioCita =
            '<i class="fa fa-clock-o CambiarHorarioCita" style="font-size: x-large;cursor: pointer;" aria-hidden="true" onclick="mostrarPopUpCambiarHorarioCita(' +
            item.CitaId + ',' +
            fecha + ',' +
            hora + ',' +
            paciente + ',' +
            pacienteId + ');"></i>';

        var botonInformacion =
            '<i class="fa fa-file-text-o EditarInformacionPaciente" style="font-size: x-large;cursor: pointer;" aria-hidden="true" onclick="mostrarPopUpInformacionPaciente(' +
            item.PacienteId + ',' +
            citaId +
            ');"></i>';

        var fila =
            '<tr>' +
            '<td>' + botonInformacion + '</td>' +
            '<td>' + botonEditar + '</td>' +
            '<td>' + botonCambiarHorarioCita + '</td>' +
            '<td>' + botonEliminar + '</td>' +
            '<td>' + item.Paciente + '</td>' +
            '<td>' + item.Practicante + '</td>' +
            '<td>' + item.FechaCita + '</td>' +
            '<td>' + item.HoraCita + '</td>' +
            '<td>' + item.Identificacion + '</td>' +
            '<td>' + item.Telefono + '</td>' +
            '<td>' + item.EstadoCita + '</td>' +

            '</tr>';

        tBody.append(fila);
    });

    var poseeDatos = lista.length > 0;

    if (poseeDatos) {
        ConfiguracionGrid($('#gridCitas'), 2);
    }
    else {
        tBody.append('<strong>Sin datos para mostrar</strong>');
    }
}

function obtenerUsuariosPorRol() {
    mostrarLoading();
    var rolPacienteId = 3;

    $.ajax({
        type: "POST",
        url: '/Usuario/ObtenerUsuariosPorRol',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify({ rolId: rolPacienteId }),
        success: function (data) {
            ocultarLoading();
            var datos = $.parseJSON(data);
            var listaUsuarios = datos.ListaUsuarios;
            var esRolPracticante = datos.EsRolPracticante;
            var usuarioId = datos.UsuarioId;
            llenarComboUsuarios(listaUsuarios, esRolPracticante, usuarioId);
            obtenerCitasPracticante();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            ocultarLoading();
            var responseText = jqXHR.responseText;
            var mensajeError = obtenerMensajeError(responseText);
            mostrarMensaje('Error', mensajeError, 'error');
        }
    });

}

function llenarComboUsuarios(listaUsuarios, esRolPracticante, usuarioId) {
    var combo = $("#ddlUsuario");
    combo.empty();

    var poseeDatos = listaUsuarios.length > 0;

    if (poseeDatos) {
        combo.append($("<option />").val('-1').text('Seleccione'));
    }
    else {
        combo.append($("<option />").val('-1').text('Sin datos'));
    }

    $.each(listaUsuarios, function () {
        var nombre = this.Nombre + ' ' + this.Apellidos;
        var usuarioId = this.UsuarioId;
        combo.append($("<option />").val(usuarioId).text(nombre));
    });

    if (esRolPracticante) {
        combo.val(usuarioId);
        combo.prop('disabled', true);
    }
}