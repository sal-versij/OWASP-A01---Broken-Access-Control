using Microsoft.EntityFrameworkCore;

namespace OwaspA01.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public abstract class RelationshipAttribute : Attribute
{
    public DeleteBehavior OnDelete { get; }

    protected RelationshipAttribute(DeleteBehavior onDelete = DeleteBehavior.NoAction)
    {
        OnDelete = onDelete;
    }
}