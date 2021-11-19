using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity {get; set;}
        }

        public class CommandHandler : IRequestHandler<Command>
        {
            private readonly DataContext _dataContext;

            public CommandHandler(DataContext dataContext)
            {
                _dataContext = dataContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _dataContext.Activities.FindAsync(request.Activity.Id);

                activity.Title = request.Activity.Title ?? activity.Title;

                await _dataContext.SaveChangesAsync();

                return Unit.Value;
            }
        }

    }
}