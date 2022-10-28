using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MTP_lab3_spanzuratoare
{
    public partial class ListaJucatorilor : Form
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=root");
        MySqlCommand command;
        MySqlDataAdapter dataAdapter;
        DataSet dataSet;
        String sql;

        public ListaJucatorilor(Label lblJucator, Label lblPunctaj)
        {
            InitializeComponent();

            string jucator = lblJucator.Text;
            lblJucatorLog.Text = jucator;

            dataAdapter = new MySqlDataAdapter("SELECT * FROM mtp_sp.jucator ORDER BY punctaj DESC", connection);
            dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "jucator");
            dgvJucatori.DataSource = dataSet.Tables["jucator"];
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form = new Form1(lblJucatorLog.Text);
            form.ShowDialog();
        }
    }
}