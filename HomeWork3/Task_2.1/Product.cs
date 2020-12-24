using System;

namespace Task_2._1
{
    public class Product
    {
        public string Id { get; }
        public string Brand { get; }
        public string Model { get; }
        public decimal Price { get; }

        public Product(string id, string brand, string model, decimal price)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Price = price;
        }

        public Product(string[] args)
            : this(args[0], args[1], args[2], Decimal.Parse(args[3]))
        {

        }

        public override string ToString()
        {
            return Id + " - " + Brand + " - " + Model + " - $" + Price;
        }
    }
}