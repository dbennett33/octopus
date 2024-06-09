using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octopus.Importer.Services.Interfaces
{
    public interface IImportLeagueService
    {
        Task<bool> ImportLeagues();
    }
}
