using DependenciesInstaller.Entities;
using DependenciesInstaller.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DependenciesInstaller
{
    public static class Installer
    {
        public static readonly Guid AttributeTypeId = new Guid("C3730612-B077-4B0D-A7DD-A478D6DEE990");

        public static void RunDependenciesInstaller(this IServiceCollection serviceCollection)
        {
            var assembliesPaths = GetAssembliesPath();
            foreach (var path in assembliesPaths)
            {
                var types = GetAssemblyTypes(path);
                var entitiesToRegister = GetRegisteredEntity(types);
                foreach (var entity in entitiesToRegister)
                {
                    RegisterDependencies(serviceCollection, entity);
                }
            }
        }

        private static string[] GetAssembliesPath()
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();
            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            return referencedPaths;
        }

        private static Type[] GetAssemblyTypes(string path)
        {
            Assembly assembly = Assembly.LoadFrom(path);
            var typesInAssembly = assembly.GetTypes().Where(t => t.IsClass).Where(t => !t.IsAbstract).ToArray();
            return typesInAssembly;
        }

        private static IEnumerable<RegisteredEntity> GetRegisteredEntity(Type[] types)
        {
            foreach (var type in types)
            {
                var typeInterfaces = type.GetInterfaces();
                if (typeInterfaces.Any())
                {
                    var inte = typeInterfaces.Where(t => t.Name == "I" + type.Name).FirstOrDefault();
                    if (inte != null)
                    {
                        var typeLifeTimeattribute = type.GetEntityLifeTimeAttribute();
                        yield return GetLifeTimeEntity(inte, type, typeLifeTimeattribute);
                    }
                }
            }
        }
        private static RegisteredEntity GetLifeTimeEntity(Type entityInterface, Type entityClass, Attribute entityAttribute)
        {
            Dictionary<string, RegisteredEntity> dic = new Dictionary<string, RegisteredEntity>()
            {
                { "Scoped" , new ScopedEntity(){Interface = entityInterface,Class = entityClass,LifeTime = entityAttribute }},
                { "Transient", new TransientEntity(){Interface = entityInterface,Class = entityClass,LifeTime = entityAttribute }},
                { "Singletone", new SingletoneEntity(){Interface = entityInterface,Class = entityClass,LifeTime = entityAttribute }}
            };

            RegisteredEntity registeredEntity;
            dic.TryGetValue(entityAttribute.ToString(), out registeredEntity);
            return registeredEntity;
        }

        private static void RegisterDependencies(IServiceCollection service, RegisteredEntity registeredEntity)
        {
            registeredEntity.SetLifeTime(service);
        }
    }
}
