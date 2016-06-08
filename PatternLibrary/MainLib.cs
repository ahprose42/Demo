using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternLibraryDLL
{

    public class CustomEventManager
    {
        public event EventHandler Handler;

        // Generic method to trigger an arbitrary event
        public void Trigger()
        {
            EventHandler eventHandler = Handler;
            if (eventHandler != null)
            {
                eventHandler(this, EventArgs.Empty);
            }
        }
    }

    public class CustomEventListener
    {
        // VARIABLES ////////
        public string output;
        private LogicTemplate _template;

        // CONSTRUCTORS ////////
        public CustomEventListener(LogicTemplate template, string outputReference)
        {
            output = outputReference;
            _template = template;
        }

        // Register Event
        public void Execute(object sender, EventArgs args)
        {
            output = _template.RunRules();
        }
    }

    public abstract class LogicTemplate
    {
        protected Dictionary<int, string> _rules; // modulus, string replacement

        protected LogicTemplate()
        {
            _rules = new Dictionary<int, string>();
        }

        public string RunRules()
        {
            string result = "";

            // Func to to check if a number is evenly divisible by another
            //   take the modulus of a number, if the remainder is "0", return "true"
            //   (the number is evenly divisible)
            Func<int, int, bool> isMatch = (dividend, divisor) => dividend % divisor == 0; // return true if 'i' mod 'comb' equals zero

            for (int i = 1; i <= 100; i++) // for the number 1 to 100...
            {
                List<string> matchingCombs = _rules.Where(ruleList => isMatch(i, ruleList.Key)) // Retrieve a subset of the possible patterns where the current number is evenly divisible by the pattern number 
                                                    .Select(ruleSubset => ruleSubset.Value) // Select the values from the subset of the returned dictionary
                                                    .DefaultIfEmpty(i.ToString()) // If there are no values, only inser the current "i" value
                                                    .ToList(); // Convert the returns to a list

                result += string.Join(" ", matchingCombs) + System.Environment.NewLine;
            }

            return result;
        }

        private static string PrintPatterns(Dictionary<int, string> rules)
        {
            // Func to to check if a number is evenly divisible by another
            //   take the modulus of a number, if the remainder is "0", return "true"
            //   (the number is evenly divisible)
            Func<int, int, bool> isMatch = (dividend, divisor) => dividend % divisor == 0; // return true if 'i' mod 'comb' equals zero

            string result = "";

            for (int i = 1; i <= 100; i++) // for the number 1 to 100...
            {
                List<string> matchingCombs = rules.Where(ruleList => isMatch(i, ruleList.Key)) // Retrieve a subset of the possible patterns where the current number is evenly divisible by the pattern number 
                                                    .Select(ruleSubset => ruleSubset.Value) // Select the values from the subset of the returned dictionary
                                                    .DefaultIfEmpty(i.ToString()) // If there are no values, only insert the current "i" value
                                                    .ToList(); // Convert the returns to a list

                //Console.WriteLine(string.Join(" ", matchingCombs)); // Join the return list, separated by a space character : " "
                result += string.Join(" ", matchingCombs) + System.Environment.NewLine;
            }

            return result;
        }
    }

    public class RegisterClass : LogicTemplate
    {
        public RegisterClass() : base()
        {
            _rules.Add(3, "Register"); // Add "Register" for multiples of 3
            _rules.Add(5, "Patient"); // Add "Patient" for multiples of 5
        }
    }

    public class DiagnoseClass : LogicTemplate
    {
        public DiagnoseClass() : base()
        {
            _rules.Add(2, "Diagnose"); // Add "Diagnose" for multiples of 2
            _rules.Add(7, "Patient"); // Add "Patient" for multiples of 7
        }
    }
}
