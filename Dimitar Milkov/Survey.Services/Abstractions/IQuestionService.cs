using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Survey.Models.Models.Question;

namespace Survey.Services.Abstractions
{
    public interface IQuestionService
    {
        Task<QuestionModelBase> GetById(int id);

        Task<IEnumerable<QuestionModelBase>> Get();

        Task<IEnumerable<QuestionModelExtended>> GetFull();

        Task<QuestionModelBase> Insert(QuestionCreateModel model);

        Task<QuestionModelExtended> Update(QuestionUpdateModel model);

        Task<bool> Delete(int id);
    }
}
