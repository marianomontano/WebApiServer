using Servidor.Models;
using System.Collections.Generic;

namespace Servidor.Services
{
	public interface IContactMapper
	{
		ContactModel BuscarContactoPorId(int id);
		bool EditarContacto(ContactModel contact);
		bool EliminarContacto(int id);
		bool GuardarContacto(ContactModel contact);
		List<ContactModel> ListarTodos();
	}
}