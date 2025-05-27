using Microsoft.Data.SqlClient;
using System.Data;


namespace GroepswerkTaak1.DAL
{
	public class clsDAL
	{
		public static string ConnectionString { get; } = Properties.Settings.Default.strCNcloud;
			
			
		

		//In de code hieronder werken de using-statements als geneste blokken,
		//zelfs als ze niet expliciet genest zijn.
		//Dit komt doordat elk using-statement automatisch de Dispose-methode aanroept
		//op het bijbehorende object zodra het blok wordt verlaten.
		//Hierdoor worden de resources correct vrijgegeven in de juiste volgorde,
		//alsof ze genest waren.
		//Dit is de klasse die we tijdens de les hebben gebruikt,
		//ik heb de code iets leesbaarder gemaakt
		public static DataTable ExecuteDataTable(string storedProcedureName)
		{
			DataTable dataTable;

			using var cn = new SqlConnection(ConnectionString);
			using var cmd = new SqlCommand();
			cmd.Connection = cn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = storedProcedureName;
			using var dataAdapter = new SqlDataAdapter(cmd);
			try
			{
				dataTable = new DataTable();
				dataAdapter.Fill(dataTable);
			}
			catch (SqlException ex)
			{
				throw new Exception(ex.Message);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				cn.Close();
			}

			return dataTable;
		}

		public static DataTable ExecuteDataTable(string storedProcedureName, ref int nr, params SqlParameter[]? sqlParameters)
		{
			DataTable dataTable;
			var controlValue = string.Empty;

			using var cn = new SqlConnection(ConnectionString);
			using var cmd = new SqlCommand();
			
			cmd.Connection = cn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = storedProcedureName;

			if (sqlParameters != null)
			{
				foreach (var param in sqlParameters)
				{
					cmd.Parameters.Add(param);
					if (param.ParameterName != "@ReturnValue") continue;
					controlValue = "OK";
					cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
				}
			}

			using var dataAdapter = new SqlDataAdapter(cmd);
			
			try
			{
				dataTable = new DataTable();
				dataAdapter.Fill(dataTable);

				if (controlValue == "OK")
				{
					nr = (int)cmd.Parameters["@ReturnValue"].Value;
				}
			}
			catch (SqlException ex)
			{
				throw new Exception(ex.Message);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return dataTable;
		}

		public static SqlDataReader GetData(string storeProcedureName)
		{			
			var cn = new SqlConnection(ConnectionString);
			var cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Connection = cn;
			cmd.CommandText = storeProcedureName;

			try
			{
				cn.Open();				 
			}
			catch (Exception)
			{
				throw new InvalidOperationException("Connection Error");
			}

			return cmd.ExecuteReader();
		}

		public static SqlParameter Parameter(string parameterName, object parameterValue)
		{
			return new SqlParameter(parameterName, parameterValue);
		}
	}
}
