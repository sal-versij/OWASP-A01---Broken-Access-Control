using System.Linq.Expressions;

namespace Undefended.Services.Interfaces;

public interface IExpressionProvider<TSelf, TSource> where TSelf : IExpressionProvider<TSelf, TSource> {
	public static abstract Expression<Func<TSource, TSelf>> Expression { get; }
}