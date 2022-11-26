using System;

namespace Project_SimonStasovski
{
    class Program
    {
        static int it, userInput;
        static bool validateInput;
        static void Main(string[] args)
        {

            bool menuLoop = true;

            do
            {
                
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WindowWidth = 100;
                Console.WindowHeight = 15 ;
                Console.CursorVisible = true;

                Console.WriteLine("                                      7/11                                      ");
                Console.WriteLine("//============================================================================\\\\");
                Console.WriteLine("||Please choose one of the following options by entering the associated number||");
                Console.WriteLine("||============================================================================||");
                Console.WriteLine("||1. Play Game                                                                ||");
                Console.WriteLine("||2. How to Play                                                              ||");
                Console.WriteLine("||3. Quit Game                                                                ||");
                Console.WriteLine("\\\\============================================================================//");

                GetUserInput(out userInput, "Which option would you like to select: ", "Please input a number: ", "Please input one of the following numbers \"1\", \"2\" or \"3\": ");

                switch (userInput)
                {
                    case 1:
                        Game();
                        break;
                    case 2:
                        Instructions();
                        break;
                    case 3:
                        menuLoop = ExitMenuLoop();
                        break;
                }
            } while (menuLoop);
        }
        static void GetUserInput(out int userInput, string question, string errorNotNum, string errorWrongNum)
        {
            Console.Write(question);
            do
            {
                validateInput = int.TryParse(Console.ReadLine(), out userInput);
                Console.WriteLine();
                if (!validateInput)
                {
                    Console.Write(errorNotNum);
                }
                else if (validateInput && (userInput > 4 || userInput < 1))
                {
                    Console.Write(errorWrongNum);
                    validateInput = false;
                }
            } while (!validateInput);
        }
        static void Game()
        {

            Console.Clear();
            Console.CursorVisible = false;
            Console.WindowWidth = 100;
            Console.WindowHeight = 24;
            DateTime currentTimePlayer = DateTime.Now;
            DateTime currentTimeMagentaEnemy = DateTime.Now;
            DateTime currentTimeRedEnemy = DateTime.Now;

            int playerScore = -1, enemyScore = 0, playerSpeed = 0, enemySpeed = 600, defaultX = 0, defaultY = 0;
            bool gameLoop = true, playerCollide = false, playerWin = false, enemyWinScore = false, playerExit = false;

            int[] cursorX = new int[2] { 38, defaultX };
            int[] cursorY = new int[2] { 13, defaultY };

            int[] objectiveX = new int[2] { cursorX[defaultX], cursorX[defaultX] };
            int[] objectiveY = new int[2] { cursorY[defaultY], cursorY[defaultY] };

            int[] magentaEnemiesArrayX = new int[4] { 15, 60, defaultX, defaultX};
            int[] magentaEnemiesArrayY = new int[4] { 22, 22, defaultY, defaultY};

            int[] redEnemiesArrayX = new int[4] { 15, 60, defaultX, defaultX};
            int[] redEnemiesArrayY = new int[4] { 10, 10, defaultY, defaultY};
            do
            {
                Console.SetCursorPosition(defaultX, defaultY);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("                                    7/11                                    ");
                Console.WriteLine("//======================================================================\\\\");
                Console.WriteLine("  Your score: " + playerScore + "                                                          ");
                Console.WriteLine("  Enemy score: " + enemyScore + "                                                          ");
                Console.WriteLine("\\\\======================================================================//");

                if (Wait(ref currentTimePlayer, playerSpeed))
                {
                    MoveUserChar(UserKeyStroke(), ref gameLoop, ref cursorX, ref cursorY);
                }

                if (Wait(ref currentTimeMagentaEnemy, enemySpeed))
                {
                    MoveMagentaEnemies(ref magentaEnemiesArrayX, ref magentaEnemiesArrayY, ref objectiveX, ref objectiveX);
                }

                if (Wait(ref currentTimeRedEnemy, enemySpeed))
                {
                    MoveRedEnemies(ref redEnemiesArrayX, ref redEnemiesArrayY, ref cursorX, ref cursorY);
                }

                SetDropsLocation(ref objectiveX, ref objectiveY);

                DrawGame(ref objectiveX, ref objectiveY, ref magentaEnemiesArrayX, ref magentaEnemiesArrayY, ref gameLoop, ref cursorX, ref cursorY, ref redEnemiesArrayX, ref redEnemiesArrayY, ref playerScore, ref enemyScore, ref playerCollide);

                CheckScore(ref playerScore, ref enemyScore, ref playerCollide, ref gameLoop, ref playerWin, ref enemyWinScore, ref playerExit);

            } while (gameLoop);
            Console.SetCursorPosition(defaultX, defaultY);
            Console.BackgroundColor = ConsoleColor.Black;
            GameOverScreen(ref playerWin, ref enemyWinScore, ref playerCollide, ref playerExit);
            
        }
        static ConsoleKey UserKeyStroke()
        {
            ConsoleKey userKeyInput = ConsoleKey.NoName;

            if (Console.KeyAvailable)
            {
                userKeyInput = Console.ReadKey(true).Key;
            }

            return userKeyInput;
        }
        static void MoveUserChar(ConsoleKey userKeyInput, ref bool gameLoop, ref int[] cursorX, ref int[] cursorY)
        {

            switch (userKeyInput)
            {
                case (ConsoleKey.W):
                    cursorY[0]--;
                    break;
                case (ConsoleKey.D):
                    cursorX[0]++;
                    break;
                case (ConsoleKey.S):
                    cursorY[0]++;
                    break;
                case (ConsoleKey.A):
                    cursorX[0]--;
                    break;
                case (ConsoleKey.Escape):
                    gameLoop = false;
                    break;
            }
     
        }
        static void DrawGame(ref int[] objectiveX, ref int[] objectiveY, ref int[] enemiesArrayX, ref int[] enemiesArrayY, ref bool gameLoop, ref int[] cursorX, ref int[] cursorY, ref int[] redEnemiesArrayX, ref int[] redEnemiesArrayY, ref int playerScore, ref int enemyScore, ref bool playerCollide)
        {
            Console.SetCursorPosition(cursorX[1], cursorY[1]);

            bool objectiveCaught = false;

            if (cursorX[0] >= Console.WindowWidth)
            {
                cursorX[0] = 0;
            }
            if (cursorX[0] < 0)
            {
                cursorX[0] = Console.WindowWidth - 1;
            }
            if (cursorY[0] >= Console.WindowHeight)
            {
                cursorY[0] = 5;
            }
            if (cursorY[0] < 5)
            {
                cursorY[0] = Console.WindowHeight - 1;
            }

            if (cursorX[0] == cursorX[1] && cursorY[0] == cursorY[1])
            {
                Console.Write(" ", Console.BackgroundColor = ConsoleColor.Green);
            }
            else
            {
                Console.Write(" ", Console.BackgroundColor = ConsoleColor.Black);
            }

            cursorX[1] = cursorX[0];
            cursorY[1] = cursorY[0];

            for (it = 0; it < enemiesArrayX.Length / 2; it++)
            {

                if (enemiesArrayX[it] >= Console.WindowWidth)
                {
                    enemiesArrayX[it] = 0;
                }
                if (enemiesArrayX[it] < 0)
                {
                    enemiesArrayX[it] = Console.WindowWidth - 1;
                }
                if (enemiesArrayY[it] >= Console.WindowHeight)
                {
                    enemiesArrayY[it] = 5;
                }
                if (enemiesArrayY[it] < 5)
                {
                    enemiesArrayY[it] = Console.WindowHeight - 1;
                }

                if (enemiesArrayX[it] == enemiesArrayX[it + 2] && enemiesArrayY[it] == enemiesArrayY[it + 2])
                {

                }
                else
                {
                    Console.SetCursorPosition(enemiesArrayX[it], enemiesArrayY[it]);

                    Console.Write(" ", Console.BackgroundColor = ConsoleColor.Magenta);

                    Console.SetCursorPosition(enemiesArrayX[it + 2], enemiesArrayY[it + 2]);

                    Console.Write(" ", Console.BackgroundColor = ConsoleColor.Black);
                }

                enemiesArrayX[it + 2] = enemiesArrayX[it];
                enemiesArrayY[it + 2] = enemiesArrayY[it];

                if (!objectiveCaught)
                {
                    if (enemiesArrayX[it] == objectiveX[1] && enemiesArrayY[it] == objectiveY[1])
                    {

                        objectiveCaught = true;
                        objectiveX[1] = objectiveX[0];
                        objectiveY[1] = objectiveY[0];
                        enemyScore += 1;
                    }
                    else
                    {
                        objectiveCaught = false;
                    }
                }

                if (objectiveCaught)
                {
                    Console.SetCursorPosition(objectiveX[0], objectiveY[0]);

                    Console.Write(" ", Console.BackgroundColor = ConsoleColor.Yellow);
                }
                else
                {
                    objectiveCaught = false;
                }

                if (enemiesArrayX[it] == cursorX[0] && enemiesArrayY[it] == cursorY[0])
                {
                    gameLoop = false;
                    playerCollide = true;
                }

                if (enemiesArrayX[0] == enemiesArrayX[1] && enemiesArrayY[0] == enemiesArrayY[1])
                {
                    for (it = 0; it < enemiesArrayX.Length / 2; it++)
                    {
                        enemiesArrayX[0]++;
                        enemiesArrayY[0]++;
                        enemiesArrayY[1]--;
                        enemiesArrayY[1]--;
                    }
                }
            }
            for (it = 0; it < redEnemiesArrayX.Length/2; it++)
            {

                if (redEnemiesArrayX[it] >= Console.WindowWidth)
                {
                    redEnemiesArrayX[it] = 0;
                }
                if (redEnemiesArrayX[it] < 0)
                {
                    redEnemiesArrayX[it] = Console.WindowWidth - 1;
                }
                if (redEnemiesArrayY[it] >= Console.WindowHeight)
                {
                    redEnemiesArrayY[it] = 5;
                }
                if (redEnemiesArrayY[it] < 5)
                {
                    redEnemiesArrayY[it] = Console.WindowHeight - 1;
                }

                if (redEnemiesArrayX[it] == redEnemiesArrayX[it + 2] && redEnemiesArrayY[it] == redEnemiesArrayY[it + 2])
                {

                }
                else
                {
                    Console.SetCursorPosition(redEnemiesArrayX[it], redEnemiesArrayY[it]);

                    Console.Write(" ", Console.BackgroundColor = ConsoleColor.Red);

                    Console.SetCursorPosition(redEnemiesArrayX[it + 2], redEnemiesArrayY[it + 2]);

                    Console.Write(" ", Console.BackgroundColor = ConsoleColor.Black);
                }

                redEnemiesArrayX[it + 2] = redEnemiesArrayX[it];
                redEnemiesArrayY[it + 2] = redEnemiesArrayY[it];

                if (redEnemiesArrayX[0] == redEnemiesArrayX[1] && redEnemiesArrayY[0] == redEnemiesArrayY[1])
                {
                    for (it = 0; it < redEnemiesArrayX.Length / 2; it++)
                    {
                        redEnemiesArrayX[0]++;
                        redEnemiesArrayY[0]++;
                        redEnemiesArrayY[1]--;
                        redEnemiesArrayY[1]--;
                    }
                }

                if (redEnemiesArrayX[it] == objectiveX[1] && redEnemiesArrayY[it] == objectiveY[1])
                {

                    objectiveCaught = true;
                    objectiveX[1] = objectiveX[0];
                    objectiveY[1] = objectiveY[0];
                    enemyScore += 1;
                }
                else
                {
                    objectiveCaught = false;
                }

                if (objectiveCaught)
                {
                    Console.SetCursorPosition(objectiveX[0], objectiveY[0]);

                    Console.Write(" ", Console.BackgroundColor = ConsoleColor.Yellow);
                }

                if (redEnemiesArrayX[it] == cursorX[0] && redEnemiesArrayY[it] == cursorY[0])
                {
                    gameLoop = false;
                    playerCollide = true;
                }
            }

            if (!objectiveCaught)
            {
                if (cursorX[0] == objectiveX[1] && cursorY[0] == objectiveY[1])
                {

                    objectiveCaught = true;
                    objectiveX[1] = objectiveX[0];
                    objectiveY[1] = objectiveY[0];
                    playerScore += 1;
                } 
                else
                {
                    objectiveCaught = false;
                }

            }
            if (objectiveCaught)
            {
                Console.SetCursorPosition(objectiveX[0], objectiveY[0]);

                Console.Write(" ", Console.BackgroundColor = ConsoleColor.Yellow);
            }

        }
        static void SetDropsLocation(ref int[] objectiveX, ref int[] objectiveY)
        {
            Random objectiveLocation = new Random();
            objectiveX[0] = objectiveLocation.Next(3, Console.WindowWidth - 3);
            objectiveY[0] = objectiveLocation.Next(9, Console.WindowHeight - 3);

        }
        static void MoveMagentaEnemies(ref int[] enemiesArrayX, ref int[] enemiesArrayY, ref int[] ObjectiveX, ref int[] ObjectiveY)
        {
            for (it = 0; it < enemiesArrayX.Length/2; it++)
            {
                if (enemiesArrayX[it] < ObjectiveX[1])
                {
                    enemiesArrayX[it]++;
                }
                else if (enemiesArrayX[it] > ObjectiveX[1])
                {
                    enemiesArrayX[it]--;
                }
                else if (enemiesArrayX[it] == ObjectiveX[1])
                {
                    if (enemiesArrayY[it] < ObjectiveY[1])
                    {
                        enemiesArrayY[it]++;
                    }
                    else if (enemiesArrayY[it] > ObjectiveY[1])
                    {
                        enemiesArrayY[it]--;
                    }
                }
                else if (enemiesArrayY[it] < ObjectiveY[1])
                {
                    enemiesArrayY[it]++;
                }
                else if (enemiesArrayY[it] > ObjectiveY[1])
                {
                    enemiesArrayY[it]--;
                }

            }
        }
        static bool Wait(ref DateTime currentTime, int timeWaiting)
        {


            if (Math.Abs(DateTime.Now.Millisecond - currentTime.Millisecond) > timeWaiting)
            {
                currentTime = DateTime.Now;
                return true;
            }
            else
            {
                return false;
            }
        }
        static void MoveRedEnemies(ref int[] enemiesArrayX, ref int[] enemiesArrayY, ref int[] oldCursorX, ref int[] oldCursorY)
        {
            for (it = 0; it < enemiesArrayX.Length/2; it++)
            {
                if (enemiesArrayX[it] < oldCursorX[1])
                {
                    enemiesArrayX[it]++;
                }
                else if (enemiesArrayX[it] > oldCursorX[1])
                {
                    enemiesArrayX[it]--;
                }
                else if (enemiesArrayY[it] < oldCursorY[1])
                {
                    enemiesArrayY[it]++;
                }
                else if (enemiesArrayY[it] > oldCursorY[1])
                {
                    enemiesArrayY[it]--;
                }
            }
        }
        static bool ExitMenuLoop(string questionForUser = "Are you sure you want to exit the game? Enter [y]es or [n}o. ",  string errorMessage = "The input \"{0}\" is invalid. Please enter \"y\" for yes or \"n\" for no: ")
        {
            string choiceOfUser;
            Console.Write(questionForUser);

            validateInput = false;
            do
            {
                choiceOfUser = Console.ReadLine();
                if (choiceOfUser.ToLower() == "y")
                {
                    validateInput = true;
                    Console.Clear();
                    return false;
                }
                else if (choiceOfUser.ToLower() == "n")
                { 
                    validateInput = true;
                    Console.Clear();
                    return true;
                }
                else
                {
                    Console.WriteLine();
                    Console.Write(errorMessage, choiceOfUser);
                }
            } while (!validateInput);
            Console.Clear();
            return false;
        }
        static void Instructions()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WindowWidth = 80;
            Console.WindowHeight = 24;
            Console.CursorVisible = true;
            Console.Clear();
            
