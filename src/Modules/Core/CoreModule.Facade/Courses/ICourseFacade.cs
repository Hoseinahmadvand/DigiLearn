﻿using Common.Application;
using CoreModule.Application.Courses.Create;
using CoreModule.Application.Courses.Edit;
using CoreModule.Application.Courses.Episodes.Accept;
using CoreModule.Application.Courses.Episodes.Add;
using CoreModule.Application.Courses.Episodes.Delete;
using CoreModule.Application.Courses.Episodes.Edit;
using CoreModule.Application.Courses.Sections.AddSection;
using CoreModule.Application.HelperEntities.CourseStudents.Create;
using CoreModule.Application.HelperEntities.CourseStudents.Delete;
using CoreModule.Query.Courses._DTOs;
using CoreModule.Query.Courses.Episodes.GetById;
using CoreModule.Query.Courses.GetByFilter;
using CoreModule.Query.Courses.GetById;
using CoreModule.Query.Courses.GetBySlug;
using CoreModule.Query.HelperEntities._DTOs;
using CoreModule.Query.HelperEntities.GetStudentsCours;
using CoreModule.Query.HelperEntities.GetUserCourses;
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

    Task<OperationResult> AddStudent(Guid courseId, Guid userId);
    Task<OperationResult> DeleteStudent(Guid courseId, Guid userId);

    Task<List<CourseFilterData?>> GetUserCourses(Guid userId);
    Task<List<StudentDto?>> GetStudentsCours(Guid coursId);


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


    public async Task<OperationResult> AddStudent(Guid courseId, Guid userId)
    {
        return await _mediator.Send(new CreateCourseStudentCommand(courseId, userId));

    }

    public async Task<OperationResult> DeleteStudent(Guid courseId, Guid userId)
    {
        return await _mediator.Send(new DeleteCourseStudentCommand(courseId, userId));

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

    public async Task<List<CourseFilterData?>> GetUserCourses(Guid userId)
    {
        return await _mediator.Send(new GetUserCoursesQuery(userId));
      
    }

    public async Task<List<StudentDto?>> GetStudentsCours(Guid coursId)
    {
        return await _mediator.Send(new GetStudentsCoursQuery(coursId));
    }
}