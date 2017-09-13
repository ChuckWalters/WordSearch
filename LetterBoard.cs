using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WordSearchAPI
{
    class LetterBoard
    {
        /// <summary>
        /// Duplicate letters to add more chance for a letter
        /// </summary>
        private char[] letters = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        List<List<Char>> _board;
        public List<String> ValidWords = new List<string>();
        

        public int Width { get{ return _board[0].Count; } }
        public int Height { get { return _board.Count; } }

        public bool IsOutOfBounds(int row, int col)
        {
            return row < 0 || col < 0 || row >= Height || col >= Width;
        }

        public void Load(String path)
        {
            _board = new List<List<char>>(4);

            foreach (string line in File.ReadLines(path))
            {
                String[] letters = line.Split(',');
                _board.Add(new List<char>(letters.Length));
                for(int i=0; i<letters.Length; i++)
                {
                    _board[_board.Count - 1].Add(Convert.ToChar(letters[i]));
                }
            }  
        }
        public void CreateRandom(int rows, int cols)
        {
            Random rand = new Random();

            _board = new List<List<char>>(rows);
            for(int r=0; r<rows; r++)
            {
                _board.Add(new List<char>(cols));
                for(int c=0; c<cols; c++)
                {
                    _board[r].Add(letters[rand.Next(0, letters.Length)]);
                }
            }
        }

        public void Print()
        {
            for(int row=0; row<Height; row++)
            {
                for(int col=0; col<Width; col++)
                {
                    Console.Write(" " + _board[row][col]);
                }
                Console.WriteLine();
            }
        }

        private bool[,] visited;
        public void FindWords()
        {
            visited = new bool[Height, Width];
            StringBuilder word = new StringBuilder();

            for(int r=0; r<Height; r++)
            {
                for(int c=0; c<Width; c++)
                {
                    word.Clear();
                    BoardWalk(word, r, c);
                }
            }
        }

        private void BoardWalk(StringBuilder word, int row, int col)
        {            
            if (IsOutOfBounds(row, col))
            {
                return; 
            }

            if (visited[row, col])
            {
                return; 
            }

            visited[row, col] = true;
            word.Append(_board[row][col]);

            if(WordTree.IsStartOfWord(word, out bool isWord))
            {
                if (isWord)
                {
                    AddValidWord(word.ToString());
                }

                BoardWalk(word, row - 1, col);      //N
                BoardWalk(word, row - 1, col+1);    //NE
                BoardWalk(word, row, col + 1);      // E
                BoardWalk(word, row + 1, col + 1);  //SE
                BoardWalk(word, row + 1, col);      //S
                BoardWalk(word, row + 1, col - 1);  //SW
                BoardWalk(word, row, col - 1);      // W
                BoardWalk(word, row - 1, col - 1);  //NW
            }

            word.Remove(word.Length - 1, 1);
            visited[row, col] = false;
        }

        private void AddValidWord(String s)
        {
            if (ValidWords.Contains(s)) 
            {
                return; // Skip Duplicates
            }
            ValidWords.Add(s);
        }

        public void PrintValidWords()
        {
            String outputFileName = "wordsFound.txt";

            Console.WriteLine();
            Console.WriteLine("Total words found = " + ValidWords.Count);

            if (ValidWords.Count > 0)
            {
                Console.WriteLine("Writing to file: " + outputFileName);
                File.WriteAllLines(outputFileName, ValidWords);
            }
        }

    }
}
