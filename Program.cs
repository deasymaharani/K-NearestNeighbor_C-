using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Timers;

namespace KNN
{
    class Program
    {
        public static List<double>[] shuffleArray(List<double>[] dataInput, int sizeInput)
        {
            List<double>[] newDataInput = new List<double>[4000];

            int angka = 0;
            int i = 0; int[] flag = new int[4000]; int masuk=0;
            while (masuk < sizeInput)
            {
                angka = angka + 2;
                if (angka >= sizeInput )
                {
                    angka = 1;
                }
                if (masuk == sizeInput-1)
                {
                    angka = 0;
                }
                if (flag[angka] != 1)
                {
                    masuk++;
                    flag[angka] = 1;
                    newDataInput[i] = dataInput[angka];
                    Console.WriteLine(i +" "+ angka);
                    i++;
                }
            }

            return newDataInput;
        }

        static void Main(string[] args)
        {
            List<double>[] dataInput = new List<double>[4000];
            List<double>[] shuffleInput = new List<double>[4000];
            List<double>[] dataTrain = new List<double>[4000];
            List<double>[] dataTest = new List<double>[2000];
            double inp = 0.0;

            int sizeTrain = 0, sizeTest = 0, k, sizeInput = 0;

            Console.WriteLine("Enter k : ");
            k = int.Parse(Console.ReadLine());

            //double x = 8.5;
            //Console.WriteLine(x);

            string[] linesTrain = System.IO.File.ReadAllLines(@"D:\DataAll.csv");

            foreach (string line in linesTrain)
            {
                string[] aftersplit = line.Split(',');

                dataInput[sizeInput] = new List<double>();
                foreach (string num in aftersplit)
                {

                   inp = Convert.ToDouble(num.Replace('.', ','));
                   dataInput[sizeInput].Add(inp);
                }
                
                sizeInput++;
            }

            //string[] linesTest = System.IO.File.ReadAllLines(@"D:\dataTestSepeda.txt");

            //foreach (string line in linesTest)
            //{
            //    string[] aftersplit = line.Split(',');

            //    dataTest[sizeTest] = new List<double>();
            //    foreach (string num in aftersplit)
            //    {

            //        inp = Convert.ToDouble(num.Replace('.', ','));
            //        dataTest[sizeTest].Add(inp);
            //    }

            //    sizeTest++;
            //}

            //shuffleInput = shuffleArray(dataInput, sizeInput);

            //Console.ReadKey();
            int dimension = dataInput[0].Count - 1;


            double sizeTrainD = Math.Ceiling(0.73 * sizeInput);
            sizeTrain = Convert.ToInt32(sizeTrainD);
            sizeTest = sizeInput - sizeTrain;

            for (int i = 0; i < sizeTrain; i++)
            {
                dataTrain[i] = dataInput[i];

            }
            int idxTest = sizeTrain;
            for (int i = 0; i < sizeTest; i++)
            {
                dataTest[i] = dataInput[idxTest];
                idxTest++;
            }
            idxTest--;


             //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\cekAll.txt"))
             //   {

             //       for (int i = 0; i < sizeInput; i++)
             //       {
             //           for (int j = 0; j < shuffleInput[i].Count; j++)
             //           {
             //               file.Write(shuffleInput[i][j] + " ");
             //           }
             //           file.WriteLine();
             //       }
             //   }

            //using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\cekTrainTest.txt"))
            //{
            //    file.WriteLine("Data Train");
            //    for (int i = 0; i < sizeTrain; i++)
            //    {
            //        for (int j = 0; j < dataTrain[i].Count; j++)
            //        {
            //            file.Write(dataTrain[i][j] + " ");
            //        }
            //        file.WriteLine();
            //    }

            //    file.WriteLine("Data Test");
            //    for (int i = 0; i < sizeTest; i++)
            //    {
            //        for (int j = 0; j < dataTest[i].Count; j++)
            //        {
            //            file.Write(dataTest[i][j] + " ");
            //        }
            //        file.WriteLine();
            //    }
            //}

            Classify knn = new Classify(dataTrain, dataTest, k, dimension, sizeTrain, sizeTest);

            knn.NearestNeighbor();


            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\outputSensor2.txt"))
            {

                for (int i = 0; i < sizeTest; i++)
                {
                    file.Write(dataTest[i][dimension] + " ");
                    file.WriteLine();
                }
                file.WriteLine(knn.akurasi + "%");
            }
        }
    }
}
