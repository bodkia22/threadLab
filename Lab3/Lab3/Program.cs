using System;

namespace Lab3
{
    class Program
    {
        public static Random rnd = new Random();
        static void Main(string[] args)
        {
            const int N = 4;
            int k = 0;
            int[,] matrixA = new int[N, N];

            int[,] matrixB = new int[N,N];

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    k++;
                    if (i == j)
                        matrixA[i, j] = N - i;
                    else
                        matrixA[i, j] = 0;
                }
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i <= j)
                        matrixB[i,j] = rnd.Next(1, 10);
                    else
                        matrixB[i, j]= 0;
                }
            }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(matrixA[i, j] + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(matrixB[i, j] + " ");
                }

                Console.WriteLine();
            }

            Console.WriteLine(k);
            Console.ReadLine();
        }
    }
}
