namespace HospitalManagement.ViewModel
{
    public class PatientDetailsViewModel
    {
        public int PatientId { get; set; }
        public string? FirstName { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? MedicalCondition { get; set; }
        public string? UnsuccessfulTreatmentDetails { get; set; }
    }
}
