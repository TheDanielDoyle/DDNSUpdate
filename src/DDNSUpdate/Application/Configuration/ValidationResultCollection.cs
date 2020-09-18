using FluentValidation.Results;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DDNSUpdate.Application.Configuration
{
    public class ValidationResultCollection : ReadOnlyCollection<ValidationResult>
    {
        public IEnumerable<string> ErrorMessages
        {
            get { return this.Errors.SelectMany(e => e.Errors.Select(x => x.ErrorMessage)); }
        }

        public ValidationResultCollection Errors
        {
            get { return new ValidationResultCollection(this.Where(v => !v.IsValid)); }
        }

        public bool IsValid 
        { 
            get { return this.All(v => v.IsValid); } 
        }

        public ValidationResultCollection(IEnumerable<ValidationResult> results) : base(results.ToList())
        {
        }
    }
}
