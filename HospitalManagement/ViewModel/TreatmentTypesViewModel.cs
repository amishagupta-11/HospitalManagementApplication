namespace HospitalManagement.ViewModel
{
    public class TreatmentTypesViewModel
    {
        public string? MedicalCondition {  get; set; }
        public string? TreatmentType { get; set; }
        public List<PatientInfo>? Patients { get; set; }
    }

    public class PatientInfo
    {
        public string? FirstName { get; set; }
        public int? Age { get; set; }
    }

}
