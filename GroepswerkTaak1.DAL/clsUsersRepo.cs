using GroepswerkTaak1.Model;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;

namespace GroepswerkTaak1.DAL
{
    public class clsUsersRepo
    {
        private ObservableCollection<clsUsersM> MijnCollectie;
        int nr = 0;
        public clsUsersRepo()
        {
        }

        public bool Delete(clsUsersM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                    clsDAL.ExecuteDataTable(Properties.Resources.D_User,
                    clsDAL.Parameter("ID", entity.ID),
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

        // public clsUsersM Find() { throw new NotImplementedException(); }  // not implemented

        public ObservableCollection<clsUsersM> GetAll()
        { GenerateCollection(); return MijnCollectie; }


        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_User);
            MijnCollectie = new ObservableCollection<clsUsersM>();
            while (MijnDataReader.Read())
            {
                clsUsersM Knop = new clsUsersM()
                {
                    ID = (int)MijnDataReader["ID"],
                    LoginNaam = MijnDataReader["loginNaam"].ToString(),
                    Naam = MijnDataReader["naam"].ToString(),
                    VoorNaam = MijnDataReader["voornaam"].ToString(),
                    Email = MijnDataReader["email"].ToString(),
                    Telefoon = MijnDataReader["telefoon"].ToString(),
                    RolId = (int)MijnDataReader["rolID"],
                    UserActief = (bool)MijnDataReader["actief"],
                    Wachtwoord = string.Empty, // Wachtwoord wordt niet opgeslagen in de lijst
                    ControlField = MijnDataReader["ControlField"]
                };
                MijnCollectie.Add(Knop);
            }
        }

        public clsUsersM GetByID(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.Where(x => x.ID == id).FirstOrDefault();
        }

        public bool Insert(clsUsersM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                    clsDAL.ExecuteDataTable(Properties.Resources.I_User,
                    clsDAL.Parameter("LoginNaam", entity.LoginNaam),
                    clsDAL.Parameter("Naam", entity.Naam),
                    clsDAL.Parameter("VoorNaam", entity.VoorNaam),
                    clsDAL.Parameter("Email", entity.Email),
                    clsDAL.Parameter("Telefoon", entity.Telefoon),
                    clsDAL.Parameter("RolId", entity.RolId),
                    clsDAL.Parameter("UserActief", entity.UserActief),
                    clsDAL.Parameter("Wachtwoord", entity.Wachtwoord),
                    clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }


        public bool Update(clsUsersM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                    clsDAL.ExecuteDataTable(Properties.Resources.U_User,
                    clsDAL.Parameter("ID", entity.ID),
                    clsDAL.Parameter("LoginNaam", entity.LoginNaam),
                    clsDAL.Parameter("Naam", entity.Naam),
                    clsDAL.Parameter("VoorNaam", entity.VoorNaam),
                    clsDAL.Parameter("Email", entity.Email),
                    clsDAL.Parameter("Telefoon", entity.Telefoon),
                    clsDAL.Parameter("RolId", entity.RolId),
                    clsDAL.Parameter("UserActief", entity.UserActief),
                    clsDAL.Parameter("Wachtwoord", entity.Wachtwoord),
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
