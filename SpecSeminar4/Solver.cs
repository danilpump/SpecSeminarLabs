using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SpecSeminar4
{
    public enum SortingStrategy
    {
        MinTD, RANDOM, BiggestNextVertexes, DelayIteration
    }
    class Solver
    {
        public Dictionary<int, ResourceState> resourcesState = new Dictionary<int, ResourceState>();
        public SortedSet<int> eventSet = new SortedSet<int>();
        public List<int> previousDelays = new List<int>();

        private void init(int m, int k, List<Order> orders)
        {
            for (int i = 0; i < k; i++)
            {
                eventSet.Add(orders.ElementAt(i).startTime);
            }

            for (int i = 1; i <= m; i++)
            {
                resourcesState[i] = new ResourceState(-1, null); // -1 - свободен, иначе - время освобождения
            }
        }


        public int solve(int m, int k, List<Order> orders, SortingStrategy strategy)
        {
            init(m, k, orders);

            while (!(eventSet.Count == 0))
            {
                List<Operation> front;

                while (!((front = frontalAlgorithm(eventSet.First(), orders)).Count == 0)) // колво операция которое можешь выполнить в определенный момент времени
                {

                    frontSort(front, orders, strategy, eventSet.First());
/*
                    foreach (Operation op in front)
                        Console.WriteLine(op.number + " " + op.orderId);*/


                    Operation operationToStart = front.First();
                    Order orderOfOperation = orders.ElementAt(operationToStart.orderId);

                    int resourceFreeTime = eventSet.First() + orderOfOperation.duration.GetValueOrDefault(operationToStart.number);
                    eventSet.Add(resourceFreeTime);

                    resourcesState[orderOfOperation.resources.GetValueOrDefault(operationToStart.number)] = new ResourceState(resourceFreeTime, operationToStart);

                    if (front.Count == 1)
                        break;
                }
                eventSet.Remove(eventSet.First());
            }

            int sumOfDelay = 0;

            for (int i = 0; i < orders.Count; i++)
            {
                int delay = orders.ElementAt(i).completionTime > orders.ElementAt(i).directiveTime ? orders.ElementAt(i).completionTime - orders.ElementAt(i).directiveTime : 0;
                Console.WriteLine("Заказ №" + (i + 1) + " Просрочка " + delay);
                sumOfDelay += delay;
            }

            return sumOfDelay;
        }

        public List<int> solve(int m, int k, List<Order> orders, SortingStrategy strategy, List<int> previousDelays)
        {
            init(m, k, orders);
            this.previousDelays = previousDelays;

            while (!(eventSet.Count == 0))
            {
                List<Operation> front;

                front = frontalAlgorithm(eventSet.First(), orders);

                while (front.Count != 0)
                {  

                    frontSort(front, orders, strategy, eventSet.First()); // в базовой версии приоритетно берется меньшее директивное время
                    Operation operationToStart = front.First();
                    Order orderOfOperation = orders.ElementAt(operationToStart.orderId);
                    if (resourcesState[orderOfOperation.resources.GetValueOrDefault(operationToStart.number)].freeTime != -1)
                    { front.Remove(front.First()); break; }

                    int resourceFreeTime = eventSet.First() + orderOfOperation.duration.GetValueOrDefault(operationToStart.number);
                    eventSet.Add(resourceFreeTime);

                    resourcesState[orderOfOperation.resources.GetValueOrDefault(operationToStart.number)] = new ResourceState(resourceFreeTime, operationToStart);

                }
                eventSet.Remove(eventSet.First());
            }

            List<int> delays = new();

            for (int i = 0; i < orders.Count; i++)
            {
                int delay = orders.ElementAt(i).completionTime > orders.ElementAt(i).directiveTime ? orders.ElementAt(i).completionTime - orders.ElementAt(i).directiveTime : 0;
                Console.WriteLine("Заказ №" + (i + 1) + " Просрочка " + delay);
                delays.Add(delay);
            }

            return delays;
        }

        private void frontSort(List<Operation> front, List<Order> orders, SortingStrategy strategy, int currentTime)
        {
            if (strategy == SortingStrategy.MinTD)
            {
                front.Sort((op1, op2) => orders.ElementAt(op1.orderId).directiveTime - orders.ElementAt(op2.orderId).directiveTime);
            }
            else if (strategy == SortingStrategy.RANDOM)
            {
                List<int> indexesList = generateIndexes(front.Count);
                List<Operation> newFront = new();

                for (int i = 0; i < front.Count; i++)
                {
                    newFront.Add(front.ElementAt(indexesList.ElementAt(i)));
                }

                for (int i = 0; i < front.Count; i++)
                {
                    front[i] = newFront.ElementAt(i);
                }
            }
            else if (strategy == SortingStrategy.BiggestNextVertexes)
            {
                double k1 = 0.5, k2 = 1 - k1;
                front.Sort((op1, op2) => {

                    double priority1 = k1 * orders.ElementAt(op1.orderId).directiveTime
                            + (-k2) * (!orders.ElementAt(op1.orderId).edges.ContainsKey(op1.number) ?
                            0 : orders.ElementAt(op1.orderId).edges.GetValueOrDefault(op1.number).Count);

                    double priority2 = k1 * orders.ElementAt(op2.orderId).directiveTime
                            + (-k2) * (!orders.ElementAt(op2.orderId).edges.ContainsKey(op2.number) ?
                            0 : orders.ElementAt(op2.orderId).edges.GetValueOrDefault(op2.number).Count);

                    return priority1.CompareTo(priority2);
                });
            }
            else if (strategy == SortingStrategy.DelayIteration)
            {
                Dictionary<int, int> remainingOperations = new();
                for (int i = 0; i < orders.Count; i++)
                {
                    remainingOperations.Add(i, orders.ElementAt(i).vertexes.Count);
                }

                double k1 = 0.8, k2 = 1 - k1;

                front.Sort((op1, op2) => {
                    List<int> edges1 = orders.ElementAt(op1.orderId).edges.GetValueOrDefault(op1.number);
                    List<int> edges2 = orders.ElementAt(op2.orderId).edges.GetValueOrDefault(op2.number);

                    double priority1 = -1 * k1 * previousDelays.ElementAt(op1.orderId) / remainingOperations.GetValueOrDefault(op1.orderId)
                            + -k2 * (edges1 == null ? 0 : edges1.Count);
                    double priority2 = -1 * k1 * previousDelays.ElementAt(op2.orderId) / remainingOperations.GetValueOrDefault(op2.orderId)
                            + -k2 * (edges2 == null ? 0 : edges2.Count);

                    return priority1.CompareTo(priority2);
                });
            }
        }

        public List<Operation> frontalAlgorithm(int time, List<Order> orders)
        {
            List<Operation> result = new List<Operation>();
            foreach (KeyValuePair<int, ResourceState> entry in resourcesState)
            {
                ResourceState entryResourceState = entry.Value;
                if (entryResourceState.freeTime == time)
                {
                    Operation entryOperation = entryResourceState.operation;
                    Order entryOrder = orders.ElementAt(entryOperation.orderId);

                    entryOrder.edges.Remove(entryOperation.number);
                    entryOrder.vertexes.Remove(entryOperation.number);
                    if (entryOrder.vertexes.Count == 0)
                    {
                        entryOrder.completionTime = time;
                    }
                    entryResourceState.freeTime = -1; // ресурс свободен
                    entryResourceState.operation = null;
                }
            }


            for (int i = 0; i < orders.Count; i++)
            {
                Order currentOrder = orders.ElementAt(i);
                bool readyToStart;

                for (int j = 0; j < currentOrder.vertexes.Count; j++)
                {
                    readyToStart = true;
                    foreach (KeyValuePair<int, List<int>> entry in currentOrder.edges)
                    {
                        if (entry.Value.Contains(currentOrder.vertexes.ElementAt(j)))
                        {
                            readyToStart = false;
                        }
                    }
                    if (resourcesState.GetValueOrDefault(currentOrder.resources.GetValueOrDefault(currentOrder.vertexes.ElementAt(j))).freeTime != -1) // если ресурс не занят, то берет его в заказ
                    {
                        readyToStart = false;
                    }

                    if (orders.ElementAt(i).startTime > time) // если стартовое время еще не пришло
                    {
                        readyToStart = false;
                    }

                    if (readyToStart)
                    {
                        result.Add(new Operation(currentOrder.vertexes.ElementAt(j), i));
                    }
                }
            }
            Console.WriteLine("Время " + time);
            Console.WriteLine("Результат " + result);

            return result;
        }

        private List<int> generateIndexes(int n)
        {
            List<int> genList = new List<int>();
            Random random = new Random();

            while (genList.Count != n)
            {
                int randNumber = random.Next(n);
                if (!genList.Contains(randNumber))
                {
                    genList.Add(randNumber);
                }
            }

            return genList;
        }
    }
}
