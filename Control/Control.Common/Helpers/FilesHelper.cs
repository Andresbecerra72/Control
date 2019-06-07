namespace Control.Common.Helpers
{
    using System.IO;

    public class FilesHelper //esta clase convierte un archivo en string para el envio de la foto
    {
        public static byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }

}
