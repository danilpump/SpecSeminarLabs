using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpecSeminar3
{
    class SolverBase
    {
        Delivery task;

        public SolverBase(Delivery task) => this.task = task;

        public void calculateTimeAndViolations(List<int> localOrder, out int time, out int violations) 
        {
            time = 0;
            violations = 0;

            for (int i = 0; i < localOrder.Count; i++)
            {
                int deliveryTime = time + (i == 0 ? task.moveTime[0, localOrder.ElementAt(i)] : task.moveTime[localOrder.ElementAt(i - 1), localOrder.ElementAt(i)]);
                if (deliveryTime > task.timeRequirement[localOrder.ElementAt(i) - 1])
                    violations++;
                time = deliveryTime;
            }
        }
        public int computeUpperBound(List<int> order)
        {
            List<int> localOrder = new List<int>(order);
            List<int> remainingOrders = new List<int>();

            for (int i = 1; i <= task.n; i++)
                if (!order.Contains(i))
                    remainingOrders.Add(i);
            int time = 0;
            int violations = 0;

            calculateTimeAndViolations(localOrder, out time, out violations);

            List<int> noViolations;

            while (localOrder.Count != task.n)
            {
                noViolations = new List<int>();
                for (int i = 0; i < remainingOrders.Count; i++)
                {
                    int deliveryTime = time + (localOrder.Count == 0 ? task.moveTime[0,remainingOrders.ElementAt(i)] : task.moveTime[localOrder.Last(),remainingOrders.ElementAt(i)]);
                    if (deliveryTime <= task.timeRequirement[remainingOrders.ElementAt(i) - 1])                    
                        noViolations.Add(remainingOrders.ElementAt(i));                    
                }
                if (!(noViolations.Count == 0))
                {
                    int min = int.MaxValue;
                    int minIndex = 0;

                    for (int i = 0; i < noViolations.Count; i++)                    
                        if (task.moveTime[localOrder.Last(),noViolations.ElementAt(i)] < min)
                        {
                            min = task.moveTime[localOrder.Last(),noViolations.ElementAt(i)];
                            minIndex = i;
                        }
                    
                    int totalMin = noViolations.ElementAt(minIndex);
                    localOrder.Add(totalMin);
                    time += min;
                    remainingOrders.RemoveAll(a => a == totalMin);
                }
                else
                {
                    time += task.moveTime[localOrder.Last(),remainingOrders.First()];
                    localOrder.Add(remainingOrders.First());
                    remainingOrders.Remove(remainingOrders.First());
                    violations++;
                }
            }
            return violations;
        }

        public int computeLowerBound(List<int> route)
        {
            List<int> remainingOrders = new List<int>(); 
            for (int i = 1; i <= task.n; i++)
                if (!route.Contains(i))
                    remainingOrders.Add(i);             
            
            int time = 0;
            int violations = 0;

            calculateTimeAndViolations(route, out time, out violations);

            for (int i = 0; i < remainingOrders.Count; i++)
            {
                int deliveryTime = time + (route.Count == 0 ? task.moveTime[0, remainingOrders.ElementAt(i)] : task.moveTime[route.Last(), remainingOrders.ElementAt(i)]);
                if (deliveryTime > task.timeRequirement[remainingOrders.ElementAt(i) - 1])
                    violations++;
            }

            return violations;
        }


        public BranchNode branching(List<BranchNode> branchNodeList)
        {
            BranchNode bestNode = null;
            int minLowerBound = int.MaxValue;

            foreach (BranchNode node in branchNodeList)            
                if (node.lowerBound < minLowerBound)
                {
                    minLowerBound = node.lowerBound;
                    bestNode = node;
                }
            
            return bestNode;
        }

        public int calculate()
        {
            int nodeCount = 0;
            List<BranchNode> branchNodes = new List<BranchNode>();
            int bestUpperBound = int.MaxValue;
            BranchNode bestNode;
            List<int> bestOrder;

            for (int i = 1; i <= task.n; i++)
            {
                List<int> nodeOrder = new List<int>() { 1 };
                branchNodes.Add(new BranchNode(nodeOrder, computeLowerBound(nodeOrder), computeUpperBound(nodeOrder)));
                nodeCount++;
            }

            while (branchNodes.Count != 1 || branchNodes.First().partialRoute.Count != task.n || branchNodes.First().lowerBound != branchNodes.First().upperBound)
            {
                BranchNode nodeToRemove = branching(branchNodes);
                branchNodes.Remove(nodeToRemove);
                for (int i = 1; i <= task.n; i++)
                    if (!nodeToRemove.partialRoute.Contains(i))
                    {
                        List<int> newOrder = new List<int>(nodeToRemove.partialRoute) { i };
                        BranchNode newNode = new BranchNode(newOrder, computeLowerBound(newOrder), computeUpperBound(newOrder));

                        branchNodes.Add(newNode);
                        nodeCount++;
                    }

                bestUpperBound = int.MaxValue;
                bestNode = null;

                foreach (BranchNode branchNode in branchNodes)
                    if (branchNode.upperBound <= bestUpperBound)
                    {
                        bestUpperBound = branchNode.upperBound;
                        bestNode = branchNode;
                    }
            }
            bestOrder = branchNodes.First().partialRoute;

            if (bestOrder != null)
            {
                Console.WriteLine("Оптимальный порядок: ");
                foreach (var el in bestOrder)
                    Console.Write(el + " ");
                Console.WriteLine("\nМинимум нарушений: " + bestUpperBound);
            }
            else
                Console.WriteLine("Нет решения.");

            return nodeCount;
        }
    }    
}
