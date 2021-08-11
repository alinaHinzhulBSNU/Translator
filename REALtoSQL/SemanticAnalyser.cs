using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALtoSQL
{
    public class SemanticAnalyser : Analyser
    {
        private List<String> input;

        public void setInput(List<String> input)
        {
            this.input = input;
        }

        public String SQLQuery()
        {
            StringBuilder sb = new StringBuilder("");

            foreach(String elem in input)
            {
                if (elem.Equals("["))
                {
                    // FIND PROJECTION
                    ProjectionSQL projection = new ProjectionSQL();
                    sb.Append(findOperator(projection));
                    sb.Append(" ");
                }
                else if(elem.Equals("times"))
                {
                    // FIND TIMES
                    TimesSQL times = new TimesSQL();
                    sb.Append(findOperator(times));
                    sb.Append(" ");
                }
                else if (elem.Equals("join"))
                {
                    // FIND JOIN
                    JoinSQL join = new JoinSQL();
                    sb.Append(findOperator(join));
                    sb.Append(" ");
                }
            }

            return sb.ToString();
        }

        public override bool startAnalyze()
        {
            if (!SQLQuery().Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private String findOperator(StatementSQL statement)
        {
            return statement.QuerySQL(input);
        }
    }
}
