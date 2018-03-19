using OnlineStore.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace OnlineStore.Core.Providers
{
    public class Hasher : IHasher
    {
        private readonly List<char> peppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToList();
        private readonly SHA256 algorythm;

        public Hasher()
        {
            this.algorythm = SHA256.Create();
        }

        public bool CheckPassword(string enteredPassword, string actualPassword)
        {
            return this.peppers.
                Exists(x => this.Hash(enteredPassword + x) == actualPassword);
        }

        public string CreatePassword(string password)
        {
            var pepper = this.GetPepper();

            return this.Hash(password + pepper);
        }

        private char GetPepper()
        {
            var rand = new Random();
            var range = this.peppers.Count;
            return this.peppers[rand.Next(range)];
        }

        private string Hash(string password)
        {
            var hashedPassword = new StringBuilder();
            var passwordAsBytes = Encoding.UTF8.GetBytes(password);
            this.algorythm.ComputeHash(passwordAsBytes)
                .ToList()
                .ForEach(x => hashedPassword.Append(x.ToString("X2")));
            return hashedPassword.ToString();
        }
    }
}
