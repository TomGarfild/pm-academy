using System.Linq;

namespace DesignPatterns.ChainOfResponsibility
{
    public class InvertMutator : BaseStringMutator
    {
        public override string Mutate(string str)
        {
            return base.Mutate(new string(str.ToCharArray()
                                    .Reverse()
                                    .ToArray()));
        }
    }
}