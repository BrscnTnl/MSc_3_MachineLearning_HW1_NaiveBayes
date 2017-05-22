using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class DataPrep
    {

        public List<Abalone> Abalone100TrainSet = new List<Abalone>();

        public List<Abalone> Abalone100TestSet = new List<Abalone>();

        public List<Abalone> Abalone1000TrainSet = new List<Abalone>();

        public List<Abalone> Abalone1000TestSet = new List<Abalone>();

        public List<Abalone> Abalone2000TrainSet = new List<Abalone>();

        public List<Abalone> Abalone2000TestSet = new List<Abalone>();

        private List<Abalone> AbaloneAllSet = new List<Abalone>();

        private List<Abalone> AbaloneOriginalSet = new List<Abalone>();

        public List<Abalone> AbaloneOriginalSetTransformed = new List<Abalone>();





        public DataPrep()
        {


        }

        public void Initialize()
        {
            ExtractData();
            TransformData();
            //AbaloneOriginalSetTransformed = NormalizeData(AbaloneOriginalSetTransformed);
            SeperateTestData(AbaloneOriginalSetTransformed);

            int MaleCount = AbaloneOriginalSetTransformed.Where(x => x.Sex == "M").ToList().Count;
            int FemaleCount = AbaloneOriginalSetTransformed.Where(x => x.Sex == "F").ToList().Count;
            int InfantCount = AbaloneOriginalSetTransformed.Where(x => x.Sex == "I").ToList().Count;

            int YoungCount = AbaloneOriginalSetTransformed.Where(x => x.Age==1).ToList().Count;
            int MiddleCount = AbaloneOriginalSetTransformed.Where(x => x.Age == 2).ToList().Count;
            int OldCount = AbaloneOriginalSetTransformed.Where(x => x.Age == 3).ToList().Count;



        }

        private void ExtractData()
        {
            var reader = new StreamReader(File.OpenRead(@"D:\abalone_dataset.txt"));
            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                line = line.Replace(".",",");
                var values = line.Split('\t');

                Abalone newAbalone = new Abalone();

                newAbalone.Sex = values[0];
                newAbalone.Length = Convert.ToDouble(values[1]);
                newAbalone.Diameter = Convert.ToDouble(values[2]);
                newAbalone.Height = Convert.ToDouble(values[3]);
                newAbalone.Whole_weight = Convert.ToDouble(values[4]);
                newAbalone.Shucked_weight = Convert.ToDouble(values[5]);
                newAbalone.Viscera_weight = Convert.ToDouble(values[6]);
                newAbalone.Shell_weight = Convert.ToDouble(values[7]);
                newAbalone.Age = Convert.ToInt32(values[8]);

                AbaloneOriginalSet.Add(newAbalone);

            }
            


        }


        private void TransformData()
        {
            int AutoID = 0;

            foreach (Abalone newAbalone in AbaloneOriginalSet)
            {
                AbaloneOriginalSetTransformed.Add(
                        new Abalone()
                        {
                            ID = AutoID,
                            Sex = newAbalone.Sex ,
                            Length = newAbalone.Length,
                            Diameter = newAbalone.Diameter,
                            Height = newAbalone.Height,
                            Whole_weight = newAbalone.Whole_weight,
                            Shucked_weight = newAbalone.Shucked_weight,
                            Viscera_weight = newAbalone.Viscera_weight,
                            Shell_weight = newAbalone.Shell_weight,
                            Age = newAbalone.Age
                        }
                    );
                AutoID++;

            }

 

        }



        private void SeperateTestData(List<Abalone> Abalones)
        {
            Random rnd = new Random();

            int testCount100 = 100;
            int testCount1000 = 1000;
            int testCount2000 = 2000;


            Abalone100TrainSet = Abalones.OrderBy(user => rnd.Next()).Take(testCount100).ToList();
            Abalone100TestSet = Abalones.Where(x => !Abalone100TrainSet.Any(y => y.ID == x.ID)).ToList();

            Abalone1000TrainSet = Abalones.OrderBy(user => rnd.Next()).Take(testCount1000).ToList();
            Abalone1000TestSet = Abalones.Where(x => !Abalone1000TrainSet.Any(y => y.ID == x.ID)).ToList();

            Abalone2000TrainSet = Abalones.OrderBy(user => rnd.Next()).Take(testCount2000).ToList();
            Abalone2000TestSet = Abalones.Where(x => !Abalone2000TrainSet.Any(y => y.ID == x.ID)).ToList();

        }


        private List<Abalone> NormalizeData(List<Abalone> Abalones)
        {

            double  Length_Mean, Diameter_Mean, Height_Mean, Whole_weight_Mean, Shucked_weight_Mean, Viscera_weight_Mean, Shell_weight_Mean, Age_Mean;

            double  Length_StdDev, Diameter_StdDev, Height_StdDev, Whole_weight_StdDev, Shucked_weight_StdDev, Viscera_weight_StdDev, Shell_weight_StdDev, Age_StdDev;


            //Sex_Mean = Wines.Select(x => x.Sex).ToList().Sum() / Wines.Count;
            Length_Mean = Abalones.Select(x => x.Length).ToList().Sum() / Abalones.Count;
            Diameter_Mean = Abalones.Select(x => x.Diameter).ToList().Sum() / Abalones.Count;
            Height_Mean = Abalones.Select(x => x.Height).ToList().Sum() / Abalones.Count;
            Whole_weight_Mean = Abalones.Select(x => x.Whole_weight).ToList().Sum() / Abalones.Count;
            Shucked_weight_Mean = Abalones.Select(x => x.Shucked_weight).ToList().Sum() / Abalones.Count;
            Viscera_weight_Mean = Abalones.Select(x => x.Viscera_weight).ToList().Sum() / Abalones.Count;
            Shell_weight_Mean = Abalones.Select(x => x.Shell_weight).ToList().Sum() / Abalones.Count;
            Age_Mean = Abalones.Select(x => x.Age).ToList().Sum() / Abalones.Count;

            //Sex_StdDev = Math.Sqrt(Wines.Sum(x => Math.Pow(x.Sex - Sex_Mean, 2)) / Wines.Count);
            Length_StdDev = Math.Sqrt(Abalones.Sum(x => Math.Pow(x.Length - Length_Mean, 2)) / Abalones.Count);
            Diameter_StdDev = Math.Sqrt(Abalones.Sum(x => Math.Pow(x.Diameter - Diameter_Mean, 2)) / Abalones.Count);
            Height_StdDev = Math.Sqrt(Abalones.Sum(x => Math.Pow(x.Height - Height_Mean, 2)) / Abalones.Count);
            Whole_weight_StdDev = Math.Sqrt(Abalones.Sum(x => Math.Pow(x.Whole_weight - Whole_weight_Mean, 2)) / Abalones.Count);
            Shucked_weight_StdDev = Math.Sqrt(Abalones.Sum(x => Math.Pow(x.Shucked_weight - Shucked_weight_Mean, 2)) / Abalones.Count);
            Viscera_weight_StdDev = Math.Sqrt(Abalones.Sum(x => Math.Pow(x.Viscera_weight - Viscera_weight_Mean, 2)) / Abalones.Count);
            Shell_weight_StdDev = Math.Sqrt(Abalones.Sum(x => Math.Pow(x.Shell_weight - Shell_weight_Mean, 2)) / Abalones.Count);
            Age_StdDev = Math.Sqrt(Abalones.Sum(x => Math.Pow(x.Age - Age_Mean, 2)) / Abalones.Count);
           

            //Wines.ToList().ForEach(x => x.Sex = Convert.ToSingle(((x.Sex) - Sex_Mean) / Sex_StdDev));
            //Abalones.ToList().ForEach(x => x.Length = Convert.ToSingle(((x.Length) - Length_Mean) / Length_StdDev));
            //Abalones.ToList().ForEach(x => x.Diameter = Convert.ToSingle(((x.Diameter) - Diameter_Mean) / Diameter_StdDev));
            //Abalones.ToList().ForEach(x => x.Height = Convert.ToSingle(((x.Height) - Height_Mean) / Height_StdDev));
            //Abalones.ToList().ForEach(x => x.Whole_weight = Convert.ToSingle(((x.Whole_weight) - Whole_weight_Mean) / Whole_weight_StdDev));
            //Abalones.ToList().ForEach(x => x.Shucked_weight = Convert.ToSingle(((x.Shucked_weight) - Shucked_weight_Mean) / Shucked_weight_StdDev));
            //Abalones.ToList().ForEach(x => x.Viscera_weight = Convert.ToSingle(((x.Viscera_weight) - Viscera_weight_Mean) / Viscera_weight_StdDev));
            //Abalones.ToList().ForEach(x => x.Shell_weight = Convert.ToSingle(((x.Shell_weight) - Shell_weight_Mean) / Shell_weight_StdDev));
            //Abalones.ToList().ForEach(x => x.Age = Convert.ToIn(((x.Age) - Age_Mean) / Age_StdDev));
           

            return Abalones;
        }

    }
}
