namespace Control.Common.Models
{
    using Newtonsoft.Json;

    public class City //clase ciudad para deserializar con los datos del api controller del proyecto web
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

}
