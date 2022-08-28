using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Enigmachan.Models;
using enigmachan_backend.Models;

namespace enigmachan_backend.Services.Interfaces
{
    public interface IBoardService
    {
        public Task<bool> postThread(postThreadRequest request);
        public Task<IEnumerable<Enigmachan.Models.Thread>> getAllThreads();
        public Task<IEnumerable<Reply>> getAllRepliesToThread(long req);
        public Task<Enigmachan.Models.Thread> getThreadById(long req);
        public Task<bool> postReply(postReplyRequest request);
        public Task<bool> deleteThread(long id);
    }
}