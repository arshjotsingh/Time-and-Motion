/*  Program.cs 
 *  Description - This solves the rolling ball clock problem for at least 31 balls.
 *  Version 1.0 2014-04-17
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time_and_Motion
{
    class Program
    {
        // Method to insert balls in one minute trail
        static Stack<int> fillOneMinute(Queue<int> baseQueue, Stack<int> oneMinuteTrail,int numberOfBalls)
        {
            // loop till no balls left
            while (numberOfBalls>0)
            {
                oneMinuteTrail.Push(baseQueue.Dequeue());
                numberOfBalls--;
            }

            return oneMinuteTrail;
        }

        // Method to insert balls in one minute queue
        static Queue<int> fillOneMinuteQueue(Stack<int> oneMinuteTrail, Queue<int> oneMinuteQueue, int numberOfBalls)
        {
            // loop till no balls left
            while (numberOfBalls > 0)
            {
                oneMinuteQueue.Enqueue(oneMinuteTrail.Pop());
                numberOfBalls--;
            } 

            return oneMinuteQueue;
        }

        // Method to insert balls in five minute trail
        static Stack<int> fillFiveMinute(Queue<int> oneMinuteQueue, Stack<int> fiveMinuteTrail, int numberOfBalls)
        {

            // loop till no balls left
            while (numberOfBalls>0)
                {
                        fiveMinuteTrail.Push(oneMinuteQueue.Dequeue());
                        numberOfBalls--;
                }

            return fiveMinuteTrail;
        }

        // Method to insert balls in hour trail
        static Stack<int> fillHour(Stack<int> fiveMinuteTrail, Stack<int> hourTrail)
        {

            // loop till no balls left
            while (fiveMinuteTrail.Count>0)
            {
                hourTrail.Push(fiveMinuteTrail.Pop());
            }

            return hourTrail;
        }

        // Method to insert balls in base queue
        static Queue<int> fillbaseQueue(Stack<int> hourTrail, Queue<int> baseQueue)
        {

            // loop till no balls left
            while (hourTrail.Count>0)
            {
                baseQueue.Enqueue(hourTrail.Pop());
            }
            return baseQueue;

        }

        // Method to insert balls in base queue
        static Queue<int> emptyBaseQueue(Queue<int> baseQueue)
        {
            // loop till no balls left
            while (baseQueue.Count > 0)
            {
                baseQueue.Dequeue();
            }

            return baseQueue;
        }

        // to print stack
        static void printStack(Stack<int> a) 
        {
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
        }

        // to print queue
        static void printQueue(Queue<int> a)
        {
            foreach (var item in a)
            {
                Console.WriteLine(item);
            }
        }

        static void printAll(Stack<int> hourTrail,Queue<int> oneMinuteQueue,Queue<int> baseQueue, Stack<int> fiveMinuteTrail, Stack<int> oneMinuteTrail)
        {
            //Console.WriteLine("one minute stack");
            //printStack(oneMinuteTrail);
            //Console.WriteLine("one minute Queue");
            //printQueue(oneMinuteQueue);
            //Console.WriteLine("five minute stack");
            //printStack(fiveMinuteTrail);
            //Console.WriteLine("hour stack");
            //printStack(hourTrail);
            //Console.WriteLine("Base Queue");
            //printQueue(baseQueue);
        }

        // convert every trail,queue to array for compare
        static int[] convertToArray(Stack<int> oneMinuteTrail, Queue<int> oneMinuteQueue,Stack<int> fiveMinuteTrail,Stack<int> hourTrail)
        {
            // initialising to array
            int[] arrayOMT = oneMinuteTrail.ToArray();
            int[] arrayOMQ = oneMinuteQueue.ToArray();
            int[] arrayFMT = fiveMinuteTrail.ToArray();
            int[] arrayHT = hourTrail.ToArray();

            // merging to one array
            int[] mergedArray = new int[arrayHT.Length + arrayFMT.Length + arrayOMT.Length + arrayOMQ.Length];
            arrayOMT.CopyTo(mergedArray, 0);
            arrayOMQ.CopyTo(mergedArray, arrayOMT.Length);
            arrayFMT.CopyTo(mergedArray, arrayOMT.Length + arrayOMQ.Length);
            arrayHT.CopyTo(mergedArray, arrayFMT.Length + arrayOMT.Length + arrayOMQ.Length);

            return mergedArray;
        }

        /* Here starts the Main program */
        static void Main(string[] args)
        {
            // Declarations
            Queue<int> baseQueue = new Queue<int>();
            Stack<int> oneMinuteTrail = new Stack<int>();
            Queue<int> oneMinuteQueue = new Queue<int>();
            Stack<int> fiveMinuteTrail = new Stack<int>();
            Stack<int> hourTrail = new Stack<int>();
            int twelveHourCounter = 0;
            bool isFirstArrayCreated = false;
            int[] arrayToCompare = null;
            int[] arrayFirst = null;

            int[] arrayTotalBall = new int[500];

            // input value from user 
            for (int i = 0; i < arrayTotalBall.Length; i++)
            {
                try
                {
                    // get value from user
                    int value = Convert.ToInt32(Console.ReadLine());
                    
                    // limiting value to > 31
                    while(value<31)
                    {
                        Console.WriteLine("Value should be greater than 30");
                        value = Convert.ToInt32(Console.ReadLine());
                    }
                   
                    //  insert in array
                    arrayTotalBall[i] = value;

                    if(value==0)
                    {
                        break;
                    }
                }


                catch (Exception ex)
                {
                    // print message
                    Console.WriteLine(ex.Message);
                }
            }


            /* starts Main loop to filling and flushing queues and stacks */
            for (int j = 0; j < arrayTotalBall.Length; j++)
            {
                // to fill base queue
                for (int i = 1; i <= arrayTotalBall[j]; i++)
                {
                    baseQueue.Enqueue(i);
                }

                // loop till no ball is left in array
                while (arrayTotalBall[j]>0)
                {
                    // check ball count
                    if (fiveMinuteTrail.Count < 11)
                    {

                        int fiveBallCount = fiveMinuteTrail.Count;
                        int ballsToTransferFromQueue = 11 - fiveBallCount;

                        // ball count null
                        if (oneMinuteTrail.Count == 0)
                        {
                            oneMinuteTrail = fillOneMinute(baseQueue, oneMinuteTrail, 5);
                            continue;
                        }

                        // ball count null
                        if (oneMinuteQueue.Count == 0)
                        {
                            oneMinuteQueue = fillOneMinuteQueue(oneMinuteTrail, oneMinuteQueue, 5);
                            continue;
                        }

                        // ball count less than 5
                        if (ballsToTransferFromQueue < 5)
                        {
                            fiveMinuteTrail = fillFiveMinute(oneMinuteQueue, fiveMinuteTrail, ballsToTransferFromQueue);
                        }
                            // ball count greater than 5
                        else
                        {
                            fiveMinuteTrail = fillFiveMinute(oneMinuteQueue, fiveMinuteTrail, 5);
                        }


                    }
                        // five minute couunt is 10 and hour count is 0
                    else if (fiveMinuteTrail.Count == 11 && hourTrail.Count == 0)
                    {
                        //flush elements to next queue

                        hourTrail = fillHour(fiveMinuteTrail, hourTrail);

                        // check count null
                        if (oneMinuteQueue.Count > 0)
                        {
                            fiveMinuteTrail = fillFiveMinute(oneMinuteQueue, fiveMinuteTrail, oneMinuteQueue.Count);
                        }


                    }
                    // hour couunt is 11 and five minute count is 11
                    else if (hourTrail.Count == 11 && fiveMinuteTrail.Count == 11)
                    {
                        // check if comparable array is created
                        if (!isFirstArrayCreated)
                        {
                            // convert to array
                            arrayFirst = convertToArray(oneMinuteTrail, oneMinuteQueue, fiveMinuteTrail, hourTrail);
                            isFirstArrayCreated = true;
                        }

                            // create comparable array
                        else
                        {
                            arrayToCompare = convertToArray(oneMinuteTrail, oneMinuteQueue, fiveMinuteTrail, hourTrail);
                            
                            // compare ball cycle array
                            if (arrayFirst.SequenceEqual(arrayToCompare))
                            {
                                // print number of ball cycles and days
                                Console.WriteLine(arrayTotalBall[j]+" Balls cycle after " + twelveHourCounter / 2 + " days " );
                                // empty basequeue
                                baseQueue = emptyBaseQueue(baseQueue);
                                isFirstArrayCreated = false;
                               
                                // break from loop
                                break;
                            }
                        }

                        // fill basequeue
                        baseQueue = fillbaseQueue(hourTrail, baseQueue);
                        
                        // completes 12 hour cycle count half day
                        twelveHourCounter++;

                        // fill the empty hour trail
                        hourTrail = fillHour(fiveMinuteTrail, hourTrail);

                        // fill if queue is empty
                        if (oneMinuteQueue.Count > 0)
                        {
                            fiveMinuteTrail = fillFiveMinute(oneMinuteQueue, fiveMinuteTrail, oneMinuteQueue.Count);
                        }

                    }

                    else
                    {
                        Console.WriteLine("DID NOTHING");
                    }
                }
                
            }
            // read from console
            Console.ReadLine();
        }
    }
}
