namespace Control.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    using System.Threading.Tasks;
    using Data;
    using MySql.Data.MySqlClient;
    using Newtonsoft.Json;

    public class Passanger
    {
        //private DataContext context;
        public int PassangerId { get; set; }

        [Required]
        [MaxLength(4)]
        public string Flight { get; set; }

        public int Adult { get; set; }

        public int Child { get; set; }

        public int Infant { get; set; }

        public int Total { get; set; }

        [Display(Name = "Fecha")]
        public DateTime PublishOn { get; set; }

        //[JsonIgnore]
        //public DataContext Db { get; set; }

        //public Passanger(DataContext db = null)
        //{
        //    Db = db;
        //}

        //public async Task InsertAsync()
        //{
        //    var cmd = Db.Connection.CreateCommand() as MySqlCommand;
        //    cmd.CommandText = @"INSERT INTO `Passangers` (`Flight`, `Adult`,`Child`, `Infant`,`Total`, `PublishOn`) VALUES (@Flight, @Adult, @Child, @Infant, @Total, @PublishOn);";
        //    BindParams(cmd);
        //    await cmd.ExecuteNonQueryAsync();
        //    PassangerId = (int)cmd.LastInsertedId;
        //}

        //public async Task UpdateAsync()
        //{
        //    var cmd = Db.Connection.CreateCommand() as MySqlCommand;
        //    cmd.CommandText = @"UPDATE `Passangers` SET `Flight` = @Flight, `Adult` = @Adult, `Child` = @Child, `Infant` = @Infant, `Total` = @Total, `PublishOn` = @PublishOn WHERE `PassangerId` = @PassangerId;";
        //    BindParams(cmd);
        //    BindId(cmd);
        //    await cmd.ExecuteNonQueryAsync();
        //}

        //public async Task DeleteAsync()
        //{
        //    var cmd = Db.Connection.CreateCommand() as MySqlCommand;
        //    cmd.CommandText = @"DELETE FROM `Passangers` WHERE `PassangerId` = @PassangerId;";
        //    BindId(cmd);
        //    await cmd.ExecuteNonQueryAsync();
        //}

        //private void BindId(MySqlCommand cmd)
        //{
        //    cmd.Parameters.Add(new MySqlParameter
        //    {
        //        ParameterName = "@PassangerId",
        //        DbType = DbType.Int32,
        //        Value = PassangerId,
        //    });
        //}

        //private void BindParams(MySqlCommand cmd)
        //{
        //    cmd.Parameters.Add(new MySqlParameter
        //    {
        //        ParameterName = "@Flight",
        //        DbType = DbType.String,
        //        Value = Flight,
        //    });
        //    cmd.Parameters.Add(new MySqlParameter
        //    {
        //        ParameterName = "@Adult",
        //        DbType = DbType.Int32,
        //        Value = Adult,
        //    });
        //    cmd.Parameters.Add(new MySqlParameter
        //    {
        //        ParameterName = "@Child",
        //        DbType = DbType.Int32,
        //        Value = Child,
        //    });
        //    cmd.Parameters.Add(new MySqlParameter
        //    {
        //        ParameterName = "@Infant",
        //        DbType = DbType.Int32,
        //        Value = Infant,
        //    });
        //    cmd.Parameters.Add(new MySqlParameter
        //    {
        //        ParameterName = "@Total",
        //        DbType = DbType.Int32,
        //        Value = Total,
        //    });
        //    cmd.Parameters.Add(new MySqlParameter
        //    {
        //        ParameterName = "@PublishOn",
        //        DbType = DbType.DateTime,
        //        Value = PublishOn,
        //    });
       // }



    }
}
