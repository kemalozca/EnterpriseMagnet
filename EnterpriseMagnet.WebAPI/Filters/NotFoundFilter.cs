using EnterpriseMagnet.Entities.Concrete;
using EnterpriseMagnet.Service.Abstract.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnterpriseMagnet.WebAPI.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {

        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue;
            //   var anyEntity = await _service.AnyAsync(x => x.Id == id);
         


            throw new NotImplementedException();
        }
    }
}
