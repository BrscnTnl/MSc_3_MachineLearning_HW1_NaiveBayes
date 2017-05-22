using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public List<Abalone> Abalone100TrainSet = new List<Abalone>();

        public List<Abalone> Abalone100TestSet = new List<Abalone>();

        public List<Abalone> Abalone1000TrainSet = new List<Abalone>();

        public List<Abalone> Abalone1000TestSet = new List<Abalone>();

        public List<Abalone> Abalone2000TrainSet = new List<Abalone>();

        public List<Abalone> Abalone2000TestSet = new List<Abalone>();

        public List<Abalone> AbaloneOriginalSetTransformed = new List<Abalone>();
        public Form1()
        {
            InitializeComponent();
            
        }
        int[,] Matris = new int[4, 4];
        private void button1_Click(object sender, EventArgs e)
        {
            int result=0;
            int CorrectPredicted = 0;
            int FalsePredicted = 0;
            int ParameterType = 1;

            if(checkBox1.Checked)
                ParameterType = 2;
            else
                ParameterType = 1;


            int RealYoung = 0, PredictedYoung = 0, RealMiddle = 0, PredictedMiddle = 0, RealOld = 0, PredictedOld = 0;

            double Length=0, Diameter = 0, Height = 0, Whole_weight =0, Shucked_weight = 0, Viscera_weight =0, Shell_weight = 0, Age = 0;





            DataPrep dprp = new DataPrep();
            dprp.Initialize();

            Abalone100TrainSet = dprp.Abalone100TrainSet;
            Abalone100TestSet = dprp.Abalone100TestSet;
            Abalone1000TrainSet = dprp.Abalone1000TrainSet;
            Abalone1000TestSet = dprp.Abalone1000TestSet;
            Abalone2000TrainSet = dprp.Abalone2000TrainSet;
            Abalone2000TestSet = dprp.Abalone2000TestSet;
            Abalone2000TestSet = dprp.Abalone2000TestSet;
            AbaloneOriginalSetTransformed = dprp.AbaloneOriginalSetTransformed;
            StringBuilder str = new StringBuilder();


            str.Append("Minimum"); str.AppendLine();
            Length = AbaloneOriginalSetTransformed.Min(x => x.Length);
            Diameter = AbaloneOriginalSetTransformed.Min(x => x.Diameter);
            Height = AbaloneOriginalSetTransformed.Min(x => x.Height);
            Whole_weight = AbaloneOriginalSetTransformed.Min(x => x.Whole_weight);
            Shucked_weight = AbaloneOriginalSetTransformed.Min(x => x.Shucked_weight);
            Viscera_weight = AbaloneOriginalSetTransformed.Min(x => x.Viscera_weight);
            Shell_weight = AbaloneOriginalSetTransformed.Min(x => x.Shell_weight);
            Age = AbaloneOriginalSetTransformed.Min(x => x.Age);
            str.AppendLine("Length : " + Length);
            str.AppendLine("Diameter : " + Diameter);
            str.AppendLine("Height : " + Height);
            str.AppendLine("Whole_weight : " + Whole_weight);
            str.AppendLine("Shucked_weight : " + Shucked_weight);
            str.AppendLine("Viscera_weight : " + Viscera_weight);
            str.AppendLine("Shell_weight : " + Shell_weight);
            str.AppendLine("Age : " + Age);

            str.Append("Maximum"); str.AppendLine();
            Length = AbaloneOriginalSetTransformed.Max(x => x.Length);
            Diameter = AbaloneOriginalSetTransformed.Max(x => x.Diameter);
            Height = AbaloneOriginalSetTransformed.Max(x => x.Height);
            Whole_weight = AbaloneOriginalSetTransformed.Max(x => x.Whole_weight);
            Shucked_weight = AbaloneOriginalSetTransformed.Max(x => x.Shucked_weight);
            Viscera_weight = AbaloneOriginalSetTransformed.Max(x => x.Viscera_weight);
            Shell_weight = AbaloneOriginalSetTransformed.Max(x => x.Shell_weight);
            Age = AbaloneOriginalSetTransformed.Max(x => x.Age);
            str.AppendLine("Length : " + Length);
            str.AppendLine("Diameter : " + Diameter);
            str.AppendLine("Height : " + Height);
            str.AppendLine("Whole_weight : " + Whole_weight);
            str.AppendLine("Shucked_weight : " + Shucked_weight);
            str.AppendLine("Viscera_weight : " + Viscera_weight);
            str.AppendLine("Shell_weight : " + Shell_weight);
            str.AppendLine("Age : " + Age);

            str.Append("Distinct"); str.AppendLine();
            Length = AbaloneOriginalSetTransformed.Select(x => x.Length).Distinct().Count();
            Diameter = AbaloneOriginalSetTransformed.Select(x => x.Diameter).Distinct().Count();
            Height = AbaloneOriginalSetTransformed.Select(x => x.Height).Distinct().Count();
            Whole_weight = AbaloneOriginalSetTransformed.Select(x => x.Whole_weight).Distinct().Count();
            Shucked_weight = AbaloneOriginalSetTransformed.Select(x => x.Shucked_weight).Distinct().Count();
            Viscera_weight = AbaloneOriginalSetTransformed.Select(x => x.Viscera_weight).Distinct().Count();
            Shell_weight = AbaloneOriginalSetTransformed.Select(x => x.Shell_weight).Distinct().Count();
            Age = AbaloneOriginalSetTransformed.Select(x => x.Age).Distinct().Count();
            str.AppendLine("Length : " + Length);
            str.AppendLine("Diameter : " + Diameter);
            str.AppendLine("Height : " + Height);
            str.AppendLine("Whole_weight : " + Whole_weight);
            str.AppendLine("Shucked_weight : " + Shucked_weight);
            str.AppendLine("Viscera_weight : " + Viscera_weight);
            str.AppendLine("Shell_weight : " + Shell_weight);
            str.AppendLine("Age : " + Age);



            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////// EXPERIMENT 1 WITH SEX, LENGHT, DIAMETER VAL SET 100 Train //////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            NaiveBayes NB1 = new NaiveBayes();
            CorrectPredicted = 0;
            FalsePredicted = 0;
            RealYoung = 0; PredictedYoung = 0; RealMiddle = 0; PredictedMiddle = 0; RealOld = 0; PredictedOld = 0;
            NB1.TrainNB(Abalone100TrainSet);
            ResetMatris();

            foreach (Abalone abl in Abalone100TestSet)
            {
                result=NB1.TestNB(abl,ParameterType);
                Matris[result, abl.Age]++;

                if (result == abl.Age) CorrectPredicted++;
                else FalsePredicted++;

                if (abl.Age == 1) RealYoung++;
                if (abl.Age == 2) RealMiddle++;
                if (abl.Age == 3) RealOld++;
                if (result  == 1) PredictedYoung++;
                if (result == 2) PredictedMiddle++;
                if (result == 3) PredictedOld++;

            }

            str.Append("EXPERIMENT 1 WITH SEX, LENGHT, DIAMETER VAL SET\n"); str.AppendLine();
            str.Append("Pr \\ Re\tYoung\tMiddle\tOld");
            str.AppendLine();
            for (int i = 1; i < 4; i++)
            {
                if (i == 1) str.Append("Young\t"); else if (i == 2) str.Append("Middle\t"); else str.Append("Old\t");
                for (int j = 1; j < 4; j++)
                {
                    str.Append(Matris[i, j] + "\t");
                }
                str.AppendLine();
            }
            str.AppendLine();
            str.AppendLine("Correct Predicted : " + CorrectPredicted);
            str.AppendLine("False Predicted : " + FalsePredicted);
            str.AppendLine("Total Predicted : " + (FalsePredicted+CorrectPredicted));
            str.AppendLine("Young - Real : "+ RealYoung + " Predicted : " + PredictedYoung);
            str.AppendLine("Middle - Real : "+ RealMiddle + " Predicted : "+ PredictedMiddle);
            str.AppendLine("Old - Real : "+ RealOld + " Predicted : " + PredictedOld);
            str.AppendLine("Total Experimented - Real : " + (RealYoung+ RealMiddle + RealOld) + " Predicted : " + (PredictedYoung+ PredictedMiddle+ PredictedOld));

            str.AppendLine("Rate : " + Convert.ToSingle((double)CorrectPredicted /(double)(FalsePredicted + CorrectPredicted)));

            str.AppendLine();
            str.AppendLine();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////// EXPERIMENT 1 WITH SEX, LENGHT, DIAMETER VAL SET 100 Train //////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////// EXPERIMENT 1 WITH SEX, LENGHT, DIAMETER TRN SET 100 Train //////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            NaiveBayes NB4 = new NaiveBayes();
            CorrectPredicted = 0;
            FalsePredicted = 0;
            RealYoung = 0; PredictedYoung = 0; RealMiddle = 0; PredictedMiddle = 0; RealOld = 0; PredictedOld = 0;
            NB4.TrainNB(Abalone100TrainSet);
            ResetMatris();

            foreach (Abalone abl in Abalone100TrainSet)
            {
                result = NB4.TestNB(abl,ParameterType);
                Matris[result, abl.Age]++;

                if (result == abl.Age) CorrectPredicted++;
                else FalsePredicted++;

                if (abl.Age == 1) RealYoung++;
                if (abl.Age == 2) RealMiddle++;
                if (abl.Age == 3) RealOld++;
                if (result == 1) PredictedYoung++;
                if (result == 2) PredictedMiddle++;
                if (result == 3) PredictedOld++;
            }

            str.Append("EXPERIMENT 1 WITH SEX, LENGHT, DIAMETER TEST SET\n"); str.AppendLine();
            str.Append("Pr \\ Re\tYoung\tMiddle\tOld");
            str.AppendLine();
            for (int i = 1; i < 4; i++)
            {
                if (i == 1) str.Append("Young\t"); else if (i == 2) str.Append("Middle\t"); else str.Append("Old\t");
                for (int j = 1; j < 4; j++)
                {
                    str.Append(Matris[i, j] + "\t");
                }
                str.AppendLine();
            }
            str.AppendLine();
            str.AppendLine("Correct Predicted : " + CorrectPredicted);
            str.AppendLine("False Predicted : " + FalsePredicted);
            str.AppendLine("Total Predicted : " + (FalsePredicted + CorrectPredicted));
            str.AppendLine("Young - Real : " + RealYoung + " Predicted : " + PredictedYoung);
            str.AppendLine("Middle - Real : " + RealMiddle + " Predicted : " + PredictedMiddle);
            str.AppendLine("Old - Real : " + RealOld + " Predicted : " + PredictedOld);
            str.AppendLine("Total Experimented - Real : " + (RealYoung + RealMiddle + RealOld) + " Predicted : " + (PredictedYoung + PredictedMiddle + PredictedOld));
            str.AppendLine("Rate : " + Convert.ToSingle((double)CorrectPredicted / (double)(FalsePredicted + CorrectPredicted)));

            str.AppendLine();
            str.AppendLine();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////// EXPERIMENT 1 WITH SEX, LENGHT, DIAMETER TRN SET 100 Train //////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////// EXPERIMENT 2 WITH SEX, LENGHT, DIAMETER VAL SET 1000 Train /////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            NaiveBayes NB2 = new NaiveBayes();
            CorrectPredicted = 0;
            FalsePredicted = 0;
            RealYoung = 0; PredictedYoung = 0; RealMiddle = 0; PredictedMiddle = 0; RealOld = 0; PredictedOld = 0;
            NB2.TrainNB(Abalone1000TrainSet);
            ResetMatris();


            foreach (Abalone abl in Abalone1000TestSet)
            {
                result = NB2.TestNB(abl,ParameterType);
                Matris[result, abl.Age]++;

                if (result == abl.Age) CorrectPredicted++;
                else FalsePredicted++;

                if (abl.Age == 1) RealYoung++;
                if (abl.Age == 2) RealMiddle++;
                if (abl.Age == 3) RealOld++;
                if (result == 1) PredictedYoung++;
                if (result == 2) PredictedMiddle++;
                if (result == 3) PredictedOld++;
            }

            str.Append("EXPERIMENT 2 WITH SEX, LENGHT, DIAMETER VAL SET\n"); str.AppendLine();
            str.Append("Pr \\ Re\tYoung\tMiddle\tOld");
            str.AppendLine();
            for (int i = 1; i < 4; i++)
            {
                if (i == 1) str.Append("Young\t"); else if (i == 2) str.Append("Middle\t"); else str.Append("Old\t");
                for (int j = 1; j < 4; j++)
                {
                    str.Append(Matris[i, j] + "\t");
                }
                str.AppendLine();
            }

            str.AppendLine();
            str.AppendLine("Correct Predicted : " + CorrectPredicted);
            str.AppendLine("False Predicted : " + FalsePredicted);
            str.AppendLine("Total Predicted : " + (FalsePredicted + CorrectPredicted));
            str.AppendLine("Young - Real : " + RealYoung + " Predicted : " + PredictedYoung);
            str.AppendLine("Middle - Real : " + RealMiddle + " Predicted : " + PredictedMiddle);
            str.AppendLine("Old - Real : " + RealOld + " Predicted : " + PredictedOld);
            str.AppendLine("Total Experimented - Real : " + (RealYoung + RealMiddle + RealOld) + " Predicted : " + (PredictedYoung + PredictedMiddle + PredictedOld));
            str.AppendLine("Rate : " + Convert.ToSingle((double)CorrectPredicted / (double)(FalsePredicted + CorrectPredicted)));
            str.AppendLine();
            str.AppendLine();
            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////// EXPERIMENT 2 WITH SEX, LENGHT, DIAMETER VAL SET 1000 Train /////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////// EXPERIMENT 2 WITH SEX, LENGHT, DIAMETER TRN SET 1000 Train /////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            NaiveBayes NB5 = new NaiveBayes();
            CorrectPredicted = 0;
            FalsePredicted = 0;
            RealYoung = 0; PredictedYoung = 0; RealMiddle = 0; PredictedMiddle = 0; RealOld = 0; PredictedOld = 0;
            NB5.TrainNB(Abalone1000TrainSet);
            ResetMatris();

            foreach (Abalone abl in Abalone1000TrainSet)
            {
                result = NB5.TestNB(abl,ParameterType);
                Matris[result, abl.Age]++;

                if (result == abl.Age) CorrectPredicted++;
                else FalsePredicted++;

                if (abl.Age == 1) RealYoung++;
                if (abl.Age == 2) RealMiddle++;
                if (abl.Age == 3) RealOld++;
                if (result == 1) PredictedYoung++;
                if (result == 2) PredictedMiddle++;
                if (result == 3) PredictedOld++;
            }

            str.Append("EXPERIMENT 2 WITH SEX, LENGHT, DIAMETER TEST SET\n"); str.AppendLine();
            str.Append("Pr \\ Re\tYoung\tMiddle\tOld");
            str.AppendLine();
            for (int i = 1; i < 4; i++)
            {
                if (i == 1) str.Append("Young\t"); else if (i == 2) str.Append("Middle\t"); else str.Append("Old\t");
                for (int j = 1; j < 4; j++)
                {
                    str.Append(Matris[i, j] + "\t");
                }
                str.AppendLine();
            }
            str.AppendLine();
            str.AppendLine("Correct Predicted : " + CorrectPredicted);
            str.AppendLine("False Predicted : " + FalsePredicted);
            str.AppendLine("Total Predicted : " + (FalsePredicted + CorrectPredicted));
            str.AppendLine("Young - Real : " + RealYoung + " Predicted : " + PredictedYoung);
            str.AppendLine("Middle - Real : " + RealMiddle + " Predicted : " + PredictedMiddle);
            str.AppendLine("Old - Real : " + RealOld + " Predicted : " + PredictedOld);
            str.AppendLine("Total Experimented - Real : " + (RealYoung + RealMiddle + RealOld) + " Predicted : " + (PredictedYoung + PredictedMiddle + PredictedOld));
            str.AppendLine("Rate : " + Convert.ToSingle((double)CorrectPredicted / (double)(FalsePredicted + CorrectPredicted)));

            str.AppendLine();
            str.AppendLine();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////// EXPERIMENT 2 WITH SEX, LENGHT, DIAMETER TRN SET 1000 Train /////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////// EXPERIMENT 3 WITH SEX, LENGHT, DIAMETER VAL SET 2000 Train /////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////


            NaiveBayes NB3 = new NaiveBayes();
            CorrectPredicted = 0;
            FalsePredicted = 0;
            RealYoung = 0; PredictedYoung = 0; RealMiddle = 0; PredictedMiddle = 0; RealOld = 0; PredictedOld = 0;
            NB3.TrainNB(Abalone2000TrainSet);
            ResetMatris();

            foreach (Abalone abl in Abalone2000TestSet)
            {
                result = NB3.TestNB(abl,ParameterType);
                Matris[result, abl.Age]++;

                if (result == abl.Age) CorrectPredicted++;
                else FalsePredicted++;

                if (abl.Age == 1) RealYoung++;
                if (abl.Age == 2) RealMiddle++;
                if (abl.Age == 3) RealOld++;
                if (result == 1) PredictedYoung++;
                if (result == 2) PredictedMiddle++;
                if (result == 3) PredictedOld++;
            }

            str.Append("EXPERIMENT 3 WITH SEX, LENGHT, DIAMETER VAL SET\n"); str.AppendLine();
            str.Append("Pr \\ Re\tYoung\tMiddle\tOld");
            str.AppendLine();
            for (int i = 1; i < 4; i++)
            {
                if (i == 1) str.Append("Young\t"); else if (i == 2) str.Append("Middle\t"); else str.Append("Old\t");
                for (int j = 1; j < 4; j++)
                {
                    str.Append(Matris[i, j] + "\t");
                }
                str.AppendLine();
            }
            str.AppendLine();
            str.AppendLine("Correct Predicted : " + CorrectPredicted);
            str.AppendLine("False Predicted : " + FalsePredicted);
            str.AppendLine("Total Predicted : " + (FalsePredicted + CorrectPredicted));
            str.AppendLine("Young - Real : " + RealYoung + " Predicted : " + PredictedYoung);
            str.AppendLine("Middle - Real : " + RealMiddle + " Predicted : " + PredictedMiddle);
            str.AppendLine("Old - Real : " + RealOld + " Predicted : " + PredictedOld);
            str.AppendLine("Total Experimented - Real : " + (RealYoung + RealMiddle + RealOld) + " Predicted : " + (PredictedYoung + PredictedMiddle + PredictedOld));
            str.AppendLine("Rate : " + Convert.ToSingle((double)CorrectPredicted / (double)(FalsePredicted + CorrectPredicted)));

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////// EXPERIMENT 3 WITH SEX, LENGHT, DIAMETER VAL SET 2000 Train /////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////// EXPERIMENT 3 WITH SEX, LENGHT, DIAMETER TRN SET 2000 Train /////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            NaiveBayes NB6 = new NaiveBayes();
            CorrectPredicted = 0;
            FalsePredicted = 0;
            RealYoung = 0; PredictedYoung = 0; RealMiddle = 0; PredictedMiddle = 0; RealOld = 0; PredictedOld = 0;
            NB6.TrainNB(Abalone2000TrainSet);
            ResetMatris();

            foreach (Abalone abl in Abalone2000TrainSet)
            {
                result = NB6.TestNB(abl,ParameterType);
                Matris[result, abl.Age]++;

                if (result == abl.Age) CorrectPredicted++;
                else FalsePredicted++;

                if (abl.Age == 1) RealYoung++;
                if (abl.Age == 2) RealMiddle++;
                if (abl.Age == 3) RealOld++;
                if (result == 1) PredictedYoung++;
                if (result == 2) PredictedMiddle++;
                if (result == 3) PredictedOld++;
            }

            str.Append("EXPERIMENT 3 WITH SEX, LENGHT, DIAMETER TEST SET\n"); str.AppendLine();
            str.Append("Pr \\ Re\tYoung\tMiddle\tOld");
            str.AppendLine();
            for (int i = 1; i < 4; i++)
            {
                if (i == 1) str.Append("Young\t"); else if (i == 2) str.Append("Middle\t"); else str.Append("Old\t");
                for (int j = 1; j < 4; j++)
                {
                    str.Append(Matris[i, j] + "\t");
                }
                str.AppendLine();
            }
            str.AppendLine();
            str.AppendLine("Correct Predicted : " + CorrectPredicted);
            str.AppendLine("False Predicted : " + FalsePredicted);
            str.AppendLine("Total Predicted : " + (FalsePredicted + CorrectPredicted));
            str.AppendLine("Young - Real : " + RealYoung + " Predicted : " + PredictedYoung);
            str.AppendLine("Middle - Real : " + RealMiddle + " Predicted : " + PredictedMiddle);
            str.AppendLine("Old - Real : " + RealOld + " Predicted : " + PredictedOld);
            str.AppendLine("Total Experimented - Real : " + (RealYoung + RealMiddle + RealOld) + " Predicted : " + (PredictedYoung + PredictedMiddle + PredictedOld));
            str.AppendLine("Rate : " + Convert.ToSingle((double)CorrectPredicted / (double)(FalsePredicted + CorrectPredicted)));

            str.AppendLine();
            str.AppendLine();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////// EXPERIMENT 3 WITH SEX, LENGHT, DIAMETER TRN SET 2000 Train /////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////




            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////// EXPERIMENT 4 WITH ALL WITH SEX, LENGHT, DIAMETER 100 Train /////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            NaiveBayes NB7 = new NaiveBayes();
            CorrectPredicted = 0;
            FalsePredicted = 0;
            RealYoung = 0; PredictedYoung = 0; RealMiddle = 0; PredictedMiddle = 0; RealOld = 0; PredictedOld = 0;
            NB7.TrainNB(Abalone100TrainSet);
            ResetMatris();

            foreach (Abalone abl in AbaloneOriginalSetTransformed)
            {
                result = NB7.TestNB(abl,ParameterType);
                Matris[result, abl.Age]++;

                if (result == abl.Age) CorrectPredicted++;
                else FalsePredicted++;

                if (abl.Age == 1) RealYoung++;
                if (abl.Age == 2) RealMiddle++;
                if (abl.Age == 3) RealOld++;
                if (result == 1) PredictedYoung++;
                if (result == 2) PredictedMiddle++;
                if (result == 3) PredictedOld++;
            }

            str.Append("EXPERIMENT 4 WITH ALL WITH SEX, LENGHT, DIAMETER 100 Train\n"); str.AppendLine();
            str.Append("Pr \\ Re\tYoung\tMiddle\tOld");
            str.AppendLine();
            for (int i = 1; i < 4; i++)
            {
                if (i == 1) str.Append("Young\t"); else if (i == 2) str.Append("Middle\t"); else str.Append("Old\t");
                for (int j = 1; j < 4; j++)
                {
                    str.Append(Matris[i, j] + "\t");
                }
                str.AppendLine();
            }
            str.AppendLine();
            str.AppendLine("Correct Predicted : " + CorrectPredicted);
            str.AppendLine("False Predicted : " + FalsePredicted);
            str.AppendLine("Total Predicted : " + (FalsePredicted + CorrectPredicted));
            str.AppendLine("Young - Real : " + RealYoung + " Predicted : " + PredictedYoung);
            str.AppendLine("Middle - Real : " + RealMiddle + " Predicted : " + PredictedMiddle);
            str.AppendLine("Old - Real : " + RealOld + " Predicted : " + PredictedOld);
            str.AppendLine("Total Experimented - Real : " + (RealYoung + RealMiddle + RealOld) + " Predicted : " + (PredictedYoung + PredictedMiddle + PredictedOld));
            str.AppendLine("Rate : " + Convert.ToSingle((double)CorrectPredicted / (double)(FalsePredicted + CorrectPredicted)));

            str.AppendLine();
            str.AppendLine();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////// EXPERIMENT 4 WITH ALL WITH SEX, LENGHT, DIAMETER 100 Train /////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////// EXPERIMENT 5 WITH ALL WITH SEX, LENGHT, DIAMETER 1000 Train ////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            NaiveBayes NB8 = new NaiveBayes();
            CorrectPredicted = 0;
            FalsePredicted = 0;
            RealYoung = 0; PredictedYoung = 0; RealMiddle = 0; PredictedMiddle = 0; RealOld = 0; PredictedOld = 0;
            NB8.TrainNB(Abalone1000TrainSet);
            ResetMatris();

            foreach (Abalone abl in AbaloneOriginalSetTransformed)
            {
                result = NB8.TestNB(abl,ParameterType);
                Matris[result, abl.Age]++;

                if (result == abl.Age) CorrectPredicted++;
                else FalsePredicted++;

                if (abl.Age == 1) RealYoung++;
                if (abl.Age == 2) RealMiddle++;
                if (abl.Age == 3) RealOld++;
                if (result == 1) PredictedYoung++;
                if (result == 2) PredictedMiddle++;
                if (result == 3) PredictedOld++;
            }

            str.Append("EXPERIMENT 6 WITH ALL WITH SEX, LENGHT, DIAMETER 1000 Train\n"); str.AppendLine();
            str.Append("Pr \\ Re\tYoung\tMiddle\tOld");
            str.AppendLine();
            for (int i = 1; i < 4; i++)
            {
                if (i == 1) str.Append("Young\t"); else if (i == 2) str.Append("Middle\t"); else str.Append("Old\t");
                for (int j = 1; j < 4; j++)
                {
                    str.Append(Matris[i, j] + "\t");
                }
                str.AppendLine();
            }
            str.AppendLine();
            str.AppendLine("Correct Predicted : " + CorrectPredicted);
            str.AppendLine("False Predicted : " + FalsePredicted);
            str.AppendLine("Total Predicted : " + (FalsePredicted + CorrectPredicted));
            str.AppendLine("Young - Real : " + RealYoung + " Predicted : " + PredictedYoung);
            str.AppendLine("Middle - Real : " + RealMiddle + " Predicted : " + PredictedMiddle);
            str.AppendLine("Old - Real : " + RealOld + " Predicted : " + PredictedOld);
            str.AppendLine("Total Experimented - Real : " + (RealYoung + RealMiddle + RealOld) + " Predicted : " + (PredictedYoung + PredictedMiddle + PredictedOld));
            str.AppendLine("Rate : " + Convert.ToSingle((double)CorrectPredicted / (double)(FalsePredicted + CorrectPredicted)));

            str.AppendLine();
            str.AppendLine();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////// EXPERIMENT 5 WITH ALL WITH SEX, LENGHT, DIAMETER 1000 Train ////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////// EXPERIMENT 6 WITH ALL WITH SEX, LENGHT, DIAMETER 2000 Train ////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            NaiveBayes NB9 = new NaiveBayes();
            CorrectPredicted = 0;
            FalsePredicted = 0;
            RealYoung = 0; PredictedYoung = 0; RealMiddle = 0; PredictedMiddle = 0; RealOld = 0; PredictedOld = 0;
            NB9.TrainNB(Abalone2000TrainSet);
            ResetMatris();

            foreach (Abalone abl in AbaloneOriginalSetTransformed)
            {
                result = NB9.TestNB(abl,ParameterType);
                Matris[result, abl.Age]++;

                if (result == abl.Age) CorrectPredicted++;
                else FalsePredicted++;

                if (abl.Age == 1) RealYoung++;
                if (abl.Age == 2) RealMiddle++;
                if (abl.Age == 3) RealOld++;
                if (result == 1) PredictedYoung++;
                if (result == 2) PredictedMiddle++;
                if (result == 3) PredictedOld++;
            }

            str.Append("EXPERIMENT 6 WITH ALL WITH SEX, LENGHT, DIAMETER 1000 Train\n"); str.AppendLine();
            str.Append("Pr \\ Re\tYoung\tMiddle\tOld");
            str.AppendLine();
            for (int i = 1; i < 4; i++)
            {
                if (i == 1) str.Append("Young\t"); else if (i == 2) str.Append("Middle\t"); else str.Append("Old\t");
                for (int j = 1; j < 4; j++)
                {
                    str.Append(Matris[i, j] + "\t");
                }
                str.AppendLine();
            }
            str.AppendLine();
            str.AppendLine("Correct Predicted : " + CorrectPredicted);
            str.AppendLine("False Predicted : " + FalsePredicted);
            str.AppendLine("Total Predicted : " + (FalsePredicted + CorrectPredicted));
            str.AppendLine("Young - Real : " + RealYoung + " Predicted : " + PredictedYoung);
            str.AppendLine("Middle - Real : " + RealMiddle + " Predicted : " + PredictedMiddle);
            str.AppendLine("Old - Real : " + RealOld + " Predicted : " + PredictedOld);
            str.AppendLine("Total Experimented - Real : " + (RealYoung + RealMiddle + RealOld) + " Predicted : " + (PredictedYoung + PredictedMiddle + PredictedOld));
            str.AppendLine("Rate : " + Convert.ToSingle((double)CorrectPredicted / (double)(FalsePredicted + CorrectPredicted)));

            str.AppendLine();
            str.AppendLine();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////// EXPERIMENT 6 WITH ALL WITH SEX, LENGHT, DIAMETER 2000 Train ////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////////////




            textBox1.Text = str.ToString();

        }


        public void ResetMatris()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    Matris[i, j] = 0;
                }
                
            }


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
