using System;

namespace Sachy_Obrazky.Models
{
    public class ViewStatistic
    {
        public long IdAttept { get; set; }
        public int IdParty { get; set; }
        public StatusParty StatusParty { get; set; }
        public int CorrectAnsvers { get; set; }
        public int IncorrectAnsvers { get; set; }
        public double Accuracy { get; set; }
        public DateTime DatePassage { get; set; }
        public int PassageTime { get; set; }

        public ViewStatistic(Attept attept)
        {
            IdAttept = attept.Id;
            IdParty = attept.IdParty;
            var reason = attept.ReasonCompletion;
            if (reason == ReasonCompletionAttempt.Win)
                StatusParty = StatusParty.Completed;
            else if (reason == ReasonCompletionAttempt.ExceedingErrorsLimit)
                StatusParty = StatusParty.NotCompleted;
            else
                StatusParty = StatusParty.CompletedAheadSchedule;
            foreach (var step in attept.Steps)
            {
                IncorrectAnsvers += step.Errors;
                if (step.CorrectAnsver != null)
                {
                    CorrectAnsvers++;
                }
            }

            if (StatusParty == StatusParty.CompletedAheadSchedule || (CorrectAnsvers + IncorrectAnsvers)==0)
                Accuracy = 0;
            else
            {
                Accuracy = Math.Round((Convert.ToDouble(CorrectAnsvers) / Convert.ToDouble(CorrectAnsvers + IncorrectAnsvers))*100,3);
            }
            DatePassage = attept.Date;
            PassageTime = attept.GetTime();
        }
    }
}