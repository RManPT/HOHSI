using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HOHSI.Models
{
    public class FilesToDelete
    {
        [Key]
        public int fileId { get; set; }
        public string filePath { get; set; }
    }
}
