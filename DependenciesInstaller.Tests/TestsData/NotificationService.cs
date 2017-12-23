using DependenciesInstaller.EntityLifeTimeAttributes;

namespace DependenciesInstaller.Tests.TestsData
{
    [Transient()]
    [Singletone()]
    public class NotificationService
    {
    }
}
