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
    public partial class Impriment : Form
    {
        SERVICES_RESSOURCES_FINANCIERESEntities1 db = new SERVICES_RESSOURCES_FINANCIERESEntities1();
        public Impriment()
        {
            
           
            InitializeComponent();
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox8.DataSource = db.detail_materiel.Select(s => s.id_materiel).ToList();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void changerBureauToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            materiel_ mt = new materiel_();
           
            mt.Dock = DockStyle.Fill;
            mt.Show();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void bButton1_Click(object sender, EventArgs e)
        {
            materiel_service mt = new materiel_service();
            crystalReportViewer1.ReportSource = mt;
            crystalReportViewer1.Refresh();
        }
        verf cls = new verf();
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if(cls.vrf_idmt_idMT(int.Parse(comboBox8.Text)) == "non")
            {
                MessageBox.Show("n'exist pas c'est materiel dans stock");
            }
            else if (cls.vrf_idmt_idMT(int.Parse(comboBox8.Text)) == "difectue")
            {
                materiel_def mdf = new materiel_def();
                mdf.SetParameterValue("id_materiel", comboBox8.Text);
                crystalReportViewer1.ReportSource = mdf;
                crystalReportViewer1.Refresh();
            }
            else if(cls.vrf_idmt_idMT(int.Parse(comboBox8.Text)) != "difectue")
            {
                materiel_bon mdB = new materiel_bon();
                mdB.SetParameterValue("id_materiel", comboBox8.Text);
                crystalReportViewer1.ReportSource = mdB;
                crystalReportViewer1.Refresh();
            }
            

        }

      
        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            CrystalReport1 mt = new CrystalReport1();
            crystalReportViewer1.ReportSource = mt;
            crystalReportViewer1.Refresh();
        }
    }
}
