using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace Coding.Exercise
{
    public class Sentence
    {
        List<string> splittedText;
        WordToken[] wordTokens;
        public Sentence(string plainText)
        {
            splittedText = plainText.Split(" ").ToList();
            wordTokens = new WordToken[splittedText.Count];
        }

        public WordToken this[int index]
        {
            get
            {
                if(wordTokens[index] == null)
                    wordTokens[index] = new WordToken();
                return wordTokens[index]; 

            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            List<string> convertedWords = new List<string>();
            for (var index = 0; index < splittedText.Count; index++)
            {
                var word = splittedText[index];
                if (this[index].Capitalize == true)
                    word = word.ToUpper();
                sb.Append(word);
                if (index != splittedText.Count - 1)
                    sb.Append(" ");
            }

            return sb.ToString();
        }

        public class WordToken
        {
            public bool Capitalize;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var sentance = new Sentence("Hello world");
            sentance[1].Capitalize = true;
            
            WriteLine(sentance);
            ReadKey();
        }
    }
}
