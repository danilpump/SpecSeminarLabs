using SpecSeminar5;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Runtime.ConstrainedExecution;

int k = 36;
string fPath = "C:\\Users\\whati\\source\\repos\\SpecSeminar1\\SpecSeminar5\\resources\\8246.txt";

List<Point> listPoints = getDataFromFile(fPath, out k);
List <Point> getDataFromFile(string filePath, out int _k) 
{
    using (StreamReader sr = File.OpenText(filePath))
    {
        string s = sr.ReadLine();
        _k = Convert.ToInt32(s);

        List<Point> _listPoints = new List<Point>();
        while ((s = sr.ReadLine()) != null)
        {
            string[] str = s.Split(" ");
            _listPoints.Add(new Point(Convert.ToInt32(str[0]),
            (float)Convert.ToDouble(str[1]),
            (float)Convert.ToDouble(str[2])));
        }
        return _listPoints;
    }
}

#region baseVersion
List<PointGroup> iterations = new List<PointGroup>();
PointGroup pointGrp = new PointGroup(k);

List<Point> totalPath = new List<Point>();
List<Point> tempPath = new List<Point>();
PointGroup _gr = new PointGroup(k);

_gr = Reduce(pointGrp, listPoints, k/16);
iterations.Add(new PointGroup(_gr));

while (true)
{
    _gr = Reduce(_gr, clustersToPoints(_gr.Clusters), _gr.targetClCount / 16);
    if (_gr is null) break;
    iterations.Add(new PointGroup(_gr));
} // здесь редуцируем исходную задачу и записываем каждый шаг в список iterations

iterations.Reverse();

foreach (var el in iterations)
{
    //здесь сортируем кластеры в порядке, указанном в пути (только для последнего)
    _gr = calculateOrder(el, tempPath);

    // Находим ближайшие точки в соседних кластерах на маршруте и указываем их как стартовые и конечные
    nearestPoints(_gr);

    // найти маршрут внутри каждого кластера (с указанными sp и ep)
    // полученный маршрут применим к следующей итерации
    tempPath = findPathInClusters(_gr);
}

float result = calculateTotalDistance(tempPath);

Console.WriteLine(result);
/*foreach (var el in tempPath)
    Console.Write(el.ToString() + " ");*/
Console.WriteLine();
#endregion

#region myVersion1
iterations = new List<PointGroup>();
pointGrp = new PointGroup(k);

totalPath = new List<Point>();
tempPath = new List<Point>();
_gr = new PointGroup(k);

_gr = ReduceWR(pointGrp, listPoints, k / 16);
iterations.Add(new PointGroup(_gr));

while (true)
{
    _gr = ReduceWR(_gr, clustersToPoints(_gr.Clusters), _gr.targetClCount / 16);
    if (_gr is null) break;
    iterations.Add(new PointGroup(_gr));
} // здесь редуцируем исходную задачу и записываем каждый шаг в список iterations

iterations.Reverse();

foreach (var el in iterations)
{
    //здесь сортируем кластеры в порядке, указанном в пути (только для последнего)
    _gr = calculateOrder(el, tempPath);

    // Находим ближайшие точки в соседних кластерах на маршруте и указываем их как стартовые и конечные
    nearestPoints(_gr);

    // найти маршрут внутри каждого кластера (с указанными sp и ep)
    // полученный маршрут применим к следующей итерации
    tempPath = findPathInClusters(_gr);
}

result = calculateTotalDistance(tempPath);

Console.WriteLine(result);
/*foreach (var el in tempPath)
    Console.Write(el.ToString() + " ");*/
Console.WriteLine();
#endregion

/*#region myVersion2

iterations = new List<PointGroup>();
pointGrp = new PointGroup(k);
totalPath = new List<Point>();
tempPath = new List<Point>();
_gr = new PointGroup(k);

_gr = Reduce2(pointGrp, listPoints);
iterations.Add(new PointGroup(_gr));

while (true)
{
    _gr = Reduce2(_gr, clustersToPoints(_gr.Clusters));
    if (_gr is null) break;
    iterations.Add(new PointGroup(_gr));
} // здесь редуцируем исходную задачу и записываем каждый шаг в список iterations

iterations.Reverse();

findPath(_gr, ref tempPath);

result = calculateTotalDistance(tempPath);

Console.WriteLine(result);
#endregion*/
/*foreach (var el in tempPath)
    Console.Write(el.ToString() + " ");*/

