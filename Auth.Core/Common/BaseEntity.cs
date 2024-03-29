﻿namespace Auth.Core.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public bool? IsDeleted { get; set; }
    public DateTimeOffset? CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
    public DateTimeOffset? DeletedDate { get; set; }

}

