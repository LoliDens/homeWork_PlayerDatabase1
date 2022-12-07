using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homeWork_PlayerDatabase
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            database.Work();         
        }
    }

    class Player 
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }
        public bool IsBanned { get; private set; }

        public Player(int id,string name,int level,bool isBanned = false) 
        {
            Id = id;
            Name = name;
            Level = level;
            IsBanned = isBanned;
        }

        public void ShowInfo() 
        {
            Console.WriteLine($"ID игрока - {Id}");
            Console.WriteLine($"Имя - {Name}");
            Console.WriteLine($"Уровень  - {Level}");

            if (IsBanned)
            {
                Console.WriteLine("Игрок забанен");
            }
        }

        public void Ban() 
        {
            IsBanned = true;
        }

        public void Unban()
        {
            IsBanned = false; 
        }
    }

    class Database
    {
        private List<Player> _players;

        public Database() 
        {
            _players = new List<Player>();

            _players.Add(new Player(3211, "Гароши", 3));
            _players.Add(new Player(3125, "Присцила", 2));
            _players.Add(new Player(4512, "Рубилакс", 5));
        }

        public void Work()
        {           
            const string CommandShowInfo = "1";
            const string CommandAdd = "2";
            const string CommandBan = "3";
            const string CommandUnban = "4";
            const string CommandDelete = "5";
            const string CommandExit = "6";

            bool isExit = false;

            while (isExit == false) 
            {
                Console.WriteLine($"{CommandShowInfo} - Вывести информацию о игроках" +
                    $"\n{CommandAdd} - добавить игрока" +
                    $"\n{CommandBan} - Забанить игрока" +
                    $"\n{CommandUnban} - Разбанить игрока" +
                    $"\n{CommandDelete} - Удалить игрока" +
                    $"\n{CommandExit} - Выйти из программы");
                string userInput = Console.ReadLine();

                switch (userInput) 
                {
                    case CommandShowInfo:
                        ShowInfoAllPlayers();
                        break;

                    case CommandAdd:
                        AddPlayer();
                        break;

                    case CommandBan:                        
                        BanPlayer();
                        break;

                    case CommandUnban:                      
                        UnbanPalyer();
                        break;

                    case CommandDelete:
                        DeletPlayer();
                        break;
                    case CommandExit:
                        isExit = true;
                        break;

                    default:
                        Console.WriteLine("Введина неизвестная комманда");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowInfoAllPlayers() 
        {
            foreach(var player in _players)
            {
                player.ShowInfo();
                Console.WriteLine();
            }
        }

        private void BanPlayer() 
        {
            Console.Write("Введите id игрока которого хотите забанить: ");
            int id = ReadNumber();

            if (TryGetPlayer(id, out Player player))
            {
                player.Ban();
            }
            else 
            {
                Console.WriteLine("Игрок с таким id не найден");
            }
        }

        private void UnbanPalyer() 
        {
            Console.Write("Введите id игрока которого хотите разбанить: ");
            int id = ReadNumber();


            if (TryGetPlayer(id, out Player player))
            {
                player.Unban();
            }
            else
            {
                Console.WriteLine("Игрок с таким id не найден");
            }
        }

        private void DeletPlayer() 
        {
            Console.Write("Введите id игрока которого хотите удалить: ");
            int id = ReadNumber();


            if (TryGetPlayer(id, out Player player))
            {
                _players.Remove(player);
            }
            else
            {
                Console.WriteLine("Игрок с таким id не найден");
            }
        }

        private void AddPlayer() 
        {
            Console.Write("Введите id игрока - ");
            int id = ReadNumber();

            while (TryGetPlayer(id, out Player player) == true) 
            {
                Console.Write("\nИгрок с данным id уже существует");
                Console.WriteLine("\nВведите повторно id игрока - ");
                id = ReadNumber();
            }

            Console.Write("\nВведите имя игрока - ");
            string name = Console.ReadLine();
            Console.Write("\nВведите уровень игрока - ");
            int level = ReadNumber();

            Player newPlayer = new Player(id, name, level);
            _players.Add(newPlayer);
        }

        private bool TryGetPlayer(int id, out Player player)
        {
            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].Id == id)
                {
                    player = _players[i];
                    return true;
                }
            }

            player = null;
            return false;
        }

        static int ReadNumber()
        {
            int result;
            string numberForConvert = Console.ReadLine();


            while (int.TryParse(numberForConvert, out result) == false)
            {
                Console.WriteLine("Ошибка. Ведите число повтороно - ");
                numberForConvert = Console.ReadLine();
            }

            return result;
        }
    }
}
