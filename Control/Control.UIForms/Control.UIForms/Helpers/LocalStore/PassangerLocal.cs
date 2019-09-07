namespace Control.UIForms.Helpers.LocalStore
{
    using SQLite;
    using System;

    

    [Table("PassangerLocal")]
    public class PassangerLocal
    {

        [PrimaryKey, AutoIncrement] 
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

        public byte[] ImageArray { get; set; }//atributo para almacenar la imagen capturada desde el movil

        public string PublishOnFormat
        {
            get
            {
                return string.Format("{0:dd/MM/yyyy}", PublishOn);
            }

        }


        public override string ToString()//Usado para el listview
        {
            return string.Format("{0} {1} {2} ", PublishOnFormat, Flight, Total);
        }
    }
}
