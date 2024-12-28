using WebApplication1.model;

namespace WebApplication1.repository;

public interface IRecordOfExecutionRepository
{
    public Task<IEnumerable<RecordOfExecution>> GetAllRecordsOfExecution();
    
    public Task <RecordOfExecution?> GetRecordOfExecutionById(Guid id);
    
    public Task <RecordOfExecution> CreateRecordOfExecution(RecordOfExecution recordOfExecution);
    
    public Task UpdateRecordOfExecution(RecordOfExecution recordOfExecution);
    
    public Task DeleteRecordOfExecution(Guid id);
    
}