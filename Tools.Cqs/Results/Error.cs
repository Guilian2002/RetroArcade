using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Cqs.Results
{
    public class Error
    {
        public static Error Create(string errorMessage)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(errorMessage);
            return new Error(errorMessage);
        }

        public string ErrorMessage { get; }

        private Error(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
