using DependenciesInstaller.Entities;
using DependenciesInstaller.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DependenciesInstaller.Tests")]
namespace DependenciesInstaller
{
    internal static class Core
    {
        internal static string[] GetAssembliesPath()
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();
            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            return referencedPaths;
        }

        internal static Type[] GetAssemblyTypes(string path)
        {
            Assembly assembly = Assembly.LoadFrom(path);
            var typesInAssembly = assembly.GetTypes().Where(t => t.IsClass).Where(t => !t.IsAbstract).ToArray();
            return typesInAssembly;
        }

        internal static IEnumerable<RegisteredEntity> GetRegisteredEntity(Type[] types)
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

        internal static RegisteredEntity GetLifeTimeEntity(Type entityInterface, Type entityClass, Attribute entityAttribute)
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

       internal static void RegisterDependencies(IServiceCollection service, RegisteredEntity registeredEntity)
        {
            registeredEntity.SetLifeTime(service);
        }
    }
}
