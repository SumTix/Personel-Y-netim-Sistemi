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
    public partial class duzeltme : Form
    {
        public duzeltme()
        {
            InitializeComponent();
        }
        private void temizle()
        {
            Ad.Clear();
            Soyad.Clear();
            Telefon.Clear();
            Adres.Clear();
            No.Clear();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                OleDbCommand cmd = new OleDbCommand("DELETE FROM bilgiler WHERE [ID] = @id", con);
                foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
                {
                    int numara = Convert.ToInt16(drow.Cells[0].Value);
                    cmd.Parameters.AddWithValue("@id", numara);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                OleDbDataAdapter data = new OleDbDataAdapter("SELECT * FROM bilgiler", con);
                DataTable rst = new DataTable();
                data.Fill(rst);
                dataGridView1.DataSource = rst;
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata : " + hata.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=database.accdb");
                OleDbCommand duzelt = new OleDbCommand("UPDATE bilgiler SET [Ad]=@ad, [Soyad]=@soyad, [Telefon]=@telefon, [Adres]=@adres, [No]=@no WHERE [ID]=@id", con);
                duzelt.Parameters.AddWithValue("@ad", Ad.Text);
                duzelt.Parameters.AddWithValue("@soyad", Soyad.Text);
                duzelt.Parameters.AddWithValue("@telefon", Telefon.Text);
                duzelt.Parameters.AddWithValue("@adres", Adres.Text);
                duzelt.Parameters.AddWithValue("@no", No.Text);
                foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
                {
                    int numara = Convert.ToInt16(drow.Cells[0].Value);
                    duzelt.Parameters.AddWithValue("@id", numara);
                }
                con.Open();
                duzelt.ExecuteNonQuery();
                con.Close();
                OleDbDataAdapter data = new OleDbDataAdapter("SELECT * FROM bilgiler", con);
                DataTable rst = new DataTable();
                data.Fill(rst);
                dataGridView1.DataSource = rst;
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata : " + hata.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                connection.Open();
                OleDbCommand insert = new OleDbCommand("INSERT INTO bilgiler ([Ad],[Soyad],[Telefon],[Adres],[No]) VALUES (@ad,@soyad,@telefon,@adres,@no)", connection);
                insert.Parameters.AddWithValue("@ad", Ad.Text);
                insert.Parameters.AddWithValue("@soyad", Soyad.Text);
                insert.Parameters.AddWithValue("@telefon", Telefon.Text);
                insert.Parameters.AddWithValue("@adres", Adres.Text);
                insert.Parameters.AddWithValue("@no", No.Text);
                insert.ExecuteNonQuery();
                connection.Close();
                OleDbDataAdapter data = new OleDbDataAdapter("SELECT * FROM bilgiler", connection);
                DataTable rst = new DataTable();
                data.Fill(rst);
                dataGridView1.DataSource = rst;
                temizle();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM bilgiler", connection);
                DataTable rst = new DataTable();
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
            temizle();
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
        private void duzeltme_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    MessageBox.Show("Bağlantı Başarılı");
                }
                else
                {
                    MessageBox.Show("Bağlantı Başarısız");
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                Ad.Text = row.Cells[1].Value.ToString();
                Soyad.Text = row.Cells[2].Value.ToString();
                Telefon.Text = row.Cells[3].Value.ToString();
                Adres.Text = row.Cells[4].Value.ToString();
                No.Text = row.Cells[5].Value.ToString();
            }
        }
    }
}
