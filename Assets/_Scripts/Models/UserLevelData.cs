namespace MatchThree
{
    [System.Serializable]
    public class UserLevelData
    {
        public int id;
        public bool unlocked;
        public int numberOfStars = 0;
        public int highScore;

        public UserLevelData(int _level)
        {
            this.id = _level;
            this.unlocked = false;
            this.numberOfStars = 0;
            this.highScore = 0;
        }

        public void Save(int _numberOfStars, int highScore)
        {
            this.numberOfStars = _numberOfStars;
            this.highScore = highScore;
        }

        public void Unlock()
        {
            this.unlocked = true;
        }
    }
}
