using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBIN.DAL
{
    public class Connection
    {
        public IBINEntities _db { get; set; }
        public Connection()
        {
            _db = new IBINEntities();
        }
    }
}
