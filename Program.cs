using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crossword
{
    class Program
    {
        static char[] chararray = { 'q','w','e','r','t','y','u','i','o','p','a','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m'};
        static Dictionary<int, Tuple<string, bool>> words = new Dictionary<int, Tuple<string, bool>>();
        static List<string> fromfile = new List<string>();
        static List<string> quastions = new List<string>();
        static List<int> used = new List<int>();
        static List<int> usedWords = new List<int>();
        static int CurrentWord;
        static int countwords=1;
        static bool did = false;
        public static char[,] map = new char[,]
        { {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},
          {'-', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','-'},
          {'-', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','-'},
          {'-', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','-'},
          {'-', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','-'},
          {'-', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','-'},
          {'-', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','-'},
          {'-', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','-'},
          {'-', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','-'},
          {'-', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','-'},
          {'-', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','-'},
          {'-', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','-'},
          {'-', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','-'},
          {'-', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','-'},
          {'-', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','-'},
          {'-', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','-'},
          {'-', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0','-'},
          {'-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'},};
        static void Main()
        {
            //try
            //{
                ReadWordsFromFile();
                AddToDictAndToMap();
                ReadQuastionsFromFile();
                Paint();
                ShowQuastions();
                Console.WriteLine();
        //}
    //        catch(Exception e)
    //        {
    //            Paint();
    //}
            while (true)
            {
                Console.SetCursorPosition(0, 31);
                int key = Convert.ToInt32(Console.ReadLine());
                if (words.Keys.Contains(key))
                {if (usedWords.Contains(key))
                        Console.WriteLine("You have guesed this word!)");
                    else
                    {
                        Console.WriteLine($"Write an answer for {key} quastion");
                        CurrentWord = key;
                        did = true;
                    }
                }
                else
                {
                    Console.WriteLine($"I dont have {key} quastion");
                }
                if (did)
                {
                    string word = Console.ReadLine();
                    if (word == words[CurrentWord].Item1) { usedWords.Add(CurrentWord); WriteWord(words[CurrentWord], CurrentWord); }
                    Clear();
                }
                did = false;
            }
        }
        public static bool CheckBoundaries(int X, int Y, int lenght, bool vertical)
        {
            int countofnull = 0;
            if (X>18||Y>30) return false;
            if (vertical)
            {
                for (int i = 0; i < lenght; i++)
                {
                    if (Checking( X + i,Y) == null) countofnull++;
                    else { if (Checking(X + i, Y) == false) return false; }
                }
            }
            else
            {
                for (int i = 0; i < lenght; i++)
                {
                    if (Checking(X , Y+i) == null) countofnull++;
                    else { if (Checking(X, Y+i) == false) return false; }
                }
            }
            if (countofnull > 1) return false;
            //else if(countofnull==0) return false;
            else return true;
        }
        public static bool? Checking(int x, int y)
        {
            try
            {
                int count = 0;
                if (map[y,x] == '-') return false;
                if (chararray.Contains(map[y, x])) return null;
                if (map[y,x] == '0')
                {
                    if (chararray.Contains(map[y+1, x])) count++;
                    if (chararray.Contains(map[ y - 1, x])) count++;
                    if (chararray.Contains(map[ y + 1,x + 1])) count++;
                    if (chararray.Contains(map[y - 1,x + 1])) count++;
                    if (chararray.Contains(map[ y + 1, x - 1])) count++;
                    if (chararray.Contains(map[y - 1, x - 1])) count++;
                    if (chararray.Contains(map[ y, x + 1])) count++;
                    if (chararray.Contains(map[y,x-1])) count++;
                    if (count > 2) return false;
                    else return true;
                }
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
       
        static void AddToDictAndToMap()
        {
            foreach (string item in fromfile)
            {
                if (EqualLatter(fromfile[fromfile.Count-1], item))
                {
                    used.Add(countwords);
                    words.Add(countwords, new Tuple<string, bool>(fromfile[0], false));
                    fromfile.Remove(fromfile[0]);
                    fromfile.Remove(item);
                    break;
                }
            }
            AddToMap(words.Last(), 6, 15);
            Dictionary<int, Tuple<string, bool>> Testwords = new Dictionary<int, Tuple<string, bool>>();
            //for (int g = 0; g < fromfile.Count; g++)
            //{
            //    string item = fromfile[g];
            foreach (string item in fromfile)
            {
                foreach (var test in Testwords)
                {
                    if (!words.ContainsKey(test.Key))
                    {
                        words.Add(test.Key, test.Value);
                    }
                }
                Testwords = new Dictionary<int, Tuple<string, bool>>();
                foreach (var word in words)
                //Random rnd = new Random();
                //int key = rnd.Next(1, words.Count);
                //var word = words[key];
                {
                    if (EqualLatter(word.Value.Item1, item)
                        &&
                        !(words.Values.Contains(new Tuple<string, bool>(item,true)) || words.Values.Contains(new Tuple<string, bool>(item, false)))
                        &&
                        !(Testwords.Values.Contains(new Tuple<string, bool>(item, true)) || Testwords.Values.Contains(new Tuple<string, bool>(item, false))))
                    {
                        int i = 0, f = -1;
                        foreach (char c in item)
                        {
                            if (f != -1) break;
                            i++;
                            for (int j = 0; j < word.Value.Item1.Length; j++)
                            {
                                if (word.Value.Item1[j] == c) {f = j; break; }
                            }
                        }
                        var point = FindCoordOfNumber(word.Key);
                        //if (point == null) point = FindCoordOfNumber(Testwords.Last().Key);
                        if (word.Value.Item2)
                        {
                            if (CheckBoundaries(point.Item1 + i, point.Item2 - f, item.Length, !word.Value.Item2))
                            {
                                used.Add(countwords);
                                Testwords.Add(countwords, new Tuple<string, bool>(item, !word.Value.Item2));
                                AddToMap(Testwords.Last(), point.Item1 + i, point.Item2 - f);
                                continue;
                            }
                        }
                        else
                        {
                            if (CheckBoundaries(point.Item1 + f, point.Item2-i+1, item.Length, !word.Value.Item2))
                            {
                                used.Add(countwords);
                                Testwords.Add(countwords, new Tuple<string, bool>(item, !word.Value.Item2));
                                AddToMap(Testwords.Last(), point.Item1 + f , point.Item2 - i+1);
                                continue;
                            }
                        }
                    }
                }
            }
        }
        static void Clear()
        {
            for (int i = 0; i < 60; i++)
            {
                for (int f = 31; f < 35; f++)
                {
                    Console.SetCursorPosition(i, f);
                    Console.Write(" ");

                }
            }
        }
        static void ShowQuastions()
        {
            for (int i = 0; i < quastions.Count; i++)
            {
                Console.SetCursorPosition(55, i);
                Console.WriteLine(i + 1 + ". " + quastions[i]);
            }
        }
        static void WriteWord(Tuple<string, bool> word, int number)
        {
            Tuple<int, int> point = FindCoordOfNumber(number);
            if (word.Item2)//true == vertical
            {
                for (int i = 0; i < word.Item1.Length; i++)
                {
                    Console.SetCursorPosition(point.Item1 * 3 + 1, point.Item2 + i);
                    Console.Write(word.Item1[i]);
                }
            }
            else
            {
                for (int i = 0; i < word.Item1.Length; i++)
                {
                    Console.SetCursorPosition(point.Item1 * 3 + i * 3 + 1, point.Item2);
                    Console.Write(word.Item1[i]);

                }
            }
        }
        static Tuple<int, int> FindCoordOfNumber(int number)
        {
            for (int i = 0; i < 18; i++)
            {
                for (int f = 0; f < 31; f++)
                {
                    if (map[i, f].ToString() == number.ToString()) return new Tuple<int, int>(i, f);
                }
            }
            return null;
        }
        static void AddToMap(KeyValuePair<int,Tuple<string, bool>> keyValuePair, int X, int Y)
        {
            countwords++;
            map[X, Y] = Inttochar(keyValuePair.Key);
            if (keyValuePair.Value.Item2)
            {
                for (int i = 1; i < keyValuePair.Value.Item1.Length; i++)
                {
                    if(map[X, Y + i]=='0') map[X, Y + i] = keyValuePair.Value.Item1[i];
                }
            }
            else
            {
                for (int i = 1; i < keyValuePair.Value.Item1.Length; i++)
                {
                    if (map[X+i, Y] == '0')  map[X + i, Y] = keyValuePair.Value.Item1[i];
                }
            }

        }
        static char Inttochar(int n)
        {
            switch (n)
            {
                case 1:return '1';
                case 2: return '2'; 
                case 3: return '3';
                case 4: return '4';
                case 5: return '5';
                case 6: return '6';
                case 7: return '7';
                case 8: return '8';
                case 9: return '9';
                default:return '0';
            }
        }
        static void ReadWordsFromFile()
        {
            string path = @"C:\Users\Олег\source\repos\Crossword\Words.txt";
            foreach (string word in File.ReadAllLines(path))
            {
                fromfile.Add(word);
            }
        }
        static void ReadQuastionsFromFile()
        {
            string path = @"C:\Users\Олег\source\repos\Crossword\Quastions.txt";
            foreach (string qua in File.ReadAllLines(path))
            {
                quastions.Add(qua);
            }
        }
        static void Paint()
        {
            for (int i = 0; i < 18; i++)
            {
                for (int f = 0; f < 31; f++)
                {
                    Console.SetCursorPosition(i * 3, f);
                    if (map[i, f] == '0') { Console.Write(" "); }
                    else if (map[i, f] == '-') { Console.Write("*"); }
                    else if (int.TryParse(map[i, f].ToString(), out int n)) { Console.SetCursorPosition(i * 3 , f); Console.WriteLine($"[{n}]"); }
                    else Console.Write("[ ]");

                }
            }
        }
        static bool EqualLatter(string first,string second)
        {
            foreach(char c in first)
            {
                if (second.Contains(c)) return true;
            }
            return false;
        }
    }
}
