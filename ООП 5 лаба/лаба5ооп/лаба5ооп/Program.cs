using System;
using System.Text.RegularExpressions;

namespace Lab5
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int choice = 0;
            while (choice != 4)
            {
                Console.WriteLine();
                Console.WriteLine("ГЛАВНОЕ МЕНЮ");
                Console.WriteLine("1 - Двумерный массив");
                Console.WriteLine("2 - Рваный массив");
                Console.WriteLine("3 - Строка");
                Console.WriteLine("4 - Выход");
                choice = ReadInt("Выбор: ", 1, 4);

                if (choice == 1) Menu2D();
                if (choice == 2) MenuJag();
                if (choice == 3) MenuStr();
            }
        }

       
        static int ReadInt(string text, int min, int max)
        {
            int x;
            while (true)
            {
                Console.Write(text);
                try
                {
                    x = int.Parse(Console.ReadLine());
                    if (x >= min && x <= max) return x;
                    Console.WriteLine("Число должно быть от " + min + " до " + max);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введи целое число");
                }
            }
        }
        static void Menu2D()
        {
            int[,] matr = null;
            int choice = 0;
            while (choice != 4)
            {
                Console.WriteLine();
                Console.WriteLine("Двумерный массив");
                Console.WriteLine("1 - Создать случайно");
                Console.WriteLine("2 - Ввести вручную");
                Console.WriteLine("3 - Добавить столбцы после чётных");
                Console.WriteLine("4 - Назад");
                choice = ReadInt("Выбор: ", 1, 4);

                if (choice == 1) { matr = Random2D(); Show(matr); }
                if (choice == 2) { matr = Manual2D(); Show(matr); }
                if (choice == 3)
                {
                    if (matr == null) Console.WriteLine("Массив не создан");
                    else { matr = AddCols(matr); Show(matr); }
                }
            }
        }

        static int[,] Random2D()
        {
            int rows = ReadInt("Строк: ", 1, 10);
            int cols = ReadInt("Столбцов: ", 1, 10);
            Random rnd = new Random();
            int[,] m = new int[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    m[i, j] = rnd.Next(1, 10);
            return m;
        }

        static int[,] Manual2D()
        {
            int rows = ReadInt("Строк: ", 1, 10);
            int cols = ReadInt("Столбцов: ", 1, 10);
            int[,] m = new int[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    m[i, j] = ReadInt("Элемент: ", -100, 100);
            return m;
        }
        static void Show(int[,] m)
        {
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                    Console.Write(m[i, j] + " ");
                Console.WriteLine();
            }
        }
        static int[,] AddCols(int[,] m)
        {
            int rows = m.GetLength(0);
            int cols = m.GetLength(1);

            int newCols = cols;
            for (int j = 0; j < cols; j++)
                if (j % 2 == 0) newCols++;

            int[,] res = new int[rows, newCols];
            int nc = 0;
            for (int j = 0; j < cols; j++)
            {
                for (int i = 0; i < rows; i++)
                    res[i, nc] = m[i, j];
                nc++;
                if (j % 2 == 0)
                {
                    for (int i = 0; i < rows; i++)
                        res[i, nc] = 0;
                    nc++;
                }
            }
            return res;
        }
        static void MenuJag()
        {
            int[][] jag = null;
            int choice = 0;
            while (choice != 4)
            {
                Console.WriteLine();
                Console.WriteLine("Рваный массив");
                Console.WriteLine("1 - Создать случайно");
                Console.WriteLine("2 - Ввести вручную");
                Console.WriteLine("3 - Добавить K строк начиная с N");
                Console.WriteLine("4 - Назад");
                choice = ReadInt("Выбор: ", 1, 4);

                if (choice == 1) { jag = RandomJag(); Show(jag); }
                if (choice == 2) { jag = ManualJag(); Show(jag); }
                if (choice == 3)
                {
                    if (jag == null) Console.WriteLine("Массив не создан");
                    else
                    {
                        int n = ReadInt("С какого номера: ", 1, jag.Length);
                        int k = ReadInt("Сколько строк: ", 1, 10);
                        jag = AddRows(jag, n, k);
                        Show(jag);
                    }
                }
            }
        }

        static int[][] RandomJag()
        {
            int rows = ReadInt("Строк: ", 1, 10);
            Random rnd = new Random();
            int[][] jag = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                int cols = rnd.Next(2, 6);
                jag[i] = new int[cols];
                for (int j = 0; j < cols; j++)
                    jag[i][j] = rnd.Next(1, 10);
            }
            return jag;
        }

        static int[][] ManualJag()
        {
            int rows = ReadInt("Строк: ", 1, 10);
            int[][] jag = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                int cols = ReadInt("Столбцов в строке: ", 1, 10);
                jag[i] = new int[cols];
                for (int j = 0; j < cols; j++)
                    jag[i][j] = ReadInt("Элемент: ", -100, 100);
            }
            return jag;
        }
        static void Show(int[][] jag)
        {
            for (int i = 0; i < jag.Length; i++)
            {
                Console.Write("Строка " + (i + 1) + ": ");
                for (int j = 0; j < jag[i].Length; j++)
                    Console.Write(jag[i][j] + " ");
                Console.WriteLine();
            }
        }
        static int[][] AddRows(int[][] jag, int n, int k)
        {
            Random rnd = new Random();
            int oldCount = jag.Length;
            int[][] res = new int[oldCount + k][];
            int pos = 0;

            for (int i = 0; i < n - 1; i++)
            {
                res[pos] = jag[i];
                pos++;
            }

            for (int i = 0; i < k; i++)
            {
                int cols = rnd.Next(2, 6);
                res[pos] = new int[cols];
                for (int j = 0; j < cols; j++)
                    res[pos][j] = rnd.Next(1, 10);
                pos++;
            }

            for (int i = n - 1; i < oldCount; i++)
            {
                res[pos] = jag[i];
                pos++;
            }
            return res;
        }
        // строкв
        static void MenuStr()
        {
            string text = null;
            int choice = 0;
            while (choice != 4)
            {
                Console.WriteLine();
                Console.WriteLine("Работа со строкой");
                Console.WriteLine("1 - Ввести строку");
                Console.WriteLine("2 - Тестовая строка");
                Console.WriteLine("3 - Удалить предложения с '!'");
                Console.WriteLine("4 - Назад");
                choice = ReadInt("Выбор: ", 1, 4);

                if (choice == 1)
                {
                    Console.Write("Введите текст: ");
                    text = Console.ReadLine();
                }
                if (choice == 2)
                {
                    text = "В лесу родилась елочка! В лесу она росла. Зимой и летом стройная, зеленая была!";
                    Console.WriteLine("Взято: " + text);
                }
                if (choice == 3)
                {
                    if (text == null) Console.WriteLine("Строка не введена!");
                    else
                    {
                        Console.WriteLine("Было: " + text);
                        Console.WriteLine("Стало: " + Remove(text));
                    }
                }
            }
        }
        static string Remove(string text)
        {
            MatchCollection mc = Regex.Matches(text, @"[^.!?]+[.!?]");
            string res = "";
            for (int i = 0; i < mc.Count; i++)
            {
                string s = mc[i].Value.Trim();
                if (s[s.Length - 1] != '!')
                    res = res + s + " ";
            }
            return res.Trim();
        }
    }
}