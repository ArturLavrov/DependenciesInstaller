using SoftGene.DependencyInstaller.Exceptions;
using SoftGene.DependencyInstaller.Extensions;
using SoftGene.DependencyInstaller.Tests.TestsData;
using System;
using Xunit;

namespace DependenciesInstaller.Tests.Extensions
{
    public class TypeExtensionTests
    {
        [Fact, Trait("Category", "Unit")]
        public void GetEntityLifeTimeAttribute_PassType_ReturnAttribute()
        {
            var typeUnderTest = typeof(RepositoryArticle);
            var resultObj = typeUnderTest.GetEntityLifeTimeAttribute();
            Assert.True(resultObj.ToString() == "Transient");
        }
        [Fact, Trait("Category", "Unit")]
        public void GetEntityLifeTimeAttribute_TypeWithSeveralLifeTimeAttributes_ThrowException()
        {
            var typeUnderTest = typeof(NotificationService);
            
            Exception ex = Assert.Throws<MultipleLifetimeAttributeException>(() => typeUnderTest.GetEntityLifeTimeAttribute());
            Assert.Equal("Can't resolve Dependencies. Class contains more that two LifeTime attributes.", ex.Message);
        }
    }
}
