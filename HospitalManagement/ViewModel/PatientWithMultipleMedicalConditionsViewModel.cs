using HospitalManagement.Models;

namespace HospitalManagement.ViewModel
{
    public class PatientWithMultipleMedicalConditionsViewModel
    {
        public Patient? Patient { get; set; }
        public ICollection<MedicalHistory>? Conditions { get; set; }
    }
}
