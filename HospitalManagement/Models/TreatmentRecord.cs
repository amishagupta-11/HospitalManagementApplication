using System;
using System.Collections.Generic;

namespace HospitalManagement.Models;

public partial class TreatmentRecord
{
    public int RecordId { get; set; }

    public int PatientId { get; set; }

    public string? TreatmentType { get; set; } 

    public DateOnly TreatmentDate { get; set; }

    public string? Outcome { get; set; } 

    public virtual Patient? Patient { get; set; } 
}
