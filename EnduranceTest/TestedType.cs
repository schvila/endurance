using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnduranceTest
{
    public class TestedType
    {
        private System.Type _type;
        public System.Type TestType => _type;
        private object _instance;
        public object Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = System.Activator.CreateInstance(_type);
                }
                return _instance;
            }
        }

        public TestedType(System.Type type)
        {
            _type = type;
        }
        private TestedType() { }


    }
}
