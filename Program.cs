using System;
using System.Collections.Generic;
using System.Linq;

namespace MatrixLayerRotation_HackerRank
{
    class Program // Some comments
    {
        static void Main(string[] args)
        {
            var matrix = new List<List<int>>();
            matrix.Add(new List<int> { 1, 2, 3, 4 });
            matrix.Add(new List<int> { 12, 1, 2, 5 });
            matrix.Add(new List<int> { 11, 4, 3, 6 });
            matrix.Add(new List<int> { 10, 9, 8, 7 });

            matrixRotation(matrix, 2);
        }

        static void matrixRotation(List<List<int>> matrix, int r)
        {
            MatrixLayerRotation(matrix, r);

            PrintMatrix(matrix);
        }

        static void MatrixLayerRotation(List<List<int>> matrix, int r)
        {
            var n = matrix.Count;
            if (n < 1)
                throw new Exception();

            var m = matrix[0].Count;

            for (var k = 0; k < GetNumOfRings(n, m); k++)
                RotateRing(k, matrix, r);
        }

        static void RotateRing(int k, List<List<int>> matrix, int r)
        {
            var n = matrix.Count;
            if (n < 1)
                throw new Exception();

            var m = matrix[0].Count;

            var xMax = GetNumOfItemsInRing(k, n, m);
            var arr = new int[xMax];
            for (var x = 0; x < xMax; x++)
            {
                var tuple = XtoIJ(x, k, n, m);
                arr[Move(x, xMax, r)] = matrix[tuple.Item1][tuple.Item2];
            }

            for (var x = 0; x < xMax; x++)
            {
                var tuple = XtoIJ(x, k, n, m);
                matrix[tuple.Item1][tuple.Item2] = arr[x];
            }
        }

        static int Move(int x, int xMax, int r) => (x + r) % xMax;

        static Tuple<int, int> XtoIJ(int x, int k, int n, int m)
        {
            if (k >= GetNumOfRings(n, m))
                return null;

            var boundaryX = new int[5]
            {
                0,
                n - 2 * k - 1,
                n + m - 4 * k - 2,
                2 * n + m - 6 * k - 3,
                2 * n + 2 * m - 8 * k - 4
            };

            // x - sequential number
            if (x >= 2 * n + 2 * m - 8 * k - 4)
                return null;

            var part = GetPart(k, x, n, m, boundaryX);

            Tuple<int, int> tuple = null;

            switch (part)
            {
                case 0:
                    tuple = new Tuple<int, int>(x + k, k);
                    break;

                case 1:
                    tuple = new Tuple<int, int>(n - k - 1, x - (n - 2 * k) + k + 1);
                    break;

                case 2:
                    tuple = new Tuple<int, int>(n - k - 1 - (x - (n - 2 * k) - (m - 2 * k) + 2), m - k - 1);
                    break;

                case 3:
                    tuple = new Tuple<int, int>(k, m - k - 1 - (x - 2 * (n - 2 * k) - (m - 2 * k) + 3));
                    break;
            }

            return tuple;
        }

        static int GetNumOfItemsInRing(int k, int n, int m) => 2 * n + 2 * m - 8 * k - 4;

        static int GetNumOfRings(int n, int m)
        {
            var z = Math.Min(n, m);
            return z / 2 + z % 2;
        }

        static int GetPart(int k, int x, int n, int m, int[] boundaryX)
        {
            for (var y = 0; y < boundaryX.Length; y++)
                if (boundaryX[y] <= x && x < boundaryX[y + 1])
                    return y;

            return -1;
        }

        static void PrintMatrix(List<List<int>> matrix)
        {
            var n = matrix.Count;
            if (n < 1)
                throw new Exception();

            var m = matrix[0].Count;

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                    Console.Write($"{matrix[i][j]} ");

                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}
