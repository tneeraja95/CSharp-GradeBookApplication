using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            int numberofStudentsPerGrade = FindNumberofStudentsPerGrade();
            int rank = FindRank(averageGrade);

            if(Students.Count < 5)
                throw new InvalidOperationException();
            if (rank <= numberofStudentsPerGrade)
                return 'A';
            if (rank <= numberofStudentsPerGrade * 2)
                return 'B';
            if (rank <= numberofStudentsPerGrade * 3)
                return 'C';
            if (rank <= numberofStudentsPerGrade * 4)
                return 'D';
            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            else
                base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
            else
                base.CalculateStudentStatistics(name);
        }
        public int FindNumberofStudentsPerGrade()
        {
            return Students.Count / 5;
        }

        public int FindRank(double averageGrade)
        {
            var StudentRankList = Students.OrderByDescending(x => x.AverageGrade).ToList();
            for(int i=0; i<StudentRankList.Count; i++)
            {
                if(StudentRankList[i].AverageGrade == averageGrade)
                {
                    return i + 1;
                }
            }
            return -1;
        }

    }
}
