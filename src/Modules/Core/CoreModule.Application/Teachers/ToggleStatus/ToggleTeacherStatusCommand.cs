using Common.Application;
using CoreModule.Domain.Teachers.Repository;

namespace CoreModule.Application.Teachers.ToggleStatus;

public record ToggleTeacherStatusCommand(Guid TeacherId) : IBaseCommand;



class ToggleTeacherStatusCommandHandler : IBaseCommandHandler<ToggleTeacherStatusCommand>
{
    private readonly ITeacherRepository _repository;

    public ToggleTeacherStatusCommandHandler(ITeacherRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(ToggleTeacherStatusCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _repository.GetTracking(request.TeacherId);
        if (teacher == null)
        {
            return OperationResult.NotFound();
        }

        teacher.ToggleStatus();
        await _repository.Save();
        return OperationResult.Success();
    }
}