using GradePilot.Domain.Entities;

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

    public Semester(
        string name,
        int academicYear,
        int semesterNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Semester name is required.");

        if (academicYear < 2000)
            throw new ArgumentOutOfRangeException(nameof(academicYear));

        if (semesterNumber < 1 || semesterNumber > 3)
            throw new ArgumentOutOfRangeException(nameof(semesterNumber));

        Name = name;
        AcademicYear = academicYear;
        SemesterNumber = semesterNumber;
    }

    public void AddCourse(GradedCourse course)
    {
        ArgumentNullException.ThrowIfNull(course);

        if (_courses.Any(c =>
            string.Equals(
                c.CourseName,
                course.CourseName,
                StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException(
                "This course already exists in this semester.");
        }

        _courses.Add(course);
    }

    public bool RemoveCourse(string courseName)
    {
        var course = _courses.FirstOrDefault(c =>
            string.Equals(
                c.CourseName,
                courseName,
                StringComparison.OrdinalIgnoreCase));

        return course is not null && _courses.Remove(course);
    }
}