using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjetoStarter.Data;
using ProjetoStarter.Models;

namespace ProjetoStarter.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {  
    
        private readonly ApplicationDbContext database;

        public UsuariosController(ApplicationDbContext database)
        {
            this.database = database;

        }

        [HttpPost("registro")]
        //api/v1/usuarios/registro
        public IActionResult Registro([FromBody] Usuario usuario)
        {
            database.Add(usuario);
            database.SaveChanges();
            return Ok(new { msg = "Usuário cadastrado com sucesso" });
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] Usuario credenciais)
        {           
            try
            {
                Usuario usuario = database.Usuarios.First(user => user.Email.Equals(credenciais.Email));
                if (usuario != null)
                {
                    // Achou um usuário com cadastro válido
                    if (usuario.Senha.Equals(credenciais.Senha))
                    {
                        //Geração de Token
                        string chaveDeSeguranca = "Gft_melhor_empresa";       // Chave de segurança              
                        var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveDeSeguranca));
                        var credenciaisDeAcesso = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature);
                    
                        var claims = new List<Claim>();
                        claims.Add(new Claim("id", usuario.Id.ToString()));
                        claims.Add(new Claim("email", usuario.Email.ToString()));
                        claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                       


                        
                        var JWT = new JwtSecurityToken(
                            issuer: "starters.com",  //Quem está fornecendo o JWT para o usuário
                            expires: DateTime.Now.AddHours(1),
                            audience: "usuario_comum",
                            signingCredentials: credenciaisDeAcesso,
                            claims: claims   // adicionando claims no token
                        );

                        return Ok(new JwtSecurityTokenHandler().WriteToken(JWT));
                    
                    
                    }
                    else
                    {
                        Response.StatusCode = 401; // Não autorizado // Não existe nenhum usuário com este e-mail
                        return new ObjectResult("");
                    }
                }
                else
                {
                    // Não existe nenhum usuário com este e-mail
                    Response.StatusCode = 401; // Não autorizado
                    return new ObjectResult("");
                }
            }
            catch (Exception)
            {
                // Não existe nenhum usuário com este e-mail
                Response.StatusCode = 401; // Não autorizado
                return new ObjectResult("");
            }
        }
    }
}
    
