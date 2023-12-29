namespace SharedLibrary.Requests
{
    public class LevelChangeRequest
    {
        public int Num { get; set; }
        public float Time { get; set; }
        public string Way { get; set; }

        public LevelChangeRequest()
        {
            Num = default;
            Time = default;
            Way = default;
        }

        public LevelChangeRequest(int num, float time, string way)
        {
            Num = num;
            Time = time;
            Way = way;
        }
    }
}