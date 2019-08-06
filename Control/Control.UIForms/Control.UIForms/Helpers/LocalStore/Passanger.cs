namespace Control.UIForms.Helpers.LocalStore
{
    using System;
    using Control.Common.Models;
    using SQLite;
   

    public class Passanger
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

       
        public DateTime PublishOn { get; set; }

        
        public string Flight { get; set; }

        public int Adult { get; set; }

        public int Child { get; set; }

        public int Infant { get; set; }

        public int Total { get; set; }

      
        public string Remark { get; set; }

        public string Day { get; set; }

        public string Month { get; set; }

        public string Year { get; set; }

       
        public string ImageUrl { get; set; }

        public User User { get; set; }//relacion de usuarios con los datos reportados


        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", PublishOn, Flight, Adult, Child, Infant, Total);
        }
    }
}
