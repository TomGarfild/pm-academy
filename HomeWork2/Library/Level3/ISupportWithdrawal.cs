namespace Library
{
    public interface ISupportWithdrawal
    {
        void StartWithdrawal(decimal amount, string currency);
    }
}