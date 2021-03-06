﻿//  Copyright (C) 2015, The Duplicati Team
//  http://www.duplicati.com, info@duplicati.com
//
//  This library is free software; you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as
//  published by the Free Software Foundation; either version 2.1 of the
//  License, or (at your option) any later version.
//
//  This library is distributed in the hope that it will be useful, but
//  WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//  Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public
//  License along with this library; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
using System;using System.Collections.Generic;

namespace Duplicati.Server.WebServer.RESTMethods
{
    public class ServerSetting : IRESTMethodGET, IRESTMethodPUT, IRESTMethodDocumented
    {        public void GET(string key, RequestInfo info)        {            var prop = typeof(Database.ApplicationSettings).GetProperty(key);            if (prop == null)                info.OutputError(null, System.Net.HttpStatusCode.NotFound, "Not found");            else                info.OutputOK(prop.GetValue(Program.DataConnection.ApplicationSettings));        }        public void PUT(string key, RequestInfo info)        {            var prop = typeof(Database.ApplicationSettings).GetProperty(key);            if (prop == null)                info.OutputError(null, System.Net.HttpStatusCode.NotFound, "Not found");            else            {                var dict = new Dictionary<string, string>();                dict[key] = info.Request.Form["data"].Value;                Program.DataConnection.ApplicationSettings.UpdateSettings(dict);                info.OutputOK();            }        }        public string Description { get { return "Return a list of settings for the server"; } }        public IEnumerable<KeyValuePair<string, Type>> Types        {            get            {                return new KeyValuePair<string, Type>[] {                    new KeyValuePair<string, Type>(HttpServer.Method.Get, typeof(string)),                    new KeyValuePair<string, Type>(HttpServer.Method.Put, typeof(string))                };            }        }    
    }
}

