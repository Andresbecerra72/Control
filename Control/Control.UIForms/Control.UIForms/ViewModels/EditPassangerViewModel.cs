using Control.Common.Models;

namespace Control.UIForms.ViewModels
{
    public class EditPassangerViewModel
    {
        public Passanger Passanger { get; set; }

        public EditPassangerViewModel(Passanger passanger)
        {
            this.Passanger = passanger;
        }
    }



}
