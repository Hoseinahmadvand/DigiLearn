using Common.Application;
using CoreModule.Application.Teachers.AcceptRequest;
using CoreModule.Domain.Teachers.Events;
using CoreModule.Domain.Teachers.Repository;
using MediatR;

namespace CoreModule.Application.Teachers.RejectRequest;

public class RejectTeacherRequestCommand : IBaseCommand
{
    public Guid TeacherId { get; set; }
    public string Description { get; set; }
}

public class RejectTeacherRequestCommandHandler : IBaseCommandHandler<RejectTeacherRequestCommand>
{
    private readonly ITeacherRepository _repository;
    private IMediator _mediator;
    public RejectTeacherRequestCommandHandler(ITeacherRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }


    public async Task<OperationResult> Handle(RejectTeacherRequestCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _repository.GetTracking(request.TeacherId);
        if (teacher == null)
            return OperationResult.NotFound();



        _repository.Delete(teacher);
        //Send Event
        await _repository.Save();
        await _mediator.Publish(new RejectTeacherRequestEvent()
        {
            Description = request.Description,
            UserId = teacher.UserId
        }, cancellationToken);
        return OperationResult.Success();
    }
}