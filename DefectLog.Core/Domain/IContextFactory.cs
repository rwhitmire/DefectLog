using System.Data.Entity;

namespace DefectLog.Core.Domain
{
    public interface IContextFactory
    {
        DbContext GetContext();
    }
}