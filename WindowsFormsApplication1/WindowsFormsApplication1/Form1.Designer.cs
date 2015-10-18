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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.NameLabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveNewButton = new System.Windows.Forms.Button();
            this.save_button = new System.Windows.Forms.Button();
            this.comboBox10 = new System.Windows.Forms.ComboBox();
            this.comboBox9 = new System.Windows.Forms.ComboBox();
            this.comboBox8 = new System.Windows.Forms.ComboBox();
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.picturePreviewBox = new System.Windows.Forms.PictureBox();
            this.newCocktailName = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.quantity1 = new System.Windows.Forms.TextBox();
            this.quantity2 = new System.Windows.Forms.TextBox();
            this.quantity3 = new System.Windows.Forms.TextBox();
            this.quantity4 = new System.Windows.Forms.TextBox();
            this.quantity5 = new System.Windows.Forms.TextBox();
            this.quantity6 = new System.Windows.Forms.TextBox();
            this.quantity7 = new System.Windows.Forms.TextBox();
            this.quantity8 = new System.Windows.Forms.TextBox();
            this.quantity9 = new System.Windows.Forms.TextBox();
            this.quantity10 = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cocktailPhoto)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picturePreviewBox)).BeginInit();
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
            this.newCocktailToolStripMenuItem.Click += new System.EventHandler(this.newCocktailToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
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
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Location = new System.Drawing.Point(12, 27);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.quantity10);
            this.panel1.Controls.Add(this.quantity9);
            this.panel1.Controls.Add(this.quantity8);
            this.panel1.Controls.Add(this.quantity7);
            this.panel1.Controls.Add(this.quantity6);
            this.panel1.Controls.Add(this.quantity5);
            this.panel1.Controls.Add(this.quantity4);
            this.panel1.Controls.Add(this.quantity3);
            this.panel1.Controls.Add(this.quantity2);
            this.panel1.Controls.Add(this.quantity1);
            this.panel1.Controls.Add(this.browseButton);
            this.panel1.Controls.Add(this.newCocktailName);
            this.panel1.Controls.Add(this.picturePreviewBox);
            this.panel1.Controls.Add(this.NameLabel);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.saveNewButton);
            this.panel1.Controls.Add(this.save_button);
            this.panel1.Controls.Add(this.comboBox10);
            this.panel1.Controls.Add(this.comboBox9);
            this.panel1.Controls.Add(this.comboBox8);
            this.panel1.Controls.Add(this.comboBox7);
            this.panel1.Controls.Add(this.comboBox6);
            this.panel1.Controls.Add(this.comboBox5);
            this.panel1.Controls.Add(this.comboBox4);
            this.panel1.Controls.Add(this.comboBox3);
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Location = new System.Drawing.Point(2, 277);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(548, 223);
            this.panel1.TabIndex = 16;
            this.panel1.Visible = false;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.Location = new System.Drawing.Point(16, 27);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(57, 18);
            this.NameLabel.TabIndex = 22;
            this.NameLabel.Text = "Name:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(231, 183);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 18);
            this.label11.TabIndex = 21;
            this.label11.Text = "10.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(231, 156);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 18);
            this.label10.TabIndex = 20;
            this.label10.Text = "9.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(231, 129);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(22, 18);
            this.label9.TabIndex = 19;
            this.label9.Text = "8.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(231, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 18);
            this.label8.TabIndex = 18;
            this.label8.Text = "7.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(231, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 18);
            this.label7.TabIndex = 17;
            this.label7.Text = "6.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 182);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 18);
            this.label6.TabIndex = 16;
            this.label6.Text = "5.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 18);
            this.label5.TabIndex = 15;
            this.label5.Text = "4.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 18);
            this.label4.TabIndex = 14;
            this.label4.Text = "3.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 18);
            this.label3.TabIndex = 13;
            this.label3.Text = "2.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 18);
            this.label2.TabIndex = 12;
            this.label2.Text = "1.";
            // 
            // saveNewButton
            // 
            this.saveNewButton.Location = new System.Drawing.Point(470, 188);
            this.saveNewButton.Name = "saveNewButton";
            this.saveNewButton.Size = new System.Drawing.Size(75, 23);
            this.saveNewButton.TabIndex = 11;
            this.saveNewButton.Text = "Save";
            this.saveNewButton.UseVisualStyleBackColor = true;
            this.saveNewButton.Click += new System.EventHandler(this.saveNewButton_Click);
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(470, 188);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(75, 23);
            this.save_button.TabIndex = 10;
            this.save_button.Text = "Save";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // comboBox10
            // 
            this.comboBox10.FormattingEnabled = true;
            this.comboBox10.Location = new System.Drawing.Point(259, 180);
            this.comboBox10.Name = "comboBox10";
            this.comboBox10.Size = new System.Drawing.Size(121, 21);
            this.comboBox10.TabIndex = 9;
            // 
            // comboBox9
            // 
            this.comboBox9.FormattingEnabled = true;
            this.comboBox9.Location = new System.Drawing.Point(259, 153);
            this.comboBox9.Name = "comboBox9";
            this.comboBox9.Size = new System.Drawing.Size(121, 21);
            this.comboBox9.TabIndex = 8;
            // 
            // comboBox8
            // 
            this.comboBox8.FormattingEnabled = true;
            this.comboBox8.Location = new System.Drawing.Point(259, 126);
            this.comboBox8.Name = "comboBox8";
            this.comboBox8.Size = new System.Drawing.Size(121, 21);
            this.comboBox8.TabIndex = 7;
            // 
            // comboBox7
            // 
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.Location = new System.Drawing.Point(259, 99);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(121, 21);
            this.comboBox7.TabIndex = 6;
            // 
            // comboBox6
            // 
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Location = new System.Drawing.Point(259, 72);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(121, 21);
            this.comboBox6.TabIndex = 5;
            // 
            // comboBox5
            // 
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Location = new System.Drawing.Point(44, 179);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(121, 21);
            this.comboBox5.TabIndex = 4;
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Location = new System.Drawing.Point(44, 152);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(121, 21);
            this.comboBox4.TabIndex = 3;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(44, 125);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(121, 21);
            this.comboBox3.TabIndex = 2;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(44, 98);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(44, 71);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // picturePreviewBox
            // 
            this.picturePreviewBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picturePreviewBox.BackgroundImage")));
            this.picturePreviewBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picturePreviewBox.Location = new System.Drawing.Point(437, 3);
            this.picturePreviewBox.Name = "picturePreviewBox";
            this.picturePreviewBox.Size = new System.Drawing.Size(108, 107);
            this.picturePreviewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picturePreviewBox.TabIndex = 23;
            this.picturePreviewBox.TabStop = false;
            // 
            // newCocktailName
            // 
            this.newCocktailName.Location = new System.Drawing.Point(79, 27);
            this.newCocktailName.Name = "newCocktailName";
            this.newCocktailName.Size = new System.Drawing.Size(243, 20);
            this.newCocktailName.TabIndex = 24;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(455, 116);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(75, 23);
            this.browseButton.TabIndex = 25;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // quantity1
            // 
            this.quantity1.Location = new System.Drawing.Point(171, 73);
            this.quantity1.Name = "quantity1";
            this.quantity1.Size = new System.Drawing.Size(43, 20);
            this.quantity1.TabIndex = 26;
            this.quantity1.Text = "0";
            // 
            // quantity2
            // 
            this.quantity2.Location = new System.Drawing.Point(171, 100);
            this.quantity2.Name = "quantity2";
            this.quantity2.Size = new System.Drawing.Size(43, 20);
            this.quantity2.TabIndex = 27;
            this.quantity2.Text = "0";
            // 
            // quantity3
            // 
            this.quantity3.Location = new System.Drawing.Point(171, 127);
            this.quantity3.Name = "quantity3";
            this.quantity3.Size = new System.Drawing.Size(43, 20);
            this.quantity3.TabIndex = 28;
            this.quantity3.Text = "0";
            // 
            // quantity4
            // 
            this.quantity4.Location = new System.Drawing.Point(171, 154);
            this.quantity4.Name = "quantity4";
            this.quantity4.Size = new System.Drawing.Size(43, 20);
            this.quantity4.TabIndex = 29;
            this.quantity4.Text = "0";
            // 
            // quantity5
            // 
            this.quantity5.Location = new System.Drawing.Point(171, 181);
            this.quantity5.Name = "quantity5";
            this.quantity5.Size = new System.Drawing.Size(43, 20);
            this.quantity5.TabIndex = 30;
            this.quantity5.Text = "0";
            // 
            // quantity6
            // 
            this.quantity6.Location = new System.Drawing.Point(386, 73);
            this.quantity6.Name = "quantity6";
            this.quantity6.Size = new System.Drawing.Size(43, 20);
            this.quantity6.TabIndex = 31;
            this.quantity6.Text = "0";
            // 
            // quantity7
            // 
            this.quantity7.Location = new System.Drawing.Point(386, 100);
            this.quantity7.Name = "quantity7";
            this.quantity7.Size = new System.Drawing.Size(43, 20);
            this.quantity7.TabIndex = 32;
            this.quantity7.Text = "0";
            // 
            // quantity8
            // 
            this.quantity8.Location = new System.Drawing.Point(386, 128);
            this.quantity8.Name = "quantity8";
            this.quantity8.Size = new System.Drawing.Size(43, 20);
            this.quantity8.TabIndex = 33;
            this.quantity8.Text = "0";
            // 
            // quantity9
            // 
            this.quantity9.Location = new System.Drawing.Point(386, 155);
            this.quantity9.Name = "quantity9";
            this.quantity9.Size = new System.Drawing.Size(43, 20);
            this.quantity9.TabIndex = 34;
            this.quantity9.Text = "0";
            // 
            // quantity10
            // 
            this.quantity10.Location = new System.Drawing.Point(386, 182);
            this.quantity10.Name = "quantity10";
            this.quantity10.Size = new System.Drawing.Size(43, 20);
            this.quantity10.TabIndex = 35;
            this.quantity10.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(563, 512);
            this.Controls.Add(this.panel1);
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picturePreviewBox)).EndInit();
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox10;
        private System.Windows.Forms.ComboBox comboBox9;
        private System.Windows.Forms.ComboBox comboBox8;
        private System.Windows.Forms.ComboBox comboBox7;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveNewButton;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox newCocktailName;
        private System.Windows.Forms.PictureBox picturePreviewBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox quantity10;
        private System.Windows.Forms.TextBox quantity9;
        private System.Windows.Forms.TextBox quantity8;
        private System.Windows.Forms.TextBox quantity7;
        private System.Windows.Forms.TextBox quantity6;
        private System.Windows.Forms.TextBox quantity5;
        private System.Windows.Forms.TextBox quantity4;
        private System.Windows.Forms.TextBox quantity3;
        private System.Windows.Forms.TextBox quantity2;
        private System.Windows.Forms.TextBox quantity1;
    }
}

