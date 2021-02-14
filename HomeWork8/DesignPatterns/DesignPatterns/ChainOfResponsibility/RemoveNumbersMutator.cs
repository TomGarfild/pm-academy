using System.Linq;

namespace DesignPatterns.ChainOfResponsibility
{
    public class RemoveNumbersMutator : BaseStringMutator
    {
        public override string Mutate(string str)
        {
            return base.Mutate(new string(
                                    str.ToCharArray()
                                    .Where(ch => !char.IsDigit(ch))
                                    .ToArray()));
        }
    }
}