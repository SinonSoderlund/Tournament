using Service.Contracts.RequestObjects.ErrorSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts.RequestObjects.TournamentRequests
{
    public class TournamentPatchRequest : RequestWithValidation
    {
        public int Id { get; set; }
        public object PatchDocument { get; set; }

        public TournamentPatchRequest(IAPIErrorSystem errorSystem, Func<object, bool> validation, object patchDocument, int id) : base(errorSystem, validation)
        {
            Id = id;
            PatchDocument = patchDocument;
        }
    }
}
