using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieGame2
{
    internal class Die
    {
        /// <summary>
        /// Create a new object <c>rd</c> of Random class
        /// </summary>
        private static readonly Random _rd = new Random();
        private int _currentValue;

        /// <summary>
        /// Property <c>CurrentValue</c> represents the die with its value
        /// </summary>
        public int CurrentValue
        {
            get { return _currentValue; }
        }

        /// <summary>
        /// Method <c>Roll</c> allows user to roll the die by generating a random value.
        /// </summary>
        /// <returns>
        /// An integer representing the value of the die after being rolled.
        /// </returns>
        public int Roll()
        {
            _currentValue = _rd.Next(1, 7);

            return _currentValue;
        }
    }
}

