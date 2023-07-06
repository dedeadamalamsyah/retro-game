﻿using MySqlConnector;
using Rent.Controller;
using Rent.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Rent.View
{
    public partial class Form4 : Form
    {
        private ConsoleController cc;
        private Connection conn;
        private Validation valid;

        public Form4()
        {
            cc = new ConsoleController();
            conn = new Connection();
            valid = new Validation();
            InitializeComponent();
        }

        private void refresh()
        {
            Controls.Clear();
            InitializeComponent();
            dataGridView1.DataSource = cc.readConsole();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            refresh();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            cc.addConsole(Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, comboBox1.Text);
            refresh();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            cc.updateConsole(Convert.ToInt32(textBox1.Text), textBox2.Text, textBox3.Text, comboBox1.Text);
            refresh();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            int selectedValue = (int)dataGridView1.SelectedRows[0].Cells["idconsole"].Value;
            cc.deleteConsole(selectedValue);
            refresh();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Hide();
            Form3 f3 = new Form3();
            f3.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string search = "SELECT * FROM consoles WHERE CONCAT (idconsole, brand, name, available) like '%" + textBox4.Text + "%'";
            MySqlDataAdapter da = new MySqlDataAdapter(search, conn.GetConn());
            DataTable table = new DataTable();
            da.Fill(table);
            dataGridView1.DataSource = table;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }
    }
}