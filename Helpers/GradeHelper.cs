namespace KSI_Project.Helpers
{
    public static class GradeHelper
    {
        public static double GetGradePoint(string grade)
        {
            return grade.ToUpper() switch
            {
                "O" => 10,
                "A+" => 9,
                "A" => 8,
                "B+" => 7,
                "B" => 6,
                "C" => 5,
                "RA" => 0,
                _ => 0
            };
        }
    }
}
