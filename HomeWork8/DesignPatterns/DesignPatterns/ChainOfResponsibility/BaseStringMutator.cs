namespace DesignPatterns.ChainOfResponsibility
{
    public class BaseStringMutator : IStringMutator
    {
        private IStringMutator _mutator;
        public IStringMutator SetNext(IStringMutator next)
        {
            return _mutator = next;
        }

        public virtual string Mutate(string str)
        {
            return _mutator == null ? str : _mutator.Mutate(str);
        }
    }
}