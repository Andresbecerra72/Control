﻿namespace Control.Common.Models
{
    using Newtonsoft.Json;
    using System;

    public class User //esta clase es para ser usada en movil **contiene la conversion de json del postman a c#
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("document")]
        public long Document { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("normalizedUserName")]
        public string NormalizedUserName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("normalizedEmail")]
        public string NormalizedEmail { get; set; }

        [JsonProperty("emailConfirmed")]
        public bool EmailConfirmed { get; set; }

        [JsonProperty("passwordHash")]
        public string PasswordHash { get; set; }

        [JsonProperty("securityStamp")]
        public string SecurityStamp { get; set; }

        [JsonProperty("concurrencyStamp")]
        public Guid ConcurrencyStamp { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("phoneNumberConfirmed")]
        public bool PhoneNumberConfirmed { get; set; }

        [JsonProperty("twoFactorEnabled")]
        public bool TwoFactorEnabled { get; set; }

        [JsonProperty("lockoutEnd")]
        public object LockoutEnd { get; set; }

        [JsonProperty("lockoutEnabled")]
        public bool LockoutEnabled { get; set; }

        [JsonProperty("accessFailedCount")]
        public long AccessFailedCount { get; set; }
    }
}
