namespace Library
{
    public interface ISupportDeposit
    {
        void StartDeposit(decimal amount, string currency);
    }
}