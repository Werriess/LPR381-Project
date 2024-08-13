using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPR381_Project.src.Utils
{
    internal class CanonicalForm
    {
        public bool Maximise { get; set; }
        public bool Minimise { get; set; }
        public string[] Contraints { get; set; }
        public string[] SignRestriction { get; set; }
        public string[] ObjectiveFunction { get; set; }
        public bool Simplex { get; set; }
        public bool DualSimplex { get; set; }


        public CanonicalForm(bool maximise, bool minimise, string[] contraints, string[] signRestriction, string[] objectiveFunction)
        {
            Maximise = maximise;
            Minimise = minimise;
            Contraints = contraints;
            SignRestriction = signRestriction;
            ObjectiveFunction = objectiveFunction;
        }

        public void ReturnConoicalForm()
        {
            if(Maximise == true)
            {
                for(int i = 0; i < ObjectiveFunction.Length; i++)
                {
                    int coefficient = int.Parse(ObjectiveFunction[i]);
                    coefficient *= -1;
                    ObjectiveFunction[i] = coefficient.ToString();
                }
            }
            Console.WriteLine("Canonical Objective Function:");
            Console.WriteLine(string.Join(" ", ObjectiveFunction));
        }
    }
}
