using Microsoft.EntityFrameworkCore;

namespace Defended.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public abstract class RelationshipAttribute : Attribute
{
    public DeleteBehavior OnDelete { get; }

    protected RelationshipAttribute(DeleteBehavior onDelete = DeleteBehavior.NoAction)
    {
        OnDelete = onDelete;
    }
}