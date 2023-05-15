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
    public partial class Assiette : Form
    {
        SERVICES_RESSOURCES_FINANCIERESEntities1 db = new SERVICES_RESSOURCES_FINANCIERESEntities1();
        public Assiette()
        {
            InitializeComponent();
            rempli();
        }
        public void rempli()
        {
            label7.Text = db.materiel.Where(w => w.bureau.id_service == 1).Count(c => c.id_type_materiel == 4).ToString();
            label8.Text = db.materiel.Where(w => w.bureau.id_service == 1).Count(c => c.id_type_materiel == 5).ToString();
            label9.Text = db.materiel.Where(w => w.bureau.id_service == 1).Count(c => c.id_type_materiel == 6).ToString();
            label10.Text = db.materiel.Where(w => w.bureau.id_service == 1).Count(c => c.id_type_materiel == 1).ToString();
            label11.Text = db.materiel.Where(w => w.bureau.id_service == 1).Count(c => c.id_type_materiel == 2).ToString();
            label12.Text = db.materiel.Where(w => w.bureau.id_service == 1).Count(c => c.id_type_materiel == 3).ToString();
            dataGridView1.DataSource = db.materiel.Where(w => w.bureau.type_service.nom_type == "Assiette").Select(s => new { s.id_bureau,s.id_materiel, s.type_materiel.libelle, s.detail_materiel.etat, s.detail_materiel.fiche_technique }).ToList();
            comboBureau.DataSource = db.bureau.Where(w => w.type_service.nom_type == "Assiette").Select(s => s.id_bureau).ToList();
            comboBureau.DisplayMember = "id_bureau";

        }
        private void Assiette_Load(object sender, EventArgs e)
        {
            try
            {
                label17.Text = db.type_service.Where(w => w.id_service == 1).Select(s => s.nom_type).Single();

            }
            catch (Exception)
            {

            }

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBureau_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboTYP_mtr.DataSource = db.type_materiel.ToList();
            comboTYP_mtr.DisplayMember = "libelle";
            comboTYP_mtr.ValueMember = "id_type_materiel";
        }

        private void comboTYP_mtr_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int j = int.Parse(comboTYP_mtr.SelectedValue.ToString());
                comboID_MTR.DataSource = db.detail_materiel.Where(w => w.id_type_materiel == j ).ToList();
                comboID_MTR.DisplayMember = "id_materiel";
            }
            catch { }
        }
        verf cls = new verf();
        private void bButton1_Click(object sender, EventArgs e)
        {
             if (comboBureau.Text!="" && comboID_MTR.Text!="" && comboTYP_mtr.Text != "")
            {
                int ID_MTR = int.Parse(comboID_MTR.Text);
                int ID_TYP = int.Parse(comboTYP_mtr.SelectedValue.ToString());

                if (cls.vrf_idmt_idTp(ID_MTR, ID_TYP) == true)
                {
                   
                    cls.ajote_matereil(comboBureau.Text, ID_MTR, ID_TYP);
                   
                    
                    rempli();
                    MessageBox.Show("bien ajoute !!");
                }
                else MessageBox.Show("mateeiel deja utillese");
            }
            else MessageBox.Show("complete les donne SVP !!");
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (comboBureau.Text != "" && comboID_MTR.Text != "" && comboTYP_mtr.Text != "")
            {
                int ID_MTR = int.Parse(comboID_MTR.Text);
                int ID_TYP = int.Parse(comboTYP_mtr.SelectedValue.ToString());

                if (cls.vrf_idmt_idTp(ID_MTR, ID_TYP) == false)
                {
                    try
                    {
                        cls.supprimer_matereil(comboBureau.Text, ID_MTR, ID_TYP);
                        


                        rempli();
                        MessageBox.Show("bien suprimer !!");
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("materiel utilise avec auter bureau !!");
                    }
                    

                }
                else MessageBox.Show("n'exest pas !!");
            }
            else MessageBox.Show("complete les donne SVP !!");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBureau.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboTYP_mtr.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            comboID_MTR.Text= dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            if (comboBureau.Text != "" && comboID_MTR.Text != "" && comboTYP_mtr.Text != "")
            {
                int ID_MTR = int.Parse(comboID_MTR.Text);
                int ID_TYP = int.Parse(comboTYP_mtr.SelectedValue.ToString());

                if (cls.vrf_idmt_idTp(ID_MTR, ID_TYP) == false)
                {
                    materiel mt = db.materiel.Where(w => w.id_materiel == ID_MTR && w.id_type_materiel == ID_TYP && w.id_bureau == comboBureau.Text).FirstOrDefault();
                   
                        if (mt != null)
                        {
                        if (mt.detail_materiel.etat != "difectue")
                        {
                            // MessageBox.Show("TYPE MATERIL :" + comboTYP_mtr.Text + "\n etat :" + mt.detail_materiel.etat + "\n FICHER TEHNIQUE : " + mt.detail_materiel.fiche_technique);

                            resultate res = new resultate(comboTYP_mtr.Text, mt.detail_materiel.etat, mt.detail_materiel.fiche_technique, "");
                            res.ShowDialog();
                        }
                        else
                        {
                            resultate res = new resultate(comboTYP_mtr.Text, mt.detail_materiel.etat, mt.detail_materiel.fiche_technique, mt.detail_materiel.materiel_difectue.description_MD);
                            res.ShowDialog();
                        }
                        }
                       


                    
                    else 
                    {

                        MessageBox.Show("materiel utilise avec auter bureau !!");
                    }


                }
                else MessageBox.Show("n'exest pas !!");
            }
            else MessageBox.Show("complete les donne SVP !!");

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
