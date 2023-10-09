namespace BusinessPortal.Models.DTO_s
{
    public class RequestUpdateDTO
    {

        public int Id { get; set; }
        public int RequestTypeId { get; set; }
        public DateTime Period { get; set; }
        public int PersonalId { get; set; }
    }
}
