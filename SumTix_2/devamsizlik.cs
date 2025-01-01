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
    public partial class devamsizlik : Form
    {
        public devamsizlik()
        {
            InitializeComponent();
        }
        private void Ekleme(string ad, string soyad, string no, string tarih2)
        {
            try
            {
                bool i = false;
                foreach (DataGridViewRow r in dataGridView2.Rows)
                {
                    if (r.Cells[1].Value != null)
                    {
                        if (no == r.Cells[2].Value.ToString() && tarih2 == r.Cells[1].Value.ToString())
                        {
                            i = true;
                        }
                    }


                }
                if (i == true)
                {
                    MessageBox.Show("Bir personele aynı günde birden fazla devamsızlık ekleyemezsiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO devamsizlik ([Tarih],[No],[Ad],[Soyad],[Raporlu],[İzinli],[İzinsiz]) VALUES (@tarih,@no,@ad,@soyad,@raporlu,@izinli,@izinsiz)", connection);
                    cmd.Parameters.AddWithValue("@tarih", tarih2);
                    cmd.Parameters.AddWithValue("@no", no);
                    cmd.Parameters.AddWithValue("@ad", ad);
                    cmd.Parameters.AddWithValue("@soyad", soyad);
                    cmd.Parameters.AddWithValue("@raporlu", radioButton1.Checked);
                    cmd.Parameters.AddWithValue("@izinli", radioButton2.Checked);
                    cmd.Parameters.AddWithValue("@izinsiz", radioButton3.Checked);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Başarılı");
                    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM devamsizlik", connection);
                    DataTable rst = new DataTable();
                    adapter.Fill(rst);
                    dataGridView2.DataSource = rst;
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
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
        private void devamsizlik_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            OleDbConnection connection = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM bilgiler", connection);
            DataTable rst = new DataTable();
            OleDbDataAdapter adapter2 = new OleDbDataAdapter("SELECT * FROM devamsizlik", connection);
            DataTable rst2 = new DataTable();
            adapter.Fill(rst);
            adapter2.Fill(rst2);
            dataGridView1.DataSource = rst;
            dataGridView2.DataSource = rst2;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            ChangeDatagridviewDesign(dataGridView1);
            ChangeDatagridviewDesign(dataGridView2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string ad, soyad, no, tarih;
            tarih = dateTimePicker1.Value.ToString();
            string tarih2 = tarih.Remove(10);
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                ad = row.Cells[1].Value.ToString();
                soyad = row.Cells[2].Value.ToString();
                no = row.Cells[5].Value.ToString();
                Ekleme(ad, soyad, no, tarih2);
            }
        }
    }
}