Console.ReadLine();
//----------------------------------------------------------------------

void findPath(PointGroup pg, ref List<Point> outPath) 
{
    foreach (var el in iterations)
    {
        //здесь сортируем кластеры в порядке, указанном в пути (только для последнего)
        pg = calculateOrder(el, outPath);
        // Находим ближайшие точки в соседних кластерах на маршруте и указываем их как стартовые и конечные
        nearestPoints(pg);
        // найти маршрут внутри каждого кластера (с указанными sp и ep) полученный маршрут применим к следующей итерации
        outPath = findPathInClusters(pg);
    }
}

float calculateTotalDistance(List<Point> currentPath) 
{
    float _result = 0;
    for (int i = 0; i < currentPath.Count; i++)
    {
        Point p1 = currentPath.ElementAt(i);
        Point p2;
        if (i == currentPath.Count - 1)
            p2 = currentPath.ElementAt(0);
        else
            p2 = currentPath.ElementAt(i + 1);
        _result += calculateDistance(p1, p2);
    }
    return _result;
}

PointGroup calculateOrder(PointGroup tmp, List<Point> path) 
{
    PointGroup _pg = new(tmp);
    _pg.Clusters.Clear();
    foreach (var el in path)
        _pg.Clusters.TryAdd(el.index, tmp.Clusters.GetValueOrDefault(el.index));
    return _pg;
} //здесь сортируем кластеры в порядке, указанном в пути (только для последнего)

void nearestPoints(PointGroup group) 
{
    for (int i = 0; i < group.Clusters.Count; i++)
    {
        Cluster tempL1 = group.Clusters.ElementAt(i).Value;
        Cluster tempL2;
        if (i == group.Clusters.Count - 1)
            tempL2 = group.Clusters.ElementAt(0).Value;
        else
            tempL2 = group.Clusters.ElementAt(i + 1).Value;

        Point pointS = null, pointE = null;
        float min = float.MaxValue;
        foreach (var endP in tempL1.list)
        {
            Point startP = minDistancedPoint(tempL2.list, endP);
            if ((tempL2.endPoint == startP) && tempL2.list.Count != 1)
            {
                Cluster cl = new(tempL2);
                cl.list.Remove(startP);
                startP = minDistancedPoint(cl.list, endP);
            }
            float locMin = calculateDistance(startP, endP);
            if (min > locMin &&
                ((tempL2.endPoint != startP) || tempL2.list.Count == 1) &&
                ((tempL1.startPoint != endP) || tempL1.list.Count == 1))
            {
                min = locMin;
                pointS = startP;
                pointE = endP;
            }
        }

        tempL1.endPoint = pointE;
        tempL2.startPoint = pointS;
    }
}// Находим ближайшие точки в соседних кластерах на маршруте и указываем их как стартовые и конечные
List<Point> findPathInClusters(PointGroup group) 
{
        List<Point> localPath = new List<Point>();
    for (int i = 0; i < group.Clusters.Count; i++)
    {
        Cluster curCluster = group.Clusters.ElementAt(i).Value;
        localPath.AddRange(curCluster.calculatePathGreedy());
    }
    return localPath;
} // найти маршрут внутри каждого кластера (с указанными sp и ep)
// полученный маршрут применим к следующей итерации

