using SpecSeminar5;

Point p = new(0, 0, 0);
Point p2 = new(3, 3, 1);

Console.WriteLine("Distance is: " + calculateDistance(p, p2));


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
float calculateDistance(Point p1, Point p2)
{
    return (float)Math.Sqrt(Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2));
}

void Reduce(PointGroup pg)
{
    //todo
    PointGroup pointGroup = new PointGroup(pg);
    int count = 0;
    while (count < pg.clustersCount / 4)
    {
        //todo
        /*foreach (KeyValuePair<int, Cluster> cl in pointGroup.dict)
            cl.Value.list.Contains*/
    }
}

void Expand(PointGroup pg)
{
    //todo
}