            Console.WriteLine("                                  How to Play                                   ");
            Console.WriteLine("//============================================================================\\\\");
            Console.WriteLine("||1. Controls:                                                                ||");
            Console.WriteLine("||   W key: Move Up                                                           ||");
            Console.WriteLine("||   A key: Move Left                                                         ||");
            Console.WriteLine("||   S key: Move Down                                                         ||");
            Console.WriteLine("||   D key: Move Right                                                        ||");
            Console.WriteLine("||   Escape key: Exit Game                                                    ||");
            Console.WriteLine("||============================================================================||");
            Console.WriteLine("||2. How to Win/Lose:                                                         ||");
            Console.WriteLine("||   There is one way to win, by collecting 11 coins before the enemies       ||");
            Console.WriteLine("||   collect 7 coins. The coins spawn in random locations.                    ||");
            Console.WriteLine("||   There are two ways to lose, getting hit by any enemy or the enemies get  ||");
            Console.WriteLine("||   7 coins before you get 11 coins.                                         ||");
            Console.WriteLine("||============================================================================||");
            Console.WriteLine("||3. Enemy types and behaviour:                                               ||");
            Console.WriteLine("||   Magenta enemies: they will not chase you, they will only go for coins. Be||");
            Console.WriteLine("||   careful, they can still kill you if you touch them.                      ||");
            Console.WriteLine("||   Red enemies: they will chase you, they will not go for the coins however,||");
            Console.WriteLine("||   they will not hesitate to pick up any coins if they are in their way.    ||");
            Console.WriteLine("\\\\============================================================================//");

