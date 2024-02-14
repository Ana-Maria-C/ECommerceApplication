using ECommerceApplication.Domain.Common;

namespace ECommerceApplication.Domain.Entities
{
    public class Product : AuditableEntity
    {
        private Product(Guid companyId, string productName, decimal price)
        {
            ProductId = Guid.NewGuid();
            CompanyId = companyId;
            ProductName = productName;
            Price = price;
            Reviews = new List<Review>();
        }
        public Guid ProductId { get; private set; }
        public Guid CompanyId { get; private set; }
        public string ProductName { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public string? Description { get; private set; } 
        public string? ImageUrl { get; private set; }
        public List<Review> Reviews { get; private set; }

        public static Result<Product> Create(Guid companyId, string productName, decimal price)
        {
            if (companyId == Guid.Empty)
            {
                return Result<Product>.Failure("Company Id required.");
            }

            if (string.IsNullOrWhiteSpace(productName))
            {
                return Result<Product>.Failure("Product Name is required.");
            }
            if (price <= 0)
            {
                return Result<Product>.Failure("Price must be greater than zero.");
            }
            return Result<Product>.Success(new Product(companyId, productName, price));
        }
        public Guid CategoryId { get; private set; }
        public Category Category { get; private set; }

        public void AttachDescription(string description)
        {
            if (!string.IsNullOrWhiteSpace(description))
            {
                Description = description;
            }
        }

        public void AttachImageUrl(string imageUrl)
        {
            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                ImageUrl = imageUrl;
            }
        }

        public void AttachCategory(Category category)
        {
            Category = category;
            CategoryId = category.CategoryId;
        }

        public void AddReview(Review review)
        {
            Reviews.Add(review);
        }

        public void Update(string productName, decimal price, string? description, string? imageUrl, Guid categoryId)
        {
            ProductName = productName;
            Price = price;
            Description = description;
            ImageUrl = imageUrl;
            CategoryId = categoryId;
        }

        /*public void RemoveReview(Guid reviewId)
        {
            if (Reviews == null || !Reviews.Any())
            {
                throw new InvalidOperationException("No reviews available for this product.");
            }

            var reviewToRemove = Reviews.FirstOrDefault(r => r.ReviewId == reviewId);

            if (reviewToRemove != null)
            {
                Reviews.Remove(reviewToRemove);
            }
            else
            {
                throw new InvalidOperationException("Review not found for this product.");
            }
        }*/

    }
}