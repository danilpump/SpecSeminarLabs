using System;
using System.Numerics;
using static System.Console;
using static System.Runtime.InteropServices.JavaScript.JSType;


int L = 219;
int n = 1001;
string str = "105 42 42 79 51 51 105 42 42 72 126 95 92 83 83 130 33 33 69 69 120 35 35 112 39 39 77 50 50 70 53 53 93 47 47 87 48 48 81 49 49 74 74 108 108 130 130 89 116 94 44 44 101 43 43 127 88 125 34 34 67 146 78 51 51 87 47 47 75 53 53 134 31 31 120 37 37 112 95 97 44 44 72 106 99 44 44 111 40 40 116 116 80 49 49 89 46 46 78 50 50 132 32 32 134 134 73 51 51 78 52 52 108 108 107 107 114 39 39 84 49 49 70 53 53 105 40 40 96 43 43 126 33 33 95 45 45 90 90 95 44 44 106 41 41 129 32 32 78 127 81 81 132 32 32 128 33 33 94 91 71 52 52 117 37 37 118 36 36 90 47 47 132 33 33 126 33 33 68 68 126 34 34 123 35 35 104 42 42 130 130 118 36 36 68 109 118 35 35 90 111 89 120 138 138 124 124 101 96 112 39 39 128 32 32 129 65 116 37 37 107 101 105 105 121 37 37 138 78 134 32 32 132 32 32 127 33 33 74 54 54 138 28 28 116 38 38 74 52 52 138 138 92 102 100 42 42 136 136 92 104 113 38 38 76 52 52 81 49 49 79 52 52 114 114 135 135 128 34 34 105 43 43 97 43 43 79 79 69 54 54 72 109 110 40 40 79 50 50 116 116 71 141 77 77 114 39 39 78 78 118 36 36 123 36 36 66 66 77 50 50 135 30 30 80 49 49 87 49 49 90 90 78 51 51 73 52 52 138 30 30 82 82 89 47 47 126 68 116 116 94 94 87 97 76 53 53 83 48 48 104 42 42 115 38 38 86 48 48 114 114 111 40 40 131 32 32 116 80 80 51 51 134 82 108 42 42 127 34 34 132 32 32 121 36 36 80 80 104 41 41 103 42 42 106 87 121 121 84 48 48 130 79 125 34 34 82 123 103 42 42 117 117 82 82 126 126 101 108 115 37 37 116 37 37 110 110 79 104 96 45 45 121 121 118 94 124 35 35 125 35 35 120 36 36 71 105 121 36 36 80 50 50 75 51 51 110 39 39 117 117 136 136 102 44 44 73 73 118 118 70 125 135 77 134 63 84 49 49 138 69 72 54 54 115 37 37 136 29 29 71 130 128 32 32 126 67 90 46 46 82 49 49 107 107 119 35 35 133 32 32 85 85 93 46 46 93 93 130 130 93 47 47 134 134 71 52 52 96 99 112 38 38 128 33 33 73 53 53 94 44 44 87 105 126 89 90 48 48 98 42 42 130 32 32 98 98 74 54 54 75 52 52 136 72 65 65 85 48 48 117 117 78 49 49 124 35 35 77 77 81 49 49 125 125 136 31 31 114 38 38 125 33 33 127 33 33 131 33 33 76 51 51 67 56 56 117 117 90 48 48 94 46 46 132 132 76 76 127 33 33 83 48 48 72 133 75 52 52 98 43 43 88 127 66 66 67 54 54 110 38 38 103 96 123 123 121 35 35 68 129 131 31 31 133 66 107 42 42 79 49 49 72 52 52 70 110 111 40 40 112 39 39 76 53 53 67 152 84 49 49 125 125 79 109 74 74 116 38 38 126 126 134 30 30 72 53 53 71 54 54 121 35 35 94 45 45 89 47 47 131 67 97 45 45 126 126 119 89 108 41 41 120 35 35 75 142 85 48 48 131 131 69 53 53 102 43 43 116 116 137 80 125 33 33 92 46 46 119 119 123 78 89 48 48 107 41 41 110 110 93 45 45 93 47 47 121 35 35 88 109 97 97 127 84 71 54 54 120 35 35 72 72 116 38 38 124 78 79 52 52 96 96 104 104 126 34 34 127 34 34 129 34 34 80 51 51 128 32 32 133 31 31 111 111 85 48 48 70 121 134 32 32 103 43 43 91 46 46 138 138 131 32 32 81 51 51 83 83 65 65 76 76 111 40 40 104 43 43 89 89 96 91 125 125 112 39 39 127 33 33 138 138 119 36 36 86 120 119 119 130 72 123 34 34 134 63 67 57 57 137 31 31 81 49 49 135 31 31 89 48 48 77 77 130 130 108 40 40 79 52 52 104 104 122 122 104 42 42 113 38 38 106 106 70 127 68 56 56 117 37 37 106 41 41 112 38 38 123 36 36 115 37 37 78 52 52 97 44 44 107 82 110 110 80 80 67 137 105 41 41 100 43 43 137 29 29 94 94 68 54 54 126 33 33 119 92 114 37 37 90 90 97 46 46 78 51 51 101 42 42 119 36 36 71 71 125 33 33 83 83";

WriteLine("Enter the L:");
//Convert.ToInt32(ReadLine());
WriteLine("Enter the n:");
// Convert.ToInt32(ReadLine());
WriteLine("Enter the string:");
//ReadLine();

int[] results = input(str);
//Array.Sort(results);
//Array.Reverse(results);

