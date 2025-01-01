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
    public partial class kullanicilar : Form
    {
        public kullanicilar()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
        private void kullanicilar_Load(object sender, EventArgs e)
        {
            OleDbConnection cn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM giris", cn);
            DataTable rst = new DataTable();
            adapter.Fill(rst);
            dataGridView1.DataSource = rst;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Kullanıcı";
            ChangeDatagridviewDesign(dataGridView1);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in dataGridView1.SelectedRows)
            {
                Ad.Text = r.Cells[1].Value.ToString();
                Soyad.Text = r.Cells[2].Value.ToString();
                if (r.Cells[3].Value.ToString() == "Admin")
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = true;
                }
                if (r.Cells[3].Value.ToString() == "Muhasebe")
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                    radioButton3.Checked = false;
                }
                if (r.Cells[3].Value.ToString() == "Yönetim")
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ad.Text != "" && Soyad.Text != "")
                {
                    bool i = false;
                    foreach (DataGridViewRow r in dataGridView1.Rows)
                    {
                        if (Ad.Text == r.Cells[1].Value.ToString())
                        {
                            i = true;
                            break;
                        }
                    }
                    if (i == false)
                    {
                        OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                        connection.Open();
                        OleDbCommand insert = new OleDbCommand("INSERT INTO giris ([Kullanici],[Parola],[Yetki]) VALUES (@kullanici,@sifre,@yetki)", connection);
                        insert.Parameters.AddWithValue("@kullanici", Ad.Text);
                        insert.Parameters.AddWithValue("@sifre", Soyad.Text);
                        if (radioButton1.Checked) { insert.Parameters.AddWithValue("@yetki", "Yönetim"); }
                        if (radioButton2.Checked) { insert.Parameters.AddWithValue("@yetki", "Muhasebe"); }
                        if (radioButton3.Checked) { insert.Parameters.AddWithValue("@yetki", "Admin"); }
                        insert.ExecuteNonQuery();
                        connection.Close();
                        OleDbDataAdapter data = new OleDbDataAdapter("SELECT * FROM giris", connection);
                        DataTable rst = new DataTable();
                        data.Fill(rst);
                        dataGridView1.DataSource = rst;
                        Ad.Clear();
                        Soyad.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Aynı kullanıcıyı birden fazla kez ekleyemezsiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    } 
                }
                else
                {
                    MessageBox.Show("Boş Alan Bırakılmamalıdır.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                if (Ad.Text != "" && Soyad.Text != "")
                {
                    foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                    {
                        if (Convert.ToInt16(r.Cells[0].Value) == 1)
                        {
                            OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                            connection.Open();
                            OleDbCommand insert = new OleDbCommand("UPDATE giris SET [Parola]=@sifre where [ID]=@id", connection);
                            insert.Parameters.AddWithValue("@sifre", Soyad.Text);
                            foreach (DataGridViewRow r2 in dataGridView1.SelectedRows)
                            {
                                insert.Parameters.AddWithValue("@id", Convert.ToInt16(r2.Cells[0].Value));
                            }
                            insert.ExecuteNonQuery();
                            connection.Close();
                            OleDbDataAdapter data = new OleDbDataAdapter("SELECT * FROM giris", connection);
                            DataTable rst = new DataTable();
                            data.Fill(rst);
                            dataGridView1.DataSource = rst;
                            Ad.Clear();
                            Soyad.Clear();
                        }
                        else
                        {
                            OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                            connection.Open();
                            OleDbCommand insert = new OleDbCommand("UPDATE giris SET [Kullanici]=@kullanici , [Parola]=@sifre , [Yetki]=@yetki where [ID]=@id", connection);
                            insert.Parameters.AddWithValue("@kullanici", Ad.Text);
                            insert.Parameters.AddWithValue("@sifre", Soyad.Text);
                            if (radioButton1.Checked) { insert.Parameters.AddWithValue("@yetki", "Yönetim"); }
                            if (radioButton2.Checked) { insert.Parameters.AddWithValue("@yetki", "Muhasebe"); }
                            if (radioButton3.Checked) { insert.Parameters.AddWithValue("@yetki", "Admin"); }
                            foreach (DataGridViewRow r2 in dataGridView1.SelectedRows)
                            {
                                insert.Parameters.AddWithValue("@id", Convert.ToInt16(r2.Cells[0].Value));
                            }
                            insert.ExecuteNonQuery();
                            connection.Close();
                            OleDbDataAdapter data = new OleDbDataAdapter("SELECT * FROM giris", connection);
                            DataTable rst = new DataTable();
                            data.Fill(rst);
                            dataGridView1.DataSource = rst;
                            Ad.Clear();
                            Soyad.Clear();
                        }
                    } 
                }
                else
                {
                    MessageBox.Show("Boş Alan Bırakılmamalıdır.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
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
                foreach (DataGridViewRow r in dataGridView1.SelectedRows)
                {
                    if (Convert.ToInt16(r.Cells[0].Value) != 1)
                    {
                        OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                        connection.Open();
                        OleDbCommand insert = new OleDbCommand("DELETE FROM giris where [ID]=@id", connection);
                        foreach (DataGridViewRow rr in dataGridView1.SelectedRows)
                        {
                            insert.Parameters.AddWithValue("@id", Convert.ToInt16(rr.Cells[0].Value));
                        }
                        insert.ExecuteNonQuery();
                        connection.Close();
                        OleDbDataAdapter data = new OleDbDataAdapter("SELECT * FROM giris", connection);
                        DataTable rst = new DataTable();
                        data.Fill(rst);
                        dataGridView1.DataSource = rst;
                        Ad.Clear();
                        Soyad.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Bu Kullanıcıyı Silemezsiniz.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
            OleDbDataAdapter data = new OleDbDataAdapter("SELECT * FROM giris", connection);
            DataTable rst = new DataTable();
            data.Fill(rst);
            dataGridView1.DataSource = rst;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Ad.Clear();
            Soyad.Clear();
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
                connection.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }
    }
}
