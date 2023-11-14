using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TD.Model
{
    public class TDModel
    {

        public ObservableCollection<Mark> marks = new ObservableCollection<Mark>();
        public ObservableCollection<Missing> missings = new ObservableCollection<Missing>();
        public ObservableCollection<Student> students = new ObservableCollection<Student>();
        public ObservableCollection<SchoolClass> schools = new ObservableCollection<SchoolClass>();
        public ObservableCollection<CalendarItem> calendarItems = new ObservableCollection<CalendarItem>();
        Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(TDModel)).Assembly;

        public TDModel()
        {


            // marks
            using (Stream stream = assembly.GetManifestResourceStream("TD.Model.Data.Marks.txt"))
            using (StreamReader sr = new StreamReader(stream))
            {
                string[] token;
                while (!sr.EndOfStream)
                {
                    token = sr.ReadLine().Split(',');
                    marks.Add(new Mark(Int32.Parse(token[0]), Int32.Parse(token[1]), Double.Parse(token[2])));
                }

            }

            // missings
            using (Stream stream = assembly.GetManifestResourceStream("TD.Model.Data.Missings.txt"))
            using (StreamReader sr = new StreamReader(stream))
            {
                string[] token;
                while (!sr.EndOfStream)
                {
                    token = sr.ReadLine().Split(',');
                    missings.Add(new Missing(Int32.Parse(token[0]), token[1]));
                }

            }

            // students
            using (Stream stream = assembly.GetManifestResourceStream("TD.Model.Data.Students.txt"))
            using (StreamReader sr = new StreamReader(stream))
            {
                string[] token;
                while (!sr.EndOfStream)
                {
                    token = sr.ReadLine().Split(',');
                    students.Add(new Student(Int32.Parse(token[0]), token[1], token[2]));
                }

            }

            //SchoolClasses
            using (Stream stream = assembly.GetManifestResourceStream("TD.Model.Data.SchoolClass.txt"))
            using (StreamReader sr = new StreamReader(stream))
            {
                string[] token;
                while (!sr.EndOfStream)
                {
                    token = sr.ReadLine().Split(',');
                    schools.Add(new SchoolClass(token[0]));
                }

            }

        }

        public List<Student> findStudentsByClassName(string name)
        {
            return students.Where(s => s.ClassName.Equals(name)).ToList();
        }

        public List<Mark> findMarksByStudentId(int id)
        {
            return marks.Where(m => m.StudentId == id).ToList();
        }

        public ObservableCollection<Missing> findMissingsByStudentId(int id)
        {
            var missing = missings.Where(m => m.StudentId == id).ToList();
            ObservableCollection<Missing> obsMiss = new ObservableCollection<Missing>(missing);
            return obsMiss;
           // return missings.Where(m => m.StudentId == id).ToList();
        }

        public Student findStudentById(int id)
        {
            return students.Where(s=>s.Id==id).FirstOrDefault();
        }

    }
}
