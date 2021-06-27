﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AssignmentManager.Shared;

namespace AssignmentManager.Server.Models
{
    public class Student
    {
        [Key] public int IsuId { get; set; }
        [Required] public string Name { get; set; }
        public string MiddleName { get; set; }
        [Required] public string Lastname { get; set; }
        [Required] public string Email { get; set; }
        public string Phone { get; set; }

        public int GroupId { get; set; }
        [Required] public Group Group { get; set; }

        //TODO: rework
        public virtual IList<Solution> Solutions { get; set; }

        public Student()
        {
            Solutions = new List<Solution>();
        }

        public Student(SaveStudentResource studentResource)
        {
            int asId = 0;
            if (studentResource.GroupId.HasValue)
                asId = studentResource.GroupId.Value;
            IsuId = studentResource.IsuId;
            Name = studentResource.Name;
            MiddleName = studentResource.MiddleName;
            Lastname = studentResource.LastName;
            Email = studentResource.Email;
            Phone = studentResource.Phone;
            GroupId = asId;
        }
    }
}