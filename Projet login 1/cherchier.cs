using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_login_1
{
    public partial class cherchier : Form
    {
        SERVICES_RESSOURCES_FINANCIERESEntities1 db = new SERVICES_RESSOURCES_FINANCIERESEntities1();

        public cherchier()
        {
            InitializeComponent();
          
            SERVECE.DataSource = db.type_service.ToList();
            SERVECE.DisplayMember = "nom_type";
            SERVECE.ValueMember = "id_service";
            comboBox8.DataSource = db.bureau.ToList();
            comboBox8.DisplayMember = "nom_bureau";
            comboBox8.ValueMember = "id_bureau";
            //remple bon etat et mouyen
            label7.Text = db.detail_materiel.Where(w => w.etat != "difectue").Count(c => c.id_type_materiel == 4).ToString();
            label8.Text = db.detail_materiel.Where(w => w.etat != "difectue").Count(c => c.id_type_materiel == 5).ToString();
            label9.Text = db.detail_materiel.Where(w => w.etat != "difectue").Count(c => c.id_type_materiel == 6).ToString();
            label10.Text = db.detail_materiel.Where(w => w.etat != "difectue").Count(c => c.id_type_materiel == 1).ToString();
            label11.Text = db.detail_materiel.Where(w => w.etat != "difectue").Count(c => c.id_type_materiel == 2).ToString();
            label12.Text = db.detail_materiel.Where(w => w.etat != "difectue").Count(c => c.id_type_materiel == 3).ToString();
            //remple difectue
            label19.Text = db.detail_materiel.Where(w => w.etat == "difectue").Count(c => c.id_type_materiel == 4).ToString();
            label33.Text = db.detail_materiel.Where(w => w.etat == "difectue").Count(c => c.id_type_materiel == 5).ToString();
            label38.Text = db.detail_materiel.Where(w => w.etat == "difectue").Count(c => c.id_type_materiel == 6).ToString();
            label17.Text = db.detail_materiel.Where(w => w.etat == "difectue").Count(c => c.id_type_materiel == 1).ToString();
            label21.Text = db.detail_materiel.Where(w => w.etat == "difectue").Count(c => c.id_type_materiel == 2).ToString();
            label35.Text = db.detail_materiel.Where(w => w.etat == "difectue").Count(c => c.id_type_materiel == 3).ToString();


        }

        private void cherchier_Load(object sender, EventArgs e)
        {
            SERVECE.SelectedItem = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                try
                {

                    string x = Convert.ToString(bureau.SelectedValue);
                    int mt = Convert.ToInt32(typr_matr.SelectedValue);
                    dataGridView1.DataSource = db.materiel.Where(w => w.id_bureau == x && w.id_type_materiel == mt && w.detail_materiel.etat == "difectue").Select(b => new { b.bureau.nom_bureau, catigorie = b.type_materiel.libelle, b.detail_materiel.fiche_technique, b.detail_materiel.etat, b.detail_materiel.materiel_difectue.description_MD }).ToList();

                }
                catch { }
            }
            else comboBox5_SelectedIndexChanged(sender, e);
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SERVECE.Text != "")
            {
                try
                {
                    int k = Convert.ToInt32(SERVECE.SelectedValue);
                    //comboBox2.DataSource = from i in db.bureau where i.id_service == k select i;
                    //db.bureau.Where(w => w.id_service == j).ToList();
                    bureau.DisplayMember = "nom_bureau";
                    bureau.ValueMember = "id_bureau";
                    bureau.DataSource = db.bureau.Where(w => w.id_service == k).ToList();
                }
                catch { }
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bureau.Text != "")
            {

                typr_matr.DataSource = db.type_materiel.ToList();
                typr_matr.DisplayMember = "libelle";
                typr_matr.ValueMember = "id_type_materiel";
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string x = Convert.ToString(bureau.SelectedValue);
                int mt = Convert.ToInt32(typr_matr.SelectedValue);
                dataGridView1.DataSource = db.materiel.Where(w => w.id_bureau == x && w.id_type_materiel == mt).Select(b => new { b.bureau.nom_bureau, catigorie = b.type_materiel.libelle, b.detail_materiel.fiche_technique, b.detail_materiel.etat }).ToList();

            }
            catch { }
        }

        private void bButton1_Click(object sender, EventArgs e)
        {
            string x = Convert.ToString(comboBox8.SelectedValue);
            dataGridView1.DataSource = db.materiel.Where(w => w.id_bureau == x).Select(b => new { b.bureau.nom_bureau, catigorie = b.type_materiel.libelle, b.detail_materiel.fiche_technique, b.detail_materiel.etat }).ToList();

        }

    }
}
