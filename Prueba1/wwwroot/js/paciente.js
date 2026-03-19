function cargarPaciente(id, nombre, aPaterno, aMaterno, fechaNacimiento, correoElectronico, telefono) {
    document.getElementById("Id").value = id;
    document.getElementById("Nombre").value = nombre;
    document.getElementById("APaterno").value = aPaterno;
    document.getElementById("AMaterno").value = aMaterno;
    document.getElementById("FechaNacimiento").value = fechaNacimiento;
    document.getElementById("CorreoElectronico").value = correoElectronico;
    document.getElementById("Telefono").value = telefono;
}

function limpiarFormulario() {
    document.getElementById("Id").value = "";
    document.getElementById("Nombre").value = "";
    document.getElementById("APaterno").value = "";
    document.getElementById("AMaterno").value = "";
    document.getElementById("FechaNacimiento").value = "";
    document.getElementById("CorreoElectronico").value = "";
    document.getElementById("Telefono").value = "";
}

document.addEventListener("DOMContentLoaded", function () {
    // Botones Editar
    document.querySelectorAll(".edit-btn").forEach(function (btn) {
        btn.addEventListener("click", function () {
            cargarPaciente(
                this.dataset.id,
                this.dataset.nombre,
                this.dataset.apaterno,
                this.dataset.amaterno,
                this.dataset.fecha,
                this.dataset.correo,
                this.dataset.telefono
            );
        });
    });

    // Botón Cancelar
    document.getElementById("btnCancelar").addEventListener("click", function () {
        limpiarFormulario();
    });
});
