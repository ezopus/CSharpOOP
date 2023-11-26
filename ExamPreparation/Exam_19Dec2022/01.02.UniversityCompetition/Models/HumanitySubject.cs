namespace UniversityCompetition.Models
{
    public class HumanitySubject : Subject
    {
        private const double TechnicalSubjectRate = 1.15d;
        public HumanitySubject(int subjectId, string subjectName)
            : base(subjectId, subjectName, TechnicalSubjectRate)
        {
        }
    }
}
