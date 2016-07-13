namespace TaskManager.HubService.Corn
{
    /// <summary>
    /// 格式[runonce]
    /// </summary>
    public class RunOnceCorn : SimpleCorn
    {
        public RunOnceCorn(string corn)
            : base(corn)
        {
            Corn = "[simple,,1,,]";
        }
    }
}
