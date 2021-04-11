using System.Collections;
using System.Data;

namespace Services
{
	public interface IDataAccess
	{
		int Escribir(string query, Hashtable parametros);
		DataTable Listar(string query);
	}
}