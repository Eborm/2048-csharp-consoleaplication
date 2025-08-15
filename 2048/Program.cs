using System.Numerics;

namespace _2048
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, List<int>> storage = new Dictionary<int, List<int>>
            {
                { 0, new List<int>(){ 0, 0, 0, 0 } },
                { 1, new List < int >() { 0, 0, 0, 0 } },
                { 2, new List < int >() { 0, 0, 0, 0 } },
                { 3, new List < int >() { 0, 0, 0, 0 } }
            }
            ;

            CustomFunc _usethis = new CustomFunc();

            Console.WriteLine("Welcome to my console representation of 2048");

            while (true)
            {
                Console.Clear();

                List<int> all_numbers_wincheck = new List<int>();
                for (int i = 0; i < 3; i++)
                {
                    foreach (var num in storage[i])
                    {
                        all_numbers_wincheck.Add(num);
                    }
                }
                if (all_numbers_wincheck.Contains(2048))
                {
                    Console.WriteLine("Congratulations! You've reached 2048!");
                    Console.ReadKey();
                    return;
                }


                bool successfully_added = false;
                int amount_of_checks = 0;

                while (successfully_added != true)
                {
                    amount_of_checks++;
                    int number = _usethis.numbergenerator();
                    int row = _usethis.getrandomlocation();
                    int column = _usethis.getrandomlocation();

                    var column_storage = storage[row];

                    if (column_storage[column] == 0)
                    {
                        column_storage[column] = number;
                        successfully_added = true;
                    }
                    else if (amount_of_checks > 20)
                    {
                        List<int> all_numbers = new List<int>();
                        for (int i = 0; i < 3; i++)
                        {
                            foreach (var num in storage[i])
                            {
                                all_numbers.Add(num);
                            }
                        }
                        if (all_numbers_wincheck.Contains(2048) == false)
                        {
                            Console.WriteLine("No more space available to add a new number.");
                            Console.WriteLine("Game Over! No more moves available.");
                            Console.ReadKey();
                            return;
                        }
                    }
                }


                _usethis.printboard(storage);
                int direction = _usethis.getdirection(); //0 = up, 1 = right, 2 = down, 3 = left
                if (direction == 0)
                {
                    Dictionary<int, List<int>> rotated = new Dictionary<int, List<int>>();
                    for (int i = 0; i < 4; i++)
                    {
                        List<int> new_column = new List<int>();
                        for (int j = 3; j > -1; j--)
                        {
                            new_column.Add(storage[j][i]);
                        }
                        rotated.Add(i, new_column);
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        List<int> column_storage = rotated[i];
                        for (int j = 0; j < 4; j++)
                        {
                            for (int k = 3; k > -1; k -= 1)
                            {
                                int currentindex = k;
                                int nextindex = k + 1;
                                if (currentindex != 3)
                                {
                                    if (column_storage[nextindex] == 0)
                                    {
                                        column_storage[nextindex] = column_storage[currentindex];
                                        column_storage[currentindex] = 0;
                                    }
                                    else if (column_storage[nextindex] == column_storage[currentindex])
                                    {
                                        column_storage[nextindex] = column_storage[nextindex] * 2;
                                        column_storage[currentindex] = 0;
                                    }
                                }
                            }
                        }
                        rotated[i] = column_storage;
                    }
                    for (int k = 0; k < 3; k++)
                    {
                        Dictionary<int, List<int>> rotated_back = new Dictionary<int, List<int>>();
                        for (int i = 0; i < 4; i++)
                        {
                            List<int> new_column = new List<int>();
                            for (int j = 3; j > -1; j--)
                            {
                                new_column.Add(rotated[j][i]);
                            }
                            rotated_back.Add(i, new_column);
                        }
                        rotated = rotated_back;
                    }
                    storage = rotated;
                }
                else if (direction == 1)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        List<int> column_storage = storage[i];
                        for (int j = 0; j < 4; j++)
                        {
                            for (int k = 3; k > -1; k -= 1)
                            {
                                int currentindex = k;
                                int nextindex = k + 1;
                                if (currentindex != 3)
                                {
                                    if (column_storage[nextindex] == 0)
                                    {
                                        column_storage[nextindex] = column_storage[currentindex];
                                        column_storage[currentindex] = 0;
                                    }
                                    else if (column_storage[nextindex] == column_storage[currentindex])
                                    {
                                        column_storage[nextindex] = column_storage[nextindex] * 2;
                                        column_storage[currentindex] = 0;
                                    }
                                }
                            }
                        }
                        storage[i] = column_storage;
                    }
                }
                else if (direction == 2)
                {
                    Dictionary<int, List<int>> rotated = new Dictionary<int, List<int>>();
                    for (int i = 0; i < 4; i++)
                    {
                        List<int> new_column = new List<int>();
                        for (int j = 3; j > -1; j--)
                        {
                            new_column.Add(storage[j][i]);
                        }
                        rotated.Add(i, new_column);
                    }
                    for (int k = 0; k < 2; k++)
                    {
                        Dictionary<int, List<int>> rotated_back_2 = new Dictionary<int, List<int>>();
                        for (int i = 0; i < 4; i++)
                        {
                            List<int> new_column = new List<int>();
                            for (int j = 3; j > -1; j--)
                            {
                                new_column.Add(rotated[j][i]);
                            }
                            rotated_back_2.Add(i, new_column);
                        }
                        rotated = rotated_back_2;
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        List<int> column_storage = rotated[i];
                        for (int j = 0; j < 4; j++)
                        {
                            for (int k = 3; k > -1; k -= 1)
                            {
                                int currentindex = k;
                                int nextindex = k + 1;
                                if (currentindex != 3)
                                {
                                    if (column_storage[nextindex] == 0)
                                    {
                                        column_storage[nextindex] = column_storage[currentindex];
                                        column_storage[currentindex] = 0;
                                    }
                                    else if (column_storage[nextindex] == column_storage[currentindex])
                                    {
                                        column_storage[nextindex] = column_storage[nextindex] * 2;
                                        column_storage[currentindex] = 0;
                                    }
                                }
                            }
                        }
                        rotated[i] = column_storage;
                    }
                    Dictionary<int, List<int>> rotated_back = new Dictionary<int, List<int>>();
                    for (int i = 0; i < 4; i++)
                    {
                        List<int> new_column = new List<int>();
                        for (int j = 3; j > -1; j--)
                        {
                            new_column.Add(rotated[j][i]);
                        }
                        rotated_back.Add(i, new_column);
                    }
                    rotated = rotated_back;
                    storage = rotated;
                }
                else if (direction == 3)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        List<int> column_storage = storage[i];
                        for (int j = 0; j < 4; j++)
                        {
                            for (int k = 0; k < 4; k++)
                            {

                                int currentindex = k;
                                int nextindex = k - 1;
                                if (currentindex != 0)
                                {

                                    if (column_storage[nextindex] == 0)
                                    {
                                        column_storage[nextindex] = column_storage[currentindex];
                                        column_storage[currentindex] = 0;
                                    }
                                    else if (column_storage[nextindex] == column_storage[currentindex])
                                    {
                                        column_storage[nextindex] = column_storage[nextindex] * 2;
                                        column_storage[currentindex] = 0;
                                    }
                                }
                            }
                        }
                        storage[i] = column_storage;
                    }
                }
            }





        }

        internal class CustomFunc
        {
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

            public int getrandomlocation()
            {
                Random random = new Random();
                int location = random.Next(0, 4); //0, 1, 2, or 3
                return location;
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
        }
    }

}
