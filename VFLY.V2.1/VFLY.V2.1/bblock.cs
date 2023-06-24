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

namespace VFLY.V2._1
{
    public partial class bblock : Form
    {

        SqlConnection baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_VFLY.V2;Integrated Security=True");

        public bblock()
        {
            InitializeComponent();
        }
        public void verileriGoster(string veriler, SqlConnection baglanti)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);

            ds.Tables[0].Columns["name"].ColumnName = "Name";
            ds.Tables[0].Columns["ip"].ColumnName = "IP";
            ds.Tables[0].Columns["hostname"].ColumnName = "HOSTNAME";
            ds.Tables[0].Columns["location"].ColumnName = "LOCATION";
            ds.Tables[0].Columns["state"].ColumnName = "STATE";

            dataGridView1.DataSource = ds.Tables[0];

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

        private async void timer1_Tick(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_VFLY.V2;Integrated Security=True");
            baglanti.Open();

            verileriGoster("Select name,ip,hostname,location,state From ABlock", baglanti);

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
                    SqlCommand command = new SqlCommand("UPDATE BBLock SET state = @stat WHERE ip = @ip;", baglanti);
                    command.Parameters.AddWithValue("@stat", stat);
                    command.Parameters.AddWithValue("@ip", dataGridView1.Rows[a].Cells[1].Value.ToString());
                    command.ExecuteNonQuery();
                }
                else
                {
                    //renk.BackColor = Color.Red;
                    //dataGridView1.Rows[a].Cells[4].Style = renk;
                    SqlCommand command = new SqlCommand("UPDATE BBLock SET state = @stat WHERE ip = @ip;", baglanti);
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
            timer1.Interval = 100000;


        }

        private void bblock_Load(object sender, EventArgs e)
        {
            try
            {
                baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_VFLY.V2;Integrated Security=True");
                baglanti.Open();
                timer1.Interval = 100;
                timer1.Start();

                verileriGoster("Select name,ip,hostname,location,state From ABlock", baglanti);
                verileriGoster("Select name,ip,hostname,location,state From BBlock", baglanti);
                verileriGoster("Select name,ip,hostname,location,state From CBlock", baglanti);

                baglanti.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            finally
            {
                if (baglanti != null)
                    baglanti.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
            this.Hide();
        }
    }
}
