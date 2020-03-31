using System;
using System.Collections.Generic;
using System.Text;

namespace Cw2
{
    class Student
    {
        public string indexNumber, fname, lname, birthdate, email, mothersName, fathersName, studiesName, studiesMode;

        public Student(string[] args)
        {
            indexNumber = "s"+args[4];
            fname = args[0];
            lname = args[1];
            birthdate = DateTime.Parse(args[5]).ToString("dd.mm.yyyy");
            email = args[6];
            mothersName = args[7];
            fathersName = args[8];
            studiesName = args[2];
            studiesMode = args[3];
        }
    }
}
