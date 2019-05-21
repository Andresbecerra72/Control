namespace Control.Web.Data.Entities
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser //hereda funciones del netcore
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Document { get; set; }

    }
}
