using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Proyecto2PDS.DTOs;
using Proyecto2PDS.Entities;
using Proyecto2PDS.Services;

namespace Proyecto2PDS.Services;

public class GeneradorTokensJWTService  {        
    private BitacoraService _bitacoraService;
    public GeneradorTokensJWTService(IConfiguration configuration, BitacoraService _bitacoraService) {
        
        this._bitacoraService = _bitacoraService;
    }
    
    public String GenerarToken(UsuarioDTO usuario, String key, int noHoras, int idUsuarioSesion) {
        
        var tokenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));        
        var expiraToken = DateTime.Now.AddHours(noHoras).AddHours(6);
        var identidad = new ClaimsIdentity(new List<Claim>{ 
            new Claim(JwtRegisteredClaimNames.Email, usuario.CorreoElectronico),            
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),            
            new Claim(ClaimTypes.DateOfBirth, DateTime.Now.ToString()),
            new Claim(ClaimTypes.Role, usuario.Puesto),
            new Claim("puesto", usuario.Puesto),
            new Claim("scope", "CursosUIMono"),
            new Claim("idUsuario", usuario.ID.ToString()),
            new Claim("idUsuarioSesion", idUsuarioSesion.ToString()),
            new Claim(JwtRegisteredClaimNames.Name, usuario.nombreCompleto),          
            new Claim(JwtRegisteredClaimNames.NameId, usuario.CorreoElectronico) 
            });

        var credencialesFirma = new SigningCredentials(tokenKey, 
                                    SecurityAlgorithms.HmacSha256Signature);
        var descriptorTokenSeguridad = new SecurityTokenDescriptor {
            Subject = identidad, 
            Expires = expiraToken, 
            IssuedAt =DateTime.Now,
            NotBefore = DateTime.Now,
            SigningCredentials = credencialesFirma
        };

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var securityToken = jwtSecurityTokenHandler.CreateToken(descriptorTokenSeguridad);
        var token = jwtSecurityTokenHandler.WriteToken(securityToken);

        _bitacoraService.RegistrarAccion(
             new UsuarioAccionDTO() {
                IDUsuario = usuario.ID,            
                IDUsuarioSesion = idUsuarioSesion,     
                Accion = "Generación de nuevo Token"
            });
        return token;
    }
    
}