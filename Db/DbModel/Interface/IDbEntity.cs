using System;
using Db.DbModel.Enum;

namespace Db.DbModel.Interface
{
    public interface IDbEntity
    {
        long Id { get; set; }
        EntityStatusEnum Status { get; set; }
        DateTime? AddedDateTime { get; set; }
        long? AddedBy { get; set; }
        DateTime? UpdatedDateTime { get; set; }
        long? UpdatedBy { get; set; }

        User AddedByUser { get; set; }
        User UpdatedByUser { get; set; }
    }
}
