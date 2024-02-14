using ECommerceApplication.Domain.Common;

namespace ECommerceApplication.Domain.Entities
{
    public class WinningGame
    {
        private WinningGame(Guid userId, int price, DateTime timestamp, List<Discount> discounts, int size)
        {
            WinningGameId = Guid.NewGuid();
            UserId = userId;
            Price = price;
            Timestamp = timestamp;
            Discounts = discounts;
            Size = size;
        }

        public Guid WinningGameId { get; }
        public Guid UserId { get; }
        public int Price { get; }
        public DateTime Timestamp { get; }
        public List<Discount> Discounts { get; set; }
        public int Size { get; set; }

        public static Result<WinningGame> Create(Guid userId, int price, DateTime timestamp, List<Discount> discounts, int size)
        {
            if (userId == default)
            {
                return Result<WinningGame>.Failure("User id is required.");
            }
            if (price == default || price<0)
            {
                return Result<WinningGame>.Failure("Price is required.");
            }
            if (timestamp == default|| timestamp <DateTime.UtcNow)
            {
                return Result<WinningGame>.Failure("Timestamp is required.");
            }
            if (size == default|| size<0)
            {
                return Result<WinningGame>.Failure("Size is required.");
            }
            return Result<WinningGame>.Success(new WinningGame(userId, price, timestamp, discounts, size));
        }


        public void AddDiscount(List<Discount> discounts)
        {
            int len = Discounts.Count;
            int i = 0;
            while (len < Size && i < discounts.Count)
            {
                if (!Discounts.Contains(discounts[i]))
                {
                    Discounts.Add(discounts[i]);
                    len++;
                }
                i++;
            }
        }
        public void RemoveDiscount(Discount discount)
        {
            if(Discounts.Contains(discount))
            {

                Discounts.Remove(discount);
            }
        }

        public void SpinWheel(Reward reward)
        {
            if (reward.RewardValue >= Price)
            {
                Random random = new Random();
                int index = random.Next(0, Discounts.Count);
                reward.DecreaseReward(Price);
                reward.AddDiscount(Discounts[index]);
            }

        }

    }
}
