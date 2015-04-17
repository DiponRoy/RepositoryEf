using System;
using Db.DbModel.Enum;
using Db.DbModel.Interface;

namespace Db.DbModel
{
    public class User : IDbEntity
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public EntityStatusEnum Status { get; set; }
        public DateTime? AddedDateTime { get; set; }
        public long? AddedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public long? UpdatedBy { get; set; }

        public virtual User AddedByUser { get; set; }
        public virtual User UpdatedByUser { get; set; }
    }
}