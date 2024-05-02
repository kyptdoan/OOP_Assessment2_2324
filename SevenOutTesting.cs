using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieGame2
{
    internal class SevenOutTesting : ITesting
    {
        /// <summary>
        /// A reference to the SevenOut object
        /// </summary>
        private SevenOut _sevenOutGame;

        /// <summary>
        /// A reference to the LogFile object
        /// </summary>
        private LogFile _logFile;

        /// <summary>
        /// A property to access the reference to SevenOut object
        /// </summary>
        public SevenOut GetGame 
        { 
            get { return _sevenOutGame; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"> A reference to a SevenOut object</param>
        public SevenOutTesting(SevenOut game)
        {
            _sevenOutGame = game;
            _logFile = new LogFile("sevenOutLogFile.txt");
        }

        /// <summary>
        /// Implementation for the CheckDieRange method defined by the interface
        /// </summary>
        void ITesting.CheckDieRange()
        {
            Debug.Assert(_sevenOutGame.GetDie1.CurrentValue >= 1 && _sevenOutGame.GetDie1.CurrentValue <= 6, "Die value must be from 1 to 6, inclusive.");
            Debug.Assert(_sevenOutGame.GetDie1.CurrentValue >= 1 && _sevenOutGame.GetDie2.CurrentValue <= 6, "Die value must be from 1 to 6, inclusive.");

            if ((_sevenOutGame.GetDie1.CurrentValue >= 1 && _sevenOutGame.GetDie1.CurrentValue <= 6)
                && (_sevenOutGame.GetDie1.CurrentValue >= 1 && _sevenOutGame.GetDie2.CurrentValue <= 6))
            {
                _logFile.Log($"{DateTime.Now}. Test passed: 2 dice value are from 1 to 6 (inclusive). ({_sevenOutGame.GetDie1.CurrentValue}, {_sevenOutGame.GetDie2.CurrentValue})");
            }
            else
            {
                _logFile.Log($"{DateTime.Now}. Test failed: Dice value are out of range 1 to 6. ({_sevenOutGame.GetDie1.CurrentValue}, {_sevenOutGame.GetDie2.CurrentValue})");
            }
        }

        /// <summary>
        /// Implementation for the CheckGameCondition method defined by the interface
        /// </summary>
        void ITesting.CheckGameCondition()
        {
            Debug.Assert(
                (_sevenOutGame.GetTempTotal == 7 && _sevenOutGame.GetStatus == true)
                || (_sevenOutGame.GetTempTotal != 7 && _sevenOutGame.GetStatus == false),
                "The game ends when total = 7");       
            
            if ((_sevenOutGame.GetTempTotal == 7 && _sevenOutGame.GetStatus == true)
                || (_sevenOutGame.GetTempTotal != 7 && _sevenOutGame.GetStatus == false))
            {
                _logFile.Log("Test passed: The game ends when the total is 7.");
            }
            else
            {
                _logFile.Log("Test failed: The game does not end when the total is 7.");
                _logFile.Log($"{_sevenOutGame.GetTempTotal} & {_sevenOutGame.GetStatus}.");
            }
        }

        /// <summary>
        /// Implementation for the CheckPointsCalculation method defined by the interface
        /// </summary>
        void ITesting.CheckPointsCalculation()
        {
            if (_sevenOutGame.GetDie1.CurrentValue == _sevenOutGame.GetDie2.CurrentValue)
            {
                Debug.Assert(_sevenOutGame.GetPointsList[_sevenOutGame.GetCurrentTurn -1] 
                    == _sevenOutGame.GetCurrentPoint + 2*(_sevenOutGame.GetTempTotal), "The point was calculated incorrectly.");
            }
            else
            {
                Debug.Assert(_sevenOutGame.GetPointsList[_sevenOutGame.GetCurrentTurn - 1]
                    == _sevenOutGame.GetCurrentPoint + _sevenOutGame.GetTempTotal, "The point was calculated incorrectly.");
            }

            if (_sevenOutGame.GetDie1.CurrentValue == _sevenOutGame.GetDie2.CurrentValue)
            {
                if (_sevenOutGame.GetPointsList[_sevenOutGame.GetCurrentTurn - 1]
                    == _sevenOutGame.GetCurrentPoint + 2 * (_sevenOutGame.GetTempTotal))
                {
                    _logFile.Log("Test passed: The point was calculated correctly.");
                    _logFile.Log($"Previous point: {_sevenOutGame.GetCurrentPoint} / Dice value total: {_sevenOutGame.GetTempTotal} " +
                        $"/ Calculated Points: {_sevenOutGame.GetPointsList[_sevenOutGame.GetCurrentTurn - 1]}.");
                }
                else
                {
                    _logFile.Log("Test passed: The point was calculated incorrectly.");
                    _logFile.Log($"Previous point: {_sevenOutGame.GetCurrentPoint} / Dice value total: {_sevenOutGame.GetTempTotal} " +
                        $"/ Calculated Points: {_sevenOutGame.GetPointsList[_sevenOutGame.GetCurrentTurn - 1]}.");
                }
            }
            else
            {
                if (_sevenOutGame.GetPointsList[_sevenOutGame.GetCurrentTurn - 1]
                    == _sevenOutGame.GetCurrentPoint + _sevenOutGame.GetTempTotal)
                {
                    _logFile.Log("Test passed: The point was calculated correctly.");
                    _logFile.Log($"Previous point: {_sevenOutGame.GetCurrentPoint} / Dice value total: {_sevenOutGame.GetTempTotal} " +
                        $"/ Calculated Points: {_sevenOutGame.GetPointsList[_sevenOutGame.GetCurrentTurn - 1]}.");
                }
                else
                {
                    _logFile.Log("Test passed: The point was calculated incorrectly.");
                    _logFile.Log($"Previous point: {_sevenOutGame.GetCurrentPoint} / Dice value total: {_sevenOutGame.GetTempTotal} " +
                        $"/ Calculated Points: {_sevenOutGame.GetPointsList[_sevenOutGame.GetCurrentTurn - 1]}.");
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
