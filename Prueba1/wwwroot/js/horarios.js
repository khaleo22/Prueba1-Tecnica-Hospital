$(document).ready(function () {

    // Editar Horario
    $(document).on("click", ".btnEditarHorario", function () {
        var id = $(this).data("id");
        var idDoctor = $(this).data("doctor");
        var dia = $(this).data("dia");
        var horaInicio = $(this).data("inicio");
        var horaFin = $(this).data("fin");

        Swal.fire({
            title: 'Editar horario',
            html:
                '<input id="swal-dia" class="swal2-input" value="' + dia + '">' +
                '<input id="swal-inicio" type="time" class="swal2-input" value="' + horaInicio + '">' +
                '<input id="swal-fin" type="time" class="swal2-input" value="' + horaFin + '">',
            focusConfirm: false,
            preConfirm: () => {
                return {
                    dia: document.getElementById('swal-dia').value,
                    horaInicio: document.getElementById('swal-inicio').value,
                    horaFin: document.getElementById('swal-fin').value
                }
            }
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/Horarios/EditarHorario',
                    type: 'POST',
                    data: {
                        id: id,
                        idMedico: idDoctor,
                        dia: result.value.dia,
                        horaInicio: result.value.horaInicio,
                        horaFin: result.value.horaFin
                    },
                    success: function (response) {
                        Swal.fire({
                            icon: response.success ? 'success' : 'error',
                            title: response.success ? 'Éxito' : 'Error',
                            text: response.message
                        }).then(() => {
                            if (response.success) location.reload();
                        });
                    }
                });
            }
        });
    });

    // Eliminar Horario (igual que antes)
    $(document).on("click", ".btnEliminarHorario", function () {
        var id = $(this).data("id");

        Swal.fire({
            title: '¿Eliminar horario?',
            text: "Esta acción no se puede deshacer",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Sí, eliminar'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/Horarios/EliminarHorario',
                    type: 'POST',
                    data: { id: id },
                    success: function (response) {
                        Swal.fire({
                            icon: response.success ? 'success' : 'error',
                            title: response.success ? 'Eliminado' : 'Error',
                            text: response.message
                        }).then(() => {
                            if (response.success) location.reload();
                        });
                    }
                });
            }
        });
    });

});
