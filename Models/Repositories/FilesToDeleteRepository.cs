using HOHSI.Data;
using HOHSI.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HOHSI.Models.Repositories
{
    public class FilesToDeleteRepository : Repository<FilesToDelete>, IFilesToDeleteRepository
    {
        public FilesToDeleteRepository(HOHSIContext context) : base(context)
        {
        }
    }
}