List<Point> calculatePath2(PointGroup pg)
{
    List<Point> path = new List<Point>();
    List<Point> _list = clustersToPoints(pg.Clusters);
    var result = ShowAllCombinations(_list);
    float min = float.MaxValue;
    foreach (List<Point> lst in result)
    {
        float localMin = 0;
        for (int i = 0; i < lst.Count - 1; i++)
            localMin += calculateDistance(lst.ElementAt(i), lst.ElementAt(i + 1));
        if (localMin < min)
        {
            min = localMin;
            path = lst;
        }
    }
    return path;
}
List<List<Point>> ShowAllCombinations(List<Point> arr, List<List<Point>> list = null, List<Point> current = null)
{
    if (list == null) list = new List<List<Point>>();
    if (current == null) current = new List<Point>();
    if (arr.Count == 0) //если все элементы использованы, выводим на консоль получившуюся строку и возвращаемся
    {
        list.Add(current);
        return list;
    }
    for (int i = 0; i < arr.Count; i++) //в цикле для каждого элемента прибавляем его к итоговой строке, создаем новый список из оставшихся элементов, и вызываем эту же функцию рекурсивно с новыми параметрами.
    {
        List<Point> lst = new List<Point>(arr);
        lst.RemoveAt(i);
        var newlst = new List<Point>(current);
        newlst.Add(arr.ElementAt(i));
        ShowAllCombinations(lst, list, newlst);
    }
    return list;
}

PointGroup Reduce(PointGroup pg, List<Point> _lp, int _k)
{
    List<Point> lp = new(_lp);
    if (_k < 8 && pg.Clusters.Count < 8 && pg.targetClCount < 8)
    {
        tempPath = calculatePath2(pg);
        return null; // функция расчета пути для задачи
    }

    PointGroup pointGr = pg.Clusters is null? new PointGroup(_k) : new PointGroup(pg.Clusters.Count);
    Point[] points = maxDistancedPoints(lp); // для начала узнаем самые удаленные друг от друга точки
    pointGr.AddPoint(points[0].index, points[0]);
    lp.Remove(points[0]);
    pointGr.AddPoint(points[1].index, points[1]);
    lp.Remove(points[1]);

    while (pointGr.Clusters.Count < pg.Clusters.Count)
    {
        Point temp1 = maxDistancedPoint2(lp, pointGr.Clusters);
        pointGr.AddPoint(temp1.index, temp1);
        lp.Remove(temp1);
    }

    foreach (Point p in lp)
    {
        Cluster tempCl1 = minDistancedCluster(pointGr.Clusters, p);//вычисляем мин отд кластер    
        tempCl1.Add(p); // добавляем точку в мин отд кластер
    }

    return pointGr;
}

PointGroup ReduceWR(PointGroup pg, List<Point> _lp, int _k)
{
    List<Point> lp = new(_lp);
    if (_k < 8 && pg.Clusters.Count < 8 && pg.targetClCount < 8)
    {
        tempPath = calculatePath2(pg);
        return null; // функция расчета пути для задачи
    }

    PointGroup pointGr = pg.Clusters is null ? new PointGroup(_k) : new PointGroup(pg.Clusters.Count);
    Point[] points = maxDistancedPoints(lp); // для начала узнаем самые удаленные друг от друга точки
    pointGr.AddPointWR(points[0].index, points[0]);
    lp.Remove(points[0]);
    pointGr.AddPointWR(points[1].index, points[1]);
    lp.Remove(points[1]);

    while (pointGr.Clusters.Count < pg.Clusters.Count)
    {
        Point temp1 = maxDistancedPoint2(lp, pointGr.Clusters);
        pointGr.AddPointWR(temp1.index, temp1);
        lp.Remove(temp1);
    }

    foreach (Point p in lp)
    {
        Cluster tempCl1 = minDistancedCluster(pointGr.Clusters, p);//вычисляем мин отд кластер    
        tempCl1.AddWR(p); // добавляем точку в мин отд кластер
    }

    return pointGr;
}

