using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALtoSQL
{
    public class JoinSQL : StatementSQL
    {
        //JOIN Products ON Products.Id = Orders.ProductId;
        private String table1;
        private String table2;
        private String column;

        // ВИЯВЛЕННЯ ЗАЛЕЖНОСТЕЙ МІЖ ТОКЕНАМИ
        public override bool filter(List<String> tokens)
        { 
            for(int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Equals("join"))
                {
                    table1 = tokens[i - 1];
                    table2 = tokens[i + 1];
                    column = tokens[i + 3];

                    return true;
                }
            }

            return false;
        }

        // ГОТОВИЙ ЗАПИТ
        public override String QuerySQL(List<String> tokens)
        {
            if (filter(tokens))
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("SELECT * FROM ");
                sb.Append(table1);
                sb.Append(" JOIN ");
                sb.Append(table2);
                sb.Append(" ON ");
                sb.Append(column);
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
