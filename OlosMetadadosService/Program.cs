using CsvHelper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace OlosMetadadosService
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder csvcontent = new StringBuilder();
            //E definido o cabecalho do CSV
            csvcontent.AppendLine("Nome,Login,Matricula,CPF,Quartil,Admissao");
            try
            {
                //Definida a
                string connectionString = "SERVER=" + "172.21.0.40" + ";" + "DATABASE=" +
                "winover" + ";" + "UID=" + "user_olos" + ";" + "PASSWORD=" + "G710MrC2TX7PpNfYdNbp" + ";";

                MySqlConnection connection = new MySqlConnection(connectionString);

                connection.Open();

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "select * from vw_metadados_olos;";
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        csvcontent.AppendLine (String.Format("{0},{1},{2},{3},{4},{5}", reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5)));
                    }
                }
                
                string csvpath = "\\\\172.21.0.230\\importFiles\\importUsers\\metadados.csv";
                try
                {
                    FileInfo arquivo = new FileInfo(csvpath);
                    arquivo.Delete();
                    File.AppendAllText(csvpath, csvcontent.ToString());
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                    Console.Read();
                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message.ToString());
            }
        }
    }
}
