using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        string newPhotoLocation;
        Int16 calib_mililiters = 50;
        int connect_retry = 0;
        string[,] updateDownloadList;
        int downcount_target = 0;
        int downcount_now = 0;
        bool updateandclose = false;

        public Form1()
        {
            InitializeComponent();
            updateDownloadList = new string[500, 2];
        }

        private void LoadAll(object sender, EventArgs e)
        {
            SetCollors();

            SerialPortInit();

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
                con.Close();

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

        private void SerialPortInit()
        {
            //serialPort1.Open();
            string[] ports = SerialPort.GetPortNames();
            serialPortsAvailable.Items.Clear();

            foreach (string port in ports)
            {
                serialPortsAvailable.Items.Add(port);
            }
            if (ports.Length > 0)
                serialPortsAvailable.SelectedIndex = 0;
            else
                comStatus.Text = "No COMPORT detected";
        }

        private void SetCollors()
        {
            var buttons = Controls
                        .OfType<Button>();
            foreach (Button txt in buttons)
            {
                txt.ForeColor = Color.Black;
            }

            buttons = settingsPanel.Controls
                         .OfType<Button>()
                         .Where(txt => txt.Name.StartsWith(""));
            foreach (Button txt in buttons)
            {
                txt.ForeColor = Color.Black;
            }
            buttons = AddNewPannel.Controls
                         .OfType<Button>()
                         .Where(txt => txt.Name.StartsWith(""));
            foreach (Button txt in buttons)
            {
                txt.ForeColor = Color.Black;
            }

            buttons = cocktailDisplay.Controls
                         .OfType<Button>()
                         .Where(txt => txt.Name.StartsWith(""));
            foreach (Button txt in buttons)
            {
                txt.ForeColor = Color.Black;
            }


            dataGridView1.ForeColor = Color.Black;


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
                if (calib_value[i] == 0)
                    calib_value[i] = 50;
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


            int getWidth = cocktailPhoto.Image.Width;
            int getHeight = cocktailPhoto.Image.Height;
            int maxH = 179;
            int maxW = 141;

            var ratioX = (double)maxW / getWidth;
            var ratioY = (double)maxH / getHeight;
            var ratio = Math.Max(ratioX, ratioY);

            cocktailPhoto.Width = (int)(getWidth * ratio);
            cocktailPhoto.Height = (int)(getHeight * ratio);
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
            con.Close();
            DataTable[] combo;
            combo = new DataTable[10];

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
                           .Where(txt => txt.Name.ToLower().StartsWith("calib"));

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
                // close seetings panel 
                settingsPanel.Visible = false;
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

            // make an array with all the ingredients selected from combo boxes
            Int32[] new_ingredients;
            new_ingredients = new Int32[10];

            var combo_boxes = AddNewPannel.Controls
                          .OfType<ComboBox>()
                          .Where(txt => txt.Name.ToLower().StartsWith("ing"));

            int ing = 0;
            foreach (ComboBox txt in combo_boxes)
            {
                new_ingredients[ing] = txt.SelectedIndex;
                ing++;
            }


            // make an array with all the quantities written in the text boxes
            Int32[] new_quantity;
            new_quantity = new Int32[10];
            var quantity_boxes = AddNewPannel.Controls
                          .OfType<TextBox>()
                          .Where(txt => txt.Name.ToLower().StartsWith("quantity"));

            ing = 0;
            foreach (TextBox txt in quantity_boxes)
            {
                new_quantity[ing] = Convert.ToInt32(txt.Text);
                ing++;
            }

            // let's check for duplicates now
            bool duplicate = false;

            for (int i = 0; i < 10; i++)
            {
                for (int j = i + 1; j < 10; j++)
                {
                    if ((new_ingredients[i] == new_ingredients[j]) && (new_ingredients[i] != 0))
                        duplicate = true;
                }

            }

            // if no duplicate found
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

                // format both ingredients (IDs) and quantities as xx|xx|xx and yy|yy|yy
                // we keep also the zeros, for easier handleing

                for (int i = 0; i < 10; i++)
                {
                    mix += new_ingredients[i];
                    mix += "|";
                    qty += new_quantity[i];
                    qty += "|";
                }

                // if any photo selected, copy it to local folder 
                // using the name of the cocktail without spaces and lowercases
                string photoFileName = "";
                if (newPhotoLocation != null)
                {
                    photoFileName = newCocktailName.Text.Replace(" ", "").ToLower();
                    photoFileName = Regex.Replace(photoFileName, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
                    System.IO.File.Copy(newPhotoLocation, "./img/" + photoFileName + ".jpg");
                }

                // insert into DB

                sql = "insert into Cocktails (name, [desc], img, mix, qty) VALUES('" + newCocktailName.Text + "', 'abc', '" + photoFileName + "', '" + mix + "', '" + qty + "')";
                try
                {
                    connection.Open();
                    oledbAdapter.InsertCommand = new OleDbCommand(sql, connection);
                    oledbAdapter.InsertCommand.ExecuteNonQuery();
                    MessageBox.Show("Cocktail added! ");

                    // hide the add new panel
                    AddNewPannel.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                // reload to display the new cocktail
                LoadAll(this, EventArgs.Empty);

            }
            else
            {
                MessageBox.Show("Duplicates were found. \nPlease make sure that you don't have the same ingredient in two slots");
            }
        }

        // load the addNew panel
        private void AddNew_Load()
        {
            string[] lines = System.IO.File.ReadAllLines(@"available_ing.txt");

            Int32[] available_ingredients;
            available_ingredients = new Int32[10];

            // load the file into an array
            for (int ingNo = 0; ingNo < 10; ingNo++)
            {
                available_ingredients[ingNo] = Int32.Parse(lines[ingNo]);
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
            combo = new DataTable[10];

            // populate combo boxes and select the null ingredient
            // each combo box needs another data table

            var combo_boxes = AddNewPannel.Controls
                           .OfType<ComboBox>()
                           .Where(txt => txt.Name.ToLower().StartsWith("ing"));

            int ing = 0;
            foreach (ComboBox txt in combo_boxes)
            {
                combo[ing] = dt.Copy();

                txt.ValueMember = "ID";
                txt.DisplayMember = "Field1";
                txt.DataSource = combo[ing];
                txt.SelectedIndex = 0;
                ing++;


            }

        }

        // show the Add Cocktail panel
        private void newCocktailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewPannel.Visible = true;
            AddNew_Load();
            settingsPanel.Visible = false;

        }

        // let the user select a picture
        private void browseButton_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            string PictureFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ofd.InitialDirectory = PictureFolder;
            ofd.Filter = "Pictures|*.jpg;*.bmp;*.png";
            System.Windows.Forms.DialogResult dr = ofd.ShowDialog();

            // now save the picture and show thumbnail
            if (dr == DialogResult.OK)
            {
                newPhotoLocation = ofd.FileName;
                picturePreviewBox.Image = Image.FromFile(newPhotoLocation);
            }

        }

        //cancel button add new
        private void add_cancel_Click(object sender, EventArgs e)
        {
            AddNewPannel.Visible = false;
        }

        //cancel button settings panel
        private void settingsCancel_Click(object sender, EventArgs e)
        {
            settingsPanel.Visible = false;
        }

        //delete cocktail button
        private void delCocktail_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the selected cocktail?", "Delete", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
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


        }

        // quit
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //help
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help f2 = new Help();
            f2.ShowDialog(); // Shows Help
        }

        // handle keyboard for shortcuts
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                AddNewPannel.Visible = false;
                settingsPanel.Visible = false;
            }
            if ((e.KeyCode == Keys.Enter) && AddNewPannel.Visible)
                saveNewButton_Click(this, EventArgs.Empty);

            if ((e.KeyCode == Keys.Enter) && settingsPanel.Visible)
                save_button_Click(this, EventArgs.Empty);
        }

        //start
        private void Start_button_Click(object sender, EventArgs e)
        {
            ComHistory.AppendText(output.Text + "\r\n");
            //serialPort1.Write(output.Text);
        }
        // calibration buttons
        private void calib_button1_Click(object sender, EventArgs e)
        {
            ComHistory.AppendText("1:50|2:0|3:0|4:0|5:0|6:0|7:0|8:0|9:0|10:0|\r\n");
        }

        private void calib_button2_Click(object sender, EventArgs e)
        {

            ComHistory.AppendText("1:0|2:50|3:0|4:0|5:0|6:0|7:0|8:0|9:0|10:0|\r\n");
        }

        private void calib_button3_Click(object sender, EventArgs e)
        {
            ComHistory.AppendText("1:0|2:0|3:50|4:0|5:0|6:0|7:0|8:0|9:0|10:0|\r\n");
        }

        private void calib_button4_Click(object sender, EventArgs e)
        {
            ComHistory.AppendText("1:0|2:0|3:0|4:50|5:0|6:0|7:0|8:0|9:0|10:0|\r\n");
        }

        private void calib_button5_Click(object sender, EventArgs e)
        {
            ComHistory.AppendText("1:0|2:0|3:0|4:0|5:50|6:0|7:0|8:0|9:0|10:0|\r\n");
        }

        private void calib_button6_Click(object sender, EventArgs e)
        {
            ComHistory.AppendText("1:0|2:0|3:0|4:0|5:0|6:50|7:0|8:0|9:0|10:0|\r\n");
        }

        private void calib_button7_Click(object sender, EventArgs e)
        {
            ComHistory.AppendText("1:0|2:0|3:0|4:0|5:0|6:0|7:50|8:0|9:0|10:0|\r\n");
        }

        private void calib_button8_Click(object sender, EventArgs e)
        {
            ComHistory.AppendText("1:0|2:0|3:0|4:0|5:0|6:0|7:0|8:50|9:0|10:0|\r\n");
        }

        private void calib_button9_Click(object sender, EventArgs e)
        {
            ComHistory.AppendText("1:0|2:0|3:0|4:0|5:0|6:0|7:0|8:0|9:50|10:0|\r\n");
        }

        private void calib_button10_Click(object sender, EventArgs e)
        {
            ComHistory.AppendText("1:0|2:0|3:0|4:0|5:0|6:0|7:0|8:0|9:0|10:50|\r\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SerialPortInit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = Convert.ToString(serialPortsAvailable.SelectedItem);
            connectTimer.Enabled = true;

        }

        private void connectTimer_Tick(object sender, EventArgs e)
        {
            if (connect_retry < 5)
            {
                try
                {
                    serialPort1.Open();
                }
                catch
                { }
                if (serialPort1.IsOpen)
                {
                    comStatus.Text = "Connected";
                    connectTimer.Enabled = false;

                }
                else
                {
                    comStatus.Text = "Connecting (" + Convert.ToString(connect_retry) + ")";
                    connect_retry++;
                }
            }
            else
            {
                connect_retry = 0;
                connectTimer.Enabled = false;
                comStatus.Text = "Connection error";
            }
        }

        private void update_Click(object sender, EventArgs e)
        {
            if (downcount_target == downcount_now)
            {
                update.Text = "Stop Update";
                string filelocation = "ftp://dragoschiotoroiu.ro/CocktailerDB/";
                string user = "dragos";
                string pass = "parola210593";
                DownloadFileWeb("http://dragoschiotoroiu.ro/CocktailerDB/CocktailsDB.accdb", AppDomain.CurrentDomain.BaseDirectory + "CocktailsDB.accdb");
                cocktailPhoto.Image = Image.FromFile("./img/nophoto.jpg");
                foreach (string file in GetFileList(filelocation + "img/", user, pass))
                {
                    if (file.EndsWith("jpg"))
                    {
                        DownloadFileWeb("http://dragoschiotoroiu.ro/CocktailerDB/img/" + file, AppDomain.CurrentDomain.BaseDirectory + "/img/" + file);

                    }

                }

                DownloadNextFile(downcount_target);
            }
            else
            {
                downcount_now = 0;
                downcount_target = 0;
            }


        }
        public string[] GetFileList(string filelocation, string user, string pass)
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            WebResponse response = null;
            StreamReader reader = null;
            try
            {
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(filelocation));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(user, pass);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFTP.Proxy = null;
                reqFTP.KeepAlive = false;
                reqFTP.UsePassive = false;
                response = reqFTP.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                // to remove the trailing '\n'
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                return result.ToString().Split('\n');
            }
            catch
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                downloadFiles = null;
                return downloadFiles;
            }
        }

        public void DownloadFileWeb(string filelocation, string savepath)
        {
            updateDownloadList[downcount_target, 0] = filelocation;
            updateDownloadList[downcount_target, 1] = savepath;
            downcount_target++;
        }

        private void DownloadNextFile(int downcount_target)
        {
            if (downcount_target > downcount_now)
            {

                WebClient downloader = new WebClient();
                downloader.DownloadFileCompleted += new AsyncCompletedEventHandler(downloader_DownloadFileCompleted);
                downloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(downloader_DownloadProgressChanged);
                downloader.DownloadFileAsync(new Uri(updateDownloadList[downcount_now, 0]), updateDownloadList[downcount_now, 1]);
                downcount_now++;
            }
            else
            {
                downcount_now = 0;
                downcount_target = 0;
                comStatus.Text = "Update completed";
                update.Text = "Update";
                LoadAll(this, EventArgs.Empty);
            }
        }

        private void DownloadFtpFile(string filelocation, string savePath, string user, string pass)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(filelocation);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(user, pass);
            request.UseBinary = true;

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                using (Stream rs = response.GetResponseStream())
                {
                    using (FileStream ws = new FileStream(savePath, FileMode.Create))
                    {
                        byte[] buffer = new byte[2048];
                        int bytesRead = rs.Read(buffer, 0, buffer.Length);

                        while (bytesRead > 0)
                        {
                            ws.Write(buffer, 0, bytesRead);
                            bytesRead = rs.Read(buffer, 0, buffer.Length);
                        }
                    }
                }
            }
        }

        void downloader_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            comStatus.Text = "Updating:" + Convert.ToString(e.ProgressPercentage) + "%";
        }

        void downloader_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {




            if (e.Error != null)
                comStatus.Text = "DB Update failed";
            else
            {
                if (updateandclose)
                {
                    downcount_target = 0;
                    downcount_now = 0;
                    this.Close();
                }
                else
                {
                    DownloadNextFile(downcount_target);
                }
            }



        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!updateandclose)
            {
                string message = "You really want to quit?";

                var res = MessageBox.Show(this, message, "Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (res != DialogResult.Yes)
                {

                    e.Cancel = true;
                    return;
                }
                else
                {
                    if (downcount_now < downcount_target)
                    {
                        e.Cancel = true;
                        updateandclose = true;
                        return;
                    }
                }
            }

        }


    }
}

