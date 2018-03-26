using OnlineStore.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace OnlineStore.Core.Providers.Providers
{
    public class Hasher : IHasher
    {
        private readonly string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
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

        public void ValidatePassword(string password)
        {
            if (password == string.Empty)
            {
                throw new ArgumentException("Password is Required");
            }

            if (!Regex.IsMatch(password, this.passwordPattern))
            {
                throw new ArgumentException("Password must be minimum eight characters, at least one letter and one number!");
            }
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
