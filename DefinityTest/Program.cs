using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DefinityTest
{
    internal class Program
    {
        /*
        Write a solution that takes in a collection of Marbles and returns the filtered & ordered list of marbles back.
        The palindrome should ignore capitalization and punctuation so Bob o’Bob is a palindrome.
        
        Please include comments describing the time and space complexity of your algorithm. 
        --I think the weakest part of my solution is the bubble sort but for the time I couldn´t work in something more sofisticade

        Additionally, please include comments that describe what your deployment strategy would be to host this workload in
        (any) cloud platform in a way that is accessible, repeatable, & modular (example: as a serverless function or REST service… etc). 
        Discuss any deployment technologies & automation strategies you would wrap around the solution. 
        -- Well this is a calssic example of a serverless function, you recibe data and have to extract the stuff you want before save it,
        In the case thats the only part of a requirment I'd implement a serverless function with azure function.
        But it'd seem wierd to me if this is the only part of a system, I've faced thing like this more common in API projects,
        I think we'd add more features in the future,  so I'd select an web service  (API project) for deployment.

        What would you do if Bob has millions of marbles to process? 
        -- Well in that case, I'd have to refactor the bubble sort for something more efficient and sofisticated,
        definitely we'd have to say good bye to buuble sort, maybe add another property to the class to deal with the order
        better than a string. 
        
        To submit, you can use https://ideone.com/ or send us a git link.
         */
        //Test Case Result
        //blue violet indigo green blue
        //g b b i v

        //Comments at the end
        static void Main(string[] args)
        {
            //Seed data
            var marbles = new List<marble> {
            new marble { id =  1, color =  "blue", name =  "Bob", weight =  0.5 },
            new marble { id =  2, color =  "red", name =  "John Smith", weight =  0.25 },
            new marble { id =  3, color =  "violet", name =  "Bob O'Bob", weight =  0.5 },
            new marble { id =  4, color =  "indigo", name =  "Bob Dad-Bob", weight =  0.75 },
            new marble { id =  5, color =  "yellow", name =  "John", weight =  0.5 },
            new marble { id =  6, color =  "orange", name =  "Bob", weight =  0.25 },
            new marble { id =  7, color =  "blue", name =  "Smith", weight =  0.5 },
            new marble { id =  8, color =  "blue", name =  "Bob", weight =  0.25 },
            new marble { id =  9, color =  "green", name =  "Bobb Ob", weight =  0.75 },
            new marble { id =  10, color =  "blue", name =  "Bob", weight =  0.5 }
            };
            // Filtered the data
            var marblesFiltered = GetFilteredMarbles(marbles);
            // Send (Print) result
            SendResult(marblesFiltered);
        }

        private static void SendResult(List<marble> marblesFiltered)
        {
            for (int i = 0; i < marblesFiltered.Count; i++)
            {
                Console.WriteLine("Id: " + marblesFiltered[i].id +
                                  " name: " + marblesFiltered[i].name +
                                  " color: " + marblesFiltered[i].color +
                                  " weight: " + marblesFiltered[i].weight 
                                    );
            }
            Console.Read();
        }

        private static List<marble> GetFilteredMarbles(List<marble> marbles)
        {
            // with linq I clean the data by weight and if the names are palindrome.
            marbles = marbles.Where(marble => IsPalindorme(marble.name) == true && marble.weight >= 0.5).ToList();
            // with the bubble sort and the position in the suggest order (ROYGBIV ) I sorted the data
            for (int j = 0; j < marbles.Count - 1; j++)
            {
                for (int i = 0; i < marbles.Count - 1; i++)
                {
                    //I got the index of the marble in the color order (0,1,2,3,4,5,6)
                    int aPosition = GetIndex(marbles[i].color.First());
                    int bPosition = GetIndex(marbles[i + 1].color.First());

                    if (bPosition != aPosition)
                        if (bPosition < aPosition)
                        {
                            //if the marbles are in wrong position I change the position.
                            var temp = marbles[i + 1];
                            marbles[i + 1] = marbles[i];
                            marbles[i] = temp;
                        }
                }
            }

            return marbles;
        }

        private static bool IsPalindorme(string name)
        {
            //With Regex, I stablished only the letters as aceptable characters
            Regex rgx = new Regex("[^a-zA-Z]");
            //Ignore the Capital letters
            name = name.ToLower();
            //Ignore everything exept the letters
            name = rgx.Replace(name, "");
            //verify if is a palindrome
            if (name == new string(name.Reverse().ToArray()))
                return true;
            else
                return false;
        }

        public static int GetIndex(char initialCharacter)
        {   //ROYGBIV 
            //
            return Array.IndexOf(new[] { 'r', 'o', 'y', 'g', 'b', 'i', 'v' }, initialCharacter);
        }

        public class marble
        {
            //I created a class to work more comfortable than with anonymous objects
            public int id;
            public string color;
            public string name;
            public double weight;
        }
    }
}
/*
 
 
 */
