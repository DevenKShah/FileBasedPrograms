#:package Humanizer@3.0.1

using Humanizer;


CourseType courseType = CourseType.ApprenticeshipUnit;

Console.WriteLine($"{courseType} Manage your {courseType}".Humanize().Pluralize());

Console.WriteLine($"Select a {courseType}".Humanize());

public enum CourseType
{
    Apprenticeship,
    ApprenticeshipUnit,
    Bootstrap
}
