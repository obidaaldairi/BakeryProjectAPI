namespace Domin.Entity
{
    public abstract class BaseEntity
    {
        public Guid ID { get; set; }
        public bool IsDeleted { get; set; }
    }
}
