using SpecSeminar3;

Console.WriteLine("Hello, World!");


int n = 3;
int[] timeRequirement = { 19, 23, 19 };
int[,] moveTime = {
                { 0, 20, 7, 20 },
                { 20, 0, 15, 0 },
                { 7, 15, 0, 15 },
                { 20, 0, 15, 0 }
        };


/*int n = 3;
int[] timeRequirement = { 26, 29, 28 };
int[,] moveTime = {
                {0, 15, 15, 5 },
                {15, 0, 0, 15},
                {15, 0, 0, 15},
                {5, 15, 15, 0}
        };*/

Delivery task = new Delivery(n, timeRequirement, moveTime);
SolverBase solver = new SolverBase(task);
solver.calculate();