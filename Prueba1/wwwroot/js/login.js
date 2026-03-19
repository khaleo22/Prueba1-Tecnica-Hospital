class LoginHandler {
    constructor(formSelector) {
        this.form = document.querySelector(formSelector);
        this.init();
    }

    init() {
        this.form.addEventListener("submit", (e) => this.handleSubmit(e));
    }

    async handleSubmit(e) {
        e.preventDefault();

        const usuario = document.getElementById("usuario").value;
        const password = document.getElementById("password").value;

        try {
            const response = await fetch("/api/LoginApi/validar", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ Usuario: usuario, Password: password })
            });

            if (response.ok) {
                const data = await response.json();
                Swal.fire({
                    icon: "success",
                    title: "¡Bienvenido!",
                    text: data.mensaje,
                    confirmButtonColor: "#0078d7"
                }).then(() => {
                    window.location.href = "/Home/Privacy";
                });
            } else {
                const error = await response.json();
                Swal.fire({
                    icon: "error",
                    title: "Error de inicio de sesión",
                    text: error.mensaje,
                    confirmButtonColor: "#d33"
                });
            }
        } catch (err) {
            Swal.fire({
                icon: "warning",
                title: "Problema de conexión",
                text: "No se pudo contactar al servidor",
                confirmButtonColor: "#f39c12"
            });
        }
    }
}

// Inicializar la clase cuando cargue la página
document.addEventListener("DOMContentLoaded", () => {
    new LoginHandler(".login-form");
});
