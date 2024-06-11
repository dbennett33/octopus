namespace Octopus.EF.Data.Entities
{
    /// <summary>
    /// Represents a statistics period.
    /// </summary>
    public class StatsPeriod
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Period { get; set; } = string.Empty;
        public int Percentage { get; set; }

        public int Total { get; set; }
    }

    public class StatsPeriodRedCards : StatsPeriod
    {
        
    }

    public class StatsPeriodYellowCards : StatsPeriod
    {
    }

    public class StatsPeriodGoalsScored : StatsPeriod
    {
    }

    public class StatsPeriodGoalsConceded : StatsPeriod
    {
    }
}