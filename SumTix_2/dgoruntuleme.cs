using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;
namespace SumTix_2
{
    public partial class dgoruntuleme : Form
    {
        public dgoruntuleme()
        {
            InitializeComponent();
        }
        public static void ChangeDatagridviewDesign(DataGridView datagridview)
        {
            datagridview.RowHeadersVisible = false;
            datagridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datagridview.BorderStyle = BorderStyle.None;
            datagridview.DefaultCellStyle.SelectionBackColor = Color.FromArgb(128, 222, 234);
            datagridview.DefaultCellStyle.SelectionForeColor = Color.Black;
            datagridview.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            datagridview.EnableHeadersVisualStyles = false;
            datagridview.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSeaGreen;
            datagridview.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            datagridview.ColumnHeadersHeight = 10;
            datagridview.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.LightSeaGreen;
            datagridview.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            datagridview.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            datagridview.RowTemplate.Height = 40;
            datagridview.AllowUserToDeleteRows = false;
            datagridview.AllowUserToResizeRows = false;
            datagridview.AllowUserToResizeColumns = false;
        }
        private void dgoruntuleme_Load(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM devamsizlik", connection);
            System.Data.DataTable rst = new System.Data.DataTable();
            adapter.Fill(rst);
            dataGridView1.DataSource = rst;
            ChangeDatagridviewDesign(dataGridView1);
            dataGridView1.Columns[0].Visible = false;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dataGridView1.SelectedRows)
            {
                if (Convert.IsDBNull(r.Cells[5].Value) && Convert.IsDBNull(r.Cells[6].Value) && Convert.IsDBNull(r.Cells[7].Value))
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                }
                else
                {
                    radioButton1.Checked = Convert.ToBoolean(r.Cells[5].Value);
                    radioButton2.Checked = Convert.ToBoolean(r.Cells[6].Value);
                    radioButton3.Checked = Convert.ToBoolean(r.Cells[7].Value);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection cn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                OleDbCommand cmd = new OleDbCommand("UPDATE devamsizlik SET [Raporlu]=@raporlu , [İzinli]=@izinli , [İzinsiz]=@izinsiz WHERE [ID]=@id", cn);
                cmd.Parameters.AddWithValue("@raporlu", radioButton1.Checked);
                cmd.Parameters.AddWithValue("@izinli", radioButton2.Checked);
                cmd.Parameters.AddWithValue("@izinsiz", radioButton3.Checked);
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    int id = Convert.ToInt16(r.Cells[0].Value);
                    cmd.Parameters.AddWithValue("@id", id);
                }
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM devamsizlik", cn);
                System.Data.DataTable rst = new System.Data.DataTable();
                adapter.Fill(rst);
                dataGridView1.DataSource = rst;
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                OleDbCommand cmd = new OleDbCommand("DELETE FROM devamsizlik", connection);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM devamsizlik", connection);
                System.Data.DataTable rst = new System.Data.DataTable();
                adapter.Fill(rst);
                dataGridView1.DataSource = rst;
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM devamsizlik", connection);
                System.Data.DataTable rst = new System.Data.DataTable();
                adapter.Fill(rst);
                dataGridView1.DataSource = rst;
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                OleDbCommand cmd = new OleDbCommand("DELETE FROM devamsizlik WHERE [ID] = @id", con);
                foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
                {
                    int numara = Convert.ToInt16(drow.Cells[0].Value);
                    cmd.Parameters.AddWithValue("@id", numara);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                OleDbDataAdapter data = new OleDbDataAdapter("SELECT * FROM devamsizlik", con);
                System.Data.DataTable rst = new System.Data.DataTable();
                data.Fill(rst);
                dataGridView1.DataSource = rst;
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata : " + hata.Message);
            }
        }
        private void ExportToExcel(DataGridView dataGridView)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

            excel.Visible = true;

            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);

            Microsoft.Office.Interop.Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

            for (int i = 0; i < dataGridView.Columns.Count; i++)
            {
                sheet1.Cells[1, i + 1] = dataGridView.Columns[i].HeaderText;
            }

            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {
                    sheet1.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value.ToString();
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToExcel(dataGridView1);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }
    }
}