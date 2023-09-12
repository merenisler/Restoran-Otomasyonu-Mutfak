namespace PizzaciSon1Mutfak
{
    partial class Anasayfa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Anasayfa));
            panelSalon = new FlowLayoutPanel();
            lstViewSiparis = new ListView();
            yazdir = new ColumnHeader();
            Adet = new ColumnHeader();
            Ürün = new ColumnHeader();
            Fiyatı = new ColumnHeader();
            urun = new ColumnHeader();
            kategori = new ColumnHeader();
            btnYazdir = new Button();
            btnOnayla = new Button();
            btnGonder = new Button();
            btnIptalEt = new Button();
            btnYenile = new Button();
            panelBahce = new FlowLayoutPanel();
            button1 = new Button();
            button2 = new Button();
            btnMasa = new Button();
            txtBxToplamTutar = new TextBox();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            printPreviewDialog1 = new PrintPreviewDialog();
            SuspendLayout();
            // 
            // panelSalon
            // 
            panelSalon.AutoScroll = true;
            panelSalon.Location = new Point(12, 76);
            panelSalon.Name = "panelSalon";
            panelSalon.Size = new Size(308, 507);
            panelSalon.TabIndex = 0;
            // 
            // lstViewSiparis
            // 
            lstViewSiparis.Columns.AddRange(new ColumnHeader[] { yazdir, Adet, Ürün, Fiyatı, urun, kategori });
            lstViewSiparis.Font = new Font("Microsoft Sans Serif", 12.75F, FontStyle.Regular, GraphicsUnit.Point);
            lstViewSiparis.Location = new Point(637, 76);
            lstViewSiparis.Name = "lstViewSiparis";
            lstViewSiparis.Size = new Size(321, 457);
            lstViewSiparis.TabIndex = 1;
            lstViewSiparis.UseCompatibleStateImageBehavior = false;
            lstViewSiparis.View = View.Details;
            // 
            // yazdir
            // 
            yazdir.Text = "yazdir";
            yazdir.Width = 1;
            // 
            // Adet
            // 
            Adet.Text = "Adet";
            // 
            // Ürün
            // 
            Ürün.Text = "Ürün";
            Ürün.Width = 185;
            // 
            // Fiyatı
            // 
            Fiyatı.Text = "Fiyatı";
            Fiyatı.Width = 65;
            // 
            // urun
            // 
            urun.Text = "urun";
            urun.Width = 1;
            // 
            // kategori
            // 
            kategori.Text = "kategori";
            kategori.Width = 1;
            // 
            // btnYazdir
            // 
            btnYazdir.BackColor = Color.LightBlue;
            btnYazdir.FlatAppearance.BorderSize = 0;
            btnYazdir.FlatStyle = FlatStyle.Flat;
            btnYazdir.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            btnYazdir.Location = new Point(964, 162);
            btnYazdir.Name = "btnYazdir";
            btnYazdir.Size = new Size(92, 62);
            btnYazdir.TabIndex = 4;
            btnYazdir.Text = "Yazdır";
            btnYazdir.UseVisualStyleBackColor = false;
            btnYazdir.Click += btnYazdir_Click;
            // 
            // btnOnayla
            // 
            btnOnayla.BackColor = Color.LimeGreen;
            btnOnayla.FlatAppearance.BorderSize = 0;
            btnOnayla.FlatStyle = FlatStyle.Flat;
            btnOnayla.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            btnOnayla.Location = new Point(964, 230);
            btnOnayla.Name = "btnOnayla";
            btnOnayla.Size = new Size(92, 62);
            btnOnayla.TabIndex = 5;
            btnOnayla.Text = "Onayla";
            btnOnayla.UseVisualStyleBackColor = false;
            btnOnayla.Click += btnOnayla_Click;
            // 
            // btnGonder
            // 
            btnGonder.BackColor = Color.Turquoise;
            btnGonder.FlatAppearance.BorderSize = 0;
            btnGonder.FlatStyle = FlatStyle.Flat;
            btnGonder.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            btnGonder.Location = new Point(964, 366);
            btnGonder.Name = "btnGonder";
            btnGonder.Size = new Size(92, 62);
            btnGonder.TabIndex = 6;
            btnGonder.Text = "Gönder";
            btnGonder.UseVisualStyleBackColor = false;
            btnGonder.Click += btnGonder_Click;
            // 
            // btnIptalEt
            // 
            btnIptalEt.BackColor = Color.Crimson;
            btnIptalEt.FlatAppearance.BorderSize = 0;
            btnIptalEt.FlatStyle = FlatStyle.Flat;
            btnIptalEt.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            btnIptalEt.Location = new Point(964, 298);
            btnIptalEt.Name = "btnIptalEt";
            btnIptalEt.Size = new Size(92, 62);
            btnIptalEt.TabIndex = 7;
            btnIptalEt.Text = "İptal Et";
            btnIptalEt.UseVisualStyleBackColor = false;
            btnIptalEt.Click += btnIptalEt_Click;
            // 
            // btnYenile
            // 
            btnYenile.BackColor = Color.LightYellow;
            btnYenile.FlatAppearance.BorderSize = 0;
            btnYenile.FlatStyle = FlatStyle.Flat;
            btnYenile.Font = new Font("Microsoft JhengHei UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            btnYenile.Location = new Point(12, 12);
            btnYenile.Name = "btnYenile";
            btnYenile.Size = new Size(622, 31);
            btnYenile.TabIndex = 8;
            btnYenile.Text = "Yenile";
            btnYenile.UseVisualStyleBackColor = false;
            btnYenile.Click += btnYenile_Click;
            // 
            // panelBahce
            // 
            panelBahce.AutoScroll = true;
            panelBahce.Location = new Point(326, 76);
            panelBahce.Name = "panelBahce";
            panelBahce.Size = new Size(308, 507);
            panelBahce.TabIndex = 1;
            // 
            // button1
            // 
            button1.BackColor = Color.CornflowerBlue;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(12, 46);
            button1.Name = "button1";
            button1.Size = new Size(308, 27);
            button1.TabIndex = 9;
            button1.Text = "Salon";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.CornflowerBlue;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Microsoft JhengHei UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            button2.Location = new Point(326, 46);
            button2.Name = "button2";
            button2.Size = new Size(308, 27);
            button2.TabIndex = 10;
            button2.Text = "Bahçe";
            button2.UseVisualStyleBackColor = false;
            // 
            // btnMasa
            // 
            btnMasa.BackColor = Color.CornflowerBlue;
            btnMasa.FlatAppearance.BorderSize = 0;
            btnMasa.FlatStyle = FlatStyle.Flat;
            btnMasa.Font = new Font("Microsoft JhengHei UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnMasa.Location = new Point(637, 12);
            btnMasa.Name = "btnMasa";
            btnMasa.Size = new Size(321, 61);
            btnMasa.TabIndex = 11;
            btnMasa.UseVisualStyleBackColor = false;
            // 
            // txtBxToplamTutar
            // 
            txtBxToplamTutar.BackColor = Color.Black;
            txtBxToplamTutar.BorderStyle = BorderStyle.None;
            txtBxToplamTutar.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            txtBxToplamTutar.ForeColor = Color.White;
            txtBxToplamTutar.Location = new Point(637, 535);
            txtBxToplamTutar.Multiline = true;
            txtBxToplamTutar.Name = "txtBxToplamTutar";
            txtBxToplamTutar.Size = new Size(321, 48);
            txtBxToplamTutar.TabIndex = 12;
            txtBxToplamTutar.Text = "Toplam Tutar: 0.00";
            txtBxToplamTutar.TextAlign = HorizontalAlignment.Center;
            // 
            // printDocument1
            // 
            printDocument1.PrintPage += printDocument1_PrintPage;
            // 
            // printPreviewDialog1
            // 
            printPreviewDialog1.AutoScrollMargin = new Size(0, 0);
            printPreviewDialog1.AutoScrollMinSize = new Size(0, 0);
            printPreviewDialog1.ClientSize = new Size(400, 300);
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.Enabled = true;
            printPreviewDialog1.Icon = (Icon)resources.GetObject("printPreviewDialog1.Icon");
            printPreviewDialog1.Name = "printPreviewDialog1";
            printPreviewDialog1.Visible = false;
            // 
            // Anasayfa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(1066, 596);
            Controls.Add(txtBxToplamTutar);
            Controls.Add(btnMasa);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(panelBahce);
            Controls.Add(btnYenile);
            Controls.Add(btnIptalEt);
            Controls.Add(btnGonder);
            Controls.Add(btnOnayla);
            Controls.Add(btnYazdir);
            Controls.Add(lstViewSiparis);
            Controls.Add(panelSalon);
            Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "Anasayfa";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Anasayfa";
            Load += Anasayfa_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel panelSalon;
        private ListView lstViewSiparis;
        private ColumnHeader yazdir;
        private ColumnHeader Adet;
        private ColumnHeader Ürün;
        private ColumnHeader Fiyatı;
        private ColumnHeader urun;
        private ColumnHeader kategori;
        private Button btnYazdir;
        private Button btnOnayla;
        private Button btnGonder;
        private Button btnIptalEt;
        private Button btnYenile;
        private FlowLayoutPanel panelBahce;
        private Button button1;
        private Button button2;
        private Button btnMasa;
        private TextBox txtBxToplamTutar;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private PrintPreviewDialog printPreviewDialog1;
    }
}