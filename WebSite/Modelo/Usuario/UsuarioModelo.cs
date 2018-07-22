using System;

namespace Modelo.Usuario
{
	public class UsuarioModelo
	{
		public int UsuarioId { set; get; }
		public char Accion { set; get; }
		public string Nombre { set; get; }
		public string Apellidos { set; get; }
		public string DescripcionRol { set; get; }
		public string Identificacion { set; get; }
		public int RolId { set; get; }
		public string Password { set; get; }
		public int? CarreraId { set; get; }
		public string InicioPractica { set; get; }
		public string FinPractica { set; get; }
        public string Correo { set; get; }
		public bool Permiso { set; get; }
	}
}
