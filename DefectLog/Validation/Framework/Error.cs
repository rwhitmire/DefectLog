namespace DefectLog.Validation.Framework
{
    public class Error
    {
        public Error()
        {
            PropertyName = "";
        }

        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}