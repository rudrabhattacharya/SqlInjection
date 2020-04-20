using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Person
    {
        public int Age { get; set; }

        public string Name { get; set; }

        public double Salary { get; set; }

        public string Title { get; set; }

        public List<Person> Children { get; set; }

        public Person Partner { get; set; }
    }
}
