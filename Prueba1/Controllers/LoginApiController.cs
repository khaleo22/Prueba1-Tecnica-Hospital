using Microsoft.AspNetCore.Mvc;
using Prueba1.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Prueba1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginApiController : ControllerBase
    {
        string cs = "Server=localhost\\SQLEXPRESS;Database=PruebaTecnica;Trusted_Connection=True;TrustServerCertificate=True;";

        [HttpPost("validar")]
        public IActionResult Validar([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Usuario) || string.IsNullOrEmpty(request.Password))
                return BadRequest(new { mensaje = "Usuario y contraseña son requeridos" });

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("sp_LoginUsuario", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@usuario", request.Usuario);
                cmd.Parameters.AddWithValue("@password", request.Password);

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    return Ok(new
                    {
                        id = dr["id"],
                        usuario = dr["usuario"],
                        mensaje = "Login correcto"
                    });
                }
                else
                {
                    return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos" });
                }
            }
        }
    }
}
