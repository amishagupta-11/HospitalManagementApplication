namespace HospitalManagement.ViewModel
{
    public class IndividualTreatmentSuccessRateViewModel
    {
        public string? TreatmentName { get; set; }
        public int TotalTreatments { get; set; }
        public int SuccessfulTreatments { get; set; }
        public double SuccessRate { get; set; }
        public string? MedicalCondition { get; set; }
    }
}
