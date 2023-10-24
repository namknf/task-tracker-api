using Microsoft.EntityFrameworkCore;
using TaskTracker.Contract;
using TaskTracker.Entities.Data;
using TaskTracker.Entities.Models;

namespace TaskTracker.Repository
{
    public class CodeAttemptRepository : RepositoryBase<CodeAttempt>, ICodeAttemptRepository
    {
        public CodeAttemptRepository(DataContext dataContext) : base(dataContext) { }

        public void CreateCodeAttempt(CodeAttempt attempt) => Create(attempt);

        public async Task<CodeAttempt?> GetCodeAttemptAsync(string phoneNumber, string code, bool trackChanges)
        {
            return await FindByCondition(e => e.PhoneNumber.Equals(phoneNumber) && e.Code.Equals(code), trackChanges).FirstOrDefaultAsync();
        }
    }
}
