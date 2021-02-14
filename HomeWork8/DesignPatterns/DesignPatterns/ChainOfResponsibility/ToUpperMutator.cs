namespace DesignPatterns.ChainOfResponsibility
{
    public class ToUpperMutator : BaseStringMutator
    {
        public override string Mutate(string str)
        {
            return base.Mutate(str.ToUpper());
        }
    }
}