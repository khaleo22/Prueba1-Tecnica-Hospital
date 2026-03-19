using Microsoft.AspNetCore.Mvc;
using Prueba1.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Prueba1.Controllers
{
    public class HorariosController : Controller
    {
        private readonly string cs = "Server=localhost\\SQLEXPRESS;Database=PruebaTecnica;Trusted_Connection=True;TrustServerCertificate=True;";
        [Route("Horarios")]

        public IActionResult Horarios()
        {
            ViewBag.Doctores = ObtenerDoctores();
            var horarios = ObtenerHorarios() ?? new List<HorarioMedico>();
            return View(horarios);
        }

        [HttpPost]
        public IActionResult Guardar(int idMedico, List<string> dias, string horaInicio, string horaFin)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    foreach (var dia in dias)
                    {
                        SqlCommand cmd = new SqlCommand("sp_InsertHorarioMedico", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idMedico", idMedico);
                        cmd.Parameters.AddWithValue("@diaSemana", dia);
                        cmd.Parameters.AddWithValue("@horaInicio", horaInicio);
                        cmd.Parameters.AddWithValue("@horaFin", horaFin);
                        cmd.ExecuteNonQuery();
                    }
                }

                //TempData["SwalMessage"] = "Horario guardado correctamente";
                //TempData["SwalType"] = "success";
            }
            catch (Exception ex)
            {
                TempData["SwalMessage"] = "Error al guardar: " + ex.Message;
                TempData["SwalType"] = "error";
            }

            return RedirectToAction("Index");
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

        private List<HorarioMedico> ObtenerHorarios()
        {
            var lista = new List<HorarioMedico>();
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_SelectHorariosMedico", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new HorarioMedico
                    {
                        Id = (int)dr["id"],
                        IdMedico = (int)dr["idDoctor"],
                        Dia = dr["dia"].ToString(),
                        HoraInicio = dr["horaInicio"].ToString(),
                        HoraFin = dr["horaFinal"].ToString(),
                        NombreMedico = dr["Nombre"].ToString() + " " + dr["APaterno"].ToString()
                    });
                }
            }
            return lista; // nunca null
        }

        [HttpPost]
        public JsonResult EditarHorario(int id, int idMedico, string dia, string horaInicio, string horaFin)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "UPDATE HorarioDoctores SET idDoctor=@idDoctor, dia=@dia, horaInicio=@horaInicio, horaFinal=@horaFinal, estatus=1 WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@idDoctor", idMedico);
                    cmd.Parameters.AddWithValue("@dia", dia);
                    cmd.Parameters.AddWithValue("@horaInicio", horaInicio);
                    cmd.Parameters.AddWithValue("@horaFinal", horaFin);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                return Json(new { success = true, message = "Horario actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al actualizar: " + ex.Message });
            }
        }


        [HttpPost]
        public JsonResult EliminarHorario(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM HorarioDoctores WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                return Json(new { success = true, message = "Horario eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error al eliminar: " + ex.Message });
            }
        }

    }
}
