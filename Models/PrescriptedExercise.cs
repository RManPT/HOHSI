using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOHSI.Models
{
    public class PrescriptedExercise
    {
        public int PrescriptionId { get; set; }
        public int ExerciseId { get; set; }
        public virtual Prescription Prescription { get; set; }
        public virtual Exercise Exercise { get; set; }
        public int Time { get; set; }
        public int Repetitions { get; set; }
        public string Instructions { get; set; }

    }
}

//        [Required(ErrorMessage = "{0} is a mandatory field")]
//        [Range(0, Double.PositiveInfinity, ErrorMessage = "The Age should be between 0 and 150 years")]
//        [Required(ErrorMessage = "{0} is a mandatory field")]
//        [MaxLength(500, ErrorMessage = "{0} cannot exceed {1} characters")]