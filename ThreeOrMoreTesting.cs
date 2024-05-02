using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieGame2
{
    internal class ThreeOrMoreTesting : ITesting
    {
        /// <summary>
        /// A reference to a ThreeOrMore object
        /// </summary>
        private ThreeOrMore _threeOrMoreGame;

        /// <summary>
        /// A reference to a LogFile object
        /// </summary>
        private LogFile _logFile;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public ThreeOrMoreTesting(ThreeOrMore game)
        {
            _threeOrMoreGame = game;
            _logFile = new LogFile("threeOrMoreLogFile.txt");
        }

        /// <summary>
        /// A property to the reference to a ThreeOrMore object
        /// </summary>
        public ThreeOrMore GetGame
        {
            get { return _threeOrMoreGame; }
        }

        /// <summary>
        /// Implementation for the CheckDieRange method defined by the interface
        /// </summary>
        void ITesting.CheckDieRange()
        {
            bool check = true;

            for (int i = 0; i < 5; i++)
            {
                Debug.Assert(_threeOrMoreGame.dice[i].CurrentValue >= 1 && _threeOrMoreGame.dice[i].CurrentValue <= 6, "Die value must be from 1 to 6, inclusive.");
            }

            for (int i = 0; i < 5; i++)
            {
                if (!(_threeOrMoreGame.dice[i].CurrentValue >= 1 && _threeOrMoreGame.dice[i].CurrentValue <= 6))
                {
                    check = false;
                    break;
                }
            }

            if (check == false)
            {
                _logFile.Log($"{DateTime.Now}. Test failed: The game does not end when a player reaches 20 or more.");

                for (int i = 0; i < 5; i++)
                {
                    _logFile.Log($" Die {i}: {_threeOrMoreGame.dice[i].CurrentValue}");
                }
            }
            else
            {
                _logFile.Log($"{DateTime.Now}. Pass passed: The game ends when a player reaches 20 or more.");

                for (int i = 0; i < 5; i++)
                {
                    _logFile.Log($" Die {i}: {_threeOrMoreGame.dice[i].CurrentValue}");
                }
            }
        }

        /// <summary>
        /// A method to test the range of the rerolled dice values
        /// </summary>
        /// <param name="RerolledDiceRange"></param>
        public void CheckRerolledDieRange(List<int> RerolledDiceRange) 
        {
            bool check = true;
            for (int i = 0; i < 5; i++)
            {
                Debug.Assert(RerolledDiceRange[i] >= 1 && RerolledDiceRange[i] <= 6, "Die value must be from 1 to 6, inclusive.");                
            }

            for (int i = 0; i < 5; i++)
            {
                if (!(_threeOrMoreGame.dice[i].CurrentValue >= 1 && _threeOrMoreGame.dice[i].CurrentValue <= 6))
                {
                    check = false;
                    break;
                }
            }

            if (check == false)
            {
                _logFile.Log($"{DateTime.Now}. Test failed: 5 dice value exceed the range 1 to 6 (inclusive).");

                for (int i = 0; i < 5; i++)
                {
                    _logFile.Log($" Die {i}: {_threeOrMoreGame.dice[i].CurrentValue}");
                }
            }
            else
            {
                _logFile.Log($"{DateTime.Now}. Test passed: 5 dice value are from 1 to 6 (inclusive).");

                for (int i = 0; i < 5; i++)
                {
                    _logFile.Log($" Die {i}: {_threeOrMoreGame.dice[i].CurrentValue}");
                }
            }
        }

        /// <summary>
        /// Implementation for the CheckGameCondition method defined by the interface
        /// </summary>
        void ITesting.CheckGameCondition()
        {
            Debug.Assert(
                (_threeOrMoreGame.GetPointsList[0] < 20 && _threeOrMoreGame.CheckStatus() == false)
                || (_threeOrMoreGame.GetPointsList[1] < 20 && _threeOrMoreGame.CheckStatus() == false)
                || (_threeOrMoreGame.GetPointsList[0] >= 20 && _threeOrMoreGame.CheckStatus() == true)
                || (_threeOrMoreGame.GetPointsList[1] >= 20 && _threeOrMoreGame.CheckStatus() == true),
                "The game must end when one of two players reaches the points of 20."
            );

            if((_threeOrMoreGame.GetPointsList[0] < 20 && _threeOrMoreGame.CheckStatus() == false)
                || (_threeOrMoreGame.GetPointsList[1] < 20 && _threeOrMoreGame.CheckStatus() == false)
                || (_threeOrMoreGame.GetPointsList[0] >= 20 && _threeOrMoreGame.CheckStatus() == true)
                || (_threeOrMoreGame.GetPointsList[1] >= 20 && _threeOrMoreGame.CheckStatus() == true))
            {
                _logFile.Log($"{DateTime.Now}. Test passed: The game ends when a player reaches 20 or more.");
            }
            else
            {
                _logFile.Log($"{DateTime.Now}. Test failed: The game does not end when a player reaches 20 or more.");
            }
        }

        /// <summary>
        /// Implementation for the CheckPointsCalculation method defined by the interface
        /// </summary>
        void ITesting.CheckPointsCalculation()
        {
            if (_threeOrMoreGame.GetKind == 3)
            {
                Debug.Assert(_threeOrMoreGame.GetPointsList[_threeOrMoreGame.GetCurrentTurn - 1]
                    == _threeOrMoreGame.GetCurrentPoint + 3, "The point was calculated incorrectly");
            }
            else if (_threeOrMoreGame.GetKind == 4)
            {
                Debug.Assert(_threeOrMoreGame.GetPointsList[_threeOrMoreGame.GetCurrentTurn - 1]
                    == _threeOrMoreGame.GetCurrentPoint + 6, "The point was calculated incorrectly");
            }
            else if (_threeOrMoreGame.GetKind == 5)
            {
                Debug.Assert(_threeOrMoreGame.GetPointsList[_threeOrMoreGame.GetCurrentTurn - 1]
                    == _threeOrMoreGame.GetCurrentPoint + 12, "The point was calculated incorrectly");
            }

            if (_threeOrMoreGame.GetKind == 3)
            {
                if (_threeOrMoreGame.GetPointsList[_threeOrMoreGame.GetCurrentTurn - 1]
                    == _threeOrMoreGame.GetCurrentPoint + 3)
                {
                    _logFile.Log("Test passed: The point was calculated correctly.");
                    _logFile.Log($"Previous point: {_threeOrMoreGame.GetCurrentPoint} / Kind: {_threeOrMoreGame.GetKind}" +
                        $" / Calculated point: {_threeOrMoreGame.GetPointsList[_threeOrMoreGame.GetCurrentTurn - 1]}");
                }
                else
                {
                    _logFile.Log("Test passed: The point was calculated incorrectly.");
                    _logFile.Log($"Previous point: {_threeOrMoreGame.GetCurrentPoint} / Kind: {_threeOrMoreGame.GetKind}" +
                        $" / Calculated point: {_threeOrMoreGame.GetPointsList[_threeOrMoreGame.GetCurrentTurn - 1]}");
                }
            }
            else if (_threeOrMoreGame.GetKind == 4)
            {
                if (_threeOrMoreGame.GetPointsList[_threeOrMoreGame.GetCurrentTurn - 1]
                    == _threeOrMoreGame.GetCurrentPoint + 6)
                {
                    _logFile.Log("Test passed: The point was calculated correctly.");
                    _logFile.Log($"Previous point: {_threeOrMoreGame.GetCurrentPoint} / Kind: {_threeOrMoreGame.GetKind}" +
                        $" / Calculated point: {_threeOrMoreGame.GetPointsList[_threeOrMoreGame.GetCurrentTurn - 1]}");
                }
                else
                {
                    _logFile.Log("Test passed: The point was calculated incorrectly.");
                    _logFile.Log($"Previous point: {_threeOrMoreGame.GetCurrentPoint} / Kind: {_threeOrMoreGame.GetKind}" +
                        $" / Calculated point: {_threeOrMoreGame.GetPointsList[_threeOrMoreGame.GetCurrentTurn - 1]}");
                }
            }
            else if (_threeOrMoreGame.GetKind == 5)
            {
                if (_threeOrMoreGame.GetPointsList[_threeOrMoreGame.GetCurrentTurn - 1]
                    == _threeOrMoreGame.GetCurrentPoint + 12)
                {
                    _logFile.Log("Test passed: The point was calculated correctly.");
                    _logFile.Log($"Previous point: {_threeOrMoreGame.GetCurrentPoint} / Kind: {_threeOrMoreGame.GetKind}" +
                        $" / Calculated point: {_threeOrMoreGame.GetPointsList[_threeOrMoreGame.GetCurrentTurn - 1]}");
                }
                else
                {
                    _logFile.Log("Test passed: The point was calculated incorrectly.");
                    _logFile.Log($"Previous point: {_threeOrMoreGame.GetCurrentPoint} / Kind: {_threeOrMoreGame.GetKind}" +
                        $" / Calculated point: {_threeOrMoreGame.GetPointsList[_threeOrMoreGame.GetCurrentTurn - 1]}");
                }
            }
        }

        /// <summary>
        /// A public method to invoke the CheckDieRange method
        /// </summary>
        public void GetCheckDieRange()
        {
            ((ITesting)this).CheckDieRange();
        }

        /// <summary>
        /// A public method to invoke the CheckGameCondition method
        /// </summary>
        public void GetCheckGameCondition()
        {
            ((ITesting)this).CheckGameCondition();
        }

        /// <summary>
        /// A public method to invoke the CheckPointsCalculation method
        /// </summary>
        public void GetCheckPointsCalculation()
        {
            ((ITesting)this).CheckPointsCalculation();
        }
    }
}
