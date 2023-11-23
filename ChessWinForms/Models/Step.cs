using System;
using System.Collections.Generic;

namespace Sachy_Obrazky.Models
{
    public class Step
    {
        public Step(){}
        public int Errors { get; set; }
        public DateTime Date { get; set; }
        public int[] CorrectAnsver { get; set; }
        public List<int[]> IncorrectAnsvers { get; set; }

        public void AddIncorrectAnsver(int[] incorrectAnsver)
        {
            Date = DateTime.Now;
            if (IncorrectAnsvers == null)
                IncorrectAnsvers = new List<int[]>();
            IncorrectAnsvers.Add(incorrectAnsver);
            Errors++;
        }

        public void AddCorrectAnsver(int[] correctAnsver)
        {
            Date = DateTime.Now;
            CorrectAnsver = correctAnsver;
        }
    }
}