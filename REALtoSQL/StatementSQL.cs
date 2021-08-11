using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALtoSQL
{
    public abstract class StatementSQL
    {
        public virtual bool filter(List<String> tokens)
        {
            return false;
        }
        
        public virtual String QuerySQL(List<String> tokens)
        {
            return "";
        }
    }
}
