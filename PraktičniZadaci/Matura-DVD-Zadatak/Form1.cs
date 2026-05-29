using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

// Sql
using System.Data;
using System.Data.SqlClient;

using System.Threading.Tasks;
using System.Windows.Forms;



/*
 ______   ______     __   __   ______        ______   ______     ______     __    __     ______    
/\  == \ /\  == \   /\ \ / /  /\  __ \      /\  ___\ /\  __ \   /\  == \   /\ "-./  \   /\  __ \   
\ \  _-/ \ \  __<   \ \ \'/   \ \  __ \     \ \  __\ \ \ \/\ \  \ \  __<   \ \ \-./\ \  \ \  __ \  
 \ \_\    \ \_\ \_\  \ \__|    \ \_\ \_\     \ \_\    \ \_____\  \ \_\ \_\  \ \_\ \ \_\  \ \_\ \_\ 
  \/_/     \/_/ /_/   \/_/      \/_/\/_/      \/_/     \/_____/   \/_/ /_/   \/_/  \/_/   \/_/\/_/   
*/



namespace Matura_DVD_Zadatak {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        // Sql konekcioni string
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=DVD;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e) {
            con.Open(); // OTVARANJE KONEKCIJE

            string upit = "select ProducentID, Ime, Email from Producent"; // Sql upit za selekciju i ovo kasnije trebamo da prikazemo u listBox-u

            SqlCommand cmd = new SqlCommand(upit, con);
            SqlDataReader dr = cmd.ExecuteReader();

            while(dr.Read()) { 
                string s = dr[0].ToString() + "\t" + dr[1].ToString() + "\t" + dr[2].ToString() + "\t";

                listBox1.Items.Add(s);
            }

            con.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {
            textBox1.Text = listBox1.SelectedItem.ToString().Split('\t')[0];
            textBox2.Text = listBox1.SelectedItem.ToString().Split('\t')[1];
            textBox3.Text = listBox1.SelectedItem.ToString().Split('\t')[2];
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            con.Open();

            string upit1 = "update Producent set Ime=@ime, Email=@mejl where ProducentID=@id";

            SqlCommand cmd1 = new SqlCommand(upit1, con);

            listBox1.Items.Clear();
            cmd1.Parameters.AddWithValue("@ime", textBox2.Text);
            cmd1.Parameters.AddWithValue("@mejl", textBox3.Text);
            cmd1.Parameters.AddWithValue("@id", textBox1.Text);
            cmd1.ExecuteNonQuery();

            string upit2 = "select ProducentID, Ime, Email from Producent"; // Sql upit za selekciju i ovo kasnije trebamo da prikazemo u listBox-u

            listBox1.Items.Clear();

            SqlCommand cmd2 = new SqlCommand(upit2, con);
            SqlDataReader dr1 = cmd2.ExecuteReader();

            while (dr1.Read())
            {
                string s1 = dr1[0].ToString() + "\t" + dr1[1].ToString() + "\t" + dr1[2].ToString() + "\t";

                listBox1.Items.Add(s1);
            }

            MessageBox.Show("Uspesna izmena !");

            con.Close();
        }

        /*
          ___  ___ _   _  ___   _     ___ ___  ___ __  __   _   
         |   \| _ \ | | |/ __| /_\   | __/ _ \| _ \  \/  | /_\  
         | |) |   / |_| | (_ |/ _ \  | _| (_) |   / |\/| |/ _ \ 
         |___/|_|_\\___/ \___/_/ \_\ |_| \___/|_|_\_|  |_/_/ \_\
                                                        
            -> NAPRAVIS DRUGU FORMU I OVO JE KOD ZA NJU

            private void button1_Click(object sender, EventArgs e)
            {
                string upit="SELECT a.Ime, COUNT(b.FilmID) AS \"Broj filmova\" FROM Producent a, Producirao b WHERE a.ProducentID = b.ProducentID GROUP BY a.Ime"; -> sql query

                chart1.Series.Clear();
                chart1.Series.Add("Filmovi");

                SqlCommand kom = new SqlCommand(upit,kon);

                con.Open();	

                SqlDataAdapter da = new SqlDataAdapter(kom);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                con.Close();

                int i = 0;
                foreach (DataRow item in dt.Rows) 
                {
                    chart1.Series[0].Points.AddXY(item[0].ToString(), item[1]);
                    i++;
                }
             } 
         */

    }
}
