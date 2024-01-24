using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OPSExporter.Data.Entity;

namespace OPSExporter.Data.Repository;

public class NodesRepository {
    private readonly ApplicationContext _context = new ApplicationContext();
    
    public async Task<List<IGrouping<string?, Node>>> GetAll() {
        await using IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();

        try {
            List<IGrouping<string?, Node>> result = await _context.Nodes.GroupBy(node => node.DeviceName).ToListAsync();

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return result;
        }
        catch (Exception) {
            await transaction.RollbackAsync();
        } finally {
            await transaction.DisposeAsync();
        }

        return [];
    }
}