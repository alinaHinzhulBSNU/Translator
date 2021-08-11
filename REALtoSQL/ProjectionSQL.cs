using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALtoSQL
{
    public class ProjectionSQL : StatementSQL
    {
        private List<String> columns;
        private String table;

        // ВИЯВЛЕННЯ ЗАЛЕЖНОСТЕЙ МІЖ ТОКЕНАМИ
        public override bool filter(List<String> tokens)
        {
            columns = new List<string>();

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Equals("["))
                {
                    // Таблиця ТОЧНО є попереднім елементам, адже вважається, що рядок пройшов синтаксичний та лексичний аналіз
                    table = tokens[i - 1];
                    int j = i + 1;

                    // Колонки, які обираються з таблиці
                    while (!tokens[j].Equals("]"))
                    {
                        if (!tokens[j].Equals(","))
                        {
                            columns.Add(tokens[j]);
                        }

                        j++;
                    }

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
                StringBuilder sb = new StringBuilder("");
                sb.Append("SELECT ");

                for(int i = 0; i < columns.Count; i++)
                {
                    sb.Append(columns[i]);
                    if(i != columns.Count - 1)
                    {
                        sb.Append(", ");
                    }
                    else
                    {
                        sb.Append(" ");
                    }
                }

                sb.Append("FROM ");
                sb.Append(table);
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
