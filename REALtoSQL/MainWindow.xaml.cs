using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace REALtoSQL
{
    public partial class MainWindow : Window
    {
        private String input;
        private String output;

        private bool lexicalAnalysisResult;
        private bool syntacticAnalysisResult;
        private bool semanticAnalysisResult;

        private LexicalAnalyser lexicalAnalyser;
        private SyntacticAnalyser syntacticAnalyser;
        private SemanticAnalyser semanticAnalyser;

        public MainWindow()
        {
            InitializeComponent();

            output = "";

            lexicalAnalysisResult = false;
            syntacticAnalysisResult = false;
            semanticAnalysisResult = false;

            lexicalAnalyser = new LexicalAnalyser();
            syntacticAnalyser = new SyntacticAnalyser();
            semanticAnalyser = new SemanticAnalyser();
        }


        private void translate(object sender, RoutedEventArgs e)
        {
            //Оновлення значень
            output = "";
            lexicalAnalysisResult = false;
            syntacticAnalysisResult = false;
            semanticAnalysisResult = false;

            // Отриманий рядок
            input = REALQuery.Text.ToString();

            //АНАЛІЗ
            lexicalAnalyser.setInput(input);
            lexicalAnalysisResult = lexicalAnalyser.startAnalyze();

            if (lexicalAnalysisResult)
            {
                syntacticAnalyser.setInput(lexicalAnalyser.getTokens());
                syntacticAnalysisResult = syntacticAnalyser.startAnalyze();
            }
            if(lexicalAnalysisResult && syntacticAnalysisResult)
            {
                semanticAnalyser.setInput(lexicalAnalyser.getTokensForSemanticAnalyser());
                semanticAnalysisResult = semanticAnalyser.startAnalyze();
            }

            // Результат для виведення
            if(lexicalAnalysisResult && syntacticAnalysisResult && semanticAnalysisResult)
            {
                output = semanticAnalyser.SQLQuery();
            }

            // Показати процес
            showProcess();

            // Показати результат
            showResult();
        }

        private void clear(object sender, RoutedEventArgs e)
        {
            // Поле з REAL-запитом
            REALQuery.Clear();

            // Результати лексичного аналізу
            lexicalIsTrue.Visibility = Visibility.Hidden;
            lexicalIsFalse.Visibility = Visibility.Hidden;

            lexicalIsTrueHint.Visibility = Visibility.Hidden;
            lexicalisFalseHint.Visibility = Visibility.Hidden;

            // Результати синтаксичного аналізу
            syntacticIsTrue.Visibility = Visibility.Hidden;
            syntacticIsFalse.Visibility = Visibility.Hidden;

            syntacticIsTrueHint.Visibility = Visibility.Hidden;
            syntacticIsFalseHint.Visibility = Visibility.Hidden;

            //Результати семантичного аналізу
            semanticIsTrue.Visibility = Visibility.Hidden;
            semanticIsFalse.Visibility = Visibility.Hidden;

            semanticIsTrueHint.Visibility = Visibility.Hidden;
            semanticIsFalseHint.Visibility = Visibility.Hidden;

            // Поле з SQL-запитом
            SQLQuery.Clear();
        }

        private void showProcess()
        {
            // Результати лексичного аналізу
            lexicalIsTrue.Visibility = lexicalAnalysisResult ? Visibility.Visible : Visibility.Hidden;
            lexicalIsFalse.Visibility = lexicalAnalysisResult ? Visibility.Hidden : Visibility.Visible;

            lexicalIsTrueHint.Visibility = lexicalAnalysisResult ? Visibility.Visible : Visibility.Hidden;
            lexicalisFalseHint.Visibility = lexicalAnalysisResult ? Visibility.Hidden : Visibility.Visible;

            // Результати синтаксичного аналізу
            syntacticIsTrue.Visibility = syntacticAnalysisResult ? Visibility.Visible : Visibility.Hidden;
            syntacticIsFalse.Visibility = syntacticAnalysisResult ? Visibility.Hidden : Visibility.Visible;

            syntacticIsTrueHint.Visibility = syntacticAnalysisResult ? Visibility.Visible : Visibility.Hidden;
            syntacticIsFalseHint.Visibility = syntacticAnalysisResult ? Visibility.Hidden : Visibility.Visible;

            //Результати семантичного аналізу
            semanticIsTrue.Visibility = semanticAnalysisResult ? Visibility.Visible : Visibility.Hidden;
            semanticIsFalse.Visibility = semanticAnalysisResult ? Visibility.Hidden : Visibility.Visible;

            semanticIsTrueHint.Visibility = semanticAnalysisResult ? Visibility.Visible : Visibility.Hidden;
            semanticIsFalseHint.Visibility = semanticAnalysisResult ? Visibility.Hidden : Visibility.Visible;
        }

        private void showResult()
        {
            SQLQuery.Text = output;

            if (output.Equals(""))
            {
                SQLQuery.IsEnabled = false;
            }
            else
            {
                // Зробити поле активним, для того, щоб з нього можна було зробити копію
                SQLQuery.IsEnabled = true;
            }

        }
    }
}
