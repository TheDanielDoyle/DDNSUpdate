using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FluentValidation.Results;

namespace DDNSUpdate.Infrastructure.Settings;

internal sealed class ValidationResults : ReadOnlyCollection<ValidationResult>
{
    public ValidationResults(IEnumerable<ValidationResult> validationResults) : this(validationResults.ToList())
    {
    }
    
    public ValidationResults(IList<ValidationResult> validationResults) : base(validationResults)
    {
    }

    public IEnumerable<ValidationFailure> Errors()
    {
        return this.SelectMany(x => x.Errors);
    }

    public bool IsValid()
    {
        return this.All(v => v.IsValid);
    }
}