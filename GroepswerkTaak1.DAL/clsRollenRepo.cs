using GroepswerkTaak1.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroepswerkTaak1;

namespace GroepswerkTaak1.DAL
{
    public class clsRollenRepo
    {
        private ObservableCollection<clsRollenM> MijnCollectie;
        int nr = 0;
        public clsRollenRepo()
        {
        }

        public bool Delete(clsRollenM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                    clsDAL.ExecuteDataTable(Properties.Resources.D_User,
                    clsDAL.Parameter("ID", entity.RolId),
                    ///TODO: vervangen door de loginnaam
                    ///
                    clsDAL.Parameter("User", Environment.UserName),
                    clsDAL.Parameter("ControlField", entity.ControlField),
                    clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        // public clsRollenM Find() { throw new NotImplementedException(); }  // not implemented
        public ObservableCollection<clsRollenM> GetAll()
        { GenerateCollection(); return MijnCollectie; }


        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Rol);
            MijnCollectie = new ObservableCollection<clsRollenM>();
            while (MijnDataReader.Read())
            {
                clsRollenM Knop = new clsRollenM()
                {
                    RolId = (int)MijnDataReader["ID"],
                    RolNaam = MijnDataReader["rol"].ToString(),
                    ControlField = MijnDataReader["ControlField"]
                };
                MijnCollectie.Add(Knop);
            }
        }

        public clsRollenM GetByID(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.Where(x => x.RolId == id).FirstOrDefault();
        }



        public bool Insert(clsRollenM entity,string LoginNaam)
        {
            (DataTable DT, bool OK, string Boodschap) =
                    clsDAL.ExecuteDataTable(Properties.Resources.I_Rol,
                    clsDAL.Parameter("RolNaam", entity.RolNaam),
                    clsDAL.Parameter("UserNaam", LoginNaam),
                    clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }


        public bool Update(clsRollenM entity, string LoginNaam)
        {
            (DataTable DT, bool OK, string Boodschap) =
                    clsDAL.ExecuteDataTable(Properties.Resources.U_Rol,
                    clsDAL.Parameter("ID", entity.RolId),
                    clsDAL.Parameter("RolNaam", entity.RolNaam),
                    clsDAL.Parameter("UserNaam",LoginNaam),
                    clsDAL.Parameter("ControlField", entity.ControlField),
                    clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;

        }
    }
}
