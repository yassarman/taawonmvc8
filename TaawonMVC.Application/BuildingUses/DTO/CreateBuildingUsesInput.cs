﻿using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaawonMVC.BuildingUses.DTO
{   
    [AutoMap(typeof(Models.BuildingUses))]
 public class CreateBuildingUsesInput
    {   
       // [Required]
      //  public int id { get; set; }

        [Required]
        public string UsedFor { get; set; }
    }
}
