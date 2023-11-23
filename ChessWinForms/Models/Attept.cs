using System;
using System.Collections.Generic;
using Sachy_Obrazky.Repository;

namespace Sachy_Obrazky.Models
{
    public class Attept
    {
        public Attept() { }
        public Attept(int idParty)
        {
            Date = DateTime.Now;
            Id = NewId();
            IdParty = idParty;
            Steps = new List<Step>();
        }

        private long NewId()
        {
            var players = new PlayerRepository().GetAll();
            long count = 0;
            foreach (var player in players)
            {
                foreach (var attept in player.AtteptsRestoreChessParty)
                {
                    if (attept.Id >= count)
                    {
                        count = attept.Id + 1;
                    }
                }
            }
            return count;
        }

        public int GetTime()
        {
            double time = 0;
            if (Steps.Count > 0)
            {
                var dateMin = Steps[0].Date;
                DateTime dateMax = Steps[0].Date;
                for (int i = Steps.Count - 1; i >= 0; i--)
                {
                    if (Steps[i].Date < dateMin)
                        continue;
                    else
                    {
                        dateMax = Steps[i].Date;
                        break;
                    }
                }
                var res = dateMax - dateMin;
                time = res.TotalSeconds;
            }

            if (time < Time)
                return Time;
            return Convert.ToInt32(time);
        }
        public int Time { get; set; }
        public DateTime Date { get; set; }
        public long Id { get; set; }
        public int IdParty { get; set; }
        public List<Step> Steps { get; set; }
        public ReasonCompletionAttempt ReasonCompletion { get; set; }

    }
}