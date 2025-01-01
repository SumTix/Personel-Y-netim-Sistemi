﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SumTix_2
{
    public partial class Form1 : Form
    {
        private Form activeForm;
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPane.Controls.Add(childForm);
            this.panelDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            label2.Text = childForm.Text;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new duzeltme(), sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new goruntuleme(), sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new devamsizlik(), sender);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new dgoruntuleme(), sender);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenChildForm(new odemeekleme(), sender);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenChildForm(new odemekaydi(), sender);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenChildForm(new kullanicilar(), sender);
        }
    }
}
