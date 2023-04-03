using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Undefended.Attributes;

namespace Undefended.Extensions;

public static class ModelBuilderExtensions {
	public static void ConfigureRelationships(this ModelBuilder builder) {
		foreach(var entityType in builder.Model.GetEntityTypes())
		foreach(var property in entityType.GetProperties()) {
			var attribute = property.PropertyInfo?.GetCustomAttribute<RelationshipAttribute>(true);

			switch (attribute) {
			case null: continue;
			case OneToManyAttribute oneToManyAttribute:
				builder.Entity(entityType.Name)
					   .HasOne(property.Name)
					   .WithMany()
					   .OnDelete(oneToManyAttribute.OnDelete);
				break;
			case ManyToOneAttribute manyToOneAttribute:
				builder.Entity(entityType.Name)
					   .HasMany(property.Name)
					   .WithOne()
					   .OnDelete(manyToOneAttribute.OnDelete);
				break;
			default: throw new ArgumentOutOfRangeException(nameof(attribute));
			}
		}
	}
}
