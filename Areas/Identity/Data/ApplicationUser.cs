﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StaffTraffic.Areas.Identity.Data;

// Add profile data for application users by adding properties to the User class
public class ApplicationUser : IdentityUser<Guid>
{
    [MaxLength(256)]
    public string FirstName { get; set; }

    [MaxLength(256)]
    public string LastName { get; set; }

    [DefaultValue(true)]
    public bool IsEnable { get; set; }
}

