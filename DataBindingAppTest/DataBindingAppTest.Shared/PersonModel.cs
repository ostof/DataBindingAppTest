using System;
using System.Collections.Generic;
using System.Text;

namespace DataBindingAppTest
{
    public class PersonModel
    {
        public string Name { get; set; }
        public string Age { get; set; }

        public PersonModel() { }

        public PersonModel(string username, int age)
        {
            this.Name = username;
            this.Age = age.ToString() + " ans";
        }

        public override string ToString()
        {
            return Name;
        }


    }
}
