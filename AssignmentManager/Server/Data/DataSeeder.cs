﻿using System;
using System.Collections.Generic;
using AssignmentManager.Server.Models;
using AssignmentManager.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssignmentManager.Server.Data
{
    public class DataSeeder
    {
        public static void SeedData(ModelBuilder builder)
        {
            Speciality[] specs =
            {
                new Speciality
                    {Id = 100, Code = "09.03.02", EnumStudyType = EStudyType.Bach },
                new Speciality
                    {Id = 101, Code = "09.03.02", EnumStudyType = EStudyType.Mast }
            };

            Group[] groups =
            {
                new Group { Id = 100, Name = "M11", SpecialityId = specs[0].Id },
                new Group { Id = 101, Name = "M12", SpecialityId = specs[0].Id },
                new Group { Id = 102, Name = "M21", SpecialityId = specs[1].Id },
                new Group { Id = 103, Name = "M22", SpecialityId = specs[1].Id }
            };

            Student[] students = new Student[8];
            int groupIndex = 0;
            for (int id = 0; id < 8; ++id)
            {
                string name = id % 2 == 0 ? "Alex" : "Bob";
                students[id] = new Student
                {
                    IsuId = id + 100,
                    Email = "student@hotmail.com",
                    Name = name,
                    Lastname = name + "son",
                    GroupId = groupIndex + 100,
                };

                if (id % 2 == 1)
                {
                    ++groupIndex;
                }
            }

            Instructor[] instructors =
            {
                new Instructor { IsuId = 111114, LastName = "Mavrin", FirstName = "Pavel", Email = "e@mail.ru"},
                new Instructor { IsuId = 111115, LastName = "Priiskalov", FirstName = "Roman", PatronymicName = "Andreevich", Email = "e@mail.ru"},
                new Instructor { IsuId = 111113, LastName = "Beresnev", FirstName = "Artem", PatronymicName = "Dmitrievich", Email = "e@mail.ru"},
                new Instructor { IsuId = 111112, LastName = "Mayatin", FirstName = "Alexander", PatronymicName = "Vladimirovich", Email = "e@mail.ru"}
            };

            Subject[] subjects =
            {
                new Subject { SubjectId = 1, SubjectName = "OS"},
                new Subject { SubjectId = 2, SubjectName = "DB"},
                new Subject { SubjectId = 100, SubjectName = "algorithms"},
                new Subject { SubjectId = 101, SubjectName = "advanced stuff"}
            };

            object[] assignments =
            {
                new { AssignmentId = 100, Name = "implement bubble sort", Deadline = new DateTime(2077, 11, 11), SubjectId = 1 },
                new { AssignmentId = 101, Name = "implement merge sort", Deadline = new DateTime(2077, 12, 11), SubjectId = 1 },
                new { AssignmentId = 102, Name = "get a job", Deadline = new DateTime(2077, 11, 11), SubjectId = 2 },
                new { AssignmentId = 103, Name = "do work", Deadline = new DateTime(2077, 12, 11), SubjectId = 2 }
            };

            object[] solutions = 
            {
                new { SolutionId = 100, Content = "github link", AssignmentId = 100 },
                new { SolutionId = 101, Content = "github link", AssignmentId = 101 },
                new { SolutionId = 102, Content = "github link", AssignmentId = 101 },
                new { SolutionId = 103, Content = "github link", AssignmentId = 102 },
                new { SolutionId = 104, Content = "github link", AssignmentId = 103 },
                new { SolutionId = 105, Content = "github link", AssignmentId = 103 },
            };
            
            object[] specialitiesSubjects =
            {
                new {SpecialitiesId = specs[0].Id, SubjectsSubjectId = subjects[0].SubjectId},
                new {SpecialitiesId = specs[1].Id, SubjectsSubjectId = subjects[1].SubjectId}
            };

            object[] solutionsStudents =
            {
                //"StudentsIsuId", "SolutionsSolutionId"
                new { StudentsIsuId = students[0].IsuId, SolutionsSolutionId = 100 },
                new { StudentsIsuId = students[0].IsuId, SolutionsSolutionId = 101 },
                new { StudentsIsuId = students[0].IsuId, SolutionsSolutionId = 102 },
                new { StudentsIsuId = students[1].IsuId, SolutionsSolutionId = 100 },
                new { StudentsIsuId = students[1].IsuId, SolutionsSolutionId = 101 },
                new { StudentsIsuId = students[2].IsuId, SolutionsSolutionId = 103 },
                new { StudentsIsuId = students[3].IsuId, SolutionsSolutionId = 104 },
                new { StudentsIsuId = students[4].IsuId, SolutionsSolutionId = 105 }
            };
            InstructorSubject[] instructorSubjects =
            {
                new InstructorSubject { Id = 1, IsuId = 111112, SubjectId = 1},
                new InstructorSubject { Id = 2, IsuId = 111112, SubjectId = 2},

            };
            
            builder.Entity<Speciality>().HasData(specs);
            builder.Entity<Group>().HasData(groups);
            builder.Entity<Student>().HasData(students);
            builder.Entity<Instructor>().HasData(instructors);
            builder.Entity<Subject>().HasData(subjects);
            builder.Entity<Assignment>().HasData(assignments);
            builder.Entity<Solution>().HasData(solutions);
            builder.Entity<InstructorSubject>().HasData(instructorSubjects);
            builder.Entity<Subject>()
                .HasMany(t => t.Specialities)
                .WithMany(t => t.Subjects)
                .UsingEntity<Dictionary<string, object>>(
                    "SpecialitySubject",
                    r => r.HasOne<Speciality>().WithMany().HasForeignKey("SpecialitiesId"),
                    l => l.HasOne<Subject>().WithMany().HasForeignKey("SubjectsSubjectId"),
                    je =>
                    {
                        je.HasKey("SpecialitiesId", "SubjectsSubjectId");
                        je.HasData(specialitiesSubjects);
                    }
                );
            builder.Entity<Solution>()
                .HasMany(t => t.Students)
                .WithMany(t => t.Solutions)
                .UsingEntity<Dictionary<string, object>>(
                    "SolutionStudent",
                    r => r.HasOne<Student>().WithMany().HasForeignKey("StudentsIsuId"),
                    l => l.HasOne<Solution>().WithMany().HasForeignKey("SolutionsSolutionId"),
                    je =>
                    {
                        je.HasKey("StudentsIsuId", "SolutionsSolutionId");
                        je.HasData(solutionsStudents);
                    }
                );
            
            ApplicationUser user = new ApplicationUser
            {
                Id = "932b7349-cc72-4217-9fc5-e421ee27f693",
                UserName = "BigBoss",
                Email = "qwer@qwer.qwer",
                EmailConfirmed = true,
                PhoneNumber = "88005553535"
            };

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            passwordHasher.HashPassword(user, "QWERty.123");

            builder.Entity<ApplicationUser>().HasData(user);
        }
    }
}