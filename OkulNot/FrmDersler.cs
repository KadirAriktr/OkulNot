using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulNot
{
    public partial class FrmDersler : Form
    {
        public FrmDersler()
        {
            InitializeComponent();
        }
        private void Temizle()
        {
            foreach (Control control in this.Controls)
            {
                if (control is GroupBox)
                {
                    foreach (Control altControl in control.Controls)
                    {
                        if (altControl is TextBox)
                        {
                            ((TextBox)altControl).Clear();
                        }
                    }
                }
            }
        }
        private bool AlanKontrol() 
        {
            if (string.IsNullOrWhiteSpace(txtDersAd.Text)) 
            {
                MessageBox.Show("Ders Adı geçersizdir!","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return false;
            }
            return true;
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
            pictureBox6.BackColor=Color.Transparent;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            FrmOgretmen frm = new FrmOgretmen();
            frm.Show();
            this.Hide();
        }

        private void pictureBox7_MouseHover(object sender, EventArgs e)
        {
            pictureBox7.BackColor = Color.LightYellow;

        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.BackColor=Color.Transparent; 
        }
        OkulNotSistemi.DataSet1TableAdapters.Tbl_DerslerTableAdapter ds = new OkulNotSistemi.DataSet1TableAdapters.Tbl_DerslerTableAdapter();
        private void FrmDersler_Load(object sender, EventArgs e)
        {
           
            dataGridView1.DataSource = ds.DersListesi();
        }
        
        
        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (!AlanKontrol()) 
            {
                
                Temizle();
                return;
            }
            
            int sayi =(int)ds.DersVarmi(txtDersAd.Text);
            if (sayi > 0) 
            {
                Temizle();
                MessageBox.Show("Bu ders zaten mevcut!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ds.DersEkle(txtDersAd.Text);
            MessageBox.Show("Ders Başarıyla Eklendi.","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
            Temizle();
            dataGridView1.DataSource = ds.DersListesi();
            

        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.DersListesi();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen=dataGridView1.SelectedCells[0].RowIndex;
            txtDersId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtDersAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (!AlanKontrol()) 
            {
                return;
            }
            int sayi = (int)ds.DersVarmi(txtDersAd.Text);
            if (sayi>0) 
            {
                ds.DersGuncelle(txtDersAd.Text, byte.Parse(txtDersId.Text));
                MessageBox.Show("Güncelleme işlemi gerçekleştirilmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Temizle();
                dataGridView1.DataSource = ds.DersListesi();
            }
            else
            {
                MessageBox.Show("Geçersiz Ders Adı!","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                Temizle();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (!AlanKontrol())
                {
                    return;
                }

                int sayi = (int)ds.DersEslesiyorMu(byte.Parse(txtDersId.Text), txtDersAd.Text);
                if (sayi==0 )
                {
                    MessageBox.Show("Bu ID ile eşleşen bir ders bulunamadı!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Temizle();
                    return;
                }
                ds.DersSil(byte.Parse(txtDersId.Text),txtDersAd.Text);
                MessageBox.Show("Silme işlemi gerçekleştirilmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Temizle();
                dataGridView1.DataSource = ds.DersListesi();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
           
        }
    }

