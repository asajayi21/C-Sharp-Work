///A. Seun Ajayi
///AjayiP8
///8/7/2022
///aajayi@cnm.edu

using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace FlashCard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BindingList<Card> Cards { get; set; }
        public Card CurrentCard { get; set; }
        public Card SelectedCard { get; set; }
        public List<Card> CardList { get; set; }
        public string CardTitle { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string CardID { get; set; }
        private DBManager _dbManager;

        public MainWindow()
        {
            InitializeComponent();
            Cards = new BindingList<Card>();
            DataContext = this;
            _dbManager = new DBManager("FlashCardDB");

            ///Fills the list ListCard variable
            var listCards = new BindingList<Card>();
            _dbManager.GetCards(listCards);
            CardList = listCards.ToList();

            ///Refreshs the list
            RefreshList(); 
           
        }

        private void addCard_Click(object sender, RoutedEventArgs e)
        {
            var newCard = new Card();
            newCard.Title = CardTitle;
            newCard.Question = Question;
            newCard.Answer = Answer;

            _dbManager.AddCard(newCard);
            RefreshList();
        }

        private void RefreshList()
        {
            ///Reset values to empty string
            label_CardID.Content = "";
            title.Text = "";
            question.Text = "";
            answer.Text = "";
            
            ///reset selected item
            cards.UnselectAll();

            ///Set focus to textbox
            title.Focus();

            Cards.Clear();
            _dbManager.GetCards(Cards);

            CardList = Cards.ToList();

            DisplayCardQuestion();
        }

        /// <summary>
        /// Returns random Card object
        /// </summary>
        /// <returns></returns>
        private void GetRandomCard()
        {
            ///Sets button to enabled or not enabled
            btn_Show.IsEnabled = true;
            btn_Right.IsEnabled = false;
            btn_Wrong.IsEnabled = false;
            txAnswer.Visibility = Visibility.Hidden;

            ///Random number generator
            Random rnd = new Random();
            int count = CardList.Count();

           int nextNum = rnd.Next(0, count);

            CurrentCard = CardList[nextNum];
        }

        private void DisplayCardQuestion()
        {
            /// Calls the random card method
            GetRandomCard();

            ///Sets value of obtects
            Lb_CardID.Content = CurrentCard.CardID.ToString();
            Tx_Title.Text = CurrentCard.Title;
            txQuestion.Text = CurrentCard.Question;
            txAnswer.Text = CurrentCard.Answer;
            tx_Display.Text = "Think about the answer then click Show Answer button.";
        }

        private void btn_Show_Click(object sender, RoutedEventArgs e)
        {
            ///Sets button to false or true
            txAnswer.Visibility = Visibility.Visible;
            btn_Show.IsEnabled = false;
            btn_Right.IsEnabled = true;
            btn_Wrong.IsEnabled = true;

            ///Calls the calc method to get the ratio of right to wrong
            CurrentCard.Calc();
            tx_Display.Text = "If you were correct click Right else click Wrong.\n\n" + $"Your right to wrong ratio for question is: {CurrentCard.RightWrongRatio:F1}";
        }

        /// <summary>
        /// Event for button right clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Right_Click(object sender, RoutedEventArgs e)
        {
            ///Sets button to disabled
            btn_Wrong.IsEnabled = false;

            ///Adds one to the numright property
            CurrentCard.NumRight += 1;

            ///Updated the DB
            _dbManager.UpdateCard(CurrentCard);

            ///Gets the next random question
            DisplayCardQuestion();
        }

        private void btn_Wrong_Click(object sender, RoutedEventArgs e)
        {
            ///Sets button to disabled
            btn_Right.IsEnabled = false;

            ///Adds one to the numwrong property
            CurrentCard.NumWrong += 1;

            ///Updated the DB
            _dbManager.UpdateCard(CurrentCard);

            ///Gets the next random question
            DisplayCardQuestion();
        }

        /// <summary>
        /// Calls the DBManager Update query
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Update_Click(object sender, RoutedEventArgs e)
        {
            ///Assing value to the Card variable 
            if (cards.SelectedIndex > -1)
            {
                var newCard = new Card();
                newCard.Title = title.Text;
                newCard.Question = question.Text;
                newCard.Answer = answer.Text;
                newCard.CardID = int.Parse(label_CardID.Content.ToString());
                newCard.NumRight = ((Card)cards.SelectedItem).NumRight;
                newCard.NumWrong = ((Card)cards.SelectedItem).NumWrong;

                ///Updated the DB
                _dbManager.UpdateCard(newCard);
            }
                RefreshList();
                answer_Copy.Text = "Item has been updated!";
            
        }

        /// <summary>
        /// Calls the delete query
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            ///Removed item only if an item is selected 
            if (cards.SelectedIndex > -1)
            {
                _dbManager.RemoveCard(((Card)cards.SelectedItem));
                RefreshList();
                answer_Copy.Text = "Item has been deleted!";
            }
        }

        /// <summary>
        /// Sets value to the selected item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ///Assign values to the textbox when item selected on list
            if (cards.SelectedIndex > -1)
            {
                label_CardID.Content = ((Card)cards.SelectedItem).CardID;
                title.Text = ((Card)cards.SelectedItem).Title;
                question.Text = ((Card)cards.SelectedItem).Question;
                answer.Text = ((Card)cards.SelectedItem).Answer;
                answer_Copy.Text = "";

                ///Set focus to textbox
                title.Focus();
                
            }
        }

       
    }
}
