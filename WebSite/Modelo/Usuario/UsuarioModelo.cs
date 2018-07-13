using System;

namespace Modelo.Usuario
{
	public class UsuarioModelo
	{
		public int UsuarioId { set; get; }
		public string Nombre { set; get; }
		public string Apellidos { set; get; }
		public string Identificacion { set; get; }
		public int RolId { set; get; }
		public string Password { set; get; }
		public int CarreraId { set; get; }
		public DateTime InicioPractica { set; get; }
		public DateTime FinPractica { set; get; }
		public bool Permiso { set; get; }
	}
}
