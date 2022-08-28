using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Enigmachan.Models;
using enigmachan_backend.Models;
using enigmachan_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace enigmachan_backend.Services
{
    public class BoardService: IBoardService
    {
        private readonly ThreadContext _context;
        private readonly IMapper _mapper;

        public BoardService(ThreadContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<bool> postThread(postThreadRequest request)
        {
            var post_id = _context.Posts.ToList().Select(t => t.key).LastOrDefault(1);
            if (post_id != 1) post_id++; 
            var post = new Post(post_id, post_id);
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            var thread = _mapper.Map<postThreadRequest, Enigmachan.Models.Thread>(request);
            var id = _context.Posts.Select(t => t.post_id).ToList().Last();
            thread.post_id = id;
            await _context.Threads.AddAsync(thread);
            await _context.Replies.AddAsync(_mapper.Map<Enigmachan.Models.Thread, Reply>(thread));
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Enigmachan.Models.Thread>> getAllThreads()
        {
            return _context.Threads.ToList();
        }

        public async Task<IEnumerable<Reply>> getAllRepliesToThread(long request)
        {
            return _context.Replies.ToList().Where(t => (t.mainPostId == request) && (t.post_id!=t.mainPostId));
        }

        public async Task<Enigmachan.Models.Thread> getThreadById(long req)
        {
            var thread = _context.Threads.ToList().FirstOrDefault(t => t.post_id == req, null);
            Console.WriteLine(thread.post_id);
            return thread;
        }

        public async Task<bool> postReply(postReplyRequest request)
        {
            var post_id = _context.Posts.ToList().Select(t => t.key).LastOrDefault(0) + 1;
            var post = new Post(post_id, request.mainPostId);
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            
            var id = _context.Posts.ToList().Select(t => t.post_id).Last();
            var rep = _mapper.Map<postReplyRequest, Reply>(request);
            rep.post_id = id;
            _context.Replies.Add(rep);
            if (request.reply_to != null)
            {
                foreach (var reply in request.reply_to)
                {
                    _context.Replies.Where(t => t.post_id==Int64.Parse(reply)).ToList().ForEach(t =>
                    {
                        if (t.replies == null) t.replies = new List<string>();
                        t.replies.Add(reply);
                    });
                    try
                    {
                        _context.Threads.Where(t => t.post_id==Int64.Parse(reply)).ToList().ForEach(t =>
                        {
                            if (t.replies == null) t.replies = new List<string>();
                            t.replies.Add(reply);
                        });
                    }
                    catch (System.InvalidOperationException ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }

            _context.Threads.First(t => t.post_id==request.mainPostId).bumps++;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> deleteThread(long id)
        {
            IEnumerable<Reply> repliesToRemove = _context.Replies.Where(e => e.mainPostId == id);
            IEnumerable<Enigmachan.Models.Thread> threadsToRemove = _context.Threads.Where(e => e.post_id == id);
            _context.Replies.RemoveRange(repliesToRemove);
            _context.Threads.RemoveRange(threadsToRemove);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}