using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OkulNot
{
    public partial class FrmOgrenciNotlar : Form
    {
        public FrmOgrenciNotlar()
        {
            InitializeComponent();
        }
        SqlConnection baglanti=new SqlConnection(@"Data Source=KADIR\SQLEXPRESS;Initial Catalog=OkulNot;Integrated Security=True;Encrypt=False");
        public string numara;
        private string adSoyad="";
        private void FrmOgrenciNotlar_Load(object sender, EventArgs e)
        {
            SqlCommand cmd= new SqlCommand("Select DersAd,Sinav1,Sinav2,Sinav3,Proje,Ortalama,Durum From Tbl_Notlar inner join Tbl_Dersler on Tbl_Notlar.DersId=Tbl_Dersler.DersId where OgrenciId=@p1", baglanti);
            cmd.Parameters.AddWithValue("@p1", numara);
           // this.Text=numara.ToString();
            DataTable dt = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

            baglanti.Open();

            SqlCommand cmd2 = new SqlCommand("Select *From Tbl_Ogrenciler where OgrenciId=@p1", baglanti);
            cmd2.Parameters.AddWithValue("@p1", numara);
            SqlDataReader reader = cmd2.ExecuteReader();
            while (reader.Read()) 
            {
                adSoyad = reader[1]+" "+reader[2];
            }
            this.Text = adSoyad.ToString();
            
        }
    }
}
