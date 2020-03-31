using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Cw2
{
    class Program
    {
        static void Main(string[] args)
        {
            string csvFileAddress;
            string resultPath;
            string format;

            List<Student> students = new List<Student>();

            const string defaultCsvFileAddress = "data.csv";
            const string defaultResultPath = "result.xml";
            const string defaultFormat = "xml";

            if (args.Length == 3)
            {
                csvFileAddress = args[0];
                resultPath = args[1];
                format = args[2];
            }
            else
            {
                csvFileAddress = defaultCsvFileAddress;
                resultPath = defaultResultPath;
                format = defaultFormat;

            }

            Console.WriteLine(csvFileAddress);
            Console.WriteLine(resultPath);
            Console.WriteLine(format);

            try
            {
                using (var stream = new StreamReader(File.OpenRead(csvFileAddress)))
                {
                    string line = null;
                    while ((line = stream.ReadLine()) != null)
                    {
                        string[] dataFromCsv = line.Split(',');
                        if (dataFromCsv.Length == 9)
                        {
                            bool noEmptyValues = true;
                            for (int i = 0; i < 9; i++)
                            {
                                if (dataFromCsv[i] == "")
                                    noEmptyValues = false;
                            }
                            if (noEmptyValues)
                            {
                                students.Add(new Student(dataFromCsv));
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File " + csvFileAddress + " not found.");
            }

            if (!File.Exists(resultPath))
            {
                File.Create(resultPath);
            }

            using (var stream = new StreamWriter(File.OpenWrite(resultPath)))
            {
                stream.Write("<uczelnia\ncreatedAt = \"{0}\"\nauthor = \"Jerzy Stajszczak\">",DateTime.Today.ToString("dd.mm.yyyy"));
                stream.Write("\n\t<studenci>");

                foreach (var student in students)
                {
                    stream.Write("\n\t\t<student indexNumber=\"{0}\">",student.indexNumber);
                    stream.Write("\n\t\t\t<fname>{0}</fname>",student.fname);
                    stream.Write("\n\t\t\t<lname>{0}</lname>", student.lname);
                    stream.Write("\n\t\t\t<birthdate>{0}</birthdate>", student.birthdate);
                    stream.Write("\n\t\t\t<email>{0}</email>", student.email);
                    stream.Write("\n\t\t\t<mothersName>{0}</mothersName>", student.mothersName);
                    stream.Write("\n\t\t\t<fathersName>{0}</fathersName>", student.fathersName);
                    stream.Write("\n\t\t\t<studies>");
                    stream.Write("\n\t\t\t\t<name>{0}</name>",student.studiesName);
                    stream.Write("\n\t\t\t\t<mode>{0}</mode>", student.studiesMode);
                    stream.Write("\n\t\t\t</studies>");
                    stream.Write("\n\t\t</student>");
                }

                stream.Write("\n\t</studenci>");
                stream.Write("\n</uczelnia>");
            }

        }
    }
}
