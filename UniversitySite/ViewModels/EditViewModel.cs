namespace UniversitySite.ViewModels
{
    public class EditViewModel
    {
        public StudentViewModel Student { get; set; }
        public ProfessorViewModel Professor { get; set; }

        public ChangePasswordViewModel ChangePasswordViewModel { get; set; } = new ChangePasswordViewModel();
    }
}