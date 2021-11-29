using System;
using System.Collections.Generic;
using System.Linq;

namespace Matrix
{
    static class MatrixOperation
    {
        //перетин
        public static T[,] Intersection<T>(T[,] a, T[,] b)
        {
            if (!IsEqualCount(a, b))
            {
                Console.WriteLine("error");
                return null;
            }

            var matrix = new T[a.GetLength(0), a.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                    if (Compare(a[i, j], b[i, j]))
                        matrix[i, j] = a[i, j];

            return matrix;
        }

        //об'єднання
        public static T[,] Collapse<T>(T[,] a, T[,] b)
        {
            if (!IsEqualCount(a, b))
            {
                Console.WriteLine("error");
                return null;
            }

            var matrix = new T[a.GetLength(0), a.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                    if (Compare(a[i, j], default(T)) == false)
                        matrix[i, j] = a[i, j];
                    else if (Compare(b[i, j], default(T)) == false)
                        matrix[i, j] = b[i, j];

            return matrix;
        }

        //різниця
        public static T[,] Difference<T>(T[,] a, T[,] b)
        {
            if (!IsEqualCount(a, b))
            {
                Console.WriteLine("error");
                return null;
            }

            var matrix = new T[a.GetLength(0), a.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                    if (Compare(a[i, j], b[i, j]) == false)
                        matrix[i, j] = a[i, j];

            return matrix;
        }

        //включення
        public static bool Inclusion<T>(T[,] a, T[,] b)
        {
            if (!IsEqualCount(a, b))
            {
                Console.WriteLine("error");
                return false;
            }

            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                    if (Compare(a[i, j], default(T)) == false &&
                        Compare(a[i, j], b[i, j]) == false)
                        return false;

            return true;
        }

        //симетрична різниця
        public static T[,] SimetricDifference<T>(T[,] a, T[,] b)
        {
            if (!IsEqualCount(a, b))
            {
                Console.WriteLine("error");
                return null;
            }

            return Difference(Collapse(a, b), Intersection(a, b)); ;
        }

        //обернена
        public static T[,] Invert<T>(T[,] a)
        {
            var matrix = new T[a.GetLength(1), a.GetLength(0)];
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                    matrix[j, i] = a[i, j];

            return matrix;
        }

        //доповнення
        public static T[,] Addition<T>(T[,] a, Func<T, T> Convert)
        {
            var matrix = new T[a.GetLength(1), a.GetLength(0)];
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                    matrix[i, j] = (T)Convert(a[i, j]);

            return matrix;
        }

        //двоїсте
        public static T[,] Dual<T>(T[,] a, Func<T, T> Convert)
        {
            var addition = Addition(a, Convert);
            var result = Invert(addition);
            return result;
        }

        public static bool Equal<T>(T[,] a, T[,] b)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                    if (Compare(a[i, j], b[i, j]) == false)
                        return false;
            return true;
        }

        public static int[,] Multiply(int[,] a, int[,] b)
        {
            var matrix = new int[a.GetLength(0), b.GetLength(1)];
            for (var i = 0; i < a.GetLength(0); i++)
                for (var j = 0; j < b.GetLength(1); j++)
                    for (var k = 0; k < a.GetLength(1); k++)
                        matrix[i, j] += a[i, k] * b[k, j];
            return matrix;
        }

        //Композиція
        public static int[,] Product(int[,] a, int[,] b)
        {
            if (a.GetLength(0) != b.GetLength(1))
            {
                Console.WriteLine("error");
                return null;
            }

            var matrix = new int[a.GetLength(0), b.GetLength(1)];
            for (var i = 0; i < a.GetLength(0); i++)
                for (var j = 0; j < b.GetLength(1); j++)
                    for (var k = 0; k < a.GetLength(1); k++)
                        matrix[i, j] += a[i, k] * b[k, j];

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (matrix[i, j] >= 1)
                        matrix[i, j] = 1;

            return matrix;
        }

        private static bool IsEqualCount<T>(T[,] a, T[,] b)
            => a.GetLength(0) == b.GetLength(0) &&
            a.GetLength(1) == b.GetLength(1);

        public static bool Compare<T>(T x, T y)
            => EqualityComparer<T>.Default.Equals(x, y);

        public static bool Reflexion<T>(T[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                if (Compare(a[i, i], default(T)))
                    return false;
            return true;
        }

        public static bool Irreflexion<T>(T[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                if (Compare(a[i, i], default(T)) == false)
                    return false;
            return true;
        }

        public static bool Symmetry<T>(T[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < i; j++)
                    if (Compare(a[i, j], a[j, i]) == false)
                        return false;
            return true;
        }

        public static bool Asymmetry<T>(T[,] a)
        {
            if (Irreflexion(a))
            {
                for (int i = 0; i < a.GetLength(0); i++)
                    for (int j = 0; j < a.GetLength(1); j++)
                        if (Compare(a[i, j], default(T)) == false
                            && Compare(a[j, i], default(T)) == false)
                            return false;
                return true;
            }
            return false;
        }

        public static bool Antisymmetry<T>(T[,] a)
        {
            if (Irreflexion(a) == false)
            {
                for (int i = 0; i < a.GetLength(0); i++)
                    for (int j = 0; j < a.GetLength(1); j++)
                        if (i != j && Compare(a[i, j], default(T)) == false
                            && Compare(a[j, i], default(T)) == false)
                            return false;
                return true;
            }
            return false;
        }

        public static bool Trans(int[,] a)
        {
            var m2 = Multiply(a, a);
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                    if (m2[i, j] > a[i, j])
                        return false;
            return true;
        }

        public static bool Antitrans(int[,] a)
            => Trans(Addition(a, (int v) => v == 0 ? 1 : 0));

        public static bool StrongTrans(int[,] a)
            => Trans(a) && Antitrans(a);

        public static bool Connectivity<T>(T[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
                for (int j = 0; j < a.GetLength(1); j++)
                    if (Compare(a[i, j], default(T)) && Compare(a[j, i], default(T)) && i != j)
                        return false;
            return true;
        }

        private struct C
        {
            public int x;
            public int y;
            public C(int x, int y) { this.x = x; this.y = y; }
            public override bool Equals(object obj)
                => base.Equals(obj);
        }

        private static bool NotCyclePath(List<C> path)
        {
            for (int i = 0; i < path.Count; i++)
                for (int j = 0; j < path.Count; j++)
                    if (path[i].Equals(path[j]) && i != j)
                        return false;
            return true;
        }
        private static bool AsyncPath(int[,] a, List<C> path)
        {
            var x = path[path.Count - 1].y;
            for (int i = 0; i < a.GetLength(0); i++)
                if (a[x, i] == 1)
                {
                    var p = new List<C>(path);
                    p.Add(new C(x, i));
                    if (NotCyclePath(p) == false || AsyncPath(a, p) == false)
                        return false;
                }
            return true;
        }

        public static bool Acyclivity(int[,] a)
        {
            var l = a.GetLength(0);
            for (int i = 0; i < l; i++)
                for (int j = 0; j < l; j++)
                    if (a[i, j] == 1)
                        if (AsyncPath(a, new List<C> { new C(i, j) }) == false)
                            return false;
            return true;
        }

        public static List<int> Max(int[,] a)
        {
            var list = new List<int>();
            var l = a.GetLength(0);
            for (int i = 0; i < l; i++)
            {
                var con = 0;
                for (int j = 0; j < l; j++)
                    if (a[i, j] == 1)
                        con++;
                if (con == l) list.Add(i + 1);
            }
            return list;
        }

        public static List<int> Min(int[,] a)
        {
            var list = new List<int>();
            var l = a.GetLength(0);
            for (int i = 0; i < l; i++)
            {
                var con = 0;
                for (int j = 0; j < l; j++)
                    if (a[j, i] == 1)
                        con++;
                if (con == l) list.Add(i + 1);
            }
            return list;
        }

        public static List<int> Major(int[,] a)
            => Min(Addition(a, (int v) => v == 0 ? 1 : 0));

        public static List<int> Minor(int[,] a)
            => Max(Addition(a, (int v) => v == 0 ? 1 : 0));

        public static bool UnstrictOrder(int[,] a)
            => Reflexion(a) && Antisymmetry(a) && Trans(a);

        public static bool StrictOrder(int[,] a)
            => Irreflexion(a) && Asymmetry(a) && Trans(a);

        public static int[,] Equivalent(int[,] a)
            => Intersection(a, Invert(a));

        public static int[,] StrictAdvantage(int[,] a)
            => Difference(a, Invert(a));

        public static int[,] Tolerance(int[,] a)
            => Addition(Difference(Collapse(a, Invert(a)), Intersection(a, Invert(a))), (int v) => v == 0 ? 1 : 0);

        public static int[,] Dominance(int[,] a)
            => Difference(a, Equivalent(a));

        public static void ChoiceFuncByDefault(int[,] a)
        {
            var strict = StrictAdvantage(a);
            ChoiceFuncByStrict(strict);
        }

        public static void ChoiceFuncByStrict(int[,] strict)
        {
            var nums = new int[strict.GetLength(0)];
            for (int i = 0; i < nums.Length; i++)
                nums[i] = i;

            for (int i = 1; i < strict.GetLength(0) + 1; i++)
            {
                var l = GetComboByN(nums, i);

                if (l.Count != 0)
                {
                    for (int j = 0; j < l.Count; j++)
                    {
                        var result = GetCMaxR(strict, l[j]);
                        Console.Write("C({ ");
                        for (int k = 0; k < l[j].Count; k++)
                            Console.Write($"x{l[j][k] + 1}, ");
                        Console.Write("}) = {");
                        for (int k = 0; k < result.Count; k++)
                            Console.Write($"x{result[k] + 1}, ");
                        Console.Write("}");
                        Console.WriteLine();
                    }
                }
            }
        }

        private static List<List<int>> GetCombWithTwo(List<int> l)
        {
            var result = new List<List<int>>();
            for (int i = 0; i < l.Count; i++)
                for (int j = i + 1; j < l.Count; j++)
                    result.Add(new List<int> { l[i], l[j] });
            return result;
        }

        private static List<int> GetCMaxR(int[,] a, List<int> l)
        {
            var lv = new Dictionary<int, int>();
            if (l.Count <= 2)
            {
                for (int i = l.First(); i < l.Last() + 1; i++)
                {
                    if (l.Any(v => v == i))
                    {
                        var sum = 0;
                        for (int j = l.First(); j < l.Last() + 1; j++)
                            if (l.Any(v => v == j))
                                if (a[i, j] == 1)
                                    sum++;
                        lv.Add(i, sum);
                    }
                }
                var max = 0;
                foreach (var item in lv)
                    if (item.Value > max)
                        max = item.Value;

                var result = new List<int>();
                foreach (var item in lv)
                    if (item.Value == max)
                        result.Add(item.Key);

                return result;
            }
            else
            {
                var comb = GetCombWithTwo(l);
                var result = new List<int>(l);

                for (int i = 0; i < comb.Count; i++)
                {
                    var curr = GetCMaxR(a, comb[i]);

                    for (int j = 0; j < comb[i].Count; j++)
                    {
                        if (curr.Any(v => v == comb[i][j]) == false)
                            result.Remove(comb[i][j]);
                    }
                }
                return result;
            }
        }

        private static bool CompareList(List<int> a, List<int> b)
        {
            if (a.Count != b.Count)
                return false;

            for (int i = 0; i < a.Count; i++)
                if (a[i] != b[i])
                    return false;
            return true;
        }

        private static bool HasEqualElem(List<int> a)
        {
            for (int i = 0; i < a.Count; i++)
                for (int j = i + 1; j < a.Count; j++)
                    if (a[i] == a[j])
                        return true;
            return false;
        }

        private static void Combinations(int[] list, int[] buffer, int order,
                                                                List<List<int>> container)
        {
            if (order < buffer.Length)
                for (int i = 0; i < list.Length; i++)
                {
                    buffer[order] = list[i];
                    Combinations(list, buffer, order + 1, container);
                }
            else
            {
                container.Add(new List<int>());
                for (int i = 0; i < buffer.Length; i++)
                    container.Last().Add(buffer[i]);
            }
        }

        private static List<List<int>> GetComboByN(int[] nums, int n)
        {
            var l = new List<List<int>>();

            Combinations(nums, new int[n], 0, l);
            l.ForEach(ml => ml.Sort());

            for (int i = 0; i < l.Count; i++)
                for (int j = i + 1; j < l.Count; j++)
                    if (j < l.Count && CompareList(l[i], l[j]))
                    {
                        l.RemoveAt(j);
                        j--;
                    }

            for (int i = 0; i < l.Count; i++)
                if (i < l.Count && HasEqualElem(l[i]))
                {
                    l.RemoveAt(i);
                    i--;
                }

            return l;
        }
    }
}