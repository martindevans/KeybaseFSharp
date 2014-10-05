using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Angeronia.Model.Session
{
    public class Proof
    {
        private string _type;
        public string Type { get { return _type; } }

        public Proof(string type)
        {
            _type = type;
        }
    }
}
