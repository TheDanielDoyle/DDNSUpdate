using DDNSUpdate.Application.Configuration;
using DDNSUpdate.Tests.Helpers;
using FluentValidation.Results;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace DDNSUpdate.Tests.Application.Configuration
{
    public class ValidationResultCollectionTests : TestBase
    {
        private readonly ValidationResult _validResult = new();

        [Theory]
        [ClassData(typeof(InvalidValidationResultCollections))]
        public void InvalidResult(ValidationResultCollection results)
        {
            Assert.False(results.IsValid);
            Assert.Single(results.Errors);
            Assert.Single(results.ErrorMessages);
        }

        [Fact]
        public void ValidResult()
        {
            ValidationResultCollection results = new(new[] { _validResult });

            Assert.True(results.IsValid);
            Assert.Empty(results.Errors);
            Assert.Empty(results.ErrorMessages);
        }

        private class InvalidValidationResultCollections : IEnumerable<object[]>
        {
            private ValidationResult _invalidResult = new(new[] { new ValidationFailure("Property", "Error Message") });
            private ValidationResult _validResult = new();

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new ValidationResultCollection(new[] { _invalidResult }) };
                yield return new object[] { new ValidationResultCollection(new[] { _invalidResult, _validResult }) };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
