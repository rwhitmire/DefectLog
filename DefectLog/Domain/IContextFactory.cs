using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DefectLog.Domain
{
    public interface IContextFactory
    {
        DbContext GetContext();
    }
}