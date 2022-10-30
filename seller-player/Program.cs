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
            Player player = new Player("Игрок", new List<Product> (), 1000);
            Seller seller = new Seller("Продавец", new List<Product> { new Product("кожаный нагрудник", 400), new Product("железные сапоги", 350), new Product("меч", 500), new Product("лук", 200), new Product("декоративный нож", 800) });

            while (isFinish == false)
            {
                Console.SetCursorPosition(0, 3);

                player.ShowInventory();

                Console.SetCursorPosition(0, 15);

                seller.ShowInventory();

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
        protected List<Product> Inventory;
        private string _name;
        public int Money { get; private set; }


        public Person(string name, List<Product> products, int money)
        {
            Money = money;
            Inventory = products;
            _name = name;
        }

        public virtual void ShowInventory()
        {
            Console.WriteLine($"{_name}:");

            foreach (var product in Inventory)
            {
                ShowProductInfo(product);
            }

            Console.WriteLine($"\nмонет: {Money}");
        }

        protected abstract void ShowProductInfo(Product product);

        protected void GiveMoney(int money)
        {
            Money -= money;
        }

        protected void TakeMoney(int money)
        {
            Money += money;
        }
    }
    
    class Player: Person
    {
        public Player(string name, List<Product> products, int money = 0) : base(name, products, money) { }

        public void BuyProduct(int price, Product product)
        {
            GiveMoney(price);
            Inventory.Add(product);
        }

        protected override void ShowProductInfo(Product product)
        {
            Console.WriteLine($"{product.Title}");
        }
    }

    class Seller: Person
    {
        public Seller(string name, List<Product> products, int money = 0) : base(name, products, money) { } 

        public void SellProduct(Player player, string productTitle)
        {
            foreach (var product in Inventory)
            {
                if (product.Title == productTitle && player.Money >= product.Cost)
                {
                    int cost = product.Cost;

                    player.BuyProduct(cost, product);
                    TakeMoney(cost);
                    Inventory.Remove(product);
                    break;
                }
            }
        }

        protected override void ShowProductInfo(Product product)
        {
            Console.WriteLine($"{product.Title}: {product.Cost}");
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
