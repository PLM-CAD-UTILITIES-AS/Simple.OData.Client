﻿using System.Collections.Generic;
using System.Net;
using Simple.NExtLib;

namespace Simple.Data.OData
{
    public class CommandRequestBuilder : RequestBuilder
    {
        public CommandRequestBuilder(string urlBase)
            : base(urlBase)
        {
        }

        public override void AddCommandToRequest(HttpCommand command)
        {
            var uri = CreateRequestUrl(command.CommandText);
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = command.Method;
            request.ContentLength = (command.FormattedContent ?? string.Empty).Length;

            // TODO: revise
            //if (method == "PUT" || method == "DELETE" || method == "MERGE")
            //{
            //    request.Headers.Add("If-Match", "*");
            //}

            if (command.FormattedContent != null)
            {
                request.ContentType = "application/atom+xml";
                request.SetContent(command.FormattedContent);
            }

            command.Request = request;
        }

        public override int GetContentId(IDictionary<string, object> content)
        {
            return 0;
        }
    }
}