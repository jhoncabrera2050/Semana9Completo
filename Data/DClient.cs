﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

namespace Data
{
    public class DClient
    {

        public static string connectionString = "Data Source=DESKTOP-DKU7QGM\\SQLEXPRESS;Initial Catalog=Semana7;User ID=jhon;Password=123456";

        public  List<Client> ListarClientes()
        {
            List<Client> clientes = new List<Client>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Abrir la conexión
                connection.Open();
                string query = "usp_ListarClientes";

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Verificar si hay filas
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Leer los datos de cada fila
                                clientes.Add(new Client
                                {
                                    Id = (int)reader["customer_id"],
                                    Name = (string)reader["name"],
                                    Address = (string)reader["address"],
                                    Phone = (string)reader["phone"],
                                    Active = (bool)reader["active"]

                                });

                            }
                        }
                    }
                }

                // Cerrar la conexión
                connection.Close();


            }
            return clientes;

        }

        public void InsertClient(string name, string address, string phone)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("InsertClien", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.ExecuteNonQuery();
            }

        }

        public void ActivateClient(int id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                SqlCommand cmd = new SqlCommand("ActivateClient", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

            }

        }

        public void DeleteClient(int id) 
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                SqlCommand cmd = new SqlCommand("DeleteCliente", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

            }

        }

        public void UpdateClient(int id, string address, string phone)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                SqlCommand cmd = new SqlCommand("UpdateClient", connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.ExecuteNonQuery();  

            }

        }

    }

}

