namespace BakeryProjectAPI.Utility
{
    public sealed class ApiResponse<T> where T : class
    {
        public T Data { get; set; }
        public List<T> ListOfData { get; set; }
        public bool IsSuccessful { get; set; }
        public string  Error { get; set; }
        public string  Message { get; set; }
    }
}
