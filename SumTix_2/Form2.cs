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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        /*public static void ChangeDatagridviewDesign(DataGridView datagridview)
        {
            datagridview.RowHeadersVisible = false;
            datagridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            datagridview.BorderStyle = BorderStyle.None;
            datagridview.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 255, 128);
            datagridview.DefaultCellStyle.SelectionForeColor = Color.FromArgb(211, 36, 44);
            datagridview.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            datagridview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            datagridview.EnableHeadersVisualStyles = false;
            datagridview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            datagridview.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(8, 188, 164);
            datagridview.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            datagridview.ColumnHeadersHeight = 10;
            datagridview.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(8, 188, 164);
            datagridview.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            datagridview.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            datagridview.RowTemplate.Height = 40;
            datagridview.AllowUserToDeleteRows = false;
            datagridview.AllowUserToResizeRows = false;
            datagridview.AllowUserToResizeColumns = false;
        }*/
        private void Form2_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            OleDbConnection cn = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = database.accdb");
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM giris", cn);
            DataTable rst = new DataTable();
            adapter.Fill(rst);
            dataGridView1.DataSource = rst;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ku, pa;
                ku = textBox1.Text;
                pa = textBox2.Text;
                bool i = false;
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    foreach (DataGridViewRow r in dataGridView1.Rows)
                    {
                        if (r.Cells[0].Value != null)
                        {
                            if (ku == r.Cells[1].Value.ToString() && pa == r.Cells[2].Value.ToString())
                            {
                                if (r.Cells[3].Value.ToString() == "Admin")
                                {
                                    Form1 f = new Form1();
                                    f.Show();
                                    this.Hide();
                                    i = true;
                                    break;
                                }
                                else if (r.Cells[3].Value.ToString() == "Muhasebe")
                                {
                                    MuhasebeForm f = new MuhasebeForm();
                                    f.Show();
                                    this.Hide();
                                    i = true;
                                    break;
                                }
                                else if (r.Cells[3].Value.ToString() == "Yönetim")
                                {
                                    YonetimForm f = new YonetimForm();
                                    f.Show();
                                    this.Hide();
                                    i = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (i != true)
                    {
                        MessageBox.Show("Bilgileri Kontrol Edip Tekrar Deneyiniz","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Boş Alan Bırakılmamalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
