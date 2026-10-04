using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public string Surname { get; set; }
    public string Group { get; set; }
    public int Grade { get; set; }  
    public int Year { get; set; }  

    public Student(string surname, string group, int grade, int year)
    {
        Surname = surname;
        Group = group;
        Grade = grade;
        Year = year;
    }
}
