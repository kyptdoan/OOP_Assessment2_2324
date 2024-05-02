using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DieGame2
{
    internal class SevenOut : Game
    {
        /// <summary>
        /// Represent the first die of the game.
        /// </summary>
        private readonly Die _die1;

        /// <summary>
        /// Represent the second die of the game.
        /// </summary>
        private readonly Die _die2;

        /// <summary>
        /// The total of 2 rolled dice.
        /// </summary>
        private int _tempTotal = 0;

        /// <summary>
        /// The current point of the indexed player
        /// </summary>
        private int _currentPoint;

        /// <summary>
        /// A reference to an object of SevenOutTesting class
        /// </summary>
        private SevenOutTesting _sevenOutTesting;

        /// <summary>
        /// A reference to an object of ThreeOrMore Testing class
        /// </summary>
        private Statistics _statistics;

        /// <summary>
        /// A property to acess reference of the first die
        /// </summary>
        public Die GetDie1
        {
            get { return _die1; }
        }

        /// <summary>
        /// A property to acess reference of the first die
        /// </summary>
        public Die GetDie2
        {
            get { return _die2; }
        }

        /// <summary>
        ///  A property to acess the total of 2 rolled dice
        /// </summary>
        public int GetTempTotal
        {
            get { return _tempTotal; }
        }

        /// <summary>
        ///  A property to acess the current point of the indexed player
        /// </summary>
        public int GetCurrentPoint
        {
            get { return _currentPoint; }
        }

        /// <summary>
        /// A property to access the points list
        /// </summary>
        public List<int> GetPointsList
        {
            get { return _points; }
        }

        /// <summary>
        /// A property to access the current turn
        /// </summary>
        public int GetCurrentTurn
        {
            get { return _currentTurn; }
        }

        /// <summary>
        /// A property to access the status of the game
        /// </summary>
        public bool GetStatus
        {
            get { return _status; }
        }

        /// <summary>
        /// A property to access the reference of Statistics object
        /// </summary>
        public Statistics GetStatistic
        {
            get { return _statistics; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public SevenOut()
        {
            _die1 = new Die();
            _die2 = new Die();
            _sevenOutTesting = new SevenOutTesting(this);
            _statistics = new Statistics();
        }

        /// <summary>
        /// Implementation for the Control abstract class
        /// </summary>
        public override void Control()
        {
            // When the game starts, update the time
            _statistics.UpdateTime();

            int playerAction;

            // initialise points when the game begins
            _points.Add(0);
            _points.Add(0);

            ChooseGameSetting();

            // implementation for 2-player version
            if (_setting == 1)
            {
                // initialise the turn for the beginning
                _currentTurn = 1;

                // Clear Points List
                _points[0] = 0;
                _points[1] = 0;

                while (true)
                {
                    _tempTotal = 0;                    
                    
                    // validate player's action to roll the dice
                    while (true)
                    {
                        Console.WriteLine($"Player {_currentTurn}'s turn. Press '{_currentTurn}' to roll your dice.");
                        try
                        {
                            playerAction = int.Parse(Console.ReadLine());
                            if (playerAction == _currentTurn)
                            {
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch
                        {
                            Console.WriteLine($"Please press {_currentTurn} to roll your dice.");
                            continue;
                        }
                    }

                    // roll 2 dice
                    Console.WriteLine($"Player {_currentTurn}'s 1st die value: {_die1.Roll()}");
                    Console.WriteLine($"Player {_currentTurn}'s 2nd die value: {_die2.Roll()}");

                    // check dice range
                    _sevenOutTesting.GetCheckDieRange();

                    // calculate the total from 2 rolled dice
                    _tempTotal += _die1.CurrentValue + _die2.CurrentValue;

                    // check if the total is equal to 7
                    if (CheckStatus() == true)
                    {
                        // test status
                        _sevenOutTesting.GetCheckGameCondition();

                        Console.WriteLine("The total value of 2 dice is 7. The game terminates.");

                        // update points
                        Console.WriteLine("\n");
                        Console.WriteLine($"Player 1's points: {_points[0]}");
                        Console.WriteLine($"Player 2's points: {_points[1]}");
                        Console.WriteLine("\n");

                        // find winner
                        FindWinner_2Player();

                        // Update Points Statistics
                        _statistics.UpdatePoints(_points);

                        // Update Winner
                        _statistics.UpdateWinner(_winner);

                        // Reset status
                        _status = false;

                        break;
                    }
                    else
                    {
                        // test status
                        _sevenOutTesting.GetCheckGameCondition();

                        // Save the current point
                        _currentPoint = _points[_currentTurn - 1];

                        // update the points
                        CalculatePoint();

                        // Test Points Calculation
                        _sevenOutTesting.GetCheckPointsCalculation();

                        Console.WriteLine("\n");
                        Console.WriteLine($"Player 1's points: {_points[0]}");
                        Console.WriteLine($"Player 2's points: {_points[1]}");
                        Console.WriteLine("\n");

                        // update turn 
                        UpdateTurn();
                        continue;
                    }                 
                }
            }
            else
            {
                // initialise the turn for the beginning (1-human/2-computer)
                // human plays first
                _currentTurn = 1;

                // Clear Points List
                _points[0] = 0;
                _points[1] = 0;

                while(true)
                {
                    _tempTotal = 0;

                    if(_currentTurn == 1)
                    {
                        // Human's turn
                        while (true)
                        {
                            Console.WriteLine("Press '1' to roll your dice.");
                            try
                            {
                                playerAction = int.Parse(Console.ReadLine());
                                if(playerAction == 1)
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Please press '1' to roll your dice.");
                                    continue;
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Please press '1' to roll your dice.");
                                continue;
                            }
                        }

                        // Roll and print out the values of 2 dice
                        Console.WriteLine($"Human's 1st die value: {_die1.Roll()}");
                        Console.WriteLine($"Human's 2nd die value: {_die2.Roll()}");
                        Console.WriteLine("\n");

                        // check dice range
                        _sevenOutTesting.GetCheckDieRange();

                        // check if the total is equal to 7
                        _tempTotal = _die1.CurrentValue + _die2.CurrentValue;

                        if(CheckStatus() == true)
                        {
                            // check game status
                            _sevenOutTesting.GetCheckGameCondition();                         

                            Console.WriteLine("The total value of 2 dice is 7. The game terminates.");

                            // Update points
                            Console.WriteLine($"Human's points: {_points[0]}");
                            Console.WriteLine($"Computer's points: {_points[1]}");
                            Console.WriteLine("\n");

                            // Find winner
                            FindWinner_HumanComputer();

                            // Update Points Statistics
                            _statistics.UpdatePoints(_points);

                            // Update Winners Statistics
                            _statistics.UpdateWinner(_winner);

                            // Reset status
                            _status = false;
                            
                            break;
                        }
                        else
                        {
                            // check game status
                            _sevenOutTesting.GetCheckGameCondition();

                            // update the points
                            CalculatePoint();
                            Console.WriteLine($"Human's points: {_points[0]}");
                            Console.WriteLine($"Computer's points: {_points[1]}");
                            Console.WriteLine("\n");

                            // update turn
                            UpdateTurn();
                            continue;
                        }
                    }
                    else
                    {
                        // Computer's turn
                        // 2 dice are rolled automatically
                        Console.WriteLine($"Computer's 1st die value: {_die1.Roll()}");
                        Console.WriteLine($"Computer's 2nd die value: {_die2.Roll()}");

                        // check dice range
                        _sevenOutTesting.GetCheckDieRange();

                        // Check if the total is equal to 7
                        _tempTotal = _die1.CurrentValue + _die2 .CurrentValue;

                        if(CheckStatus() == true)
                        {
                            // check game status
                            _sevenOutTesting.GetCheckGameCondition();

                            Console.WriteLine("The total value of 2 dice is 7. The game terminates.");

                            // Update the points
                            Console.WriteLine($"Human's points: {_points[0]}");
                            Console.WriteLine($"Computer's points: {_points[1]}");
                            Console.WriteLine("\n");

                            // Find winner
                            FindWinner_HumanComputer();

                            // Update Points Statistics
                            _statistics.UpdatePoints( _points); 

                            // Update Winners Statistics
                            _statistics.UpdateWinner(_winner);

                            // Reset status
                            _status = false;

                            break;
                        }
                        else
                        {
                            // check game status
                            _sevenOutTesting.GetCheckGameCondition();

                            // Save Current Point
                            _currentPoint = _points[_currentTurn - 1];

                            // update the points
                            CalculatePoint();

                            // Test Points Calculation
                            _sevenOutTesting.GetCheckPointsCalculation();

                            Console.WriteLine("\n");
                            Console.WriteLine($"Human's points: {_points[0]}");
                            Console.WriteLine($"Computer's points: {_points[1]}");
                            Console.WriteLine("\n");

                            // update turn
                            UpdateTurn();
                            continue;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Implementation for the CalculatePoint abstract class
        /// </summary>
        /// <returns>
        /// the updated point of the indexed player
        /// </returns>
        public override int CalculatePoint()
        {
            if (_die1.CurrentValue == _die2.CurrentValue)
            {
                _points[_currentTurn - 1] += 2 * (_tempTotal);
            }
            else
            {
                _points[_currentTurn - 1] += _tempTotal;
            }
            
            return _points[_currentTurn - 1];
        }

        /// <summary>
        /// Implementation for the CheckStatus abstract class
        /// </summary>
        /// <returns>
        /// status boolean value
        /// </returns>
        public override bool CheckStatus()
        {
            if (_tempTotal == 7)
            {
                // set the status to true
                _status = true;
            }
            return _status;
        }
        
        /// <summary>
        /// Implementation for the ChooseGameSetting abstract class
        /// </summary>
        /// <returns>
        /// the chosen integer representing the game version
        /// </returns>
        public override int ChooseGameSetting()
        {
            while (true)
            {
                Console.WriteLine("Please choose your preferred game setting (1-two players/2-human computer).");
                try
                {
                    _setting = int.Parse(Console.ReadLine());
                    if (_setting != 1 && _setting != 2)
                    {
                        Console.WriteLine("Please press '1' for 2 players or '2' for human computer.");
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("Please press '1' for 2 players or '2' for human computer.");
                }
            }
            return _setting;
        }

        /// <summary>
        /// Implementation for the FindWinner_2Player abstract method
        /// </summary>
        public override void FindWinner_2Player()
        {
            if (_points[0] > _points[1])
            {
                _winner = "Player 1";
                Console.WriteLine("PLAYER 1 WINS.");
            }
            else if (_points[0] < _points[1])
            {
                _winner = "Player 2";
                Console.WriteLine("PLAYER 2 WINS.");
            }
            else
            {
                _winner = "Tie";
                Console.WriteLine("TIE.");
            }
        }

        /// <summary>
        /// Implementation for the FindWinner_HumanComputer abstract method
        /// </summary>
        public override void FindWinner_HumanComputer()
        {
            if (_points[0] > _points[1]) 
            {
                _winner = "Human";
                Console.WriteLine("HUMAN WINS.");
            }
            else if (_points[0] < _points[1])
            {
                _winner = "Computer";
                Console.WriteLine("COMPUTER WINS.");
            }
            else
            {
                _winner = "Tie";
                Console.WriteLine("TIE.");
            }
        }
    }
}
