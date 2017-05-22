using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class NaiveBayes
    {

        double[] Length_Mean = new double[4], Diameter_Mean = new double[4], Height_Mean = new double[4], Whole_weight_Mean = new double[4], Shucked_weight_Mean = new double[4], Viscera_weight_Mean = new double[4], Shell_weight_Mean = new double[4], Age_Mean = new double[4];

        double[] Length_StdDev = new double[4], Diameter_StdDev = new double[4], Height_StdDev = new double[4], Whole_weight_StdDev = new double[4], Shucked_weight_StdDev = new double[4], Viscera_weight_StdDev = new double[4], Shell_weight_StdDev = new double[4], Age_StdDev = new double[4];

        double[] Length_Variance = new double[4], Diameter_Variance = new double[4], Height_Variance = new double[4], Whole_weight_Variance = new double[4], Shucked_weight_Variance = new double[4], Viscera_weight_Variance = new double[4], Shell_weight_Variance = new double[4], Age_Variance = new double[4];

        double ProbabilityOfYoung = 0, ProbabilityOfMiddleAged = 0, ProbabilityOfOld = 0;

        double ProbabilityOfMale = 0, ProbabilityOfFemale = 0, ProbabilityOfInfant = 0;


        int OldMaleCount = 0, OldFemaleCount = 0, OldInfantCount = 0;
        int MiddleMaleCount = 0, MiddleFemaleCount = 0, MiddleInfantCount = 0;
        int YoungMaleCount = 0, YoungFemaleCount = 0, YoungInfantCount = 0;
        int MaleCount = 0, FemaleCount = 0, InfantCount = 0;

        double Evidence = 0;
        double ProbabilityYoung = 0, ProbabilityMiddle = 0, ProbabilityOld = 0;

        //double[] ProbabilityOfAge;

        public NaiveBayes()
        { }

        public void TrainNB(List<Abalone> Abalones)
        {
            ResetArrays();
            ComputeMeanVariance(Abalones);
        }

        public int TestNB(Abalone Abalone,int ExperimentType)
        {
            double FinalResult1, FinalResult2, FinalResult3;

            if (ExperimentType == 1)
            {
                Evidence = ComputeEvidenceFor(Abalone, true, true, true);
                ProbabilityYoung = ComputeProbabilityFor(1, Abalone, true, true, true);
                ProbabilityMiddle = ComputeProbabilityFor(2, Abalone, true, true, true);
                ProbabilityOld = ComputeProbabilityFor(3, Abalone, true, true, true);
            }
            else
            {
                Evidence = ComputeEvidenceFor(Abalone, true, true, true, true, true, true, true, true);
                ProbabilityYoung = ComputeProbabilityFor(1, Abalone, true, true, true, true, true, true, true, true);
                ProbabilityMiddle = ComputeProbabilityFor(2, Abalone, true, true, true, true, true, true, true, true);
                ProbabilityOld = ComputeProbabilityFor(3, Abalone, true, true, true, true, true, true, true, true);
            }

            FinalResult1 = ProbabilityYoung / Evidence;
            FinalResult2 = ProbabilityMiddle / Evidence;
            FinalResult3 = ProbabilityOld / Evidence;

            if (FinalResult1 > FinalResult2 && FinalResult1 > FinalResult3)
                return 1;
            else if (FinalResult2 > FinalResult3)
                return 2;
            else
                return 3;


        }


        private void ResetArrays()
        {

            for (int i = 1; i < 4; i++)
            {


                Length_Mean[i] = 0;
                Diameter_Mean[i] = 0;
                Height_Mean[i] = 0;
                Whole_weight_Mean[i] = 0;
                Shucked_weight_Mean[i] = 0;
                Viscera_weight_Mean[i] = 0;
                Shell_weight_Mean[i] = 0;


                //Sex_StdDev = Math.Sqrt(Wines.Sum(x => Math.Pow(x.Sex - Sex_Mean, 2)) / Wines.Count);
                Length_Variance[i] = 0;
                Diameter_Variance[i] = 0;
                Height_Variance[i] = 0;
                Whole_weight_Variance[i] = 0;
                Shucked_weight_Variance[i] = 0;
                Viscera_weight_Variance[i] = 0;
                Shell_weight_Variance[i] = 0;
                //Age_Variance[i] = Abalones.Where(z => z.Age == i).Sum(x => Math.Pow(x.Age - Age_Mean[i], 2)) / Abalones.Count;

                //Sex_StdDev = Math.Sqrt(Wines.Sum(x => Math.Pow(x.Sex - Sex_Mean, 2)) / Wines.Count);
                Length_StdDev[i] = 0;
                Diameter_StdDev[i] = 0;
                Height_StdDev[i] = 0;
                Whole_weight_StdDev[i] = 0;
                Shucked_weight_StdDev[i] = 0;
                Viscera_weight_StdDev[i] = 0;
                Shell_weight_StdDev[i] = 0;
                //Age_StdDev[i] = Math.Sqrt(Age_Variance[i]);
            }


        }

        private void ComputeMeanVariance(List<Abalone> Abalones)
        {
            ProbabilityOfYoung = ((double)Abalones.Where(x => x.Age == 1).ToList().Count) / ((double)Abalones.ToList().Count);
            ProbabilityOfMiddleAged = ((double)Abalones.Where(x => x.Age == 2).ToList().Count) / ((double)Abalones.ToList().Count);
            ProbabilityOfOld = ((double)Abalones.Where(x => x.Age == 3).ToList().Count) / ((double)Abalones.ToList().Count);

            ProbabilityOfMale = ((double)Abalones.Where(x => x.Sex == "M").ToList().Count) / ((double)Abalones.ToList().Count);
            ProbabilityOfFemale = ((double)Abalones.Where(x => x.Sex == "F").ToList().Count) / ((double)Abalones.ToList().Count);
            ProbabilityOfInfant = ((double)Abalones.Where(x => x.Sex == "I").ToList().Count) / ((double)Abalones.ToList().Count);


            OldMaleCount = Abalones.Where(x => x.Age == 3 && x.Sex == "M").ToList().Count;
            OldFemaleCount = Abalones.Where(x => x.Age == 3 && x.Sex == "F").ToList().Count;
            OldInfantCount = Abalones.Where(x => x.Age == 3 && x.Sex == "I").ToList().Count;
            MiddleMaleCount = Abalones.Where(x => x.Age == 2 && x.Sex == "M").ToList().Count;
            MiddleFemaleCount = Abalones.Where(x => x.Age == 2 && x.Sex == "F").ToList().Count;
            MiddleInfantCount = Abalones.Where(x => x.Age == 2 && x.Sex == "I").ToList().Count;
            YoungMaleCount = Abalones.Where(x => x.Age == 1 && x.Sex == "M").ToList().Count;
            YoungFemaleCount = Abalones.Where(x => x.Age == 1 && x.Sex == "F").ToList().Count;
            YoungInfantCount = Abalones.Where(x => x.Age == 1 && x.Sex == "I").ToList().Count;

            MaleCount = Abalones.Where(x => x.Sex == "M").ToList().Count;
            FemaleCount = Abalones.Where(x => x.Sex == "F").ToList().Count;
            InfantCount = Abalones.Where(x => x.Sex == "I").ToList().Count;



            for (int i = 1; i < 4; i++)
            {

                //Sex_Mean = Wines.Select(x => x.Sex).ToList().Sum() / Wines.Count;
                Length_Mean[i] = Abalones.Where(z => z.Age == i).Select(x => x.Length).ToList().Sum() / Abalones.Count;
                Diameter_Mean[i] = Abalones.Where(z => z.Age == i).Select(x => x.Diameter).ToList().Sum() / Abalones.Count;
                Height_Mean[i] = Abalones.Where(z => z.Age == i).Select(x => x.Height).ToList().Sum() / Abalones.Count;
                Whole_weight_Mean[i] = Abalones.Where(z => z.Age == i).Select(x => x.Whole_weight).ToList().Sum() / Abalones.Count;
                Shucked_weight_Mean[i] = Abalones.Where(z => z.Age == i).Select(x => x.Shucked_weight).ToList().Sum() / Abalones.Count;
                Viscera_weight_Mean[i] = Abalones.Where(z => z.Age == i).Select(x => x.Viscera_weight).ToList().Sum() / Abalones.Count;
                Shell_weight_Mean[i] = Abalones.Where(z => z.Age == i).Select(x => x.Shell_weight).ToList().Sum() / Abalones.Count;
                //Age_Mean[i] = Abalones.Where(z => z.Age == i).Select(x => x.Age).ToList().Sum() / Abalones.Count;

                //Sex_StdDev = Math.Sqrt(Wines.Sum(x => Math.Pow(x.Sex - Sex_Mean, 2)) / Wines.Count);
                Length_Variance[i] = Abalones.Where(z => z.Age == i).Sum(x => Math.Pow(x.Length - Length_Mean[i], 2)) / Abalones.Count;
                Diameter_Variance[i] = Abalones.Where(z => z.Age == i).Sum(x => Math.Pow(x.Diameter - Diameter_Mean[i], 2)) / Abalones.Count;
                Height_Variance[i] = Abalones.Where(z => z.Age == i).Sum(x => Math.Pow(x.Height - Height_Mean[i], 2)) / Abalones.Count;
                Whole_weight_Variance[i] = Abalones.Where(z => z.Age == i).Sum(x => Math.Pow(x.Whole_weight - Whole_weight_Mean[i], 2)) / Abalones.Count;
                Shucked_weight_Variance[i] = Abalones.Where(z => z.Age == i).Sum(x => Math.Pow(x.Shucked_weight - Shucked_weight_Mean[i], 2)) / Abalones.Count;
                Viscera_weight_Variance[i] = Abalones.Where(z => z.Age == i).Sum(x => Math.Pow(x.Viscera_weight - Viscera_weight_Mean[i], 2)) / Abalones.Count;
                Shell_weight_Variance[i] = Abalones.Where(z => z.Age == i).Sum(x => Math.Pow(x.Shell_weight - Shell_weight_Mean[i], 2)) / Abalones.Count;
                //Age_Variance[i] = Abalones.Where(z => z.Age == i).Sum(x => Math.Pow(x.Age - Age_Mean[i], 2)) / Abalones.Count;

                //Sex_StdDev = Math.Sqrt(Wines.Sum(x => Math.Pow(x.Sex - Sex_Mean, 2)) / Wines.Count);
                Length_StdDev[i] = Math.Sqrt(Length_Variance[i]);
                Diameter_StdDev[i] = Math.Sqrt(Diameter_Variance[i]);
                Height_StdDev[i] = Math.Sqrt(Height_Variance[i]);
                Whole_weight_StdDev[i] = Math.Sqrt(Whole_weight_Variance[i]);
                Shucked_weight_StdDev[i] = Math.Sqrt(Shucked_weight_Variance[i]);
                Viscera_weight_StdDev[i] = Math.Sqrt(Viscera_weight_Variance[i]);
                Shell_weight_StdDev[i] = Math.Sqrt(Shell_weight_Variance[i]);
                //Age_StdDev[i] = Math.Sqrt(Age_Variance[i]);
            }


        }


        private double ComputeBayesianProbability(double Variance, double ObservationValueOfVariable, double Mean)
        {

            double GaussianNaiveBayes = 0;

            GaussianNaiveBayes = (1 / (2 * Math.PI* Variance))*Math.Pow(Math.E,-1*(Math.Pow(ObservationValueOfVariable - Mean,2) / (2 * Variance)));

            return GaussianNaiveBayes;
        }


        private double ComputeEvidenceFor( Abalone abln, bool Sex = false, bool Length = false, bool Diameter = false, bool Height = false, bool Whole_weight = false, bool Shucked_weight = false, bool Viscera_weight = false, bool Shell_weight = false)
        {
            double AgeTotal = 1, evidence=0;
            double SexCountVariable = 0, SexCountVariable2 = 0;

            for (int TestAge = 1; TestAge < 4; TestAge++)
            {

                if (Sex)
                {

                    switch (TestAge)
                    {
                        case 1:
                            if (abln.Sex == "M")
                                SexCountVariable = YoungMaleCount;
                            else if (abln.Sex == "F")
                                SexCountVariable = YoungFemaleCount;
                            else if (abln.Sex == "I")
                                SexCountVariable = YoungInfantCount;
                            break;
                        case 2:
                            if (abln.Sex == "M")
                                SexCountVariable = MiddleMaleCount;
                            else if (abln.Sex == "F")
                                SexCountVariable = MiddleFemaleCount;
                            else if (abln.Sex == "I")
                                SexCountVariable = MiddleInfantCount;
                            break;
                        case 3:
                            if (abln.Sex == "M")
                                SexCountVariable = OldMaleCount;
                            else if (abln.Sex == "F")
                                SexCountVariable = OldFemaleCount;
                            else if (abln.Sex == "I")
                                SexCountVariable = OldInfantCount;
                            break;
                    }

                    if (abln.Sex == "M")
                        SexCountVariable2 = MaleCount;
                    else if (abln.Sex == "F")
                        SexCountVariable2 = FemaleCount;
                    else if (abln.Sex == "I")
                        SexCountVariable2 = InfantCount;




                    AgeTotal *= ((double)SexCountVariable) / ((double)SexCountVariable2);
                }
                if (Length)
                        AgeTotal *= ComputeBayesianProbability(Length_Variance[TestAge], abln.Length, Length_Mean[TestAge]);
                if (Diameter)
                        AgeTotal *= ComputeBayesianProbability(Diameter_Variance[TestAge], abln.Diameter, Diameter_Mean[TestAge]);
                if (Height)
                        AgeTotal *= ComputeBayesianProbability(Height_Variance[TestAge], abln.Height, Height_Mean[TestAge]);
                if (Whole_weight)
                        AgeTotal *= ComputeBayesianProbability(Whole_weight_Variance[TestAge], abln.Whole_weight, Whole_weight_Mean[TestAge]);
                if (Shucked_weight)
                        AgeTotal *= ComputeBayesianProbability(Shucked_weight_Variance[TestAge], abln.Shucked_weight, Shucked_weight_Mean[TestAge]);
                if (Viscera_weight)
                        AgeTotal *= ComputeBayesianProbability(Viscera_weight_Variance[TestAge], abln.Viscera_weight, Viscera_weight_Mean[TestAge]);
                if (Shell_weight)
                        AgeTotal *= ComputeBayesianProbability(Shell_weight_Variance[TestAge], abln.Shell_weight, Shell_weight_Mean[TestAge]);

                evidence += AgeTotal;
                AgeTotal = 1;
            }


            return evidence;
        }

        private double ComputeProbabilityFor(int TestAge,Abalone abln, bool Sex = false, bool Length = false, bool Diameter = false, bool Height = false, bool Whole_weight = false, bool Shucked_weight = false, bool Viscera_weight = false, bool Shell_weight = false)
        {
            double AgeTotal = 1;
            int SexCountVariable = 0, SexCountVariable2 = 0;

            

                if (Sex)
                {

                    switch (TestAge)
                    {
                        case 1:
                            if (abln.Sex == "M")
                                SexCountVariable = YoungMaleCount;
                            else if (abln.Sex == "F")
                                SexCountVariable = YoungFemaleCount;
                            else if (abln.Sex == "I")
                                SexCountVariable = YoungInfantCount;
                            break;
                        case 2:
                            if (abln.Sex == "M")
                                SexCountVariable = MiddleMaleCount;
                            else if (abln.Sex == "F")
                                SexCountVariable = MiddleFemaleCount;
                            else if (abln.Sex == "I")
                                SexCountVariable = MiddleInfantCount;
                            break;
                        case 3:
                            if (abln.Sex == "M")
                                SexCountVariable = OldMaleCount;
                            else if (abln.Sex == "F")
                                SexCountVariable = OldFemaleCount;
                            else if (abln.Sex == "I")
                                SexCountVariable = OldInfantCount;
                            break;
                    }

                    if (abln.Sex == "M")
                        SexCountVariable2 = MaleCount;
                    else if (abln.Sex == "F")
                        SexCountVariable2 = FemaleCount;
                    else if (abln.Sex == "I")
                        SexCountVariable2 = InfantCount;




                    AgeTotal *= ((double)SexCountVariable) / ((double)SexCountVariable2);
                }
                if (Length)
                    AgeTotal *= ComputeBayesianProbability(Length_Variance[TestAge], abln.Length, Length_Mean[TestAge]);
                if (Diameter)
                    AgeTotal *= ComputeBayesianProbability(Diameter_Variance[TestAge], abln.Diameter, Diameter_Mean[TestAge]);
                if (Height)
                    AgeTotal *= ComputeBayesianProbability(Height_Variance[TestAge], abln.Height, Height_Mean[TestAge]);
                if (Whole_weight)
                    AgeTotal *= ComputeBayesianProbability(Whole_weight_Variance[TestAge], abln.Whole_weight, Whole_weight_Mean[TestAge]);
                if (Shucked_weight)
                    AgeTotal *= ComputeBayesianProbability(Shucked_weight_Variance[TestAge], abln.Shucked_weight, Shucked_weight_Mean[TestAge]);
                if (Viscera_weight)
                    AgeTotal *= ComputeBayesianProbability(Viscera_weight_Variance[TestAge], abln.Viscera_weight, Viscera_weight_Mean[TestAge]);
                if (Shell_weight)
                    AgeTotal *= ComputeBayesianProbability(Shell_weight_Variance[TestAge], abln.Shell_weight, Shell_weight_Mean[TestAge]);

               

            


            return AgeTotal;
        }

    }
}
