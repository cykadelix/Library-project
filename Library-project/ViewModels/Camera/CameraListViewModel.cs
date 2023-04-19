namespace Library_project.ViewModels.Camera
{
    public class CameraListViewModel
    {
        public int NumAvailable { get; set; } = 0;
        public List<CameraViewModel>? Cameras {  get; set; }
    }
}
