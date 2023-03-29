﻿using Microsoft.EntityFrameworkCore;

namespace Defended.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public abstract class RelationshipAttribute : Attribute {
	public DeleteBehavior OnDelete { get; set; } = DeleteBehavior.NoAction;
}
