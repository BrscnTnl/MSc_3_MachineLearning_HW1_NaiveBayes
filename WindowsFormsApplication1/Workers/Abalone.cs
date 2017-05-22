using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class Abalone
    {

        public int ID { get; set; }

        public string Sex { get; set; } // M or F or I 

        public double Length { get; set; }
        public double Diameter { get; set; }
        public double Height { get; set; }
        public double Whole_weight { get; set; }
        public double Shucked_weight { get; set; }
        public double Viscera_weight { get; set; }
        public double Shell_weight { get; set; }
        public int Age { get; set; }
        

        public double temp1 { get; set; }
        public double temp2 { get; set; }
        public double temp3 { get; set; }

        public double GetArray(int i)
        {
            switch (i)
            {
                case 0: 
                        switch (Sex)
                    {
                        case "M": return 1.0;
                        case "F": return 2.0;
                        case "I": return 3.0;
                        default: return 1.0;
                    }
                        
                        
                        ;
                case 1: return Length;
                case 2: return Diameter;
                case 3: return Height;
                case 4: return Whole_weight;
                case 5: return Shucked_weight;
                case 6: return Viscera_weight;
                case 7: return Shell_weight;
                case 8: return Age;
                default: return 0;
            }
        }
    }
}
