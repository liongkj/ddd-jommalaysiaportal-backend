using System.Collections.Generic;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.PlaceUseCase.Create
{
    public class CreatePlaceResponse : UseCaseResponseMessage
    {
        public string Id { get; }
        public IEnumerable<string> Errors { get; }

        public CreatePlaceResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public CreatePlaceResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            Id = id;
        }
    }
}