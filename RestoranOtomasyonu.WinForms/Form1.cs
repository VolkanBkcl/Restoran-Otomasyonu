using RestoranOtomasyonu.Entities.DAL;
using RestoranOtomasyonu.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestoranOtomasyonu.WinForms
{
    public partial class Form1 : Form
    {
        RestoranContext context = new RestoranContext();
        KullaniciHareketleriDal KullaniciHareketleriDal = new KullaniciHareketleriDal();
        public Form1()
        {
            InitializeComponent();

        }
    }
}
