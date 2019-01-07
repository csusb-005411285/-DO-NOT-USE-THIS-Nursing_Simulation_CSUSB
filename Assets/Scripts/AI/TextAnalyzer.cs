using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Parser
{
    //TODO make struct accessable outside of this class
    //analyzes text from input objects to look for keywords or wildcard words
    public static class TextAnalyzer
    {
        public struct analysisData
        {
            public string[] importantWords;
        }

        //return struct of important words
        public static analysisData Analyze(string input)
        {
            string[] inputWordArray = input.Split(' '); //split input into array of individal words

            List<string> importantWordsList = new List<string>();
            foreach (string word in inputWordArray)
            {
                if (IsAllUppercase(word))
                {
                    importantWordsList.Add(word);
                }
            }

            analysisData data;
            data.importantWords = importantWordsList.ToArray();
            return data;
        }

        //check if all characters in string are uppercase
        private static bool IsAllUppercase(string input)
        {
            foreach (char character in input)
            {
                if (!char.IsUpper(character))
                    return false;
            }
            return true;
        }

    }
}
