﻿using AspnetCoreMessageIdentity.DAL.Entities;

namespace AspnetCoreMessageIdentity.Models.MessageModels
{
    public class ForwadMailViewModel
    {
        public int ForwadMailViewModelID { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
    }
}
