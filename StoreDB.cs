using GroepswerkTaak1.Properties;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace GroepswerkTaak1
{
	public class clsStoreDB
	{
		private static readonly Settings sdb_Settings = new();		

		

		public ObservableCollection<byte[]> GetBytesForImages()
		{
			ObservableCollection<byte[]> imageFilesBytes = new ObservableCollection<byte[]>();


			try
			{
				using (SqlConnection CN = new SqlConnection(sdb_Settings.strCNLocalPiotr))
				{
					using (SqlCommand CMD = new SqlCommand(Resources.S_Images, CN))
					{
						CMD.CommandType = CommandType.StoredProcedure;
						CN.Open();
						using (SqlDataReader reader = CMD.ExecuteReader())
						{
							while (reader.Read()) 
							{								
								imageFilesBytes.Add((byte[])reader["Image"]);
							}
						}	
					}
				}
			}

			catch (SqlException ex)
			{
				MessageBox.Show($"Sql Error: {ex.Message}");				
			}

			catch (Exception ex)
			{
				MessageBox.Show($"Error: {ex.Message}");				
			}


			return imageFilesBytes;
		}

		public bool InsertImage(string filePath)
		{
			byte[] imageBytes = File.ReadAllBytes(filePath);
			if (imageBytes == null || imageBytes.Length == 0)
			{
				MessageBox.Show("Error, File reading problem!");
				return false;
			}


			try
			{
				using (SqlConnection CN = new SqlConnection(sdb_Settings.strCNLocalPiotr))
				{
					using (SqlCommand CMD = new SqlCommand(Resources.I_Image, CN))
					{

						CMD.CommandType = CommandType.StoredProcedure;
						CMD.Parameters.Clear();
						CMD.Parameters.Add("@Image", SqlDbType.VarBinary).Value = imageBytes;
						CN.Open();
						CMD.ExecuteNonQuery();
						return true;

					}
				}
			}

			catch (SqlException ex)
			{
				MessageBox.Show($"Sql Error: {ex.Message}");
				return false;
			}

			catch (Exception ex)
			{
				MessageBox.Show($"Error: {ex.Message}");
				return false;
			}
		}
	}
}