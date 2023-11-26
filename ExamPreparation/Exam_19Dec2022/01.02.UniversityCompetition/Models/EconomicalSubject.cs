namespace UniversityCompetition.Models
{
    public class EconomicalSubject : Subject
    {
        private const double TechnicalSubjectRate = 1.0d;
        public EconomicalSubject(int subjectId, string subjectName)
            : base(subjectId, subjectName, TechnicalSubjectRate)
        {
        }
    }
}
