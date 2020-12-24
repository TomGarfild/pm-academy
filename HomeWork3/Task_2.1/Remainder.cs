namespace Task_2._1
{
    public class Remainder
    {
        public string ProductId { get; }
        public string Location { get; }
        public int RemainingAmount { get; }

        public Remainder(string productId, string location, int  remainingAmount)
        {
            ProductId = productId;
            Location = location;
            RemainingAmount = remainingAmount;
        }

        public Remainder(string[] args)
            : this(args[0], args[1], int.Parse(args[2]))
        {

        }
    }
}