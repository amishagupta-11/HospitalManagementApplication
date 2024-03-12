using HospitalManagement.Models;
using HospitalManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;


namespace HospitalManagement.Controllers
{
    public class HospitalManagementController : Controller
    {
        
        private readonly HospitalManagementContext dbContext = new HospitalManagementContext();

        /// <summary>
        /// Action Method to find the average age distribution based on a particular medical condition
        /// </summary>
        /// <returns></returns>
        public ActionResult AverageAgeByGenderAndCondition()
        {

            var query = from patient in dbContext.Patients
                        join medicalHistory in dbContext.MedicalHistories on patient.PatientId equals medicalHistory.PatientId
                        where medicalHistory.MedicalCondition == "Cancer"
                        group patient by new { patient.Gender, Condition = medicalHistory.MedicalCondition } into genderGroup
                        select new GenderConditionViewModel
                        {
                            Gender = genderGroup.Key.Gender,
                            Condition = genderGroup.Key.Condition,
                            AverageAge = genderGroup.Average(patient => patient.Age)
                        };

            return View(query.ToList());

        }

        /// <summary>
        /// Action method to show patient details on undergoing different types of treatments for a particular medical condition
        /// </summary>
        /// <returns></returns>

        public ActionResult TreatmentTypesForPatients()
        {
            var query = from patient in dbContext.Patients
                        join medicalHistory in dbContext.MedicalHistories on patient.PatientId equals medicalHistory.PatientId
                        where medicalHistory.MedicalCondition=="Cancer"
                        join treatmentRecord in dbContext.TreatmentRecords on patient.PatientId equals treatmentRecord.PatientId
                        group new { patient, treatmentRecord } by treatmentRecord.TreatmentType into treatmentGroup
                        select new TreatmentTypesViewModel
                        {                   
                            MedicalCondition="Cancer",
                            TreatmentType = treatmentGroup.Key,
                            Patients = treatmentGroup.SelectMany(entry => entry.patient.TreatmentRecords
                                                 .Select(treatmentRecord => new PatientInfo
                                                 {
                                                     FirstName = entry.patient.FirstName,
                                                     Age = entry.patient.Age
                                                 })).Distinct().ToList()
                        };

            return View(query.ToList());
        }

        /// <summary>
        /// Action method to show patient details who are undergoing combination of treatments.
        /// </summary>
        /// <returns></returns>

        public ActionResult PatientsWithMultipleTreatments()
        {

            var query = from patient in dbContext.Patients
                        where patient.TreatmentRecords.Count()>0
                        from treatment in patient.TreatmentRecords
                        group treatment by patient into patientGroup
                        select new PatientWithMultipleTreatmentsViewModel
                        {
                            Patient = patientGroup.Key,
                            Treatments = patientGroup.Distinct().ToList()
                        };

            var result = query.ToList();

            return View(result);

        }

        /// <summary>
        /// Method to display patient details who  are suffering from more than 1 kind of medical condition.
        /// </summary>
        /// <returns></returns>

        public ActionResult PatientsWithMultipleMedicalConditions()
        {

            var query = from patient in dbContext.Patients
                        where patient.MedicalHistories.Count() >= 1
                        select new PatientWithMultipleMedicalConditionsViewModel
                        {
                            Patient = patient,
                            Conditions = patient.MedicalHistories.Distinct().ToList()
                        };
            var result = query.ToList();

            return View(result);

        }

        /// <summary>
        /// Method to display patients whose treatment was unsuccessful.
        /// </summary>
        /// <returns></returns>
        public ActionResult UnsuccessfulTreatments()
        {
            var unsuccessfulPatients = from patient in dbContext.Patients
                                       join medicalConditions in dbContext.MedicalHistories on patient.PatientId equals medicalConditions.PatientId
                                       join treatment in dbContext.TreatmentRecords on patient.PatientId equals treatment.PatientId
                                       where treatment.Outcome.Equals("Failed")
                                       select new PatientDetailsViewModel
                                       {
                                           PatientId=patient.PatientId,
                                           FirstName = patient.FirstName,
                                           Age = patient.Age,
                                           Address = patient.Address,
                                           MedicalCondition = medicalConditions.MedicalCondition,
                                           UnsuccessfulTreatmentDetails = treatment.TreatmentType,
                                       };

            return View(unsuccessfulPatients.Distinct().ToList());

        }

        /// <summary>
        /// Method to display success rate for a individualtreatment associated with a medical condition
        /// </summary>
        /// <returns></returns>
        public IActionResult GetIndividualTreatmentSuccessRates()
        {
            var treatmentSuccessRates = (from patient in dbContext.Patients
                                         join medicalHistory in dbContext.MedicalHistories on patient.PatientId equals medicalHistory.PatientId
                                         join treatmentRecord in dbContext.TreatmentRecords on patient.PatientId equals treatmentRecord.PatientId
                                         where medicalHistory.MedicalCondition == "Cancer"
                                         group treatmentRecord by treatmentRecord.TreatmentType into treatmentGroup
                                         select new IndividualTreatmentSuccessRateViewModel
                                         {
                                             TreatmentName = treatmentGroup.Key,
                                             TotalTreatments = treatmentGroup.Count(),
                                             SuccessfulTreatments = treatmentGroup.Count(t => t.Outcome.Equals("Successful")),
                                             SuccessRate = (double)treatmentGroup.Count(t => t.Outcome.Equals("Successful")) / treatmentGroup.Count(),
                                             MedicalCondition = "Cancer"
                                         }).ToList();

            return View(treatmentSuccessRates);
        }


    }

}











