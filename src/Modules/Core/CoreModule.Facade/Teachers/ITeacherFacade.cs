﻿using Common.Application;
using CoreModule.Application.Teachers.AcceptRequest;
using CoreModule.Application.Teachers.Register;
using CoreModule.Application.Teachers.RejectRequest;
using CoreModule.Application.Teachers.ToggleStatus;
using CoreModule.Query.Teachers._DTOs;
using CoreModule.Query.Teachers.GetById;
using CoreModule.Query.Teachers.GetByUserId;
using CoreModule.Query.Teachers.GetByUserName;
using CoreModule.Query.Teachers.GetList;
using MediatR;                

namespace CoreModule.Facade.Teachers;

public interface ITeacherFacade
{
    Task<OperationResult> Register(RegisterTeacherCommand command);
    Task<OperationResult> AcceptRequest(AcceptTeacherRequestCommand command);
    Task<OperationResult> RejectRequest(RejectTeacherRequestCommand command);
    Task<OperationResult> ToggleStatus(Guid teacherId);



    Task<TeacherDto?> GetById(Guid id);
    Task<TeacherDto?> GetByUserName(string username);
    Task<TeacherDto?> GetByUserId(Guid userId);
    Task<List<TeacherDto>> GetList();
}
class TeacherFacade : ITeacherFacade
{
    private readonly IMediator _mediator;

    public TeacherFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Register(RegisterTeacherCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AcceptRequest(AcceptTeacherRequestCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> RejectRequest(RejectTeacherRequestCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> ToggleStatus(Guid teacherId)
    {
        return await _mediator.Send(new ToggleTeacherStatusCommand(teacherId));

    }

    public async Task<TeacherDto?> GetById(Guid id)
    {
        return await _mediator.Send(new GetTeacherByIdQuery(id));
    }

    public async Task<TeacherDto?> GetByUserId(Guid userId)
    {
        return await _mediator.Send(new GetTeacherByUserIdQuery(userId));

    }

    public async Task<List<TeacherDto>> GetList()
    {
        return await _mediator.Send(new GetTeacherListQuery());
    }

    public async Task<TeacherDto?> GetByUserName(string username)
    {
        return await _mediator.Send(new GetTeacherByUserNameQuery(username));
    }
}