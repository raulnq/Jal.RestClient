﻿using System;
using System.Net;
using Jal.RestClient.Impl;
using Jal.RestClient.Interface;

namespace Jal.RestClient.Fluent
{
    public static class RestHandlerDescriptorExtensions
    {
        public static IContentDescriptor Json(this IContentDescriptor descriptor, string content)
        {
            descriptor.WithContentType("application/json");
            descriptor.WithContent(content);
            return descriptor;
        }

        public static IContentDescriptor Json<T>(this IContentDescriptor descriptor, T content, Func<T, string> converter)
        {
            descriptor.WithContentType("application/json");
            descriptor.WithContent<T>(content, converter);
            return descriptor;
        }

        public static IContentDescriptor Xml(this IContentDescriptor descriptor, string content)
        {
            descriptor.WithContentType("text/xml");
            descriptor.WithContent(content);
            return descriptor;
        }

        public static IContentDescriptor Xml<T>(this IContentDescriptor descriptor, T content, Func<T, string> converter)
        {
            descriptor.WithContentType("text/xml");
            descriptor.WithContent<T>(content, converter);
            return descriptor;
        }

        public static IContentDescriptor FormUrlEncoded(this IContentDescriptor descriptor, string content)
        {
            descriptor.WithContentType("application/x-www-form-urlencoded");
            descriptor.WithContent(content);
            return descriptor;
        }

        public static IContentDescriptor FormUrlEncoded<T>(this IContentDescriptor descriptor, T content, Func<T, string> converter)
        {
            descriptor.WithContentType("application/x-www-form-urlencoded");
            descriptor.WithContent<T>(content, converter);
            return descriptor;
        }

        public static IContentDescriptor MultipartFormData(this IContentDescriptor descriptor, string content)
        {
            descriptor.WithContentType("multipart/form-data");
            descriptor.WithContent(content);
            return descriptor;
        }

        public static IContentDescriptor MultipartFormData<T>(this IContentDescriptor descriptor, T content, Func<T, string> converter)
        {
            descriptor.WithContentType("multipart/form-data");
            descriptor.WithContent<T>(content, converter);
            return descriptor;
        }

        public static IAcceptedTypeDescriptor JsonAcceptedType(this IAcceptedTypeDescriptor descriptor)
        {
            descriptor.WithAcceptedType("application/json");
            return descriptor;
        }

        public static IAcceptedTypeDescriptor XmlAcceptedType(this IAcceptedTypeDescriptor descriptor)
        {
            descriptor.WithAcceptedType("text/xml");
            return descriptor;
        }

        public static IAuthenticatorDescriptor BearerTokenAuthenticator(this IAuthenticatorDescriptor descriptor, string token)
        {
            var auth = new BearerTokenAuthenticator(token);
            descriptor.WithAuthenticator(auth);
            return descriptor;
        }

        public static IAuthenticatorDescriptor BasicHttpAuthenticator(this IAuthenticatorDescriptor descriptor, string user, string password)
        {
            var auth = new BasicHttpAuthenticator(user, password);
            descriptor.WithAuthenticator(auth);
            return descriptor;
        }
    }
}