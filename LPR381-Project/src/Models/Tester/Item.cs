using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPR381_Project.src.Models.Tester
{
    internal class Item
    {
        public float Weight { get; set; }
        public float Value { get; set; }
        public string Name { get; set; }
        public float Ratio { get; set; }
        public int Cofficient { get; set; }
        public int Chosen { get; set; }
        public float Subtract { get; set; }

        public Item(string name, int coefficient, float value, float weight, int chosen, float subtract)
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
