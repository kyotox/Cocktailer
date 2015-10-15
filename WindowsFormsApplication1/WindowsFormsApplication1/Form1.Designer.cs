namespace WindowsFormsApplication1
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newCocktailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.checkBox15 = new System.Windows.Forms.CheckBox();
            this.labelName = new System.Windows.Forms.Label();
            this.cocktailPhoto = new System.Windows.Forms.PictureBox();
            this.cocktail_ing1 = new System.Windows.Forms.Label();
            this.cocktail_ing2 = new System.Windows.Forms.Label();
            this.cocktail_ing3 = new System.Windows.Forms.Label();
            this.cocktail_ing4 = new System.Windows.Forms.Label();
            this.cocktail_ing5 = new System.Windows.Forms.Label();
            this.cocktail_ing10 = new System.Windows.Forms.Label();
            this.cocktail_ing9 = new System.Windows.Forms.Label();
            this.cocktail_ing8 = new System.Windows.Forms.Label();
            this.cocktail_ing7 = new System.Windows.Forms.Label();
            this.cocktail_ing6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cocktailPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCocktailToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(563, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newCocktailToolStripMenuItem
            // 
            this.newCocktailToolStripMenuItem.Name = "newCocktailToolStripMenuItem";
            this.newCocktailToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.newCocktailToolStripMenuItem.Text = "New cocktail";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.quitToolStripMenuItem.Text = "Quit";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(240, 222);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // checkBox15
            // 
            this.checkBox15.AutoSize = true;
            this.checkBox15.Location = new System.Drawing.Point(0, 250);
            this.checkBox15.Name = "checkBox15";
            this.checkBox15.Size = new System.Drawing.Size(67, 17);
            this.checkBox15.TabIndex = 2;
            this.checkBox15.Text = "Show All";
            this.checkBox15.UseVisualStyleBackColor = true;
            this.checkBox15.CheckedChanged += new System.EventHandler(this.checkBox15_CheckedChanged);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(312, 27);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(35, 13);
            this.labelName.TabIndex = 3;
            this.labelName.Text = "label1";
            // 
            // cocktailPhoto
            // 
            this.cocktailPhoto.Location = new System.Drawing.Point(410, 39);
            this.cocktailPhoto.Name = "cocktailPhoto";
            this.cocktailPhoto.Size = new System.Drawing.Size(141, 213);
            this.cocktailPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.cocktailPhoto.TabIndex = 4;
            this.cocktailPhoto.TabStop = false;
            // 
            // cocktail_ing1
            // 
            this.cocktail_ing1.AutoSize = true;
            this.cocktail_ing1.Location = new System.Drawing.Point(274, 67);
            this.cocktail_ing1.Name = "cocktail_ing1";
            this.cocktail_ing1.Size = new System.Drawing.Size(35, 13);
            this.cocktail_ing1.TabIndex = 5;
            this.cocktail_ing1.Text = "label1";
            // 
            // cocktail_ing2
            // 
            this.cocktail_ing2.AutoSize = true;
            this.cocktail_ing2.Location = new System.Drawing.Point(274, 82);
            this.cocktail_ing2.Name = "cocktail_ing2";
            this.cocktail_ing2.Size = new System.Drawing.Size(35, 13);
            this.cocktail_ing2.TabIndex = 6;
            this.cocktail_ing2.Text = "label1";
            // 
            // cocktail_ing3
            // 
            this.cocktail_ing3.AutoSize = true;
            this.cocktail_ing3.Location = new System.Drawing.Point(274, 97);
            this.cocktail_ing3.Name = "cocktail_ing3";
            this.cocktail_ing3.Size = new System.Drawing.Size(35, 13);
            this.cocktail_ing3.TabIndex = 7;
            this.cocktail_ing3.Text = "label2";
            // 
            // cocktail_ing4
            // 
            this.cocktail_ing4.AutoSize = true;
            this.cocktail_ing4.Location = new System.Drawing.Point(274, 112);
            this.cocktail_ing4.Name = "cocktail_ing4";
            this.cocktail_ing4.Size = new System.Drawing.Size(35, 13);
            this.cocktail_ing4.TabIndex = 8;
            this.cocktail_ing4.Text = "label3";
            // 
            // cocktail_ing5
            // 
            this.cocktail_ing5.AutoSize = true;
            this.cocktail_ing5.Location = new System.Drawing.Point(274, 127);
            this.cocktail_ing5.Name = "cocktail_ing5";
            this.cocktail_ing5.Size = new System.Drawing.Size(35, 13);
            this.cocktail_ing5.TabIndex = 9;
            this.cocktail_ing5.Text = "label4";
            // 
            // cocktail_ing10
            // 
            this.cocktail_ing10.AutoSize = true;
            this.cocktail_ing10.Location = new System.Drawing.Point(274, 201);
            this.cocktail_ing10.Name = "cocktail_ing10";
            this.cocktail_ing10.Size = new System.Drawing.Size(35, 13);
            this.cocktail_ing10.TabIndex = 14;
            this.cocktail_ing10.Text = "label5";
            // 
            // cocktail_ing9
            // 
            this.cocktail_ing9.AutoSize = true;
            this.cocktail_ing9.Location = new System.Drawing.Point(274, 186);
            this.cocktail_ing9.Name = "cocktail_ing9";
            this.cocktail_ing9.Size = new System.Drawing.Size(35, 13);
            this.cocktail_ing9.TabIndex = 13;
            this.cocktail_ing9.Text = "label6";
            // 
            // cocktail_ing8
            // 
            this.cocktail_ing8.AutoSize = true;
            this.cocktail_ing8.Location = new System.Drawing.Point(274, 171);
            this.cocktail_ing8.Name = "cocktail_ing8";
            this.cocktail_ing8.Size = new System.Drawing.Size(35, 13);
            this.cocktail_ing8.TabIndex = 12;
            this.cocktail_ing8.Text = "label7";
            // 
            // cocktail_ing7
            // 
            this.cocktail_ing7.AutoSize = true;
            this.cocktail_ing7.Location = new System.Drawing.Point(274, 156);
            this.cocktail_ing7.Name = "cocktail_ing7";
            this.cocktail_ing7.Size = new System.Drawing.Size(35, 13);
            this.cocktail_ing7.TabIndex = 11;
            this.cocktail_ing7.Text = "label8";
            // 
            // cocktail_ing6
            // 
            this.cocktail_ing6.AutoSize = true;
            this.cocktail_ing6.Location = new System.Drawing.Point(274, 141);
            this.cocktail_ing6.Name = "cocktail_ing6";
            this.cocktail_ing6.Size = new System.Drawing.Size(35, 13);
            this.cocktail_ing6.TabIndex = 10;
            this.cocktail_ing6.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(258, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 264);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cocktail_ing10);
            this.Controls.Add(this.cocktail_ing9);
            this.Controls.Add(this.cocktail_ing8);
            this.Controls.Add(this.cocktail_ing7);
            this.Controls.Add(this.cocktail_ing6);
            this.Controls.Add(this.cocktail_ing5);
            this.Controls.Add(this.cocktail_ing4);
            this.Controls.Add(this.cocktail_ing3);
            this.Controls.Add(this.cocktail_ing2);
            this.Controls.Add(this.cocktail_ing1);
            this.Controls.Add(this.cocktailPhoto);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.checkBox15);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.LoadAll);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cocktailPhoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newCocktailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox checkBox15;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.PictureBox cocktailPhoto;
        private System.Windows.Forms.Label cocktail_ing1;
        private System.Windows.Forms.Label cocktail_ing2;
        private System.Windows.Forms.Label cocktail_ing3;
        private System.Windows.Forms.Label cocktail_ing4;
        private System.Windows.Forms.Label cocktail_ing5;
        private System.Windows.Forms.Label cocktail_ing10;
        private System.Windows.Forms.Label cocktail_ing9;
        private System.Windows.Forms.Label cocktail_ing8;
        private System.Windows.Forms.Label cocktail_ing7;
        private System.Windows.Forms.Label cocktail_ing6;
        private System.Windows.Forms.Label label1;
    }
}

