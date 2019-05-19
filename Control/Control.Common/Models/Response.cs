namespace Control.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Response  //este codigo permite comprobar las acciones y el funcionamiento del API
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }
    }
}