            Console.Write("Press any key to return: ");
            Console.ReadKey();
            Console.Clear();
        }
        static void CheckScore(ref int playerScore, ref int enemyScore, ref bool playerCollide, ref bool gameLoop, ref bool playerWin, ref bool enemyWinScore, ref bool playerExit)
        {
            
            if (!gameLoop)
            {
                playerExit = true;
            }
            else if (playerScore == 11)
            {
                gameLoop = false;
                playerWin = true;
            }
            else if (enemyScore == 7)
            {
                gameLoop = false;
                enemyWinScore = true;
            }
            else if (playerCollide)
            {
                gameLoop = false;
                enemyWinScore = false;
            }

        }
        static void GameOverScreen(ref bool playerWin, ref bool enemyScore, ref bool playerCollide, ref bool playerExit)
        {
            Console.Clear();
            Console.SetCursorPosition(0,0);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WindowWidth = 80;
            Console.WindowHeight = 16;
            Console.CursorVisible = true;

            string endMessage = "||                                                                            ||";

            if (playerWin)
            {
                endMessage = "||                              Congrats! You Won!                            ||";
            }
            else if (enemyScore)
            {
                endMessage = "||                     Game Over! The Enemies got 7 Points!                   ||";
            }
            else if (playerCollide) 
            {
                endMessage = "||                     Game Over! The Enemies Killed You!                     ||";
            }
            else if (playerExit)
            {
                return;
            }

            Console.WriteLine("                                      7/11                                      ");
            Console.WriteLine("//============================================================================\\\\");
            Console.WriteLine("||                                                                            ||");
            Console.WriteLine("||                                                                            ||");
            Console.WriteLine(endMessage);
            Console.WriteLine("||                                                                            ||");
            Console.WriteLine("||                                                                            ||");
            Console.WriteLine("\\\\============================================================================//");
            Console.Write("Please enter any key to continue: ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ReadLine();
            Console.Clear();
        }
    }
}
