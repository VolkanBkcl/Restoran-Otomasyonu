using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using RestoranOtomasyonu.WinForms.Menular;
using RestoranOtomasyonu.WinForms.Urunler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.AnaMenu
{
    public partial class frmAnaMenu : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        void FormGetir(XtraForm frm)
        {
            frm.MdiParent = this;
            frm.Show();
        }
        public frmAnaMenu()
        {
            InitializeComponent();
        }

        private void btnUrunler_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmUrunler frm = new frmUrunler();
            FormGetir(frm);
        }

        private void btnMenuler_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmMenuler frm = new frmMenuler();
            frm.ShowDialog();
        }

        private void frmAnaMenu_Load(object sender, EventArgs e)
        {

        }
    }
}