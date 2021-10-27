using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Survey.Data;
using Survey.Data.Entities;
using Survey.Models.Models.Option;
using Survey.Services.Abstractions;

namespace Survey.Services.Services
{
    public class OptionService : IOptionService
    {
        private readonly midTermDbContext _context;
        private readonly IMapper _mapper;
        public OptionService(Data.midTermDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<OptionModel> GetById(int id)
        {
            var option = await _context.Options.Include(o => o.Question).FirstOrDefaultAsync(o => o.Id == id);
            return _mapper.Map<OptionModel>(option);
        }

        public async Task<IEnumerable<OptionModel>> GetByQuestionId(int id)
        {
            var options = await _context.Options.Include(o => o.Question).Where(q => q.Id == id).ToListAsync();
            return _mapper.Map<IEnumerable<OptionModel>>(options);
        }

        public async Task<IEnumerable<OptionModel>> Get()
        {
            var options = await _context.Options.ToListAsync();
            return _mapper.Map<IEnumerable<OptionModel>>(options);
        }

        public async Task<OptionModel> Insert(OptionCreateModel model)
        {
            var entity = _mapper.Map<Option>(model);
            await _context.Options.AddAsync(entity);
            await SaveAsync();
            return _mapper.Map<OptionModel>(entity);
        }

        public async Task<OptionModel> Update(OptionUpdateModel model)
        {
            var entity = _mapper.Map<Option>(model);
            //not async since i am assuming we need the record immediately after
            _context.Options.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await SaveAsync();
            return _mapper.Map<OptionModel>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Options.FindAsync(id);
            _context.Options.Remove(entity);
            return await SaveAsync() > 0;
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
