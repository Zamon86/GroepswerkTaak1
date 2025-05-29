using GroepswerkTaak1.Model;
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
        (DataTable DT, bool OK , string Boodschap)=
                clsDAL.ExecuteDataTable(Properties.Resources.D_Knop,
                clsDAL.Parameter("ID", entity.KnopId),
                clsDAL.Parameter("User", Environment.UserName),
                clsDAL.Parameter("ControlField", entity.ControlField),
                clsDAL.Parameter("@ReturnValue", 0));
            if (!OK)
            { 
            // entity.ErrorBoodschap = Boodschap;
            }
            return OK;
        }
    }
}
