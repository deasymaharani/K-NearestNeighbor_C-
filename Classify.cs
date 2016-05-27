using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNN
{
    public class Classify
    {
        List<double>[] dataTrain = new List<double>[4000];
        List<double>[] dataTest = new List<double>[2000];
        int k, dimension, sizeTrain, sizeTest;
        public double akurasi;

        List<KeyValuePair<double, int>>[] neighbors = new List<KeyValuePair<double, int>>[4000];
        
        public Classify()
        { }
        
        public Classify(List<double>[] dataTrain, List<double>[] dataTest, int k, int dimension, int sizeTrain, int sizeTest)
        {
            this.dataTrain = dataTrain;
            this.dataTest = dataTest;
            this.k = k;
            this.dimension = dimension;
            this.sizeTrain = sizeTrain;
            this.sizeTest = sizeTest;
        }

        static int Compare1(KeyValuePair<double, int> a, KeyValuePair<double, int> b)
        {
            return a.Key.CompareTo(b.Key);
        }

        static int Compare2(KeyValuePair<double, int> a, KeyValuePair<double, int> b)
        {
            return a.Value.CompareTo(b.Value);
        }

        public double EuclideanDistance(int a, int b, int dimension)
        {
            double diff, sumSquare = 0, sumSquareRoot;
            double[] diffs = new double[10000];

            for (int j = 0; j < dimension; j++)
            {
                diff = dataTest[a][j] - dataTrain[b][j];
                diffs[j] = diff;
            }

            for (int i = 0; i < dimension; i++)
            {
                double square = Math.Pow(diffs[i], 2);
                sumSquare += square;
            }

            sumSquareRoot = Math.Sqrt(sumSquare);

            return sumSquareRoot;
        }

        public void NearestNeighbor()
        {
            int benar=0;
            double distance;
            for (int i = 0; i < sizeTest; i++)
            {
                neighbors[i] = new List<KeyValuePair<double, int>>();

                for (int j = 0; j < sizeTrain; j++)
                {
                    if (i != j)
                    {
                        distance = EuclideanDistance(i,j,dimension);
                        neighbors[i].Add(new KeyValuePair<double, int>(distance, j));
                    }
                }
                neighbors[i].Sort(Compare1);
            }

            Console.WriteLine("in");
            
            List<int>[] nearestNeighbor = new List<int>[2000];

            int[] count = new int[sizeTrain]; 
            List<KeyValuePair<double, int>>[] detClass = new List<KeyValuePair<double, int>>[sizeTest];
            for (int i = 0; i < sizeTest; i++)
            {
                for (int m = 0; m < sizeTrain; m++)
                {
                    count[m] = 0;
                }
                
                nearestNeighbor[i] = new List<int>();
                detClass[i] = new List<KeyValuePair<double, int>>();
                for (int j = 0; j < k; j++)
                {
                    nearestNeighbor[i].Add(neighbors[i][j].Value);
                    double temp = dataTrain[nearestNeighbor[i][j]][dimension];
                    int temps = (int)temp;
                    double counter = count[(int)temp]++;
                    detClass[i].Add(new KeyValuePair<double, int>(counter, temps));
                }

                detClass[i].Sort(Compare1);

                if (dataTest[i][dimension] == detClass[i][k - 1].Value)
                { benar++; } 

                dataTest[i][dimension] = detClass[i][k-1].Value;
            }

            double b = Convert.ToDouble(benar);
            double st = Convert.ToDouble(sizeTest);
            akurasi = (b / st)*100;
        }

    }
}