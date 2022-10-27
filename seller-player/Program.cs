using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seller_player
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool isFinish = false;
            string input;
            Player player = new Player(1000);
            Seller seller = new Seller(new List<Product> { new Product("кожаный нагрудник", 400), new Product("железные сапоги", 350), new Product("меч", 500), new Product("лук", 200), new Product("декоративный нож", 800) });

            while (isFinish == false)
            {
                Console.SetCursorPosition(0, 3);
                player.ShowInventory();
                Console.WriteLine($"монет: {player.Money}");
                Console.SetCursorPosition(0, 10);
                seller.ShowInventory();
                Console.WriteLine($"монет: {seller.Money}");
                Console.SetCursorPosition(0, 0);
                Console.Write("Приветствую вас!\nЧто желаете купить: ");
                input = Console.ReadLine();
                seller.SellProduct(player, input);
                Console.Clear();
            }
        }
    }
    
    class Player
    {
        public int Money { get; private set; }
        private List<Product> _playerInventory = new List<Product>();

        public Player(int money)
        {
            Money = money;
        }

        public void BuyProduct(int price, Product product)
        {
            Money -= price;
            _playerInventory.Add(product);
        }

        public void ShowInventory()
        {
            Console.WriteLine("Игрок");

            foreach (var product in _playerInventory)
            {
                Console.WriteLine($"{product.Title}");
            }
        }
    }

    class Seller
    {
        public int Money { get; private set; }
        private List<Product> _sellerInventory = new List<Product>();

        public Seller(List<Product> products)
        {
            Money = 0;
            _sellerInventory = products;
        }

        public void SellProduct(Player player, string productTitle)
        {
            /*foreach (var product in _sellerInventory) //не понимаю почему при выполнение данной строчки выдает ошибку
            {
                if (product.Title == productTitle && player.Money >= product.Cost)
                {
                    int cost = product.Cost;

                    player.BuyProduct(cost, product);
                    Money += cost;
                    _sellerInventory.Remove(product);
                }
            }*/
            for (int i = 0; i < _sellerInventory.Count; i++)
            {
                if (_sellerInventory[i].Title == productTitle && player.Money >= _sellerInventory[i].Cost)
                {
                    int cost = _sellerInventory[i].Cost;

                    player.BuyProduct(cost, _sellerInventory[i]);
                    Money += cost;
                    _sellerInventory.Remove(_sellerInventory[i]);
                }
            }
        }

        public void ShowInventory()
        {
            Console.WriteLine("Продавец");

            foreach(var product in _sellerInventory)
            {
                Console.WriteLine($"{product.Title}: {product.Cost}");
            }
        }
    }

    class Product
    {
        public string Title { get; private set; }
        public int Cost { get; private set; }

        public Product(string title, int cost)
        {
            Title = title;
            Cost = cost;
        }
    }
}
