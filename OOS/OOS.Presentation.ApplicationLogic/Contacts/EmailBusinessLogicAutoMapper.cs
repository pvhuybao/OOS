﻿using AutoMapper;
using OOS.Domain.Contacts.Models;
using OOS.Presentation.ApplicationLogic.Contacts.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace OOS.Presentation.ApplicationLogic.Contacts
{
    public class EmailBusinessLogicAutoMapper : Profile
    {
        public EmailBusinessLogicAutoMapper()
        {
            CreateMap<CreateEmailSubscribeRequest, EmailSubsribe>();
        }
    }
}