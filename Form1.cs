using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;


namespace LearnWords
{
    public partial class Form1 : Form
    {
        int TrueNumber = 0;
        int AllNumber = 0;
        bool isSure = false;
        bool nextOne = false;
       
        

        public Form1()
        {
            SQLiteConnectionStringBuilder scsb = new SQLiteConnectionStringBuilder();
            scsb.DataSource = "a.db";
            SQLiteConnection scon = new SQLiteConnection(scsb.ToString());

            while (isSure)
            {
                if(scon.State == ConnectionState.Closed)
                    scon.Open();

                string SQLiteStr = "SELECT * FROM Words ORDER BY RANDOM()limit 1;";

                SQLiteCommand scom = new SQLiteCommand(SQLiteStr, scon);
                SQLiteDataReader sdr = null;
                try
                {
                    sdr = scom.ExecuteReader();
                    while (sdr.Read())
                    {
                        label3.Text = sdr["explanations"].ToString();
                        textBox1.Text = sdr["words"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    scon.Close();
                    isSure = false;
                }

            }

            InitializeComponent();
            textBox1.Text = null;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AllNumber++;
            if (textBox1.Text == label3.Text)
            {
                TrueNumber++;
                listBox1.Items.Add("True");
                listBox1.Items.Add("You have answered " + AllNumber + " words，and " + TrueNumber + " are true");
                isSure = true;
            }
            else
            {
                listBox1.Items.Add("False");
                listBox1.Items.Add("You have answered " + AllNumber + " words，and " + TrueNumber + " are true");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isSure = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            nextOne = true;
        }
    }
}
