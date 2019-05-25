namespace Control.Common.Models
{
    using Newtonsoft.Json;
    using System;

    public class Passanger //esta clase es para ser usada en movil **contiene la conversion de json del postman a c#
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("flight")]
        public long Flight { get; set; }

        [JsonProperty("adult")]
        public long Adult { get; set; }

        [JsonProperty("child")]
        public long Child { get; set; }

        [JsonProperty("infant")]
        public long Infant { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }

        [JsonProperty("publishOn")]
        public DateTimeOffset PublishOn { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("imageFullPath")]
        public Uri ImageFullPath { get; set; }
    }

}
