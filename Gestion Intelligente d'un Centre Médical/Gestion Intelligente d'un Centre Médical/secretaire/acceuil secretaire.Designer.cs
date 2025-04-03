namespace Gestion_Intelligente_d_un_Centre_Médical
{
    partial class acceuil_secretaire
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(acceuil_secretaire));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_payer = new System.Windows.Forms.Button();
            this.nbr_fact = new System.Windows.Forms.Label();
            this.total = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.photo = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_dossier = new System.Windows.Forms.Button();
            this.btn_rendez = new System.Windows.Forms.Button();
            this.cin = new System.Windows.Forms.Label();
            this.nom = new System.Windows.Forms.Label();
            this.prenom = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.photo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(611, 11);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(429, 331);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Cin Patient";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Nom Patient";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Prenom Patient";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 125;
            // 
            // btn_payer
            // 
            this.btn_payer.Location = new System.Drawing.Point(196, 98);
            this.btn_payer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_payer.Name = "btn_payer";
            this.btn_payer.Size = new System.Drawing.Size(137, 26);
            this.btn_payer.TabIndex = 1;
            this.btn_payer.Text = "payer les factures";
            this.btn_payer.UseVisualStyleBackColor = true;
            this.btn_payer.Click += new System.EventHandler(this.button1_Click);
            // 
            // nbr_fact
            // 
            this.nbr_fact.AutoSize = true;
            this.nbr_fact.Location = new System.Drawing.Point(4, 26);
            this.nbr_fact.Name = "nbr_fact";
            this.nbr_fact.Size = new System.Drawing.Size(142, 16);
            this.nbr_fact.TabIndex = 2;
            this.nbr_fact.Text = "Nombre des factures =";
            // 
            // total
            // 
            this.total.AutoSize = true;
            this.total.Location = new System.Drawing.Point(4, 59);
            this.total.Name = "total";
            this.total.Size = new System.Drawing.Size(108, 16);
            this.total.TabIndex = 3;
            this.total.Text = "Total montants = ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.total);
            this.groupBox1.Controls.Add(this.nbr_fact);
            this.groupBox1.Controls.Add(this.btn_payer);
            this.groupBox1.Location = new System.Drawing.Point(24, 207);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(347, 135);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Factures à payer";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // photo
            // 
            this.photo.Location = new System.Drawing.Point(24, 31);
            this.photo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.photo.Name = "photo";
            this.photo.Size = new System.Drawing.Size(180, 151);
            this.photo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.photo.TabIndex = 5;
            this.photo.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 11);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Photo de patient";
            // 
            // btn_dossier
            // 
            this.btn_dossier.Location = new System.Drawing.Point(412, 288);
            this.btn_dossier.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_dossier.Name = "btn_dossier";
            this.btn_dossier.Size = new System.Drawing.Size(129, 54);
            this.btn_dossier.TabIndex = 7;
            this.btn_dossier.Text = "gerer le dossier adminstratif";
            this.btn_dossier.UseVisualStyleBackColor = true;
            // 
            // btn_rendez
            // 
            this.btn_rendez.Location = new System.Drawing.Point(412, 207);
            this.btn_rendez.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_rendez.Name = "btn_rendez";
            this.btn_rendez.Size = new System.Drawing.Size(129, 54);
            this.btn_rendez.TabIndex = 8;
            this.btn_rendez.Text = "gerer les rendez vous";
            this.btn_rendez.UseVisualStyleBackColor = true;
            this.btn_rendez.Click += new System.EventHandler(this.btn_rendez_Click);
            // 
            // cin
            // 
            this.cin.AutoSize = true;
            this.cin.Location = new System.Drawing.Point(227, 39);
            this.cin.Name = "cin";
            this.cin.Size = new System.Drawing.Size(35, 16);
            this.cin.TabIndex = 10;
            this.cin.Text = "CIN :";
            // 
            // nom
            // 
            this.nom.AutoSize = true;
            this.nom.Location = new System.Drawing.Point(227, 94);
            this.nom.Name = "nom";
            this.nom.Size = new System.Drawing.Size(42, 16);
            this.nom.TabIndex = 10;
            this.nom.Text = "Nom :";
            // 
            // prenom
            // 
            this.prenom.AutoSize = true;
            this.prenom.Location = new System.Drawing.Point(227, 146);
            this.prenom.Name = "prenom";
            this.prenom.Size = new System.Drawing.Size(60, 16);
            this.prenom.TabIndex = 10;
            this.prenom.Text = "Prenom :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(566, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 29);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(515, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "Filter";
            // 
            // acceuil_secretaire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 370);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.prenom);
            this.Controls.Add(this.nom);
            this.Controls.Add(this.cin);
            this.Controls.Add(this.btn_rendez);
            this.Controls.Add(this.btn_dossier);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.photo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "acceuil_secretaire";
            this.Text = "acceuil_secretaire";
            this.Load += new System.EventHandler(this.acceuil_secretaire_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.photo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_payer;
        private System.Windows.Forms.Label nbr_fact;
        private System.Windows.Forms.Label total;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox photo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_dossier;
        private System.Windows.Forms.Button btn_rendez;
        private System.Windows.Forms.Label cin;
        private System.Windows.Forms.Label nom;
        private System.Windows.Forms.Label prenom;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}