namespace Sachy_Obrazky.Models
{
    public class Party
    {
        public Party(){}
        public Party(string name, string steps)
        {
            Name = name;
            Steps = steps;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Steps { get; set; }
    }
}