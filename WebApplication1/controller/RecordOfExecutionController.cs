using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.model;
using WebApplication1.repository;

namespace WebApplication1.controller;


[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RecordOfExecutionController : ControllerBase
{
    private readonly IRecordOfExecutionRepository _recordOfExecutionRepository;

    public RecordOfExecutionController(IRecordOfExecutionRepository recordOfExecutionRepository)
    {
        _recordOfExecutionRepository = recordOfExecutionRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecordOfExecution>>> GetAllRecord()
    {
        var users = await _recordOfExecutionRepository.GetAllRecordsOfExecution();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RecordOfExecution>> GetRecordById(Guid id)
    {
        var record = await _recordOfExecutionRepository.GetRecordOfExecutionById(id);
        if (record == null)
        {
            return NotFound();
        }

        return Ok(record);
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateRecord(RecordOfExecution record)
    {
        var createdRecord = await _recordOfExecutionRepository.CreateRecordOfExecution(record);
        return CreatedAtAction(nameof(GetRecordById), new { id = createdRecord.Id }, createdRecord);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRecord(Guid id, RecordOfExecution record)
    {
        if (id != record.Id)
        {
            return BadRequest();
        }

        await _recordOfExecutionRepository.UpdateRecordOfExecution(record);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult>?DeleteRecord(Guid id)
    {
        var record = await _recordOfExecutionRepository.GetRecordOfExecutionById(id);
        if (record == null)
        {
            return NotFound();
        }

        await _recordOfExecutionRepository.DeleteRecordOfExecution(id);
        return NoContent();
    }
}