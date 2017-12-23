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
        internal static readonly Guid AttributeTypeId = new Guid("C3730612-B077-4B0D-A7DD-A478D6DEE990");

        internal static string[] GetAssembliesPath()
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();
            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            return referencedPaths;
        }

        internal static List<Type> GetAssemblyTypes(string path)
        {
            Assembly assembly = Assembly.LoadFrom(path);
            var typesInAssembly = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract).ToList();
            return typesInAssembly;
        }

        internal static IEnumerable<RegisteredEntity> GetRegisteredEntity(IEnumerable<Type> types)
        {
            var filteredTypes = types.Where(type => type.GetInterfaces().Any())
                                  .Select(type => new
                                  {
                                      entityInterface = type.GetInterfaces()
                                                  .Where(i => i.Name == "I" + type.Name).FirstOrDefault(),
                                      entityClass = type
                                  })
                                 .Where(type => type.entityInterface != null);
            
            foreach (var type in filteredTypes)
            {
               var typeLifeTimeattribute = type.entityClass.GetEntityLifeTimeAttribute();
               yield return GetLifeTimeEntity(type.entityInterface, type.entityClass, typeLifeTimeattribute);
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
