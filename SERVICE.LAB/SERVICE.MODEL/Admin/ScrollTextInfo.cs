namespace Service.Model.Admin
{   
    public class RequestDataScrollText
    {
        public int venueNo { get; set; }
        public int venueBranchNo { get; set; }
        public int userNo { get; set; }
    }
    public class ResponseDataScrollText
    {
        public string textInformation { get; set; }
    }
}
