using System;
using System.Collections.Generic;
using MA = Matrix.MatrixOperation;

namespace Matrix
{
    public static class ListHelpres
    {
        public static string Output(this List<int> l)
        {
            var str = "( ";
            for (int i = 0; i < l.Count; i++)
                str += l[i] + " ";
            str += ")";
            return str;
        }
    }

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
        static private void Lab8()
        {
            int[,] a = {
                { 1, 1, 1, 1},
                { 0, 1, 0, 0},
                { 1, 1, 1, 0},
                { 1, 0, 1, 1}
            };
            MA.ChoiceFunc(a);
        }
        static private void Lab7()
        {
            var o = new MatrixOutput();

            int[,] a = {
                { 1, 0, 1, 1, 0},
                { 0, 1, 1, 1, 0},
                { 1, 1, 0, 1, 1},
                { 1, 1, 1, 1, 1},
                { 0, 0, 1, 1, 0}
            };

            o.Output(a, "Matrix A:");

            Console.WriteLine($"Max is {MA.Max(a).Output()}");
            Console.WriteLine($"Min is {MA.Min(a).Output()}");
            Console.WriteLine($"Major is {MA.Major(a).Output()}");
            Console.WriteLine($"Minor is {MA.Minor(a).Output()}");
            o.Output(MA.Equivalent(a), $"Equivalent is:");
            o.Output(MA.StrictAdvantage(a), $"Strict Advantage is:");
            o.Output(MA.Tolerance(a), $"Tolerance is:");

        }
        static private void Lab6()
        {
            var o = new MatrixOutput();

            int[,] a = {
                { 1, 0, 1, 1, 0},
                { 0, 1, 1, 1, 0},
                { 1, 1, 0, 1, 1},
                { 1, 1, 1, 1, 1},
                { 0, 0, 1, 1, 0}
            };

            o.Output(a, "Matrix A:");

            Console.WriteLine($"Reflexion is {MA.Reflexion(a)}");
            Console.WriteLine($"Irreflexion is {MA.Irreflexion(a)}");
            Console.WriteLine($"Symmetry is {MA.Symmetry(a)}");
            Console.WriteLine($"Asymmetry is {MA.Asymmetry(a)}");
            Console.WriteLine($"Antisymmetry is {MA.Antisymmetry(a)}");
            Console.WriteLine($"Trans is {MA.Trans(a)}");
            Console.WriteLine($"Antitrans is {MA.Antitrans(a)}");
            Console.WriteLine($"StrongTrans is {MA.StrongTrans(a)}");
            Console.WriteLine($"Connectivity is {MA.Connectivity(a)}");
            Console.WriteLine($"Acyclivity is {MA.Acyclivity(a)}");

        }
        static private void Lab4()
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
        static void Main(string[] args)
        {
            Lab8();
        }
    }
}