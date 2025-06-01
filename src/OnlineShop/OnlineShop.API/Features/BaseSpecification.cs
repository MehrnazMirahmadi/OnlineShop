using System.Linq.Expressions;

namespace OnlineShop.API.Features;

public abstract class BaseSpecification<TEntity>
{
    public bool IsPaginationEnabled { get; private set; }
    public int Skip { get; private set; }
    public int Take { get; private set; }
    public Expression<Func<TEntity, bool>>? Criteria { get; private set; }
    public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }
    public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }

    protected void AddCriteria(Expression<Func<TEntity, bool>> criteria)
    {
        if (Criteria is null)
        {
            Criteria = criteria;
        }
        else
        {
            var oldParameter = Criteria.Parameters[0];
            var newParameter = criteria.Parameters[0];
            var visitor = new ReplaceParameterVisitor(oldParameter, newParameter);

            var oldExpression = Criteria.Body;
            var newExpression = visitor.Visit(criteria.Body);
            var combined = Expression.AndAlso(oldExpression, newExpression);

            Criteria = Expression.Lambda<Func<TEntity, bool>>(combined, oldParameter);
        }
    }

    protected void AddOrderBy(Expression<Func<TEntity, object>> expression, OrderType orderType)
    {
        if (orderType == OrderType.Ascending)
        {
            OrderByExpression = expression;
        }
        else
        {
            OrderByDescendingExpression = expression;
        }
    }
    protected void AddPagination(int pageSize, int pageNumber)
    {
        IsPaginationEnabled = true;
        Skip = pageSize * (pageNumber - 1);
        Take = pageSize;
    }

    private class ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter) : ExpressionVisitor
    {
        protected override Expression VisitParameter(ParameterExpression parameter)
        {
            if (ReferenceEquals(parameter, newParameter))
            {
                return oldParameter;
            }

            return base.VisitParameter(parameter);
        }
    }
}
