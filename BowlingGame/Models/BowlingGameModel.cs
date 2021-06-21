using System.ComponentModel;

namespace BowlingGame.Models
{
    public class BowlingGameModel
    {
        public string[,] rollArray { get; set; } = new string[10, 2];
        [DefaultValue("")]
        public string bonus { get; set; } 
        public int score { get; set; } = 0;
    }
}
