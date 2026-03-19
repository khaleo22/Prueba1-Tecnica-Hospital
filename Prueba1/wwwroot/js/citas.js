$(document).ready(function () {

    // Validación al agendar cita
    $("#formAgendar").on("submit", function (e) {
        e.preventDefault();

        var paciente = $("#ddlPacientes").val();
        var doctor = $("#ddlMedicos").val();
        var fecha = $("#fecha").val();
        var hora = $("#ddlHoras").val();

        if (!paciente) {
            Swal.fire("Error", "Debe seleccionar un paciente", "error");
            return;
        }
        if (!doctor) {
            Swal.fire("Error", "Debe seleccionar un doctor", "error");
            return;
        }
        if (!fecha) {
            Swal.fire("Error", "Debe seleccionar una fecha", "error");
            return;
        }
        if (!hora) {
            Swal.fire("Error", "Debe seleccionar una hora", "error");
            return;
        }

        // 🔹 Validar cancelaciones antes de enviar
        $.getJSON("/Citas/ContarCancelaciones", { idPaciente: paciente }, function (response) {
            if (response.success) {
                if (response.cancelaciones >= 3) {
                    Swal.fire({
                        icon: 'warning',
                        title: 'Aviso importante',
                        text: 'El paciente ha cancelado ' + response.cancelaciones + ' citas en los últimos 30 días.',
                        confirmButtonText: 'Entendido'
                    }).then(() => {
                        $("#formAgendar")[0].submit();
                    });
                } else {
                    $("#formAgendar")[0].submit();
                }
            } else {
                Swal.fire("Error", response.message, "error");
            }
        });
    });



    // Al cambiar doctor, cargar días disponibles
    $("#ddlMedicos").on("change", function () {
        var idDoctor = $(this).val();

        if (!idDoctor) {
            $("#txtDiasDisponibles").val("");
            return;
        }

        $.getJSON("/Citas/ObtenerDiasDisponibles", { idDoctor: idDoctor }, function (response) {
            if (response.success) {
                $("#txtDiasDisponibles").val(response.dias.join(", "));
            } else {
                Swal.fire("Error", response.message, "error");
            }
        });
    });

    // Al cambiar fecha, cargar horas disponibles
    $("#fecha").on("change", function () {
        var idDoctor = $("#ddlMedicos").val();
        var fecha = $(this).val();

        if (!idDoctor || !fecha) {
            $("#ddlHoras").empty().append('<option value="">Seleccione una hora</option>');
            return;
        }

        $.getJSON("/Citas/ObtenerHorasDisponibles", { idDoctor: idDoctor, fecha: fecha }, function (response) {
            $("#ddlHoras").empty();

            if (response.success) {
                if (response.horas.length === 0) {
                    $("#ddlHoras").append('<option value="">No hay horarios disponibles este día</option>');
                } else {
                    $("#ddlHoras").append('<option value="">Seleccione una hora</option>');
                    $.each(response.horas, function (i, h) {
                        $("#ddlHoras").append($("<option>").text(h).val(h));
                    });
                }
            } else {
                Swal.fire("Error", response.message, "error");
            }
        });
    });

});

// 🔹 Función para abrir modal de cancelación
function abrirModalCancelar(idCita) {
    $("#idCitaCancelar").val(idCita);
    $("#modalCancelar").modal("show");
}
