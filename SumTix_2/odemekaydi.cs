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
    public partial class odemekaydi : Form
    {
        public odemekaydi()
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
        private void odemekaydi_Load(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection cn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM odemeler", cn);
                System.Data.DataTable rst = new System.Data.DataTable();
                adapter.Fill(rst);
                dataGridView1.DataSource = rst;
                ChangeDatagridviewDesign(dataGridView1);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[5].HeaderText = "Miktar";

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
                int odeme = Convert.ToInt16(textBox1.Text);
                OleDbConnection cn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                OleDbCommand cmd = new OleDbCommand("UPDATE odemeler SET [OdemeMiktari]=@odeme WHERE [ID]=@id", cn);
                cmd.Parameters.AddWithValue("@odeme", odeme);
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    int id = Convert.ToInt16(row.Cells[0].Value);
                    cmd.Parameters.AddWithValue("@id", id);
                }
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM odemeler", cn);
                System.Data.DataTable rst = new System.Data.DataTable();
                adapter.Fill(rst);
                dataGridView1.DataSource = rst;
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OleDbConnection cn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
            OleDbCommand cmd = new OleDbCommand("DELETE FROM odemeler WHERE [ID]=@id", cn);
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                int id = Convert.ToInt16(row.Cells[0].Value);
                cmd.Parameters.AddWithValue("@id", id);
            }
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM odemeler", cn);
            System.Data.DataTable rst = new System.Data.DataTable();
            adapter.Fill(rst);
            dataGridView1.DataSource = rst;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection cn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                OleDbCommand cmd = new OleDbCommand("DELETE FROM odemeler", cn);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM odemeler", cn);
                System.Data.DataTable rst = new System.Data.DataTable();
                adapter.Fill(rst);
                dataGridView1.DataSource = rst;
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection cn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM odemeler", cn);
                System.Data.DataTable rst = new System.Data.DataTable();
                adapter.Fill(rst);
                dataGridView1.DataSource = rst;
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
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
        private void button3_Click(object sender, EventArgs e)
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
