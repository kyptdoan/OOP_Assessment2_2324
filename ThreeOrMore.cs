using System;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DieGame2
{
    internal class ThreeOrMore : Game
    {
        /// <summary>
        /// A list contains 5 objects of 5 dice in the game
        /// </summary>
        public readonly List<Die> dice = new List<Die>();

        /// <summary>
        /// The kind which the dice value make
        /// </summary>
        private int _kind;

        /// <summary>
        /// The die value which makes the kind
        /// </summary>
        private int _kindValue;

        /// <summary>
        /// The current point of the indexed value
        /// </summary>
        private int _currentPoint;

        /// <summary>
        /// A list for dice rolled value
        /// </summary>
        private List<int> _diceValue = new List<int>();

        /// <summary>
        /// A reference to a ThreeOrMoreTesting object
        /// </summary>
        private ThreeOrMoreTesting _threeOrMoreTesting;

        /// <summary>
        /// A reference to a Statistics object
        /// </summary>
        private Statistics _statistics;

        /// <summary>
        /// A property to access the kind
        /// </summary>
        public int GetKind
        {
            get { return _kind; }
        }

        /// <summary>
        /// A property to access the current turn
        /// </summary>
        public int GetCurrentTurn
        {
            get { return _currentTurn; }
        }

        /// <summary>
        /// A property to access the points list
        /// </summary>
        public List<int> GetPointsList
        {
            get { return _points; }
        }

        /// <summary>
        /// A property to access the current point of the indexed player
        /// </summary>
        public int GetCurrentPoint
        {
            get { return _currentPoint; }
        }

        /// <summary>
        /// A property to access the reference to the Statistics object
        /// </summary>
        public Statistics GetStatistics
        {
            get { return _statistics; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ThreeOrMore()
        {
            dice.Add(new Die());
            dice.Add(new Die());
            dice.Add(new Die());
            dice.Add(new Die());
            dice.Add(new Die());
            _threeOrMoreTesting = new ThreeOrMoreTesting(this);
            _statistics = new Statistics();
        }

        /// <summary>
        /// Implementation for the Control abstract class
        /// </summary>
        public override void Control()
        {
            // Update Times Statistics
            _statistics.UpdateTime();

            int playerAction;

            // initialise points when the game begins
            _points.Clear();
            _points.Add(0);
            _points.Add(0);

            // Reset Dice Value
            _diceValue.Clear();

            ChooseGameSetting();

            // implementation for 2-player version
            if (_setting == 1)
            {
                // reset the points
                _points[0] = 0;
                _points[1] = 0;

                // initialise the turn for the beginning
                _currentTurn = 1;

                // 2 players throw all 5 dice at the beginning
                while (true)
                {
                    while (true)
                    {
                        Console.WriteLine($"Player {_currentTurn}. Press '{_currentTurn}' to roll your dice.");
                        try
                        {
                            playerAction = int.Parse(Console.ReadLine());
                            if (playerAction != _currentTurn)
                            {
                                continue;
                            }
                            else
                            {
                                break;
                            }
                        }
                        catch
                        {
                            Console.WriteLine($"Please press '{_currentTurn}' to roll your dice.");
                            continue;
                        }
                    }

                    Console.WriteLine("\n");

                    // Roll 5 dice
                    for (int i = 0; i < 5; i++)
                    {
                        dice[i].Roll();
                        _diceValue.Add(dice[i].CurrentValue);
                        Console.WriteLine($"Player {_currentTurn}'s die {i + 1} value: {_diceValue[i]}.");
                    }

                    // Test Dice Range
                    _threeOrMoreTesting.GetCheckDieRange();

                    // Update kind
                    FindKind(_diceValue);

                    if (_kind != 2)
                    {
                        // Save Current Point
                        _currentPoint = _points[_currentTurn - 1];

                        // Calculate Points
                        CalculatePoint();

                        // Test Points Calculation
                        _threeOrMoreTesting.GetCheckPointsCalculation();

                        Console.WriteLine("\n");
                        Console.WriteLine($"Player 1's points: {_points[0]}");
                        Console.WriteLine($"Player 2's points: {_points[1]}");
                        Console.WriteLine("\n");

                        // Check if the condition is met
                        if (CheckStatus() == true)
                        {
                            // Test game status
                            _threeOrMoreTesting.GetCheckGameCondition();

                            // Find Winner
                            FindWinner_2Player();

                            // Update Points Statistics
                            _statistics.UpdatePoints(_points);

                            // Update Winner Statistics
                            _statistics.UpdateWinner(_winner);

                            // Reset Status
                            _status = false;                       

                            break;
                        }

                        // Test game status
                        _threeOrMoreTesting.GetCheckGameCondition();

                        // Update turn
                        UpdateTurn();

                        // Clear dice value list
                        _diceValue.Clear();

                        continue;
                    }
                    else
                    {
                        Console.WriteLine($"Player {_currentTurn}. You have 2-of-a-kind.");
                        while (true)
                        {
                            Console.WriteLine($"Player {_currentTurn}.");
                            Console.WriteLine("Press '1': reroll the remaining dice.");
                            Console.WriteLine("Press '2': reroll all the dice.");

                            try
                            {
                                playerAction = int.Parse(Console.ReadLine());
                                if (playerAction != 1 && playerAction != 2)
                                {
                                    Console.WriteLine("Please press '1' or '2'.");
                                    continue;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Please press '1' or '2'.");
                                continue;
                            }
                        }

                        // Player chooses to reroll the remaining or all the dice
                        if (playerAction == 1)
                        {
                            List<int> rerolledDiceValue = new List<int>();

                            Console.WriteLine("\n");

                            for (int i = 0; i < 5; i++)
                            {
                                if (_kindValue != _diceValue[i])
                                {
                                    dice[i].Roll();
                                    Console.WriteLine($"Player {_currentTurn}'s die number {i + 1} rerolled value: {dice[i].CurrentValue}");
                                    rerolledDiceValue.Add(dice[i].CurrentValue);
                                }
                                else
                                {
                                    Console.WriteLine($"Player {_currentTurn}'s die number {i + 1}: {dice[i].CurrentValue}");
                                    rerolledDiceValue.Add(dice[i].CurrentValue);
                                }
                            }
                            
                            // Test Rerolled Dice Range
                            _threeOrMoreTesting.CheckRerolledDieRange(rerolledDiceValue);

                            // Update Kind
                            FindKind(rerolledDiceValue);

                            // Save Current Point
                            _currentPoint = _points[_currentTurn - 1];

                            // Calculate Points
                            CalculatePoint();

                            // Test Points Calculation
                            _threeOrMoreTesting.GetCheckPointsCalculation();

                            Console.WriteLine("\n");
                            Console.WriteLine($"Player 1's points: {_points[0]}");
                            Console.WriteLine($"Player 2's points: {_points[1]}");
                            Console.WriteLine("\n");

                            // Check if the condition is met
                            if(CheckStatus() == true)
                            {
                                // Test Game Status
                                _threeOrMoreTesting.GetCheckGameCondition();

                                // Find Winner
                                FindWinner_2Player();

                                // Update Points Statistics
                                _statistics.UpdatePoints(_points);

                                // Update Winners Statistics
                                _statistics.UpdateWinner(_winner);

                                // Reset Status
                                _status = false;

                                break;
                            }

                            // Test Game Status
                            _threeOrMoreTesting.GetCheckGameCondition();

                            // Update turn
                            UpdateTurn();

                            // Clear dice value list
                            _diceValue.Clear();
                            rerolledDiceValue.Clear();

                            continue;
                        }
                        else if (playerAction == 2)
                        {
                            // Clear dice value list
                            _diceValue.Clear();

                            Console.WriteLine("\n");

                            // Reroll all 5 dice
                            for (int i = 0; i < 5; i++)
                            {
                                dice[i].Roll();
                                Console.WriteLine($"Player {_currentTurn}'s die {i+1} value: {dice[i].CurrentValue}");
                                _diceValue.Add(dice[i].CurrentValue);
                            }

                            // Test Dice Range
                            _threeOrMoreTesting.GetCheckDieRange();

                            // Update kind
                            FindKind(_diceValue);

                            // Save Current Point
                            _currentPoint = _points[_currentTurn - 1];

                            // Calculate points
                            CalculatePoint() ;

                            // Test Points Calculation
                            _threeOrMoreTesting.GetCheckPointsCalculation();

                            Console.WriteLine("\n");
                            Console.WriteLine($"Player 1's points: {_points[0]}");
                            Console.WriteLine($"Player 2's points: {_points[1]}");
                            Console.WriteLine("\n");

                            // Check if the condition is met
                            if (CheckStatus() == true)
                            {
                                // Test Game Status
                                _threeOrMoreTesting.GetCheckGameCondition();   

                                // Find winner
                                FindWinner_2Player();

                                // Update Points Statistics
                                _statistics.UpdatePoints(_points);

                                // Update Winners Statistics
                                _statistics.UpdateWinner(_winner);

                                // Reset Status
                                _status = false;

                                break;
                            }

                            // Test Game Status
                            _threeOrMoreTesting.GetCheckGameCondition();

                            // Update turn
                            UpdateTurn();

                            // Clear dice value list
                            _diceValue.Clear();
                        }
                    }
                }
            }
            // implementation for human-computer version
            else if (_setting == 2)
            {
                // reset the points
                _points[0] = 0;
                _points[1] = 0;

                // initialise the turn for the beginning
                _currentTurn = 1;

                while (true)
                {
                    // Implementation for human turn
                    if (_currentTurn == 1)
                    {
                        while (true)
                        {
                            Console.WriteLine($"Human. Press '{_currentTurn}' to roll your dice.");
                            try
                            {
                                playerAction = int.Parse(Console.ReadLine());
                                if (playerAction != _currentTurn)
                                {
                                    continue;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            catch
                            {
                                Console.WriteLine($"Please press '{_currentTurn}' to roll your dice.");
                                continue;
                            }
                        }

                        Console.WriteLine("\n");

                        // Roll 5 dice
                        for (int i = 0; i < 5; i++)
                        {
                            dice[i].Roll();
                            _diceValue.Add(dice[i].CurrentValue);
                            Console.WriteLine($"Human's die {i + 1} value: {_diceValue[i]}.");
                        }

                        // Test Dice Range
                        _threeOrMoreTesting.GetCheckDieRange();

                        // Update kind
                        FindKind(_diceValue);

                        if (_kind != 2)
                        {
                            // Save Current Points
                            _currentPoint = _points[_currentTurn - 1];

                            // Calculate Points
                            CalculatePoint();

                            // Test Points Calculation
                            _threeOrMoreTesting.GetCheckPointsCalculation();

                            Console.WriteLine("\n");
                            Console.WriteLine($"Human's points: {_points[0]}");
                            Console.WriteLine($"Computer's points: {_points[1]}");
                            Console.WriteLine("\n");

                            if (CheckStatus() == true)
                            {
                                // Test Game Status
                                _threeOrMoreTesting.GetCheckGameCondition();

                                // Find Winner
                                FindWinner_HumanComputer();

                                // Update Points Statistics
                                _statistics.UpdatePoints(_points);

                                // Update Winners Statistics
                                _statistics.UpdateWinner(_winner);

                                // Reset Status
                                _status = false;

                                break;
                            }

                            // Test Game Status
                            _threeOrMoreTesting.GetCheckGameCondition();

                            // Update turn
                            UpdateTurn();

                            // Clear dice value list
                            _diceValue.Clear();

                            continue;
                        }
                        else
                        {
                            Console.WriteLine($"Human. You have 2-of-a-kind.");
                            while (true)
                            {
                                Console.WriteLine("Human.");
                                Console.WriteLine("Press '1': reroll the remaining dice.");
                                Console.WriteLine("Press '2': reroll all the dice.");

                                try
                                {
                                    playerAction = int.Parse(Console.ReadLine());
                                    if (playerAction != 1 && playerAction != 2)
                                    {
                                        Console.WriteLine("Please press '1' or '2'.");
                                        continue;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                catch
                                {
                                    Console.WriteLine("Please press '1' or '2'.");
                                    continue;
                                }
                            }

                            // Player chooses to reroll the remaining or all the dice
                            if (playerAction == 1)
                            {
                                List<int> rerolledDiceValue = new List<int>();

                                Console.WriteLine("\n");

                                // Reroll the 3 remaining dice
                                for (int i = 0; i < 5; i++)
                                {
                                    if (_kindValue != _diceValue[i])
                                    {
                                        dice[i].Roll();
                                        Console.WriteLine($"Human's die number {i + 1} rerolled value: {dice[i].CurrentValue}");
                                        rerolledDiceValue.Add(dice[i].CurrentValue);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Human's die number {i + 1}: {dice[i].CurrentValue}");
                                        rerolledDiceValue.Add(dice[i].CurrentValue);
                                    }
                                }

                                // Test Rerolled Dice Range
                                _threeOrMoreTesting.CheckRerolledDieRange(rerolledDiceValue);   

                                // Update Kind
                                FindKind(rerolledDiceValue);

                                // Save Current Point
                                _currentPoint = _points[_currentTurn - 1];

                                // Calculate Points
                                CalculatePoint();

                                // Test Points Calculation
                                _threeOrMoreTesting.GetCheckPointsCalculation();

                                Console.WriteLine("\n");
                                Console.WriteLine($"Human's points: {_points[0]}");
                                Console.WriteLine($"Computer's points: {_points[1]}");
                                Console.WriteLine("\n");

                                // Check if the condition is met
                                if(CheckStatus() == true)
                                {
                                    // Test Game Status
                                    _threeOrMoreTesting.GetCheckGameCondition();

                                    // Find Winner
                                    FindWinner_HumanComputer();

                                    // Update Points Statistics
                                    _statistics.UpdatePoints(_points);

                                    // Update Winners Statistics
                                    _statistics.UpdateWinner(_winner);

                                    // Reset Status
                                    _status = false;

                                    break;
                                }

                                // Test Game Status
                                _threeOrMoreTesting.GetCheckGameCondition();

                                // Update turn
                                UpdateTurn();

                                // Clear dice value list
                                _diceValue.Clear();
                                rerolledDiceValue.Clear();

                                continue;
                            }
                            else if (playerAction == 2)
                            {
                                _diceValue.Clear();

                                for (int i = 0; i < 5; i++)
                                {
                                    dice[i].Roll();
                                    Console.WriteLine($"Human's die {i + 1} value: {dice[i].CurrentValue}");
                                    _diceValue.Add(dice[i].CurrentValue);
                                }                                

                                // Test Dice Range
                                _threeOrMoreTesting.GetCheckDieRange();

                                // Update kind
                                FindKind(_diceValue);

                                // Save Current Points
                                _currentPoint = _points[_currentTurn - 1];

                                // Calculate points
                                CalculatePoint();

                                // Test Point Calculation
                                _threeOrMoreTesting.GetCheckPointsCalculation();

                                Console.WriteLine("\n");
                                Console.WriteLine($"Human's points: {_points[0]}");
                                Console.WriteLine($"Computer's points: {_points[1]}");
                                Console.WriteLine("\n");

                                // Check if the condition is met
                                if (CheckStatus() == true)
                                {
                                    // Test Game Status
                                    _threeOrMoreTesting.GetCheckGameCondition();

                                    // Find Winner
                                    FindWinner_HumanComputer();

                                    // Update Points Statistics
                                    _statistics.UpdatePoints( _points);

                                    // Update Winners Statistics
                                    _statistics.UpdateWinner(_winner);

                                    // Reset Status
                                    _status = false;

                                    break;
                                }

                                // Test Game Status
                                _threeOrMoreTesting.GetCheckGameCondition();

                                // Update turn
                                UpdateTurn();

                                // Clear dice value list
                                _diceValue.Clear();

                                continue;
                            }
                        }
                    }
                    // Implementation for computer turn
                    else
                    {
                        // Automatically roll all 5 dice at the beginning of each turn
                        for (int i = 0; i < 5; i++)
                        {
                            dice[i].Roll();
                            _diceValue.Add(dice[i].CurrentValue);
                            Console.WriteLine($"Computer's die {i + 1} value: {_diceValue[i]}.");
                        }

                        // Test Dice Range
                        _threeOrMoreTesting.GetCheckDieRange();

                        // Update kind
                        FindKind(_diceValue);

                        // Check for kinds
                        if (_kind != 2)
                        {
                            // Save Current Point
                            _currentPoint = _points[_currentTurn - 1];

                            // Calculate Points
                            CalculatePoint();

                            // Test Points Calculation
                            _threeOrMoreTesting.GetCheckPointsCalculation();

                            Console.WriteLine("\n");
                            Console.WriteLine($"Human's points: {_points[0]}");
                            Console.WriteLine($"Computer's points: {_points[1]}");
                            Console.WriteLine("\n");

                            if (CheckStatus() == true)
                            {
                                // Test Game Status
                                _threeOrMoreTesting.GetCheckGameCondition();

                                // Find Winner
                                FindWinner_HumanComputer();

                                // Update Points Statistics
                                _statistics.UpdatePoints(_points);

                                // Update Winners Statistics
                                _statistics.UpdateWinner(_winner);

                                // Reset Status
                                _status = false;

                                break;
                            }

                            // Test Game Status
                            _threeOrMoreTesting.GetCheckGameCondition();

                            // Update turn
                            UpdateTurn();

                            // Clear dice value list
                            _diceValue.Clear();

                            continue;
                        }
                        else
                        {
                            // Randomly generate a choice to reroll the remaining or all 5 dice
                            Random rd = new Random();
                            playerAction = rd.Next(1, 3);

                            // Announce the computer's choice
                            if(playerAction == 1)
                            {
                                Console.WriteLine("Computer's choice: Reroll the remaining.");
                            }
                            else
                            {
                                Console.WriteLine("Computer's choice: Reroll all 5 dice.");
                            }                         

                            // Reroll the 3 remaining
                            if (playerAction == 1)
                            {
                                List<int> rerolledDiceValue = new List<int>();

                                Console.WriteLine("\n");
                              
                                for (int i = 0; i < 5; i++)
                                {
                                    if (_kindValue != _diceValue[i])
                                    {
                                        dice[i].Roll();
                                        Console.WriteLine($"Computer's die number {i + 1} rerolled value: {dice[i].CurrentValue}");
                                        rerolledDiceValue.Add(dice[i].CurrentValue);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Computer's die number {i + 1}: {dice[i].CurrentValue}");
                                        rerolledDiceValue.Add(dice[i].CurrentValue);
                                    }
                                }

                                // Test Dice Range
                                _threeOrMoreTesting.CheckRerolledDieRange(rerolledDiceValue);

                                // Update Kind
                                FindKind(rerolledDiceValue);

                                // Save Current Point
                                _currentPoint = _points[_currentTurn - 1];

                                // Calculate Points
                                CalculatePoint();

                                // Test Points Calculation
                                _threeOrMoreTesting.GetCheckPointsCalculation();

                                Console.WriteLine("\n");
                                Console.WriteLine($"Human's points: {_points[0]}");
                                Console.WriteLine($"Computer's points: {_points[1]}");
                                Console.WriteLine("\n");

                                // Check game condition
                                if(CheckStatus() == true)
                                {
                                    // Test Game Status
                                    _threeOrMoreTesting.GetCheckGameCondition();

                                    // Find Winner
                                    FindWinner_HumanComputer();

                                    // Update Points Statistics
                                    _statistics.UpdatePoints(_points);

                                    // Update Winners Statistics
                                    _statistics.UpdateWinner(_winner);
                                                     
                                    // Reset Status
                                    _status = false;

                                    break;
                                }

                                // Test Game Status
                                _threeOrMoreTesting.GetCheckGameCondition();

                                // Update turn
                                UpdateTurn();

                                // Clear dice value list
                                _diceValue.Clear();
                                rerolledDiceValue.Clear();

                                continue;
                            }
                            // Reroll all 5 dice
                            else
                            {
                                _diceValue.Clear();

                                Console.WriteLine("\n");

                                // Reroll 5 dice
                                for (int i = 0; i < 5; i++)
                                {
                                    dice[i].Roll();
                                    Console.WriteLine($"Computer's die {i + 1} value: {dice[i].CurrentValue}");
                                    _diceValue.Add(dice[i].CurrentValue);
                                }

                                // Test Dice Range
                                _threeOrMoreTesting.GetCheckDieRange();

                                // Update kind
                                FindKind(_diceValue);

                                // Save Current Point
                                _currentPoint = _points[_currentTurn - 1];

                                // Calculate points
                                CalculatePoint();

                                // Test Points Calculation
                                _threeOrMoreTesting.GetCheckPointsCalculation();

                                Console.WriteLine("\n");
                                Console.WriteLine($"Human's points: {_points[0]}");
                                Console.WriteLine($"Computer's points: {_points[1]}");
                                Console.WriteLine("\n");

                                // Check if the condition is met
                                if (CheckStatus() == true)
                                {
                                    // Test Game Status
                                    _threeOrMoreTesting.GetCheckGameCondition();

                                    // Find Winner
                                    FindWinner_HumanComputer();

                                    // Update Points Statistics
                                    _statistics.UpdatePoints(_points);

                                    // Update Winner Statistics
                                    _statistics.UpdateWinner(_winner);

                                    // Reset Status
                                    _status= false;

                                    break;
                                }

                                // Test Game Status
                                _threeOrMoreTesting.GetCheckGameCondition();

                                // Update turn
                                UpdateTurn();

                                // Clear dice value list
                                _diceValue.Clear();

                                continue;
                            }
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
            if (_kind == 3)
            {
                _points[_currentTurn - 1] += 3;
            }
            else if (_kind == 4)
            {
                _points[_currentTurn - 1] += 6;
            }
            else if (_kind == 5)
            {
                _points[_currentTurn - 1] += 12;
            }

            return _points[_currentTurn - 1];
        }

        /// <summary>
        /// Implementation for the FindWinner_2Player abstract method
        /// </summary>
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
        /// Implementation for the CheckStatus abstract class
        /// </summary>
        /// <returns>
        /// status boolean value
        /// </returns>
        public override bool CheckStatus()
        {
            for (int i = 0; i < 2; i++)
            {
                if (_points[i] >= 20)
                {
                    _status = true;
                }
            }
            return _status;
        }
        
        /// <summary>
        /// Implementation for the FindWinner_2Player abstract class
        /// </summary>
        public override void FindWinner_2Player()
        {
            if (_points[0] >= 20)
            {
                _winner = "Player 1";
                Console.WriteLine("PLAYER 1 WINS.");
            }
            else if (_points[1] >= 20)
            {
                _winner = "Player 2";
                Console.WriteLine("PLAYER 2 WINS.");
            }           
        }

        /// <summary>
        /// Implementation for the FindWinner_HumanComputer abstract class
        /// </summary>
        public override void FindWinner_HumanComputer()
        {
            if (_points[0] >= 20)
            {
                _winner = "Human";
                Console.WriteLine("HUMAN WINS.");
            }
            else if (_points[1] >= 20)
            {
                _winner = "Computer";
                Console.WriteLine("COMPUTER WINS.");
            }
        }

        /// <summary>
        /// A method to find the kind made from 5 dice values
        /// </summary>
        /// <param name="numbers"> A list of dice values </param>
        /// <returns>
        /// the kind
        /// the kind value
        /// </returns>
        public (int, int) FindKind(List<int> numbers)
        {
            int[] kindValue = new int[2];
            _kind = 1;
            _kindValue = 0;

            var groupedValue = numbers.GroupBy(n => n);

            foreach (var group in groupedValue)
            {
                if (group.Count() > _kind)
                {
                    _kind = group.Count();
                    _kindValue = group.Key;
                }
            }
            return (_kind, _kindValue);
        }

    }
}
