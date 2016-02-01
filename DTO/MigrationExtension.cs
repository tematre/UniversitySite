using System;
using System.Collections.Generic;
using System.Linq;
using DbStudent = Domain.University.Student;
using DbProfessor = Domain.University.Professor;
using DbMark = Domain.University.Mark;
using DbSubject = Domain.University.Subject;
namespace DTO
{
    public static class MigrationExtension
    {
        public static ResponseBase ToResponse(this IEnumerable<DbStudent> that)
        {
            var dic = new Dictionary<string, object>();
            var enumerable = that as DbStudent[] ?? that.ToArray();
            foreach (var obj in enumerable.SelectMany(a => a.Subjects))
            {
                dic[obj.Id.ToString()] = obj.ToDto();
                dic[obj.Professor.Id.ToString()] = obj.Professor.ToDto();
            }

            foreach (var obj in enumerable.SelectMany(a => a.Marks))
            {
                dic[obj.Id.ToString()] = obj.ToDto();
            }

            var listOfStudents = new List<Student>();
            foreach (var student in enumerable)
            {
                var dto = student.ToDto();
                dic[student.Id] = dto;
                listOfStudents.Add(dto);
            }

            return new ResponseBase
            {
                ObjectsDictionary = dic,
                Data = listOfStudents
            };
        }

        public static ResponseBase ToResponse(this IEnumerable<DbProfessor> that)
        {
            var dic = new Dictionary<string, object>();
            var enumerable = that as DbProfessor[] ?? that.ToArray();
            foreach (var obj in enumerable.SelectMany(a => a.Subjects))
            {
                dic[obj.Id.ToString()] = obj.ToDto();
            }

            foreach (var obj in enumerable.SelectMany(a => a.Subjects).SelectMany(a => a.Marks))
            {
                dic[obj.Id.ToString()] = obj.ToDto();
            }

            foreach (var obj in enumerable.SelectMany(a => a.Subjects).Select(a => a.Professor))
            {
                dic[obj.Id.ToString()] = obj.ToDto();
            }
            var listOfprofessors = new List<Professor>();
            foreach (var student in enumerable)
            {
                var dto = student.ToDto();
                dic[student.Id.ToString()] = dto;
                listOfprofessors.Add(dto);
            }

            return new ResponseBase
            {
                ObjectsDictionary = dic,
                Data = listOfprofessors
            };
        }



        public static ResponseBase ToResponse(this DbStudent that)
        {
            var dic = new Dictionary<string, object>();
            foreach (var obj in that.Subjects)
            {
                dic[obj.Id.ToString()] = obj.ToDto();
            }

            foreach (var obj in that.Subjects.SelectMany(a => a.Marks))
            {
                dic[obj.Id.ToString()] = obj.ToDto();
            }

            foreach (var obj in that.Subjects.Select(a => a.Professor))
            {
                dic[obj.Id.ToString()] = obj.ToDto();
            }

            dic[that.Id] = that.ToDto();

            return new ResponseBase
            {
                ObjectsDictionary = dic,
                Data = dic[that.Id]
            };

        }

        public static ResponseBase ToResponse(this DbProfessor that)
        {
            var dic = new Dictionary<string, object>();
            foreach (var obj in that.Subjects)
            {
                dic[obj.Id.ToString()] = obj.ToDto();
            }

            foreach (var obj in that.Subjects.SelectMany(a => a.Students))
            {
                dic[obj.Id] = obj.ToDto();
            }

            foreach (var obj in that.Subjects.SelectMany(a => a.Students).SelectMany(a => a.Marks))
            {
                dic[obj.Id.ToString()] = obj.ToDto();
            }

            dic[that.Id.ToString()] = that.ToDto();



            return new ResponseBase
            {
                ObjectsDictionary = dic,
                Data = dic[that.Id.ToString()]
            };

        }

        public static Student ToDto(this DbStudent that)
        {
            var result = new Student
            {
                Name = that.Name,
                MarkIds = that.Marks.Select(a => a.Id).ToList(),
                StudentId = that.Id,
                Surname = that.Surname,
                SubjectIds = that.Subjects.Select(a => a.Id).ToList(),
                AverageMarks = new Dictionary<string, double>()
            };


            foreach (var subject in that.Subjects)
            {
                var marks = that.Marks.Where(a => a.SubjectId == subject.Id).ToArray();
                var averageMark = ((double)marks.Sum(a => a.Value)) / marks.Length;

                result.AverageMarks[subject.Id.ToString()] = averageMark;
            }

            result.ComonAverageMark = ((double)that.Marks.Sum(a => a.Value)) / that.Marks.Count();

            return result;
        }
        public static Subject ToDto(this DbSubject that)
        {
            return new Subject
            {
                Name = that.Name,
                MarkIds = that.Marks.Select(a => a.Id).ToList(),
                SubjectId = that.Id,
                StudentIds = that.Students.Select(a => Guid.Parse(a.Id)).ToList(),
                ProfessorId = that.Professor.Id,
            };
        }

        public static Mark ToDto(this DbMark that)
        {
            return new Mark
            {
                Id = that.Id,
                Subject = that.Subject.Id,
                Student = that.StudentId,
                Value = that.Value
            };
        }
        public static Professor ToDto(this DbProfessor that)
        {
            return new Professor
            {
                ProfessorId = that.Id,
                Surname = that.Surname,
                Name = that.Name,
                SubjectIds = that.Subjects.Select(a => a.Id).ToList(),
                StudentsCount = that.Subjects.SelectMany(a => a.Students).Count()
            };
        }
    }
}