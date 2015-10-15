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
                DataTable scores = new DataTable();
                da.Fill(scores);
                dataGridView1.DataSource = scores;
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
            //hide unwanted columns
            dataGridView1.Columns["ID"].Visible = false;
            dataGridView1.Columns["desc"].Visible = false;
            dataGridView1.Columns["img"].Visible = false;
            dataGridView1.Columns["qty"].Visible = false;
            dataGridView1.Columns["mix"].Visible = false;
            dataGridView1.Columns["Name"].Width = 200;
            dataGridView1.CurrentCell = dataGridView1[1, 0];
            //dataGridView1_CellClick();
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
                                output_pump[ing_no] = motor_no;
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
                    output_qty[ing_no] = Convert.ToInt16(s_q);
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

            for (int i = 0; i < 10; i++)
            {
                if ((i < mix_split.Length) && (mix_split[i] != ""))
                {
                    mix_split_copy[i] = mix_split[i];
                }
                else
                {
                    mix_split_copy[i] = "0";
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

            cocktailPhoto.Image = Image.FromFile("./img/" + Convert.ToString(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["img"].Value));
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1_CellClick();
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            LoadAll(this, EventArgs.Empty);
        }
        
    }
}
