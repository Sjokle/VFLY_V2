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

namespace VFLY.V2._1
{
    public partial class notping : Form
    {
        public notping()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();   
            form.Show();
            this.Hide();
        }

        SqlConnection baglanti = null;
        public void verileriGoster(string veriler, SqlConnection baglanti)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);


            string[] ayri = veriler.Split(' ');

            if (ayri[3] == "ABlock")
            {
                ds.Tables[0].Columns["name"].ColumnName = "Name";
                ds.Tables[0].Columns["ip"].ColumnName = "IP";
                ds.Tables[0].Columns["hostname"].ColumnName = "HOSTNAME";
                ds.Tables[0].Columns["location"].ColumnName = "LOCATION";
                ds.Tables[0].Columns["state"].ColumnName = "STATE";

                dataGridView1.DataSource = ds.Tables[0];
            }
            else if (ayri[3] == "BBlock")
            {
                ds.Tables[0].Columns["name"].ColumnName = "Name";
                ds.Tables[0].Columns["ip"].ColumnName = "IP";
                ds.Tables[0].Columns["hostname"].ColumnName = "HOSTNAME";
                ds.Tables[0].Columns["location"].ColumnName = "LOCATION";
                ds.Tables[0].Columns["state"].ColumnName = "STATE";

                dataGridView2.DataSource = ds.Tables[0];
            }
            else if (ayri[3] == "CBlock")
            {
                ds.Tables[0].Columns["name"].ColumnName = "Name";
                ds.Tables[0].Columns["ip"].ColumnName = "IP";
                ds.Tables[0].Columns["hostname"].ColumnName = "HOSTNAME";
                ds.Tables[0].Columns["location"].ColumnName = "LOCATION";
                ds.Tables[0].Columns["state"].ColumnName = "STATE";

                dataGridView3.DataSource = ds.Tables[0];
            }
        }


            private void notping_Load(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_VFLY.V2;Integrated Security=True");
            baglanti.Open();
            timer1.Interval=10000;
            timer1.Start();
            verileriGoster("Select name,ip,hostname,location,state From ABlock where state!='Success'", baglanti);
            verileriGoster("Select name,ip,hostname,location,state From BBlock where state!='Success'", baglanti);
            verileriGoster("Select name,ip,hostname,location,state From CBlock where state!='Success'", baglanti);



        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            verileriGoster("Select name,ip,hostname,location,state From ABlock where state!='Success'", baglanti);
            verileriGoster("Select name,ip,hostname,location,state From BBlock where state!='Success'", baglanti);
            verileriGoster("Select name,ip,hostname,location,state From CBlock where state!='Success'", baglanti);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
