using DependenciesInstaller.Entities;
using DependenciesInstaller.EntityLifeTimeAttributes;
using DependenciesInstaller.Tests.TestsData;
using System;
using Xunit;
namespace DependenciesInstaller.Tests
{
    public class CoreTests : IDisposable
    {
        public CoreTests()
        {

        }
        [Fact]
        public void GetLifeTimeEntity_PassScopedEntityParams_ReturnScopedEntity()
        {
            var registeredEntityInterface = typeof(IRepositoryArticle);
            var registeredEntityClass = typeof(RepositoryArticle);
            var registeredEntityLifeTimeAttribute = new ScopedAttribute();

            ScopedEntity expectedEntity = new ScopedEntity()
            {
                Class = registeredEntityClass,
                Interface = registeredEntityInterface,
                LifeTime = registeredEntityLifeTimeAttribute,
            };
            //Act
            var resultObj = Core.GetLifeTimeEntity(registeredEntityInterface, registeredEntityClass, registeredEntityLifeTimeAttribute);

            //Assert
            Assert.True(expectedEntity.Class == resultObj.Class);
        }

        public void Dispose()
        {

        }
    }
}
