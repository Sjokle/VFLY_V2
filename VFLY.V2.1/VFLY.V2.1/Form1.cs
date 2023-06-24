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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace VFLY.V2._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public  int test = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 2000;
            timer1.Start();
        }
        SqlConnection baglanti = null;
        SqlCommand command = null;

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text!="" && textBox3.Text != "" && textBox2.Text != "" && (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked) && comboBox1.Text!="")
            {
                label10.Text=textBox1.Text;
                label11.Text = textBox2.Text;
                label12.Text = textBox3.Text;
                if(radioButton1.Checked)
                {
                    label13.Text = "A BLOK";
                }
                else if(radioButton2.Checked)
                {
                    label13.Text = "B BLOK";
                }
                else if(radioButton3.Checked)
                {
                    label13.Text = "C BLOK";
                }
                label14.Text = comboBox1.Text;
                test = 1;
            }
            else
            {
                MessageBox.Show("Eksik Giriş Yapıldı", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ablock ablok = new ablock();
            ablok.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            bblock bblock = new bblock();
            bblock.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cblock cblock = new cblock();
            cblock.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db_VFLY.V2;Integrated Security=True");
            baglanti.Open();
            if (test==1)
            {
                if(label13.Text=="A BLOK")
                {
                    SqlCommand command = new SqlCommand("INSERT INTO ABlock (name,ip,hostname,location,state) values (@name,@ip,@hostname,@location,@state)", baglanti);
                    command.Parameters.AddWithValue("@name", label10.Text);
                    command.Parameters.AddWithValue("@ip", label12.Text);
                    command.Parameters.AddWithValue("@hostname", label11.Text);
                    command.Parameters.AddWithValue("@location", label14.Text);
                    command.Parameters.AddWithValue("@state", "TimedOut");
                    MessageBox.Show("KAyıt başarılı bir şekilde eklenmiştir.");
                    // Komutu çalıştır
                    command.ExecuteNonQuery();
                }
                else if (label13.Text=="B BLOK")
                {
                    SqlCommand command = new SqlCommand("INSERT INTO BBlock (name,ip,hostname,location,state) values (@name,@ip,@hostname,@location,@state)", baglanti);
                    command.Parameters.AddWithValue("@name", label10.Text);
                    command.Parameters.AddWithValue("@ip", label12.Text);
                    command.Parameters.AddWithValue("@hostname", label11.Text);
                    command.Parameters.AddWithValue("@location", label14.Text);
                    command.Parameters.AddWithValue("@state", "TimedOut");
                    MessageBox.Show("KAyıt başarılı bir şekilde eklenmiştir.");
                    // Komutu çalıştır
                    command.ExecuteNonQuery();
                }
                else if (label13.Text=="C BLOK")
                {
                    SqlCommand command = new SqlCommand("INSERT INTO CBlock (name,ip,hostname,location,state) values (@name,@ip,@hostname,@location,@state)", baglanti);
                    command.Parameters.AddWithValue("@name", label10.Text);
                    command.Parameters.AddWithValue("@ip", label12.Text);
                    command.Parameters.AddWithValue("@hostname", label11.Text);
                    command.Parameters.AddWithValue("@location", label14.Text);
                    command.Parameters.AddWithValue("@state", "TimedOut");
                    // Komutu çalıştır
                    command.ExecuteNonQuery();
                }
                test = 0;
            }
            else
                MessageBox.Show("Önce Sınama Yapınız!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            notping notping = new notping();
            notping.Show();
            this.Hide();
        }
    }
}
