using Microsoft.EntityFrameworkCore;

namespace RiverBooks.OrderProcessing.Data;

internal class EfOrderRepository(OrderProcessingDbContext dbContext) : IOrderRepository
{
  public async Task<List<Order>> ListAsync() => await dbContext.Orders.ToListAsync();
  public async Task AddAsync(Order order) => await dbContext.Orders.AddAsync(order);
  public Task SaveChangesAsync() => dbContext.SaveChangesAsync();
}
