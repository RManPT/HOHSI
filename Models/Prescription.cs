using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HOHSI.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }
        [DisplayName("Prescripton date")]
        [Required(ErrorMessage = "Enter the issued date.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateAndTime { get; set; }
        public virtual int PatientId { get; set; }
        [DisplayName("Prescriber")]
        public virtual int PrescriptorId { get; set; }
        public virtual ICollection<PrescriptedExercise> Exercises { get; set; }
    }
}
