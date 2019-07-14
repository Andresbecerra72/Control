namespace Control.Common.Models
{
    using Newtonsoft.Json;
    using System;

    public class Passanger //esta clase es para ser usada en movil **contiene la conversion de json del postman a c#
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("flight")]
        public string Flight { get; set; }

        [JsonProperty("adult")]
        public int Adult { get; set; }

        [JsonProperty("child")]
        public int Child { get; set; }

        [JsonProperty("infant")]
        public int Infant { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("publishOn")]
        public DateTime PublishOn { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("remark")]
        public string Remark { get; set; }

        [JsonProperty("day")]
        public string Day { get; set; }

        [JsonProperty("month")]
        public string Month { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }

        [JsonProperty("imageFullPath")]
        public string ImageFullPath { get; set; }

        public byte[] ImageArray { get; set; }//atributo para almacenar la imagen capturada desde el movil

        public override string ToString()//codigo para el listview de app movil forms
        {
            return $"{this.Flight}{this.Total}";
        }
    }

}
