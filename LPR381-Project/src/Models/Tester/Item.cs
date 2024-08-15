using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPR381_Project.src.Models.Tester
{
    internal class Item
    {
        public double Weight { get; set; }
        public double Value { get; set; }
        public string Name { get; set; }
        public double Ratio { get; set; }
        public int Cofficient { get; set; }
        public int Chosen { get; set; }
        public double Subtract { get; set; }

        public Item(string name, int coefficient, double value, double weight, int chosen, double subtract)
        {
            this.Weight = weight;
            this.Value = value;
            this.Name = name;
            this.Ratio = value / weight;
            this.Cofficient = coefficient;
            this.Chosen = chosen;
            this.Subtract = subtract;
        }

       
    }
}
