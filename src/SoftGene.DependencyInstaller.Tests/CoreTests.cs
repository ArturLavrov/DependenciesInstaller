using SoftGene.DependencyInstaller.Entities;
using SoftGene.DependencyInstaller.Attributes;
using SoftGene.DependencyInstaller.Tests.TestsData;
using System;
using Xunit;

namespace SoftGene.DependencyInstaller.Tests
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
            string path = AppDomain.CurrentDomain.BaseDirectory + @"\SoftGene.DependencyInstaller.Tests.dll";
            var typesInCurrentAssembly = Core.GetAssemblyTypes(path);
            Assert.NotNull(typesInCurrentAssembly);
            foreach (var type in typesInCurrentAssembly)
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
            Type[] arrayTypes = { repositoryClass };
            ScopedEntity expectedObj = new ScopedEntity()
            {
                Class = repositoryClass,
                Interface = iRepositoryArticle,
                LifeTime = new TransientAttribute(),
            };

            var resultEnumeration = Core.GetRegisteredEntity(arrayTypes);

            foreach (var obj in resultEnumeration)
            {
                Assert.NotNull(resultEnumeration);
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
