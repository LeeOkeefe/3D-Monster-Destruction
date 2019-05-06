using System;

namespace Highscores
{
    [Serializable]
    public class HighscoresJsonBinding
    {
        // Public fields so they can be serialized by Json
        //
        public float Highscore1;
        public float Highscore2;
        public float Highscore3;
    }
}
