using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Survey.Data;
using Survey.Data.Entities;
using Survey.Models.Models.Question;
using Survey.Services.Abstractions;

namespace Survey.Services.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly midTermDbContext _context;
        private readonly IMapper _mapper;

        public QuestionService(Data.midTermDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<QuestionModelBase>> Get()
        {
            var questions = await _context.Questions.ToListAsync();
            return _mapper.Map<IEnumerable<QuestionModelBase>>(questions);
        }

        public async Task<QuestionModelBase> GetById(int id)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == id);

            return _mapper.Map<QuestionModelBase>(question);
        }

        public async Task<IEnumerable<QuestionModelExtended>> GetFull()
        {
            var questions = await _context.Questions.ToListAsync();
            return _mapper.Map<IEnumerable<QuestionModelExtended>>(questions);
        }

        public async Task<QuestionModelBase> Insert(QuestionCreateModel model)
        {
            var entity = _mapper.Map<Question>(model);
            await _context.Questions.AddAsync(entity);
            await SaveAsync();
            return _mapper.Map<QuestionModelBase>(entity);
        }

        public async Task<QuestionModelExtended> Update(QuestionUpdateModel model)
        {
            var entity = _mapper.Map<Question>(model);
            //not async since i am assuming we need the record immediately after
            _context.Questions.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await SaveAsync();
            return _mapper.Map<QuestionModelExtended>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(entity);
            return await SaveAsync() > 0;
        }

        //just saves typing
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
