using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieGame2
{
    internal class Statistics
    {
        /// <summary>
        /// The times the game is played
        /// </summary>
        private int _times;

        /// <summary>
        /// A list contains points of each time the game is played
        /// </summary>
        private List<int> _pointsCollection;

        /// <summary>
        /// A list contains the winner of each time the game is played
        /// </summary>
        private List<string> _winners;

        /// <summary>
        /// Constructor
        /// </summary>
        public Statistics()
        {
            _times = 0;
            _pointsCollection = new List<int>();
            _winners = new List<string>();
        }

        /// <summary>
        /// A method to update the times
        /// </summary>
        /// <returns>
        /// the times the game is played
        /// </returns>
        public int UpdateTime()
        {
            _times++;
            return _times;
        }

        /// <summary>
        /// A method to update the points of each time the game is played
        /// </summary>
        /// <param name="pointsList"> the points list of each time</param>
        /// <returns>
        /// a list contains the points of each time
        /// </returns>
        public List<int> UpdatePoints (List<int> pointsList)
        {
            _pointsCollection.Add(pointsList[0]);
            _pointsCollection.Add(pointsList[1]);
            return _pointsCollection;
        }

        /// <summary>
        /// A method to update the winners of each time the game is played
        /// </summary>
        /// <param name="winner"> the winner of each time </param>
        /// <returns>
        /// A list contains the winners of each time
        /// </returns>
        public List<string> UpdateWinner(string winner)
        {
            _winners.Add(winner);
            return _winners;
        }

        /// <summary>
        /// A method to print out the times
        /// </summary>
        public void ReportTimes()
        {
            Console.WriteLine($"The game has been played: {_times} times.");
        }       
        
        /// <summary>
        /// A method to print out the points of each times
        /// </summary>
        public void ReportPoints()
        {
            int j = 0;
            Console.WriteLine("Time: Player 1 (Human)'s Points, Player 2 (Computer)'s Points.");
            for (int i = 0; i < _times; i++)
            {
                Console.WriteLine($"{i+1}: {_pointsCollection[j]}, {_pointsCollection[j+1]}.");
                j += 2;
            }
        }

        /// <summary>
        /// A method to print out the winner of each time
        /// </summary>
        public void ReportWinners()
        {
            Console.Write("Winners List: ");
            foreach (var winner in _winners)
            {
                Console.Write(winner);
                Console.Write(" ");
            }
        }
    }
}
