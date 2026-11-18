using GradePilot.Domain.Common;

namespace GradePilot.Domain.Entities;

public sealed class Semester
{
    private readonly List<GradedCourse> _courses = [];

    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; }

    public int AcademicYear { get; private set; }

    public int SemesterNumber { get; private set; }

    public IReadOnlyCollection<GradedCourse> Courses =>
        _courses.AsReadOnly();

    // Used by EF Core when materializing from the database.
    // Application code should create semesters through Semester.Create.
    public Semester(
        string name,
        int academicYear,
        int semesterNumber)
    {
        Name = name;
        AcademicYear = academicYear;
        SemesterNumber = semesterNumber;
    }

    public static Result<Semester> Create(
        string name,
        int academicYear,
        int semesterNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Fail<Semester>("Semester name is required.");

        if (academicYear < 2000)
            return Result.Fail<Semester>("Academic year must be 2000 or later.");

        if (semesterNumber is < 1 or > 3)
            return Result.Fail<Semester>("Semester number must be between 1 and 3.");

        return Result.Ok(new Semester(name, academicYear, semesterNumber));
    }

    public Result<Unit> AddCourse(GradedCourse course)
    {
        ArgumentNullException.ThrowIfNull(course);

        if (_courses.Any(c =>
            string.Equals(
                c.CourseName,
                course.CourseName,
                StringComparison.OrdinalIgnoreCase)))
        {
            return Result.Fail<Unit>("This course already exists in this semester.");
        }

        _courses.Add(course);
        return Result.Ok(Unit.Value);
    }

    public Result<Unit> RemoveCourse(string courseName)
    {
        var course = _courses.FirstOrDefault(c =>
            string.Equals(
                c.CourseName,
                courseName,
                StringComparison.OrdinalIgnoreCase));

        if (course is null)
            return Result.Fail<Unit>("Course not found in this semester.");

        _courses.Remove(course);
        return Result.Ok(Unit.Value);
    }
}
