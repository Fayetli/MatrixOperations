using System;
using System.Collections.Generic;

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

            var collapse = Collapse(a, b);
            var intersection = Intersection(a, b);
            var matrix = Difference(collapse, intersection);

            return matrix;
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
    }
}