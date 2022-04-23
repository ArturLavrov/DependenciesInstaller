
# DependenciesInstaller             <img width="173" align="right" alt="image" src="https://user-images.githubusercontent.com/10619880/164889982-23cc7d84-776d-4431-a674-535d5d235706.png">

[![Build status](https://ci.appveyor.com/api/projects/status/10v1yxj93ey0j37x?svg=true)](https://ci.appveyor.com/project/ArturLavrov/dependenciesinstaller)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)


Lightweight, fast and convenient dependencies installer for ASP.NET Core  

Replace huge piles of similar code for dependencies registration with one single line.

## Before
```C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc(
        config => {
            config.Filters.Add(typeof(ExceptionFilter));
        }
    );

    services.AddDbContext<DataContext>(
        options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

    services.AddScoped<IDataContext, DataContext>();

    services.AddScoped<IRepositoryLanguage, RepositoryLanguage>();
    services.AddScoped<IRepositoryTransaction, RepositoryTransaction>();
    services.AddScoped<IRepositoryTransactionCategory, RepositoryTransactionCategory>();
    services.AddScoped<IRepositoryTransactionCategoryLcz, RepositoryTransactionCategoryLcz>();
    services.AddScoped<IRepositoryUserTransaction, RepositoryUserTransaction>();
    ...

    services.AddScoped<IDataAccessServiceTransaction, DataAccessServiceTransaction>();
    services.AddScoped<IDataAccessServiceTransactionCategory, DataAccessServiceTransactionCategory>();
    ...
}

```
## After
```C#
public void ConfigureServices(IServiceCollection services)
{
     services.AddMvc(
        config => {
            config.Filters.Add(typeof(ExceptionFilter));
        }
    );

    services.AddDbContext<DataContext>(
        options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);


    services.RunDependencyInstaller();
}
```


## Getting Started

1) Install project from Nuget:
```
Install-Package DependencesInstaller 
```
2) Inside ConfigureServices method call extension method **RunDependencyInstaller**
```
public void ConfigureServices(IServiceCollection services)
{
    ...
    
    services.RunDependencyInstaller();
    
    ...
}
```

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

## Authors

* **Artur Lavrov** 
* **Nikita Litvinenko**

### Inspired by 
[IoC-manager-net](https://github.com/Mart-Bogdan/IoC-manager-net) (by Bogdan Mart)

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
