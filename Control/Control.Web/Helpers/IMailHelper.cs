namespace Control.Web.Helpers
{
    public interface IMailHelper //interfase para enviar los correos de confirmacion de usuarios
    {
        void SendMail(string to, string subject, string body);
    }


}
