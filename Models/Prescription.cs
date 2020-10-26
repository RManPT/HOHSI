using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HOHSI.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }
        public DateTime Date { get; set; }
        public virtual int PatientId { get; set; }
        public virtual int PrescriptorId { get; set; }
        public virtual ICollection<PrescriptedExercise> Exercises { get; set; }
    }
}
