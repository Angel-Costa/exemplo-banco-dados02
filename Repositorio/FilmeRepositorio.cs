﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;

namespace Repositorio
{
    public class FilmeRepositorio
    {
        string CadeiaConexao = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\64716\Documents\ExemploBancoDados02.mdf;Integrated Security=True;Connect Timeout=30";

        /*
         * Método que irá retornar os dados dos
         * filmes(List<Filme) da tabela de filmes.
         */
        public List<Filme> ObterTodos()
        {
            
            // Cria a conexão com o banco de dados e abre.
            
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = CadeiaConexao;
            conexao.Open();
            
            /* 
             * Cria o comando para ser executado no BD 
             * e diz para este comando qual é a conexão que está aberta.
             */
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexao;
            comando.CommandText = "SELECT * FROM filmes";

            /*
             * Cria uma tabela em memória para poder obter os dados que são 
             * retornados do BD executando o comando no BD.
             */
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());

            // Cria uma lista para adicionar os filmes do BD.
            List<Filme> filmes = new List<Filme>();

            // Percorre todos os registros lidos do BD.
            for(int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                // Cria um objeto com as informações obtidas do BD.
                Filme filme = new Filme();
                filme.Id = Convert.ToInt32(linha["id"]);
                filme.Nome = linha["nome"].ToString();
                filme.Avaliacao = Convert.ToDecimal(linha["avaliacao"]);
                filme.Duracao = Convert.ToDateTime(linha["duracao"]);
                filme.Curtiu = Convert.ToBoolean(linha["curtiu"]);
                filme.Categoria = linha["categoria"].ToString();
                filme.TemSequencia = Convert.ToBoolean(linha["tem_sequencia"]);
                // Adiciona o objeto que foi criado a lista de filmes.
                filmes.Add(filme);
            }
            // Fecha a conexão do BD.
            conexao.Close();
            // Retorna a ista de filmes.
            return filmes;
        }
    }
}