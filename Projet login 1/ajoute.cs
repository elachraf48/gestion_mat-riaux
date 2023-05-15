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
    public partial class ajoute : Form
    {
        SERVICES_RESSOURCES_FINANCIERESEntities1 db = new SERVICES_RESSOURCES_FINANCIERESEntities1();
        public ajoute()
        {
            InitializeComponent();
            comboType.DataSource = db.type_materiel.ToList();
            comboType.DisplayMember = "libelle";
            comboType.ValueMember = "id_type_materiel";
            comboType1.DataSource = db.type_service.ToList();
            comboType1.DisplayMember = "nom_type";
            comboType1.ValueMember = "id_service";
            
        }

        private void difectue_CheckedChanged(object sender, EventArgs e)
        {
            if (difectue.Checked == true)
            {
                richTextBox1.Visible = true;
                label5.Visible = true;

            }
            else
            {
                richTextBox1.Visible = false;
                richTextBox1.Text = "";
                label5.Visible = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        string etat;
        private void bButton1_Click(object sender, EventArgs e)
        {
            int id_mt;
            int cout;

            if (difectue.Checked == true) { etat = "difectue"; }
            else if (moyen.Checked == true) { etat = "moyen"; }
            else { etat = "bon"; }
            if (comboType.Text != "" && richTextBox2.Text != "")
            {

                detail_materiel dt = new detail_materiel();
                dt.id_type_materiel = int.Parse(comboType.SelectedValue.ToString());
                dt.fiche_technique = richTextBox2.Text;
                dt.etat = etat;
                dt.date_entr = dateTimePicker1.Value;
                dt.Quantity =Convert.ToInt32( Quantity.Value.ToString());
                if (difectue.Checked == true && richTextBox1.Text != "")
                {
                    db.detail_materiel.Add(dt);
                    db.SaveChanges();
                    cout = db.detail_materiel.Count() - 1;
                    id_mt = db.detail_materiel.Select(s => s.id_materiel).ToList().ElementAt(cout);

                    materiel_difectue md = new materiel_difectue();
                    md.id_materiel = id_mt;
                    md.description_MD = richTextBox1.Text;
                    db.materiel_difectue.Add(md);

                    db.SaveChanges();
                    combID.DataSource = db.detail_materiel.Select(s => s.id_materiel).ToList();
                    MessageBox.Show("bien ajoute !!");


                }
                else if (difectue.Checked == true && richTextBox1.Text == "")
                {
                    MessageBox.Show("description probleme est vide");
                    return;
                }
                else if (difectue.Checked == false)
                {
                    db.detail_materiel.Add(dt);
                    db.SaveChanges();
                    combID.DataSource = db.detail_materiel.Select(s => s.id_materiel).ToList();
                    MessageBox.Show("bien ajoute !!");

                }



            }

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (combID.Text != "")
            {
                int comID = Convert.ToInt32(combID.Text);
                detail_materiel dm = (from i in db.detail_materiel where i.id_materiel == comID select i).Single();
                db.detail_materiel.Remove(dm);
                db.SaveChanges();
                combID.DataSource = db.detail_materiel.Select(s => s.id_materiel).ToList();
                MessageBox.Show("bien supprimer !!");
            }
            else { MessageBox.Show("choise un id SVP !!"); }

        }

        private void combID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int comID = Convert.ToInt32(combID.Text);
            detail_materiel dm = (from i in db.detail_materiel where i.id_materiel == comID select i).Single();
            comboType.Text = dm.type_materiel.libelle;
            richTextBox2.Text = dm.fiche_technique;
            dateTimePicker1.Text = dm.date_entr.ToString();
            Quantity.Value =Convert.ToInt32( dm.Quantity.ToString());
            if (dm.etat == "difectue") { difectue.Checked = true; }
            else if (dm.etat == "moyen") { moyen.Checked = true; }
            else if (dm.etat == "bon") { bon.Checked = true; }
            if (difectue.Checked == true) { richTextBox1.Text = db.materiel_difectue.Where(w => w.id_materiel == comID).Select(s => s.description_MD).FirstOrDefault(); }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            int comID = Convert.ToInt32(combID.Text);
            detail_materiel dm = (from i in db.detail_materiel where i.id_materiel == comID select i).Single();
            int typ = int.Parse(comboType.SelectedValue.ToString());
            dm.id_type_materiel = typ;
            dm.fiche_technique = richTextBox2.Text;
            dm.date_entr = dateTimePicker1.Value;
            dm.Quantity = Convert.ToInt32(Quantity.Value.ToString());
            if (difectue.Checked == true) { dm.etat = "difectue"; }
            else if (moyen.Checked == true) { dm.etat = "moyen"; }
            else if (bon.Checked == true) { dm.etat = "bon"; }
            materiel_difectue df = (from i in db.materiel_difectue where i.id_materiel == comID select i).FirstOrDefault();
            int cpt_mt_df = (from i in db.materiel_difectue where i.id_materiel == comID select i).Count();
            if (difectue.Checked == true)
            {
                if (cpt_mt_df != 0)
                {
                    df.description_MD = richTextBox1.Text;
                }


                else
                {
                    materiel_difectue ma_df = new materiel_difectue();
                    ma_df.id_materiel = comID;
                    ma_df.description_MD = richTextBox1.Text;
                    db.materiel_difectue.Add(ma_df);
                }
            }
            if (difectue.Checked == false)
            {
                if (cpt_mt_df != 0)
                {
                    db.materiel_difectue.Remove(df);
                }


            }
            db.SaveChanges();
            combID.DataSource = db.detail_materiel.Select(s => s.id_materiel).ToList();
            MessageBox.Show("bien modifer !!");
        }

        private void ajoute_Load(object sender, EventArgs e)
        {
            combID.DataSource = db.detail_materiel.Select(s => s.id_materiel).ToList();


        }

        private void comboType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            combobureau.DataSource = db.bureau.Where(w => w.type_service.nom_type == comboType1.Text).Select(s => s.id_bureau).ToList();
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            bureau br = new bureau();
            br.id_bureau = combobureau.Text;
            br.id_service = int.Parse(comboType1.SelectedValue.ToString());
            br.nom_bureau = textBox1.Text;
            int verf_exest = db.bureau.Where(w => w.id_bureau == combobureau.Text || w.nom_bureau==textBox1.Text).Count();

            if (comboType1.Text != "" && combobureau.Text != "" && textBox1.Text != "")
            {
                if (verf_exest == 0)
                {
                    db.bureau.Add(br);
                    db.SaveChanges();

                    MessageBox.Show("bien ajoute !!");
                }
                else
                {
                    MessageBox.Show("nom de bureau deja exist  !!");
                }
            }

            else { MessageBox.Show("complete les donne SVP  !!"); }



        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            if (comboType1.Text != "" && combobureau.Text != "" && textBox1.Text != "")
            {
                DialogResult res = MessageBox.Show("sure!!", "supprimer", MessageBoxButtons.YesNo);
                int ID_SER = int.Parse(comboType1.SelectedValue.ToString());
                bureau br = db.bureau.Where(w => w.id_service == ID_SER && w.id_bureau == combobureau.Text).FirstOrDefault();
                if (res == DialogResult.Yes)
                {
                    if (br != null)
                    {
                        db.bureau.Remove(br);
                        db.SaveChanges();

                        MessageBox.Show("bien supprimer !!");
                    }
                    else
                    {
                        MessageBox.Show("bureau n'exist pas !!");
                    }

                }
            }

            else { MessageBox.Show("complete les donne SVP  !!"); }
        }

        private void combobureau_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = db.bureau.Where(w => w.id_bureau == combobureau.Text).Select(s => s.nom_bureau).FirstOrDefault();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            if (comboType1.Text != "" && combobureau.Text != "" && textBox1.Text != "")
            {
                DialogResult res = MessageBox.Show("sure!!", "modifier", MessageBoxButtons.YesNo);
                int ID_SER = int.Parse(comboType1.SelectedValue.ToString());
                bureau br = db.bureau.Where(w => w.id_service == ID_SER && w.id_bureau == combobureau.Text).FirstOrDefault();
                if (res == DialogResult.Yes)
                {
                    if (br != null)
                    {
                        br.nom_bureau = textBox1.Text;
                       
                        db.SaveChanges();

                        MessageBox.Show("bien modifier !!");
                    }
                    else
                    {
                        MessageBox.Show("bureau n'exist pas !!");
                    }

                }
            }

            else { MessageBox.Show("complete les donne SVP  !!"); }
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            type_materiel mt = new type_materiel();
            mt.libelle = typeMT.Text;
           
            int verf_exest = db.type_materiel.Where(w => w.libelle == typeMT.Text ).Count();

            if (typeMT.Text != "" )
            {
                if (verf_exest == 0)
                {
                    db.type_materiel.Add(mt);
                    db.SaveChanges();

                    MessageBox.Show("bien ajoute !!");
                }
                else
                {
                    MessageBox.Show("nom de material deja exist  !!");
                }
            }

            else { MessageBox.Show("complete les donne SVP  !!"); }

        }

        private void bunifuThinButton27_Click(object sender, EventArgs e)
        {

        }
    }
}
