using System;

namespace Library
{
    public abstract class PaymentMethodBase
    {
        public string Name { get; }

        protected PaymentMethodBase(string name)
        {
            Name = name;
        }
    }
}