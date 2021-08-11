using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALtoSQL
{
    public class SyntacticAnalyser : Analyser
    {
        private List<String> tokens;
        private int position;
        private ParsingTable parsingTable;
        private Stack<String> stack;

        public SyntacticAnalyser() : base()
        {
            position = 0;
            parsingTable = new ParsingTable();
            stack = new Stack<string>();
        }

        public void setInput(List<String> input)
        {
            this.tokens = input;
        }

        public override bool startAnalyze()
        {
            // Поточний елемент в tokens
            position = 0;

            // Останнім елементом завжди має бути '$'
            stack.Push("$");

            // Перше правило для розбору
            stack.Push("<Прогр_РА>");

            while (position < tokens.Count && !stack.Peek().Equals("$"))
            {
                // Правило для розбору береться зі стеку
                String nonTerminal = stack.Pop();

                // Елемент береться з tokens
                String terminal = tokens[position];

                // ЧИ Є ТЕРМІНАЛ У FIRST або FOLLOW для даного НЕ ТЕРМІНАЛА? Якщо так, повернути відповідні похідні правила.
                string[] resultArray = parsingTable.parse(nonTerminal, terminal);

                if (resultArray.Length == 0)
                {
                    // Не знайдено відповідність. Спробувати повернутись на 1 елемент назад.
                    if(position != 0)
                    {
                        stack.Push(nonTerminal);
                        position = position - 1;
                        resultArray = parsingTable.parse(stack.Pop(), tokens[position]);

                        if (resultArray.Length == 0)
                        {
                            // Відповідності немає
                            return false;
                        }
                        else
                        {
                            richStack(resultArray);
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    richStack(resultArray);
                }
            }

            // Для елемента endRA
            if (position > 0 && stack.Peek().Equals(tokens[position - 1]))
            {
                stack.Pop();
            }

            // Результат
            if (stack.Peek().Equals("$") && position > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void richStack(string [] resultArray)
        {
            //Заповнити стек відповідними похідними правилами
            foreach (String element in resultArray)
            {
                if (element != null)
                {
                    if (!element.Equals("0"))
                    {
                        stack.Push(element);
                    }
                }
                else
                {
                    // Перейти до наступного token
                    position = position + 1;
                }
            }
        }
    }
}
