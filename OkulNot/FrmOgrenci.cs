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
using System.Diagnostics.Eventing.Reader;

namespace OkulNot
{
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox6_MouseHover(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.LightYellow;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BackColor= Color.Transparent;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            FrmOgretmen frm= new FrmOgretmen();
            frm.Show();
            this.Hide(); 
        }

        private void pictureBox7_MouseHover(object sender, EventArgs e)
        {
            pictureBox7.BackColor = Color.LightYellow;

        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.BackColor= Color.Transparent;
        }

        OkulNotSistemi.DataSet1TableAdapters.DataTable1TableAdapter ds = new OkulNotSistemi.DataSet1TableAdapters.DataTable1TableAdapter();
        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
            SqlConnection baglanti = new SqlConnection(@"Data Source=KADIR\SQLEXPRESS;Initial Catalog=OkulNot;Integrated Security=True;Encrypt=False");
            baglanti.Open();
            SqlCommand komut=new SqlCommand("Select*From Tbl_Kulupler",baglanti);
            DataTable dt=new DataTable();
            SqlDataAdapter da=new SqlDataAdapter(komut);
            da.Fill(dt);
            cbKulup.DisplayMember = "KulupAd";
            cbKulup.ValueMember = "KulupId";
            cbKulup.DataSource=dt;
        }
        public void Temizle() 
        {
            foreach (Control control in this.Controls)
            {
                if (control is GroupBox)
                {
                    foreach (Control control2 in control.Controls)
                    {
                        if (control2 is TextBox)
                        {
                            ((TextBox)control2).Clear();

                        }
                    }
                    foreach (Control control3 in control.Controls)
                    {
                        if (control3 is RadioButton)
                        {
                            ((RadioButton)control3).Checked = false;
                        }
                    }
                    
                }
            }
        }

        OkulNotSistemi.DataSet1TableAdapters.DataTable1TableAdapter ds2 = new OkulNotSistemi.DataSet1TableAdapters.DataTable1TableAdapter();
        private void btnEkle_Click(object sender, EventArgs e)
        {
            string c="";
            if (rbErkek.Checked == true) 
            {
                c = "Erkek";
            }
            if(rbKiz.Checked==true)
            {
                c = "Kız";
            }
            ds2.OgrenciEkle(txtOgrenciAd.Text,txtOgrenciSoyad.Text,byte.Parse(cbKulup.SelectedValue.ToString()),c);
            MessageBox.Show("Öğrenci Eklendi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            dataGridView1.DataSource = ds2.OgrenciListesi();
            Temizle();

        }
        


        private void btnListele_Click(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = ds2.OgrenciListesi();
            Temizle();
        }

        private void cbKulup_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtOgrenciId.Text=cbKulup.SelectedValue.ToString();
        }

        OkulNotSistemi.DataSet1TableAdapters.DataTable1TableAdapter ds4 = new OkulNotSistemi.DataSet1TableAdapters.DataTable1TableAdapter();
        private void btnSil_Click(object sender, EventArgs e)
        {
            ds4.OgrenciSil(int.Parse(txtOgrenciId.Text));
            MessageBox.Show("Seçilen Öğrenci Silindi.","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            dataGridView1.DataSource=ds4.OgrenciListesi();
            Temizle();
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtOgrenciId.Text=dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtOgrenciAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtOgrenciSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cbKulup.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            string cinsiyet = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            if (cinsiyet == "Erkek") 
            {
                rbErkek.Checked= true;
            }
            else if (cinsiyet == "Kız") 
            {
                rbKiz.Checked= true;
            }
            else 
            {
                MessageBox.Show("Geçersiz.");
            }
        }
        OkulNotSistemi.DataSet1TableAdapters.DataTable1TableAdapter ds3 = new OkulNotSistemi.DataSet1TableAdapters.DataTable1TableAdapter();
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            string cinsiyet = "";
            if (rbErkek.Checked == true) 
            {
                cinsiyet = "Erkek";

            }
            else if (rbKiz.Checked == true) 
            {
                cinsiyet = "Kız";
            }
            ds3.OgrenciGuncelle(txtOgrenciAd.Text, txtOgrenciSoyad.Text,byte.Parse(cbKulup.SelectedValue.ToString()),cinsiyet,int.Parse(txtOgrenciId.Text));
            MessageBox.Show("Öğrenci Güncellenmiştir.","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
            Temizle();
        }
    }
}
