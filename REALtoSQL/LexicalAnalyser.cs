using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace REALtoSQL
{
    public class LexicalAnalyser : Analyser
    {
        private String input;
        private List<string> tokens;

        public void setInput(String input)
        {
            this.input = input;
        }

        public List<String> getTokens()
        {
            // Новий список токенів
            tokens = new List<string>();

            // Оточити пробілами символи, які ідуть окремо при синтаксичному розборі
            String inputWithSpaces = insertSpaces(input);
            
            // Розбити на слова по пробілам(всі символи, що мають бути окремо, будуть окремі)
            string[] words = inputWithSpaces.Split(' ');

            // ОБРОБКА ОТРИМАНИХ СЛІВ
            foreach (String word in words)
            {
                // Прибрати зайві пробіли
                String resultWord = word.Replace(" ", "");

                // Цифра і буква з id йдуть окремо при синтаксичному розборі
                if (isId(resultWord))
                {
                    tokens.Add(resultWord.Substring(0, 1));
                    tokens.Add(resultWord.Substring(1, 1));
                }
                else if (resultWord.Length > 0)
                {
                    // ЖОДНИЙ ТОКЕН НЕ МОЖЕ ПОЧИНАТИСЯ З ЦИФРИ!!!
                    if (!Char.IsDigit(resultWord[0]))
                    {
                        tokens.Add(resultWord);
                    }
                }
            }

            return tokens;
        }

        public List<String> getTokensForSemanticAnalyser()
        {
            // Новий список токенів
            tokens = new List<string>();

            // Оточити пробілами символи, які ідуть окремо при синтаксичному розборі
            String inputWithSpaces = insertSpaces(input);

            // Розбити на слова по пробілам(всі символи, що мають бути окремо, будуть окремі)
            string[] words = inputWithSpaces.Split(' ');

            // ОБРОБКА ОТРИМАНИХ СЛІВ
            foreach (String word in words)
            {
                // Прибрати зайві пробіли
                String resultWord = word.Replace(" ", "");

                // ДЛЯ ЦЬОГО ТИПУ АНАЛІЗУ id МАЄ БУТИ ЄДИНИМ СЛОВОМ
                if (resultWord.Length > 0)
                {
                    // ЖОДНИЙ ТОКЕН НЕ МОЖЕ ПОЧИНАТИСЯ З ЦИФРИ!!!
                    if (!Char.IsDigit(resultWord[0]))
                    {
                        tokens.Add(resultWord);
                    }
                }
            }

            return tokens;
        }


        public override bool startAnalyze()
        {
            getTokens();

            if(tokens.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool isId(String word)
        {
            // Знайти id серед токенів
            Regex regex = new Regex(@"^\S\d$");
            MatchCollection matches = regex.Matches(word);

            if(matches.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private String insertSpaces(String word)
        {
            StringBuilder sb = new StringBuilder(word);

            sb.Replace(",", " , ");
            sb.Replace(";", " ; ");
            sb.Replace("[", " [ ");
            sb.Replace("]", " ] ");
            sb.Replace(":=", " := ");

            return sb.ToString();
        }
    }
}
