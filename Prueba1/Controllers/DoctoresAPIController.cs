using Microsoft.AspNetCore.Mvc;
using Prueba1.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Prueba1.Controllers
{
    public class DoctoresController : Controller
    {
        private readonly string cs = "Server=localhost\\SQLEXPRESS;Database=PruebaTecnica;Trusted_Connection=True;TrustServerCertificate=True;";

        [Route("Doctores")]
        // LISTAR (sp_SelectMedicos)
        public IActionResult Doctores()
        {
            var listaDoctores = ObtenerDoctores();
            ViewBag.Especialidades = ObtenerEspecialidades();
            return View(listaDoctores);
        }

        private List<Doctor> ObtenerDoctores()
        {
            var lista = new List<Doctor>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SelectMedicos", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Doctor
                    {
                        Id = (int)dr["id"],
                        Nombre = dr["nombre"].ToString(),
                        APaterno = dr["aPaterno"].ToString(),
                        AMaterno = dr["aMaterno"].ToString(),
                        Cedula = dr["cedula"].ToString(),
                        IdEspecialista = (int)dr["idEspecialista"],
                        EspecialidadConcepto = dr["EspecialidadConcepto"].ToString()
                    });
                }
            }
            return lista;
        }


        // EDITAR (sp_UpdateMedico)
        [HttpGet]
        public IActionResult Editar(int id)
        {
            Doctor doctor = new Doctor();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_UpdateMedico", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if ((int)dr["id"] == id)
                    {
                        doctor.Id = (int)dr["id"];
                        doctor.Nombre = dr["nombre"].ToString();
                        doctor.APaterno = dr["aPaterno"].ToString();
                        doctor.AMaterno = dr["aMaterno"].ToString();
                        doctor.Cedula = dr["cedula"].ToString();
                        doctor.IdEspecialista = (int)dr["idEspecialista"];
                        break;
                    }
                }
            }
            return View(doctor);
        }

        [HttpPost]
        public IActionResult Editar(Doctor doctor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_UpdateMedico", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", doctor.Id);
                cmd.Parameters.AddWithValue("@nombre", doctor.Nombre);
                cmd.Parameters.AddWithValue("@aPaterno", doctor.APaterno);
                cmd.Parameters.AddWithValue("@aMaterno", doctor.AMaterno);
                cmd.Parameters.AddWithValue("@cedula", doctor.Cedula);
                cmd.Parameters.AddWithValue("@idEspecialista", doctor.IdEspecialista);

                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // ELIMINAR (sp_DeleteMedico)
        public IActionResult Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_DeleteMedico", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public List<Especialidad> ObtenerEspecialidades()
        {
            var lista = new List<Especialidad>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ConsultaEspecialidades", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Especialidad
                    {
                        Id = (int)dr["IdEspecialidad"],
                        Concepto = dr["concepto"].ToString()
                    });
                }
            }
            return lista;
        }

        [HttpPost]
        public IActionResult Guardar(Doctor doctor)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd;

                if (doctor.Id == 0)
                {
                    // Nuevo registro
                    cmd = new SqlCommand("sp_InsertMedico", con);
                }
                else
                {
                    // Actualización
                    cmd = new SqlCommand("sp_UpdateMedico", con);
                    cmd.Parameters.AddWithValue("@id", doctor.Id);
                }

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nombre", doctor.Nombre);
                cmd.Parameters.AddWithValue("@aPaterno", doctor.APaterno);
                cmd.Parameters.AddWithValue("@aMaterno", doctor.AMaterno);
                cmd.Parameters.AddWithValue("@cedula", doctor.Cedula);
                cmd.Parameters.AddWithValue("@idEspecialista", doctor.IdEspecialista);

                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Doctores");
        }
    }
}
