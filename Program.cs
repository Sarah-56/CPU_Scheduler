/************************************************************************************************
 * CPU scheduler FCFS
 * takes input arrival time and burst time for each process and number of processes
 * the output is processes' queue stored in list (queue) and linked list in case needed
 ************************************************************************************************/
using System;
using System.Collections.Generic;

namespace Scheduler
{
    struct process
    {
        public int arrivalTime;
        public int burstTime;
        public int completionTime;
        public int turnArroundTime;
        public int waitingTime;
        public int pID;
    };
    class Program
    {
        public static int processesNum;
        //Final output linkedlist
        public static LinkedList<process> output = new LinkedList<process>();
        public static LinkedListNode<process> index = null;
        //create an empty list of type process
        public static List<process> processes = new List<process>();
        //Final processes arrangement
        public static List<process> queue = new List<process>();

        //Time calculations and store the final processes' queue in a new list and linked list
        static void arrange(process task, int start, int total)
        {
            task.completionTime = total - start;
            task.turnArroundTime = task.completionTime - task.arrivalTime;
            task.waitingTime = task.turnArroundTime - task.burstTime; 
            queue.Add(task);
            
            if (output.Count == 0)
            {
                output.AddFirst(task);
                 index = output.Last;
            }
            else
            {
                output.AddLast(task);
                index = output.Last;        
            }
        }
        //Main function
        static void Main(string[] args)
        {
            Console.Write("Enter number of processes: ");
            processesNum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            process task = new process();

            for (int i = 1; i <= processesNum; i++)
            {
                task.pID = i;
                Console.Write("Enter arrival time of process " + i + ": ");
                task.arrivalTime = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter burst time of process " + i + ": ");
                task.burstTime = Convert.ToInt32(Console.ReadLine());

                processes.Add(task);
                
                Console.WriteLine();
            }

            //sort the list according to the arrival time
            processes.Sort((x, y) => x.arrivalTime - y.arrivalTime);

            int start = processes[0].arrivalTime;
            int total = 0;
            foreach (process i in processes)
            {
                total += i.burstTime;
                arrange(i, start, total);
            }
            processes.Clear();

            float averageWaitingTime = 0;
            float averageTurnArround = 0;

            //print sorted list elements
            Console.WriteLine("\t Arrival time \t Burst time \t completion time \t Turn arround \t waiting time");
            int p = 1;
            foreach (process k in queue)
            {
                Console.Write("process {0}: ", p);
                Console.WriteLine(k.arrivalTime + "\t\t" + k.burstTime + "\t\t" + k.completionTime + "\t\t\t" + k.turnArroundTime + "\t\t" + k.waitingTime);
                averageWaitingTime += k.waitingTime;
                averageTurnArround += k.turnArroundTime;
                p++;
            }
            Console.WriteLine();

            Console.Write("Sequence: ");
            foreach (process i in queue)
            {
                Console.Write(i.pID + " ");
            }
            Console.WriteLine("\n");

            averageWaitingTime /= processesNum;
            averageTurnArround /= processesNum;

            Console.WriteLine("Average waiting time = " + averageWaitingTime);
            Console.WriteLine("Average turn arround time = " + averageTurnArround);
            foreach(process k in output)
            {
                Console.WriteLine(k.pID + "\t\t" + k.arrivalTime + "\t\t" + k.burstTime + "\t\t" + k.completionTime + "\t\t\t" + k.turnArroundTime + "\t\t" + k.waitingTime);
            }
            Console.ReadLine();
        }
    }
}
