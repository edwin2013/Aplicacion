using Modelo.Usuario;
using Utiles;

namespace WebSite.Models.Seguridad
{
	public class FrontUser
	{
		public static bool TienePermiso(RolesPermisos valor)
		{
			int codigoFuncionalidad = (int)valor;

			return true;
		}
	}
}