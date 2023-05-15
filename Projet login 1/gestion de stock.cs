using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_login_1
{
    public partial class gestion_de_stock : Form
    {
        SERVICES_RESSOURCES_FINANCIERESEntities1 db = new SERVICES_RESSOURCES_FINANCIERESEntities1();

        public gestion_de_stock()
        {
            InitializeComponent();
            combotypds.DataSource = db.type_materiel.ToList();
            combotypds.ValueMember = "libelle";
            combotypds.ValueMember = "id_type_materiel";
            combotypnds.DataSource = db.type_materiel.Select(s => s.libelle).ToList();
        }

        private void gestion_de_stock_Load(object sender, EventArgs e)
        {

        }

        private void combotypds_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\sqlexpress;Initial Catalog=SERVICES_RESSOURCES_FINANCIERES;Integrated Security=True;");
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter("select * from detail_materiel where id_materiel not in(select id_materiel from materiel) and id_type_materiel=" + combotypds.SelectedValue.ToString(), con);
                DataSet ds = new DataSet();
                adp.Fill(ds, "donne");
                dataGridView1.DataSource = ds.Tables["donne"];
            }
            catch (Exception)
            {


            }
        }

        private void combotypnds_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            dataGridView2.DataSource = db.materiel.Where(w => w.detail_materiel.type_materiel.libelle == combotypnds.Text).Select(s => new { num = s.id_materiel, s.detail_materiel.etat, date = s.detail_materiel.date_entr, s.detail_materiel.fiche_technique }).ToList();
            
        }
    }
}
