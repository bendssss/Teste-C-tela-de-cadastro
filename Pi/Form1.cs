using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace Projeto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const string DADOS_CONEXAO =
            "server=localhost;user=root;password=;database=projeto_banco_csharp";
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string campoNome = txtNome.Text;
            string campoServico = cbServico.Text;
            string campoData = dtpData.Text;

            DateTime dataConvertida = DateTime.Parse(campoData);

            int controleLinhasAfetadas = 0;

            //MessageBox.Show( 
            //    $"nome: {campoNome}\n" +
            //    $"serviço: {campoServico}\n" +
            //    $"data: {campoData}\n" +
            //    $"data Convertida: {dataConvertida}");

            
            using (MySqlConnection conn = new MySqlConnection(DADOS_CONEXAO) )
            {// utilizo das informações
                conn.Open();
                string scriptInsert = "INSERT INTO tb_cadastro (nome, servico, data_servico) " +
                                        "VALUE (@nome, @servico, @data_servico)";

                using (MySqlCommand comando = new MySqlCommand(scriptInsert,conn))
                {
                    comando.Parameters.AddWithValue("@nome", campoNome);
                    comando.Parameters.AddWithValue("@servico", campoServico);
                    comando.Parameters.AddWithValue("@data_servico", dataConvertida);

                    controleLinhasAfetadas = comando.ExecuteNonQuery();
                }
                conn.Close();
            }//MysqlConnection

            if (controleLinhasAfetadas > 0)
            {
                MessageBox.Show("Dados salvo com sucesso!");
            } else
            {
                MessageBox.Show("Ops. Algo deu errado!!!");
            }

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            string idConsulta = txtId.Text;


            using (MySqlConnection conn = new MySqlConnection(DADOS_CONEXAO))
            {
                conn.Open();
                string scriptConsultaIndividual = "SELECT * FROM tb_cadastro WHERE id = @id";

                using (MySqlCommand comando = new MySqlCommand(scriptConsultaIndividual, conn))
                {
                    comando.Parameters.AddWithValue("@id", idConsulta);


                    var dadosResultado = comando.ExecuteReader();

                    while (dadosResultado.Read())
                    {
                        lbIdResultado.Text = dadosResultado["id"].ToString();
                        lbNomeResultado.Text = dadosResultado["nome"].ToString();
                        lbServicoResultado.Text = dadosResultado["servico"].ToString();
                        lbDataResultado.Text = dadosResultado["data_servico"].ToString();
                    }


                }

                conn.Close();
            }
            
        }

        private void btnConsultarLista_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(DADOS_CONEXAO))
            {
                conn.Open();

                string campoServico = cbServico.Text;
                DateTime dataConvertida = DateTime.Parse(dtpData.Text);

                string scriptConsulta = "";

                if (campoServico != "")
                {
                    scriptConsulta = "SELECT * FROM tb_cadastro WHERE servico = @servico";
                } else
                {
                    scriptConsulta = "SELECT * FROM tb_cadastro";
                }

                if (dataConvertida != null && dataConvertida != DateTime.Now)
                {
                    scriptConsulta = "SELECT * FROM tb_cadastro WHERE data_servico = @data_servico";
                }
                else
                {
                    scriptConsulta = "SELECT * FROM tb_cadastro";
                }

                using (MySqlCommand comando = new MySqlCommand(scriptConsulta, conn))
                {
                    if (campoServico != "")
                    {
                        comando.Parameters.AddWithValue("@servico", campoServico);
                    }

                    if (dataConvertida != null && dataConvertida != DateTime.Now)
                    {
                        comando.Parameters.AddWithValue("@data_servico", dataConvertida);
                    }

                    MySqlDataAdapter resultadoConsultaMySql = new MySqlDataAdapter(comando);

                    DataTable dt = new DataTable();

                    resultadoConsultaMySql.Fill(dt);

                    dgvListarTudo.DataSource = dt;



                }

                conn.Close();
            }
        }
    }
}
