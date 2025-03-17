﻿namespace IdentecSolutions.Domain.Entities
{
    public class AuditRecord
    {
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
