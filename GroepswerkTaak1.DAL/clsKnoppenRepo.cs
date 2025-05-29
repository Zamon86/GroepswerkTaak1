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
    public class clsKnoppenRepo
    {
        private ObservableCollection<clsKnoppenM> MijnCollectie;
        int nr = 0;
        public clsKnoppenRepo()
        {
        }

        public bool Delete(clsKnoppenM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                    clsDAL.ExecuteDataTable(Properties.Resources.D_Knop,
                    clsDAL.Parameter("ID", entity.KnopId),
                    clsDAL.Parameter("User", Environment.UserName),
                    clsDAL.Parameter("ControlField", entity.ControlField),
                    clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }

        // public clsKnoppenM Find() { throw new NotImplementedException(); }  // not implemented

        public ObservableCollection<clsKnoppenM> GetAll()
        { GenerateCollection(); return MijnCollectie; }


        private void GenerateCollection()
        {
            SqlDataReader MijnDataReader = clsDAL.GetData(Properties.Resources.S_Knop);
            MijnCollectie = new ObservableCollection<clsKnoppenM>();
            while (MijnDataReader.Read())
            {
                clsKnoppenM Knop = new clsKnoppenM()
                {
                    KnopId = (int)MijnDataReader["ID"],
                    KnopNaam = MijnDataReader["Naam"].ToString(),
                    KnopTekst = MijnDataReader["Tekst"].ToString(),
                    KnopPositie = (int) MijnDataReader["Positie"],
                    KnopImage = (byte[])MijnDataReader["Image"],
                    ControlField = MijnDataReader["ControlField"]
                };
                MijnCollectie.Add(Knop);
            }
        }

        public clsKnoppenM GetByID(int id)
        {
            if (MijnCollectie == null)
            {
                GenerateCollection();
            }
            return MijnCollectie.Where(x => x.KnopId == id).FirstOrDefault();
        }

        public bool Insert(clsKnoppenM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                    clsDAL.ExecuteDataTable(Properties.Resources.I_Knop,
                    clsDAL.Parameter("Naam", entity.KnopNaam),
                    clsDAL.Parameter("Tekst", entity.KnopTekst),
                    clsDAL.Parameter("Positie", entity.KnopPositie),
                    clsDAL.Parameter("Image", entity.KnopImage),
                    clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            {
                entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }
        public bool Update(clsKnoppenM entity)
        {
            (DataTable DT, bool OK, string Boodschap) =
                    clsDAL.ExecuteDataTable(Properties.Resources.I_Knop,
                    clsDAL.Parameter("ID", entity.KnopId),
                    clsDAL.Parameter("Naam", entity.KnopNaam),
                    clsDAL.Parameter("Tekst", entity.KnopTekst),
                    clsDAL.Parameter("Positie", entity.KnopPositie),
                    clsDAL.Parameter("Image", entity.KnopImage),
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
