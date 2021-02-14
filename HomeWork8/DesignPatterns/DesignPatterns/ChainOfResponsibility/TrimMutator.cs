namespace DesignPatterns.ChainOfResponsibility
{
    public class TrimMutator : BaseStringMutator
    {
        public override string Mutate(string str)
        {
            return base.Mutate(str.Trim());
        }
    }
}