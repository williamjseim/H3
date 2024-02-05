using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HæveAutomat
{
    public class Bank
    {
        Account Account = new Account();

        /// <summary>
        /// validates the pin
        /// </summary>
        /// <param name="pin"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1234)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public Token ValidatePin(string pin)
        {
            if(pin == Account.pin)
            {
                return new Token();
            }
            return null;
        }

        /// <summary>
        /// tries to withdraw money
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool WithDrawMoney(decimal amount)
        {
            if(this.Account.Money >= amount)
            {
                Account.WithDrawMoney(amount);
                return true;
            }
            return false;
        }
    }
}
