using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearchAPI
{
    class Program
    {
        static String _dictionaryPath;
        static String _boardPath;

        static void ParseArgs(string[] args)
        {
            for(int i=0; i<args.Length; i++)
            {
                String[] opt = args[i].Split('=');
                if (opt[0].Equals("words"))
                {
                    _dictionaryPath = opt[1];
                }else if (opt[0].Equals("board"))
                {
                    _boardPath = opt[1];
                }
            }
        }

        static void Main(string[] args)
        {

            ParseArgs(args);

            if (null == _dictionaryPath)
            {
                Console.WriteLine("Error: must provide dictionary path as arg e.g.,  words=Data/twl.txt");
                Console.ReadKey();
                return;
            }

            int minWordLength = 3;
            WordTree.LoadDictionary(_dictionaryPath, minWordLength);

            LetterBoard board = new LetterBoard();
            if(null == _boardPath)
            {
                Console.WriteLine("Warning: missing   board=Data/4x4.txt  generating random board");
                board.CreateRandom(6, 6);
            }
            else
            {
                board.Load(_boardPath);
            }
            board.Print();

            board.FindWords();
            board.PrintValidWords();

            Console.ReadKey();
        }        
    }
}
