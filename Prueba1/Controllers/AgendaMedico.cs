using Microsoft.AspNetCore.Mvc;
using Prueba1.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Prueba1.Controllers
{
    public class AgendaMedico : Controller
    {
        private readonly string cs = "Server=localhost\\SQLEXPRESS;Database=PruebaTecnica;Trusted_Connection=True;TrustServerCertificate=True;";

        [HttpGet]
        public IActionResult AgendaMedicos(int idMedico, DateTime fecha)
        {
            var citas = new List<Cita>();
            var medicos = new List<Medico>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                // Obtener lista de médicos
                SqlCommand cmdMedicos = new SqlCommand("SELECT Id, Nombre + ' ' + Apaterno AS NombreCompleto FROM Medico", con);
                SqlDataReader drMedicos = cmdMedicos.ExecuteReader();
                while (drMedicos.Read())
                {
                    medicos.Add(new Medico
                    {
                        Id = Convert.ToInt32(drMedicos["Id"]),
                        NombreCompleto = drMedicos["NombreCompleto"].ToString()
                    });
                }
                drMedicos.Close(); 

            
                SqlCommand cmd = new SqlCommand("sp_SelectCitasPorMedico", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdMedico", idMedico);
                cmd.Parameters.AddWithValue("@Fecha", fecha);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    citas.Add(new Cita
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Fecha = Convert.ToDateTime(dr["Fecha"]),
                        Horario = dr["Horario"].ToString(),
                        Estatus = Convert.ToInt32(dr["Estatus"]),
                        NombrePaciente = dr["NombrePaciente"].ToString(),
                        Especialidad = dr["Especialidad"].ToString()
                    });
                }
                dr.Close();
            }

            citas = citas.OrderBy(c => TimeSpan.Parse(c.Horario.Split('-')[0].Trim())).ToList();

            ViewBag.Medicos = new SelectList(medicos, "Id", "NombreCompleto", idMedico);

            return View(citas);
        }
    }
}
