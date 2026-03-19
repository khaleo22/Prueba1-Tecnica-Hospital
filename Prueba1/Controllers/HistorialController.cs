using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Prueba1.Models;

namespace Prueba1.Controllers
{
    public class HistorialController : Controller
    {
        private readonly string cs = "Server=localhost\\SQLEXPRESS;Database=PruebaTecnica;Trusted_Connection=True;TrustServerCertificate=True;";

        [HttpGet]
        public IActionResult CitasPaciente(int idPaciente = 0)
        {
            var historial = new List<HistorialCita>();
            var pacientes = new List<Paciente>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                // Obtener lista de pacientes
                SqlCommand cmdPacientes = new SqlCommand("sp_SelectPacientes1", con);
                SqlDataReader drPacientes = cmdPacientes.ExecuteReader();
                while (drPacientes.Read())
                {
                    pacientes.Add(new Paciente
                    {
                        Id = Convert.ToInt32(drPacientes["Id"]),
                        NombreCompleto = drPacientes["NombreCompleto"].ToString()
                    });
                }
                drPacientes.Close();

                // Si se seleccionó un paciente, obtener su historial
                if (idPaciente > 0)
                {
                    SqlCommand cmd = new SqlCommand("sp_HistorialCitasPaciente", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdPaciente", idPaciente);

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        historial.Add(new HistorialCita
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Fecha = Convert.ToDateTime(dr["Fecha"]),
                            Horario = dr["Horario"].ToString(),
                            NombreMedico = dr["NombreMedico"].ToString(),
                            Especialidad = dr["Especialidad"].ToString(),
                            Estatus = Convert.ToInt32(dr["Estatus"])
                        });
                    }
                    dr.Close();
                }
            }

            ViewBag.Pacientes = new SelectList(pacientes, "Id", "NombreCompleto", idPaciente);

            return View(historial);
        }
    }
}
