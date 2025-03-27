using Common.Application;
using Common.Application.FileUtil.Interfaces;
using CoreModule.Application._Utilities;
using CoreModule.Domain.Teachers.DomainServices;
using CoreModule.Domain.Teachers.Models;
using CoreModule.Domain.Teachers.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace CoreModule.Application.Teachers.Register;

public class RegisterTeacherCommand : IBaseCommand
{
    public IFormFile CvFile { get; set; }
    public string UserName { get; set; }
    public Guid UserId { get; set; }
    public string? Description { get; private set; }
    public string? Biography { get; private set; }
}

public class RegisterTeacherCommandHandler : IBaseCommandHandler<RegisterTeacherCommand>
{
    private readonly ITeacherRepository _repository;
    private readonly ITeacherDomainService _domainService;
    private readonly ILocalFileService _localFileService;
    public RegisterTeacherCommandHandler(ITeacherRepository repository, ITeacherDomainService domainService, ILocalFileService localFileService)
    {
        _repository = repository;
        _domainService = domainService;
        _localFileService = localFileService;
    }

    public async Task<OperationResult> Handle(RegisterTeacherCommand request, CancellationToken cancellationToken)
    {
        var cvFileName = await _localFileService.SaveFileAndGenerateName(request.CvFile, CoreModuleDirectories.CvFileNames);

        var teacher = new Domain.Teachers.Models.Teacher(cvFileName, request.UserName, request.UserId,request.Description,request.Biography, _domainService);
        _repository.Add(teacher);
        await _repository.Save();
        return OperationResult.Success();
    }
}


public class RegisterTeacherCommandValidator : AbstractValidator<RegisterTeacherCommand>
{
    public RegisterTeacherCommandValidator()
    {
        RuleFor(r => r.CvFile)
            .NotEmpty()
            .NotNull();

        RuleFor(r => r.UserName)
            .NotEmpty()
            .NotNull();
    }
}
