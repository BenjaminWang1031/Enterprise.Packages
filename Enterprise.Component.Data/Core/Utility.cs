using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enterprise.Component.Nhiberate.Base
{
    public class Utility
    {
        private const int InitialPrime = 23;
        private const int FactorPrime = 29;

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <param name="hashCodesForProperties"></param>
        /// <returns></returns>
        public static int GetHashCode(params int[] hashCodesForProperties)
        {
            unchecked
            {
                int hash = InitialPrime;
                foreach (var code in hashCodesForProperties)
                    hash = hash * FactorPrime + code;
                return hash;
            }
        }
    }
}
