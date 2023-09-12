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

namespace PizzaciSon1Mutfak
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        public static string ConnectionString = "Data Source=.;Initial Catalog = Pizzacı; Integrated Security = True";
        public static int masaNo = 0;
        public static string masaYeri = "";
        public static string fisNotu = "";

        public void dinamikMetodSalon(object sender, EventArgs e)
        {
            SqlConnection bg = new SqlConnection(ConnectionString);
            string listBox = "";
            int ucret = 0;
            bg.Open();
            SqlCommand cmd = new SqlCommand("select * from ListboxKontrol where masaNo=" + ((sender as System.Windows.Forms.Button).TabIndex) + " and masaYeri='Salon' and durum=1", bg);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                listBox = (string)dr["listbox"];
                ucret = (int)dr["tutar"];
                fisNotu = (string)dr["fisNotu"];
            }
            bg.Close();

            lstViewSiparis.Items.Clear();
            if (listBox != "")
            {
                char ayrac = ',';
                char ayrac2 = '/';
                string[] parcalar = listBox.Split(ayrac);
                for (int i = 0; i < parcalar.Length - 1; i++)
                {
                    string[] Urun = parcalar[i].Split(ayrac2);
                    var satir = new ListViewItem(Urun);
                    lstViewSiparis.Items.Add(satir);
                }
            }
            btnMasa.Text = "Masa Yeri: Salon \n Masa No: " + ((sender as System.Windows.Forms.Button).TabIndex);
            txtBxToplamTutar.Text = "Toplam Tutar: " + ucret;
            masaNo = ((sender as System.Windows.Forms.Button).TabIndex);
            masaYeri = "Salon";
        }

        public void dinamikMetodBahce(object sender, EventArgs e)
        {
            SqlConnection bg = new SqlConnection(ConnectionString);
            string listBox = "";
            int ucret = 0;
            bg.Open();
            SqlCommand cmd = new SqlCommand("select * from ListboxKontrol where masaNo=" + ((sender as System.Windows.Forms.Button).TabIndex) + " and masaYeri='Bahçe' and durum=1", bg);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                listBox = (string)dr["listbox"];
                ucret = (int)dr["tutar"];
                fisNotu = (string)dr["fisNotu"];
            }
            bg.Close();

            lstViewSiparis.Items.Clear();
            if (listBox != "")
            {
                char ayrac = ',';
                char ayrac2 = '/';
                string[] parcalar = listBox.Split(ayrac);
                for (int i = 0; i < parcalar.Length - 1; i++)
                {
                    string[] Urun = parcalar[i].Split(ayrac2);
                    var satir = new ListViewItem(Urun);
                    lstViewSiparis.Items.Add(satir);
                }
            }
            btnMasa.Text = "Masa Yeri: Bahçe \n Masa No: " + ((sender as System.Windows.Forms.Button).TabIndex);
            txtBxToplamTutar.Text = "Toplam Tutar: " + ucret;
            masaNo = ((sender as System.Windows.Forms.Button).TabIndex);
            masaYeri = "Bahçe";
        }


        private void Anasayfa_Load(object sender, EventArgs e)
        {
            SqlConnection bg = new SqlConnection(ConnectionString);
            int masaAdet = 0;
            bg.Open();
            SqlCommand cmd = new SqlCommand("select count(masaNo) from ListboxKontrol where durum=1 and masaYeri='Salon'", bg);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                masaAdet = (int)dr[0];
            }
            bg.Close();

            int masaNo = 0;
            List<int> No = new List<int>();
            List<int> ucret = new List<int>();
            List<string> tarih = new List<string>();
            for (int i = 1; i <= masaAdet; i++)
            {
                bg.Open();
                cmd = new SqlCommand("Select tablo.* From (SELECT ROW_NUMBER() OVER (ORDER BY masaNo) indexer, * from ListboxKontrol where durum=1 and masaYeri='Salon') tablo where tablo.indexer=" + i + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                { masaNo = ((int)dr["masaNo"]); }
                bg.Close();
                bg.Open();
                cmd = new SqlCommand("select * from ListboxKontrol where masaYeri='Salon' and durum=1 and masaNo=" + masaNo + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    No.Add((int)dr["masaNo"]);
                    ucret.Add((int)dr["tutar"]);
                }
                bg.Close();
            }
            for (int i = 0; i < masaAdet; i++)
            {
                int mutfak = 0;
                string siparisDurumu = "";
                bg.Open();
                SqlCommand komut = new SqlCommand("select * from Masalar where masaNo=" + No[(i)] + " and masaYeri='Salon'", bg);
                dr = komut.ExecuteReader();
                while (dr.Read())
                { mutfak = (int)dr["mutfak"]; }
                bg.Close();
                if (mutfak == 1)
                    siparisDurumu = "Sipariş Geldi";
                if (mutfak == 2)
                    siparisDurumu = "Sipariş Onaylandı";
                if (mutfak == 3)
                    siparisDurumu = "Sipariş Gönderildi";
                if (mutfak == 4)
                    siparisDurumu = "Siparis İptal Edildi";

                System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
                btn.Name = "Buton" + i.ToString();
                btn.Size = new Size(290, 150);
                try { btn.Text = ("Masa Yeri:Salon\n " + "Masa No: " + No[(i)] + "\n" + "Fiyat: " + ucret[(i)] + "\n" + "Sipariş Durumu: " + siparisDurumu); }
                catch (Exception) { }
                btn.Margin = new Padding(0);
                btn.Font = new Font("Microsoft JhengHei", 14, FontStyle.Bold);
                btn.BackColor = Color.Firebrick;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                panelSalon.Controls.Add(btn);
                btn.Click += new EventHandler(dinamikMetodSalon);
                btn.TabIndex = No[(i)];

                bg.Open();
                cmd = new SqlCommand("select mutfak from Masalar where masaYeri='Salon' and masaNo=" + No[(i)] + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                    mutfak = (int)dr[0];
                bg.Close();
                if (mutfak == 1)
                    btn.BackColor = Color.Orange;
                if (mutfak == 2)
                    btn.BackColor = Color.Green;
                if (mutfak == 3)
                    btn.BackColor = Color.Turquoise;
                if (mutfak == 4)
                    btn.BackColor = Color.Red;
            }




            int masaAdet2 = 0;
            bg.Open();
            cmd = new SqlCommand("select count(masaNo) from ListboxKontrol where durum=1 and masaYeri='Bahçe'", bg);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                masaAdet2 = (int)dr[0];
            }
            bg.Close();

            int masaNo2 = 0;
            List<int> No2 = new List<int>();
            List<int> ucret2 = new List<int>();
            List<string> tarih2 = new List<string>();
            for (int i = 1; i <= masaAdet2; i++)
            {
                bg.Open();
                cmd = new SqlCommand("Select tablo.* From (SELECT ROW_NUMBER() OVER (ORDER BY masaNo) indexer, * from ListboxKontrol where durum=1 and masaYeri='Bahçe') tablo where tablo.indexer=" + i + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                { masaNo2 = ((int)dr["masaNo"]); }
                bg.Close();
                bg.Open();
                cmd = new SqlCommand("select * from ListboxKontrol where masaYeri='Bahçe' and durum=1 and masaNo=" + masaNo2 + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    No2.Add((int)dr["masaNo"]);
                    ucret2.Add((int)dr["tutar"]);
                }
                bg.Close();
            }
            for (int i = 0; i < masaAdet2; i++)
            {
                int mutfak = 0;
                string siparisDurumu = "";
                bg.Open();
                SqlCommand komut = new SqlCommand("select * from Masalar where masaNo=" + No2[(i)] + " and masaYeri='Bahçe'", bg);
                dr = komut.ExecuteReader();
                while (dr.Read())
                { mutfak = (int)dr["mutfak"]; }
                bg.Close();
                if (mutfak == 1)
                    siparisDurumu = "Sipariş Geldi";
                if (mutfak == 2)
                    siparisDurumu = "Sipariş Onaylandı";
                if (mutfak == 3)
                    siparisDurumu = "Sipariş Gönderildi";
                if (mutfak == 4)
                    siparisDurumu = "Siparis İptal Edildi";

                System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
                btn.Name = "Buton" + i.ToString();
                btn.Size = new Size(290, 150);
                try { btn.Text = ("Masa Yeri:Bahçe\n " + "Masa No: " + No2[(i)] + "\n" + "Fiyat: " + ucret2[(i)] + "\n" + "Sipariş Durumu: " + siparisDurumu); }
                catch (Exception) { }
                btn.Margin = new Padding(0);
                btn.Font = new Font("Microsoft JhengHei", 14, FontStyle.Bold);
                btn.BackColor = Color.Firebrick;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                panelBahce.Controls.Add(btn);
                btn.Click += new EventHandler(dinamikMetodBahce);
                btn.TabIndex = No2[(i)];

                bg.Open();
                cmd = new SqlCommand("select mutfak from Masalar where masaYeri='Bahçe' and masaNo=" + No2[(i)] + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                    mutfak = (int)dr[0];
                bg.Close();
                if (mutfak == 1)
                    btn.BackColor = Color.Orange;
                if (mutfak == 2)
                    btn.BackColor = Color.Green;
                if (mutfak == 3)
                    btn.BackColor = Color.Turquoise;
                if (mutfak == 4)
                    btn.BackColor = Color.Red;
            }
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            lstViewSiparis.Items.Clear();
            txtBxToplamTutar.Text = "Toplam Tutar: 0.00";
            btnMasa.Text = "";

            panelSalon.Controls.Clear();
            SqlConnection bg = new SqlConnection(ConnectionString);
            int masaAdet = 0;
            bg.Open();
            SqlCommand cmd = new SqlCommand("select count(masaNo) from ListboxKontrol where durum=1 and masaYeri='Salon'", bg);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                masaAdet = (int)dr[0];
            }
            bg.Close();

            int masaNo = 0;
            List<int> No = new List<int>();
            List<int> ucret = new List<int>();
            List<string> tarih = new List<string>();
            for (int i = 1; i <= masaAdet; i++)
            {
                bg.Open();
                cmd = new SqlCommand("Select tablo.* From (SELECT ROW_NUMBER() OVER (ORDER BY masaNo) indexer, * from ListboxKontrol where durum=1 and masaYeri='Salon') tablo where tablo.indexer=" + i + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                { masaNo = ((int)dr["masaNo"]); }
                bg.Close();
                bg.Open();
                cmd = new SqlCommand("select * from ListboxKontrol where masaYeri='Salon' and durum=1 and masaNo=" + masaNo + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    No.Add((int)dr["masaNo"]);
                    ucret.Add((int)dr["tutar"]);
                }
                bg.Close();
            }
            for (int i = 0; i < masaAdet; i++)
            {
                int mutfak = 0;
                string siparisDurumu = "";
                bg.Open();
                SqlCommand komut = new SqlCommand("select * from Masalar where masaNo=" + No[(i)] + " and masaYeri='Salon'", bg);
                dr = komut.ExecuteReader();
                while (dr.Read())
                { mutfak = (int)dr["mutfak"]; }
                bg.Close();
                if (mutfak == 1)
                    siparisDurumu = "Sipariş Geldi";
                if (mutfak == 2)
                    siparisDurumu = "Sipariş Onaylandı";
                if (mutfak == 3)
                    siparisDurumu = "Sipariş Gönderildi";
                if (mutfak == 4)
                    siparisDurumu = "Siparis İptal Edildi";

                System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
                btn.Name = "Buton" + i.ToString();
                btn.Size = new Size(290, 150);
                try { btn.Text = ("Masa Yeri:Salon\n " + "Masa No: " + No[(i)] + "\n" + "Fiyat: " + ucret[(i)] + "\n" + "Sipariş Durumu: " + siparisDurumu); }
                catch (Exception) { }
                btn.Margin = new Padding(0);
                btn.Font = new Font("Microsoft JhengHei", 14, FontStyle.Bold);
                btn.BackColor = Color.Firebrick;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                panelSalon.Controls.Add(btn);
                btn.Click += new EventHandler(dinamikMetodSalon);
                btn.TabIndex = No[(i)];

                bg.Open();
                cmd = new SqlCommand("select mutfak from Masalar where masaYeri='Salon' and masaNo=" + No[(i)] + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                    mutfak = (int)dr[0];
                bg.Close();
                if (mutfak == 1)
                    btn.BackColor = Color.Orange;
                if (mutfak == 2)
                    btn.BackColor = Color.Green;
                if (mutfak == 3)
                    btn.BackColor = Color.Turquoise;
                if (mutfak == 4)
                    btn.BackColor = Color.Red;
            }



            panelBahce.Controls.Clear();
            int masaAdet2 = 0;
            bg.Open();
            cmd = new SqlCommand("select count(masaNo) from ListboxKontrol where durum=1 and masaYeri='Bahçe'", bg);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                masaAdet2 = (int)dr[0];
            }
            bg.Close();

            int masaNo2 = 0;
            List<int> No2 = new List<int>();
            List<int> ucret2 = new List<int>();
            List<string> tarih2 = new List<string>();
            for (int i = 1; i <= masaAdet2; i++)
            {
                bg.Open();
                cmd = new SqlCommand("Select tablo.* From (SELECT ROW_NUMBER() OVER (ORDER BY masaNo) indexer, * from ListboxKontrol where durum=1 and masaYeri='Bahçe') tablo where tablo.indexer=" + i + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                { masaNo2 = ((int)dr["masaNo"]); }
                bg.Close();
                bg.Open();
                cmd = new SqlCommand("select * from ListboxKontrol where masaYeri='Bahçe' and durum=1 and masaNo=" + masaNo2 + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    No2.Add((int)dr["masaNo"]);
                    ucret2.Add((int)dr["tutar"]);
                }
                bg.Close();
            }
            for (int i = 0; i < masaAdet2; i++)
            {
                int mutfak = 0;
                string siparisDurumu = "";
                bg.Open();
                SqlCommand komut = new SqlCommand("select * from Masalar where masaNo=" + No2[(i)] + " and masaYeri='Bahçe'", bg);
                dr = komut.ExecuteReader();
                while (dr.Read())
                { mutfak = (int)dr["mutfak"]; }
                bg.Close();
                if (mutfak == 1)
                    siparisDurumu = "Sipariş Geldi";
                if (mutfak == 2)
                    siparisDurumu = "Sipariş Onaylandı";
                if (mutfak == 3)
                    siparisDurumu = "Sipariş Gönderildi";
                if (mutfak == 4)
                    siparisDurumu = "Siparis İptal Edildi";

                System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
                btn.Name = "Buton" + i.ToString();
                btn.Size = new Size(290, 150);
                try { btn.Text = ("Masa Yeri:Bahçe\n " + "Masa No: " + No2[(i)] + "\n" + "Fiyat: " + ucret2[(i)] + "\n" + "Sipariş Durumu: " + siparisDurumu); }
                catch (Exception) { }
                btn.Margin = new Padding(0);
                btn.Font = new Font("Microsoft JhengHei", 14, FontStyle.Bold);
                btn.BackColor = Color.Firebrick;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                panelBahce.Controls.Add(btn);
                btn.Click += new EventHandler(dinamikMetodBahce);
                btn.TabIndex = No2[(i)];

                bg.Open();
                cmd = new SqlCommand("select mutfak from Masalar where masaYeri='Bahçe' and masaNo=" + No2[(i)] + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                    mutfak = (int)dr[0];
                bg.Close();
                if (mutfak == 1)
                    btn.BackColor = Color.Orange;
                if (mutfak == 2)
                    btn.BackColor = Color.Green;
                if (mutfak == 3)
                    btn.BackColor = Color.Turquoise;
                if (mutfak == 4)
                    btn.BackColor = Color.Red;
            }
        }

        private void btnYazdir_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            SqlConnection bg = new SqlConnection(ConnectionString);

            int adisyonAralik = 0;
            int aralik = 30;
            try
            {
                Font font = new Font("Arial", 14);
                SolidBrush firca = new SolidBrush(Color.Black);
                // Pen kalem = new Pen(Color.Black);
                e.Graphics.DrawString($"Tarih={DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}", font, firca, 50, 25);

                font = new Font("Arial", 15, FontStyle.Bold);
                Font cizgi = new Font("Arial", 12);
                e.Graphics.DrawString("Bölüm", font, firca, 100, 70);
                e.Graphics.DrawString("-------------", cizgi, firca, 96, 82);
                e.Graphics.DrawString("Masa", font, firca, 200, 70);
                e.Graphics.DrawString("-----------", cizgi, firca, 197, 82);
                e.Graphics.DrawString("Adisyon No", font, firca, 285, 70);
                e.Graphics.DrawString("---------------------", cizgi, firca, 282, 82);

                font = new Font("Arial", 16);
                if (masaYeri == "Salon")
                {
                    e.Graphics.DrawString("Salon", font, firca, 100, 100);
                    e.Graphics.DrawString(masaNo.ToString(), font, firca, 215, 100);
                }
                else if (masaYeri == "Bahçe")
                {
                    e.Graphics.DrawString("Bahçe", font, firca, 100, 100);
                    e.Graphics.DrawString(masaNo.ToString(), font, firca, 215, 100);
                }

                int adisyonNo = 0;
                bg.Open();
                SqlCommand komut = new SqlCommand("select Count(adisyonNo) as AdisyonNo from GunlukAdisyon", bg);
                SqlDataReader dr = komut.ExecuteReader();
                while (dr.Read())
                { adisyonNo = (int)dr["AdisyonNo"]; }
                bg.Close();
                e.Graphics.DrawString((adisyonNo + 1).ToString(), font, firca, 325, 100);

                for (int i = 0; i < lstViewSiparis.Items.Count; i++)
                {
                    font = new Font("Arial", 16);
                    if (lstViewSiparis.Items[i].SubItems[0].Text == "0")
                    {
                        string[] dizi = new string[lstViewSiparis.Items.Count];
                        dizi[i] = lstViewSiparis.Items[i].SubItems[1].Text + " - " + lstViewSiparis.Items[i].SubItems[2].Text + " - " + lstViewSiparis.Items[i].SubItems[3].Text;
                        if (i == 0)
                            e.Graphics.DrawString(dizi[i], font, firca, 100, 148);
                        else
                            e.Graphics.DrawString(dizi[i], font, firca, 100, (148 + (aralik * i)));
                        adisyonAralik = 148 + (aralik * i);
                    }

                }
                font = new Font("Arial", 16, FontStyle.Bold);
                e.Graphics.DrawString("------------------------------------------------", cizgi, firca, 100, (adisyonAralik + aralik + 5));
                e.Graphics.DrawString(("Fiş Notu: " + fisNotu), font, firca, 100, (adisyonAralik + 67));

            }
            catch (Exception) { }
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            SqlConnection bg = new SqlConnection(ConnectionString);
            bg.Open();
            string sql = "update Masalar set mutfak=2 where masaNo=" + masaNo + " and masaYeri='" + masaYeri + "'";
            SqlCommand cmd = new SqlCommand(sql, bg);
            cmd.ExecuteNonQuery();
            bg.Close();
            MessageBox.Show("Sipariş Onaylandı");

            lstViewSiparis.Items.Clear();
            txtBxToplamTutar.Text = "Toplam Tutar: 0.00";
            btnMasa.Text = "";

            panelSalon.Controls.Clear();
            int masaAdet = 0;
            bg.Open();
            cmd = new SqlCommand("select count(masaNo) from ListboxKontrol where durum=1 and masaYeri='Salon'", bg);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                masaAdet = (int)dr[0];
            }
            bg.Close();

            int masaNo3 = 0;
            List<int> No = new List<int>();
            List<int> ucret = new List<int>();
            List<string> tarih = new List<string>();
            for (int i = 1; i <= masaAdet; i++)
            {
                bg.Open();
                cmd = new SqlCommand("Select tablo.* From (SELECT ROW_NUMBER() OVER (ORDER BY masaNo) indexer, * from ListboxKontrol where durum=1 and masaYeri='Salon') tablo where tablo.indexer=" + i + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                { masaNo3 = ((int)dr["masaNo"]); }
                bg.Close();
                bg.Open();
                cmd = new SqlCommand("select * from ListboxKontrol where masaYeri='Salon' and durum=1 and masaNo=" + masaNo3 + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    No.Add((int)dr["masaNo"]);
                    ucret.Add((int)dr["tutar"]);
                }
                bg.Close();
            }
            for (int i = 0; i < masaAdet; i++)
            {
                int mutfak = 0;
                string siparisDurumu = "";
                bg.Open();
                SqlCommand komut = new SqlCommand("select * from Masalar where masaNo=" + No[(i)] + " and masaYeri='Salon'", bg);
                dr = komut.ExecuteReader();
                while (dr.Read())
                { mutfak = (int)dr["mutfak"]; }
                bg.Close();
                if (mutfak == 1)
                    siparisDurumu = "Sipariş Geldi";
                if (mutfak == 2)
                    siparisDurumu = "Sipariş Onaylandı";
                if (mutfak == 3)
                    siparisDurumu = "Sipariş Gönderildi";
                if (mutfak == 4)
                    siparisDurumu = "Siparis İptal Edildi";

                System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
                btn.Name = "Buton" + i.ToString();
                btn.Size = new Size(290, 150);
                try { btn.Text = ("Masa Yeri:Salon\n " + "Masa No: " + No[(i)] + "\n" + "Fiyat: " + ucret[(i)] + "\n" + "Sipariş Durumu: " + siparisDurumu); }
                catch (Exception) { }
                btn.Margin = new Padding(0);
                btn.Font = new Font("Microsoft JhengHei", 14, FontStyle.Bold);
                btn.BackColor = Color.Firebrick;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                panelSalon.Controls.Add(btn);
                btn.Click += new EventHandler(dinamikMetodSalon);
                btn.TabIndex = No[(i)];

                bg.Open();
                cmd = new SqlCommand("select mutfak from Masalar where masaYeri='Salon' and masaNo=" + No[(i)] + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                    mutfak = (int)dr[0];
                bg.Close();
                if (mutfak == 1)
                    btn.BackColor = Color.Orange;
                if (mutfak == 2)
                    btn.BackColor = Color.Green;
                if (mutfak == 3)
                    btn.BackColor = Color.Turquoise;
                if (mutfak == 4)
                    btn.BackColor = Color.Red;
            }

            panelBahce.Controls.Clear();
            int masaAdet2 = 0;
            bg.Open();
            cmd = new SqlCommand("select count(masaNo) from ListboxKontrol where durum=1 and masaYeri='Bahçe'", bg);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                masaAdet2 = (int)dr[0];
            }
            bg.Close();

            int masaNo2 = 0;
            List<int> No2 = new List<int>();
            List<int> ucret2 = new List<int>();
            List<string> tarih2 = new List<string>();
            for (int i = 1; i <= masaAdet2; i++)
            {
                bg.Open();
                cmd = new SqlCommand("Select tablo.* From (SELECT ROW_NUMBER() OVER (ORDER BY masaNo) indexer, * from ListboxKontrol where durum=1 and masaYeri='Bahçe') tablo where tablo.indexer=" + i + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                { masaNo2 = ((int)dr["masaNo"]); }
                bg.Close();
                bg.Open();
                cmd = new SqlCommand("select * from ListboxKontrol where masaYeri='Bahçe' and durum=1 and masaNo=" + masaNo2 + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    No2.Add((int)dr["masaNo"]);
                    ucret2.Add((int)dr["tutar"]);
                }
                bg.Close();
            }
            for (int i = 0; i < masaAdet2; i++)
            {
                int mutfak = 0;
                string siparisDurumu = "";
                bg.Open();
                SqlCommand komut = new SqlCommand("select * from Masalar where masaNo=" + No2[(i)] + " and masaYeri='Bahçe'", bg);
                dr = komut.ExecuteReader();
                while (dr.Read())
                { mutfak = (int)dr["mutfak"]; }
                bg.Close();
                if (mutfak == 1)
                    siparisDurumu = "Sipariş Geldi";
                if (mutfak == 2)
                    siparisDurumu = "Sipariş Onaylandı";
                if (mutfak == 3)
                    siparisDurumu = "Sipariş Gönderildi";
                if (mutfak == 4)
                    siparisDurumu = "Siparis İptal Edildi";

                System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
                btn.Name = "Buton" + i.ToString();
                btn.Size = new Size(290, 150);
                try { btn.Text = ("Masa Yeri:Bahçe\n " + "Masa No: " + No2[(i)] + "\n" + "Fiyat: " + ucret2[(i)] + "\n" + "Sipariş Durumu: " + siparisDurumu); }
                catch (Exception) { }
                btn.Margin = new Padding(0);
                btn.Font = new Font("Microsoft JhengHei", 14, FontStyle.Bold);
                btn.BackColor = Color.Firebrick;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                panelBahce.Controls.Add(btn);
                btn.Click += new EventHandler(dinamikMetodBahce);
                btn.TabIndex = No2[(i)];

                bg.Open();
                cmd = new SqlCommand("select mutfak from Masalar where masaYeri='Bahçe' and masaNo=" + No2[(i)] + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                    mutfak = (int)dr[0];
                bg.Close();
                if (mutfak == 1)
                    btn.BackColor = Color.Orange;
                if (mutfak == 2)
                    btn.BackColor = Color.Green;
                if (mutfak == 3)
                    btn.BackColor = Color.Turquoise;
                if (mutfak == 4)
                    btn.BackColor = Color.Red;
            }
        }

        private void btnIptalEt_Click(object sender, EventArgs e)
        {
            int mutfak2 = 0;
            SqlConnection bg = new SqlConnection(ConnectionString);
            bg.Open();
            SqlCommand komut = new SqlCommand("select * from Masalar where masaNo=" + masaNo + " and masaYeri='" + masaYeri + "'", bg);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            { mutfak2 = (int)dr["mutfak"]; }
            bg.Close();

            if (mutfak2 == 1)
            {
                bg.Open();
                string sql = "update Masalar set mutfak=4 where masaNo=" + masaNo + " and masaYeri='" + masaYeri + "'";
                SqlCommand cmd = new SqlCommand(sql, bg);
                cmd.ExecuteNonQuery();
                bg.Close();
                MessageBox.Show("Sipariş İptal Edildi");

                lstViewSiparis.Items.Clear();
                txtBxToplamTutar.Text = "Toplam Tutar: 0.00";
                btnMasa.Text = "";

                panelSalon.Controls.Clear();
                int masaAdet = 0;
                bg.Open();
                cmd = new SqlCommand("select count(masaNo) from ListboxKontrol where durum=1 and masaYeri='Salon'", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    masaAdet = (int)dr[0];
                }
                bg.Close();

                int masaNo3 = 0;
                List<int> No = new List<int>();
                List<int> ucret = new List<int>();
                List<string> tarih = new List<string>();
                for (int i = 1; i <= masaAdet; i++)
                {
                    bg.Open();
                    cmd = new SqlCommand("Select tablo.* From (SELECT ROW_NUMBER() OVER (ORDER BY masaNo) indexer, * from ListboxKontrol where durum=1 and masaYeri='Salon') tablo where tablo.indexer=" + i + "", bg);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    { masaNo3 = ((int)dr["masaNo"]); }
                    bg.Close();
                    bg.Open();
                    cmd = new SqlCommand("select * from ListboxKontrol where masaYeri='Salon' and durum=1 and masaNo=" + masaNo3 + "", bg);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        No.Add((int)dr["masaNo"]);
                        ucret.Add((int)dr["tutar"]);
                    }
                    bg.Close();
                }
                for (int i = 0; i < masaAdet; i++)
                {
                    int mutfak = 0;
                    string siparisDurumu = "";
                    bg.Open();
                    komut = new SqlCommand("select * from Masalar where masaNo=" + No[(i)] + " and masaYeri='Salon'", bg);
                    dr = komut.ExecuteReader();
                    while (dr.Read())
                    { mutfak = (int)dr["mutfak"]; }
                    bg.Close();
                    if (mutfak == 1)
                        siparisDurumu = "Sipariş Geldi";
                    if (mutfak == 2)
                        siparisDurumu = "Sipariş Onaylandı";
                    if (mutfak == 3)
                        siparisDurumu = "Sipariş Gönderildi";
                    if (mutfak == 4)
                        siparisDurumu = "Siparis İptal Edildi";

                    System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
                    btn.Name = "Buton" + i.ToString();
                    btn.Size = new Size(290, 150);
                    try { btn.Text = ("Masa Yeri:Salon\n " + "Masa No: " + No[(i)] + "\n" + "Fiyat: " + ucret[(i)] + "\n" + "Sipariş Durumu: " + siparisDurumu); }
                    catch (Exception) { }
                    btn.Margin = new Padding(0);
                    btn.Font = new Font("Microsoft JhengHei", 14, FontStyle.Bold);
                    btn.BackColor = Color.Firebrick;
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    panelSalon.Controls.Add(btn);
                    btn.Click += new EventHandler(dinamikMetodSalon);
                    btn.TabIndex = No[(i)];

                    bg.Open();
                    cmd = new SqlCommand("select mutfak from Masalar where masaYeri='Salon' and masaNo=" + No[(i)] + "", bg);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                        mutfak = (int)dr[0];
                    bg.Close();
                    if (mutfak == 1)
                        btn.BackColor = Color.Orange;
                    if (mutfak == 2)
                        btn.BackColor = Color.Green;
                    if (mutfak == 3)
                        btn.BackColor = Color.Turquoise;
                    if (mutfak == 4)
                        btn.BackColor = Color.Red;
                }



                panelBahce.Controls.Clear();
                int masaAdet2 = 0;
                bg.Open();
                cmd = new SqlCommand("select count(masaNo) from ListboxKontrol where durum=1 and masaYeri='Bahçe'", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    masaAdet2 = (int)dr[0];
                }
                bg.Close();

                int masaNo2 = 0;
                List<int> No2 = new List<int>();
                List<int> ucret2 = new List<int>();
                List<string> tarih2 = new List<string>();
                for (int i = 1; i <= masaAdet2; i++)
                {
                    bg.Open();
                    cmd = new SqlCommand("Select tablo.* From (SELECT ROW_NUMBER() OVER (ORDER BY masaNo) indexer, * from ListboxKontrol where durum=1 and masaYeri='Bahçe') tablo where tablo.indexer=" + i + "", bg);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    { masaNo2 = ((int)dr["masaNo"]); }
                    bg.Close();
                    bg.Open();
                    cmd = new SqlCommand("select * from ListboxKontrol where masaYeri='Bahçe' and durum=1 and masaNo=" + masaNo2 + "", bg);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        No2.Add((int)dr["masaNo"]);
                        ucret2.Add((int)dr["tutar"]);
                    }
                    bg.Close();
                }
                for (int i = 0; i < masaAdet2; i++)
                {
                    int mutfak = 0;
                    string siparisDurumu = "";
                    bg.Open();
                    komut = new SqlCommand("select * from Masalar where masaNo=" + No2[(i)] + " and masaYeri='Bahçe'", bg);
                    dr = komut.ExecuteReader();
                    while (dr.Read())
                    { mutfak = (int)dr["mutfak"]; }
                    bg.Close();
                    if (mutfak == 1)
                        siparisDurumu = "Sipariş Geldi";
                    if (mutfak == 2)
                        siparisDurumu = "Sipariş Onaylandı";
                    if (mutfak == 3)
                        siparisDurumu = "Sipariş Gönderildi";
                    if (mutfak == 4)
                        siparisDurumu = "Siparis İptal Edildi";

                    System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
                    btn.Name = "Buton" + i.ToString();
                    btn.Size = new Size(290, 150);
                    try { btn.Text = ("Masa Yeri:Bahçe\n " + "Masa No: " + No2[(i)] + "\n" + "Fiyat: " + ucret2[(i)] + "\n" + "Sipariş Durumu: " + siparisDurumu); }
                    catch (Exception) { }
                    btn.Margin = new Padding(0);
                    btn.Font = new Font("Microsoft JhengHei", 14, FontStyle.Bold);
                    btn.BackColor = Color.Orange;
                    btn.ForeColor = Color.White;
                    btn.FlatStyle = FlatStyle.Flat;
                    panelBahce.Controls.Add(btn);
                    btn.Click += new EventHandler(dinamikMetodBahce);
                    btn.TabIndex = No2[(i)];

                    bg.Open();
                    cmd = new SqlCommand("select mutfak from Masalar where masaYeri='Bahçe' and masaNo=" + No2[(i)] + "", bg);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                        mutfak = (int)dr[0];
                    bg.Close();
                    if (mutfak == 1)
                        btn.BackColor = Color.Orange;
                    if (mutfak == 2)
                        btn.BackColor = Color.Green;
                    if (mutfak == 3)
                        btn.BackColor = Color.Turquoise;
                    if (mutfak == 4)
                        btn.BackColor = Color.Red;
                }
            }
            else
                MessageBox.Show("İptal Edemezsiniz!");
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            SqlConnection bg = new SqlConnection(ConnectionString);
            bg.Open();
            string sql = "update Masalar set mutfak=3 where masaNo=" + masaNo + " and masaYeri='" + masaYeri + "'";
            SqlCommand cmd = new SqlCommand(sql, bg);



            cmd.ExecuteNonQuery();
            bg.Close();
            MessageBox.Show("Sipariş Gönderildi");

            lstViewSiparis.Items.Clear();
            txtBxToplamTutar.Text = "Toplam Tutar: 0.00";
            btnMasa.Text = "";

            panelSalon.Controls.Clear();
            int masaAdet = 0;
            bg.Open();
            cmd = new SqlCommand("select count(masaNo) from ListboxKontrol where durum=1 and masaYeri='Salon'", bg);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                masaAdet = (int)dr[0];
            }
            bg.Close();

            int masaNo3 = 0;
            List<int> No = new List<int>();
            List<int> ucret = new List<int>();
            List<string> tarih = new List<string>();
            for (int i = 1; i <= masaAdet; i++)
            {
                bg.Open();
                cmd = new SqlCommand("Select tablo.* From (SELECT ROW_NUMBER() OVER (ORDER BY masaNo) indexer, * from ListboxKontrol where durum=1 and masaYeri='Salon') tablo where tablo.indexer=" + i + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                { masaNo3 = ((int)dr["masaNo"]); }
                bg.Close();
                bg.Open();
                cmd = new SqlCommand("select * from ListboxKontrol where masaYeri='Salon' and durum=1 and masaNo=" + masaNo3 + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    No.Add((int)dr["masaNo"]);
                    ucret.Add((int)dr["tutar"]);
                }
                bg.Close();
            }
            for (int i = 0; i < masaAdet; i++)
            {
                int mutfak = 0;
                string siparisDurumu = "";
                bg.Open();
                SqlCommand komut = new SqlCommand("select * from Masalar where masaNo=" + No[(i)] + " and masaYeri='Salon'", bg);
                dr = komut.ExecuteReader();
                while (dr.Read())
                { mutfak = (int)dr["mutfak"]; }
                bg.Close();
                if (mutfak == 1)
                    siparisDurumu = "Sipariş Geldi";
                if (mutfak == 2)
                    siparisDurumu = "Sipariş Onaylandı";
                if (mutfak == 3)
                    siparisDurumu = "Sipariş Gönderildi";
                if (mutfak == 4)
                    siparisDurumu = "Siparis İptal Edildi";

                System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
                btn.Name = "Buton" + i.ToString();
                btn.Size = new Size(290, 150);
                try { btn.Text = ("Masa Yeri:Salon\n " + "Masa No: " + No[(i)] + "\n" + "Fiyat: " + ucret[(i)] + "\n" + "Sipariş Durumu: " + siparisDurumu); }
                catch (Exception) { }
                btn.Margin = new Padding(0);
                btn.Font = new Font("Microsoft JhengHei", 14, FontStyle.Bold);
                btn.BackColor = Color.Firebrick;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                panelSalon.Controls.Add(btn);
                btn.Click += new EventHandler(dinamikMetodSalon);
                btn.TabIndex = No[(i)];

                bg.Open();
                cmd = new SqlCommand("select mutfak from Masalar where masaYeri='Salon' and masaNo=" + No[(i)] + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                    mutfak = (int)dr[0];
                bg.Close();
                if (mutfak == 1)
                    btn.BackColor = Color.Orange;
                if (mutfak == 2)
                    btn.BackColor = Color.Green;
                if (mutfak == 3)
                    btn.BackColor = Color.Turquoise;
                if (mutfak == 4)
                    btn.BackColor = Color.Red;
            }

            panelBahce.Controls.Clear();
            int masaAdet2 = 0;
            bg.Open();
            cmd = new SqlCommand("select count(masaNo) from ListboxKontrol where durum=1 and masaYeri='Bahçe'", bg);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                masaAdet2 = (int)dr[0];
            }
            bg.Close();

            int masaNo4 = 0;
            List<int> No2 = new List<int>();
            List<int> ucret2 = new List<int>();
            List<string> tarih2 = new List<string>();
            for (int i = 1; i <= masaAdet2; i++)
            {
                bg.Open();
                cmd = new SqlCommand("Select tablo.* From (SELECT ROW_NUMBER() OVER (ORDER BY masaNo) indexer, * from ListboxKontrol where durum=1 and masaYeri='Bahçe') tablo where tablo.indexer=" + i + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                { masaNo4 = ((int)dr["masaNo"]); }
                bg.Close();
                bg.Open();
                cmd = new SqlCommand("select * from ListboxKontrol where masaYeri='Bahçe' and durum=1 and masaNo=" + masaNo4 + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    No2.Add((int)dr["masaNo"]);
                    ucret2.Add((int)dr["tutar"]);
                }
                bg.Close();
            }
            for (int i = 0; i < masaAdet2; i++)
            {
                int mutfak = 0;
                string siparisDurumu = "";
                bg.Open();
                SqlCommand komut = new SqlCommand("select * from Masalar where masaNo=" + No2[(i)] + " and masaYeri='Bahçe'", bg);
                dr = komut.ExecuteReader();
                while (dr.Read())
                { mutfak = (int)dr["mutfak"]; }
                bg.Close();
                if (mutfak == 1)
                    siparisDurumu = "Sipariş Geldi";
                if (mutfak == 2)
                    siparisDurumu = "Sipariş Onaylandı";
                if (mutfak == 3)
                    siparisDurumu = "Sipariş Gönderildi";
                if (mutfak == 4)
                    siparisDurumu = "Siparis İptal Edildi";

                System.Windows.Forms.Button btn = new System.Windows.Forms.Button();
                btn.Name = "Buton" + i.ToString();
                btn.Size = new Size(290, 150);
                try { btn.Text = ("Masa Yeri:Bahçe\n " + "Masa No: " + No2[(i)] + "\n" + "Fiyat: " + ucret2[(i)] + "\n" + "Sipariş Durumu: " + siparisDurumu); }
                catch (Exception) { }
                btn.Margin = new Padding(0);
                btn.Font = new Font("Microsoft JhengHei", 14, FontStyle.Bold);
                btn.BackColor = Color.Firebrick;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                panelBahce.Controls.Add(btn);
                btn.Click += new EventHandler(dinamikMetodBahce);
                btn.TabIndex = No2[(i)];

                bg.Open();
                cmd = new SqlCommand("select mutfak from Masalar where masaYeri='Bahçe' and masaNo=" + No2[(i)] + "", bg);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                    mutfak = (int)dr[0];
                bg.Close();
                if (mutfak == 1)
                    btn.BackColor = Color.Orange;
                if (mutfak == 2)
                    btn.BackColor = Color.Green;
                if (mutfak == 3)
                    btn.BackColor = Color.Turquoise;
                if (mutfak == 4)
                    btn.BackColor = Color.Red;
            }
        }
    }
}
