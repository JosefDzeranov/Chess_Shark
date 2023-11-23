using System.Collections.Generic;
using System;

namespace Sachy_Obrazky.Models
{
    public class Player
    {
        public Player()
        {
        }
        public Player(string name, string family)
        {
            Id = Guid.NewGuid();
            Name = name;
            Family = family;
            AtteptsRestoreChessParty = new List<Attept>();
            RecordRestoringPosition = 0;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public List<Attept> AtteptsRestoreChessParty { get; set; }
        public int RecordRestoringPosition { get; set; }
    }
}
