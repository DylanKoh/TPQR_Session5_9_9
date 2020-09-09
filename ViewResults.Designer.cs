namespace TPQR_Session5_9_9
{
    partial class ViewResults
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cbSkill = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTotalSession = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCompletedSessions = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbGold1 = new System.Windows.Forms.PictureBox();
            this.pbGold2 = new System.Windows.Forms.PictureBox();
            this.btnGold = new System.Windows.Forms.Button();
            this.pbSilver1 = new System.Windows.Forms.PictureBox();
            this.pbSilver2 = new System.Windows.Forms.PictureBox();
            this.btnSilver = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnBronze = new System.Windows.Forms.Button();
            this.pbBronze2 = new System.Windows.Forms.PictureBox();
            this.pbBronze1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGold1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGold2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSilver1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSilver2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbBronze2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBronze1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1011, 86);
            this.panel1.TabIndex = 2;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 26);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(83, 38);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 14F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(775, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(231, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "ASEAN Skills 2020";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Maroon;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 702);
            this.panel2.Margin = new System.Windows.Forms.Padding(5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1011, 55);
            this.panel2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 14F);
            this.label2.Location = new System.Drawing.Point(428, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 29);
            this.label2.TabIndex = 4;
            this.label2.Text = "View Results";
            // 
            // cbSkill
            // 
            this.cbSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSkill.FormattingEnabled = true;
            this.cbSkill.Location = new System.Drawing.Point(363, 136);
            this.cbSkill.Name = "cbSkill";
            this.cbSkill.Size = new System.Drawing.Size(360, 33);
            this.cbSkill.TabIndex = 10;
            this.cbSkill.SelectedIndexChanged += new System.EventHandler(this.cbSkill_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(285, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Skill: ";
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
            this.dataGridView1.Location = new System.Drawing.Point(12, 208);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(555, 489);
            this.dataGridView1.TabIndex = 11;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Competitor";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Country";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Total Marks";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 125;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 25);
            this.label4.TabIndex = 12;
            this.label4.Text = "Total No of Sessions: ";
            // 
            // lblTotalSession
            // 
            this.lblTotalSession.AutoSize = true;
            this.lblTotalSession.Location = new System.Drawing.Point(248, 180);
            this.lblTotalSession.Name = "lblTotalSession";
            this.lblTotalSession.Size = new System.Drawing.Size(25, 25);
            this.lblTotalSession.TabIndex = 13;
            this.lblTotalSession.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(522, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(289, 25);
            this.label5.TabIndex = 14;
            this.label5.Text = "No of Completed Sessions: ";
            // 
            // lblCompletedSessions
            // 
            this.lblCompletedSessions.AutoSize = true;
            this.lblCompletedSessions.Location = new System.Drawing.Point(817, 180);
            this.lblCompletedSessions.Name = "lblCompletedSessions";
            this.lblCompletedSessions.Size = new System.Drawing.Size(25, 25);
            this.lblCompletedSessions.TabIndex = 15;
            this.lblCompletedSessions.Text = "0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGold);
            this.groupBox1.Controls.Add(this.pbGold2);
            this.groupBox1.Controls.Add(this.pbGold1);
            this.groupBox1.Location = new System.Drawing.Point(573, 208);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 159);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Gold";
            // 
            // pbGold1
            // 
            this.pbGold1.Location = new System.Drawing.Point(78, 31);
            this.pbGold1.Name = "pbGold1";
            this.pbGold1.Size = new System.Drawing.Size(100, 50);
            this.pbGold1.TabIndex = 0;
            this.pbGold1.TabStop = false;
            // 
            // pbGold2
            // 
            this.pbGold2.Location = new System.Drawing.Point(271, 31);
            this.pbGold2.Name = "pbGold2";
            this.pbGold2.Size = new System.Drawing.Size(100, 50);
            this.pbGold2.TabIndex = 1;
            this.pbGold2.TabStop = false;
            // 
            // btnGold
            // 
            this.btnGold.Location = new System.Drawing.Point(154, 102);
            this.btnGold.Name = "btnGold";
            this.btnGold.Size = new System.Drawing.Size(141, 40);
            this.btnGold.TabIndex = 2;
            this.btnGold.Text = "More Gold";
            this.btnGold.UseVisualStyleBackColor = true;
            this.btnGold.Visible = false;
            // 
            // pbSilver1
            // 
            this.pbSilver1.Location = new System.Drawing.Point(78, 31);
            this.pbSilver1.Name = "pbSilver1";
            this.pbSilver1.Size = new System.Drawing.Size(100, 50);
            this.pbSilver1.TabIndex = 0;
            this.pbSilver1.TabStop = false;
            // 
            // pbSilver2
            // 
            this.pbSilver2.Location = new System.Drawing.Point(271, 31);
            this.pbSilver2.Name = "pbSilver2";
            this.pbSilver2.Size = new System.Drawing.Size(100, 50);
            this.pbSilver2.TabIndex = 1;
            this.pbSilver2.TabStop = false;
            // 
            // btnSilver
            // 
            this.btnSilver.Location = new System.Drawing.Point(154, 102);
            this.btnSilver.Name = "btnSilver";
            this.btnSilver.Size = new System.Drawing.Size(141, 40);
            this.btnSilver.TabIndex = 2;
            this.btnSilver.Text = "More Silver";
            this.btnSilver.UseVisualStyleBackColor = true;
            this.btnSilver.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSilver);
            this.groupBox2.Controls.Add(this.pbSilver2);
            this.groupBox2.Controls.Add(this.pbSilver1);
            this.groupBox2.Location = new System.Drawing.Point(573, 373);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(426, 159);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Silver";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnBronze);
            this.groupBox3.Controls.Add(this.pbBronze2);
            this.groupBox3.Controls.Add(this.pbBronze1);
            this.groupBox3.Location = new System.Drawing.Point(573, 538);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(426, 159);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bronze";
            // 
            // btnBronze
            // 
            this.btnBronze.Location = new System.Drawing.Point(144, 113);
            this.btnBronze.Name = "btnBronze";
            this.btnBronze.Size = new System.Drawing.Size(162, 40);
            this.btnBronze.TabIndex = 2;
            this.btnBronze.Text = "More Bronze";
            this.btnBronze.UseVisualStyleBackColor = true;
            this.btnBronze.Visible = false;
            // 
            // pbBronze2
            // 
            this.pbBronze2.Location = new System.Drawing.Point(271, 31);
            this.pbBronze2.Name = "pbBronze2";
            this.pbBronze2.Size = new System.Drawing.Size(100, 50);
            this.pbBronze2.TabIndex = 1;
            this.pbBronze2.TabStop = false;
            // 
            // pbBronze1
            // 
            this.pbBronze1.Location = new System.Drawing.Point(78, 31);
            this.pbBronze1.Name = "pbBronze1";
            this.pbBronze1.Size = new System.Drawing.Size(100, 50);
            this.pbBronze1.TabIndex = 0;
            this.pbBronze1.TabStop = false;
            // 
            // ViewResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 757);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCompletedSessions);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblTotalSession);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cbSkill);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "ViewResults";
            this.Text = "View Results";
            this.Load += new System.EventHandler(this.ViewResults_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbGold1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGold2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSilver1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSilver2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbBronze2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBronze1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbSkill;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTotalSession;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCompletedSessions;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGold;
        private System.Windows.Forms.PictureBox pbGold2;
        private System.Windows.Forms.PictureBox pbGold1;
        private System.Windows.Forms.PictureBox pbSilver1;
        private System.Windows.Forms.PictureBox pbSilver2;
        private System.Windows.Forms.Button btnSilver;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnBronze;
        private System.Windows.Forms.PictureBox pbBronze2;
        private System.Windows.Forms.PictureBox pbBronze1;
    }
}