using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telephony.Models
{
    public class Smartphone : ICallable, IBrowsable
    {
        public string Call(string number)
        {
            if (!ValidatePhoneNumber(number))
            {
                throw new ArgumentException("Invalid number!");
            }

            return $"Calling... {number}";
        }

        public bool ValidatePhoneNumber(string number)
        {
            return number.All(n => char.IsDigit(n));
        }

        public string Browse(string url)
        {
            if (ValidateURL(url))
            {
                throw new ArgumentException("Invalid URL!");
            }

            return $"Browsing: {url}!";
        }

        public bool ValidateURL(string url)
        {
            return url.Any(u => char.IsDigit(u));
        }
    }
}
