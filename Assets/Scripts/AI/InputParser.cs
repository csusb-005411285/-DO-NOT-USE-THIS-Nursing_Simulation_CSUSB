using System;
using UnityEngine;

namespace AI.Parser
{
    //FIXME TEST ME
    //the threaded logic for the input parser
    public class InputParser
    {
        private bool debugOutput = false;

        private string closestStringMatch; //closest matching string
        private int bestComparisonScore; //score of the closest matching string

        //TODO account for wildcards and "important" words
        public InputParser(int threadNum, string input, bool debugOutput = false)
        {
            this.debugOutput = debugOutput;

            closestStringMatch = "";
            bestComparisonScore = 100;

            string[] stringsToCompare = ParserData.speechOrganizerArray[threadNum].speechInput.inputPhrases;
            float threshold = ParserData.speechOrganizerArray[threadNum].speechInput.threshold;

            for (int i = 0; i < stringsToCompare.Length; i++)   //lowercase all strings for comparison
            {
                stringsToCompare[i] = stringsToCompare[i].ToLower();
            }
            input = input.ToLower();
            //TODO might have to trim out punctuatuion?
            //TODO account for wildcards (such as user's name)


            //=============== Comparison Algorithm ================

            string[] inputWordArray = input.Split(' '); //split input into array of individal words

            //measure closeness of strings of equivalent size
            foreach (string phrase in stringsToCompare) {
                if (inputWordArray.Length <= GetWordCount(phrase))  //if input is shorter than phrase to compare to, just compare
                {
                    UpdateBestScore(LevenshteinDistance(phrase, input), phrase);
                }
                else  //input is longer than phrase, roll through input sentence to see what section is closest
                {
                    int head = 0, tail = GetWordCount(phrase);
                    for (int i = 0; i < (inputWordArray.Length - GetWordCount(phrase)); i++)
                    {
                        string inputSection = String.Join(" ", inputWordArray, head, tail);
                        UpdateBestScore(LevenshteinDistance(phrase, inputSection), phrase+" >> \""+inputSection+"\"");
                    }
                }
            }

            //update ParserData
            ParserData.closestString[threadNum] = closestStringMatch;
            ParserData.closestStringScore[threadNum] = bestComparisonScore;

            //FIXME broken, needs to account for threshold score somehow
            //100 means perfect match required, 50 means half match, 0 means basically anything matches
            //calculate threshold passability
            if (bestComparisonScore >= threshold)
            {
                ParserData.speechOrganizerWasTriggered[threadNum] = true;
            }
        }

        //check if score is higher than current score, update highestComparisonScore & closestStringMatch
        private void UpdateBestScore(int score, string inputString)
        {   //TODO what to do in event of a score tie?
            if (debugOutput == true) { Debug.Log("score: "+score+"; input: "+inputString); }
            if (score == bestComparisonScore)
            {
                Debug.LogWarning("equal closeness: " + closestStringMatch + " & " + inputString);
            }
            if (score < bestComparisonScore)
            {
                bestComparisonScore = score;
                closestStringMatch = inputString;
            }
        }

        //get number of words in a string, ignoring whitespace and punctuation
        private int GetWordCount(string input)
        {
            int wordCount = 0, index = 0;

            while (index < input.Length)
            {
                // check if current char is part of a word
                while (index < input.Length && !char.IsWhiteSpace(input[index]))
                    index++;

                wordCount++;

                // skip whitespace until next word
                while (index < input.Length && char.IsWhiteSpace(input[index]))
                    index++;
            }
            return wordCount;
        }

        //get number of character changes required to make one string equivalent to another
        private int LevenshteinDistance(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                if (string.IsNullOrEmpty(t))
                    return 0;
                return t.Length;
            }

            if (string.IsNullOrEmpty(t))
            {
                return s.Length;
            }

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // initialize the top and right of the table to 0, 1, 2, ...
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 1; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    int min1 = d[i - 1, j] + 1;
                    int min2 = d[i, j - 1] + 1;
                    int min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }
            return d[n, m];
        }

    }
}
