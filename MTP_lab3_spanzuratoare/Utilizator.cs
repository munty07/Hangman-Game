using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MTP_lab3_spanzuratoare
{
    public partial class Utilizator : Form
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=root");
        MySqlCommand command;
        static string insert_nume;
        public static string pct;
        String sql;

        public Utilizator()
        {
            InitializeComponent();
        }
        public void insert_db()
        {
            insert_nume = txtNumeJucator.Text;
            pct = "0";
            sql = "INSERT INTO mtp_sp.jucator(nume,punctaj) VALUES('" + insert_nume + "','" + pct + "')";
            connection.Open();
            command = new MySqlCommand(sql, connection);
            try
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Datele au fost inserate!");
                }
                else
                {
                    MessageBox.Show("Datele nu au fost inserate!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            connection.Close();
        }

        public bool cauta_utilizator()
        {
            string p = "";
            bool found = false;
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT nume FROM mtp_sp.jucator WHERE nume = '"+ txtNumeJucator.Text +"';";

            try
            {
                connection.Open();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro" + erro);
                this.Close();
            }

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                p = reader.GetString(0);
                found = true;
            }
            connection.Close();
            
            if (found)
                return true;
            else
                return false;
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            bool cauta = cauta_utilizator();
            
            if (cauta==false)
            {
                if (String.IsNullOrEmpty(txtNumeJucator.Text))
                {
                    MessageBox.Show("Introduceti numele!");
                }
                else
                {
                    MessageBox.Show("Jucatorul nu exista!\nAcesta va fi introdus in baza de date!");
                    insert_db();
                    this.Hide();
                    Form1 f = new Form1(txtNumeJucator.Text);
                    f.ShowDialog();
                }
            }
            else
            {
                if (String.IsNullOrEmpty(txtNumeJucator.Text))
                {
                    MessageBox.Show("Introduceti numele!");
                }
                else
                {
                    this.Hide();
                    Form1 f = new Form1(txtNumeJucator.Text);
                    f.ShowDialog();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}