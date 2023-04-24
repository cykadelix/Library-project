namespace Library_project.ViewModels.Hold
{
    public class CreateHoldVM
    {
        public int mediaid { get; set; }
        public int userid{ get; set; }
        public string title { get; set; }
        public string date { get; set; } = "";
        public bool available{ get; set; }
        

    }
}
