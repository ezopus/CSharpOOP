using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private readonly SubjectRepository subjects;
        private readonly StudentRepository students;
        private readonly UniversityRepository universities;

        public Controller()
        {
            subjects = new SubjectRepository();
            students = new StudentRepository();
            universities = new UniversityRepository();
        }
        public string AddSubject(string subjectName, string subjectType)
        {
            if (subjectType != nameof(EconomicalSubject) && subjectType != nameof(TechnicalSubject) &&
                subjectType != nameof(HumanitySubject))
            {
                return string.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }

            if (subjects.FindByName(subjectName) != null)
            {
                return string.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }

            ISubject currentSubject;
            if (subjectType == nameof(EconomicalSubject))
            {
                currentSubject = new EconomicalSubject(subjects.Models.Count + 1, subjectName);
            }
            else if (subjectType == nameof(TechnicalSubject))
            {
                currentSubject = new TechnicalSubject(subjects.Models.Count + 1, subjectName);
            }
            else
            {
                currentSubject = new HumanitySubject(subjects.Models.Count + 1, subjectName);
            }
            subjects.AddModel(currentSubject);
            return string.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName,
                subjects.GetType().Name);
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (universities.FindByName(universityName) != null)
            {
                return string.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }

            List<int> currentSubjects = new List<int>();
            foreach (var subject in requiredSubjects)
            {
                if (subjects.FindByName(subject) != null)
                {
                    currentSubjects.Add(subjects.FindByName(subject).Id);
                }
            }

            IUniversity currentUniversity = new University(universities.Models.Count + 1, universityName, category, capacity, currentSubjects);

            universities.AddModel(currentUniversity);
            return string.Format(OutputMessages.UniversityAddedSuccessfully, universityName, universities.GetType().Name);
        }

        public string AddStudent(string firstName, string lastName)
        {
            if (students.FindByName($"{firstName} {lastName}") != null)
            {
                return string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            }

            IStudent currentStudent = new Student(students.Models.Count + 1, firstName, lastName);
            students.AddModel(currentStudent);
            return string.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, students.GetType().Name);
        }

        public string TakeExam(int studentId, int subjectId)
        {
            IStudent currentStudent = students.FindById(studentId);
            ISubject currentSubject = subjects.FindById(subjectId);

            if (currentStudent == null)
            {
                return string.Format(OutputMessages.InvalidStudentId);
            }

            if (currentSubject == null)
            {
                return string.Format(OutputMessages.InvalidSubjectId);
            }

            if (currentStudent.CoveredExams.Contains(currentSubject.Id))
            {
                return string.Format(OutputMessages.StudentAlreadyCoveredThatExam, currentStudent.FirstName,
                    currentStudent.LastName, currentSubject.Name);
            }

            currentStudent.CoverExam(currentSubject);
            return string.Format(OutputMessages.StudentSuccessfullyCoveredExam, currentStudent.FirstName,
                currentStudent.LastName, currentSubject.Name);
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            IStudent currentStudent = students.FindByName(studentName);

            //if there is no such student
            if (currentStudent == null)
            {
                string[] names = studentName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                return string.Format(OutputMessages.StudentNotRegitered, names[0], names[1]);
            }

            //if there is no such univesity
            IUniversity currentUniversity = universities.FindByName(universityName);
            if (currentUniversity == null)
            {
                return string.Format(OutputMessages.UniversityNotRegitered, universityName);
            }

            //if student already joined university
            if (currentStudent.University != null && currentStudent.University.Name == universityName)
            {
                return string.Format(OutputMessages.StudentAlreadyJoined, currentStudent.FirstName,
                    currentStudent.LastName, currentUniversity.Name);
            }

            //check if student has passed all exams
            bool hasCoveredAllExams = true;
            foreach (int exam in currentUniversity.RequiredSubjects)
            {
                if (!currentStudent.CoveredExams.Contains(exam))
                {
                    hasCoveredAllExams = false;
                }
            }

            if (!hasCoveredAllExams)
            {
                return string.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
            }
            else
            {
                currentStudent.JoinUniversity(currentUniversity);
                return string.Format(OutputMessages.StudentSuccessfullyJoined, currentStudent.FirstName,
                    currentStudent.LastName, universityName);
            }
        }

        public string UniversityReport(int universityId)
        {
            IUniversity currentUniversity = universities.FindById(universityId);
            StringBuilder sb = new StringBuilder();
            int studentsCount = students.Models.Where(s => s.University == currentUniversity).Count();

            sb.AppendLine($"*** {currentUniversity.Name} ***");
            sb.AppendLine($"Profile: {currentUniversity.Category}");
            sb.AppendLine($"Students admitted: {studentsCount}");
            sb.AppendLine($"University vacancy: {currentUniversity.Capacity - studentsCount}");

            return sb.ToString().TrimEnd();

        }
    }
}
