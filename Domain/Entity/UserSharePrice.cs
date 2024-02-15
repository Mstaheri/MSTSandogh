﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class UserSharePrice
    {
        public string NameBankSafe { get; private set; }
        public string UserName { get; private set; }
        public int NumberOfShares { get; private set; }
        public BankSafe BankSafe { get; private set; }
        public User User { get; private set; }
        public UserSharePrice(string nameBankSafe, string userName, int numberOfShares)
        {
            NameBankSafe = nameBankSafe;
            UserName = userName;
            NumberOfShares = numberOfShares;
        }
        public void Edit(int numberOfShares)
        {
            NumberOfShares = numberOfShares;
        }

    }
}