PointGroup Reduce2(PointGroup pg, List<Point> _lp)
{
    List<Point> lp = new(_lp);
    
    if (pg.Clusters.Count < 8 && pg.targetClCount == 0 || lp.Count == 1)
    {
        tempPath = calculatePath2(pg);
        return null; // функция расчета пути для задачи
    }

    PointGroup pointGr = pg.Clusters.Count == 0 ? new PointGroup(pg.targetClCount/2) : new PointGroup(pg.Clusters.Count);
    Point[] points = null;

    if (lp.Count == 1)
    {
        pointGr.AddPoint(lp.First().index, lp.First());
        lp.Remove(lp.First());
    }
    else
    {
        points = minDistancedPoints(lp);
        pointGr.AddPoint(points[0].index, points[0]);
        lp.Remove(points[0]);
        Point _p = minDistancedPoint(lp, points[0]);
        pointGr.AddPoint(points[0].index, _p);
        lp.Remove(_p);
    }

    while (lp.Count != 0)
    {
        if (lp.Count == 1)
        { 
            pointGr.AddPoint(lp.First().index, lp.First());
            lp.Remove(lp.First());
            continue;
        }
        points = minDistancedPoints(lp); // для начала узнаем самые удаленные друг от друга точки
        pointGr.AddPoint(points[0].index, points[0]);
        lp.Remove(points[0]);
        Point _p = minDistancedPoint(lp, points[0]);
        pointGr.AddPoint(points[0].index, _p);
        lp.Remove(_p);
    }

    foreach (Point p in lp)
    {
        Cluster tempCl1 = minDistancedCluster(pointGr.Clusters, p);//вычисляем мин отд кластер    
        tempCl1.Add(p); // добавляем точку в мин отд кластер
    }

    return pointGr;
}

List<Point> clustersToPoints(Dictionary<int, Cluster> clPoints) 
{
    List<Point> list = new List<Point>();

    foreach (KeyValuePair<int, Cluster> cl in clPoints)
        list.Add(cl.Value.point);
    return list;
}
Point minDistancedPoint(List<Point> cl, Point point)
{
    if (cl == null) return null;
    float min = float.MaxValue;
    Point p2 = null;
    foreach (Point _p in cl)
        if (min > calculateDistance(point, _p))
        {
            min = calculateDistance(point, _p);
            p2 = _p;
        }
    return p2;
}
Cluster minDistancedCluster(Dictionary<int, Cluster> clPoints, Point point)
{
    if (clPoints == null) return null;
    float min = float.MaxValue;
    Cluster cl2 = null;
    foreach (KeyValuePair<int, Cluster> _p in clPoints)
        if (min > calculateDistance(point, _p.Value.point))
        {
            min = calculateDistance(point, _p.Value.point);
            cl2 = _p.Value;
        }
    return cl2;
}
Point maxDistancedPoint2(List<Point> cl, Dictionary<int, Cluster> clPoints)
{
    if (cl == null) return null;
    float max = 0;
    float maxLocal = 0;
    Point p2 = null;

    foreach (Point _p in cl)
    {
        maxLocal = 0;
        foreach (KeyValuePair<int, Cluster> _p1 in clPoints)
            maxLocal += calculateDistance(_p1.Value.point, _p);

        if (max < maxLocal)
        {
            max = maxLocal;
            p2 = _p;
        }
    }
    return p2;
}

Point minDistancedPoint2(List<Point> cl, Dictionary<int, Cluster> clPoints)
{
    if (cl == null) return null;
    float min = float.MaxValue;
    float maxLocal = 0;
    Point p2 = null;

    foreach (Point _p in cl)
    {
        maxLocal = 0;
        foreach (KeyValuePair<int, Cluster> _p1 in clPoints)
            maxLocal += calculateDistance(_p1.Value.point, _p);

        if (min > maxLocal)
        {
            min = maxLocal;
            p2 = _p;
        }
    }
    return p2;
}

Point[] maxDistancedPoints(List<Point> cl)
{
    if (cl == null) return null;
    float max = 0;
    Point[] points = new Point[2];
    foreach (Point _p2 in cl)
        foreach (Point _p1 in cl)
            if (max < calculateDistance(_p2, _p1))
            {
                max = calculateDistance(_p2, _p1);
                points[0] = _p1;
                points[1] = _p2;
            }
    return points;
}

Point[] minDistancedPoints(List<Point> cl)
{
    if (cl == null) return null;
    float min = float.MaxValue;
    Point[] points = new Point[2];
    foreach (Point _p2 in cl)
        foreach (Point _p1 in cl)
            if (min > calculateDistance(_p2, _p1) && _p1 != _p2)
            {
                min = calculateDistance(_p2, _p1);
                points[0] = _p1;
                points[1] = _p2;
            }
    return points;
}
float calculateDistance(Point p1, Point p2)
{
    return (float)Math.Sqrt(Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2));
}

void Expand(PointGroup pg)
{
    //todo
}
