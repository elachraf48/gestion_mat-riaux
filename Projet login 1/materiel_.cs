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
    public partial class materiel_ : Form
    {
        SERVICES_RESSOURCES_FINANCIERESEntities1 db = new SERVICES_RESSOURCES_FINANCIERESEntities1();
        public materiel_()
        {
            InitializeComponent();
         
            //comboMTR1.DataSource = db.detail_materiel.Select(s => s.id_materiel).ToList();
            combservice.DataSource = db.type_service.ToList();
            combservice.DisplayMember = "nom_type";
            combservice.ValueMember = "id_service";
            combservicechn.DataSource = db.type_service.ToList();
            combservicechn.DisplayMember = "nom_type";
            combservicechn.ValueMember = "id_service";
            combotypchn.Text = "";
            combBureauchn.DataSource = db.bureau.Select(s => s.id_bureau).ToList();

        }



        private void comboID_MTR_SelectedIndexChanged_1(object sender, EventArgs e)
        {
          
        }

        private void materiel__Load(object sender, EventArgs e)
        {
            combotypchn.Text = "";
        }

        private void combservice_SelectedIndexChanged(object sender, EventArgs e)
        {
            combBureau.DataSource = db.bureau.Where(w => w.type_service.nom_type == combservice.Text).Select(s => s.id_bureau).ToList();

        }
        verf cls = new verf();
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            int ID_MTR = int.Parse(comboMTR1.Text);
            int ID_TYP = int.Parse(combservice.SelectedValue.ToString());
            
            int vref = db.materiel.Where(w => w.id_materiel == ID_MTR).Count();
            if (combBureau.Text != "" && comboMTR1.Text != "" && combservice.Text != "")
            {
                if (vref > 0)
            {
                materiel mtDELET = db.materiel.Where(w => w.id_materiel == ID_MTR).FirstOrDefault();
                DialogResult res = MessageBox.Show("Cest matereil est en service "+mtDELET.bureau.type_service.nom_type+" exactement dans un bureau "+mtDELET.id_bureau+" !! Voulez-vous le changer pour ce bureau : "+combBureau.Text+"?", "information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    if (mtDELET != null)
                    {
                        db.materiel.Remove(mtDELET);
                        db.SaveChanges();
                       
                        cls.ajote_matereil(combBureau.Text, ID_MTR, ID_TYP);
                        MessageBox.Show("bien modefier !!", "information", MessageBoxButtons.OK, MessageBoxIcon.Question);

                    }
                }
            }
            else if (vref == 0)
            {
                DialogResult res1 = MessageBox.Show("C'est un matereil qui ne se trouvait pas auparavant dans un bureau !! Voulez-vous l'ajouter à ce bureau?", "information", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res1 == DialogResult.Yes)
                {



                        
                        cls.ajote_matereil(combBureau.Text, ID_MTR, ID_TYP);

                        MessageBox.Show("bien ajoute !!");

                   
               

            }
                }
            }
            else MessageBox.Show("complete les donne SVP !!", "information", MessageBoxButtons.OK, MessageBoxIcon.Question);


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            combotypchn.DataSource = db.type_materiel.Select(s => s.libelle).ToList();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            combBureauchn.DataSource = db.bureau.Where(w => w.type_service.nom_type == combservicechn.Text).Select(s => s.id_bureau).ToList();

        }

        private void combotypchn_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboMTR1.Text = "";
            comboMTR1.DataSource= db.materiel.Where(w=>w.type_materiel.libelle==combotypchn.Text && w.id_bureau== combBureauchn.Text).Select(s => s.id_materiel).ToList();
        }

        private void comboMTR1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID =Convert.ToInt16(comboMTR1.Text);
            try
            {
                detail_materiel dt = db.detail_materiel.Where(w => w.id_materiel == ID).FirstOrDefault();
                dataGridView1.DataSource = db.histoir.Where(w => w.id_materiel == ID).Select(s => new {sevice=s.bureau.type_service.nom_type, bureau = s.id_bureau, date = s.date_histoir }).ToList();

                
                textETchn.Text = dt.etat;
                dateTimePicker2.Text = dt.date_entr.ToString();
                richFICHchn.Text = dt.fiche_technique;
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);

            }
        }

        private void textTYP_TextChanged(object sender, EventArgs e)
        {

        }

        private void textET_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
