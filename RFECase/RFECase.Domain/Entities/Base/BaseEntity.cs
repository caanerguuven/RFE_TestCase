using System;

namespace RFECase.Domain.Base.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate => DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
