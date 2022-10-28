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
            Player player = new Player(new List<Product> (), 1000);
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

    abstract class Person
    {
        public int Money { get; private set; }
        protected List<Product> _inventory = new List<Product>();

        public Person(List<Product> products, int money)
        {
            Money = money;
            _inventory = products;
        }

        public abstract void ShowInventory();

        public void SellProduct(Player player, string productTitle)
        {
            foreach (var product in _inventory)
            {
                if (product.Title == productTitle && player.Money >= product.Cost)
                {
                    int cost = product.Cost;

                    player.BuyProduct(cost, product);
                    Money += cost;
                    _inventory.Remove(product);
                    break;
                }
            }
        }

        private void BuyProduct(int price, Product product)
        {
            Money -= price;
            _inventory.Add(product);
        }
    }
    
    class Player: Person
    {
        public Player(List<Product> products, int money = 0) : base(products, money) { }

        public override void ShowInventory()
        {
            Console.WriteLine("Игрок");

            foreach (var product in _inventory)
            {
                Console.WriteLine($"{product.Title}");
            }
        }
    }

    class Seller: Person
    {
        public Seller(List<Product> products, int money = 0) : base(products, money) { }

        public override void ShowInventory()
        {
            Console.WriteLine("Продавец");

            foreach (var product in _inventory)
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
