using HOHSI.Data;
using HOHSI.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOHSI.Models.Repositories
{
    public class PrescriptionRepository : Repository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(HOHSIContext context) : base(context)
        {
        }

        public IEnumerable<Prescription> GetAllWithExercises()
        {
            return _context.Prescriptions.Include(p => p.Exercises);
        }

        public Prescription GetWithExercises(int id)
        {
            return _context.Prescriptions
                .Where(p => p.PrescriptionId == id)
                .Include(p => p.Exercises)
                .FirstOrDefault();
        }

    }
}
