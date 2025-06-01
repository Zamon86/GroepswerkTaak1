using GroepswerkTaak1.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroepswerkTaak1.DAL
{
    public class clsLoggingRepo
    {
        private ObservableCollection<clsLoggingM> MijnCollectie;
        int nr = 0;
        public clsLoggingRepo()
        {
        }
        public ObservableCollection<clsLoggingM> GetAll()
        { GenerateCollection(); return MijnCollectie; }


        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Logging);
            MijnCollectie = new ObservableCollection<clsLoggingM>();
            while (MijnDataReader.Read())
            {
                clsLoggingM Logging = new clsLoggingM()
                {
                   
                    Aktie = MijnDataReader["aktie"].ToString(),
                    TabelColumn = MijnDataReader["Tbl_Col"].ToString(),
                    PreValue = MijnDataReader["PreValue"].ToString(),
                    PostValue = MijnDataReader["PostValue"].ToString(),
                    Gebruiker = MijnDataReader["gebruiker"].ToString(), // opgelet , dit is eigenlijk de beheerder die de logging deed
                    Registratie = (DateTime)MijnDataReader["dt_registratie"],
                };
                MijnCollectie.Add(Logging);
            }
        }


        //mss aanpassen om "verkorte" tabellen te krijgen
        //public clsRollenM GetByID(int id)
        //{
        //    if (MijnCollectie == null)
        //    {
        //        GenerateCollection();
        //    }
        //    return MijnCollectie.Where(x => x.RolId == id).FirstOrDefault();
        //}

    }
}
