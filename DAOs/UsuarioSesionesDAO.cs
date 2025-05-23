using System;
using System.Data.SqlClient;
using Proyecto2PDS.Entities;

namespace Proyecto2PDS.DAOs
{
    public class UsuarioSesionesDAO
    {
        private readonly CursosContext _context;

        public UsuarioSesionesDAO(CursosContext context)
        {
            _context = context;
        }
        

        public Usuario_Sesion? BuscarSesionActiva(int idUsuario, int horas) {
            var hora = DateTime.Now.AddHours(-horas);
            var sesion = _context.Usuario_Sesions
                    .Where(us => us.IDUsuario == idUsuario && us.FechaInicio >= hora)
                    .OrderByDescending(us => us.FechaInicio)
                    .FirstOrDefault();  
            return sesion;        
        }

        public Usuario_Sesion? GenerarSesion(int idUsuario, string ip) {
            Usuario_Sesion sesion = new Usuario_Sesion() {
                IDUsuario = idUsuario,
                FechaInicio = DateTime.Now,
                FechaUltimoAcceso = DateTime.Now,
                DireccionIP = ip
            };
            _context.Usuario_Sesions.Add(sesion);
            _context.SaveChanges();        
            return sesion;
        }

        public void AsignarTokenSesion(int IDSesion, string token) {
            var sesion = _context.Usuario_Sesions.Find(IDSesion);
            sesion.Token = token;
            _context.SaveChanges();            
        }
    }    
}