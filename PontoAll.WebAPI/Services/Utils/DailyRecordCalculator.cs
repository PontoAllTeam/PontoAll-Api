using PontoAll.WebAPI.Objects.Models;

namespace PontoAll.WebAPI.Services.Utils;

public static class DailyRecordCalculator
{
    public static void CalculateValues(DailyRecord dailyRecord, List<TimeRecord> timeRecords, WorkSchedule workSchedule)
    {
        var expectedTimes = GetExpectedTimes(workSchedule);
        dailyRecord.ExpectedHours = CalculateExpectedHours(expectedTimes);
        
        var actualTimes = timeRecords.OrderBy(tr => tr.Time).Select(tr => tr.Time).ToList();
        dailyRecord.TotalWorkedHours = CalculateWorkedHours(actualTimes);
        
        dailyRecord.IsAbsent = actualTimes.Count == 0;
        
        if (dailyRecord.TotalWorkedHours > dailyRecord.ExpectedHours)
        {
            dailyRecord.OvertimeHours = dailyRecord.TotalWorkedHours - dailyRecord.ExpectedHours;
            dailyRecord.MissingHours = 0;
        }
        else
        {
            dailyRecord.OvertimeHours = 0;
            dailyRecord.MissingHours = dailyRecord.ExpectedHours - dailyRecord.TotalWorkedHours;
        }
    }

    private static List<TimeOnly> GetExpectedTimes(WorkSchedule workSchedule)
    {
        var times = new List<TimeOnly?> 
        { 
            workSchedule.MarkTime1, workSchedule.MarkTime2, workSchedule.MarkTime3, 
            workSchedule.MarkTime4, workSchedule.MarkTime5, workSchedule.MarkTime6,
            workSchedule.MarkTime7, workSchedule.MarkTime8, workSchedule.MarkTime9, 
            workSchedule.MarkTime10 
        };
        
        return times.Where(t => t.HasValue).Select(t => t.Value).OrderBy(t => t).ToList();
    }

    private static double CalculateExpectedHours(List<TimeOnly> expectedTimes)
    {
        double totalHours = 0;
        for (int i = 0; i < expectedTimes.Count; i += 2)
        {
            if (i + 1 < expectedTimes.Count)
            {
                var start = expectedTimes[i];
                var end = expectedTimes[i + 1];
                totalHours += (end - start).TotalHours;
            }
        }
        return totalHours;
    }

    private static double CalculateWorkedHours(List<TimeOnly> actualTimes)
    {
        double totalHours = 0;
        for (int i = 0; i < actualTimes.Count; i += 2)
        {
            if (i + 1 < actualTimes.Count)
            {
                var start = actualTimes[i];
                var end = actualTimes[i + 1];
                totalHours += (end - start).TotalHours;
            }
        }
        return totalHours;
    }
}