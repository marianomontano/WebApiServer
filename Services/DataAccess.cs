using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Linq;

namespace Services
{
	public class DataAccess : IDataAccess
	{
		private SqlConnection connection = new SqlConnection(@"Data Source =.\; Initial Catalog = COELSA_API; Integrated Security = True");

		public DataTable Listar(string query)
		{
			var table = new DataTable();

			if (connection.State != ConnectionState.Open)
				connection.Open();

			using (connection)
			{
				var command = new SqlCommand(query, connection);
				command.CommandType = CommandType.Text;
				var adapter = new SqlDataAdapter(command);
				adapter.Fill(table);
			}

			return table;
		}

		public int Escribir(string query, Hashtable parametros)
		{
			int resultado;

			if (connection.State != ConnectionState.Open)
				connection.Open();

			using (connection)
			{
				var transaction = connection.BeginTransaction();
				var command = new SqlCommand(query, connection);
				command.CommandType = CommandType.Text;
				command.Transaction = transaction;

				if (parametros != null)
				{
					foreach (string param in parametros.Keys)
					{
						command.Parameters.AddWithValue(param, parametros[param]);
					}
				}

				try
				{
					resultado = command.ExecuteNonQuery();
					transaction.Commit();

					return resultado;
				}
				catch (Exception)
				{
					transaction.Rollback();
					return -1;
				}
			}
		}
	}
}
