using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordSearchAPI
{
    class WordTree
    {
        class LetterNode
        {
            public bool IsWord = false;
            public Dictionary<Char, LetterNode> Letter;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="size">Warning!!! keep small. </param>
            public LetterNode(int size)
            {
                Letter = new Dictionary<char, LetterNode>(size);
            }

        }

        static LetterNode _root;

        public static void LoadDictionary(String path, int minWordLength)
        {
            _root = new LetterNode(26);

            foreach(string word in File.ReadLines(path))
            {
                if(word.Length < minWordLength)
                {
                    continue;
                }
                AddWord(word);
            }
            
        }

        /// <summary>
        /// Add a word to the tree _root
        /// </summary>
        /// 
        /// <param name="s">word to add</param>
        private static void AddWord(String s)
        {
            LetterNode node = _root;

            for (int i = 0; i < s.Length; i++)
            {
                LetterNode next;
                if (false == node.Letter.TryGetValue(s[i], out next))
                {
                    next = new LetterNode(2);
                    node.Letter.Add(s[i], next);
                }
                node = next;
            }
            node.IsWord = true; // mark node as being the end of a valid word
        }

        /// <summary>
        /// Check if a string is the beginning of a valid word and if it is currently a valid word.
        /// </summary>
        /// <param name="s">word to check</param>
        /// <param name="isWord">true if string is a valid word</param>
        /// <returns>true if string is a valid start to a word</returns>
        public static bool IsStartOfWord(StringBuilder s, out bool isWord)
        {
            LetterNode node = _root;

            for(int i=0; i<s.Length; i++)
            {
                LetterNode next;
                if (false == node.Letter.TryGetValue(s[i], out next))
                {
                    isWord = false;
                    return false;
                }
                node = next;
            }
            isWord = node.IsWord;
            return true;
        }

        public static bool IsWord(StringBuilder s)
        {
            IsStartOfWord(s, out bool isWord);
            return isWord;
        }
    }
}
