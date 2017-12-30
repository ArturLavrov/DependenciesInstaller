using Microsoft.Extensions.DependencyInjection;
using SoftGene.DependencyInstaller.Attributes;
using SoftGene.DependencyInstaller.Entities;
using SoftGene.DependencyInstaller.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SoftGene.DependencyInstaller.Tests")]
namespace SoftGene.DependencyInstaller
{
    internal static class Core
    {
        internal static readonly Guid EntityLifetimeAttributeTypeId = 
            new Guid("C3730612-B077-4B0D-A7DD-A478D6DEE990");
        internal static readonly Guid BindToAttributeTypeId =
            new Guid("C3730612-B155-4B0D-A7DD-A478D6DEE881");

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
            var filteredTypes = types.Where(type => type.GetInterfaces().Any() &&
                                                    type.GetCustomAttribute<BindToAttribute>() == null)
                                  .Select(type => new BindablePair()
                                  {
                                      EntityInterface = type.GetInterfaces()
                                                  .Where(i => i.Name == "I" + type.Name).FirstOrDefault(),
                                      EntityClass = type
                                  })
                                 .Where(type => type.EntityInterface != null);

            var typesWithBindTo = types.Where(type => type.GetCustomAttribute<BindToAttribute>() != null)
                                  .Select(type => new BindablePair()
                                  {
                                      EntityClass = type,
                                      EntityInterface = type.GetCustomAttribute<BindToAttribute>().InterfaceType
                                  })
                                  .Where(type => type.EntityInterface != null);

            foreach (var type in filteredTypes.Union(typesWithBindTo))
            {
                var typeLifeTimeattribute = type.EntityClass.GetEntityLifeTimeAttribute();
                yield return GetLifeTimeEntity(type.EntityInterface, type.EntityClass, typeLifeTimeattribute);
            }
        }

        internal static RegisteredEntity GetLifeTimeEntity(Type entityInterface, Type entityClass, Attribute entityAttribute)
        {
            Dictionary<string, RegisteredEntity> dic = new Dictionary<string, RegisteredEntity>()
            {
                { "Scoped" , new ScopedEntity(){Interface = entityInterface,Class = entityClass,LifeTime = entityAttribute }},
                { "Transient", new TransientEntity(){Interface = entityInterface,Class = entityClass,LifeTime = entityAttribute }},
                { "Singletone", new SingletonEntity(){Interface = entityInterface,Class = entityClass,LifeTime = entityAttribute }}
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
