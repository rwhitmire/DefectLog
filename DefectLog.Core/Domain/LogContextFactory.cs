using System.Data.Entity;

namespace DefectLog.Core.Domain
{
    public class LogContextFactory : IContextFactory
    {
        private readonly LogContext _context;

        public LogContextFactory()
        {
            _context = new LogContext();
        }

        public DbContext GetContext()
        {
            return _context;
        }
    }
}