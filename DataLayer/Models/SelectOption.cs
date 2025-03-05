using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SelectOption
    {
        public string Label { get; set; } = string.Empty;
        public string Value { get; set; }
        public bool Selected { get; set; } = false;

        public SelectOption() { }
        public SelectOption(string label, string value)
        {
            Label = label;
            Value = value;
        }
    }
}
