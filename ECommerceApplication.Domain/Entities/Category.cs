using ECommerceApplication.Domain.Common;

namespace ECommerceApplication.Domain.Entities
{
    public class Category : AuditableEntity
    {
        private Category(string categoryName)
        {
            CategoryId = Guid.NewGuid();
            CategoryName = categoryName;
        }

        public Guid CategoryId { get; private set; }
        public string CategoryName { get; private set; } = string.Empty;

        public static Result<Category> Create(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return Result<Category>.Failure("Category Name is required.");
            }
            return Result<Category>.Success(new Category(categoryName));
        }

        public Result<Category> Update(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                return Result<Category>.Failure("Invalid input for updating profile.");
            }
            CategoryName = categoryName;

            return Result<Category>.Success(this);
        }

    }
}
