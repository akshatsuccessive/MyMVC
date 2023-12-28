namespace MyFirstMVCCore.Models
{
    public class UpdateStudentViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public string City { get; set; }
    }
}
