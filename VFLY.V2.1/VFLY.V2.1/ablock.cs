using System;
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
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Net;
using static System.Windows.Forms.AxHost;
using System.Collections;

namespace VFLY.V2._1
{
    public partial class ablock : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_VFLY.V2;Integrated Security=True");

        public ablock()
        {
            InitializeComponent();
        }
        public void verileriGoster(string veriler, SqlConnection baglanti)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);


            string[] ayri = veriler.Split(' ');

            if (ayri.Last() == "ABlock")
            {
                ds.Tables[0].Columns["name"].ColumnName = "Name";
                ds.Tables[0].Columns["ip"].ColumnName = "IP";
                ds.Tables[0].Columns["hostname"].ColumnName = "HOSTNAME";
                ds.Tables[0].Columns["location"].ColumnName = "LOCATION";
                ds.Tables[0].Columns["state"].ColumnName = "STATE";

                dataGridView1.DataSource = ds.Tables[0];
            }
            else if (ayri.Last() == "BBlock")
            {
                ds.Tables[0].Columns["name"].ColumnName = "Name";
                ds.Tables[0].Columns["ip"].ColumnName = "IP";
                ds.Tables[0].Columns["hostname"].ColumnName = "HOSTNAME";
                ds.Tables[0].Columns["location"].ColumnName = "LOCATION";
                ds.Tables[0].Columns["state"].ColumnName = "STATE";

                dataGridView2.DataSource = ds.Tables[0];
            }
            else if (ayri.Last() == "CBlock")
            {
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


        private void ablock_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            verileriGoster("Select name,ip,hostname,location,state From ABlock", baglanti);
            verileriGoster("Select name,ip,hostname,location,state From BBlock", baglanti);
            verileriGoster("Select name,ip,hostname,location,state From CBlock", baglanti);
            baglanti.Close();

            timer1.Interval = 100;
            timer1.Start();
            timer2.Interval = 200;
            timer2.Start();
            timer3.Interval = 300;
            timer3.Start();
        }
        private async void timer1_Tick(object sender, EventArgs e)
        {

            SqlConnection baglanti2 = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_VFLY.V2;Integrated Security=True");
            baglanti2.Open();
            verileriGoster("Select name,ip,hostname,location,state From ABlock", baglanti2);

            var tasks = new List<Task<PingReply>>();
            for (int a = 0; a < dataGridView1.Rows.Count - 1; a++)
            {
                string ipAddress = dataGridView1.Rows[a].Cells[1].Value.ToString();
                tasks.Add(PingHostAsync(ipAddress));
            }

            await Task.WhenAll(tasks);
            
            for (int a = 0; a < dataGridView1.Rows.Count - 1; a++)
            {
                var pingReply = tasks[a].Result;

                string stat = pingReply.Status.ToString();
                DataGridViewCellStyle renk = new DataGridViewCellStyle();
                if (pingReply.Status == IPStatus.Success)
                {
                    //renk.BackColor = Color.Green;
                    //dataGridView1.Rows[a].Cells[4].Style = renk;
                    SqlCommand command = new SqlCommand("UPDATE ABLock SET state = @stat WHERE ip = @ip;", baglanti2);
                    command.Parameters.AddWithValue("@stat", stat);
                    command.Parameters.AddWithValue("@ip", dataGridView1.Rows[a].Cells[1].Value.ToString());
                    command.ExecuteNonQuery();
                }
                else
                {
                    //renk.BackColor = Color.Red;
                    //dataGridView1.Rows[a].Cells[4].Style = renk;
                    SqlCommand command = new SqlCommand("UPDATE ABLock SET state = @stat WHERE ip = @ip;", baglanti2);
                    command.Parameters.AddWithValue("@stat", stat);
                    command.Parameters.AddWithValue("@ip", dataGridView1.Rows[a].Cells[1].Value.ToString());
                    command.ExecuteNonQuery();
                }


                if (dataGridView1.Rows[a].Cells[4].Value.ToString() == "Success")
                {
                    renk.BackColor = Color.Green;
                    dataGridView1.Rows[a].Cells[4].Style = renk;
                }
                else
                {
                    renk.BackColor = Color.Red;
                    dataGridView1.Rows[a].Cells[4].Style = renk;
                }

            }
            timer1.Interval = 10000;
            baglanti2.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }

        

        
        

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            timer1.Stop();
            SqlCommand komut = new SqlCommand("Select name,ip,hostname,location,state From ABlock where name like '%"+richTextBox1.Text+"%'",baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            verileriGoster("Select name,ip,hostname,location,state From ABlock", baglanti);
            verileriGoster("Select name,ip,hostname,location,state From BBlock", baglanti);
            verileriGoster("Select name,ip,hostname,location,state From CBlock", baglanti);
            timer1.Interval = 100;
            timer1.Start();
            timer2.Interval = 200;
            timer2.Start();
            timer3.Interval = 300;
            timer3.Start();
            baglanti.Close();
        }

        private async void timer2_Tick(object sender, EventArgs e)
        {
            SqlConnection baglanti2 = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_VFLY.V2;Integrated Security=True");
            baglanti2.Open();
            verileriGoster("Select name,ip,hostname,location,state From BBlock", baglanti2);

            var tasks = new List<Task<PingReply>>();
            for (int a = 0; a < dataGridView2.Rows.Count - 1; a++)
            {
                string ipAddress = dataGridView2.Rows[a].Cells[1].Value.ToString();
                tasks.Add(PingHostAsync(ipAddress));
            }

            await Task.WhenAll(tasks);

            for (int a = 0; a < dataGridView2.Rows.Count - 1; a++)
            {
                var pingReply = tasks[a].Result;

                string stat = pingReply.Status.ToString();
                DataGridViewCellStyle renk = new DataGridViewCellStyle();
                if (pingReply.Status == IPStatus.Success)
                {
                    //renk.BackColor = Color.Green;
                    //dataGridView1.Rows[a].Cells[4].Style = renk;
                    SqlCommand command = new SqlCommand("UPDATE BBLock SET state = @stat WHERE ip = @ip;", baglanti2);
                    command.Parameters.AddWithValue("@stat", stat);
                    command.Parameters.AddWithValue("@ip", dataGridView2.Rows[a].Cells[1].Value.ToString());
                    command.ExecuteNonQuery();
                }
                else
                {
                    //renk.BackColor = Color.Red;
                    //dataGridView1.Rows[a].Cells[4].Style = renk;
                    SqlCommand command = new SqlCommand("UPDATE BBLock SET state = @stat WHERE ip = @ip;", baglanti2);
                    command.Parameters.AddWithValue("@stat", stat);
                    command.Parameters.AddWithValue("@ip", dataGridView2.Rows[a].Cells[1].Value.ToString());
                    command.ExecuteNonQuery();
                }


                if (dataGridView2.Rows[a].Cells[4].Value.ToString() == "Success")
                {
                    renk.BackColor = Color.Green;
                    dataGridView2.Rows[a].Cells[4].Style = renk;
                }
                else
                {
                    renk.BackColor = Color.Red;
                    dataGridView2.Rows[a].Cells[4].Style = renk;
                }

            }
            timer2.Interval = 10000;
            baglanti2.Close();
        }

        private async void timer3_Tick(object sender, EventArgs e)
        {
            SqlConnection baglanti2 = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_VFLY.V2;Integrated Security=True");
            baglanti2.Open();
            verileriGoster("Select name,ip,hostname,location,state From CBlock", baglanti2);

            var tasks = new List<Task<PingReply>>();
            for (int a = 0; a < dataGridView3.Rows.Count - 1; a++)
            {
                string ipAddress = dataGridView3.Rows[a].Cells[1].Value.ToString();
                tasks.Add(PingHostAsync(ipAddress));
            }

            await Task.WhenAll(tasks);

            for (int a = 0; a < dataGridView3.Rows.Count - 1; a++)
            {
                var pingReply = tasks[a].Result;

                string stat = pingReply.Status.ToString();
                DataGridViewCellStyle renk = new DataGridViewCellStyle();
                if (pingReply.Status == IPStatus.Success)
                {
                    //renk.BackColor = Color.Green;
                    //dataGridView1.Rows[a].Cells[4].Style = renk;
                    SqlCommand command = new SqlCommand("UPDATE CBLock SET state = @stat WHERE ip = @ip;", baglanti2);
                    command.Parameters.AddWithValue("@stat", stat);
                    command.Parameters.AddWithValue("@ip", dataGridView3.Rows[a].Cells[1].Value.ToString());
                    command.ExecuteNonQuery();
                }
                else
                {
                    //renk.BackColor = Color.Red;
                    //dataGridView1.Rows[a].Cells[4].Style = renk;
                    SqlCommand command = new SqlCommand("UPDATE CBLock SET state = @stat WHERE ip = @ip;", baglanti2);
                    command.Parameters.AddWithValue("@stat", stat);
                    command.Parameters.AddWithValue("@ip", dataGridView3.Rows[a].Cells[1].Value.ToString());
                    command.ExecuteNonQuery();
                }


                if (dataGridView3.Rows[a].Cells[4].Value.ToString() == "Success")
                {
                    renk.BackColor = Color.Green;
                    dataGridView3.Rows[a].Cells[4].Style = renk;
                }
                else
                {
                    renk.BackColor = Color.Red;
                    dataGridView3.Rows[a].Cells[4].Style = renk;
                }

            }
            timer3.Interval = 10000;
            baglanti2.Close();
        }
    }
}
