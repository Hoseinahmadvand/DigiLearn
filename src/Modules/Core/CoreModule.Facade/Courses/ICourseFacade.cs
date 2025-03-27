﻿using Common.Application;
using CoreModule.Application.Courses.Create;
using CoreModule.Application.Courses.Edit;
using CoreModule.Application.Courses.Episodes.Accept;
using CoreModule.Application.Courses.Episodes.Add;
using CoreModule.Application.Courses.Episodes.Delete;
using CoreModule.Application.Courses.Episodes.Edit;
using CoreModule.Application.Courses.Sections.AddSection;
using CoreModule.Query.Courses._DTOs;
using CoreModule.Query.Courses.Episodes.GetById;
using CoreModule.Query.Courses.GetByFilter;
using CoreModule.Query.Courses.GetById;
using CoreModule.Query.Courses.GetBySlug;
using MediatR;

namespace CoreModule.Facade.Courses;

public interface ICourseFacade
{
    Task<OperationResult> Create(CreateCourseCommand command);
    Task<OperationResult> Edit(EditCourseCommand command);
    Task<OperationResult> AddSection(AddCourseSectionCommand command);
    Task<OperationResult> AddEpisode(AddCourseEpisodeCommand command);
    Task<OperationResult> AcceptEpisode(AcceptCourseEpisodeCommand command);
    Task<OperationResult> DeleteEpisode(DeleteCourseEpisodeCommand command);
    Task<OperationResult> EditEpisode(EditEpisodeCommand command);


    Task<CourseFilterResult> GetCourseFilter(CourseFilterParams param);
    Task<CourseDto?> GetCourseById(Guid id);
    Task<CourseDto?> GetCourseBySlug(string slug);
    Task<EpisodeDto?> GetEpisodeById(Guid id);

}


class CourseFacade : ICourseFacade
{
    private readonly IMediator _mediator;

    public CourseFacade(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OperationResult> Create(CreateCourseCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> Edit(EditCourseCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddSection(AddCourseSectionCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AddEpisode(AddCourseEpisodeCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> AcceptEpisode(AcceptCourseEpisodeCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> DeleteEpisode(DeleteCourseEpisodeCommand command)
    {
        return await _mediator.Send(command);
    }

    public async Task<OperationResult> EditEpisode(EditEpisodeCommand command)
    {
        return await _mediator.Send(command);

    }

    public async Task<CourseFilterResult> GetCourseFilter(CourseFilterParams param)
    {
        return await _mediator.Send(new GetCoursesByFilterQuery(param));

    }

    public async Task<CourseDto?> GetCourseById(Guid id)
    {
        return await _mediator.Send(new GetCourseByIdQuery(id));

    }

    public async Task<CourseDto?> GetCourseBySlug(string slug)
    {
        return await _mediator.Send(new GetCourseBySlugQuery(slug));

    }

    public async Task<EpisodeDto?> GetEpisodeById(Guid id)
    {
        return await _mediator.Send(new GetEpisodeByIdQuery(id));

    }
}