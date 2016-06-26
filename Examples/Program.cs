using System;
using NLog;

namespace OperationResult.Examples
{
    class Program
    {
        private static Logger logger = LogManager.GetLogger("x");

        static void Main(string[] args)
        {
            string msg = "Press any key to run the examples or Enter to exit.";
            Console.WriteLine(msg);

            // create a random number generator
            Random random = new Random();

            // loop until Enter is pressed
            while (Console.ReadKey(true).Key != ConsoleKey.Enter)
            {
                //-------------------------------------------------------------------------------------
                // fetch a typeless result and print a message after examining the result
                Result firstResult = ResultExample(random);

                // compare the ResultCode to the desired ResultCode
                if (firstResult.ResultCode != ResultCode.Success)
                    logger.Info("Failed to find a match.");
                else
                    logger.Info("Found a result!");

                
                //-------------------------------------------------------------------------------------
                // fetch a typed result
                Result<int> secondResult = ResultTExample(random);

                // check the ResultCode using the explicit operator
                // this is shorthand for the expression (secondResult.ResultCode == ResultCode.Success)
                if (!secondResult)
                    logger.Info("Failed to find a match.");
                else
                    logger.Info("Found a result! Result: " + secondResult.ReturnValue);

                logger.Info("-----------------------------------------------");

                //-------------------------------------------------------------------------------------
                // combine the results
                firstResult.Incorporate(secondResult);

                // print the combined results
                firstResult.LogResult(logger.Info);

                // repeat the instructions
                Console.WriteLine(msg);
            }
        }

        public static Result ResultExample(Random random)
        {
            // create a Result
            Result result = new Result();

            int num = 0;
            int attempts = 0;

            // loop no more than 1000 times
            while (attempts < 1000)
            {
                // get a new random number
                num = random.Next(100000);

                // if the number is a multiple of 1000, add some messages
                // to the result
                if (num % 1000 == 0)
                {
                    result.AddInfo("The lucky number is: " + num);
                    result.AddInfo("Attempts: " + attempts);
                    break;
                }
                // if not a multiple, continue trying
                else
                    attempts++;
            }

            // if no messages were added, we didn't find a match.
            // add an error message which sets ResultCode to Failure.
            if (result.Messages.Count == 0)
                result.AddError("Didn't find a matching number.");

            // log the results and return
            result.LogResult(logger.Info);
            return result;
        }

        public static Result<int> ResultTExample(Random random)
        {
            // create a new result with ReturnValue of type int
            Result<int> result = new Result<int>();

            int num = 0;
            int attempts = 0;

            // loop no more than 1000 times
            while (attempts < 1000)
            {
                // get a new random number
                num = random.Next(100000);

                // if the number a multiple of 1000, add some messages
                // to the result and set the ReturnValue to the matching
                // number
                if (num % 1000 == 0)
                {
                    result.AddInfo("The lucky number is: " + num);
                    result.AddInfo("Attempts: " + attempts);
                    result.ReturnValue = num;
                    break;
                }
                // if not a multiple, continue trying
                else
                    attempts++;
            }

            // if no messages were added, we didn't find a match.
            // add an error message which sets ResultCode to Failure.
            if (result.Messages.Count == 0)
                result.AddError("Didn't find a matching number.");
            
            // log the results and return
            result.LogResult(logger.Info);
            return result;
        }
    }
}
