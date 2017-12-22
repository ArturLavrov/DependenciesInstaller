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
            Assert.True(expectedEntity.Interface == resultObj.Interface);
            Assert.True(expectedEntity.LifeTime.ToString() == resultObj.LifeTime.ToString());
        }
        [Fact]
        public void GetAssemblyTypes_PassAssemblyPathAsString_ReturnFilteredTypes()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\DependenciesInstaller.Tests.dll";
            Type[] typesInCurrentAssembly = Core.GetAssemblyTypes(path);
            foreach(var type in typesInCurrentAssembly)
            {
                Assert.False(type.IsAbstract);
                Assert.False(type.IsInterface);
                Assert.True(type.IsClass);
            }
        }
        [Fact]
        public void GetRegisteredEntity_PassTypesArray_ReturnIEnumerableOfRegisteredEntities()
        {
            var repositoryClass = typeof(RepositoryArticle);
            var iRepositoryArticle = typeof(IRepositoryArticle);
            Type[] arrayTypes = {repositoryClass};
            ScopedEntity expectedObj = new ScopedEntity()
            {
                Class = repositoryClass,
                Interface = iRepositoryArticle,
                LifeTime = new ScopedAttribute(),
            };

            var resultEnumeration = Core.GetRegisteredEntity(arrayTypes);
            foreach(var obj in resultEnumeration)
            {
                Assert.True(expectedObj.Class == obj.Class);
                Assert.True(expectedObj.Interface == obj.Interface);
                Assert.True(expectedObj.LifeTime.ToString() == obj.LifeTime.ToString());
            }

            
        }
        public void Dispose()
        {
            
        }
    }
}
