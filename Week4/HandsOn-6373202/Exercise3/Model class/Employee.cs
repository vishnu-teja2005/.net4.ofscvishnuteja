using System;
using System.Collections.Generic;
namespace WebApplication2.models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public bool Permanent { get; set; }
        public Department Department { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>()
        public DateTime DateOfBirth { get; set; }
    }
}
