using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Riddle
{
    public partial class Main : Form
    {

        //Array list of words and hints
        private ArrayList wordList = new ArrayList()
        {
            new Word("coin", "What has a head and a tail \n\nbut no body?\n\nHint: You can use it to buy things."),
            new Word("future", "What is always in front of you \n\nbut can’t be seen?\n\nHint: It hasn't happened yet."),
            new Word("sponge", "What is full of holes but \n\nstill holds water?\n\nHint: It's often used for cleaning \ndishes or surfaces."),
            new Word("age", "What goes up but never comes down?\n\nHint: It increases every year."),
            new Word("clock", "What has a face and two hands but no legs?\n\nHint: It doesn't stop running."),
            new Word("pencil", "What has to be broken before you can use it\n\nHint: John Wick used it to kill three men in a bar."),
            new Word("gloves", "What has a thumb and four fingers but is not alive\n\nHint: It's something you use to hold things."),
            new Word("computer", "What has a heart that doesn't beat?\n\nHint: It's something you use to store information.")
        };
        public class Word
        {
            public string WordValue;
            public string HintValue;

            public Word(string word, string hint)
            {
                WordValue = word;
                HintValue = hint;
            }
        }
        //Selected word and hint
        private string selectedWord;
        private string selectedHint;

        //Number of lives remaining
        private int lives = 5;

        //Words guessed correctly and incorrectly 
        // private ArrayList correctGuesses = new ArrayList();

        public Main()
        {
            InitializeComponent();
            //Add click event handlers to the alphabet buttons 
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                if (control is Button)
                {
                    ((Button)control).Click += new EventHandler(tableLayoutPanel1_Click);
                }
            }
        }

        private void tableLayoutPanel1_Click(object sender, EventArgs e)
        {
            //Get the player's guess
            string guess = ((Button)sender).Text.ToLower();

            //Disable the clicked letter picturebox
            ((Button)sender).Enabled = false;

            //Check if the guess is correct
            if (selectedWord.Contains(guess))
            {
                //Replace the blanks with the correct letter(s)
                for (int i = 0; i < selectedWord.Length; i++)
                {
                    if (selectedWord[i] == guess[0])
                    {
                        wordLabel.Text = wordLabel.Text.Remove(i * 2, 1);
                        wordLabel.Text = wordLabel.Text.Insert(i * 2, guess);
                    }
                }

                //Add the guess to the list of correct guesses
                /*
                if (!correctGuesses.Contains(guess))
                {
                    correctGuesses.Add(guess);
                }*/

                //Check if the player has guessed all the words
                if (wordLabel.Text.IndexOf('_') == -1)
                {
                    this.Hide();
                    Win winner = new Win();
                    winner.Show();
                }
            }
            else
            {
                //Decrement the number of lives
                lives--;
                if (lives > 0)
                {
                    switch (lives)
                    {
                        case 4:
                            hans.Visible = false;
                            break;
                        case 3:
                            charles.Visible = false;
                            break;
                        case 2:
                            keiro.Visible = false;
                            break;
                        case 1:
                            jerald.Visible = false;
                            break;
                        case 0:
                            pau.Visible = false;
                            break;
                    }
                }

                //Check if the player has run out of lives
                if (lives == 0)
                {
                    this.Hide();
                    Lose loser = new Lose();
                    loser.Show();
                }

            }
            
            //Disable the clicked letter button
            ((Button)sender).Enabled = true;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //Select a random word and hint 
            Random random = new Random();
            int index = random.Next(0, wordList.Count);
            Word word = (Word)wordList[index];
            selectedWord = word.WordValue;
            selectedHint = word.HintValue;

            //Displaying the hint and blanks for the word
            hintLabel.Text = selectedHint;
            wordLabel.Text = "";
            for (int i = 0; i < selectedWord.Length; i++)
            {
                wordLabel.Text += "_ ";
            }
        }
    }
}
