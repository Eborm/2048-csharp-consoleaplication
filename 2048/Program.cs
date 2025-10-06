using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Net;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _2048
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool notwin = true;
            Dictionary<int, List<int>> storage = new Dictionary<int, List<int>>
            {
                { 0, new List<int>(){ 0, 0, 0, 0 } },
                { 1, new List < int >() { 0, 0, 0, 0 } },
                { 2, new List < int >() { 0, 0, 0, 0 } },
                { 3, new List < int >() { 0, 0, 0, 0 } }
            };

            List<int> freespots = new List<int>(Enumerable.Range(0, 16)); //Keep track of what spots are free
            List<int> trackinglist = new List<int>() { // 0 = empty 1 = filled
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0
            };

            CustomFunc _usethis = new CustomFunc();
            Console.WriteLine("Welcome to my console representation of 2048");
            Console.ReadKey();

            while (notwin)
            {
                Console.Clear();
                for (int i = 0; i < 4; i++)
                {
                    foreach (var num in storage[i])
                    {
                        if (num == 2048)
                        {
                            Console.WriteLine("Congratulations! You've reached 2048!");
                            Console.ReadKey();
                            notwin = false;
                            break;
                        }
                    }
                }
                if (freespots.Count == 0)
                {
                    Console.WriteLine("Game Over! No more moves left.");
                    Console.ReadKey();
                    break;
                }

                var _location = freespots[_usethis.getrandomspot(freespots)]; //0-3 first row , 4-7 second row, 8-11 third row, 12-15 fourth row
                int _number = _usethis.numbergenerator();

                decimal tempnum = _location/4;
                int _column = _location % 4;
                int _row = (int)Math.Floor(tempnum);
                var _column_storage = storage[_row];
                _column_storage[_column] = _number;
                freespots.Remove(_location);
                trackinglist[_location] = 1;

                _usethis.printboard(storage);

                last_round = storage;

                int direction = _usethis.getdirection(); //0 = up, 1 = right, 2 = down, 3 = left

                switch (direction)
                {
                    case 0:
                        // up
                        for (int i = 0; i < 4; i++) // i = row
                        {
                            for (int j = 3; j > -1; j--) // j = column
                            {
                                int list_index = j + i * 4;
                                if (list_index != 0 && list_index != 1 && list_index != 2 && list_index != 3)
                                {
                                    int move_index = list_index - 4;
                                    List<int> moveindexinij = _usethis.TranslateIndexToI_J(move_index);
                                    if (trackinglist[move_index] == 0 && trackinglist[list_index] == 1)
                                    {
                                        trackinglist[move_index] = 1;
                                        trackinglist[list_index] = 0;
                                        freespots.Add(list_index);
                                        freespots.Remove(move_index);
                                        storage[moveindexinij[0]][moveindexinij[1]] = storage[i][j];
                                        storage[i][j] = 0;
                                    }
                                    else if (storage[i][j] == storage[moveindexinij[0]][moveindexinij[1]] && trackinglist[list_index] != 0)
                                    {
                                        trackinglist[move_index] = 1;
                                        trackinglist[list_index] = 0;
                                        storage[moveindexinij[0]][moveindexinij[1]] = storage[i][j] * 2;
                                        storage[i][j] = 0;
                                        freespots.Add(list_index);
                                    }
                                }
                            }
                        }
                        break;
                    case 1:
                        // right
                        for (int i = 3; i > -1; i--) // i = row
                        {
                            for (int j = 3; j > -1; j--) // j = column
                            {
                                int list_index = j + i * 4;
                                if (list_index != 3 && list_index != 7 && list_index != 11 && list_index != 15)
                                {
                                    int move_index = list_index + 1;
                                    if (trackinglist[move_index] == 0 && trackinglist[list_index] == 1)
                                    {
                                        trackinglist[move_index] = 1;
                                        trackinglist[list_index] = 0;
                                        freespots.Add(list_index);
                                        freespots.Remove(move_index);
                                        storage[i][j + 1] = storage[i][j];
                                        storage[i][j] = 0;
                                    }
                                    else if (storage[i][j] == storage[i][j + 1] && trackinglist[list_index] != 0)
                                    {
                                        trackinglist[move_index] = 1;
                                        trackinglist[list_index] = 0;
                                        storage[i][j] = 0;
                                        storage[i][j + 1] = storage[i][j + 1] * 2;
                                        freespots.Add(list_index);
                                    }
                                }
                            }
                        }
                        break;
                    case 2:
                        //down
                        for (int i = 3; i > -1; i--) // i = row
                        {
                            for (int j = 3; j > -1; j--) // j = column
                            {
                                int list_index = j + i * 4;
                                if (list_index != 12 && list_index != 13 && list_index != 14 && list_index != 15)
                                {
                                    int move_index = list_index + 4;
                                    List<int> moveindexinij = _usethis.TranslateIndexToI_J(move_index);
                                    if (trackinglist[move_index] == 0 && trackinglist[list_index] == 1)
                                    {
                                        trackinglist[move_index] = 1;
                                        trackinglist[list_index] = 0;
                                        freespots.Add(list_index);
                                        freespots.Remove(move_index);
                                        storage[moveindexinij[0]][moveindexinij[1]] = storage[i][j];
                                        storage[i][j] = 0;
                                    }
                                    else if (storage[i][j] == storage[moveindexinij[0]][moveindexinij[1]] && trackinglist[list_index] != 0)
                                    {
                                        trackinglist[move_index] = 1;
                                        trackinglist[list_index] = 0;
                                        storage[moveindexinij[0]][moveindexinij[1]] = storage[i][j] * 2;
                                        storage[i][j] = 0;
                                        freespots.Add(list_index);
                                    }
                                }
                            }
                        }
                        break;
                    case 3:
                        //left
                        for (int i = 0; i<4; i++) // i = row
                        {
                            for (int j = 0; j<4; j++) // j = column
                            {
                                int list_index = j + i * 4;
                                if (list_index != 0 && list_index != 4 && list_index != 8 && list_index != 12)
                                {
                                    int move_index = list_index - 1;
                                    if (trackinglist[move_index] == 0 && trackinglist[list_index] == 1)
                                    {
                                        trackinglist[move_index] = 1;
                                        trackinglist[list_index] = 0;
                                        freespots.Add(list_index);
                                        freespots.Remove(move_index);
                                        storage[i][j - 1] = storage[i][j];
                                        storage[i][j] = 0;
                                    }
                                    else if (storage[i][j] == storage[i][j-1] && trackinglist[list_index] != 0)
                                    {
                                        trackinglist[move_index] = 1;
                                        trackinglist[list_index] = 0;
                                        storage[i][j] = 0;
                                        storage[i][j - 1] = storage[i][j-1] * 2;
                                        freespots.Add(list_index);
                                    }
                                }
                            }
                        }
                        break          
                }
            }
        }

        internal class CustomFunc
        {
            Random mainrandom = new Random();
            public int numbergenerator()
            {
                Random random = new Random();
                int number = random.Next(1, 3); //make 2 twice as common as 4
                if (number == 1)
                {
                    return 4;
                }
                else
                {
                    return 2;
                }
            }

            public int getrandomspot(List<int>freespots)
            {
                return mainrandom.Next(0, freespots.Count); //0-15
            }

            public void printboard(Dictionary<int, List<int>> storage)
            {
                for (int i = 0; i < 4; i++)
                {
                    List<string> list = storage[i].Select(x => x.ToString()).ToList();
                    for (int j = 0; j < 4; j++)
                    {
                        if (list[j] == "0")
                        {
                            list[j] = "-";
                        }
                    }
                    Console.WriteLine($"{list[0]} | {list[1]} | {list[2]} | {list[3]}");
                }
            }

            public int getdirection()
            {
                int direction = 0; //0 = up, 1 = right, 2 = down, 3 = left
                var key = Console.ReadKey(false).Key;

                switch (key.ToString())
                {
                    case "UpArrow":
                        direction = 0;
                        break;
                    case "RightArrow":
                        direction = 1;
                        break;
                    case "DownArrow":
                        direction = 2;
                        break;
                    case "LeftArrow":
                        direction = 3;
                        break;
                }
                return direction;
            }

            public List<int> TranslateIndexToI_J(int index)
            {
                List<int> result = new List<int>();
                int i = index / 4;
                int j = index % 4;
                result.Add(i);
                result.Add(j);
                return result;
            }
        }
    }
}