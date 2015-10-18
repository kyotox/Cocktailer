﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string newPhotoLocation;
        Int16 calib_mililiters = 50;
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadAll(object sender, EventArgs e)
        {
            string[] available_ingredients;
            available_ingredients = new string[10];

            LoadSettings();
            // Load available ingredients from file and cocktails database
            try
            {
                available_ingredients[0] = Convert.ToString(setIng1.SelectedIndex);
                available_ingredients[1] = Convert.ToString(setIng2.SelectedIndex);
                available_ingredients[2] = Convert.ToString(setIng3.SelectedIndex);
                available_ingredients[3] = Convert.ToString(setIng4.SelectedIndex);
                available_ingredients[4] = Convert.ToString(setIng5.SelectedIndex);
                available_ingredients[5] = Convert.ToString(setIng6.SelectedIndex);
                available_ingredients[6] = Convert.ToString(setIng7.SelectedIndex);
                available_ingredients[7] = Convert.ToString(setIng8.SelectedIndex);
                available_ingredients[8] = Convert.ToString(setIng9.SelectedIndex);
                available_ingredients[9] = Convert.ToString(setIng10.SelectedIndex);


                string strProvider = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = CocktailsDB.accdb";
                string strSql = "Select * from Cocktails";

                OleDbConnection con = new OleDbConnection(strProvider);
                OleDbCommand cmd = new OleDbCommand(strSql, con);
                con.Open();
                cmd.CommandType = CommandType.Text;
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataTable cocktails = new DataTable();
                da.Fill(cocktails);
                dataGridView1.DataSource = cocktails;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            // filter the unavailable cocktails if checkbox unchecked
            if (!checkBox15.Checked)
            {

                for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                {
                    labelName.Text = Convert.ToString(dataGridView1.Rows.Count);
                    bool display = true;
                    string mix = Convert.ToString(dataGridView1.Rows[i].Cells["mix"].Value);
                    string[] mix_split = mix.Split('|');

                    foreach (string mix_ing in mix_split)
                    {
                        if (mix_ing != "" && !available_ingredients.Contains(mix_ing))
                        {
                            display = false;
                        }
                    }
                    if (!display)
                    {
                        dataGridView1.Rows.RemoveAt(i);
                    }
                }
            }

            if (dataGridView1.Rows.Count == 0)
            {
                return;
            }
            //hide unwanted columns
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["desc"].Visible = false;
            dataGridView1.Columns["img"].Visible = false;
            dataGridView1.Columns["qty"].Visible = false;
            dataGridView1.Columns["mix"].Visible = false;
            dataGridView1.Columns["Name"].Width = 237;
            dataGridView1.CurrentCell = dataGridView1[1, 0];
            dataGridView1_CellClick();

        }
        // select cocktail
        private void dataGridView1_CellClick()
        {
            string mix = Convert.ToString(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["mix"].Value);
            string qty = Convert.ToString(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["qty"].Value);

            int[] output_pump;
            output_pump = new int[10]; //here we will save the pump needed for each ingredient of the mix

            int[] output_qty;
            output_qty = new int[10];

            string[] mix_split = mix.Split('|');
            short ing_no = 0;
            
            // split the mix field in all the ingredients and check at which pump is each one
            foreach (string mix_ing in mix_split)
            {
                if (mix_ing != "")
                {
                    try
                    {
                        string[] available_ingredients;
                        available_ingredients = new string[10];
                        available_ingredients[0] = Convert.ToString(setIng1.SelectedIndex);
                        available_ingredients[1] = Convert.ToString(setIng2.SelectedIndex);
                        available_ingredients[2] = Convert.ToString(setIng3.SelectedIndex);
                        available_ingredients[3] = Convert.ToString(setIng4.SelectedIndex);
                        available_ingredients[4] = Convert.ToString(setIng5.SelectedIndex);
                        available_ingredients[5] = Convert.ToString(setIng6.SelectedIndex);
                        available_ingredients[6] = Convert.ToString(setIng7.SelectedIndex);
                        available_ingredients[7] = Convert.ToString(setIng8.SelectedIndex);
                        available_ingredients[8] = Convert.ToString(setIng9.SelectedIndex);
                        available_ingredients[9] = Convert.ToString(setIng10.SelectedIndex);

                        // check at which pump we have each ingredient
                        for (int motor_no=0; motor_no < 10;motor_no++)
                        {
                            
                                if (available_ingredients[motor_no] == mix_ing)
                                {
                                    output_pump[ing_no] = motor_no+1;
                                // +1 because in Arduino the motors are labeled from 1 to 10
                                }
                                
                        }




                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    ing_no++; //go to next ingredient in the mix
                }
                
            }


            Int16[] calib_value;
            calib_value = new Int16[10];
            calib_value[0] = Convert.ToInt16(calib1.Text);
            calib_value[1] = Convert.ToInt16(calib2.Text);
            calib_value[2] = Convert.ToInt16(calib3.Text);
            calib_value[3] = Convert.ToInt16(calib4.Text);
            calib_value[4] = Convert.ToInt16(calib5.Text);
            calib_value[5] = Convert.ToInt16(calib6.Text);
            calib_value[6] = Convert.ToInt16(calib7.Text);
            calib_value[7] = Convert.ToInt16(calib8.Text);
            calib_value[8] = Convert.ToInt16(calib9.Text);
            calib_value[9] = Convert.ToInt16(calib10.Text);

            string[] qty_split = qty.Split('|');
            ing_no = 0;

            foreach (string s_q in qty_split)
            {

                if (s_q != "")
                {
                    output_qty[ing_no-1] = Convert.ToInt16(s_q);
                }
                ing_no++;
            }

            string output_string = "";

            for (int i = 0; i < 10; i++)
            {
                if (output_pump[i] > 0)
                {
                    output_string += Convert.ToString(output_pump[i]);
                    output_string += ":";
                    output_string += Convert.ToString(output_qty[i] * calib_value[i]/calib_mililiters);
                    output_string += "|";
                }
            }

            //load ingredients lirary
            string strProvider = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = CocktailsDB.accdb";
            string strSql = "Select * from Ingredients";

            OleDbConnection con = new OleDbConnection(strProvider);
            OleDbCommand cmd = new OleDbCommand(strSql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Field1", typeof(string));
            da.Fill(dt);

            label1.Text = output_string;
            labelName.Text = Convert.ToString(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Name"].Value);
            string[] mix_split_copy;
            mix_split_copy = new string[10];

            string[] qty_split_copy;
            qty_split_copy = new string[10];

            for (int i = 1; i < 11; i++)
            {
                if ((i < mix_split.Length) && (mix_split[i] != "0"))
                {
                    mix_split_copy[i-1] = mix_split[i];
                    qty_split_copy[i-1] = " - " + qty_split[i] + "ml";

                }
                else
                {
                    mix_split_copy[i-1] = "0";
                    qty_split_copy[i-1] = "";
                }
            }

            cocktail_ing1.Text = dt.Rows[Convert.ToInt16(mix_split_copy[0])]["Field1"].ToString();
            cocktail_ing2.Text = dt.Rows[Convert.ToInt16(mix_split_copy[1])]["Field1"].ToString();
            cocktail_ing3.Text = dt.Rows[Convert.ToInt16(mix_split_copy[2])]["Field1"].ToString();
            cocktail_ing4.Text = dt.Rows[Convert.ToInt16(mix_split_copy[3])]["Field1"].ToString();
            cocktail_ing5.Text = dt.Rows[Convert.ToInt16(mix_split_copy[4])]["Field1"].ToString();
            cocktail_ing6.Text = dt.Rows[Convert.ToInt16(mix_split_copy[5])]["Field1"].ToString();
            cocktail_ing7.Text = dt.Rows[Convert.ToInt16(mix_split_copy[6])]["Field1"].ToString();
            cocktail_ing8.Text = dt.Rows[Convert.ToInt16(mix_split_copy[7])]["Field1"].ToString();
            cocktail_ing9.Text = dt.Rows[Convert.ToInt16(mix_split_copy[8])]["Field1"].ToString();
            cocktail_ing10.Text = dt.Rows[Convert.ToInt16(mix_split_copy[9])]["Field1"].ToString();


            cocktail_ing1.Text += qty_split_copy[0];
            cocktail_ing2.Text += qty_split_copy[1];
            cocktail_ing3.Text += qty_split_copy[2];
            cocktail_ing4.Text += qty_split_copy[3];
            cocktail_ing5.Text += qty_split_copy[4];
            cocktail_ing6.Text += qty_split_copy[5];
            cocktail_ing7.Text += qty_split_copy[6];
            cocktail_ing8.Text += qty_split_copy[7];
            cocktail_ing9.Text += qty_split_copy[8];
            cocktail_ing10.Text += qty_split_copy[9];
            string Photofilelocation = "./img/" + Convert.ToString(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["img"].Value) + ".jpg";
            if (File.Exists(Photofilelocation))
                cocktailPhoto.Image = Image.FromFile(Photofilelocation);
            else
                cocktailPhoto.Image = Image.FromFile("./img/nophoto.jpg");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1_CellClick();
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            LoadAll(this, EventArgs.Empty);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsPanel.Visible = true;
            AddNewPannel.Visible = false;
        }

        private void LoadSettings()
        {
            string[] lines = System.IO.File.ReadAllLines(@"available_ing.txt");

            Int32[] available_ingredients;
            Int32[] calib_values;
            available_ingredients = new Int32[10];
            calib_values = new Int32[10];

            short lineNo = 0;
            foreach (string line in lines)
            {
                if (lineNo < 10)
                    available_ingredients[lineNo] = Int32.Parse(line);
                else
                    calib_values[lineNo - 10] = Int32.Parse(line);
                lineNo++;
            }

            //load ingredients lirary
            string strProvider = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = CocktailsDB.accdb";
            string strSql = "Select * from Ingredients";

            OleDbConnection con = new OleDbConnection(strProvider);
            OleDbCommand cmd = new OleDbCommand(strSql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Field1", typeof(string));
            da.Fill(dt);

            DataTable combo1 = new DataTable();
            combo1 = dt.Copy();
            DataTable combo2 = new DataTable();
            combo2 = dt.Copy();
            DataTable combo3 = new DataTable();
            combo3 = dt.Copy();
            DataTable combo4 = new DataTable();
            combo4 = dt.Copy();
            DataTable combo5 = new DataTable();
            combo5 = dt.Copy();
            DataTable combo6 = new DataTable();
            combo6 = dt.Copy();
            DataTable combo7 = new DataTable();
            combo7 = dt.Copy();
            DataTable combo8 = new DataTable();
            combo8 = dt.Copy();
            DataTable combo9 = new DataTable();
            combo9 = dt.Copy();
            DataTable combo10 = new DataTable();
            combo10 = dt.Copy();



            setIng1.ValueMember = "ID";
            setIng1.DisplayMember = "Field1";
            setIng1.DataSource = combo1;
            setIng1.SelectedIndex = available_ingredients[0];
            calib1.Text = Convert.ToString(calib_values[0]);

            setIng2.ValueMember = "ID";
            setIng2.DisplayMember = "Field1";
            setIng2.DataSource = combo2;
            setIng2.SelectedIndex = available_ingredients[1];
            calib2.Text = Convert.ToString(calib_values[1]);

            setIng3.ValueMember = "ID";
            setIng3.DisplayMember = "Field1";
            setIng3.DataSource = combo3;
            setIng3.SelectedIndex = available_ingredients[2];
            calib3.Text = Convert.ToString(calib_values[2]);

            setIng4.ValueMember = "ID";
            setIng4.DisplayMember = "Field1";
            setIng4.DataSource = combo4;
            setIng4.SelectedIndex = available_ingredients[3];
            calib4.Text = Convert.ToString(calib_values[3]);

            setIng5.ValueMember = "ID";
            setIng5.DisplayMember = "Field1";
            setIng5.DataSource = combo5;
            setIng5.SelectedIndex = available_ingredients[4];
            calib5.Text = Convert.ToString(calib_values[4]);

            setIng6.ValueMember = "ID";
            setIng6.DisplayMember = "Field1";
            setIng6.DataSource = combo6;
            setIng6.SelectedIndex = available_ingredients[5];
            calib6.Text = Convert.ToString(calib_values[5]);

            setIng7.ValueMember = "ID";
            setIng7.DisplayMember = "Field1";
            setIng7.DataSource = combo7;
            setIng7.SelectedIndex = available_ingredients[6];
            calib7.Text = Convert.ToString(calib_values[6]);

            setIng8.ValueMember = "ID";
            setIng8.DisplayMember = "Field1";
            setIng8.DataSource = combo8;
            setIng8.SelectedIndex = available_ingredients[7];
            calib8.Text = Convert.ToString(calib_values[7]);

            setIng9.ValueMember = "ID";
            setIng9.DisplayMember = "Field1";
            setIng9.DataSource = combo9;
            setIng9.SelectedIndex = available_ingredients[8];
            calib9.Text = Convert.ToString(calib_values[8]);

            setIng10.ValueMember = "ID";
            setIng10.DisplayMember = "Field1";
            setIng10.DataSource = combo10;
            setIng10.SelectedIndex = available_ingredients[9];
            calib10.Text = Convert.ToString(calib_values[9]);

        }
        private void save_button_Click(object sender, EventArgs e)
        {
            settingsPanel.Visible = false;
            Int32[] new_ingredients;
            new_ingredients = new Int32[10];
            new_ingredients[0] = setIng1.SelectedIndex;
            new_ingredients[1] = setIng2.SelectedIndex;
            new_ingredients[2] = setIng3.SelectedIndex;
            new_ingredients[3] = setIng4.SelectedIndex;
            new_ingredients[4] = setIng5.SelectedIndex;
            new_ingredients[5] = setIng6.SelectedIndex;
            new_ingredients[6] = setIng7.SelectedIndex;
            new_ingredients[7] = setIng8.SelectedIndex;
            new_ingredients[8] = setIng9.SelectedIndex;
            new_ingredients[9] = setIng10.SelectedIndex;


            Int16[] calib_value;
            calib_value = new Int16[10];
            calib_value[0] = Convert.ToInt16(calib1.Text);
            calib_value[1] = Convert.ToInt16(calib2.Text);
            calib_value[2] = Convert.ToInt16(calib3.Text);
            calib_value[3] = Convert.ToInt16(calib4.Text);
            calib_value[4] = Convert.ToInt16(calib5.Text);
            calib_value[5] = Convert.ToInt16(calib6.Text);
            calib_value[6] = Convert.ToInt16(calib7.Text);
            calib_value[7] = Convert.ToInt16(calib8.Text);
            calib_value[8] = Convert.ToInt16(calib9.Text);
            calib_value[9] = Convert.ToInt16(calib10.Text);

            bool duplicate = false;

            for (int i = 0; i < 10; i++)
            {
                for (int j = i + 1; j < 10; j++)
                {
                    if ((new_ingredients[i] == new_ingredients[j]) && (new_ingredients[i] != 0))
                        duplicate = true;
                }

            }
            if (!duplicate)
            {
                try
                {
                    System.IO.File.WriteAllLines(@"available_ing.txt", new_ingredients.Select(x => x.ToString()).ToArray());
                    System.IO.File.AppendAllLines(@"available_ing.txt", calib_value.Select(x => x.ToString()).ToArray());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                LoadAll(this, EventArgs.Empty);
                AddNewPannel.Visible = false;
            }
            else
            {
                MessageBox.Show("Duplicates were found. \nPlease make sure that you don't have the same ingredient in two slots");
            }

        }

        private void saveNewButton_Click(object sender, EventArgs e)
        {
            if (newCocktailName.Text == "")
            {
                MessageBox.Show("Fill up the name of your cocktail");
                return;
            }
            AddNewPannel.Visible = false;
            Int32[] new_ingredients;
            new_ingredients = new Int32[10];
            new_ingredients[0] = ing1.SelectedIndex;
            new_ingredients[1] = ing2.SelectedIndex;
            new_ingredients[2] = ing3.SelectedIndex;
            new_ingredients[3] = ing4.SelectedIndex;
            new_ingredients[4] = ing5.SelectedIndex;
            new_ingredients[5] = ing6.SelectedIndex;
            new_ingredients[6] = ing7.SelectedIndex;
            new_ingredients[7] = ing8.SelectedIndex;
            new_ingredients[8] = ing9.SelectedIndex;
            new_ingredients[9] = ing10.SelectedIndex;
            bool duplicate = false;


            Int32[] new_quantity;
            new_quantity = new Int32[10];
            new_quantity[0] = Convert.ToInt32(quantity1.Text);
            new_quantity[1] = Convert.ToInt32(quantity2.Text);
            new_quantity[2] = Convert.ToInt32(quantity3.Text);
            new_quantity[3] = Convert.ToInt32(quantity4.Text);
            new_quantity[4] = Convert.ToInt32(quantity5.Text);
            new_quantity[5] = Convert.ToInt32(quantity6.Text);
            new_quantity[6] = Convert.ToInt32(quantity7.Text);
            new_quantity[7] = Convert.ToInt32(quantity8.Text);
            new_quantity[8] = Convert.ToInt32(quantity9.Text);
            new_quantity[9] = Convert.ToInt32(quantity10.Text);

            for (int i = 0; i < 10; i++)
            {
                for (int j = i + 1; j < 10; j++)
                {
                    if ((new_ingredients[i] == new_ingredients[j]) && (new_ingredients[i] != 0))
                        duplicate = true;
                }

            }
            if (!duplicate)
            {

                string connetionString = null;
                OleDbConnection connection;
                OleDbDataAdapter oledbAdapter = new OleDbDataAdapter();
                string sql = null;
                connetionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = CocktailsDB.accdb;";
                connection = new OleDbConnection(connetionString);
                string mix = "|";
                string qty = "|";
                for (int i=0; i<10;i++)
                {
                    mix += new_ingredients[i];
                    mix += "|";
                    qty += new_quantity[i];
                    qty += "|";
                }
                sql = "insert into Cocktails (name, [desc], img, mix, qty) VALUES('" + newCocktailName.Text + "', 'abc', '" + newCocktailName.Text.Replace(" ", "").ToLower() + "', '" + mix + "', '" + qty + "')";
                try
                {
                    connection.Open();
                    oledbAdapter.InsertCommand = new OleDbCommand(sql, connection);
                    oledbAdapter.InsertCommand.ExecuteNonQuery();
                    MessageBox.Show("Row(s) Inserted !! ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }


                LoadAll(this, EventArgs.Empty);
                AddNewPannel.Visible = false;
                if (newPhotoLocation != null)
                {
                    string photoFileName = newCocktailName.Text.Replace(" ", "").ToLower();
                    System.IO.File.Copy(newPhotoLocation, "./img/" + photoFileName + ".jpg");
                }
            }
            else
            {
                MessageBox.Show("Duplicates were found. \nPlease make sure that you don't have the same ingredient in two slots");
            }
        }

        private void AddNew_Load()
        {
            save_button.Visible = false;
            saveNewButton.Visible = true;
            string[] lines = System.IO.File.ReadAllLines(@"available_ing.txt");

            Int32[] available_ingredients;
            available_ingredients = new Int32[10];
            
            for (int ingNo = 0; ingNo < 10; ingNo++)
            {
                
                // Use a tab to indent each line of the file.
                available_ingredients[ingNo] = Int32.Parse(lines[ingNo]);

                ingNo++;
            }

            //load ingredients lirary
            string strProvider = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = CocktailsDB.accdb";
            string strSql = "Select * from Ingredients";

            OleDbConnection con = new OleDbConnection(strProvider);
            OleDbCommand cmd = new OleDbCommand(strSql, con);
            con.Open();
            cmd.CommandType = CommandType.Text;
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Field1", typeof(string));
            da.Fill(dt);

            DataTable combo1 = new DataTable();
            combo1 = dt.Copy();
            DataTable combo2 = new DataTable();
            combo2 = dt.Copy();
            DataTable combo3 = new DataTable();
            combo3 = dt.Copy();
            DataTable combo4 = new DataTable();
            combo4 = dt.Copy();
            DataTable combo5 = new DataTable();
            combo5 = dt.Copy();
            DataTable combo6 = new DataTable();
            combo6 = dt.Copy();
            DataTable combo7 = new DataTable();
            combo7 = dt.Copy();
            DataTable combo8 = new DataTable();
            combo8 = dt.Copy();
            DataTable combo9 = new DataTable();
            combo9 = dt.Copy();
            DataTable combo10 = new DataTable();
            combo10 = dt.Copy();



            ing1.ValueMember = "ID";
            ing1.DisplayMember = "Field1";
            ing1.DataSource = combo1;
            ing1.SelectedIndex = 0;

            ing2.ValueMember = "ID";
            ing2.DisplayMember = "Field1";
            ing2.DataSource = combo2;
            ing2.SelectedIndex = 0;

            ing3.ValueMember = "ID";
            ing3.DisplayMember = "Field1";
            ing3.DataSource = combo3;
            ing3.SelectedIndex = 0;

            ing4.ValueMember = "ID";
            ing4.DisplayMember = "Field1";
            ing4.DataSource = combo4;
            ing4.SelectedIndex = 0;

            ing5.ValueMember = "ID";
            ing5.DisplayMember = "Field1";
            ing5.DataSource = combo5;
            ing5.SelectedIndex = 0;

            ing6.ValueMember = "ID";
            ing6.DisplayMember = "Field1";
            ing6.DataSource = combo6;
            ing6.SelectedIndex =0;

            ing7.ValueMember = "ID";
            ing7.DisplayMember = "Field1";
            ing7.DataSource = combo7;
            ing7.SelectedIndex = 0;

            ing8.ValueMember = "ID";
            ing8.DisplayMember = "Field1";
            ing8.DataSource = combo8;
            ing8.SelectedIndex = 0;

            ing9.ValueMember = "ID";
            ing9.DisplayMember = "Field1";
            ing9.DataSource = combo9;
            ing9.SelectedIndex = 0;

            ing10.ValueMember = "ID";
            ing10.DisplayMember = "Field1";
            ing10.DataSource = combo10;
            ing10.SelectedIndex = 0;

        }

        private void newCocktailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewPannel.Visible = true;
            settingsPanel.Visible = false;

            AddNew_Load();
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            LoadFile();
        }

        private void LoadFile()
        {
            
            OpenFileDialog ofd = new OpenFileDialog();
            string PictureFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            ofd.InitialDirectory = PictureFolder;
            ofd.Filter = "Pictures|*.jpg;*.bmp;*.png";
            System.Windows.Forms.DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                newPhotoLocation = ofd.FileName;
                picturePreviewBox.Image = Image.FromFile(newPhotoLocation);
            }
        }

        private void add_cancel_Click(object sender, EventArgs e)
        {
            AddNewPannel.Visible = false;
        }

        private void settingsCancel_Click(object sender, EventArgs e)
        {
            settingsPanel.Visible = false;
        }
        
    }
}