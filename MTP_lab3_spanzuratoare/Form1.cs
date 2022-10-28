using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace MTP_lab3_spanzuratoare
{
    public partial class Form1 : Form
    {
        static String[] words;
        static String line;
        static int len;
        static int[] found = new int[50];
        static int wrong;
        static int correct = 0;
        static int scor = 0;
        static int space;
        static int pctTotal = 0;
        static int pctCurrent = 0;
        public static string pct = "0";

        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=root");
        MySqlCommand command;
        String sql;

        public void reset()
        {
            flpCuvinte.Controls.Clear();
            pictureBox.ImageLocation = "black.png";
            lblSugestie.Text = "-";
            btnStart.Enabled = true;
            btnStart.BackColor = Color.LightGray;
            wrong = 0;
            lblGreseli.Text = "-";
            lblPunctaj.Text = "-";
            tastatura_false();
        }

        public static int[] findChar(char ch)
        {
            for(int i = 0; i < len; i++)
            {
                if(words[0][i] == ch)
                {
                    found[i] = 1;
                    correct++;
                }
            }
            return found;
        }

        public void update_db()
        {
            string punct = pctTotal.ToString();

            sql = "UPDATE mtp_sp.jucator SET punctaj = '" + punct + "' WHERE nume = '" + lblJucator.Text +"'";
            connection.Open();
            command = new MySqlCommand(sql, connection);
            try
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Datele au fost actualizate!");
                }
                else
                {
                    MessageBox.Show("Datele nu au fost actualizate!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            connection.Close();
        }

        public string punctajj()
        {
            string p = "";
           
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT punctaj FROM mtp_sp.jucator WHERE nume = '" + lblJucator.Text + "'";

            try
            {
                connection.Open();
            }
            catch (Exception erro)
            {
                MessageBox.Show("Eroare " + erro);
                this.Close();
            }

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                p = reader.GetString(0);
            }
            connection.Close();
            return p;
        }

        public void messageGameOver()
        {
            MessageBox.Show("GAME OVER!");
            var result = MessageBox.Show("Do you want to play another game?", "Form Closing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            pctCurrent = Convert.ToInt32(punctajj());
            pctTotal = Convert.ToInt32(lblPunctaj.Text);
            pctTotal += pctCurrent;

            if (result == DialogResult.No)
            {
                update_db();
                this.Hide();
                Utilizator form = new Utilizator();
                form.ShowDialog();
            }
            else
            {
                update_db();
                reset();
            }
        }

        public void messageYouWin()
        {
            MessageBox.Show("YOU WIN!");
            var result = MessageBox.Show("Do you want to play another game?", "Form Closing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            pctCurrent = Convert.ToInt32(punctajj());
            pctTotal = Convert.ToInt32(lblPunctaj.Text);
            pctTotal += pctCurrent;

            if (result == DialogResult.No)
            {
                update_db();
                this.Hide();
                Utilizator form = new Utilizator();
                form.ShowDialog();
            }
            else
            {
                update_db();
                reset();
            }
        }

        public void option(int mistakes)
        {
            switch (mistakes)
            {
                case 1:
                    pictureBox.ImageLocation = "img1.png";
                    break;
                case 2:
                    pictureBox.ImageLocation = "img2.png";
                    break;
                case 3:
                    pictureBox.ImageLocation = "img3.png";
                    break;
                case 4:
                    pictureBox.ImageLocation = "img4.png";
                    break;
                case 5:
                    pictureBox.ImageLocation = "img5.png";
                    break;
            }
        }

        public Form1(string txtNumeJucator)
        {
            InitializeComponent();
            tastatura_false();
            lblJucator.Text = txtNumeJucator;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        
        private void btnStart_Click(object sender, EventArgs e)
        {
            pctTotal = 0;
            pctCurrent = 0;
            correct = 0;
            scor = 50;
            pictureBox.ImageLocation = "img.png";
            var linii = File.ReadAllLines("cuvinte.txt");
            var random = new Random();
            var linierandom = random.Next(0, linii.Length - 1);
            var l = linii[linierandom];

            StreamReader fisierCuvinte = new StreamReader("cuvinte.txt");
            line = l;
            words = line.Split('-');

            int randomLine = random.Next(0, words.Length);

            lblSugestie.Text = words[1];

            len = words[0].Length;

            bool ok = false;

            for(int i = 0; i < len; i++)
            {
                TextBox txt = new TextBox();
                Label lbl = new Label();

                if(words[0][i] == ' ')
                {
                    ok = true;
                    flpCuvinte.Controls.Add(lbl);
                    lbl.Text = " ";
                    lbl.Size = new Size(20, 70);
                }

                if (ok == false)
                {
                    flpCuvinte.Controls.Add(txt);
                    txt.Name = "txt" + i.ToString();
                    txt.Size = new Size(20, 70);
                }

                ok = false;
                btnStart.Enabled = false;
                btnStart.BackColor = Color.DarkGray;
                tastatura_true();
                lblGreseli.Text = "0/5";
                lblPunctaj.Text = "50";
                space = 0;
            }

            for (int i = 0; i < len; i++)
            {
                if (words[0][i] == ' ')
                {
                    space++;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            reset();
        }
///---LITERA A---///
        private void btnA_Click(object sender, EventArgs e)
        {
            findChar('A');
            bool ok = false;

            for(int i = 0; i < len; i++)
            {
                if(found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "A";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if(ok == false)
            {
                btnA.Enabled = false;
                btnA.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnA.BackColor = Color.Green;
                btnA.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }
            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA B---///
        private void btnB_Click(object sender, EventArgs e)
        {
            findChar('B');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "B";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnB.Enabled = false;
                btnB.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnB.BackColor = Color.Green;
                btnB.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA C---///
        private void btnC_Click(object sender, EventArgs e)
        {
            findChar('C');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "C";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnC.Enabled = false;
                btnC.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnC.BackColor = Color.Green;
                btnC.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA D---///
        private void btnD_Click(object sender, EventArgs e)
        {
            findChar('D');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "D";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnD.Enabled = false;
                btnD.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnD.BackColor = Color.Green;
                btnD.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA E---///
        private void btnE_Click(object sender, EventArgs e)
        {
            findChar('E');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "E";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnE.Enabled = false;
                btnE.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnE.BackColor = Color.Green;
                btnE.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
 ///---LITERA F---///
        private void btnF_Click(object sender, EventArgs e)
        {
            findChar('F');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "F";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnF.Enabled = false;
                btnF.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnF.BackColor = Color.Green;
                btnF.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA G---///
        private void btnG_Click(object sender, EventArgs e)
        {
            findChar('G');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "G";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnG.Enabled = false;
                btnG.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnG.BackColor = Color.Green;
                btnG.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA H---///
        private void btnH_Click(object sender, EventArgs e)
        {
            findChar('H');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "H";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnH.Enabled = false;
                btnH.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnH.BackColor = Color.Green;
                btnH.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA I---///
        private void btnI_Click(object sender, EventArgs e)
        {
            findChar('I');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "I";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnI.Enabled = false;
                btnI.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnI.BackColor = Color.Green;
                btnI.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA J---///
        private void btnJ_Click(object sender, EventArgs e)
        {
            findChar('J');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "J";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnJ.Enabled = false;
                btnJ.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnJ.BackColor = Color.Green;
                btnJ.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA K---///
        private void btnK_Click(object sender, EventArgs e)
        {
            findChar('K');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "K";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnK.Enabled = false;
                btnK.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnK.BackColor = Color.Green;
                btnK.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA L---///
        private void btnL_Click(object sender, EventArgs e)
        {
            findChar('L');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "L";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnL.Enabled = false;
                btnL.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnL.BackColor = Color.Green;
                btnL.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA M---///
        private void btnM_Click(object sender, EventArgs e)
        {
            findChar('M');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "M";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnM.Enabled = false;
                btnM.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnM.BackColor = Color.Green;
                btnM.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA N---///
        private void btnN_Click(object sender, EventArgs e)
        {
            findChar('N');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "N";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnN.Enabled = false;
                btnN.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnN.BackColor = Color.Green;
                btnN.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA O---///
        private void btnO_Click(object sender, EventArgs e)
        {
            findChar('O');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "O";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnO.Enabled = false;
                btnO.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnO.BackColor = Color.Green;
                btnO.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA P---///
        private void btnP_Click(object sender, EventArgs e)
        {
            findChar('P');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "P";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnP.Enabled = false;
                btnP.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnP.BackColor = Color.Green;
                btnP.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA Q---///
        private void btnQ_Click(object sender, EventArgs e)
        {
            findChar('Q');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "Q";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnQ.Enabled = false;
                btnQ.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnQ.BackColor = Color.Green;
                btnQ.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA R---///
        private void btnR_Click(object sender, EventArgs e)
        {
            findChar('R');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "R";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnR.Enabled = false;
                btnR.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnR.BackColor = Color.Green;
                btnR.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA S---///
        private void btnS_Click(object sender, EventArgs e)
        {
            findChar('S');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "S";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnS.Enabled = false;
                btnS.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnS.BackColor = Color.Green;
                btnS.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA T---///
        private void btnT_Click(object sender, EventArgs e)
        {
            findChar('T');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "T";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnT.Enabled = false;
                btnT.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnT.BackColor = Color.Green;
                btnT.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA U---///
        private void btnU_Click(object sender, EventArgs e)
        {
            findChar('U');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "U";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnU.Enabled = false;
                btnU.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnU.BackColor = Color.Green;
                btnU.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA V---///
        private void btnV_Click(object sender, EventArgs e)
        {
            findChar('V');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "V";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnV.Enabled = false;
                btnV.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnV.BackColor = Color.Green;
                btnV.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA W---///
        private void btnW_Click(object sender, EventArgs e)
        {
            findChar('W');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "W";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnW.Enabled = false;
                btnW.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnW.BackColor = Color.Green;
                btnW.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA X---///
        private void btnX_Click(object sender, EventArgs e)
        {
            findChar('X');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "X";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnX.Enabled = false;
                btnX.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnX.BackColor = Color.Green;
                btnX.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA Y---///
        private void btnY_Click(object sender, EventArgs e)
        {
            findChar('Y');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "Y";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnY.Enabled = false;
                btnY.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }
            }
            else
            {
                btnY.BackColor = Color.Green;
                btnY.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }
///---LITERA Z---///
        private void btnZ_Click(object sender, EventArgs e)
        {
            findChar('Z');
            bool ok = false;

            for (int i = 0; i < len; i++)
            {
                if (found[i] == 1)
                {
                    Control txt = (TextBox)Controls.Find("txt" + i.ToString(), true)[0];
                    txt.Text = "Z";
                    scor += 10;
                    lblPunctaj.Text = scor.ToString();
                    ok = true;
                }
            }
            if (ok == false)
            {
                btnZ.Enabled = false;
                btnZ.BackColor = Color.Red;
                wrong++;
                lblGreseli.Text = wrong.ToString() + "/5";
                scor -= 10;
                lblPunctaj.Text = scor.ToString();
                option(wrong);
                if (wrong == 5)
                {
                    messageGameOver();
                }

            }
            else
            {
                btnZ.BackColor = Color.Green;
                btnZ.Enabled = false;
                if (correct == words[0].Length - space)
                {
                    messageYouWin();
                }
            }

            for (int i = 0; i < len; i++)
            {
                found[i] = 0;
            }
        }

///---FUNCTIONALITATI BUTOANE TASTATURA---///
        public void tastatura_true()
        {
            //A
            btnA.Enabled = true;
            btnA.BackColor = Color.LightGray;
            //B
            btnB.Enabled = true;
            btnB.BackColor = Color.LightGray;
            //C
            btnC.Enabled = true;
            btnC.BackColor = Color.LightGray;
            //D
            btnD.Enabled = true;
            btnD.BackColor = Color.LightGray;
            //E
            btnE.Enabled = true;
            btnE.BackColor = Color.LightGray;
            //F
            btnF.Enabled = true;
            btnF.BackColor = Color.LightGray;
            //G
            btnG.Enabled = true;
            btnG.BackColor = Color.LightGray;
            //H
            btnH.Enabled = true;
            btnH.BackColor = Color.LightGray;
            //I
            btnI.Enabled = true;
            btnI.BackColor = Color.LightGray;
            //J
            btnJ.Enabled = true;
            btnJ.BackColor = Color.LightGray;
            //K
            btnK.Enabled = true;
            btnK.BackColor = Color.LightGray;
            //L
            btnL.Enabled = true;
            btnL.BackColor = Color.LightGray;
            //M
            btnM.Enabled = true;
            btnM.BackColor = Color.LightGray;
            //N
            btnN.Enabled = true;
            btnN.BackColor = Color.LightGray;
            //O
            btnO.Enabled = true;
            btnO.BackColor = Color.LightGray;
            //P
            btnP.Enabled = true;
            btnP.BackColor = Color.LightGray;
            //Q
            btnQ.Enabled = true;
            btnQ.BackColor = Color.LightGray;
            //R
            btnR.Enabled = true;
            btnR.BackColor = Color.LightGray;
            //S
            btnS.Enabled = true;
            btnS.BackColor = Color.LightGray;
            //T
            btnT.Enabled = true;
            btnT.BackColor = Color.LightGray;
            //U
            btnU.Enabled = true;
            btnU.BackColor = Color.LightGray;
            //V
            btnV.Enabled = true;
            btnV.BackColor = Color.LightGray;
            //W
            btnW.Enabled = true;
            btnW.BackColor = Color.LightGray;
            //X
            btnX.Enabled = true;
            btnX.BackColor = Color.LightGray;
            //Y
            btnY.Enabled = true;
            btnY.BackColor = Color.LightGray;
            //Z
            btnZ.Enabled = true;
            btnZ.BackColor = Color.LightGray;
        }

        public void tastatura_false()
        {
            //A
            btnA.Enabled = false;
            btnA.BackColor = Color.DarkGray;
            //B
            btnB.Enabled = false;
            btnB.BackColor = Color.DarkGray;
            //C
            btnC.Enabled = false;
            btnC.BackColor = Color.DarkGray;
            //D
            btnD.Enabled = false;
            btnD.BackColor = Color.DarkGray;
            //E
            btnE.Enabled = false;
            btnE.BackColor = Color.DarkGray;
            //F
            btnF.Enabled = false;
            btnF.BackColor = Color.DarkGray;
            //G
            btnG.Enabled = false;
            btnG.BackColor = Color.DarkGray;
            //H
            btnH.Enabled = false;
            btnH.BackColor = Color.DarkGray;
            //I
            btnI.Enabled = false;
            btnI.BackColor = Color.DarkGray;
            //J
            btnJ.Enabled = false;
            btnJ.BackColor = Color.DarkGray;
            //K
            btnK.Enabled = false;
            btnK.BackColor = Color.DarkGray;
            //L
            btnL.Enabled = false;
            btnL.BackColor = Color.DarkGray;
            //M
            btnM.Enabled = false;
            btnM.BackColor = Color.DarkGray;
            //N
            btnN.Enabled = false;
            btnN.BackColor = Color.DarkGray;
            //O
            btnO.Enabled = false;
            btnO.BackColor = Color.DarkGray;
            //P
            btnP.Enabled = false;
            btnP.BackColor = Color.DarkGray;
            //Q
            btnQ.Enabled = false;
            btnQ.BackColor = Color.DarkGray;
            //R
            btnR.Enabled = false;
            btnR.BackColor = Color.DarkGray;
            //S
            btnS.Enabled = false;
            btnS.BackColor = Color.DarkGray;
            //T
            btnT.Enabled = false;
            btnT.BackColor = Color.DarkGray;
            //U
            btnU.Enabled = false;
            btnU.BackColor = Color.DarkGray;
            //V
            btnV.Enabled = false;
            btnV.BackColor = Color.DarkGray;
            //W
            btnW.Enabled = false;
            btnW.BackColor = Color.DarkGray;
            //X
            btnX.Enabled = false;
            btnX.BackColor = Color.DarkGray;
            //Y
            btnY.Enabled = false;
            btnY.BackColor = Color.DarkGray;
            //Z
            btnZ.Enabled = false;
            btnZ.BackColor = Color.DarkGray;
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            pct = "";
            this.Hide();
            Utilizator form = new Utilizator();
            form.ShowDialog();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            this.Hide();
            ListaJucatorilor form2 = new ListaJucatorilor(lblJucator, lblPunctaj);
            form2.ShowDialog(); 
        }
    }
}