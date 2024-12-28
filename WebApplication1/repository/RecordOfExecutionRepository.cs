using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.model;

namespace WebApplication1.repository;

public class RecordOfExecutionRepository : IRecordOfExecutionRepository
{
    ApplicationDbContext db;

    public RecordOfExecutionRepository(ApplicationDbContext db)
    {
        this.db = db;
    }

    public async Task<IEnumerable<RecordOfExecution>> GetAllRecordsOfExecution() =>
        await db.Record.Include(e => e.Habits).ToListAsync();


    public async Task<RecordOfExecution?> GetRecordOfExecutionById(Guid id) =>
        await db.Record.Include(e => e.Habits).FirstOrDefaultAsync(e => e.Id == id);


    public async Task<RecordOfExecution> CreateRecordOfExecution(RecordOfExecution recordOfExecution)
    {
        await db.Record.AddAsync(recordOfExecution);
        await db.SaveChangesAsync();
        return recordOfExecution;
    }


    public async Task UpdateRecordOfExecution(RecordOfExecution recordOfExecution)
    {
        db.Record.Update(recordOfExecution);
        await db.SaveChangesAsync();
    }

    public async Task DeleteRecordOfExecution(Guid id)
    {
        var recordOfExecution = await db.Record
            .Include(e => e.Habits)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (recordOfExecution == null)
        {
            throw new KeyNotFoundException("Record not found.");
        }

        db.Record.Remove(recordOfExecution);
        await db.SaveChangesAsync();
    }
}