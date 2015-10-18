using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string newPhotoLocation;

        public Form1()
        {
            InitializeComponent();
        }

        private void LoadAll(object sender, EventArgs e)
        {
            string[] available_ingredients;
            available_ingredients = new string[10];
            
            // Load available ingredients from file and cocktails database
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"available_ing.txt");


                short ingNo = 0;
                foreach (string line in lines)
                {
                    // Use a tab to indent each line of the file.
                    available_ingredients[ingNo] = line;

                    ingNo++;
                }

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

                    foreach (string m_s in mix_split)
                    {
                        if (m_s != "" && !available_ingredients.Contains(m_s))
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
            short[] output_pump;
            output_pump = new short[10];

            int[] output_qty;
            output_qty = new int[10];

            string[] mix_split = mix.Split('|');
            short ing_no = 0;
            foreach (string s_m in mix_split)
            {
                if (s_m != "")
                {
                    try
                    {
                        string[] lines = System.IO.File.ReadAllLines(@"available_ing.txt");
                        short motor_no = 1;
                        foreach (string line in lines)
                        {
                            if (line == s_m)
                            {
                                output_pump[ing_no-1] = motor_no;
                            }

                            motor_no++;
                        }



                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                ing_no++;
            }

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
                    output_string += Convert.ToString(output_qty[i]);
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
                    qty_split_copy[i-1] = " - " + qty_split[i];

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

            cocktailPhoto.Image = Image.FromFile("./img/" + Convert.ToString(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["img"].Value) + ".jpg");
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
                panel1.Visible = true;
                saveNewButton.Visible = false;
                save_button.Visible = true;
                string[] lines = System.IO.File.ReadAllLines(@"available_ing.txt");

                Int32[] available_ingredients;
                available_ingredients = new Int32[10];

                short ingNo = 0;
                foreach (string line in lines)
                {
                    // Use a tab to indent each line of the file.
                    available_ingredients[ingNo] = Int32.Parse(line);

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



                comboBox1.ValueMember = "ID";
                comboBox1.DisplayMember = "Field1";
                comboBox1.DataSource = combo1;
                comboBox1.SelectedIndex = available_ingredients[0];

                comboBox2.ValueMember = "ID";
                comboBox2.DisplayMember = "Field1";
                comboBox2.DataSource = combo2;
                comboBox2.SelectedIndex = available_ingredients[1];

                comboBox3.ValueMember = "ID";
                comboBox3.DisplayMember = "Field1";
                comboBox3.DataSource = combo3;
                comboBox3.SelectedIndex = available_ingredients[2];

                comboBox4.ValueMember = "ID";
                comboBox4.DisplayMember = "Field1";
                comboBox4.DataSource = combo4;
                comboBox4.SelectedIndex = available_ingredients[3];

                comboBox5.ValueMember = "ID";
                comboBox5.DisplayMember = "Field1";
                comboBox5.DataSource = combo5;
                comboBox5.SelectedIndex = available_ingredients[4];

                comboBox6.ValueMember = "ID";
                comboBox6.DisplayMember = "Field1";
                comboBox6.DataSource = combo6;
                comboBox6.SelectedIndex = available_ingredients[5];

                comboBox7.ValueMember = "ID";
                comboBox7.DisplayMember = "Field1";
                comboBox7.DataSource = combo6;
                comboBox7.SelectedIndex = available_ingredients[6];

                comboBox8.ValueMember = "ID";
                comboBox8.DisplayMember = "Field1";
                comboBox8.DataSource = combo8;
                comboBox8.SelectedIndex = available_ingredients[7];

                comboBox9.ValueMember = "ID";
                comboBox9.DisplayMember = "Field1";
                comboBox9.DataSource = combo9;
                comboBox9.SelectedIndex = available_ingredients[8];

                comboBox10.ValueMember = "ID";
                comboBox10.DisplayMember = "Field1";
                comboBox10.DataSource = combo10;
                comboBox10.SelectedIndex = available_ingredients[9];



            }

        private void save_button_Click(object sender, EventArgs e)
        {

            Int32[] new_ingredients;
            new_ingredients = new Int32[10];
            new_ingredients[0] = comboBox1.SelectedIndex;
            new_ingredients[1] = comboBox2.SelectedIndex;
            new_ingredients[2] = comboBox3.SelectedIndex;
            new_ingredients[3] = comboBox4.SelectedIndex;
            new_ingredients[4] = comboBox5.SelectedIndex;
            new_ingredients[5] = comboBox6.SelectedIndex;
            new_ingredients[6] = comboBox7.SelectedIndex;
            new_ingredients[7] = comboBox8.SelectedIndex;
            new_ingredients[8] = comboBox9.SelectedIndex;
            new_ingredients[9] = comboBox10.SelectedIndex;
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
                    string[] new_ing_string = new_ingredients.Select(x => x.ToString()).ToArray();
                    System.IO.File.WriteAllLines(@"available_ing.txt", new_ing_string);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                LoadAll(this, EventArgs.Empty);
                panel1.Visible = false;
            }
            else
            {
                MessageBox.Show("Duplicates were found. \nPlease make sure that you don't have the same ingredient in two slots");
            }

        }

        private void saveNewButton_Click(object sender, EventArgs e)
        {

            Int32[] new_ingredients;
            new_ingredients = new Int32[10];
            new_ingredients[0] = comboBox1.SelectedIndex;
            new_ingredients[1] = comboBox2.SelectedIndex;
            new_ingredients[2] = comboBox3.SelectedIndex;
            new_ingredients[3] = comboBox4.SelectedIndex;
            new_ingredients[4] = comboBox5.SelectedIndex;
            new_ingredients[5] = comboBox6.SelectedIndex;
            new_ingredients[6] = comboBox7.SelectedIndex;
            new_ingredients[7] = comboBox8.SelectedIndex;
            new_ingredients[8] = comboBox9.SelectedIndex;
            new_ingredients[9] = comboBox10.SelectedIndex;
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
                panel1.Visible = false;
                string photoFileName = newCocktailName.Text.Replace(" ", "").ToLower();
                System.IO.File.Copy(newPhotoLocation, "./img/" + photoFileName + ".jpg");
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

            short ingNo = 0;
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                available_ingredients[ingNo] = Int32.Parse(line);

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



            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "Field1";
            comboBox1.DataSource = combo1;
            comboBox1.SelectedIndex = available_ingredients[0];

            comboBox2.ValueMember = "ID";
            comboBox2.DisplayMember = "Field1";
            comboBox2.DataSource = combo2;
            comboBox2.SelectedIndex = available_ingredients[1];

            comboBox3.ValueMember = "ID";
            comboBox3.DisplayMember = "Field1";
            comboBox3.DataSource = combo3;
            comboBox3.SelectedIndex = available_ingredients[2];

            comboBox4.ValueMember = "ID";
            comboBox4.DisplayMember = "Field1";
            comboBox4.DataSource = combo4;
            comboBox4.SelectedIndex = available_ingredients[3];

            comboBox5.ValueMember = "ID";
            comboBox5.DisplayMember = "Field1";
            comboBox5.DataSource = combo5;
            comboBox5.SelectedIndex = available_ingredients[4];

            comboBox6.ValueMember = "ID";
            comboBox6.DisplayMember = "Field1";
            comboBox6.DataSource = combo6;
            comboBox6.SelectedIndex = available_ingredients[5];

            comboBox7.ValueMember = "ID";
            comboBox7.DisplayMember = "Field1";
            comboBox7.DataSource = combo6;
            comboBox7.SelectedIndex = available_ingredients[6];

            comboBox8.ValueMember = "ID";
            comboBox8.DisplayMember = "Field1";
            comboBox8.DataSource = combo8;
            comboBox8.SelectedIndex = available_ingredients[7];

            comboBox9.ValueMember = "ID";
            comboBox9.DisplayMember = "Field1";
            comboBox9.DataSource = combo9;
            comboBox9.SelectedIndex = available_ingredients[8];

            comboBox10.ValueMember = "ID";
            comboBox10.DisplayMember = "Field1";
            comboBox10.DataSource = combo10;
            comboBox10.SelectedIndex = available_ingredients[9];

        }

        private void newCocktailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
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
    }
}
