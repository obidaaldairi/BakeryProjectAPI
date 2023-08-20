namespace Domin.Entity
{
    public class WebConfiguration:BaseEntity
    {
        public string ConfigKey { get; set; }
        public string ConfigValue { get; set; }
        public string Description { get; set; }=string.Empty;
    }
}
