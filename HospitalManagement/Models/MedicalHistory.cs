using System;
using System.Collections.Generic;

namespace HospitalManagement.Models;

public partial class MedicalHistory
{
    public int HistoryId { get; set; }

    public int PatientId { get; set; }

    public string? MedicalCondition { get; set; } 

    public DateOnly DiagnosisDate { get; set; }

    public string? Treatment { get; set; } 

    public virtual Patient? Patient { get; set; }
}
