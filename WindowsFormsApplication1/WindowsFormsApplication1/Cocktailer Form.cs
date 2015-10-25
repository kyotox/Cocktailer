using System;
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

            var ingredients_combobox = settingsPanel.Controls
                         .OfType<ComboBox>()
                         .Where(txt => txt.Name.ToLower().StartsWith("seting"));
            int no = 0;
            foreach (ComboBox txt in ingredients_combobox)
            {
                available_ingredients[no] = Convert.ToString(txt.Text);
                no++;
            }

            LoadSettings();         // Load available ingredients from file and cocktails database

            try
            {

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
            if (!showAll.Checked)
            {

                for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                {
                    labelName.Text = Convert.ToString(dataGridView1.Rows.Count);
                    bool toRemove = false;

                    string mix = Convert.ToString(dataGridView1.Rows[i].Cells["mix"].Value);
                    // mix contains the string from the database containing all ingredients
                    // ing separated by "|"

                    string[] mix_split = mix.Split('|');
                    // mix_split contains all the ingredient IDs as an array

                    foreach (string mix_ing in mix_split)
                    {
                        // check if all the ingredients from the cocktail are available
                        // mix_ing should not be empty
                        // if ingredient not found, toRemove turns true
                        if (mix_ing != "" && !available_ingredients.Contains(mix_ing))
                        {
                            toRemove = true;
                        }
                    }

                    if (!toRemove)
                    {
                        // the row is removed from view only, not from database
                        dataGridView1.Rows.RemoveAt(i);
                    }

                }
            }

            //stop if no cocktail available
            if (dataGridView1.Rows.Count == 0)
            {
                return;
                //Error / suggestion should be added here 
            }

            //hide unwanted columns
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["desc"].Visible = false;
            dataGridView1.Columns["img"].Visible = false;
            dataGridView1.Columns["qty"].Visible = false;
            dataGridView1.Columns["mix"].Visible = false;
            dataGridView1.Columns["Name"].Width = 237;
            dataGridView1.CurrentCell = dataGridView1[1, 0];

            // display the first cocktail in the table
            selectCocktail();

        }
        // select cocktail
        private void selectCocktail()
        {
            // Get the data from DataGrid
            string mix = Convert.ToString(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["mix"].Value);
            string qty = Convert.ToString(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["qty"].Value);

            // assign pump for each ingredient
            int[] output_pump;
            output_pump = new int[10]; //here we will save the pump needed for each ingredient of the mix

            // assign qty for each pump
            int[] output_qty;
            output_qty = new int[10];

            // split mix string
            string[] mix_split = mix.Split('|');
            short ing_no = 0;

            // split the mix field in all the ingredients and check at which pump is each one
            foreach (string mix_ing in mix_split)
            {
                if (mix_ing != "")
                {
                    string[] available_ingredients;
                    available_ingredients = new string[10];
                    var ingredients_combobox = settingsPanel.Controls
                                    .OfType<ComboBox>()
                                    .Where(txt => txt.Name.ToLower().StartsWith("seting"));
                    // get the available ingredients from the ComboBoxes and put them into an array
                    int no = 0;
                    foreach (ComboBox txt in ingredients_combobox)
                    {
                        available_ingredients[no] = Convert.ToString(txt.SelectedIndex);
                        no++;
                    }

                    // check at which pump we have each ingredient
                    for (int motor_no = 0; motor_no < 10; motor_no++)
                    {

                        if (available_ingredients[motor_no] == mix_ing)
                        {
                            output_pump[ing_no] = motor_no + 1;
                        }

                    }

                    ing_no++; //go to next ingredient in the mix
                }

            }

            // initiate calibration array
            Int16[] calib_value;
            calib_value = new Int16[10];

            var calibration_textboxes = settingsPanel.Controls
                            .OfType<TextBox>()
                            .Where(txt => txt.Name.ToLower().StartsWith("calib"));

            // get the calibration value for each pump and put them into an array
            int textBoxNo = 0;
            foreach (TextBox txt in calibration_textboxes)
            {
                calib_value[textBoxNo] = Convert.ToInt16(txt.Text);
                textBoxNo++;
            }

            // split the quantity needed for each ingredient
            string[] qty_split = qty.Split('|');
            ing_no = 0;

            foreach (string quantity in qty_split)
            {

                if (quantity != "") // quantity can't ever be empty, but let's check
                {
                    output_qty[ing_no] = Convert.ToInt16(quantity);
                }
                ing_no++; // go further
            }

            string output_string = "";

            // create the output string
            for (int i = 0; i < 10; i++)
            {
                if (output_pump[i] > 0 && output_qty[i] > 0)
                {
                    output_string += Convert.ToString(output_pump[i]);
                    output_string += ":";
                    output_string += Convert.ToString(output_qty[i] * calib_mililiters / calib_value[i]);
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
            con.Close();
            // populate data table
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Field1", typeof(string));
            da.Fill(dt);

            // display the output
            output.Text = output_string;

            // display cocktail content and photo
            labelName.Text = Convert.ToString(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["Name"].Value);

            var ingredient_label = cocktailDisplay.Controls
                .OfType<Label>()
                .Where(txt => txt.Name.ToLower().StartsWith("cocktail_ing"));

            // cast the IEnumerable into an array
            Label[] ingredient = ingredient_label.Cast<Label>().ToArray();
           
            // Put "" in all the labels to be clean

            for (int i = 0; i < 10; i++)
            {
                ingredient[i].Text = "";
            }

            int label_number = 0;

            // going through all the ingredients and display them without leaving spaces
            // in case the cocktail created had spaces like ing1, ing2, ing8 and ing10

            for (int i = 0; i < 10; i++)
            {
                string ingredient_name = dt.Rows[Convert.ToInt16(mix_split[i])]["Field1"].ToString();

                if (ingredient_name != "")
                {
                    ingredient[label_number].Text = ingredient_name;
                    ingredient[label_number].Text += " - " + qty_split[i] + " ml";
                    label_number++;
                }
            }

            // display photo of the cocktail
            string Photofilelocation = "./img/" + Convert.ToString(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["img"].Value) + ".jpg";

            // if no photo found, "NOPHOTO" displayed
            if (File.Exists(Photofilelocation))
                cocktailPhoto.Image = Image.FromFile(Photofilelocation);
            else
                cocktailPhoto.Image = Image.FromFile("./img/nophoto.jpg");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectCocktail();
        }

        private void ShowAll_CheckedChanged(object sender, EventArgs e)
        {
            //reload all
            LoadAll(this, EventArgs.Empty);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // display Settings Panel, hide AddNew
            settingsPanel.Visible = true;
            AddNewPannel.Visible = false;
        }

        private void LoadSettings()
        {
            // read the document
            string[] lines = System.IO.File.ReadAllLines(@"available_ing.txt");

            int[] available_ingredients;
            int[] calib_values;
            available_ingredients = new int[10];
            calib_values = new int[10];

            short lineNo = 0;
            // first 10 rows are ingredients IDs and the next 10, calibration values
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

            DataTable[] combo;
            combo = new DataTable [10];

            // populate combo boxes and select the needed ingredient
            // each combo box needs another data table

            var combo_boxes = settingsPanel.Controls
                           .OfType<ComboBox>()
                           .Where(txt => txt.Name.ToLower().StartsWith("seting"));
            
            int ing = 0;
            foreach (ComboBox txt in combo_boxes)
            {
                combo[ing] = dt.Copy();

                txt.ValueMember = "ID";
                txt.DisplayMember = "Field1";
                txt.DataSource = combo[ing];
                txt.SelectedIndex = available_ingredients[ing];
                ing++;
            }

            // populate the calibration textboxes
            var calib_Textboxes = settingsPanel.Controls
                           .OfType<TextBox>()
                           .Where(txt => txt.Name.ToLower().StartsWith("calib"));
            
            ing = 0;
            foreach (TextBox txt in calib_Textboxes)
            {
                txt.Text = Convert.ToString(calib_values[ing]);
                ing++;
            }
            

        }

        private void save_button_Click(object sender, EventArgs e)
        {
            // close seetings panel 
            settingsPanel.Visible = false;
            Int32[] new_ingredients;
            new_ingredients = new Int32[10];

            // get information about the selected ingredient and calibration for each pump
            var combo_boxes = settingsPanel.Controls
                           .OfType<ComboBox>()
                           .Where(txt => txt.Name.ToLower().StartsWith("seting"));
            
            int pump = 0;
            foreach (ComboBox txt in combo_boxes)
            {
                new_ingredients[pump] = txt.SelectedIndex;
                pump++;
            }

            var calib_textboxes = settingsPanel.Controls
                           .OfType<TextBox>()
                           .Where(txt => txt.Name.ToLower().StartsWith("seting"));
            
            pump = 0;
            Int16[] calib_value;
            calib_value = new Int16[10];
            foreach (TextBox txt in calib_textboxes)
            {
                calib_value[pump] = Convert.ToInt16(txt.Text);
                pump++;
            }
            
            // start assuming we don't have a duplicate
            bool duplicate = false;

            for (int i = 0; i < 10; i++)
            {
                for (int j = i + 1; j < 10; j++)
                {
                    if ((new_ingredients[i] == new_ingredients[j]) && (new_ingredients[i] != 0))
                        // verify all the ingredients for duplicates
                        duplicate = true;
                }

            }

            // give error if duplicates
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
                // Reload
                LoadAll(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Duplicates were found. \nPlease make sure that you don't have the same ingredient in two slots");
            }

        }

        private void saveNewButton_Click(object sender, EventArgs e)
        {
            // needs a name first
            if (newCocktailName.Text == "")
            {
                MessageBox.Show("Fill up the name of your cocktail");
                return;
            }

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
                string mix = "";
                string qty = "";
                for (int i = 0; i < 10; i++)
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

                    // hide the add new panel
                    AddNewPannel.Visible = false;
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
            ing6.SelectedIndex = 0;

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

        private void delCocktail_Click(object sender, EventArgs e)
        {
            string connetionString = null;
            OleDbConnection connection;
            OleDbDataAdapter oledbAdapter = new OleDbDataAdapter();
            string sql = null;
            connetionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = CocktailsDB.accdb;";
            connection = new OleDbConnection(connetionString);

            sql = "DELETE from Cocktails WHERE ID =" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["ID"].Value;


            connection.Open();
            oledbAdapter.InsertCommand = new OleDbCommand(sql, connection);
            oledbAdapter.InsertCommand.ExecuteNonQuery();
            connection.Close();
            LoadAll(this, EventArgs.Empty);

        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help f2 = new Help();
            f2.ShowDialog(); // Shows Help
        }

    }
}
