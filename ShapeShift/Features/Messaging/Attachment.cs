﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
/*
namespace ShapeShift.Features.Messaging
{
    public class Attachment
    {
        public Attachment (string fileName, object content)
        {
            Content = content;
            FileName = fileName;
            Type = AttachmentType.Text;
        }
        public enum AttachmentType
        {
            Json,
            Text
        }

        public object Content { get; set; }
        public string FileName { get; set; }
        public AttachmentType Type { get; set; }

        public async Task<MemoryStream> ContentToStreamAsync()
        {
            string text;
            switch (Type)
            {
                case AttachmentType.Json
                    text = Newtonsoft.Json.JsonConvert.SerializeObject(Content);
                    break;
                case AttachmentType.Text:
                    text = Content.ToString();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream, Encoding.UTF8);
            //gitTrubz
        }
    }
}

*/