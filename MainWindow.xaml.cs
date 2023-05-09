using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Amoba
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            NewGame(3);

        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            NewGame(4);
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            NewGame(5);
        }

        int x = 0;

        private void NewGame(int s)
        {
            x=s;
            innerGrid.Children.Clear();
            innerGrid.RowDefinitions.Clear();
            innerGrid.ColumnDefinitions.Clear();
            for (int i = 0; i < s; i++)
            {
                RowDefinition row = new RowDefinition();
                innerGrid.RowDefinitions.Add(row);
                ColumnDefinition column = new ColumnDefinition();
                innerGrid.ColumnDefinitions.Add(column);
            }
            for (int i = 0; i < s; i++)
            {
                for (int j = 0; j < s; j++)
                {
                    Button btn = new Button();
                    btn.Content = "";
                    btn.Padding = new Thickness(5);
                    btn.FontSize = 30;
                    btn.Click += Btn_Click;
                    innerGrid.Children.Add(btn);
                    Grid.SetColumn(btn, j);
                    Grid.SetRow(btn, i);
                }
            }
            current.Text = "Current player is X";
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if(btn.Content=="")
            {
                if(current.Text == "Current player is X")
                {
                    btn.Content = "X";
                }
                else
                {
                    btn.Content = "O";
                }
                IsItWon();
            }
        }

        private void IsItWon()
        {
            if(CheckRows() || CheckCols() || CheckDiag())
            {
                string winner = "X";
                if(current.Text == "Current player is O")
                {
                    winner = "O";
                }
                MessageBoxResult result = MessageBox.Show($"The winner is {winner}!","Win!");
                if(result == MessageBoxResult.OK)
                {
                    NewGame(x);
                }
            }
            else
            {
                if(current.Text == "Current player is X")
                {
                    current.Text = "Current player is O";
                }
                else
                {
                    current.Text = "Current player is X";
                }
            }
        }

        private bool CheckDiag()
        {
            bool diag = true;
            string diagonal1FirstElement = (innerGrid.Children[0] as Button).Content.ToString();
            if (diagonal1FirstElement != "")
            {
                for (int i = 1; i < x; i++)
                {
                    string currentElement = (innerGrid.Children[i * x + i] as Button).Content.ToString();
                    if (currentElement != diagonal1FirstElement)
                    {
                        diag = false;
                        break;
                    }
                }
                if (diag)
                {
                    // Bingo in diagonal from top left to bottom right
                    return true;
                }
            }

            string diagonal2FirstElement = (innerGrid.Children[x - 1] as Button).Content.ToString();
            if (diagonal2FirstElement != "")
            {
                for (int i = 1; i < x; i++)
                {
                    string currentElement = (innerGrid.Children[(x - i) * x + i] as Button).Content.ToString();
                    if (currentElement != diagonal2FirstElement)
                    {
                        diag = false;
                        break;
                    }
                }
                if (diag)
                {
                    // Bingo in diagonal from bottom left to top right
                    return true;
                }
            }
            return false;
        }

        private bool CheckCols()
        {
            for (int i = 0; i < x; i++)
            {
                bool win = true;
                string firstElement = (innerGrid.Children[i] as Button).Content.ToString();
                if (firstElement != "")
                {
                    for (int j = 1; j < x; j++)
                    {
                        string currentElement = (innerGrid.Children[j * x + i] as Button).Content.ToString();
                        if (currentElement != firstElement)
                        {
                            win = false;
                            break;
                        }
                    }
                    if (win)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CheckRows()
        {

            for (int i = 0; i < x; i++)
            {
                bool win = true;
                string firstElement = (innerGrid.Children[i * x] as Button).Content.ToString();
                if (firstElement != "")
                {
                    for (int j = 1; j < x; j++)
                    {
                        string currentElement = (innerGrid.Children[i * x + j] as Button).Content.ToString();
                        if (currentElement != firstElement)
                        {
                            win = false;
                            break;
                        }
                    }
                    if (win)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
