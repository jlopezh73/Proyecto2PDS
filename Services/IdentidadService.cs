using System.Text;
using System.Security.Cryptography;
using Proyecto2PDS.Entities;
using Proyecto2PDS.DTOs;
using Proyecto2PDS.DAOs;
using System.Threading.Tasks;

namespace Proyecto2PDS.Services;
public class IdentidadService 
{    
    UsuariosDAO _dao;
    ILogger<IdentidadService> _iLogger;
    RespuestaValidacionUsuarioDTO _respuesta;
    SesionesService _sesionesService;
    public IdentidadService(ILogger<IdentidadService> iLogger, 
            UsuariosDAO dao,  
            SesionesService sesionesService,
            RespuestaValidacionUsuarioDTO respuesta){
        this._dao = dao;
        this._iLogger = iLogger;
        this._respuesta = respuesta;
        this._sesionesService = sesionesService;
    }

public async Task<List<UsuarioDTO>> ObtenerTodosLosUsuariosAsync()
        {
            var usuariosAsync =  await _dao.ObtenerTodosAsync();
            var dtos = usuariosAsync.Select(c => new UsuarioDTO
            {
                ID = c.ID,
                CorreoElectronico = c.CorreoElectronico,            
                Nombre = c.Nombre,
                Paterno = c.Paterno,
                Materno = c.Materno,
                Puesto = c.Puesto,
                Activo = c.Activo,
                nombreCompleto = $"{c.Nombre} {c.Paterno} {c.Materno}"
                
            }).ToList();
            return dtos;
        }

    public async Task<RespuestaValidacionUsuarioDTO> 
                    ValidarUsuario(PeticionInicioSesionDTO peticionInicioSesion, String ip)
    {        
        try {
            byte[] encodedPassword = new UTF8Encoding()
                         .GetBytes(peticionInicioSesion.password);
            byte[] hash = ((HashAlgorithm) CryptoConfig
                          .CreateFromName("MD5")).ComputeHash(encodedPassword);
            string passwordMD5 = BitConverter.ToString(hash)   
                .Replace("-", string.Empty)   
                .ToLower();
            peticionInicioSesion.password = passwordMD5;

            var usuario = await _dao.ObtenerPorPeticionAsync(peticionInicioSesion);
            
            if (usuario != null) {
                var usuarioDTO = new UsuarioDTO() {
                    ID = usuario.ID,
                    CorreoElectronico = usuario.CorreoElectronico,
                    nombreCompleto = $"{usuario.Nombre}  {usuario.Paterno} {usuario.Materno}",
                    Puesto = usuario.Puesto,                
                };
                var sesion = _sesionesService.BuscarUltimaSesion(usuarioDTO);
                if (sesion == null) {
                    sesion = _sesionesService.GenerarSesion(usuarioDTO, 
                        ip);
                } 
                _respuesta.token = sesion.Token;
                _respuesta.correcto = true;
                _respuesta.usuario = usuarioDTO;                                
            } else {
                _respuesta.correcto = false;                
            }            
            return _respuesta;
        } catch (Exception e) {
            _iLogger.LogInformation(e.ToString());
            return new RespuestaValidacionUsuarioDTO() { correcto=false};
        }

    }

    public async Task CrearUsuarioAsync(UsuarioDTO usuarioDTO)
    {
        try
        {
            byte[] encodedPassword = new UTF8Encoding()
                         .GetBytes(usuarioDTO.Password);
            byte[] hash = ((HashAlgorithm) CryptoConfig
                          .CreateFromName("MD5")).ComputeHash(encodedPassword);
            string passwordMD5 = BitConverter.ToString(hash)   
                .Replace("-", string.Empty)   
                .ToLower();
            usuarioDTO.Password = passwordMD5;

            var usuario = new Usuario()
            {
                ID = usuarioDTO.ID,
                CorreoElectronico = usuarioDTO.CorreoElectronico,
                Nombre = usuarioDTO.Nombre,
                Paterno = usuarioDTO.Paterno,
                Materno = usuarioDTO.Materno,
                Puesto = usuarioDTO.Puesto,
                Activo = usuarioDTO.Activo
            };
            await _dao.AgregarAsync(usuario);
        }
        catch (Exception e)
        {
            _iLogger.LogError(e.ToString());
        }
    }
    public async Task<UsuarioDTO> ObtenerUsuarioPorIdAsync(int idUsuario)
    {
        try
        {
            var usuario = await _dao.ObtenerPorIdAsync(idUsuario);
            if (usuario != null)
            {
                return new UsuarioDTO
                {
                    ID = usuario.ID,
                    CorreoElectronico = usuario.CorreoElectronico,
                    Nombre = usuario.Nombre,
                    Paterno = usuario.Paterno,
                    Materno = usuario.Materno,
                    Puesto = usuario.Puesto,
                    Activo = usuario.Activo
                };
            }
            return null;
        }
        catch (Exception e)
        {
            _iLogger.LogError(e.ToString());
            return null;
        }
    }

    public async Task ActualizarUsuarioAsync(UsuarioDTO usuarioDTO)
    {
        try
        {
            var usuario = new Usuario()
            {
                ID = usuarioDTO.ID,
                CorreoElectronico = usuarioDTO.CorreoElectronico,
                Nombre = usuarioDTO.Nombre,
                Paterno = usuarioDTO.Paterno,
                Materno = usuarioDTO.Materno,
                Puesto = usuarioDTO.Puesto,
                Activo = usuarioDTO.Activo
            };
            await _dao.ActualizarAsync(usuario);
        }
        catch (Exception e)
        {
            _iLogger.LogError(e.ToString());
        }
    }

    public async Task EliminarUsuarioAsync(int idUsuario)
    {
        try
        {
            await _dao.EliminarAsync(idUsuario);
        }
        catch (Exception e)
        {
            _iLogger.LogError(e.ToString());
        }
    }
}