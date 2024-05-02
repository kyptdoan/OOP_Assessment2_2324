using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieGame2
{
    public interface ITesting
    {
        /// <summary>
        /// A method to test the range of dice values
        /// </summary>
        void CheckDieRange();

        /// <summary>
        /// A method to test the game condition
        /// </summary>
        void CheckGameCondition();

        /// <summary>
        /// A method to test the points calculation
        /// </summary>
        void CheckPointsCalculation();
    }
}
