using SoftGene.DependencyInstaller.Attributes;

namespace SoftGene.DependencyInstaller.Tests.TestsData
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
