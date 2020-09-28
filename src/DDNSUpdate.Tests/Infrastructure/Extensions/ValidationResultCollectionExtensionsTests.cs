using DDNSUpdate.Application.Configuration;
using DDNSUpdate.Infrastructure.Extensions;
using DDNSUpdate.Tests.Helpers;
using FluentResults;
using FluentValidation.Results;
using System.Collections.Generic;
using Xunit;

namespace DDNSUpdate.Tests.Infrastructure.Extensions
{
    public class ValidationResultCollectionExtensionsTests : TestBase
    {
        [Fact]
        public void Failure_Result_To_Result_Returns_Fail()
        {
            const string errorMessageA = "ErrorA";
            const string errorMessageB = "ErrorB";

            ValidationResultCollection result = new ValidationResultCollection(new ValidationResult[]
            {
                new ValidationResult(),
                new ValidationResult(new List<ValidationFailure>() { new ValidationFailure("TestA", errorMessageA)}),
                new ValidationResult(new List<ValidationFailure>() { new ValidationFailure("TestB", errorMessageB)}),
                new ValidationResult()
            });
            Result results = result.ToResults();
            Assert.True(results.IsFailed);
            Assert.Contains(results.Errors, r => r.Message == errorMessageA);
            Assert.Contains(results.Errors, r => r.Message == errorMessageB);
        }

        [Fact]
        public void Valid_Result_To_Result_Returns_Success()
        {
            ValidationResultCollection result = new ValidationResultCollection(new ValidationResult[] { new ValidationResult(), new ValidationResult() });
            Assert.True(result.ToResults().IsSuccess);
        }
    }
}
