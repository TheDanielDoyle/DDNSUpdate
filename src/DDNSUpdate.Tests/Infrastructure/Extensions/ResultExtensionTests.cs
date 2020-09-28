using DDNSUpdate.Infrastructure.Extensions;
using DDNSUpdate.Tests.Helpers;
using FluentResults;
using Xunit;

namespace DDNSUpdate.Tests.Infrastructure.Extensions
{
    public class ResultExtensionTests : TestBase
    {
        [Fact]
        public void Fail_Results_Are_Merged()
        {
            const string failMessageA = "failMessageA";
            const string failMessageB = "failMessageB";

            Result resultA = Result.Fail(failMessageA);
            Result resultB = Result.Fail(failMessageB);

            Result resultC = resultA.Merge(resultB);

            Assert.True(resultC.IsFailed);
            Assert.Contains(resultC.Errors, s => s.Message == failMessageA);
            Assert.Contains(resultC.Errors, s => s.Message == failMessageB);
        }

        [Fact]
        public void Mixed_Results_Are_Merged()
        {
            const string successMessageA = "successMessageA";
            const string failMessageB = "failMessageB";
            const string successMessageC = "successMessageC";

            Result resultA = Result.Ok().WithSuccess(successMessageA);
            Result resultB = Result.Fail(failMessageB);
            Result resultC = Result.Ok().WithSuccess(successMessageC);

            Result resultD = resultA.Merge(resultB).Merge(resultC);

            Assert.True(resultD.IsFailed);
            Assert.Contains(resultD.Successes, s => s.Message == successMessageA);
            Assert.Contains(resultD.Errors, s => s.Message == failMessageB);
            Assert.Contains(resultD.Successes, s => s.Message == successMessageC);
        }

        [Fact]
        public void Success_Results_Are_Merged()
        {
            const string successMessageA = "successMessageA";
            const string successMessageB = "successMessageB";

            Result resultA = Result.Ok().WithSuccess(successMessageA);
            Result resultB = Result.Ok().WithSuccess(successMessageB);

            Result resultC = resultA.Merge(resultB);

            Assert.True(resultC.IsSuccess);
            Assert.Contains(resultC.Successes, s => s.Message == successMessageA);
            Assert.Contains(resultC.Successes, s => s.Message == successMessageB);
        }
    }
}
