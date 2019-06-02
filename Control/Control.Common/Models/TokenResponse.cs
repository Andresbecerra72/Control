namespace Control.Common.Models
{
    using System;
    using Newtonsoft.Json;

    public class TokenResponse //clase transversal para login del App con token
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expiration")]
        public DateTime Expiration { get; set; }
    }

}
