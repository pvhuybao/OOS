﻿using OOS.Presentation.ApplicationLogic.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Categories.Messages
{
    public class GetCategoryRequest : RequestBase
    {
        public string Id { get; set; }
    }
}