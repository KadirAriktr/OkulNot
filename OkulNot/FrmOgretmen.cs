using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OkulNot
{
    public partial class FrmOgretmen : Form
    {
        public FrmOgretmen()
        {
            InitializeComponent();
        }

        private void btnKulupİslemleri_Click(object sender, EventArgs e)
        {
            FrmKulupİslemleri frm=new FrmKulupİslemleri();
            frm.ShowDialog();
            this.Close();
        }

        private void btnDersİslemleri_Click(object sender, EventArgs e)
        {
            FrmDersler frm=new FrmDersler();
            frm.Show();
            this.Hide();
        }

        private void btnOgrenciİslemleri_Click(object sender, EventArgs e)
        {
            FrmOgrenci frm=new FrmOgrenci();
            frm.Show();
            this.Hide();
        }
    }
}
