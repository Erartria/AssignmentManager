﻿using System.Collections.Generic;

namespace AssignmentManager.Server.Resources
{
    public class InstructorResource
    {
        public int IsuId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PatronymicName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public IList<SubjectResource> Subjects { get; set; }
    }
}