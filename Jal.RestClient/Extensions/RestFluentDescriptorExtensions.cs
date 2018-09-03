﻿using System;
using System.Threading.Tasks;
using Jal.HttpClient.Model;
using Jal.RestClient.Interface.Fluent;
using Jal.RestClient.Model;

namespace Jal.RestClient.Extensions
{
    public static class RestFluentDescriptorExtensions
    { 
        public static RestResponse Send(this IRestSenderDescriptor descriptor, string id, string parentid=null, string operationid=null)
        {
            return descriptor.Send(new HttpIdentity(id) { ParentId=parentid, OperationId = operationid });
        }

        public static Task<RestResponse> SendAsync(this IRestSenderDescriptor descriptor, string id, string parentid = null, string operationid = null)
        {
            return descriptor.SendAsync(new HttpIdentity(id) { ParentId = parentid, OperationId = operationid });
        }

        public static RestResponse<T> Send<T>(this IRestSenderDescriptor<T> descriptor, string id, string parentid = null, string operationid = null)
        {
            return descriptor.Send(new HttpIdentity(id) { ParentId = parentid, OperationId = operationid });
        }

        public static Task<RestResponse<T>> SendAsync<T>(this IRestSenderDescriptor<T> descriptor, string id, string parentid = null, string operationid = null)
        {
            return descriptor.SendAsync(new HttpIdentity(id) { ParentId = parentid, OperationId = operationid });
        }

        public static IRestMapDescriptor FormUrlEncodedData(this IRestContentDescriptor descriptor, string data)
        {
            return descriptor.Data(data, "application/x-www-form-urlencoded");
        }

        public static IRestMapDescriptor MultipartFormData(this IRestContentDescriptor descriptor, string data)
        {
            return descriptor.Data(data, "multipart/form-data");
        }

        public static IRestMapDescriptor XmlData(this IRestContentDescriptor descriptor, string data)
        {
            return descriptor.Data(data, "text/xml");
        }

        public static IRestMapDescriptor XmlData<T>(this IRestContentDescriptor descriptor, T data, Func<T, string> converter)
        {
            return descriptor.Data(data, converter, "text/xml");
        }

        public static IRestWhenDescriptor<T> MapXmlTo<T>(this IRestMapDescriptor descriptor, Func<string, T> converter)
        {
            return descriptor.MapTo(converter, "text/xml");
        }
    }
}