using BowlingGame.Models;

namespace BowlingGame.Respository
{
    public interface IBowlingGameService
    {
        BowlingGameModel PlayRadom();
        BowlingGameModel PlayStrike();
        BowlingGameModel PlaySpare();
        BowlingGameModel PlayTenth();
        BowlingGameModel PlayPerfect();
        void Roll(int pins);
        int Score();
    }
}