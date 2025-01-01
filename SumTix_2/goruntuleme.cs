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

namespace SumTix_2
{
    public partial class goruntuleme : Form
    {
        public goruntuleme()
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
        private void goruntuleme_Load(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM bilgiler", connection);
                DataTable rst = new DataTable();
                adapter.Fill(rst);
                dataGridView1.DataSource = rst;
                dataGridView1.Columns[0].Visible = false; 
                ChangeDatagridviewDesign(dataGridView1);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }
    }
}
