using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroepswerkTaak1.Repositories
{
	public class clsDAL
	{

		private static readonly string _ConnectionString = Properties.Settings.Default.strCN;

		public static string ConnectionString
		{
			get { return _ConnectionString; }
		}

		public static DataTable ExecuteDataTable(string storedProcedureName)
		{
			DataTable DT;

			using (SqlConnection CN = new SqlConnection(ConnectionString))
			{
				using (SqlCommand CMD = new SqlCommand())
				{
					CMD.Connection = CN;
					CMD.CommandType = CommandType.StoredProcedure;
					CMD.CommandText = storedProcedureName;
					using (SqlDataAdapter DA = new SqlDataAdapter(CMD))
					{
						try
						{
							DT = new DataTable();
							DA.Fill(DT);
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
							CN.Close();
						}
					}
				}
			}
			return DT;
		}

		public static DataTable ExecuteDataTable(string storedProcedureName, ref int nr, params SqlParameter[] sqlParameters)
		{
			DataTable DT;
			string controlValue = string.Empty;

			using (SqlConnection CN = new SqlConnection(ConnectionString))
			{
				using (SqlCommand CMD = new SqlCommand())
				{
					CMD.Connection = CN;
					CMD.CommandType = CommandType.StoredProcedure;
					CMD.CommandText = storedProcedureName;

					if (sqlParameters != null)
					{
						foreach (SqlParameter param in sqlParameters)
						{
							CMD.Parameters.Add(param);
							if (param.ParameterName == "@ReturnValue")
							{
								controlValue = "OK";
								CMD.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
							}
						}
					}

					using (SqlDataAdapter DA = new SqlDataAdapter(CMD))
					{
						try
						{
							DT = new DataTable();
							DA.Fill(DT);

							if (controlValue == "OK")
							{
								nr = (int)CMD.Parameters["@ReturnValue"].Value;
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
					}
				}
			}
			return DT;
		}

		public static SqlDataReader GetData(string storeProcedureName)
		{
			SqlConnection CN = new SqlConnection(_ConnectionString);
			SqlCommand CMD = new SqlCommand();
			CMD.CommandType = CommandType.StoredProcedure;
			CMD.Connection = CN;
			CMD.CommandText = storeProcedureName;
			CN.Open();

			return CMD.ExecuteReader();
		}

		public static SqlParameter Parameter(string parameterName, object parameterValue)
		{
			return new SqlParameter(parameterName, parameterValue);
		}
	}
}
