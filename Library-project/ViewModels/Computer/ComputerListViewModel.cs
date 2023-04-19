namespace Library_project.ViewModels.Computer
{
    public class ComputerListViewModel
    {
        public int NumAvailable { get; set; } = 0;
        public List<ComputerViewModel>? Computers { get; set; }
    }
}
