using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOHSI.Models.Interfaces
{
    public interface IPrescriptionRepository : IAsyncRepository<Prescription>
    {
        IEnumerable<Prescription> GetAllWithExercises();
        Prescription GetWithExercises(int id);
    }
}
