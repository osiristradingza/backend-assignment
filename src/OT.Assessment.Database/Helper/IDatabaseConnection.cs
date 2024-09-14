using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OT.Assessment.Database.Helper
{
    public interface IDatabaseConnection
    {
        IDbConnection CreateConnection();
    }
}
