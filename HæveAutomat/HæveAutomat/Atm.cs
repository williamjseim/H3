using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HæveAutomat
{
    public class Atm
    {
        Bank bank = new Bank();
        Token token = new Token();

        /// <summary>
        /// creates a token if the correct pin was given
        /// </summary>
        /// <param name="pin"></param>
        /// <returns>true if the pin was correct</returns>
        [Theory]
        [InlineData("1234")]
        public bool InsertCard(string pin)
        {
            token = this.bank.ValidatePin(pin);
            return token == null ? false : true;
        }

        /// <summary>
        /// Tries to withdraw money
        /// </summary>
        /// <param name="money"></param>
        /// <returns>true if theres enough money in the account</returns>
        [Theory]
        [InlineData(1000)]
        [InlineData(79)]
        [InlineData(int.MaxValue)]
        public bool WithDrawMoney(decimal money)
        {
            if(token != null)
            {
                return bank.WithDrawMoney(money);
            }
            return false;
        }
    }
}
