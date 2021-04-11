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
			string consulta = "SELECTALLCONTACTS";
			var tabla = _db.Listar(consulta, null);

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
			string consulta = "SELECTCONTACTBYID";
			Hashtable parametros = new Hashtable();
			parametros.Add("@Id", id);
			ContactModel contact = null;

			var tabla = _db.Listar(consulta, parametros);
			if (tabla.Rows.Count > 0)
			{
				contact = new ContactModel();
				contact.Id = int.Parse(tabla.Rows[0]["ID"].ToString());
				contact.FirstName = tabla.Rows[0]["FIRSTNAME"].ToString();
				contact.LastName = tabla.Rows[0]["LASTNAME"].ToString();
				contact.Company = tabla.Rows[0]["COMPANY"].ToString();
				contact.Email = tabla.Rows[0]["EMAIL"].ToString();
				contact.PhoneNumber = Convert.ToInt32(tabla.Rows[0]["PHONENUMBER"].ToString());
			}

			return contact;
		}

		public bool GuardarContacto(ContactModel contact)
		{
			Hashtable parametros = new Hashtable();
			parametros.Add("@FirstName", contact.FirstName);
			parametros.Add("@LastName", contact.LastName);
			parametros.Add("@Company", contact.Company);
			parametros.Add("@Email", contact.Email);
			parametros.Add("@PhoneNumber", contact.PhoneNumber);

			string consulta = "INSERTCONTACT";

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

			string consulta = "UPDATECONTACT";

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

			string consulta = "DELETECONTACT";

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
