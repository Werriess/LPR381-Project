using Microsoft.SolverFoundation.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace LPR381_Project.src.SensitivityAnalysis
{
    public class SensitivityAnalysis
    {
        private readonly SolverContext _context;
        private readonly Model _model;
        private readonly Dictionary<Constraint, double> _originalRHSValues;

        public SensitivityAnalysis(SolverContext context, Model model)
        {
            _context = context;
            _model = model;
            _originalRHSValues = new Dictionary<Constraint, double>();

            // Store the original RHS values externally when constraints are added
            foreach (var constraint in _model.Constraints)
            {
                _originalRHSValues[constraint] = ExtractRHS(constraint);
            }
        }

        public void PerformSensitivityAnalysis(Decision x1, Decision x2)
        {
            foreach (var constraint in _model.Constraints.ToList())
            {
                Console.WriteLine($"\n=== Sensitivity Analysis for Constraint: {constraint.Name} ===");

                double originalRHS = _originalRHSValues[constraint];
                Console.WriteLine($"Original RHS = {originalRHS}");
                Console.WriteLine("RHS Change\tObjective Value\tDecision Variables (x1, x2)");

                for (double delta = -2; delta <= 2; delta += 1)
                {
                    // Modify the RHS of the constraint directly
                    double newRHS = originalRHS + delta;

                    // Remove the old constraint
                    _model.RemoveConstraint(constraint);

                    // Recreate the constraint with the updated RHS
                    Term updatedConstraintTerm = UpdateConstraintRHS(constraint.Name, newRHS);
                    Constraint updatedConstraint = _model.AddConstraint(constraint.Name, updatedConstraintTerm);

                    // Re-solve the problem
                    Solution solution = _context.Solve();
                    Report report = solution.GetReport();

                    double objectiveValue = solution.Goals.First().ToDouble();
                    double x1Value = x1.GetDouble();
                    double x2Value = x2.GetDouble();

                    // Display the results
                    Console.WriteLine($"{newRHS}\t\t{objectiveValue}\t\t(x1 = {x1Value}, x2 = {x2Value})");

                    // Restore the original constraint
                    _model.RemoveConstraint(updatedConstraint);
                    _model.AddConstraint(constraint.Name, constraint.Expression);
                }

                Console.WriteLine("=== End of Sensitivity Analysis ===\n");
            }
        }

        private double ExtractRHS(Constraint constraint)
        {
            // Extract the RHS value from the constraint by using the model's syntax
            string constraintExpression = constraint.Expression.ToString();
            string[] parts = constraintExpression.Split(new[] { "<=", ">=", "=" }, StringSplitOptions.None);
            return double.Parse(parts[1].Trim());
        }

        private Term UpdateConstraintRHS(string constraintName, double newRHS)
        {
            // Recreate the constraint with a new RHS
            var decisions = _model.Decisions.ToList();
            switch (constraintName)
            {
                case "c1":
                    return 2 * decisions[0] + 3 * decisions[1] <= newRHS;
                case "c2":
                    return decisions[0] + decisions[1] <= newRHS;
                case "c3":
                    return decisions[0] <= newRHS;
                default:
                    throw new ArgumentException("Unknown constraint name");
            }
        }
    }
}

