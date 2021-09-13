using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleAppHangman
{
    class Program
    {
        static void Main(string[] args)
        {
            bool menuLoop = true;
            while (menuLoop == true)
            {
                GuessTheWord();
            }

        }//end of Program

        private static string RandomWord()
        {
            string wordsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AllWords.txt");
            string[] allWords = File.ReadAllText(wordsPath).Split(',');

            Random randomize = new Random();
            string randWord = allWords[randomize.Next(allWords.Length)];
            string theWord = randWord.ToUpper();

            return theWord;


        }//end of RandomWords




        static void GuessTheWord()
        {
            string theWord = RandomWord();
            Console.WriteLine($"Random word is: {theWord}");
            Console.ReadKey(true);

            StringBuilder hiddenWord = new StringBuilder();
            for (int i = 0; i < theWord.Length; i++)
            {
                hiddenWord.Append('_');
            }

            bool endLoop = false;
            bool won = false;
            int playerLives = 10;
            int shownLetters = 0;
            string userInput = "";
            char letterGuess;

            StringBuilder goodGuess = new StringBuilder();
            char[] goodGuesses = goodGuess.ToString().ToCharArray();

            StringBuilder allBadGuesses = new StringBuilder();
            string badGuesses = "";

            List<string> badWords = new List<string>();


            // time for the player
            while (!won && playerLives > 0)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("Enter ONE letter or type the WHOLE word\n\n" +
                                      "\nTries left: " +playerLives);

                    if(badWords.Count > 0)
                    {
                        Console.Write("Wrong words: ");
                        for (int i = 0; i < badWords.Count; i++)
                        {
                            Console.Write(badWords[i] + ", ");
                        }
                        Console.WriteLine();
                    }
                    if (allBadGuesses.Length > 0)
                    {
                        Console.Write("Wrong Guesses: ");
                        for (int i = 0; i < allBadGuesses.Length; i++)
                        {
                            Console.Write(allBadGuesses[i] + ", ");
                        }
                    }

                    Console.WriteLine($"\n\nGuess the word -->  {hiddenWord}");



                    userInput = Console.ReadLine().ToUpper();

                } while (userInput == "");

                letterGuess = userInput[0];
                Console.Clear();

                if(badWords.Contains(userInput))
                {
                    Console.WriteLine($"You have already guessed: {userInput}\nPress any key to continue");
                    Console.ReadKey();
                }
                else if (userInput.Length == theWord.Length && userInput != theWord) //Wrong word
                {
                    Console.WriteLine("It was not " + userInput);
                    Console.ReadKey(true);
                    badWords.Add(userInput);
                    playerLives--;
                }
                else if (userInput != theWord && userInput.Length != 1)
                {
                    Console.WriteLine("Please enter only ONE letter or the WHOLE word\nPress any key to continue");
                    Console.ReadKey(true);
                }
                else if (theWord == userInput)
                { won = true; }

                else if (goodGuesses.Contains(letterGuess))
                //if (Array.Exists(goodGuesses, element => element == letterGuess))
                {
                    Console.WriteLine("You have already guessed: " + letterGuess);
                    Console.ReadKey();
                }

                else if (theWord.Contains(letterGuess)) //Good guess
                {
                    for (int i = 0; i < theWord.Length; i++)
                    {
                        if (theWord[i] == letterGuess)
                        {
                            hiddenWord[i] = theWord[i];
                            shownLetters++;
                        }
                    }
                    goodGuesses = goodGuess.Append(letterGuess).ToString().ToCharArray();
                    if (shownLetters == theWord.Length)
                    { won = true; }
                }

                else if (badGuesses.Contains(letterGuess))
                {
                    Console.WriteLine($"You have already tried '{letterGuess}'!\nPress any key to continue");
                    Console.ReadKey(true);
                }
                else if (!theWord.Contains(letterGuess)) //Bad guess
                {
                    allBadGuesses.Append(letterGuess);
                    badGuesses = allBadGuesses.ToString();
                    playerLives--;
                }



            }//end of while not won or lost

            if (won == true) //Won the game
            {
                Console.WriteLine($"Congratulations!\nYou figured out it was {theWord}!");
                while (endLoop == false)
                {
                    Console.WriteLine("Do you want to play again?(Y/N)");
                    userInput = Console.ReadLine().ToUpper();
                    if (userInput == "Y" || userInput == "YES")
                    {
                        Console.WriteLine("\nPress any key to start a new game");
                        Console.ReadKey(true);
                        endLoop = true;
                    }
                    else if (userInput == "N" || userInput == "NO")
                    {
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("\nPlease choose Y/YES or N/NO");
                    }
                }
            }
            else //Lost the game
            {
                Console.WriteLine("You lost!\nThe word was: " + theWord);
                while (endLoop == false)
                {
                    Console.WriteLine("Do you want to play again?(Y/N)");
                    userInput = Console.ReadLine().ToUpper();
                    if (userInput == "Y" || userInput == "YES")
                    {
                        Console.WriteLine("\nPress any key to start a new game");
                        Console.ReadKey(true);
                        endLoop = true;
                    }
                    else if (userInput == "N" || userInput == "NO")
                    {
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        Console.WriteLine("\nPlease choose Y/YES or N/NO");
                    }
                }
            }

        }//end of GuessTheWord

    }
}
