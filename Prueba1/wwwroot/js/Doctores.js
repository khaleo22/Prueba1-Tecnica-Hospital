
    // Función para cargar datos en el formulario
    function cargarDoctor(id, nombre, aPaterno, aMaterno, cedula, idEspecialista) {
        document.getElementById("Id").value = id;
    document.getElementById("Nombre").value = nombre;
    document.getElementById("APaterno").value = aPaterno;
    document.getElementById("AMaterno").value = aMaterno;
    document.getElementById("Cedula").value = cedula;
    document.getElementById("IdEspecialista").value = idEspecialista;
    }

    // Función para limpiar el formulario
    function limpiarFormulario() {
        document.getElementById("Id").value = "";
    document.getElementById("Nombre").value = "";
    document.getElementById("APaterno").value = "";
    document.getElementById("AMaterno").value = "";
    document.getElementById("Cedula").value = "";
    document.getElementById("IdEspecialista").selectedIndex = 0;
    }

    // Asignar eventos al cargar la página
    document.addEventListener("DOMContentLoaded", function () {
        // Botones Editar
        document.querySelectorAll(".edit-btn").forEach(function (btn) {
            btn.addEventListener("click", function () {
                cargarDoctor(
                    this.dataset.id,
                    this.dataset.nombre,
                    this.dataset.apaterno,
                    this.dataset.amaterno,
                    this.dataset.cedula,
                    this.dataset.especialidad
                );
            });
        });

    // Botón Cancelar
    document.getElementById("btnCancelar").addEventListener("click", function () {
        limpiarFormulario();
        });
    });

