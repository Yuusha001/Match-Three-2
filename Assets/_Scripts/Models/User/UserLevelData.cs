namespace MatchThree
{
    [System.Serializable]
    public class UserLevelData
    {
        public int id;
        public ELevelState levelState;
        public int numberOfStars = 0;
        public int highScore;

        public UserLevelData(int _level)
        {
            this.id = _level;
            this.levelState = 0;
            this.numberOfStars = 0;
            this.highScore = 0;
        }

        public void Save(int _numberOfStars, int highScore)
        {
            this.numberOfStars = _numberOfStars;
            this.highScore = highScore;
        }

        public bool isUnlocked()
        {
            return this.levelState != ELevelState.Locked;
        }

        public bool isPlayed()
        {
            return this.levelState == ELevelState.Played;
        }

        public void UnlockLevel()
        {
            this.levelState = ELevelState.Unlocked;
        }

        public void PlayedLevel()
        {
            this.levelState = ELevelState.Played;
        }
    }

    public enum ELevelState
    {
        Locked = 0, Unlocked = 1, Played = 2
    }
}
