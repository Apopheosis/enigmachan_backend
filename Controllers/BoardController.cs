using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enigmachan.Models;
using enigmachan_backend.Models;
using enigmachan_backend.Services;
using enigmachan_backend.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

[ApiController]
[Route("/board")]
[EnableCors()]
public class BoardController : ControllerBase
{
    private readonly IBoardService _boardService;
    
    public BoardController(IBoardService service)
    {
        _boardService = service;
    }
    [HttpPost("addThread")]
    public async Task<ActionResult> postThread(postThreadRequest req)
    {
        await _boardService.postThread(req);
        return new OkResult();
    }

    [HttpPost("addReply")]
    public async Task<ActionResult> postReply(postReplyRequest req)
    {
        await _boardService.postReply(req);
        return new OkResult();
    }

    [HttpGet("{id}")]
    public async Task<Enigmachan.Models.Thread> getThread(long id)
    {
        return await _boardService.getThreadById(id);
    }

    [HttpPost("getAllThreads")]
    public async Task<IEnumerable<Enigmachan.Models.Thread>> getAllThreads()
    {
        return await _boardService.getAllThreads();
    }

    [HttpPost("getAllRepliesToThread/{id}")]
    public async Task<IEnumerable<Reply>> getAllRepliesToThread(long id)
    {
        return await _boardService.getAllRepliesToThread(id);
    }

    [HttpDelete("deleteThreadById/{id}")]
    public async Task<IActionResult> deleteThreadById(long id)
    {
        await _boardService.deleteThread(id);
        return new OkResult();
    }

}
