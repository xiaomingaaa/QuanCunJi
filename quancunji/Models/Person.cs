using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quancunji.Models
{
    class Person
    {
        private string stuno;
        private string name;
        private string depname;

        public string Stuno { get => stuno; set => stuno = value; }
        public string Name { get => name; set => name = value; }
        public string Depname { get => depname; set => depname = value; }
        public Person(string stuno,string name,string depname)
        {
            Stuno = stuno;
            Name = name;
            Depname = depname;
        }
    }
}
