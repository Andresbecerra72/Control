namespace Control.Web.Helpers
{
    using System.IO;

    public class FilesHelper //clase para la conversion del string en un archivo de la imagen tomada desde el movil
    {
        public static bool UploadPhoto(MemoryStream stream, string folder, string name)
        {
            try
            {
                stream.Position = 0;
                var path = Path.Combine(Directory.GetCurrentDirectory(), folder, name);
                File.WriteAllBytes(path, stream.ToArray());
            }
            catch
            {
                return false;
            }

            return true;
        }
    }

}