double c = ((double)results.Sum() / L);
c = Math.Ceiling(c);

WriteLine("Нижняя оценка = " + c);
/*foreach (var el in results) Write(el + " ");
WriteLine(); WriteLine();*/

int x = 0;

WriteLine("greedy");
x = greedy(results);
WriteLine("Отклонение = " + (c - x));
WriteLine(x); WriteLine();

x = greedyMod(results, 60);
WriteLine("Отклонение = " + (c - x));
WriteLine(x); WriteLine();

x = Strategy2(results, 100);
WriteLine("Отклонение = " + (c - x));
WriteLine(x); WriteLine();

x = Strategy2mod(results, 100, 55);
WriteLine("Отклонение = " + (c - x));
WriteLine(x); WriteLine();


int greedy(int[] list) // перебирать остатки
{
    int _y = 1000;
    List<int[]> _list = new List<int[]>(); // итоговый список прутов и деталей из них
    List<int> _arr = new List<int>(); // текущий прут
    int y = 1;
    int _L = L;
    for (int i = 0; i < n; i++)
    {
        if (list[i] > _L) // если текущая деталь требует больше, чем остаток прута 
        {
            bool flag = true;
            // надо перебрать существующие остатки циклом
            for (int j = 0; j < _list.Count; j++) // проходимся по всем остаткам
                if (list[i] <= (L - _list[j].Sum())) // если текущая деталь убирается в один из остатков, то записываем ее к тому пруту
                {
                    var temp = new int[_list[j].Length + 1];
                    _list[j].CopyTo(temp, 0);
                    var temp2 = new int[1] { list[i] };
                    temp2.CopyTo(temp, _list[j].Length);
                    _list[j] = temp;
                    flag = false;
                    break;
                }
            if (flag) // если ни один не подошел, то берем новый прут и берем кусок оттуда
            {
                _L = L;
                y++;
                _list.Add(_arr.ToArray());
                _arr = new List<int>();
                _L -= list[i];
                _arr.Add(list[i]);
            }
        }
        else // иначе берем кусок из текущего прута
        {
            _L -= list[i];
            _arr.Add(list[i]);
        }
    }
    //y++;
    _list.Add(_arr.ToArray());


    #region вывод
    /*Write("L = " + L + ", n = " + n + ", list = ");
    foreach (var el in list) Write(el + " ");
    WriteLine();

    WriteLine("Рекорд у = " + y);

    int k = 0;
    foreach (var el in _list)
    {

        Write(k + "прут: ");
        k++;
        foreach (var _el in el) Write(_el + " ");
        WriteLine();
    }*/
    #endregion
    return y;
}


/*
int Strategy1<T>(IList<T> arr, string current = "", int percent = 60)
{
    string firstPart = "", secondPart = "";
    int r = (int)(arr.Count * ((double)percent / 100));
    for (int i = 0; i < r; i++)
    {
        firstPart += arr[i] + " ";
        secondPart = firstPart;

        for (int j = arr.Count-1; j > i; j--)
            secondPart += arr[j] + " ";
        int y = greedy(input(secondPart.Trim()));
        if (_y > y) _y = y;
    }
    return _y;
}*/

int greedyMod(int[] arr, int percent = 60)
{
    int _y = 1000;
    WriteLine("greedyMod");
    int r = (int)(arr.Length * ((double)percent / 100));

    int[] firstPart = arr.Take(r).ToArray();
    int[] secondPart = arr.Skip(r).ToArray();

    int[] res = new int[firstPart.Length + secondPart.Length];

    Array.Reverse(secondPart);

    firstPart.CopyTo(res, 0);
    secondPart.CopyTo(res, firstPart.Length);

    int y = greedy(res);
    if (_y > y) _y = y;

    return _y;
}

int Strategy2(int[] arr, int iterations = 100) 
{
    int _y = 1000;
    WriteLine("Strategy2");
    for (int i = 0; i < iterations; i++)
    {
        Random.Shared.Shuffle(arr);
        int y = greedy(arr);
        if (_y > y) _y = y;
    }
    return _y;
}

int Strategy2mod(int[] arr, int iterations = 100, int percent = 50)
{
    int _y = 1000;
    WriteLine("Strategy2mod");

    int r = (int)(arr.Length * ((double)percent / 100));

    int[] firstPart = arr.Take(r).ToArray();
    int[] secondPart = arr.Skip(r).ToArray();

    for (int i = 0; i < iterations; i++)
    {
        Random.Shared.Shuffle(firstPart);
        Random.Shared.Shuffle(secondPart);
        int[] res = new int[firstPart.Length + secondPart.Length];
        firstPart.CopyTo(res, 0);
        secondPart.CopyTo(res, firstPart.Length);
        int y = greedy(res);
        if (_y > y) _y = y;
    }
    return _y;
}

int ShowAllCombinations<T>(IList<T> arr, string current = "")
{
    int _y = 1000;
    if (arr.Count == 0)
    {
        return greedy(input(current.Trim()));
    }
    for (int i = 0; i < arr.Count; i++)
    {
        List<T> lst = new List<T>(arr);
        lst.RemoveAt(i);
        int y = ShowAllCombinations(lst, current + arr[i].ToString() + " ");
        if(_y > y) _y = y;
        
    }
    return _y;
}

int[] input(string str)
{
    return (from x in str.Split(' ')
            let y = x.Split('-')
            select y.Length == 1
              ? new[] { int.Parse(y[0]) }
              : Enumerable.Range(int.Parse(y[0]), int.Parse(y[1]) - int.Parse(y[0]) + 1)
               ).SelectMany(x => x).ToArray();
}