namespace Control.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class KiuReport : IEntity
    {

        public int Id { get; set; }

        public string Hour { get; set; }

        public string Vuelo { get; set; }

        public string Origen_Itinerario { get; set; }

        [Display(Name = "Desde")]
        public string Source { get; set; }

        [Display(Name = "Hacia")]
        public string Dest { get; set; }

        [Display(Name = "Aeronave")]
        public string Equipo { get; set; }

        public string Matricula { get; set; }

        public string Delay { get; set; }

        public string Pais_emision { get; set; }

        public string Emisor { get; set; }

        [Display(Name = "Usuario KIU")]
        public string Agente { get; set; }

        public string Fecha_emision { get; set; }

        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}", ApplyFormatInEditMode = true)]
        public string Fecha_vuelo_real { get; set; }

        public string Fecha_vuelo_programada { get; set; }

        public string Foid { get; set; }

        public string Nrotkt { get; set; }

        public string Fim { get; set; }

        public string Cupon { get; set; }

        [Display(Name = "Pax")]
        public string Tpax { get; set; }

        [Display(Name = "Nombre Pasajero")]
        public string Pax { get; set; }

        public string Contact_pax { get; set; }

        public string Class { get; set; }

        public string Fbasis { get; set; }

        public string Tour_code { get; set; }

        public string Moneda { get; set; }

        public string Importe { get; set; }

        public string Record_locator { get; set; }

        public string Carrier { get; set; }

        public string Monlocal { get; set; }

        public string Implocal { get; set; }

        public string Endoso { get; set; }

        public string Info_adicional_fc { get; set; }

        public string Sac { get; set; }




    }
}
