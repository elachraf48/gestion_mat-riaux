using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_login_1
{

    class verf
    {
        SERVICES_RESSOURCES_FINANCIERESEntities1 db = new SERVICES_RESSOURCES_FINANCIERESEntities1();

        public bool vrf_idmt_idTp(int idMT, int idTP)
        {
            int vr = db.materiel.Where(w => w.id_materiel == idMT && w.id_type_materiel == idTP).Count();
            if (vr == 0) return true;
            else
                return false;
        }
         string res; 
        public string vrf_idmt_idMT(int IDMT)
        {
          
           var mt= db.detail_materiel.Where(w => w.id_materiel == IDMT).Select(c => c.etat).FirstOrDefault();
            if (mt == null)
            {
                res = "non";
            }
            else
            {
                if (mt == "difectue")
                {
                    res = "difectue";
                }
                else if(mt != "difectue")
                {
                    res = "bon";
                }
            }
            return  res;
        }
        public void ajote_matereil(string id_bureau, int id_materiel, int id_type_materiel)
        {
            materiel mt = new materiel();
            mt.id_bureau = id_bureau;
            mt.id_materiel = id_materiel;
            mt.id_type_materiel = id_type_materiel;
            db.materiel.Add(mt);
            histoir hs = new histoir();
            hs.id_bureau = id_bureau;
            hs.id_materiel = id_materiel;
            hs.date_histoir = DateTime.Now;
            db.histoir.Add(hs);
            db.SaveChanges();
        }
        public void supprimer_matereil(string id_bureau, int id_materiel, int id_type_materiel)
        {
            materiel mt = db.materiel.Where(w => w.id_materiel == id_materiel && w.id_type_materiel == id_type_materiel && w.id_bureau == id_bureau).FirstOrDefault();

            db.materiel.Remove(mt);
            db.SaveChanges();

        }
    }
}
