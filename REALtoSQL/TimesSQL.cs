using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALtoSQL
{
    public class TimesSQL : StatementSQL
    {
        private String table1;
        private String table2;

        public override bool filter(List<String> tokens)
        {
            for(int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Equals("times"))
                {
                    table1 = tokens[i - 1];
                    table2 = tokens[i + 1];
                    return true;
                }
            }
            return false;
        }

        public override String QuerySQL(List<String> tokens)
        {
            if (filter(tokens))
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT * FROM ");
                sb.Append(table1);
                sb.Append(" CROSS JOIN ");
                sb.Append(table2);
                sb.Append(";");

                return sb.ToString();
            }
            else
            {
                return "";
            }
        }
    }
}
