using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab2.matrix
{
    class Program
    {
        public static Matrix matrixA;
        public static Matrix vectorB;
        public static Matrix matrixA1;
        public static Matrix vectorC1;
        public static Matrix vectorB1;
        public static Matrix matrixC2;
        public static Matrix matrixA2;
        public static Matrix matrixB2;
        public static Matrix vectorY;
        public static Matrix vectorY2;
        public static Matrix matrixY3;

        public static Matrix _C1PlusB1;
        public static Matrix _B2MinC2;
        public static Matrix _Y3Pow2;
        public static Matrix _YPlusY2;
        public static Matrix _ResPart1;
        public static Matrix _ResPart2;
        static void Main(string[] args)
        {
            Stopwatch timer = Stopwatch.StartNew();
            Console.Write("Enter n: ");
            var n = Convert.ToInt32(Console.ReadLine());

            matrixA = new Matrix(n, n);
            vectorB = new Matrix(n, 1); 
            matrixA1 = new Matrix(n, n);
            vectorC1 = new Matrix(n, 1);
            vectorB1 = new Matrix(n, 1);
            matrixC2 = new Matrix(n, n);
            matrixA2 = new Matrix(n, n);
            matrixB2 = new Matrix(n, n);
            matrixY3 = new Matrix(n, n);

            Thread threadA = new Thread(matrixA.Random);
            threadA.Start();

            Thread threadB = new Thread(vectorB.RandomVectorB);
            threadB.Start();

            Thread threadA1 = new Thread(matrixA1.Random);
            threadA1.Start();

            Thread threadC1 = new Thread(vectorC1.Random);
            threadC1.Start();

            Thread threadB1 = new Thread(vectorB1.Random);
            threadB1.Start();

            Thread threadC2 = new Thread(matrixC2.Random);
            threadC2.Start();

            Thread threadA2 = new Thread(matrixA2.Random);
            threadA2.Start();

            Thread threadB2 = new Thread(matrixB2.Random);
            threadB2.Start();

            threadB2.Join();
            threadC2.Join();
            Thread threadB2MinC2 = new Thread(B2MinC2);
            threadB2MinC2.Start();

            threadA.Join();
            threadB.Join();
            //vectorY = matrixA * vectorB;
            Thread threadY = new Thread(ABMulti);
            threadY.Start();

            threadC1.Join();
            threadB1.Join();
            Thread threadC1B1 = new Thread(C1PlusB1);
            threadC1B1.Start();

            threadC1B1.Join();
            threadA1.Join();
            //vectorY2 = matrixA1 * _C1PlusB1;
            Thread threadY2 = new Thread(Y2);
            threadY2.Start();

            threadA2.Join();
            threadB2MinC2.Join();
            //matrixY3 = matrixA2 * _B2MinC2;
            Thread threadY3 = new Thread(Y3);
            threadY3.Start();

            threadY3.Join();

            Thread threadY3Pow2 = new Thread(Y3Pow2);
            threadY3Pow2.Start();

            threadY.Join();
            threadY2.Join();
            Thread threadYPlusY2 = new Thread(YPlusY2);
            threadYPlusY2.Start();

            threadY3Pow2.Join();
            //_ResPart1 = _Y3Pow2 * vectorY2;
            Thread threadRes1 = new Thread(ResPart1);
            threadRes1.Start();

            threadYPlusY2.Join();
            //_ResPart2 = matrixY3 * _YPlusY2;
            Thread threadRes2 = new Thread(ResPart2);
            threadRes2.Start();

            threadRes1.Join();
            threadRes2.Join();

            var res = _ResPart1 + _ResPart2;


            matrixA.ShowMatrix("A");
            vectorB.ShowMatrix("B");
            matrixA1.ShowMatrix("A1");
            vectorC1.ShowMatrix("C1");
            vectorB1.ShowMatrix("B1");
            matrixC2.ShowMatrix("C2");
            matrixB2.ShowMatrix("B2");
            matrixA2.ShowMatrix("A2");
            vectorY.ShowMatrix("Y");
            vectorY2.ShowMatrix("Y2");
            matrixY3.ShowMatrix("Y3");

            res.ShowMatrix("RES");
            timer.Stop();
            Console.WriteLine();

            _B2MinC2.ShowMatrix("b2-c2");
            _C1PlusB1.ShowMatrix("c1+b1");
            _Y3Pow2.ShowMatrix("Y3^2");
            _YPlusY2.ShowMatrix("y+y2");
            
            Console.WriteLine(timer.Elapsed);
        }


        public static void ABMulti()
        {
            vectorY = matrixA * vectorB;
        }

        public static void C1PlusB1()
        {
           _C1PlusB1 = vectorC1 + vectorB1;
        }

        public static void Y2()
        {
            vectorY2 = matrixA1 * _C1PlusB1;
        }

        public static void B2MinC2()
        {
            _B2MinC2 = matrixB2 - matrixC2;
        }

        public static void Y3()
        {
            matrixY3 = matrixA2 * _B2MinC2;
        }

        public static void Y3Pow2()
        {
            _Y3Pow2 = matrixY3 * matrixY3;
        }

        public static void YPlusY2()
        {
            _YPlusY2 = vectorY + vectorY2;
        }

        public static void ResPart1()
        {
            _ResPart1 = _Y3Pow2 * vectorY2;
        }

        public static void ResPart2()
        {
            _ResPart2 = matrixY3 * _YPlusY2;
        }
    }
    public class Matrix
    {
        static Random rnd = new Random();
        private double[,] array;
        int row, column;

        public Matrix(int row, int colunm)
        {
            this.row = row;
            this.column = colunm;
            array = new double[row, column];
        }

        public int Row { get { return row; } }
        public int Column { get { return column; } }

        public void Random()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    array[i, j] = rnd.Next(10);
                }
            }
        }
        public void RandomMatrixC2()
        {
            for (int i = 1; i <= row; i++)
            {
                for (int j = 1; j <= column; j++)
                {
                    array[i - 1, j - 1] = 1.0 / ((double)i + 2 * j);
                }
            }
        }
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.row != m2.row || m1.column != m2.column)
            {
                throw new Exception("Сложение невозможно");
            }

            Matrix m = new Matrix(m1.row, m1.column);

            for (int i = 0; i < m1.row; i++)
            {
                for (int j = 0; j < m1.column; j++)
                {
                    m.array[i, j] = m1.array[i, j] + m2.array[i, j];
                }
            }

            return m;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.row != m2.row || m1.column != m2.column)
            {
                throw new Exception("Вычитание невозможно");
            }

            Matrix m = new Matrix(m1.row, m1.column);

            for (int i = 0; i < m1.row; i++)
            {
                for (int j = 0; j < m1.column; j++)
                {
                    m.array[i, j] = m1.array[i, j] - m2.array[i, j];
                }
            }

            return m;
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.column != m2.row)
            {
                throw new Exception("Умножение невозможно");
            }

            Matrix m = new Matrix(m1.row, m2.column);

            for (int i = 0; i < m1.row; i++)
            {
                for (int j = 0; j < m2.column; j++)
                {
                    double sum = 0;

                    for (int k = 0; k < m1.column; k++)
                    {
                        sum += m1.array[i, k] * m2.array[k, j];
                    }

                    m.array[i, j] = sum;
                }
            }

            return m;
        }
        public void ShowMatrix(string name)
        {
            Console.WriteLine(name);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Console.Write($"{array[i, j]:0.#0}" + "\t");
                }

                Console.WriteLine();
            }
        }

        private bool IsEven(int a)
        {
            return (a % 2) == 0;
        }

        public void RandomVectorB()
        {
            for (int i = 1; i <= row; i++)
            {
                array[i - 1, 0] = IsEven(i) ? 1 / (double)i : 1 / ((double)i * i + 2);
            }
        }
    }
}
