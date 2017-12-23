using DependenciesInstaller.EntityLifeTimeAttributes;

namespace DependenciesInstaller.Tests.TestsData
{
    [Transient()]
    internal class RepositoryArticle : IRepositoryArticle
    {
        public string GetArticle()
        {
            return "article 1";
        }
    }
}
