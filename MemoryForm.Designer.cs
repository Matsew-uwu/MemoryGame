namespace Memory
{
    partial class MemoryForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MemoryForm));
            this.btn_Distribuer = new System.Windows.Forms.Button();
            this.ilSabotDeCartes = new System.Windows.Forms.ImageList(this.components);
            this.btn_Retourner = new System.Windows.Forms.Button();
            this.btn_Jouer = new System.Windows.Forms.Button();
            this.pb_Recherche = new System.Windows.Forms.PictureBox();
            this.pb_04 = new System.Windows.Forms.PictureBox();
            this.pb_03 = new System.Windows.Forms.PictureBox();
            this.pb_02 = new System.Windows.Forms.PictureBox();
            this.pb_01 = new System.Windows.Forms.PictureBox();
            this.tlpTapisDeCartes = new System.Windows.Forms.TableLayoutPanel();
            this.pb_06 = new System.Windows.Forms.PictureBox();
            this.pb_07 = new System.Windows.Forms.PictureBox();
            this.pb_08 = new System.Windows.Forms.PictureBox();
            this.pb_05 = new System.Windows.Forms.PictureBox();
            this.btn_facile = new System.Windows.Forms.Button();
            this.btn_normal = new System.Windows.Forms.Button();
            this.btn_mortel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Recherche)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_04)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_03)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_02)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_01)).BeginInit();
            this.tlpTapisDeCartes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_06)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_07)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_08)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_05)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Distribuer
            // 
            this.btn_Distribuer.Location = new System.Drawing.Point(671, 34);
            this.btn_Distribuer.Name = "btn_Distribuer";
            this.btn_Distribuer.Size = new System.Drawing.Size(85, 23);
            this.btn_Distribuer.TabIndex = 1;
            this.btn_Distribuer.Text = "DISTRIBUER";
            this.btn_Distribuer.UseVisualStyleBackColor = true;
            this.btn_Distribuer.Click += new System.EventHandler(this.Btn_Distribuer_Click);
            // 
            // ilSabotDeCartes
            // 
            this.ilSabotDeCartes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSabotDeCartes.ImageStream")));
            this.ilSabotDeCartes.TransparentColor = System.Drawing.Color.Transparent;
            this.ilSabotDeCartes.Images.SetKeyName(0, "DosCarte.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(1, "Dialogue.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(2, "Diplome.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(3, "FemmeAffaire.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(4, "FemmeBatiment.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(5, "FemmeBlanche.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(6, "FemmeBleue.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(7, "FemmeCasque.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(8, "FemmeCuisinier.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(9, "FemmeInformaticienBrune.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(10, "FemmeInformaticienChatain.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(11, "FemmeJournal.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(12, "FemmeMedecin.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(13, "FemmeMegaphone.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(14, "FemmeOrange.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(15, "FemmePhotographe.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(16, "FemmePoliciere.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(17, "HommeAffaire.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(18, "HommeBatiment.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(19, "HommeBleu.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(20, "HommeCasque.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(21, "HommeCuisinier.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(22, "HommeGarde.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(23, "HommeInformaticienBrun.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(24, "HommeInformaticienChatain.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(25, "HommeJournal.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(26, "HommeMarron.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(27, "HommeMedecin.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(28, "HommeMegaphone.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(29, "HommeOrange.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(30, "HommePhotographe.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(31, "HommePolicier.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(32, "Joker.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(33, "LivreurPizza.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(34, "MarinMousse.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(35, "MarinOfficier.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(36, "MarinPompon.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(37, "Mecanicien.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(38, "Pilote.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(39, "Pompier.jpg");
            this.ilSabotDeCartes.Images.SetKeyName(40, "serveur.jpg");
            // 
            // btn_Retourner
            // 
            this.btn_Retourner.Location = new System.Drawing.Point(671, 66);
            this.btn_Retourner.Name = "btn_Retourner";
            this.btn_Retourner.Size = new System.Drawing.Size(87, 23);
            this.btn_Retourner.TabIndex = 3;
            this.btn_Retourner.Text = "RETOURNER";
            this.btn_Retourner.UseVisualStyleBackColor = true;
            this.btn_Retourner.Click += new System.EventHandler(this.btn_Retourner_Click);
            // 
            // btn_Jouer
            // 
            this.btn_Jouer.Location = new System.Drawing.Point(287, 319);
            this.btn_Jouer.Name = "btn_Jouer";
            this.btn_Jouer.Size = new System.Drawing.Size(75, 23);
            this.btn_Jouer.TabIndex = 4;
            this.btn_Jouer.Text = "JOUER";
            this.btn_Jouer.UseVisualStyleBackColor = true;
            this.btn_Jouer.Click += new System.EventHandler(this.Btn_Jouer_Click);
            // 
            // pb_Recherche
            // 
            this.pb_Recherche.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pb_Recherche.Location = new System.Drawing.Point(623, 264);
            this.pb_Recherche.Name = "pb_Recherche";
            this.pb_Recherche.Size = new System.Drawing.Size(100, 100);
            this.pb_Recherche.TabIndex = 5;
            this.pb_Recherche.TabStop = false;
            // 
            // pb_04
            // 
            this.pb_04.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pb_04.Location = new System.Drawing.Point(333, 3);
            this.pb_04.Name = "pb_04";
            this.pb_04.Size = new System.Drawing.Size(100, 100);
            this.pb_04.TabIndex = 0;
            this.pb_04.TabStop = false;
            this.pb_04.Click += new System.EventHandler(this.Pb_04_Click);
            // 
            // pb_03
            // 
            this.pb_03.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pb_03.Location = new System.Drawing.Point(223, 3);
            this.pb_03.Name = "pb_03";
            this.pb_03.Size = new System.Drawing.Size(100, 100);
            this.pb_03.TabIndex = 0;
            this.pb_03.TabStop = false;
            this.pb_03.Click += new System.EventHandler(this.Pb_03_Click);
            // 
            // pb_02
            // 
            this.pb_02.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pb_02.Location = new System.Drawing.Point(113, 3);
            this.pb_02.Name = "pb_02";
            this.pb_02.Size = new System.Drawing.Size(100, 100);
            this.pb_02.TabIndex = 0;
            this.pb_02.TabStop = false;
            this.pb_02.Click += new System.EventHandler(this.Pb_02_Click);
            // 
            // pb_01
            // 
            this.pb_01.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pb_01.Location = new System.Drawing.Point(3, 3);
            this.pb_01.Name = "pb_01";
            this.pb_01.Size = new System.Drawing.Size(100, 100);
            this.pb_01.TabIndex = 0;
            this.pb_01.TabStop = false;
            this.pb_01.Click += new System.EventHandler(this.Pb_01_Click);
            // 
            // tlpTapisDeCartes
            // 
            this.tlpTapisDeCartes.ColumnCount = 4;
            this.tlpTapisDeCartes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTapisDeCartes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTapisDeCartes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTapisDeCartes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTapisDeCartes.Controls.Add(this.pb_01, 0, 0);
            this.tlpTapisDeCartes.Controls.Add(this.pb_02, 1, 0);
            this.tlpTapisDeCartes.Controls.Add(this.pb_03, 2, 0);
            this.tlpTapisDeCartes.Controls.Add(this.pb_04, 3, 0);
            this.tlpTapisDeCartes.Controls.Add(this.pb_06, 1, 1);
            this.tlpTapisDeCartes.Controls.Add(this.pb_07, 2, 1);
            this.tlpTapisDeCartes.Controls.Add(this.pb_08, 3, 1);
            this.tlpTapisDeCartes.Controls.Add(this.pb_05, 0, 1);
            this.tlpTapisDeCartes.Location = new System.Drawing.Point(64, 63);
            this.tlpTapisDeCartes.Name = "tlpTapisDeCartes";
            this.tlpTapisDeCartes.RowCount = 2;
            this.tlpTapisDeCartes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTapisDeCartes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tlpTapisDeCartes.Size = new System.Drawing.Size(440, 220);
            this.tlpTapisDeCartes.TabIndex = 0;
            // 
            // pb_06
            // 
            this.pb_06.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pb_06.Location = new System.Drawing.Point(113, 111);
            this.pb_06.Name = "pb_06";
            this.pb_06.Size = new System.Drawing.Size(100, 100);
            this.pb_06.TabIndex = 2;
            this.pb_06.TabStop = false;
            this.pb_06.Click += new System.EventHandler(this.pb_06_Click);
            // 
            // pb_07
            // 
            this.pb_07.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pb_07.Location = new System.Drawing.Point(223, 111);
            this.pb_07.Name = "pb_07";
            this.pb_07.Size = new System.Drawing.Size(100, 100);
            this.pb_07.TabIndex = 3;
            this.pb_07.TabStop = false;
            this.pb_07.Click += new System.EventHandler(this.pb_07_Click);
            // 
            // pb_08
            // 
            this.pb_08.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pb_08.Location = new System.Drawing.Point(333, 111);
            this.pb_08.Name = "pb_08";
            this.pb_08.Size = new System.Drawing.Size(100, 100);
            this.pb_08.TabIndex = 4;
            this.pb_08.TabStop = false;
            this.pb_08.Click += new System.EventHandler(this.pb_08_Click);
            // 
            // pb_05
            // 
            this.pb_05.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pb_05.Location = new System.Drawing.Point(3, 111);
            this.pb_05.Name = "pb_05";
            this.pb_05.Size = new System.Drawing.Size(100, 100);
            this.pb_05.TabIndex = 1;
            this.pb_05.TabStop = false;
            this.pb_05.Click += new System.EventHandler(this.pb_05_Click);
            // 
            // btn_facile
            // 
            this.btn_facile.Location = new System.Drawing.Point(671, 143);
            this.btn_facile.Name = "btn_facile";
            this.btn_facile.Size = new System.Drawing.Size(75, 23);
            this.btn_facile.TabIndex = 6;
            this.btn_facile.Text = "Facile";
            this.btn_facile.UseVisualStyleBackColor = true;
            this.btn_facile.Click += new System.EventHandler(this.btn_facile_Click);
            // 
            // btn_normal
            // 
            this.btn_normal.Location = new System.Drawing.Point(671, 172);
            this.btn_normal.Name = "btn_normal";
            this.btn_normal.Size = new System.Drawing.Size(75, 23);
            this.btn_normal.TabIndex = 7;
            this.btn_normal.Text = "Normal";
            this.btn_normal.UseVisualStyleBackColor = true;
            this.btn_normal.Click += new System.EventHandler(this.btn_normal_Click);
            // 
            // btn_mortel
            // 
            this.btn_mortel.Location = new System.Drawing.Point(671, 203);
            this.btn_mortel.Name = "btn_mortel";
            this.btn_mortel.Size = new System.Drawing.Size(75, 23);
            this.btn_mortel.TabIndex = 8;
            this.btn_mortel.Text = "Mortel";
            this.btn_mortel.UseVisualStyleBackColor = true;
            this.btn_mortel.Click += new System.EventHandler(this.btn_mortel_Click);
            // 
            // MemoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_mortel);
            this.Controls.Add(this.btn_normal);
            this.Controls.Add(this.btn_facile);
            this.Controls.Add(this.pb_Recherche);
            this.Controls.Add(this.btn_Jouer);
            this.Controls.Add(this.btn_Retourner);
            this.Controls.Add(this.btn_Distribuer);
            this.Controls.Add(this.tlpTapisDeCartes);
            this.Name = "MemoryForm";
            this.Text = "Memory";
            ((System.ComponentModel.ISupportInitialize)(this.pb_Recherche)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_04)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_03)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_02)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_01)).EndInit();
            this.tlpTapisDeCartes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_06)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_07)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_08)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_05)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_Distribuer;
        private System.Windows.Forms.ImageList ilSabotDeCartes;
        private System.Windows.Forms.Button btn_Retourner;
        private System.Windows.Forms.Button btn_Jouer;
        private System.Windows.Forms.PictureBox pb_Recherche;
        private System.Windows.Forms.PictureBox pb_04;
        private System.Windows.Forms.PictureBox pb_03;
        private System.Windows.Forms.PictureBox pb_02;
        private System.Windows.Forms.PictureBox pb_01;
        private System.Windows.Forms.TableLayoutPanel tlpTapisDeCartes;
        private System.Windows.Forms.PictureBox pb_05;
        private System.Windows.Forms.PictureBox pb_06;
        private System.Windows.Forms.PictureBox pb_07;
        private System.Windows.Forms.PictureBox pb_08;
        private System.Windows.Forms.Button btn_facile;
        private System.Windows.Forms.Button btn_normal;
        private System.Windows.Forms.Button btn_mortel;
    }
}

