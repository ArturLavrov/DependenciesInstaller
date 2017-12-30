using SoftGene.DependencyInstaller.Attributes;
using SoftGene.DependencyInstaller.Sample.Repositories.Abstract;

namespace SoftGene.DependencyInstaller.Sample.Repositories.Concrete
{
    [BindTo(typeof(IDataContext))]
    public class SampleDataContext : IDataContext { }
}
