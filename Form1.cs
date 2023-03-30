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
using Npgsql;

namespace DZ_2
{
    public partial class Form1 : Form
    {
        NpgsqlConnection connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=0000;Database=zzz;");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                string str = "";

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < 3  ; j++) 
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value != null)
                        {
                            str += dataGridView1.Rows[i].Cells[j].Value.ToString() + " ";
                        }
                    }

                    str += Environment.NewLine;
                }

                using (SaveFileDialog fileDialog = new SaveFileDialog())
                {
                    fileDialog.FileName = " ";
                    fileDialog.Filter = "Текстовый документ (*.txt)|*.txt|Файл формата csv (*.csv)|*.csv";
                    fileDialog.ShowDialog();
                    File.WriteAllText(fileDialog.FileName, str);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connection.Open();
            dataGridView1.Columns.Clear();
            string sql = "SELECT * FROM client";
            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection);
            dataGridView1.Columns.Add("S1", "Имя");
            dataGridView1.Columns.Add("S2", "Фамилия");
            dataGridView1.Columns.Add("S3", "Отчество");
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["first_name"], reader["last_name"], reader["potranymic"]);
                }
            }
            connection.Close();
        }
    }
}
