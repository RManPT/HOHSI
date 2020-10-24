using HOHSI.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOHSI.Models.Repositories
{
    public class PrescriptionRepository : Repository<Prescription>, IPrescriptionRepository
    {
        public IEnumerable<Prescription> GetAllWithExercises()
        {
            throw new NotImplementedException();
        }

        public Prescription GetWithExercises(int id)
        {
            throw new NotImplementedException();
        }
    }
}
