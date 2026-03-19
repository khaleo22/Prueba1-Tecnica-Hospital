using Microsoft.AspNetCore.Mvc;
using Prueba1.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Prueba1.Controllers
{
    public class PacientesController : Controller
    {
        private readonly string cs = "Server=localhost\\SQLEXPRESS;Database=PruebaTecnica;Trusted_Connection=True;TrustServerCertificate=True;";
        [Route("Pacientes")]
        public IActionResult Paciente()
        {
            List<Paciente> lista = new List<Paciente>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SelectPacientes", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Paciente
                    {
                        Id = (int)dr["id"],
                        Nombre = dr["nombre"].ToString(),
                        APaterno = dr["aPaterno"].ToString(),
                        AMaterno = dr["aMaterno"].ToString(),
                        FechaNacimiento = (DateTime)dr["fechaNacimiento"],
                        CorreoElectronico = dr["correoElectronico"].ToString(),
                        Telefono = dr["telefono"].ToString()
                    });
                }
            }
            return View(lista);
        }

        [HttpPost]
        public IActionResult Guardar(Paciente paciente)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd;

                if (paciente.Id == 0)
                {
                    cmd = new SqlCommand("sp_InsertPaciente", con);
                }
                else
                {
                    cmd = new SqlCommand("sp_UpdatePaciente", con);
                    cmd.Parameters.AddWithValue("@id", paciente.Id);
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", paciente.Nombre);
                cmd.Parameters.AddWithValue("@aPaterno", paciente.APaterno);
                cmd.Parameters.AddWithValue("@aMaterno", paciente.AMaterno ?? "");
                cmd.Parameters.AddWithValue("@fechaNacimiento", paciente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@correoElectronico", paciente.CorreoElectronico);
                cmd.Parameters.AddWithValue("@telefono", paciente.Telefono);

                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Paciente");
        }

        public IActionResult Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DeletePaciente", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Paciente");
        }
    }

}
