using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieGame2
{
    internal abstract class Game
    {
        /// <summary>
        /// A list contains points of each player.
        /// </summary>
        protected List<int> _points = new List<int>();

        /// <summary>
        /// Setting of the game (2-player or Human-Computer).
        /// </summary>
        protected int _setting;

        /// <summary>
        /// An index to the current player.
        /// </summary>
        protected int _currentTurn;

        /// <summary>
        /// The current status of the game's ending condition.
        /// </summary>
        protected bool _status = false;

        /// <summary>
        /// The winner.
        /// </summary>
        protected string _winner;
        
        /// <summary>
        /// A method to update the turn of the game.
        /// </summary>
        /// <returns>
        /// the current turn.
        /// </returns>
        protected int UpdateTurn()
        {
            _currentTurn = (_currentTurn % 2) + 1;
            return _currentTurn;
        }

        /// <summary>
        /// A method to get player's desired game setting.
        /// </summary>
        /// <returns>
        /// 1 for 2-player version / 2 for human-computer
        /// </returns>
        public abstract int ChooseGameSetting();

        /// <summary>
        /// A method to calculate points.
        /// </summary>
        /// <returns>
        /// The updated point of the indexed player.
        /// </returns>
        public abstract int CalculatePoint();

        /// <summary>
        /// A method to find winner for the 2-player version.
        /// </summary>
        public abstract void FindWinner_2Player();

        /// <summary>
        /// A method to find winner for the Human-Computer version.
        /// </summary>
        public abstract void FindWinner_HumanComputer();

        /// <summary>
        /// A method to check if the ending condition is met
        /// </summary>
        /// <returns>
        /// "true" if it is met
        /// "false" if not
        /// </returns>
        public abstract bool CheckStatus();

        /// <summary>
        /// A method to control the game's flow
        /// </summary>
        public abstract void Control();
    }
}
