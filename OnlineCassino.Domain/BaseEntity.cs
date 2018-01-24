using OnlineCassino.Domain.Interfaces;

namespace OnlineCassino.Domain
{
    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}