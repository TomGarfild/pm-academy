namespace Task_1._2
{
    public class Player : IPlayer
    {
        public int Age { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public PlayerRank Rank { get; }

        public Player(int age, string firstName, string lastName, PlayerRank rank)
        {
            Age = age;
            FirstName = firstName;
            LastName = lastName;
            Rank = rank;
        }

        public override string ToString() => Age + " " + FirstName + " " + LastName + " " + Rank;
    }
}