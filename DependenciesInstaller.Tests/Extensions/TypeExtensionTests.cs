using DependenciesInstaller.Extensions;
using DependenciesInstaller.Tests.TestsData;
using Xunit;

namespace DependenciesInstaller.Tests.Extensions
{
    public class TypeExtensionTests
    {
        [Fact]
        public void GetEntityLifeTimeAttribute_PassType_ReturnAttribute()
        {
            var typeUnderTest = typeof(RepositoryArticle);
            var resultObj = typeUnderTest.GetEntityLifeTimeAttribute();
            Assert.True(resultObj.ToString() == "Scoped");
        }
    }
}
