using System;
using MA = Matrix.MatrixOperation;

namespace Matrix
{
    class MatrixOutput
    {
        public void Output<T>(T[,] m, string label)
        {
            if (m == null)
                return;

            Console.WriteLine(label);
            for (int i = 0; i < m.GetLength(0); i++)
            {

                for (int j = 0; j < m.GetLength(0); j++)
                    Console.Write(m[i, j] + " ");
                Console.WriteLine();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var o = new MatrixOutput();

            int[,] a = {
                { 1, 1, 1, 1, 0},
                { 1, 1, 0, 0, 1},
                { 1, 0, 1, 0, 1},
                { 1, 0, 0, 1, 1},
                { 0, 0, 0, 0, 1}
            };
            int[,] b = {
                { 0, 1, 1, 1, 1},
                { 0, 1, 1, 1, 0},
                { 0, 0, 0, 1, 1},
                { 0, 0, 1, 1, 1},
                { 0, 1, 0, 0, 0}
            };

            o.Output(a, "Matrix A:");
            o.Output(b, "Matrix B:");

            Console.WriteLine($"Equal is {MA.Equal(a, b)}");
            Console.WriteLine($"Inclusion A of B is {MA.Inclusion(a, b)}");
            Console.WriteLine($"Inclusion B of A is {MA.Inclusion(b, a)}");

            var intersaction = MA.Intersection(a, b);
            o.Output(intersaction, "Intersection:");

            var collapse = MA.Collapse(a, b);
            o.Output(collapse, "Collapse:");

            var difference = MA.Difference(a, b);
            o.Output(difference, "Difference:");

            var simetricDifference = MA.SimetricDifference(a, b);
            o.Output(simetricDifference, "SimetricDifference:");

            var invertedA = MA.Invert(a);
            o.Output(invertedA, "Inverted A:");

            var product = MA.Product(a, b);
            o.Output(product, "Product:");

            var addition = MA.Addition(a, (int v) => v == 0 ? 1 : 0);
            o.Output(addition, "Addition:");

            var dual = MA.Dual(a, (int v) => v == 0 ? 1 : 0);
            o.Output(dual, "Dual:");
        }
    }
}
