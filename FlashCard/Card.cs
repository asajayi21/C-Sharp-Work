///A. Seun Ajayi
///AjayiP8
///8/7/2022
///aajayi@cnm.edu

using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCard
{
    
    public class Card
    {
        private int numRight;
        private int numWrong;

        #region
        public string Answer { get; set; }
        public int CardID { get; set; }

        public int NumRight
        {
            get
            {
                return numRight;
            }

            set
            {
                numRight = value;

            }
        }
        public int NumWrong
        {
            get
            {
                return numWrong;
            }

            set
            {
                numWrong = value;

            }
        }

        public string Question { get; set; }
        public float RightWrongRatio { get; set; }
        public string Title { get; set; }

        public Card() : this(0, "", "", "", 0, 0)
        {

        }

        public Card(int cardID, string title, string question, string answer, int numRight, int numWrong)
        {
            NumRight = numRight;
            NumWrong = numWrong;
            Answer = answer;
            CardID = cardID;
            Question = question;
            Title = title;
        }

        ///To calculate the rightwrong ratio
        public void Calc()
        {
            if (NumWrong > 0)
            {
                RightWrongRatio = (float)NumRight / (float)NumWrong;
            }
            else
                RightWrongRatio = 0.0F;
        }

        public override string ToString()
        {
            return $"{Title} {NumRight}/{NumWrong}";
        }
        #endregion
    }
}
