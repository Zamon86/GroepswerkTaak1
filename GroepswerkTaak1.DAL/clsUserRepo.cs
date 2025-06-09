using GroepswerkTaak1.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroepswerkTaak1.DAL
{
    public class clsUserRepo
    {
        bool isDataModified = true;
        // lees de ingelogde gebruiker , indien niet ingelogd , => program tester 
        private string loginNaam = clsActiveUserData.ActiveUser.LoginNaam ?? Environment.UserName;
        public ObservableCollection<clsUserM> MijnCollectie;
        int nr = 0;
        public clsUserRepo()
        {
            MijnCollectie = new ObservableCollection<clsUserM>();
        }
        private void UpdateCollection()
        {
            using (SqlDataReader? reader = clsDAL.GetData(Properties.Resources.S_User))
            {
                if (reader != null)
                {
                    MijnCollectie.Clear();
                    while (reader.Read())
                    {
                        clsUserM aUser = new clsUserM()
                        {
                            UserId = (int)reader["ID"],
                            LoginNaam = reader["loginNaam"].ToString(),
                            Naam = reader["naam"].ToString(),
                            VoorNaam = reader["voornaam"].ToString(),
                            Email = reader["email"].ToString(),
                            Telefoon = reader["telefoon"].ToString(),
                            RolId = (int)reader["rolID"],
                            UserActief = (bool)reader["actief"],
                            Wachtwoord = string.Empty, // Wachtwoord wordt niet opgeslagen in de lijst
                            ControlField = reader["ControlField"]
                        };
                        MijnCollectie.Add(aUser);
                    }
                }
            }
            isDataModified = false;
        }

        public clsUserM GetByID(int id)
        {
            if (isDataModified)
            {
                UpdateCollection();
                isDataModified = false;
            }
            return MijnCollectie.Where(e => e.UserId == id).FirstOrDefault();
        }

        public clsUserM GetFirst()
        {
            if (isDataModified)
            {
                UpdateCollection();
                isDataModified = false;
            }
            return MijnCollectie.FirstOrDefault();
        }
        public ObservableCollection<clsUserM> GetAll()
        {
            if (isDataModified)
            {
                UpdateCollection();
                isDataModified = false;
            }
            return MijnCollectie;
        }


        #region CodeVanDanny

        public bool Delete(clsUserM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                            clsDAL.ExecuteDataTable(Properties.Resources.D_User,
                            clsDAL.Parameter("UserId", entity.UserId),
                            clsDAL.Parameter("UserNaam", loginNaam),
                            clsDAL.Parameter("ControlField", entity.ControlField),
                            clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            else
            {
                isDataModified = true;
            }

            return OK;
        }

         public clsKnoppenM Find() { throw new NotImplementedException(); }  // not implemented

        public bool Insert(clsUserM entity)
        {
            bool OK = false;
            (DataTable DT, OK, string Boodschap) =
                            clsDAL.ExecuteDataTable(Properties.Resources.I_User,
                            clsDAL.Parameter("LoginNaam", entity.LoginNaam),
                    clsDAL.Parameter("Naam", entity.Naam),
                    clsDAL.Parameter("VoorNaam", entity.VoorNaam),
                    clsDAL.Parameter("Email", entity.Email),
                    clsDAL.Parameter("Telefoon", entity.Telefoon),
                    clsDAL.Parameter("RolId", entity.RolId),
                    clsDAL.Parameter("Actief", entity.UserActief),
                    clsDAL.Parameter("Wachtwoord", entity.Wachtwoord),
                            clsDAL.Parameter("UserNaam", loginNaam),
                            clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            else
            {
                isDataModified = true;
            }
            return OK;
        }
        public bool Update(clsUserM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                            clsDAL.ExecuteDataTable(Properties.Resources.U_User,
                            clsDAL.Parameter("UserId", entity.UserId),
                    clsDAL.Parameter("LoginNaam", entity.LoginNaam),
                    clsDAL.Parameter("Naam", entity.Naam),
                    clsDAL.Parameter("VoorNaam", entity.VoorNaam),
                    clsDAL.Parameter("Email", entity.Email),
                    clsDAL.Parameter("Telefoon", entity.Telefoon),
                    clsDAL.Parameter("RolId", entity.RolId),
                    clsDAL.Parameter("Actief", entity.UserActief),
                    clsDAL.Parameter("Wachtwoord", entity.Wachtwoord),
                            clsDAL.Parameter("UserNaam", loginNaam),
                            clsDAL.Parameter("ControlField", entity.ControlField),
                            clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;

                #endregion

            }

            return OK;
        }
    }
}
