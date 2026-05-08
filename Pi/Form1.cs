using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace Pi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Salvar_Click(object sender, EventArgs e)
        {   

            string nomedoferiado = txtNome.Text;
            int controle = 0;
            string dadosConexao =
                "server=localhost;user=root;password=;database=db_ecoenficiencia";
            using (MySqlConnection conn = new MySqlConnection(dadosConexao))
            {//utilizo das informações
                conn.Open();
                    string scriptInsert = "INSERT INTO tb_cadastro (nomedoferiado, datadoferiado, feed) VALUE (@nomedoferiado, @datadoferiado, @feed)";

                using (MySqlCommand comando = new MySqlCommand(scriptInsert, conn))
                {
                    comando.Parameters.AddWithValue("@nomedoferiado", nomedoferiado);
                    comando.Parameters.AddWithValue("@datadoferiado", DateTime.Now); // Substitua pelo valor correto
                    comando.Parameters.AddWithValue("@feed", ""); // Substitua pelo valor correto

                    controle = comando.ExecuteNonQuery();
                    
                }
                conn.Close();
            }

            if (controle > 0)
            {
                MessageBox.Show("Dados inseridos com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao inserir os dados.");
            }
                

            //mySqlConnection 
        }
    }
}
