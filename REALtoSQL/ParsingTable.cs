using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALtoSQL
{
    // СТВОРЕНО НА ОСНОВІ ТАБЛИЦІ РОЗБОРУ З ЛР7
    public class ParsingTable
    {
        public string [] parse(String nonTerminal, String terminal)
        {
            if(nonTerminal.Equals("<Прогр_РА>") && terminal.Equals("beginRA"))
            {
                return new string[] { "endRA", "<ПослідОперРА>", "beginRA" };
            }
            else if(nonTerminal.Equals("<ПослідОперРА>") && terminal.Length == 1 && Char.IsLetter(terminal[0]))
            {
                return new string[] { "<ПослідОперРА`>", "<ОперРА>" };
            }
            else if (nonTerminal.Equals("<ПослідОперРА`>") && terminal.Equals("endRA"))
            {
                return new string[] { null };
            }
            else if (nonTerminal.Equals("<ПослідОперРА`>") && terminal.Equals(";"))
            {
                return new string[] { "<ПослідОперРА`>", "<ОперРА>", ";" };
            }
            else if (nonTerminal.Equals("<ОперРА>") && terminal.Length == 1 && Char.IsLetter(terminal[0]))
            {
                return new string[] { "<ОперМаніпБД>"};
            }
            else if (nonTerminal.Equals("<ОперМаніпБД>") && terminal.Length == 1 && Char.IsLetter(terminal[0]))
            {
                return new string[] { "<ВиразРА>", ":=", "<id>" };
            }
            else if (nonTerminal.Equals("<ВиразРА>") && terminal.Length == 1 && Char.IsLetter(terminal[0]))
            {
                return new string[] { "<ВиразРА``>", "<id>" };
            }
            else if (nonTerminal.Equals("<ВиразРА``>") && terminal.Equals("times"))
            {
                return new string[] { "<ВиразРА``>", "<ВиразРА`>" };
            }
            else if (nonTerminal.Equals("<ВиразРА``>") && terminal.Equals("join"))
            {
                return new string[] { "<ВиразРА``>", "<ВиразРА`>" };
            }
            else if (nonTerminal.Equals("<ВиразРА``>") && terminal.Equals("["))
            {
                return new string[] { "<ВиразРА``>", "<ВиразРА`>" };
            }
            else if (nonTerminal.Equals("<ВиразРА``>") && terminal.Equals(";"))
            {
                return new string[] { null };
            }
            else if (nonTerminal.Equals("<ВиразРА`>") && terminal.Equals("["))
            {
                return new string[] { "<1мОперРА>" };
            }
            else if (nonTerminal.Equals("<ВиразРА`>") && terminal.Equals("times"))
            {
                return new string[] { "<id>", "<2мОперРА>" };
            }
            else if (nonTerminal.Equals("<ВиразРА`>") && terminal.Equals("join"))
            {
                return new string[] { "<id>", "<2мОперРА>" };
            }
            else if (nonTerminal.Equals("<2мОперРА>") && terminal.Equals("times"))
            {
                return new string[] { "times" };
            }
            else if (nonTerminal.Equals("<2мОперРА>") && terminal.Equals("join"))
            {
                return new string[] { "on", "<id>", "join" };
            }
            else if (nonTerminal.Equals("<1мОперРА>") && terminal.Equals("["))
            {
                return new string[] { "]", "<список параметрів>", "[" };
            }
            else if (nonTerminal.Equals("<список параметрів>") && terminal.Length == 1 && Char.IsLetter(terminal[0]))
            {
                return new string[] { "<список параметрів`>", "<id>" };
            }
            else if (nonTerminal.Equals("<список параметрів`>") && terminal.Equals("]"))
            {
                return new string[] { null };
            }
            else if (nonTerminal.Equals("<список параметрів`>") && terminal.Equals(","))
            {
                return new string[] { "<список параметрів`>", "<id>", "," };
            }
            else if (nonTerminal.Equals("<id>") && terminal.Length == 1 && Char.IsLetter(terminal[0]))
            {
                return new string[] { "<digit>", "<letter>" };
            }
            else if (nonTerminal.Equals("<letter>") && terminal.Length == 1 && Char.IsLetter(terminal[0]))
            {
                return new string[] { terminal };
            }
            else if (nonTerminal.Equals("<digit>") && terminal.Length == 1 && Char.IsDigit(terminal[0]))
            {
                return new string[] { terminal };
            }
            else if (nonTerminal.Equals(terminal))
            {
                return new string[] { null };
            }
            else
            {
                return new string[] { };
            }
        }

        public bool isRule(String terminal)
        {
            if(terminal[0].Equals('<'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
