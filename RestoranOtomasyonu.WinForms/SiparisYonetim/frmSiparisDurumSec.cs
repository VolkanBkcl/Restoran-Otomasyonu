using DevExpress.XtraEditors;
using RestoranOtomasyonu.Entities.Enums;
using System;
using System.Linq;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms.SiparisYonetim
{
    public partial class frmSiparisDurumSec : DevExpress.XtraEditors.XtraForm
    {
        public SiparisDurumu SecilenDurum { get; private set; }

        public frmSiparisDurumSec(int mevcutDurum)
        {
            InitializeComponent();
            DurumlariYukle(mevcutDurum);
        }

        private void DurumlariYukle(int mevcutDurum)
        {

            comboDurum.Properties.Items.Clear();
            comboDurum.Properties.Items.Add("Sipariş Alındı");
            comboDurum.Properties.Items.Add("Hazırlanıyor");
            comboDurum.Properties.Items.Add("Hazır");
            comboDurum.Properties.Items.Add("Servis Edildi");


            string mevcutDurumMetni = GetDurumMetni((SiparisDurumu)mevcutDurum);
            var mevcutIndex = comboDurum.Properties.Items.IndexOf(mevcutDurumMetni);
            if (mevcutIndex >= 0)
            {
                comboDurum.SelectedIndex = mevcutIndex;
            }
        }

        private string GetDurumMetni(SiparisDurumu durum)
        {
            switch ((int)durum)
            {
                case 0: return "Sipariş Alındı";
                case 1: return "Hazırlanıyor";
                case 2: return "Hazır";
                case 3: return "Servis Edildi";
                default: return "Bilinmeyen";
            }
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            if (comboDurum.SelectedIndex < 0 || comboDurum.SelectedIndex >= comboDurum.Properties.Items.Count)
            {
                XtraMessageBox.Show("Lütfen bir durum seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string secilenMetin = comboDurum.Text;
            SecilenDurum = GetDurumFromMetin(secilenMetin);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private SiparisDurumu GetDurumFromMetin(string metin)
        {
            switch (metin)
            {
                case "Sipariş Alındı": return SiparisDurumu.SiparisAlindi;
                case "Hazırlanıyor": return SiparisDurumu.Hazirlaniyor;
                case "Hazır": return SiparisDurumu.Hazir;
                case "Servis Edildi": return SiparisDurumu.ServisEdildi;
                default: return SiparisDurumu.SiparisAlindi;
            }
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
