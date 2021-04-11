using Servidor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services;
using System.Data;
using System.Collections;

namespace Servidor.Services
{
	public class ContactMapper : IContactMapper
	{
		private IDataAccess _db;

		public ContactMapper(IDataAccess db)
		{
			_db = db;
		}

		public List<ContactModel> ListarTodos()
		{
			List<ContactModel> output = new List<ContactModel>();
			string consulta = "SELECT * FROM CONTACTS";
			var tabla = _db.Listar(consulta);

			foreach (DataRow fila in tabla.Rows)
			{
				var contacto = new ContactModel();
				contacto.Id = int.Parse(fila["ID"].ToString());
				contacto.FirstName = fila["FIRSTNAME"].ToString();
				contacto.LastName = fila["LASTNAME"].ToString();
				contacto.Company = fila["COMPANY"].ToString();
				contacto.Email = fila["EMAIL"].ToString();
				contacto.PhoneNumber = Convert.ToInt32(fila["PHONENUMBER"]);

				output.Add(contacto);
			}

			return output;
		}

		public ContactModel BuscarContactoPorId(int id)
		{
			var lista = ListarTodos();
			var contact = lista.Find(c => c.Id == id);
			if (contact != null)
			{
				return contact;
			}
			else
			{
				return null;
			}
		}

		public bool GuardarContacto(ContactModel contact)
		{
			Hashtable parametros = new Hashtable();
			parametros.Add("@FirstName", contact.FirstName);
			parametros.Add("@LastName", contact.LastName);
			parametros.Add("@Company", contact.Company);
			parametros.Add("@Email", contact.Email);
			parametros.Add("@PhoneNumber", contact.PhoneNumber);

			string consulta = @"INSERT INTO CONTACTOS" +
				"FIRSTNAME, LASTNAME, COMPANY, EMAIL, PHONENUMBER) " +
				"VALUES (@FirstName, @LastName, @Company, @Email, @PhoneNumber)";

			try
			{
				_db.Escribir(consulta, parametros);
				return true;
			}
			catch (Exception)
			{
				return false;
				throw;
			}
		}

		public bool EditarContacto(ContactModel contact)
		{
			Hashtable parametros = new Hashtable();
			parametros.Add("@Id", contact.Id);
			parametros.Add("@FirstName", contact.FirstName);
			parametros.Add("@LastName", contact.LastName);
			parametros.Add("@Company", contact.Company);
			parametros.Add("@Email", contact.Email);
			parametros.Add("@PhoneNumber", contact.PhoneNumber);

			string consulta = @"UPDATE CONTACTS" +
				"SET FIRSTNAME = @FirstName, LASTNAME = @LastName, COMPANY = @Company, EMAIL = @Email, PHONENUMBER = @PhoneNumber" +
				"WHERE ID = @Id";

			try
			{
				_db.Escribir(consulta, parametros);
				return true;
			}
			catch (Exception)
			{
				return false;
				throw;
			}
		}

		public bool EliminarContacto(int id)
		{
			Hashtable parametros = new Hashtable();
			parametros.Add("@Id", id);

			string consulta = "DELETE FROM CONTACTS WHERE ID = @Id";

			try
			{
				_db.Escribir(consulta, parametros);
				return true;
			}
			catch (Exception)
			{
				return false;
				throw;
			}
		}
	}
}
