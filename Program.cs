/************************************************************************************************
 * CPU scheduler FCFS
 * takes input arrival time and burst time
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
        //3lshan ahmed :(
        public static LinkedList<process> list = new LinkedList<process>();
        public static LinkedListNode<process> index = null;
        //create an empty list of type process
        public static List<process> processes = new List<process>();
        public static List<process> queue = new List<process>();

        //Time calculations and store the final processes' queue in a new list and linked list
        static void arrange(process task, int start, int total, int i)
        {
            task.completionTime = total - start;
            task.turnArroundTime = task.completionTime - task.arrivalTime;
            task.waitingTime = task.turnArroundTime - task.burstTime; 
            queue.Add(task);
            
            if (i == 0)
            {
                list.AddFirst(task);
                 index = list.Last;
            }
            else
            {
                if(i == 2)
                {
                    list.AddLast(task);
                }
                list.AddAfter(index, task);
                index = list.Last;
            }
        }
        //Main function
        static void Main(string[] args)
        {
            Console.Write("Enter number of processes: ");
            int processesNum = Convert.ToInt32(Console.ReadLine());
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
            int c = 0;
            foreach (process i in processes)
            {
                total += i.burstTime;
                arrange(i, start, total, c);
                c++;
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
            Console.ReadLine();
        }
    }
}
