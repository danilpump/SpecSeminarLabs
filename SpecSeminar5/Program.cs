using SpecSeminar5;
using System.Diagnostics.Metrics;

/*Point p = new(0, 0, 0);
Point p2 = new(1, 3, 3);

Console.WriteLine("Distance is: " + calculateDistance(p, p2));*/


int k = 38;
PointGroup pointGr = new PointGroup(38);
List<Point> lp = new List<Point>() {
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
    new Point(36, 12386.666700f, 43334.722200f)
};

Point[] points = maxDistancedPoints(lp/*pointGr.dict.First().Value.list*/); // для начала узнаем самые удаленные друг от друга точки
pointGr.AddPoint(points[0].index, points[0]);
pointGr.AddPoint(points[1].index, points[1]);

while (pointGr.Clusters.Count < pointGr.targetClCount)
{
    Point temp1 = null;
    
    temp1 = maxDistancedPoint2(lp, pointGr.Clusters);
    pointGr.AddPoint(temp1.index, temp1);
}

foreach (Point p in lp)
{
    Cluster tempCl1 = null;
    
    if (pointGr.Clusters.Any(el => el.Value.point.Equals(p))) // Исключаем добавления самой себя
        continue;
         
    tempCl1 = minDistancedCluster(pointGr.Clusters, p);//вычисляем мин отд кластер
    
    tempCl1.Add(p); // добавляем точку в мин отд кластер
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
    float min = 0;
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

void Reduce(PointGroup pg)
{
    //todo
    PointGroup pointGroup = new PointGroup(pg);
    int count = pointGroup.targetClCount;

    Point[] points = maxDistancedPoints(pointGroup.Clusters.First().Value.list); // для начала узнаем самые удаленные друг от друга точки
    count += 2;



    /*foreach (KeyValuePair<int, Cluster> cl in pointGroup.dict)
        foreach (Point p in cl.Value.list)
            maxDistancedPoint(cl.Value, p);*/

    while (count < pg.targetClCount)
    {
        foreach (KeyValuePair<int, Cluster> cl in pointGroup.Clusters)
            foreach (Point p in cl.Value.list)
                maxDistancedPoint2(cl.Value.list, p);
    }
}

void Expand(PointGroup pg)
{
    //todo
}