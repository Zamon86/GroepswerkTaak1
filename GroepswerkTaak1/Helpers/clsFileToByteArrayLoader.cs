using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroepswerkTaak1.Helpers
{
	public static class clsFileToByteArrayLoader
	{
		public static byte[] ReadFileAsBytes(string path)
		{
			if (!File.Exists(path))
				throw new FileNotFoundException("File doesn't exist.", path);

			return File.ReadAllBytes(path);			
		}		
	}
}
