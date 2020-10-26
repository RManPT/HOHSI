using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HOHSI.Models
{
    public class Exercise
    {
        [Key]
        public int ExerciseId{ get; set; }
        [Required(ErrorMessage = "{0} is a mandatory field")]
        [MaxLength(20, ErrorMessage= "{0} cannot exceed 20 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} is a mandatory field")]
        [MaxLength(250, ErrorMessage = "{0} cannot exceed {1} characters")]
        public string Description { get; set; }
        [DisplayName("Image file")]
        public string ImageName { get; set; }
        public virtual ICollection<PrescriptedExercise> Exercises { get; set; }
    }
}
