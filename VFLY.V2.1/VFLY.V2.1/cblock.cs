using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VFLY.V2._1
{
    public partial class cblock : Form
    {
        public cblock()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = null;
        public void verileriGoster(string veriler, SqlConnection baglanti)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);


            string[] ayri = veriler.Split(' ');

            if (ayri.Last() == "ABlock")
            {
                ds.Tables[0].Columns["id"].ColumnName = "ID";
                ds.Tables[0].Columns["name"].ColumnName = "Name";
                ds.Tables[0].Columns["ip"].ColumnName = "IP";
                ds.Tables[0].Columns["hostname"].ColumnName = "HOSTNAME";
                ds.Tables[0].Columns["location"].ColumnName = "LOCATION";
                ds.Tables[0].Columns["state"].ColumnName = "STATE";

                dataGridView1.DataSource = ds.Tables[0];
            }
            else if (ayri.Last() == "BBlock")
            {
                ds.Tables[0].Columns["id"].ColumnName = "ID";
                ds.Tables[0].Columns["name"].ColumnName = "Name";
                ds.Tables[0].Columns["ip"].ColumnName = "IP";
                ds.Tables[0].Columns["hostname"].ColumnName = "HOSTNAME";
                ds.Tables[0].Columns["location"].ColumnName = "LOCATION";
                ds.Tables[0].Columns["state"].ColumnName = "STATE";

                dataGridView2.DataSource = ds.Tables[0];
            }
            else if (ayri.Last() == "CBlock")
            {
                ds.Tables[0].Columns["id"].ColumnName = "ID";
                ds.Tables[0].Columns["name"].ColumnName = "Name";
                ds.Tables[0].Columns["ip"].ColumnName = "IP";
                ds.Tables[0].Columns["hostname"].ColumnName = "HOSTNAME";
                ds.Tables[0].Columns["location"].ColumnName = "LOCATION";
                ds.Tables[0].Columns["state"].ColumnName = "STATE";

                dataGridView3.DataSource = ds.Tables[0];
            }
        }

        static PingReply PingHost(string ipAddress)
        {
            using (var ping = new Ping())
            {
                return ping.Send(ipAddress);
            }
        }

        static async Task<PingReply> PingHostAsync(string ipAddress)
        {
            using (var ping = new Ping())
            {
                return await ping.SendPingAsync(ipAddress);
            }
        }

        static ArrayList PingHosts(ArrayList ipList)
        {
            ArrayList results = new ArrayList();

            foreach (string ipAddress in ipList)
            {
                PingReply reply = PingHost(ipAddress);
                results.Add(reply.Status);
            }

            return results;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        
        private void cblock_Load(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_VFLY.V2;Integrated Security=True");
            baglanti.Open();

            
            verileriGoster("Select * From ABlock", baglanti);
            verileriGoster("Select * From BBlock", baglanti);
            verileriGoster("Select * From CBlock", baglanti);

            baglanti.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int sec = dataGridView1.SelectedCells[0].RowIndex;
                id = dataGridView1.Rows[sec].Cells[0].Value.ToString();
                richTextBox1.Text = dataGridView1.Rows[sec].Cells[1].Value.ToString();
                richTextBox2.Text = dataGridView1.Rows[sec].Cells[2].Value.ToString();
                richTextBox3.Text = dataGridView1.Rows[sec].Cells[3].Value.ToString();
                richTextBox5.Text = "ABLOCK";
                block = "ABLOCK";
                richTextBox4.Text = dataGridView1.Rows[sec].Cells[4].Value.ToString();
                label6.Text = block + " selected";
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int sec = dataGridView2.SelectedCells[0].RowIndex;
                id = dataGridView2.Rows[sec].Cells[0].Value.ToString();
                richTextBox1.Text = dataGridView2.Rows[sec].Cells[1].Value.ToString();
                richTextBox2.Text = dataGridView2.Rows[sec].Cells[2].Value.ToString();
                richTextBox3.Text = dataGridView2.Rows[sec].Cells[3].Value.ToString();
                richTextBox5.Text = "BBLOCK";
                block = "BBLOCK";
                richTextBox4.Text = dataGridView2.Rows[sec].Cells[4].Value.ToString();
                label6.Text = block + " selected";
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int sec = dataGridView3.SelectedCells[0].RowIndex;
                id = dataGridView3.Rows[sec].Cells[0].Value.ToString();
                richTextBox1.Text = dataGridView3.Rows[sec].Cells[1].Value.ToString();
                richTextBox2.Text = dataGridView3.Rows[sec].Cells[2].Value.ToString();
                richTextBox3.Text = dataGridView3.Rows[sec].Cells[3].Value.ToString();
                richTextBox5.Text = "CBLOCK";
                block = "CBLOCK";
                richTextBox4.Text = dataGridView3.Rows[sec].Cells[4].Value.ToString();
                label6.Text =block+ " selected";
            }
        }
        public string block;
        public string id;
        //private void button5_Click(object sender, EventArgs e)
        //{
        //    baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_VFLY.V2;Integrated Security=True");
        //    baglanti.Open();

        //    verileriGoster("Select * From ABlock", baglanti);
        //    verileriGoster("Select * From BBlock", baglanti);
        //    verileriGoster("Select * From CBlock", baglanti);

        //    baglanti.Close();
        //}

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_VFLY.V2;Integrated Security=True");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From ABLOCK where name like '%" + richTextBox1.Text+ "%' ", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            komut = new SqlCommand("Select * From BBLOCK where name like '%" + richTextBox1.Text + "%' ", baglanti);
            da = new SqlDataAdapter(komut);
            ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];

            komut = new SqlCommand("Select * From CBLOCK where name like '%" + richTextBox1.Text + "%' ", baglanti);
            da = new SqlDataAdapter(komut);
            ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_VFLY.V2;Integrated Security=True");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From ABLOCK where ip like '%" + richTextBox2.Text + "%' ", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            komut = new SqlCommand("Select * From BBLOCK where ip like '%" + richTextBox2.Text + "%' ", baglanti);
            da = new SqlDataAdapter(komut);
            ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];

            komut = new SqlCommand("Select * From CBLOCK where ip like '%" + richTextBox2.Text + "%' ", baglanti);
            da = new SqlDataAdapter(komut);
            ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_VFLY.V2;Integrated Security=True");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From ABLOCK where hostname like '%" + richTextBox3.Text + "%' ", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            komut = new SqlCommand("Select * From BBLOCK where hostname like '%" + richTextBox3.Text + "%' ", baglanti);
            da = new SqlDataAdapter(komut);
            ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];

            komut = new SqlCommand("Select * From CBLOCK where hostname like '%" + richTextBox3.Text + "%' ", baglanti);
            da = new SqlDataAdapter(komut);
            ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_VFLY.V2;Integrated Security=True");
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From ABLOCK where location like '%" + richTextBox4.Text + "%' ", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

            komut = new SqlCommand("Select * From BBLOCK where location like '%" + richTextBox4.Text + "%' ", baglanti);
            da = new SqlDataAdapter(komut);
            ds = new DataSet();
            da.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];

            komut = new SqlCommand("Select * From CBLOCK where location like '%" + richTextBox4.Text + "%' ", baglanti);
            da = new SqlDataAdapter(komut);
            ds = new DataSet();
            da.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_VFLY.V2;Integrated Security=True");
            baglanti.Open();

          

            // Label kontrolü oluşturun
            Label label = new Label();
            label.Text = richTextBox1.Text;
            label.Font = new Font("Arial", 12, FontStyle.Bold);
            label.Name = "name";
            label.Visible = false;

            if (block=="ABLOCK")
            {
                SqlCommand komut = new SqlCommand("UPDATE ABLOCK SET name='" + richTextBox1.Text + "',ip='" + richTextBox2.Text + "',hostname='" + richTextBox3.Text + "',location='" + richTextBox4.Text + "' where id='" + id + "'", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("'"+id+"' No ya sahip '"+richTextBox1.Text+"' isimli personelin bilgileri güncellenmiştir.");
            }
            else if (block == "BBLOCK")
            {
                SqlCommand komut = new SqlCommand("UPDATE BBLOCK SET name='" + richTextBox1.Text + "',ip='" + richTextBox2.Text + "',hostname='" + richTextBox3.Text + "',location='" + richTextBox4.Text + "' where id='" + id + "'", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show(id + " No ya sahip " + richTextBox1.Text + " isimli personelin bilgileri güncellenmiştir.");
            }
            else if (block == "CBLOCK")
            {
                SqlCommand komut = new SqlCommand("UPDATE CBLOCK SET name='" + richTextBox1.Text + "',ip='" + richTextBox2.Text + "',hostname='" + richTextBox3.Text + "',location='" + richTextBox4.Text + "' where id='" + id + "'", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show(id + " No ya sahip " + richTextBox1.Text + " isimli personelin bilgileri güncellenmiştir.");
            }


            baglanti.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_VFLY.V2;Integrated Security=True");
            baglanti.Open();


            verileriGoster("Select * From ABlock", baglanti);
            verileriGoster("Select * From BBlock", baglanti);
            verileriGoster("Select * From CBlock", baglanti);

            baglanti.Close();
        }
    }
}
