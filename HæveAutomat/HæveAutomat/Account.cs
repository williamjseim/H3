using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HæveAutomat
{
    public class Account
    {
        public string pin { get; set; } = "1234";
        public decimal Money { get; set; } = 2000;

        /// <summary>
        /// checks if account has enough money
        /// </summary>
        /// <param name="money"></param>
        public void WithDrawMoney(decimal money)
        {
            this.Money -= money;
        }
    }
}
