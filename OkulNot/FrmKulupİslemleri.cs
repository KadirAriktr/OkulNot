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
using System.Data.SqlClient;

namespace OkulNot
{
    public partial class FrmKulupİslemleri : Form
    {
        public FrmKulupİslemleri()
        {
            InitializeComponent();
        }

        private string connectionString = @"Data Source=KADIR\SQLEXPRESS;Initial Catalog=OkulNot;Integrated Security=True;Encrypt=False";

        
        private SqlConnection BaglantiAc()
        {
            SqlConnection baglanti = new SqlConnection(connectionString);
            baglanti.Open();
            return baglanti;
        }
        public void Listele() 
        {
            using (SqlConnection baglanti = BaglantiAc())
            {
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("Select*From Tbl_Kulupler", baglanti);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }

        }
        public void Temizle() 
        {
            foreach(Control control in this.Controls) 
            {
                if(control is GroupBox) 
                {
                    foreach (Control altcontrol in control.Controls)
                    {
                        if (altcontrol is TextBox) 
                        {
                            ((TextBox)altcontrol).Clear();
                        }
                    }
                }
            }
        }
        public bool AlanKontrol()
        {
            if (string.IsNullOrWhiteSpace(txtKulupAd.Text)) 
            {
                MessageBox.Show("Lütfen Alanları doldurunuz!","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtKulupAd.Text.Length > 50)
            {
                MessageBox.Show("Kulüp adı en fazla 50 karakter olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void FrmKulupİslemleri_Load(object sender, EventArgs e)
        {
          Listele();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Listele();
            Temizle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!AlanKontrol()) 
            {
                Temizle();
                return;
            }
            string kulupAd=txtKulupAd.Text.Trim();
            using (SqlConnection baglanti = BaglantiAc())
            {
                SqlCommand cmdKontrol = new SqlCommand("Select Count(*) From Tbl_Kulupler where KulupAd=@p1", baglanti);
                cmdKontrol.Parameters.Add("@p1",SqlDbType.NVarChar).Value = kulupAd;
                int count = (int)cmdKontrol.ExecuteScalar();
                if (count > 0)
                {
                    Temizle();
                    MessageBox.Show("Bu isimde bir kulüp bulunmaktadır. Lütfen başka bir kulüp ekleyiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                

                SqlCommand cmdEkle = new SqlCommand("Insert Into Tbl_Kulupler(KulupAd) values (@p1)", baglanti);
                cmdEkle.Parameters.Add("@p1",SqlDbType.NVarChar).Value=kulupAd;
                int rowsKontrol=cmdEkle.ExecuteNonQuery();
                if (rowsKontrol > 0) 
                {
                    MessageBox.Show("Kulüp Kaydedilmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listele();
                }
                else
                {
                    MessageBox.Show("Kulüp ekleme işlemi başarısız oldu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Temizle();

            }             
          
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            pictureBox6.BackColor=Color.LightYellow;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BackColor=Color.Turquoise;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (!AlanKontrol()) 
            {
                return;
            }
            string kulupAd = txtKulupAd.Text.Trim();
            using (SqlConnection baglanti = BaglantiAc()) 
            {
                if (string.IsNullOrWhiteSpace(txtKulupId.Text)) 
                {
                    MessageBox.Show("Kulüp ID'si boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SqlCommand cmdGuncelle = new SqlCommand("Update Tbl_Kulupler set KulupAd=@p1 where KulupId=@p2", baglanti);
                cmdGuncelle.Parameters.Add("@p1",SqlDbType.NVarChar).Value = kulupAd;
                cmdGuncelle.Parameters.Add("@p2",SqlDbType.Int).Value = Convert.ToInt32(txtKulupId.Text);
                int etkilenenSatır=cmdGuncelle.ExecuteNonQuery();
                if (etkilenenSatır > 0) 
                {
                    MessageBox.Show("Güncelleme Başarıyla Gerçekleşmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listele();
                    Temizle();
                }
                else 
                {
                    MessageBox.Show("Güncelleme İşlemi Gerçekleştirilemedi!","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtKulupId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtKulupAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            using (SqlConnection baglanti = BaglantiAc()) 
            {
                if (!AlanKontrol()) 
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtKulupId.Text)) 
                {
                    MessageBox.Show("Kulüp ID'si boş olamaz!","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                SqlCommand cmdSil=new SqlCommand("Delete From Tbl_Kulupler where KulupId=@p1",baglanti);
                cmdSil.Parameters.Add("@p1", SqlDbType.Int).Value = Convert.ToInt32(txtKulupId.Text);
                cmdSil.ExecuteNonQuery();
                MessageBox.Show("Silme İşlemi Gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            FrmOgretmen frm=new FrmOgretmen();
            frm.Show();
            this.Close();
        }

        private void pictureBox7_MouseHover(object sender, EventArgs e)
        {
            pictureBox7.BackColor=Color.LightYellow;

        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.BackColor=Color.Transparent;
        }

        
    }
}
