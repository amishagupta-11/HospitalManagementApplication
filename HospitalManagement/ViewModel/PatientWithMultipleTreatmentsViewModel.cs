using HospitalManagement.Models;

namespace HospitalManagement.ViewModel
{
    public class PatientWithMultipleTreatmentsViewModel
    {
        public Patient? Patient { get; set; }
        public ICollection<TreatmentRecord>? Treatments { get; set; }
    }
}
