using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALtoSQL
{
    public abstract class Analyser
    {
        private bool result;

        public Analyser()
        {
            result = false;
        }

        public virtual bool startAnalyze()
        {
            return result;
        }

        public bool Result
        {
            set
            {
                this.result = value;
            }
        }
    }
}
