namespace Task_2._1
{
    public class Tag
    {

        public string ProductId { get; }
        public string TagValue { get; }

        public Tag(string productId, string tagValue)
        {
            ProductId = productId;
            TagValue = tagValue;
        }

        public Tag(string[] args)
            : this(args[0], args[1])
        {

        }
    }
}