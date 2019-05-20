namespace Control.Web.Data
{
    using System;
    using System.Collections.Generic;
    using Control.Web.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using MySql.Data.MySqlClient;


    public class DataContext : DbContext
    {
        public DbSet<Passanger> Passangers { get; set; } //es una propiedad es para acceder al objeto pasajeros 
        public DataContext(DbContextOptions<DataContext> options) : base(options)//es la conexion a la base de datos
        {

        }



    }



}

//    public class DataContext : IDisposable
//    {
//        public MySqlConnection Connection;

//        public DataContext (string connectionString)
//        {
//            Connection = new MySqlConnection(connectionString);
//        }

//        public void Dispose()
//        {
//            Connection.Close();
//        }

//    }
//}

//public class DataContext 
//{
//    public string ConnectionString { get; set; }

//    public DataContext(string connectionString)
//    {
//        this.ConnectionString = connectionString;
//    }

//    private MySqlConnection GetConnection()
//    {
//        return new MySqlConnection(ConnectionString);
//    }

//    public List<Passanger> GetAllPassanger()
//    {
//        List<Passanger> list = new List<Passanger>();

//        using (MySqlConnection conn = GetConnection())
//        {
//            conn.Open();
//            MySqlCommand cmd = new MySqlCommand("select * from Passangers ", conn);

//            using (var reader = cmd.ExecuteReader())
//            {
//                while (reader.Read())
//                {
//                    list.Add(new Passanger()
//                    {
//                        PassangerId = Convert.ToInt32(reader["PassangerId"]),
//                        Flight = reader["Flight"].ToString(),
//                        Adult = Convert.ToInt32(reader["Adult"]),
//                        Child = Convert.ToInt32(reader["Child"]),
//                        Infant = Convert.ToInt32(reader["Infant"]),
//                        Total = Convert.ToInt32(reader["Total"]),
//                        PublishOn = Convert.ToDateTime(reader["PublishOn"])
//                    });
//                }
//            }
//        }
//        return list;
//    }



//}


//public class DataContext : DbContext
//{
//    public DbSet<Passanger> Passangers { get; set; } //es una propiedad es para acceder al objeto pasajeros 
//    public DataContext(DbContextOptions<DataContext> options) : base(options)//es la conexion a la base de datos
//    {

//    }



//}







