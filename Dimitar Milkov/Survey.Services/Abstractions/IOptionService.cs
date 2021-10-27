using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Survey.Models.Models.Option;


namespace Survey.Services.Abstractions
{
    public interface IOptionService
    {
        Task<OptionModel> GetById(int id);

        Task<IEnumerable<OptionModel>> Get();

        Task<IEnumerable<OptionModel>> GetByQuestionId(int id);

        Task<OptionModel> Insert(OptionCreateModel model);

        Task<OptionModel> Update(OptionUpdateModel model);

        Task<bool> Delete(int id);
    }
}
