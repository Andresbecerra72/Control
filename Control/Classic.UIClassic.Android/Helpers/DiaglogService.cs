namespace Classic.UIClassic.Android.Helpers
{
    using global::Android.App;
    using global::Android.Content;

    public static class DiaglogService //esta clase permite los mensajes de error cuando se realizan validaciones
    {
        public static void ShowMessage(Context context, string title, string message, string button)
        {
            new AlertDialog.Builder(context)
                .SetPositiveButton(button, (sent, args) => { })
                .SetMessage(message)
                .SetTitle(title)
                .SetCancelable(false)
                .Show();
        }
    }

}