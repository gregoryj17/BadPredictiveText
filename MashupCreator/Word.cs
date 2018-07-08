using System;
using System.Collections.Generic;
using System.Text;

namespace BadPredictiveText
{
    public class Word
    {
        String word;
        List<String> nextWords;

        public Word()
        {
            nextWords = new List<String>();
        }

        public Word(String w)
        {
            word = w.ToLower();
            nextWords = new List<String>();
        }

        public Word(String w, params String[] words)
        {
            word = w.ToLower();
            nextWords = new List<String>();
            foreach(string wo in words)
            {
                addNextWord(wo);
            }
        }

        public String getWord()
        {
            return word;
        }

        public void addNextWord(String newWord)
        {
            if (newWord != "END")
            {
                nextWords.Add(newWord.ToLower());
            }
            else
            {
                nextWords.Add(newWord);
            }
        }

        public void addNextWords(params String[] words)
        {
            foreach (String w in words)
            {
                addNextWord(w);
            }
        }
        public String getNextWord()
        {
            Random rand = new Random();
            return nextWords[rand.Next(nextWords.Count)];
        }
    }
}
