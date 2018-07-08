using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace BadPredictiveText
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean moreInput = true;
            String filename;
            Dictionary<String, Word> dict = new Dictionary<string, Word>();

            Console.Write("What file should text be read from?: ");
            filename = Console.ReadLine();


            while (moreInput)
            {
                if (String.IsNullOrWhiteSpace(filename))
                {
                    moreInput = false;
                    continue;
                }
                //string[] lines = System.IO.File.ReadAllLines("Text.txt");
                if (filename.IndexOf('.') < 0)
                {
                    filename = filename + ".txt";
                }
                string lines = System.IO.File.ReadAllText(filename);

                while (lines.IndexOf("[") > -1)
                {
                    int start = lines.IndexOf("[");
                    int end = lines.IndexOf("]");
                    lines = lines.Replace(lines.Substring(start, (end - start + 1)), "");
                }
                //Console.WriteLine(lines);
                string[] words = lines.Split(' ', ',', '.', '\n', '(', ')', '!', ';', '?', ':', '-', '"');
                
                Word prevword = null;
                foreach (string word in words)
                {
                    if (word == "" || word == " " || word == "\n" || String.IsNullOrWhiteSpace(word))
                    {
                        continue;
                    }
                    if (prevword != null)
                    {
                        prevword.addNextWord(word);
                    }
                    prevword = null;
                    if (word != "END")
                    {
                        if (!dict.ContainsKey(word.ToLower()))
                        {
                            dict.Add(word.ToLower(), new Word(word));
                        }
                        dict.TryGetValue(word.ToLower(), out prevword);
                    }
                }

                Console.WriteLine("Input processing finished.");
                Console.Write("What file should text be read from next? (Submit blank line if finished): ");
                filename = Console.ReadLine();

            }

            Console.WriteLine("Generating output...");

            String w = "we";
            Word wor;
            StreamWriter sw = new StreamWriter("song.txt");
            while(w!="END")
            {
                sw.Write(w + " ");
                dict.TryGetValue(w, out wor);
                w = wor.getNextWord();
                Thread.Sleep(15);
            }
            sw.Flush();
            Console.WriteLine("Output processing finished.");
            #if DEBUG
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
            #endif
        }
    }
}
