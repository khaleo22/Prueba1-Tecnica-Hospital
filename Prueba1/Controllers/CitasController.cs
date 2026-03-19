using Microsoft.AspNetCore.Mvc;
using Prueba1.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Prueba1.Controllers
{
    public class CitasController : Controller
    {
        private readonly string cs = "Server=localhost\\SQLEXPRESS;Database=PruebaTecnica;Trusted_Connection=True;TrustServerCertificate=True;";

        public IActionResult Agendar()
        {
            var pacientes = new List<SelectListItem>();
            var medicos = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                // Pacientes
                SqlCommand cmdPacientes = new SqlCommand("sp_SelectPacientes", con);
                cmdPacientes.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader dr = cmdPacientes.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        pacientes.Add(new SelectListItem
                        {
                            Value = dr["Id"].ToString(),
                            Text = dr["Nombre"].ToString() + " " + dr["APaterno"].ToString()
                        });
                    }
                }

                // Médicos
                SqlCommand cmdMedicos = new SqlCommand("sp_SelectMedicos", con);
                cmdMedicos.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader dr = cmdMedicos.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        medicos.Add(new SelectListItem
                        {
                            Value = dr["Id"].ToString(),
                            Text = dr["Nombre"].ToString() + " " + dr["APaterno"].ToString()
                        });
                    }
                }
            }

            ViewBag.Pacientes = pacientes;
            ViewBag.Medicos = medicos;

            return View(new List<Cita>());

            ViewBag.Pacientes = pacientes;
            ViewBag.Medicos = medicos;

            return View();
        }

        [HttpGet]
        public JsonResult ObtenerHorasDisponibles(int idDoctor, DateTime fecha)
        {
            var horas = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    // 1. Obtener horario del doctor
                    SqlCommand cmdHorario = new SqlCommand(
                        "SELECT horaInicio, horaFinal FROM HorarioDoctores WHERE idDoctor=@idDoctor AND dia=@dia", con);
                    cmdHorario.CommandType = CommandType.Text;
                    cmdHorario.Parameters.AddWithValue("@idDoctor", idDoctor);
                    cmdHorario.Parameters.AddWithValue("@dia", fecha.ToString("dddd")); // Ej: "Monday"
                    SqlDataReader drHorario = cmdHorario.ExecuteReader();

                    if (!drHorario.Read())
                        return Json(new { success = false, message = "El doctor no atiende ese día." });

                    string horaInicio = drHorario["horaInicio"].ToString();
                    string horaFinal = drHorario["horaFinal"].ToString();
                    drHorario.Close();

                    // 2. Obtener duración de cita desde especialidad
                    SqlCommand cmdDuracion = new SqlCommand(
                        "SELECT e.Tiempo FROM Especialidades e INNER JOIN Medico d ON e.IdEspecialidad = d.IdEspecialista WHERE d.Id=@idDoctor", con);
                    cmdDuracion.CommandType = CommandType.Text;
                    cmdDuracion.Parameters.AddWithValue("@idDoctor", idDoctor);
                    int duracion = Convert.ToInt32(cmdDuracion.ExecuteScalar());

                    // 3. Generar intervalos con descanso de 10 min
                    TimeSpan inicio = TimeSpan.Parse(horaInicio);
                    TimeSpan fin = TimeSpan.Parse(horaFinal);

                    while (inicio.Add(TimeSpan.FromMinutes(duracion)) <= fin)
                    {
                        TimeSpan citaFin = inicio.Add(TimeSpan.FromMinutes(duracion));
                        string intervalo = $"{inicio:hh\\:mm} - {citaFin:hh\\:mm}";

                        // 4. Validar si ese intervalo ya está ocupado en Citas
                        SqlCommand cmdValidar = new SqlCommand(
                            "SELECT COUNT(*) FROM Citas WHERE IdMedico=@idDoctor AND Fecha=@fecha AND Horario=@horario AND Estatus=1", con);
                        cmdValidar.Parameters.AddWithValue("@idDoctor", idDoctor);
                        cmdValidar.Parameters.AddWithValue("@fecha", fecha.Date);
                        cmdValidar.Parameters.AddWithValue("@horario", intervalo);

                        int count = Convert.ToInt32(cmdValidar.ExecuteScalar());

                        if (count == 0) // solo agregar si no está ocupado
                        {
                            horas.Add(intervalo);
                        }

                        inicio = citaFin.Add(TimeSpan.FromMinutes(10)); // descanso
                    }
                }
                return Json(new { success = true, horas });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpGet]
        public JsonResult ObtenerDiasDisponibles(int idDoctor)
        {
            var dias = new List<string>();
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(
                        "SELECT dia FROM HorarioDoctores WHERE idDoctor=@idDoctor", con);
                    cmd.Parameters.AddWithValue("@idDoctor", idDoctor);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            dias.Add(dr["dia"].ToString());
                        }
                    }
                }
                return Json(new { success = true, dias });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult GuardarCita(Cita cita)
        {
            try
            {
                int cancelaciones = 0;

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    // Validar cancelaciones en últimos 30 días
                    SqlCommand cmdValidar = new SqlCommand("sp_ContarCancelaciones", con);
                    cmdValidar.CommandType = CommandType.StoredProcedure;
                    cmdValidar.Parameters.AddWithValue("@IdPaciente", cita.IdPaciente);
                    cancelaciones = Convert.ToInt32(cmdValidar.ExecuteScalar());

                    // Guardar cita
                    SqlCommand cmd = new SqlCommand("sp_InsertCita", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdMedico", cita.IdMedico);
                    cmd.Parameters.AddWithValue("@IdPaciente", cita.IdPaciente);
                    cmd.Parameters.AddWithValue("@Fecha", cita.Fecha);
                    cmd.Parameters.AddWithValue("@Horario", cita.Horario);
                    cmd.ExecuteNonQuery();
                }

                TempData["Mensaje"] = "Cita guardada correctamente";

                if (cancelaciones >= 3)
                {
                    TempData["Alerta"] = "El paciente ha cancelado más de 3 citas en los últimos 30 días.";
                }

                return RedirectToAction("Agendar");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al guardar cita: " + ex.Message;
                return RedirectToAction("Agendar");
            }
        }

        [HttpGet]
        public JsonResult ContarCancelaciones(int idPaciente)
        {
            int cancelaciones = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_ContarCancelaciones", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPaciente", idPaciente);
                cancelaciones = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return Json(new { success = true, cancelaciones });
        }


        [HttpGet]
        public IActionResult ConsultarCitas(int idPaciente, DateTime fecha)
        {
            var citas = new List<Cita>();
            var pacientes = new List<SelectListItem>();
            var medicos = new List<SelectListItem>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                // Pacientes
                SqlCommand cmdPacientes = new SqlCommand("sp_SelectPacientes", con);
                cmdPacientes.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader dr = cmdPacientes.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        pacientes.Add(new SelectListItem
                        {
                            Value = dr["Id"].ToString(),
                            Text = dr["Nombre"].ToString() + " " + dr["APaterno"].ToString()
                        });
                    }
                }

                // Médicos
                SqlCommand cmdMedicos = new SqlCommand("sp_SelectMedicos", con);
                cmdMedicos.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader dr = cmdMedicos.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        medicos.Add(new SelectListItem
                        {
                            Value = dr["Id"].ToString(),
                            Text = dr["Nombre"].ToString() + " " + dr["APaterno"].ToString()
                        });
                    }
                }

                // Citas específicas
                SqlCommand cmd = new SqlCommand("sp_SelectCitas", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdPaciente", idPaciente);
                cmd.Parameters.AddWithValue("@Fecha", fecha);

                SqlDataReader drCitas = cmd.ExecuteReader();
                while (drCitas.Read())
                {
                    citas.Add(new Cita
                    {
                        Id = Convert.ToInt32(drCitas["Id"]),
                        Fecha = Convert.ToDateTime(drCitas["Fecha"]),
                        Horario = drCitas["Horario"].ToString(),
                        Estatus = Convert.ToInt32(drCitas["Estatus"]),
                        NombrePaciente = drCitas["NombrePaciente"].ToString(),
                        NombreMedico = drCitas["NombreMedico"].ToString(),
                        Especialidad = drCitas["Especialidad"].ToString()
                    });
                }
            }

            ViewBag.Pacientes = pacientes;
            ViewBag.Medicos = medicos;

            return View("Agendar", citas);
        }

        [HttpPost]
        public IActionResult CancelarCita(int idCita, string motivo)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_CancelarCita", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCita", idCita);
                    cmd.Parameters.AddWithValue("@Motivo", motivo);
                    cmd.ExecuteNonQuery();
                }

                TempData["Mensaje"] = "Cita cancelada correctamente";
                return RedirectToAction("Agendar");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cancelar cita: " + ex.Message;
                return RedirectToAction("Agendar");
            }
        }


    }
}
