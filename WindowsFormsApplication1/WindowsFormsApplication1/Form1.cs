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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
