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
    public partial class odemeekleme : Form
    {
        public odemeekleme()
        {
            InitializeComponent();
        }
        void Ekleme(string ad, string soyad, string no)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    double uc;
                    int gun = Convert.ToInt16(gunsayisi.Text);
                    int mesai_saati, saat = Convert.ToInt16(gunluk.Text);
                    double toplam;
                    uc = Convert.ToDouble(ucret.Text);
                    if (comboBox1.SelectedIndex == 1)
                    {
                        uc = Convert.ToDouble(ucret.Text) / saat;
                    }
                    else if (comboBox1.SelectedIndex == 2)
                    {
                        uc = Convert.ToDouble(ucret.Text) / (saat * 6);
                    }
                    else if (comboBox1.SelectedIndex == 3)
                    {
                        uc = Convert.ToDouble(ucret.Text) / (saat * 24);
                    }
                    if (mesai.Text != "")
                    {
                        mesai_saati = Convert.ToInt16(mesai.Text);
                        toplam = (uc * (gun * saat)) + (uc * (mesai_saati * 1.5));
                    }
                    else
                    {
                        toplam = uc * (gun * saat);
                    }
                    double odeme = toplam;
                    string tarih = dateTimePicker1.Value.ToString();
                    string tarih2 = tarih.Remove(10);
                    OleDbConnection cn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO odemeler ([Tarih],[No],[Ad],[Soyad],[OdemeMiktari]) VALUES (@tarih,@no,@ad,@soyad,@odememiktari)", cn);
                    cmd.Parameters.AddWithValue("@tarih", tarih2);
                    cmd.Parameters.AddWithValue("@no", no);
                    cmd.Parameters.AddWithValue("@ad", ad);
                    cmd.Parameters.AddWithValue("@soyad", soyad);
                    cmd.Parameters.AddWithValue("@odememiktari", odeme);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Başarılı Bir Şekilde Kaydedildi");
                }
                else
                {
                    int odeme = Convert.ToInt16(textBox1.Text);
                    string tarih = dateTimePicker1.Value.ToString();
                    string tarih2 = tarih.Remove(10);
                    OleDbConnection cn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
                    OleDbCommand cmd = new OleDbCommand("INSERT INTO odemeler ([Tarih],[No],[Ad],[Soyad],[OdemeMiktari]) VALUES (@tarih,@no,@ad,@soyad,@odememiktari)", cn);
                    cmd.Parameters.AddWithValue("@tarih", tarih2);
                    cmd.Parameters.AddWithValue("@no", no);
                    cmd.Parameters.AddWithValue("@ad", ad);
                    cmd.Parameters.AddWithValue("@soyad", soyad);
                    cmd.Parameters.AddWithValue("@odememiktari", odeme);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Başarılı Bir Şekilde Kaydedildi");
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
        private void odemeekleme_Load(object sender, EventArgs e)
        {
            try
            {
                radioButton1.Checked = true;
                OleDbConnection cn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb ");
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM bilgiler", cn);
                DataTable rst = new DataTable();
                adapter.Fill(rst);
                dataGridView1.DataSource = rst;
                ChangeDatagridviewDesign(dataGridView1);
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ad, soyad, no;
            foreach (DataGridViewRow r in dataGridView1.SelectedRows)
            {
                ad = r.Cells[1].Value.ToString();
                soyad = r.Cells[2].Value.ToString();
                no = r.Cells[5].Value.ToString();
                Ekleme(ad, soyad, no);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox1.Enabled = true;
                groupBox1.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox1.Enabled = false;
                groupBox1.Enabled = true;
            }
        }
    }
}
