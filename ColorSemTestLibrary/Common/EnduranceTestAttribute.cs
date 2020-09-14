using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorSemTestLibrary.Common
{
    [AttributeUsage(AttributeTargets.All)]
    public class EnduranceTestAttribute : Attribute
    {
        //estimated test duration
        string _description;
        public EnduranceTestAttribute(string description = null)
        {
            _description = description;
        }
        public string Description => _description;


    }
}
