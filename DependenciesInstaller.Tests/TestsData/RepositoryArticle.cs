using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependenciesInstaller.Tests.TestsData
{
    internal class RepositoryArticle : IRepositoryArticle
    {
        public string GetArticle()
        {
            return "article 1";
        }
    }
}
