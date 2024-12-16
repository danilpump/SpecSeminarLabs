using SpecSeminar5;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

int k = 36;
/*List<Point> listPoints = new List<Point>() {
    new Point(1, 11003.611100f, 42102.500000f),
    new Point(2, 11108.611100f, 42373.888900f),
    new Point(3, 11133.333300f, 42885.833300f),
    new Point(4, 11155.833300f, 42712.500000f),
    new Point(5, 11183.333300f, 42933.333300f),
    new Point(6, 11297.500000f, 42853.333300f),
    new Point(7, 11310.277800f, 42929.444400f),
    new Point(8, 11416.666700f, 42983.333300f),
    new Point(9, 11423.888900f, 43000.277800f),
    new Point(10, 11438.333300f, 42057.222200f),
    new Point(11, 11461.111100f, 43252.777800f),
    new Point(12, 11485.555600f, 43187.222200f),
    new Point(13, 11503.055600f, 42855.277800f),
    new Point(14, 11511.388900f, 42106.388900f),
    new Point(15, 11522.222200f, 42841.944400f),
    new Point(16, 11569.444400f, 43136.666700f),
    new Point(17, 11583.333300f, 43150.000000f),
    new Point(18, 11595.000000f, 43148.055600f),
    new Point(19, 11600.000000f, 43150.000000f),
    new Point(20, 11690.555600f, 42686.666700f),
    new Point(21, 11715.833300f, 41836.111100f),
    new Point(22, 11751.111100f, 42814.444400f),
    new Point(23, 11770.277800f, 42651.944400f),
    new Point(24, 11785.277800f, 42884.444400f),
    new Point(25, 11822.777800f, 42673.611100f),
    new Point(26, 11846.944400f, 42660.555600f),
    new Point(27, 11963.055600f, 43290.555600f),
    new Point(28, 11973.055600f, 43026.111100f),
    new Point(29, 12058.333300f, 42195.555600f),
    new Point(30, 12149.444400f, 42477.500000f),
    new Point(31, 12286.944400f, 43355.555600f),
    new Point(32, 12300.000000f, 42433.333300f),
    new Point(33, 12355.833300f, 43156.388900f),
    new Point(34, 12363.333300f, 43189.166700f),
    new Point(35, 12372.777800f, 42711.388900f),
    new Point(36, 12386.666700f, 43334.722200f),
    new Point(37, 12421.666700f, 42895.555600f),
    new Point(38, 12645.000000f, 42973.333300f)

};*/

List<Point> listPoints = new List<Point>() {
    new Point(1, 0, 0),
    new Point(2, 2, 3),
    new Point(3, 5, 1),
    new Point(4, 7, 4),
    new Point(5, 6, 8),
    new Point(6, 3, 9),
    new Point(7, 1, 7),
    new Point(8, 4, 5)
};

PointGroup pointGrp = new PointGroup(8);

/*List<Point> listPoints2 = new(listPoints);
Point[] points1 = maxDistancedPoints(listPoints2); // для начала узнаем самые удаленные друг от друга точки
pointGrp.AddPoint(points1[0].index, points1[0]);
listPoints2.Remove(points1[0]);
pointGrp.AddPoint(points1[1].index, points1[1]);
listPoints2.Remove(points1[1]);

while (pointGrp.Clusters.Count < pointGrp.targetClCount)
{
    Point temp1 = maxDistancedPoint2(listPoints2, pointGrp.Clusters);
    pointGrp.AddPoint(temp1.index, temp1);
    listPoints2.Remove(temp1);
}

foreach (Point p in listPoints2)
{
    Cluster tempCl1 = null;

    if (pointGrp.Clusters.Any(el => el.Value.point.Equals(p))) // Исключаем добавления самой себя
        continue;

    tempCl1 = minDistancedCluster(pointGrp.Clusters, p);//вычисляем мин отд кластер    
    tempCl1.Add(p); // добавляем точку в мин отд кластер
}*/

List<Point> path = new List<Point>();

PointGroup group1 = Reduce(pointGrp, listPoints, 4);
PointGroup group2 = Reduce(group1, clustersToPoints(group1.Clusters), group1.targetClCount / 2);
PointGroup group3 = Reduce(group2, clustersToPoints(group2.Clusters), group2.targetClCount / 2);
Console.WriteLine();
calculatePath2(group3);

/*List<Point> calculatePath(PointGroup pg) 
{
    List<Point> path = new List<Point>();

    foreach (KeyValuePair<int, Cluster> cl in pg.Clusters)
    {
        path.Add(cl.Value.point);
        foreach (KeyValuePair<int, Cluster> cl2 in pg.Clusters)
        {
            if (cl.Value.Equals(cl2.Value)) continue;
            path.Add(cl2.Value.point);
        }
    }

    return path;
}
List<List<int>> allPaths = new List<List<int>>();
List<int> resultPath = new List<int>();
void generateSequence(List<int> indexes, int start) 
{    
    if (indexes.Count == start - 1) { allPaths.Add(indexes); return; }
    else
        for (int i = start; i < indexes.Count; i++)
        {
            resultPath.Add(indexes.ElementAt(i));
            generateSequence(indexes, i + 1);
        }
}*/

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
    if (_k < 2)
    {
        path = calculatePath2(pg);
        throw new Exception(); // функция расчета пути для задачи
    }
    PointGroup pointGr = new PointGroup(_k);
    Point[] points = maxDistancedPoints(lp); // для начала узнаем самые удаленные друг от друга точки
    pointGr.AddPoint(points[0].index, points[0]);
    lp.Remove(points[0]);
    pointGr.AddPoint(points[1].index, points[1]);
    lp.Remove(points[1]);

    while (pointGr.Clusters.Count < _k)
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
List<Point> clustersToPoints(Dictionary<int, Cluster> clPoints) 
{
    List<Point> list = new List<Point>();

    foreach (KeyValuePair<int, Cluster> cl in clPoints)
        list.Add(cl.Value.point);
    return list;
}
Point maxDistancedPoint(Cluster cl, Point point)
{
    if (cl.list == null) return null;
    float max = 0;
    Point p2 = null;
    foreach (Point _p in cl.list)
        if (max < calculateDistance(point, _p))
        {
            max = calculateDistance(point, _p);
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
float calculateDistance(Point p1, Point p2)
{
    return (float)Math.Sqrt(Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2));
}

void Expand(PointGroup pg)
{
    //todo
}