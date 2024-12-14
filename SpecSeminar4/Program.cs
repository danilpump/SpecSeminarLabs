using SpecSeminar4;
using System.IO;
using System.Numerics;


//FileStream fileStream = new FileStream("resources\\task_1_n10_m2_k2.txt", FileMode.Open);task_4_n40_m4_k3.txt task_5_n250_m10_k6.txt
string path = "C:\\Users\\whati\\source\\repos\\SpecSeminar1\\SpecSeminar4\\resources\\task_3_n90_m6_k4.txt";
List<Order> orders = new List<Order>();
KeyValuePair<int, int> mAndK = getDataFromFile(orders, path);
List<int> localBaseIterResults = new List<int>();
List<int> localOwnIterResults = new List<int>();

Solver baseSolver = new Solver();
int baseResult = baseSolver.solve(mAndK.Key, mAndK.Value, copyOrders(orders), SortingStrategy.MinTD);
localBaseIterResults.Add(baseResult);
for (int i = 0; i < mAndK.Value; i++)
{
    int randResult = baseSolver.solve(mAndK.Key, mAndK.Value, copyOrders(orders), SortingStrategy.RANDOM);
    localBaseIterResults.Add(randResult);
}

int baseIterRecord = localBaseIterResults.Min();

int ownResult = baseSolver.solve(mAndK.Key, mAndK.Value, copyOrders(orders), SortingStrategy.BiggestNextVertexes);

List<int> currentDelays = new List<int>();
for (int i = 0; i < mAndK.Value; i++)
{
    currentDelays.Add(0);
}


for (int i = 0; i < mAndK.Value; i++)
{
    currentDelays = baseSolver.solve(mAndK.Key, mAndK.Value, copyOrders(orders), SortingStrategy.DelayIteration, currentDelays);
    localOwnIterResults.Add(currentDelays.Sum());
}

int ownIterResult = localOwnIterResults.Min();

Console.WriteLine(baseResult);
Console.WriteLine(baseIterRecord);
Console.WriteLine(ownResult);
Console.WriteLine(ownIterResult);


Console.WriteLine("Базовая просрочка " + (double)baseResult / baseResult);
Console.WriteLine("Базовая итерационная просрочка " + (double)baseIterRecord / baseResult);

Console.WriteLine("Своя просрочка " + (double)ownResult / baseResult);
Console.WriteLine("Своя итерационная просрочка " + (double)ownIterResult / baseResult);


KeyValuePair<int, int> getDataFromFile(List<Order> orders, string path)
{
    using (StreamReader sr = File.OpenText(path))
    {
        string s = sr.ReadLine();
        int n = Convert.ToInt32(s.Substring(2));
        s = sr.ReadLine();
        int m = Convert.ToInt32(s.Substring(2));
        s = sr.ReadLine();
        int k = Convert.ToInt32(s.Substring(2));

        while ((s = sr.ReadLine()) != null)
        {
            sr.ReadLine();

            string[] stringV = sr.ReadLine().Split(" ");
            List<int> V = new List<int>();
            for (int j = 0; j < stringV.Length; j++)
                V.Add(Convert.ToInt32(stringV[j]));

            sr.ReadLine();
            string[] stringA = sr.ReadLine().Split(" ");
            Dictionary<int, List<int>> A = getintListMap(stringA);

            sr.ReadLine();
            string[] stringR = sr.ReadLine().Split(" ");
            Dictionary<int, int> r = new Dictionary<int, int>();

            sr.ReadLine();
            string[] stringT = sr.ReadLine().Split(" ");
            Dictionary<int, int> t = new Dictionary<int, int>();

            for (int j = 0; j < stringR.Length; j++)
            {
                Dictionary<int, bool> temp = new Dictionary<int, bool>();
                temp.Add(Convert.ToInt32(stringR[j]), false);
                //r.Add(temp);
                r.Add(V.ElementAt(j), Convert.ToInt32(stringR[j]));
                t.Add(V.ElementAt(j), Convert.ToInt32(stringT[j]));
            }
            int tRN, tD;

            sr.ReadLine();
            tRN = Convert.ToInt32(sr.ReadLine());
            sr.ReadLine();
            tD = Convert.ToInt32(sr.ReadLine());

            orders.Add(new Order(V, A, r, t, tRN, tD));
        }
        return new KeyValuePair<int, int>(m, k);
    }
    
}

static Dictionary<int, List<int>> getintListMap(string[] stringA)
{
    Dictionary<int, List<int>> A = new Dictionary<int, List<int>>();

    foreach (string str in stringA) {
        string[] currentWay = str.Replace("(", "").Replace(")", "").Split(",");
        List<int> endpoints = A.GetValueOrDefault(Convert.ToInt32(currentWay[0]));
        if (endpoints == null)
        {
            endpoints = new List<int>();
        }

        endpoints.Add(Convert.ToInt32(currentWay[1]));

        A.TryAdd(Convert.ToInt32(currentWay[0]), endpoints);
    }
    return A;
}

static List<Order> copyOrders(List<Order> orders)
{
    List<Order> ordersCopy = new List<Order>();

    foreach (Order order in orders) {
        List<int> copyVertexes = new List<int>(order.vertexes);

        Dictionary<int, List<int>> copyEdges = new Dictionary<int, List<int>>();


        foreach (KeyValuePair<int, List<int>> entry in order.edges)
        {
            List<int> endpoints = new List<int>(entry.Value);
            copyEdges.Add(entry.Key, endpoints);
        }

        ordersCopy.Add(new Order(copyVertexes, copyEdges, order.resources, order.duration, order.startTime, order.directiveTime));
    }

    return ordersCopy;
}